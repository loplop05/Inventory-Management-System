using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace InventoryManagementSystem
{
    public class AuditEntry
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public string Module { get; set; }
        public string User { get; set; }

        public AuditEntry()
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
            Timestamp = DateTime.Now;
            User = Environment.UserName;
        }
    }

    /// <summary>
    /// Enterprise Audit Logging & Activity Tracking Manager.
    /// Provides real-time activity recording, searching, filtering, and export capabilities.
    /// </summary>
    public static class clsAuditLog
    {
        private static readonly string LogFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "AuditLogs.json");

        private static readonly List<AuditEntry> _logs = new List<AuditEntry>();
        private static readonly object _lock = new object();

        static clsAuditLog()
        {
            LoadLogs();
        }

        /// <summary>
        /// Logs a system action with details and module categorization.
        /// </summary>
        public static void LogAction(string action, string details, string module = "System")
        {
            lock (_lock)
            {
                var entry = new AuditEntry
                {
                    Action = action,
                    Details = details,
                    Module = module,
                    Timestamp = DateTime.Now,
                    User = Environment.UserName
                };

                _logs.Insert(0, entry);
                SaveLogs();
            }
        }

        /// <summary>
        /// Logs an error to the audit trail and the on-disk log file.
        /// </summary>
        public static void LogError(string source, Exception ex)
        {
            string details = source + ": " + (ex == null ? "Unknown error" : ex.Message);
            LogAction("Error", details, "System");
        }

        /// <summary>
        /// Retrieves filtered audit logs.
        /// </summary>
        public static List<AuditEntry> GetLogs(string moduleFilter = null, string searchKeyword = null)
        {
            lock (_lock)
            {
                IEnumerable<AuditEntry> query = _logs;

                if (!string.IsNullOrWhiteSpace(moduleFilter) && moduleFilter != "All")
                {
                    query = query.Where(l => string.Equals(l.Module, moduleFilter, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(searchKeyword))
                {
                    string k = searchKeyword.Trim().ToLower();
                    query = query.Where(l => (l.Action != null && l.Action.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                             (l.Details != null && l.Details.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                             (l.User != null && l.User.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                return query.ToList();
            }
        }

        /// <summary>
        /// Clears all audit logs.
        /// </summary>
        public static void ClearLogs()
        {
            lock (_lock)
            {
                _logs.Clear();
                SaveLogs();
            }
        }

        /// <summary>
        /// Exports audit log entries to CSV format.
        /// </summary>
        public static string ExportToCSV(List<AuditEntry> entries, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID,Timestamp,Module,Action,User,Details");

            foreach (var log in entries)
            {
                string safeDetails = "\"" + (log.Details ?? "").Replace("\"", "\"\"") + "\"";
                string safeAction = "\"" + (log.Action ?? "").Replace("\"", "\"\"") + "\"";
                sb.AppendLine($"{log.Id},{log.Timestamp:yyyy-MM-dd HH:mm:ss},{log.Module},{safeAction},{log.User},{safeDetails}");
            }

            var utf8WithBom = new UTF8Encoding(true);
            File.WriteAllText(filePath, sb.ToString(), utf8WithBom);
            return filePath;
        }

        private static void LoadLogs()
        {
            try
            {
                if (File.Exists(LogFilePath))
                {
                    string json = File.ReadAllText(LogFilePath, Encoding.UTF8);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var loaded = serializer.Deserialize<List<AuditEntry>>(json);
                    if (loaded != null)
                    {
                        _logs.Clear();
                        _logs.AddRange(loaded.OrderByDescending(l => l.Timestamp));
                    }
                }
            }
            catch
            {
                // Fallback to empty list if file corrupt
                _logs.Clear();
            }
        }

        private static void SaveLogs()
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(_logs);
                File.WriteAllText(LogFilePath, json, Encoding.UTF8);
            }
            catch
            {
                // Ignore background save errors
            }
        }
    }
}
