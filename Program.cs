using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainMenu());
        }
    }
}
