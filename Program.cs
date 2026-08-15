using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryDataAccessLayer;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // System tests disabled for now - clsSystemTests class needs to be implemented
            /*
            if (args != null && args.Length > 0 && args[0].ToLower() == "--test")
            {
                var (passed, failed, messages) = clsSystemTests.RunAllTests();
                Console.WriteLine("========================================");
                Console.WriteLine($"System Test Results: {passed} PASSED, {failed} FAILED");
                Console.WriteLine("========================================");
                foreach (var msg in messages)
                {
                    Console.WriteLine(msg);
                }
                Environment.ExitCode = (failed == 0) ? 0 : 1;
                return;
            }
            */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set explicit culture for consistent currency formatting (Jordanian Dinar)
            var culture = new CultureInfo("ar-JO");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Ensure database migrations are applied
            string migrationError;
            if (!clsDatabaseMigration.EnsureShiftsTablesExist(out migrationError))
            {
                MessageBox.Show("Database migration failed: " + migrationError, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Show login form first
            using (var loginForm = new frmLogin())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return; // User cancelled login
                }
            }

            // After successful login, check if user needs to open a shift (for cashiers and managers)
            if (clsUserManagement.CurrentUser != null && (clsUserManagement.IsCashier || clsUserManagement.IsManager))
            {
                // Check if user already has an open shift
                if (!clsShift.HasOpenShift(clsUserManagement.CurrentUser.UserID))
                {
                    using (var openShiftForm = new frmOpenShift())
                    {
                        openShiftForm.ShowDialog();
                        // User can choose to open shift or skip - no warning needed
                    }
                }
            }

            Application.Run(new frmMainMenu());
        }
    }
}
