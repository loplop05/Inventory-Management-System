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
                { "Search:", ("Search:", "بحث:") },
                { "Clear", ("Clear", "مسح") },
                { "Refresh", ("Refresh", "تحديث") },
                { "Refresh (F5)", ("Refresh (F5)", "تحديث (F5)") },
                { "Close (Esc)", ("Close (Esc)", "إغلاق (Esc)") },
                { "Export", ("Export", "تصدير") },
                { "Export CSV", ("Export CSV", "تصدير CSV") },
                { "Clear Logs", ("Clear Logs", "مسح السجلات") },
                { "Print", ("Print", "طباعة") },
                { "Print (Ctrl+P)", ("Print (Ctrl+P)", "طباعة (Ctrl+P)") },
                { "Back", ("Back", "رجوع") },
                { "Cancel", ("Cancel", "إلغاء") },
                { "Action", ("Action", "إجراء") },
                { "Status", ("Status", "الحالة") },
                { "Date", ("Date", "التاريخ") },
                { "Details", ("Details", "التفاصيل") },
                { "Previous", ("Previous", "السابق") },
                { "Next", ("Next", "التالي") },
                { "Module:", ("Module:", "الوحدة:") },
                { "Module", ("Module", "الوحدة") },

                { "Categories Management", ("Categories Management", "إدارة الأقسام والفيات") },
                { "Category Name", ("Category Name", "اسم القسم") },
                { "Category Name:", ("Category Name:", "اسم القسم:") },
                { "Category ID", ("Category ID", "معرف القسم") },
                { "Category ID:", ("Category ID:", "معرف القسم:") },
                { "Category ID :", ("Category ID :", "معرف القسم:") },
                { "Category :", ("Category :", "القسم:") },
                { "Category:", ("Category:", "القسم:") },
                { "Add Category", ("Add Category", "إضافة قسم") },
                { "Add New Category", ("Add New Category", "إضافة قسم جديد") },
                { "Update Category", ("Update Category", "تحديث قسم") },
                { "Delete Category", ("Delete Category", "حذف قسم") },
                { "Edit Category", ("Edit Category", "تعديل قسم") },
                { "Find Category to Update", ("Find Category to Update", "البحث عن قسم لتحديثه") },
                { "Enter CategoryID", ("Enter CategoryID", "أدخل معرف القسم") },
                { "Enter Category ID", ("Enter Category ID", "أدخل معرف القسم") },

                { "Suppliers Management", ("Suppliers Management", "إدارة الموردين") },
                { "Supplier Name", ("Supplier Name", "اسم المورد") },
                { "Supplier Name:", ("Supplier Name:", "اسم المورد:") },
                { "Supplier ID", ("Supplier ID", "معرف المورد") },
                { "Supplier ID:", ("Supplier ID:", "معرف المورد:") },
                { "Supplier:", ("Supplier:", "المورد:") },
                { "Phone Number", ("Phone Number", "رقم الهاتف") },
                { "Phone Number:", ("Phone Number:", "رقم الهاتف:") },
                { "Email Address", ("Email Address", "البريد الإلكتروني") },
                { "Email Address:", ("Email Address:", "البريد الإلكتروني:") },
                { "Add Supplier", ("Add Supplier", "إضافة مورد") },
                { "Add New Supplier", ("Add New Supplier", "إضافة مورد جديد") },
                { "Update Supplier", ("Update Supplier", "تحديث مورد") },
                { "Delete Supplier", ("Delete Supplier", "حذف مورد") },
                { "Edit Supplier", ("Edit Supplier", "تعديل مورد") },
                { "Find Supplier to Update", ("Find Supplier to Update", "البحث عن مورد لتحديثه") },
                { "Enter Supplier ID", ("Enter Supplier ID", "أدخل معرف المورد") },
                { "No suppliers found", ("No suppliers found", "لم يتم العثور على موردين") },

                { "Products Management", ("Products Management", "إدارة المنتجات والمخزون") },
                { "Product Name", ("Product Name", "اسم المنتج") },
                { "Product Name:", ("Product Name:", "اسم المنتج:") },
                { "Product:", ("Product:", "المنتج:") },
                { "Product ID", ("Product ID", "معرف المنتج") },
                { "Product ID:", ("Product ID:", "معرف المنتج:") },
                { "Barcode", ("Barcode", "الباركود") },
                { "Barcode:", ("Barcode:", "الباركود:") },
                { "Price", ("Price", "السعر") },
                { "Price:", ("Price:", "السعر:") },
                { "Quantity", ("Quantity", "الكمية") },
                { "Quantity:", ("Quantity:", "الكمية:") },
                { "Select Category", ("Select Category", "اختر القسم") },
                { "Select Supplier", ("Select Supplier", "اختر المورد") },
                { "Add Product", ("Add Product", "إضافة منتج") },
                { "Add New Product", ("Add New Product", "إضافة منتج جديد") },
                { "Update Product", ("Update Product", "تحديث منتج") },
                { "Delete Product", ("Delete Product", "حذف منتج") },
                { "Edit Product", ("Edit Product", "تعديل منتج") },
                { "Find Product to Update", ("Find Product to Update", "البحث عن منتج لتحديثه") },
                { "Enter Product ID or Barcode", ("Enter Product ID or Barcode", "أدخل معرف المنتج أو الباركود") },
                { "Enter Product ID", ("Enter Product ID", "أدخل معرف المنتج") },
                { "No products found", ("No products found", "لم يتم العثور على منتجات") },

                { "New Name :", ("New Name :", "الاسم الجديد:") },
                { "New Name:", ("New Name:", "الاسم الجديد:") },
                { "New Phone:", ("New Phone:", "الهاتف الجديد:") },
                { "New Email:", ("New Email:", "البريد الجديد:") },
                { "New Price:", ("New Price:", "السعر الجديد:") },
                { "New Quantity:", ("New Quantity:", "الكمية الجديدة:") },
                { "New Barcode:", ("New Barcode:", "الباركود الجديد:") },

                { "Point of Sale (POS)", ("Point of Sale (POS)", "شاشة نقطة البيع (POS)") },
                { "Subtotal", ("Subtotal", "المجموع الفرعي") },
                { "Subtotal:", ("Subtotal:", "المجموع الفرعي:") },
                { "Tax", ("Tax", "الضريبة") },
                { "Tax:", ("Tax:", "الضريبة:") },
                { "Total", ("Total", "الإجمالي") },
                { "Total:", ("Total:", "الإجمالي:") },
                { "Checkout", ("Checkout", "إتمام الدفع والدفع") },
                { "Apply Coupon", ("Apply Coupon", "تطبيق الكوبون") },
                { "Customer", ("Customer", "العميل") },
                { "Customer:", ("Customer:", "العميل:") },
                { "Payment Method", ("Payment Method", "طريقة الدفع") },
                { "Payment Method:", ("Payment Method:", "طريقة الدفع:") },
                { "Cash", ("Cash", "نقداً") },
                { "Card", ("Card", "بطاقة") },
                { "Discount:", ("Discount:", "الخصم:") },
                { "Grand Total:", ("Grand Total:", "الإجمالي الكلي:") },
                { "Search by Name or Barcode:", ("Search by Name or Barcode:", "البحث بالاسم أو الباركود:") },
                { "Add to Cart", ("Add to Cart", "إضافة للسلة") },
                { "Remove", ("Remove", "إزالة") },
                { "Clear Cart", ("Clear Cart", "تفريغ السلة") },
                { "Complete Sale", ("Complete Sale", "إتمام البيع") },
                { "Cart Summary", ("Cart Summary", "ملخص السلة") },

                { "Add New Customer", ("Add New Customer", "إضافة عميل جديد") },
                { "Customer Receipt History", ("Customer Receipt History", "سجل فواتير العميل") },
                { "Stock Valuation Report", ("Stock Valuation Report", "تقرير تقييم المخزون") },
                { "Total Stock Value: 0.00", ("Total Stock Value: 0.00", "إجمالي قيمة المخزون: 0.00") },
                { "Total Stock Value:", ("Total Stock Value:", "إجمالي قيمة المخزون:") },
                { "No products are available for stock valuation.", ("No products are available for stock valuation.", "لا توجد منتجات متاحة لتقرير التقييم.") },
                { "Daily Sales Report", ("Daily Sales Report", "تقرير المبيعات اليومي") },
                { "Product Exchange", ("Product Exchange", "استبدال المنتج") },
                { "Exchange", ("Exchange", "استبدال") },
                { "Order ID:", ("Order ID:", "رقم الطلب:") },
                { "View by Phone", ("View by Phone", "عرض حسب الهاتف") },
                { "Search Receipt", ("Search Receipt", "البحث عن فاتورة") },
                { "System Audit Logs & Activity", ("System Audit Logs & Activity", "سجلات المراجعة والنظام") },
                { "Add Coupon", ("Add Coupon", "إضافة كوبون") },
                { "Edit Coupon", ("Edit Coupon", "تعديل الكوبون") },
                { "Customer Name:", ("Customer Name:", "اسم العميل:") },
                { "Customer Phone:", ("Customer Phone:", "هاتف العميل:") },
                { "Address:", ("Address:", "العنوان:") },
                { "Notes:", ("Notes:", "ملاحظات:") },

                { "Success", ("Success", "نجاح") },
                { "Warning", ("Warning", "تنبيه") },
                { "Error", ("Error", "خطأ") },
                { "Operation completed successfully.", ("Operation completed successfully.", "تمت العملية بنجاح.") },
                { "Product added successfully.", ("Product added successfully.", "تم إضافة المنتج بنجاح.") },
                { "Product updated successfully.", ("Product updated successfully.", "تم تحديث المنتج بنجاح.") },
                { "Product deleted successfully.", ("Product deleted successfully.", "تم حذف المنتج بنجاح.") },
                { "Invalid input data.", ("Invalid input data.", "البيانات المدخلة غير صالحة.") },
                { "This field is required.", ("This field is required.", "هذا الحقل مطلوب.") },
                { "Please select a valid option.", ("Please select a valid option.", "يرجى تحديد خيار صحيح.") },

                // Additional keys for localized forms
                { "products available", ("products available", "منتج متاح") },
                { "No Products", ("No Products", "لا توجد منتجات") },
                { "No products match your search.", ("No products match your search.", "لا توجد منتجات تطابق بحثك.") },
                { "Stock:", ("Stock:", "المخزون:") },
                { "Out of stock", ("Out of stock", "نفد المخزون") },
                { "Tax (7%):", ("Tax (7%):", "الضريبة (7%):") },
                { "+ New", ("+ New", "+ جديد") },
                { "Change", ("Change", "تغيير") },
                { "New customer", ("New customer", "عميل جديد") },
                { "Stock Report", ("Stock Report", "تقرير المخزون") },
                { "Save Product", ("Save Product", "حفظ المنتج") },
                { "Find Product", ("Find Product", "البحث عن منتج") },
                { "Save Changes", ("Save Changes", "حفظ التغييرات") },
                { "Save Category", ("Save Category", "حفظ القسم") },
                { "Find Category", ("Find Category", "البحث عن قسم") },
                { "Save Supplier", ("Save Supplier", "حفظ المورد") },
                { "Find Supplier", ("Find Supplier", "البحث عن مورد") },
                { "Add Customer", ("Add Customer", "إضافة عميل") },
                { "Select", ("Select", "اختيار") },
                { "Confirm Exchange", ("Confirm Exchange", "تأكيد الاستبدال") },
                { "By Phone", ("By Phone", "حسب الهاتف") }
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
