using System;
using System.IO;

namespace InventoryDataAccessLayer
{
    /// <summary>
    /// File-based error log for the data access layer (no UI dependencies).
    /// </summary>
    public static class clsErrorLog
    {
        private static readonly string LogFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "DataAccessErrors.log");

        private static readonly object Lock = new object();

        public static void LogException(string source, Exception ex)
        {
            if (ex == null)
                return;

            lock (Lock)
            {
                try
                {
                    string line = string.Format(
                        "[{0:yyyy-MM-dd HH:mm:ss}] {1}: {2}{3}",
                        DateTime.Now,
                        source,
                        ex.Message,
                        Environment.NewLine);

                    File.AppendAllText(LogFilePath, line);
                }
                catch
                {
                    // Last-resort: never throw from logging.
                }
            }
        }

        public static void LogMessage(string source, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            lock (Lock)
            {
                try
                {
                    string line = string.Format(
                        "[{0:yyyy-MM-dd HH:mm:ss}] {1}: {2}{3}",
                        DateTime.Now,
                        source,
                        message,
                        Environment.NewLine);

                    File.AppendAllText(LogFilePath, line);
                }
                catch
                {
                }
            }
        }
    }
}
