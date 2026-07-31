using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public enum AppLanguage
    {
        English,
        Arabic
    }

    /// <summary>
    /// Centralized Language and Localization Manager.
    /// Manages English and Arabic UI translations, RTL layout transformation, and language change events.
    /// </summary>
    public static class clsLanguageManager
    {
        public static AppLanguage CurrentLanguage { get; private set; } = AppLanguage.English;

        public static event EventHandler LanguageChanged;

        private static readonly Dictionary<string, (string En, string Ar)> _translations =
            new Dictionary<string, (string En, string Ar)>(StringComparer.OrdinalIgnoreCase)
            {
                // General & Navigation
                { "Inventory Management System", ("Inventory Management System", "نظام إدارة المخزون") },
                { "Categories", ("Categories", "الأقسام") },
                { "Suppliers", ("Suppliers", "الموردين") },
                { "Products", ("Products", "المنتجات") },
                { "Receipt Search", ("Receipt Search", "البحث عن الفواتير") },
                { "Print Receipt", ("Print Receipt", "طباعة الفاتورة") },
                { "Dashboard", ("Dashboard", "لوحة التحكم") },
                { "Advanced Reports", ("Advanced Reports", "التقارير المتقدمة") },
                { "Low Stock Alerts", ("Low Stock Alerts", "تنبيهات نقص المخزون") },
                { "Coupon Manager", ("Coupon Manager", "إدارة الكوبونات") },
                { "Point of Sale", ("Point of Sale", "نقطة البيع (POS)") },
                { "Daily Report", ("Daily Report", "التقرير اليومي") },
                { "Audit Logs", ("Audit Logs", "سجلات المراجعة والنظام") },
                { "Help", ("Help", "المساعدة") },
                { "Language", ("Language", "اللغة") },
                { "English", ("English", "الإنجليزية") },
                { "Arabic", ("Arabic", "العربية") },
                { "Close", ("Close", "إغلاق") },
                { "Save", ("Save", "حفظ") },
                { "Add", ("Add", "إضافة") },
                { "Edit", ("Edit", "تعديل") },
                { "Update", ("Update", "تحديث") },
                { "Delete", ("Delete", "حذف") },
                { "Search", ("Search", "بحث") },
                { "Clear", ("Clear", "مسح") },
                { "Refresh", ("Refresh", "تحديث") },
                { "Export", ("Export", "تصدير") },
                { "Print", ("Print", "طباعة") },
                { "Back", ("Back", "رجوع") },
                { "Cancel", ("Cancel", "إلغاء") },
                { "Action", ("Action", "إجراء") },
                { "Status", ("Status", "الحالة") },
                { "Date", ("Date", "التاريخ") },
                { "Details", ("Details", "التفاصيل") },

                // Category Form Strings
                { "Categories Management", ("Categories Management", "إدارة الأقسام والفيات") },
                { "Category Name", ("Category Name", "اسم القسم") },
                { "Category ID", ("Category ID", "معرف القسم") },
                { "Add Category", ("Add Category", "إضافة قسم جديد") },
                { "Update Category", ("Update Category", "تحديث قسم") },
                { "Delete Category", ("Delete Category", "حذف قسم") },

                // Supplier Form Strings
                { "Suppliers Management", ("Suppliers Management", "إدارة الموردين") },
                { "Supplier Name", ("Supplier Name", "اسم المورد") },
                { "Phone Number", ("Phone Number", "رقم الهاتف") },
                { "Email Address", ("Email Address", "البريد الإلكتروني") },

                // Product Form Strings
                { "Products Management", ("Products Management", "إدارة المنتجات والمخزون") },
                { "Product Name", ("Product Name", "اسم المنتج") },
                { "Barcode", ("Barcode", "الباركود") },
                { "Price", ("Price", "السعر") },
                { "Quantity", ("Quantity", "الكمية") },
                { "Select Category", ("Select Category", "اختر القسم") },
                { "Select Supplier", ("Select Supplier", "اختر المورد") },

                // POS Form Strings
                { "Point of Sale (POS)", ("Point of Sale (POS)", "شاشة نقطة البيع (POS)") },
                { "Subtotal", ("Subtotal", "المجموع الفرعي") },
                { "Tax", ("Tax", "الضريبة") },
                { "Total", ("Total", "الإجمالي") },
                { "Checkout", ("Checkout", "إتمام الدفع والدفع") },
                { "Apply Coupon", ("Apply Coupon", "تطبيق الكوبون") },
                { "Customer", ("Customer", "العميل") },
                { "Payment Method", ("Payment Method", "طريقة الدفع") },

                // Messages & Toast Notifications
                { "Success", ("Success", "نجاح") },
                { "Warning", ("Warning", "تنبيه") },
                { "Error", ("Error", "خطأ") },
                { "Operation completed successfully.", ("Operation completed successfully.", "تمت العملية بنجاح.") },
                { "Product added successfully.", ("Product added successfully.", "تم إضافة المنتج بنجاح.") },
                { "Product updated successfully.", ("Product updated successfully.", "تم تحديث المنتج بنجاح.") },
                { "Product deleted successfully.", ("Product deleted successfully.", "تم حذف المنتج بنجاح.") },
                { "Invalid input data.", ("Invalid input data.", "البيانات المدخلة غير صالحة.") },
                { "This field is required.", ("This field is required.", "هذا الحقل مطلوب.") },
                { "Please select a valid option.", ("Please select a valid option.", "يرجى تحديد خيار صحيح.") }
            };

        /// <summary>
        /// Sets current application language and notifies open forms.
        /// </summary>
        public static void SetLanguage(AppLanguage language)
        {
            CurrentLanguage = language;
            string cultureCode = (language == AppLanguage.Arabic) ? "ar-SA" : "en-US";
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            // Log language change event to Audit Log
            clsAuditLog.LogAction("Language Changed", $"System language switched to {language}", "System");

            LanguageChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Toggles between English and Arabic.
        /// </summary>
        public static AppLanguage ToggleLanguage()
        {
            AppLanguage next = (CurrentLanguage == AppLanguage.English) ? AppLanguage.Arabic : AppLanguage.English;
            SetLanguage(next);
            return next;
        }

        /// <summary>
        /// Retrieves localized string for given key. If key not found, returns key.
        /// </summary>
        public static string GetString(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return key;

            if (_translations.TryGetValue(key.Trim(), out var entry))
            {
                return (CurrentLanguage == AppLanguage.Arabic) ? entry.Ar : entry.En;
            }

            return key;
        }

        /// <summary>
        /// Applies language (text translation & RTL layout) to a form and all child controls.
        /// </summary>
        public static void ApplyLanguage(Form form)
        {
            if (form == null || form.IsDisposed) return;

            bool isArabic = (CurrentLanguage == AppLanguage.Arabic);

            // RTL setup
            form.RightToLeft = isArabic ? RightToLeft.Yes : RightToLeft.No;
            form.RightToLeftLayout = isArabic;

            if (!string.IsNullOrEmpty(form.Text))
            {
                form.Text = GetString(form.Text);
            }

            TranslateControlCollection(form.Controls, isArabic);
        }

        private static void TranslateControlCollection(Control.ControlCollection controls, bool isArabic)
        {
            if (controls == null) return;

            foreach (Control control in controls)
            {
                // Translate control text if non-empty
                if (!string.IsNullOrWhiteSpace(control.Text) && !(control is TextBox))
                {
                    control.Text = GetString(control.Text);
                }

                // Adjust text alignment for labels and textboxes if appropriate
                if (control is Label lbl && lbl.TextAlign != ContentAlignment.MiddleCenter)
                {
                    lbl.TextAlign = isArabic ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft;
                }

                if (control is DataGridView dgv)
                {
                    dgv.RightToLeft = isArabic ? RightToLeft.Yes : RightToLeft.No;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (!string.IsNullOrEmpty(col.HeaderText))
                        {
                            col.HeaderText = GetString(col.HeaderText);
                        }
                    }
                }

                if (control.HasChildren)
                {
                    TranslateControlCollection(control.Controls, isArabic);
                }
            }
        }
    }
}
