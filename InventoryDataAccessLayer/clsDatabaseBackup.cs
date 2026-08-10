using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace InventoryDataAccessLayer
{
    /// <summary>
    /// Database backup and restore functionality.
    /// Supports SQL Server backup/restore operations.
    /// </summary>
    public static class clsDatabaseBackup
    {
        private static string BackupDirectory = LoadBackupDirectory();

        /// <summary>
        /// Loads the backup directory from configuration or uses default.
        /// </summary>
        private static string LoadBackupDirectory()
        {
            string backupDir = ConfigurationManager.AppSettings["BackupDirectory"];
            if (!string.IsNullOrWhiteSpace(backupDir) && Directory.Exists(backupDir))
                return backupDir;

            // Default to Backups folder in application directory
            string defaultDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            if (!Directory.Exists(defaultDir))
                Directory.CreateDirectory(defaultDir);
            
            return defaultDir;
        }

        /// <summary>
        /// Creates a full database backup.
        /// </summary>
        /// <param name="backupName">Optional custom name for the backup file.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>True if backup succeeded, false otherwise.</returns>
        public static bool CreateBackup(string backupName = null, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                string dbName = GetDatabaseName();
                string fileName = string.IsNullOrWhiteSpace(backupName) 
                    ? $"InventoryDB_{DateTime.Now:yyyyMMdd_HHmmss}.bak"
                    : $"{backupName}.bak";
                
                string backupPath = Path.Combine(BackupDirectory, fileName);

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string backupQuery = $@"
                        BACKUP DATABASE [{dbName}]
                        TO DISK = '{backupPath}'
                        WITH FORMAT,
                        MEDIANAME = 'InventoryDB_Backup',
                        NAME = 'Full Backup of InventoryDB';
                    ";

                    using (SqlCommand command = new SqlCommand(backupQuery, connection))
                    {
                        command.CommandTimeout = 300; // 5 minutes timeout
                        command.ExecuteNonQuery();
                    }
                }

                errorMessage = $"Backup created successfully: {backupPath}";
                clsAuditLog.LogAction("Database Backup", $"Backup created at {backupPath}", "System");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to create backup: " + ex.Message;
                clsErrorLog.LogException("clsDatabaseBackup.CreateBackup", ex);
                return false;
            }
        }

        /// <summary>
        /// Restores a database from a backup file.
        /// </summary>
        /// <param name="backupFilePath">Path to the backup file.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>True if restore succeeded, false otherwise.</returns>
        public static bool RestoreBackup(string backupFilePath, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!File.Exists(backupFilePath))
            {
                errorMessage = "Backup file not found: " + backupFilePath;
                return false;
            }

            try
            {
                string dbName = GetDatabaseName();

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    // Set database to single user mode to allow restore
                    string setSingleUser = $@"
                        ALTER DATABASE [{dbName}]
                        SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    ";

                    using (SqlCommand command = new SqlCommand(setSingleUser, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Restore the database
                    string restoreQuery = $@"
                        RESTORE DATABASE [{dbName}]
                        FROM DISK = '{backupFilePath}'
                        WITH REPLACE,
                        STATS = 10;
                    ";

                    using (SqlCommand command = new SqlCommand(restoreQuery, connection))
                    {
                        command.CommandTimeout = 600; // 10 minutes timeout
                        command.ExecuteNonQuery();
                    }

                    // Set database back to multi-user mode
                    string setMultiUser = $@"
                        ALTER DATABASE [{dbName}]
                        SET MULTI_USER;
                    ";

                    using (SqlCommand command = new SqlCommand(setMultiUser, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                errorMessage = $"Database restored successfully from: {backupFilePath}";
                clsAuditLog.LogAction("Database Restore", $"Database restored from {backupFilePath}", "System");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to restore backup: " + ex.Message;
                clsErrorLog.LogException("clsDatabaseBackup.RestoreBackup", ex);
                
                // Try to reset database to multi-user mode in case of error
                try
                {
                    string dbName = GetDatabaseName();
                    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                    {
                        connection.Open();
                        string setMultiUser = $@"
                            ALTER DATABASE [{dbName}]
                            SET MULTI_USER WITH ROLLBACK IMMEDIATE;
                        ";
                        using (SqlCommand command = new SqlCommand(setMultiUser, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch { }

                return false;
            }
        }

        /// <summary>
        /// Gets a list of all backup files in the backup directory.
        /// </summary>
        /// <returns>Array of backup file information.</returns>
        public static BackupFileInfo[] GetBackupFiles()
        {
            try
            {
                if (!Directory.Exists(BackupDirectory))
                    return new BackupFileInfo[0];

                var files = Directory.GetFiles(BackupDirectory, "*.bak")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.CreationTime)
                    .Select(f => new BackupFileInfo
                    {
                        FileName = f.Name,
                        FullPath = f.FullName,
                        Size = f.Length,
                        CreatedDate = f.CreationTime,
                        SizeFormatted = FormatFileSize(f.Length)
                    })
                    .ToArray();

                return files;
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("clsDatabaseBackup.GetBackupFiles", ex);
                return new BackupFileInfo[0];
            }
        }

        /// <summary>
        /// Deletes a backup file.
        /// </summary>
        /// <param name="backupFilePath">Path to the backup file.</param>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <returns>True if deletion succeeded, false otherwise.</returns>
        public static bool DeleteBackup(string backupFilePath, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (!File.Exists(backupFilePath))
                {
                    errorMessage = "Backup file not found: " + backupFilePath;
                    return false;
                }

                File.Delete(backupFilePath);
                errorMessage = "Backup deleted successfully.";
                clsAuditLog.LogAction("Backup Deletion", $"Deleted backup: {backupFilePath}", "System");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to delete backup: " + ex.Message;
                clsErrorLog.LogException("clsDatabaseBackup.DeleteBackup", ex);
                return false;
            }
        }

        /// <summary>
        /// Gets the database name from the connection string.
        /// </summary>
        private static string GetDatabaseName()
        {
            var builder = new SqlConnectionStringBuilder(clsDataAccessSettings.connectionString);
            return builder.InitialCatalog;
        }

        /// <summary>
        /// Formats file size in human-readable format.
        /// </summary>
        private static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        /// <summary>
        /// Information about a backup file.
        /// </summary>
        public class BackupFileInfo
        {
            public string FileName { get; set; }
            public string FullPath { get; set; }
            public long Size { get; set; }
            public DateTime CreatedDate { get; set; }
            public string SizeFormatted { get; set; }
        }
    }
}
