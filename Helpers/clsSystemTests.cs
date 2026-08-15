using System;
using System.Collections.Generic;
using System.IO;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Automated Verification & System Test Suite.
    /// Runs tests for Arabic/English data validation, Language Manager localization, and Audit Logging.
    /// </summary>
    public static class clsSystemTests
    {
        public static (int Passed, int Failed, List<string> Messages) RunAllTests()
        {
            int passed = 0;
            int failed = 0;
            List<string> messages = new List<string>();

            void Assert(bool condition, string testName)
            {
                if (condition)
                {
                    passed++;
                    messages.Add($"[PASS] {testName}");
                }
                else
                {
                    failed++;
                    messages.Add($"[FAIL] {testName}");
                }
            }

            // 1. Data Validation Tests
            Assert(clsDataValidation.IsAlphaOnly("Product"), "DataValidation: English Alpha");
            Assert(clsDataValidation.IsAlphaOnly("منتج"), "DataValidation: Arabic Alpha");
            Assert(clsDataValidation.IsAlphanumeric("Apple iPhone 15"), "DataValidation: English Alphanumeric");
            Assert(clsDataValidation.IsAlphanumeric("آيفون ١٥ برو"), "DataValidation: Arabic Alphanumeric");
            Assert(clsDataValidation.IsValidProductName("حليب طازج 1 لتر / Fresh Milk"), "DataValidation: Mixed Arabic & English Product Name");
            Assert(clsDataValidation.IsValidCategoryName("مشروبات و عصائر"), "DataValidation: Arabic Category Name");
            Assert(clsDataValidation.IsValidSupplierName("شركة التقنية المتقدمة Ltd."), "DataValidation: Arabic & English Supplier Name");
            Assert(clsDataValidation.ContainsArabic("مرحبا World"), "DataValidation: ContainsArabic helper");

            // 2. Language Manager Tests
            clsLanguageManager.SetLanguage(AppLanguage.English);
            Assert(clsLanguageManager.CurrentLanguage == AppLanguage.English, "LanguageManager: Set English");
            Assert(clsLanguageManager.GetString("Categories") == "Categories", "LanguageManager: English GetString");

            clsLanguageManager.SetLanguage(AppLanguage.Arabic);
            Assert(clsLanguageManager.CurrentLanguage == AppLanguage.Arabic, "LanguageManager: Set Arabic");
            Assert(clsLanguageManager.GetString("Categories") == "الأقسام", "LanguageManager: Arabic GetString");
            Assert(clsLanguageManager.GetString("Products") == "المنتجات", "LanguageManager: Arabic Products Translation");

            clsLanguageManager.SetLanguage(AppLanguage.English); // Reset to default

            // 3. Audit Log Tests
            int countBefore = clsAuditLog.GetLogs().Count;
            clsAuditLog.LogAction("Unit Test Action", "Testing audit log functionality", "UnitTestModule");
            var logs = clsAuditLog.GetLogs("UnitTestModule");
            Assert(logs.Count > 0 && logs[0].Action == "Unit Test Action", "AuditLog: Record Action");

            string testCsvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestAuditExport.csv");
            clsAuditLog.ExportToCSV(logs, testCsvPath);
            Assert(File.Exists(testCsvPath), "AuditLog: Export CSV");
            if (File.Exists(testCsvPath)) File.Delete(testCsvPath);

            string logSummary = $"System Test Results: {passed} PASSED, {failed} FAILED\n" + string.Join("\n", messages);
            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults.txt"), logSummary);

            return (passed, failed, messages);
        }
    }
}
