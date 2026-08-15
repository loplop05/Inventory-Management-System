using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;

namespace InventoryDataAccessLayer
{
    /// <summary>
    /// Database backup and restore functionality.
    /// Supports SQL Server backup/restore operations with scheduled backups.
    /// </summary>
    public static class clsDatabaseBackup
    {
        private static string BackupDirectory = LoadBackupDirectory();
        private static Timer _scheduledBackupTimer;
        private static bool _isScheduledBackupEnabled = false;
        private static readonly object _backupLock = new object();

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
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <param name="backupName">Optional custom name for the backup file.</param>
        /// <returns>True if backup succeeded, false otherwise.</returns>
        public static bool CreateBackup(out string errorMessage, string backupName = null)
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
                catch (Exception resetEx)
                {
                    clsErrorLog.LogException("clsDatabaseBackup.RestoreBackup_ResetMultiUser", resetEx);
                }

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

        /// <summary>
        /// Enables scheduled automatic backups.
        /// </summary>
        /// <param name="intervalHours">Backup interval in hours (default: 24 for daily)</param>
        public static void EnableScheduledBackup(int intervalHours = 24)
        {
            lock (_backupLock)
            {
                if (_isScheduledBackupEnabled)
                {
                    DisableScheduledBackup();
                }

                _scheduledBackupTimer = new Timer
                {
                    Interval = intervalHours * 60 * 60 * 1000, // Convert hours to milliseconds
                    AutoReset = true
                };

                _scheduledBackupTimer.Elapsed += ScheduledBackupCallback;
                _scheduledBackupTimer.Start();
                _isScheduledBackupEnabled = true;

                clsErrorLog.LogMessage("clsDatabaseBackup", $"Scheduled backup enabled with {intervalHours} hour interval.");
            }
        }

        /// <summary>
        /// Disables scheduled automatic backups.
        /// </summary>
        public static void DisableScheduledBackup()
        {
            lock (_backupLock)
            {
                if (_scheduledBackupTimer != null)
                {
                    _scheduledBackupTimer.Stop();
                    _scheduledBackupTimer.Elapsed -= ScheduledBackupCallback;
                    _scheduledBackupTimer.Dispose();
                    _scheduledBackupTimer = null;
                }

                _isScheduledBackupEnabled = false;
                clsErrorLog.LogMessage("clsDatabaseBackup", "Scheduled backup disabled.");
            }
        }

        /// <summary>
        /// Callback for scheduled backup timer.
        /// </summary>
        private static void ScheduledBackupCallback(object sender, ElapsedEventArgs e)
        {
            lock (_backupLock)
            {
                try
                {
                    string errorMessage;
                    bool success = CreateBackup(out errorMessage, $"Auto_{DateTime.Now:yyyyMMdd_HHmmss}");

                    if (success)
                    {
                        clsErrorLog.LogMessage("clsDatabaseBackup", $"Scheduled backup completed: {errorMessage}");
                    }
                    else
                    {
                        clsErrorLog.LogMessage("clsDatabaseBackup", $"Scheduled backup failed: {errorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsDatabaseBackup.ScheduledBackupCallback", ex);
                }
            }
        }

        /// <summary>
        /// Checks if scheduled backup is enabled.
        /// </summary>
        public static bool IsScheduledBackupEnabled => _isScheduledBackupEnabled;

        /// <summary>
        /// Cleans up old backup files based on retention policy.
        /// </summary>
        /// <param name="errorMessage">Output parameter for error messages.</param>
        /// <param name="retentionDays">Number of days to keep backups (default: 30)</param>
        /// <returns>True if cleanup succeeded, false otherwise.</returns>
        public static bool CleanupOldBackups(out string errorMessage, int retentionDays = 30)
        {
            errorMessage = string.Empty;

            try
            {
                if (!Directory.Exists(BackupDirectory))
                {
                    errorMessage = "Backup directory does not exist.";
                    return false;
                }

                DateTime cutoffDate = DateTime.Now.AddDays(-retentionDays);
                var backupFiles = Directory.GetFiles(BackupDirectory, "*.bak");
                int deletedCount = 0;

                foreach (var file in backupFiles)
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.CreationTime < cutoffDate)
                    {
                        File.Delete(file);
                        deletedCount++;
                    }
                }

                errorMessage = $"Cleaned up {deletedCount} old backup files older than {retentionDays} days.";
                clsErrorLog.LogMessage("clsDatabaseBackup", errorMessage);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = "Failed to cleanup old backups: " + ex.Message;
                clsErrorLog.LogException("clsDatabaseBackup.CleanupOldBackups", ex);
                return false;
            }
        }
    }
}
