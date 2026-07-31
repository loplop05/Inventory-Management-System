using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public partial class frmMainMenu : Form
    {
        private ToolTip _toolTip = new ToolTip();

        public frmMainMenu()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Inventory Management System", clsFormTheme.Icons.Home);

            // ── Categories button ──────────────────────────────────────────────
            btnCategories.Text = "Categories";
            btnCategories.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCategories.BackColor = Color.FromArgb(59, 130, 246); // Blue
            btnCategories.ForeColor = Color.White;
            btnCategories.FlatStyle = FlatStyle.Flat;
            btnCategories.FlatAppearance.BorderSize = 0;
            btnCategories.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnCategories, "Manage product categories");

            // ── Suppliers button ───────────────────────────────────────────────
            btnSuppliers.Text = "Suppliers";
            btnSuppliers.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnSuppliers.BackColor = Color.FromArgb(16, 185, 129); // Green
            btnSuppliers.ForeColor = Color.White;
            btnSuppliers.FlatStyle = FlatStyle.Flat;
            btnSuppliers.FlatAppearance.BorderSize = 0;
            btnSuppliers.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnSuppliers, "Manage suppliers");

            // ── Products button ────────────────────────────────────────────────
            btnProducts.Text = "Products";
            btnProducts.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnProducts.BackColor = Color.FromArgb(245, 158, 11); // Orange
            btnProducts.ForeColor = Color.White;
            btnProducts.FlatStyle = FlatStyle.Flat;
            btnProducts.FlatAppearance.BorderSize = 0;
            btnProducts.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnProducts, "Manage products and inventory");

            // ── Receipt Search button ────────────────────────────────────────────
            btnReceiptSearch.Text = "Receipt Search";
            btnReceiptSearch.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnReceiptSearch.BackColor = Color.FromArgb(139, 92, 246); // Purple
            btnReceiptSearch.ForeColor = Color.White;
            btnReceiptSearch.FlatStyle = FlatStyle.Flat;
            btnReceiptSearch.FlatAppearance.BorderSize = 0;
            btnReceiptSearch.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnReceiptSearch, "Search receipts and manage exchanges");

            // ── Print Receipt button ─────────────────────────────────────────────
            btnPrintReceipt.Text = "Print Receipt";
            btnPrintReceipt.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPrintReceipt.BackColor = Color.FromArgb(236, 72, 153); // Pink
            btnPrintReceipt.ForeColor = Color.White;
            btnPrintReceipt.FlatStyle = FlatStyle.Flat;
            btnPrintReceipt.FlatAppearance.BorderSize = 0;
            btnPrintReceipt.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnPrintReceipt, "Print order receipts");

            // ── Dashboard button ───────────────────────────────────────────────
            btnDashboard.Text = "Dashboard";
            btnDashboard.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnDashboard.BackColor = Color.FromArgb(99, 102, 241); // Indigo
            btnDashboard.ForeColor = Color.White;
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnDashboard, "View dashboard with key metrics");

            // ── Advanced Reports button ─────────────────────────────────────────
            btnAdvancedReports.Text = "Advanced Reports";
            btnAdvancedReports.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAdvancedReports.BackColor = Color.FromArgb(14, 165, 233); // Sky Blue
            btnAdvancedReports.ForeColor = Color.White;
            btnAdvancedReports.FlatStyle = FlatStyle.Flat;
            btnAdvancedReports.FlatAppearance.BorderSize = 0;
            btnAdvancedReports.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnAdvancedReports, "Generate advanced reports");

            // ── Low Stock Alerts button ─────────────────────────────────────────
            btnLowStockAlerts.Text = "Low Stock Alerts";
            btnLowStockAlerts.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnLowStockAlerts.BackColor = Color.FromArgb(239, 68, 68); // Red
            btnLowStockAlerts.ForeColor = Color.White;
            btnLowStockAlerts.FlatStyle = FlatStyle.Flat;
            btnLowStockAlerts.FlatAppearance.BorderSize = 0;
            btnLowStockAlerts.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnLowStockAlerts, "View low stock alerts");

            // ── Coupon Manager button ───────────────────────────────────────────
            btnCouponManager.Text = "Coupon Manager";
            btnCouponManager.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnCouponManager.BackColor = Color.FromArgb(168, 85, 247); // Purple
            btnCouponManager.ForeColor = Color.White;
            btnCouponManager.FlatStyle = FlatStyle.Flat;
            btnCouponManager.FlatAppearance.BorderSize = 0;
            btnCouponManager.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnCouponManager, "Manage coupons and discounts");

            // ── POS button ─────────────────────────────────────────────────────
            btnPOS.Text = "Point of Sale";
            btnPOS.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPOS.BackColor = Color.FromArgb(34, 197, 94); // Green
            btnPOS.ForeColor = Color.White;
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnPOS, "Open Point of Sale (POS)");

            // ── Daily Report button ────────────────────────────────────────────
            btnDailyReport.Text = "Daily Report";
            btnDailyReport.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnDailyReport.BackColor = Color.FromArgb(249, 115, 22); // Orange
            btnDailyReport.ForeColor = Color.White;
            btnDailyReport.FlatStyle = FlatStyle.Flat;
            btnDailyReport.FlatAppearance.BorderSize = 0;
            btnDailyReport.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnDailyReport, "View today's sales report");

            // ── Audit Logs button ──────────────────────────────────────────────
            btnAuditLogs.Text = "Audit Logs";
            btnAuditLogs.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAuditLogs.BackColor = Color.FromArgb(79, 70, 229); // Indigo 600
            btnAuditLogs.ForeColor = Color.White;
            btnAuditLogs.FlatStyle = FlatStyle.Flat;
            btnAuditLogs.FlatAppearance.BorderSize = 0;
            btnAuditLogs.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnAuditLogs, "View system audit logs and activity trail");

            // ── Help button ────────────────────────────────────────────────────
            btnHelp.Text = "Help";
            btnHelp.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnHelp.BackColor = Color.FromArgb(107, 114, 128); // Gray
            btnHelp.ForeColor = Color.White;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.FlatAppearance.BorderSize = 0;
            btnHelp.TextAlign = ContentAlignment.MiddleCenter;
            _toolTip.SetToolTip(btnHelp, "View help and keyboard shortcuts (F1)");

            // ── Theme Toggle button - uncomment after adding to Designer ───────────
            // btnThemeToggle.Text = clsFormTheme.IsDarkMode ? "Light Mode" : "Dark Mode";
            // btnThemeToggle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            // btnThemeToggle.BackColor = Color.FromArgb(99, 102, 241); // Indigo
            // btnThemeToggle.ForeColor = Color.White;
            // btnThemeToggle.FlatStyle = FlatStyle.Flat;
            // btnThemeToggle.FlatAppearance.BorderSize = 0;
            // btnThemeToggle.TextAlign = ContentAlignment.MiddleCenter;
            // _toolTip.SetToolTip(btnThemeToggle, "Toggle between light and dark theme");

            KeyDown += frmMainMenu_KeyDown;
            clsLanguageManager.LanguageChanged += (s, e) => ApplyLocalization();
            // clsFormTheme.ThemeChanged += (s, e) => UpdateThemeButton();

            clsAuditLog.LogAction("Application Started", "Inventory System main menu loaded", "System");
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            btnCategories.Text = clsLanguageManager.GetString("Categories");
            btnSuppliers.Text = clsLanguageManager.GetString("Suppliers");
            btnProducts.Text = clsLanguageManager.GetString("Products");
            btnReceiptSearch.Text = clsLanguageManager.GetString("Receipt Search");
            btnPrintReceipt.Text = clsLanguageManager.GetString("Print Receipt");
            btnDashboard.Text = clsLanguageManager.GetString("Dashboard");
            btnAdvancedReports.Text = clsLanguageManager.GetString("Advanced Reports");
            btnLowStockAlerts.Text = clsLanguageManager.GetString("Low Stock Alerts");
            btnCouponManager.Text = clsLanguageManager.GetString("Coupon Manager");
            btnPOS.Text = clsLanguageManager.GetString("Point of Sale");
            btnDailyReport.Text = clsLanguageManager.GetString("Daily Report");
            btnAuditLogs.Text = clsLanguageManager.GetString("Audit Logs");
            btnHelp.Text = clsLanguageManager.GetString("Help");
        }

        // ── Event handlers ─────────────────────────────────────────────────────

        private void btnCategories_Click(object sender, EventArgs e)
        {
            frmCategoriesManagment frm = new frmCategoriesManagment();
            frm.Show();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            frmSuppliersManagment frm = new frmSuppliersManagment();
            frm.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            frmProductsManagment frm = new frmProductsManagment();
            frm.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void btnDailyReport_Click(object sender, EventArgs e)
        {
            frmDailyReport frm = new frmDailyReport();
            frm.ShowDialog();
        }

        private void btnReceiptSearch_Click(object sender, EventArgs e)
        {
            frmReceiptSearch frm = new frmReceiptSearch();
            frm.ShowDialog();
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            frmPrintReceipt frm = new frmPrintReceipt();
            frm.ShowDialog();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            frmDashboard frm = new frmDashboard();
            frm.Show();
        }

        private void btnAdvancedReports_Click(object sender, EventArgs e)
        {
            clsAdvancedReports.ShowReportLauncher();
        }

        private void btnLowStockAlerts_Click(object sender, EventArgs e)
        {
            clsLowStockAlerts.ShowAlertForm();
        }

        private void btnCouponManager_Click(object sender, EventArgs e)
        {
            clsDiscountSystem.ShowCouponManager();
        }

        private void btnAuditLogs_Click(object sender, EventArgs e)
        {
            frmAuditLog frm = new frmAuditLog();
            frm.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            clsHelpSystem.ShowHelpForm(clsHelpSystem.Topics.KeyboardShortcuts);
        }

        // Theme toggle handler - uncomment after adding button to Designer
        /*
        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            clsFormTheme.ToggleTheme();
        }

        private void UpdateThemeButton()
        {
            btnThemeToggle.Text = clsFormTheme.IsDarkMode ? "Light Mode" : "Dark Mode";
        }
        */

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            ApplyLocalization();
            // ApplyRoleBasedAccess(); // Uncomment after adding login functionality
        }

        // Role-based access control - uncomment after implementing login system
        /*
        private void ApplyRoleBasedAccess()
        {
            // If user is not logged in or is a cashier, hide admin-only buttons
            if (clsUserManagement.CurrentUser == null || clsUserManagement.IsCashier)
            {
                btnCategories.Visible = false;
                btnSuppliers.Visible = false;
                btnProducts.Visible = false;
                btnReceiptSearch.Visible = false;
                btnPrintReceipt.Visible = false;
                btnDashboard.Visible = false;
                btnAdvancedReports.Visible = false;
                btnLowStockAlerts.Visible = false;
                btnCouponManager.Visible = false;
                btnAuditLogs.Visible = false;
                btnHelp.Visible = false;

                // Cashiers only see POS
                btnPOS.Visible = true;
                btnDailyReport.Visible = false; // Cashiers shouldn't see reports
            }
            else if (clsUserManagement.IsAdmin)
            {
                // Admins see everything
                btnCategories.Visible = true;
                btnSuppliers.Visible = true;
                btnProducts.Visible = true;
                btnReceiptSearch.Visible = true;
                btnPrintReceipt.Visible = true;
                btnDashboard.Visible = true;
                btnAdvancedReports.Visible = true;
                btnLowStockAlerts.Visible = true;
                btnCouponManager.Visible = true;
                btnPOS.Visible = true;
                btnDailyReport.Visible = true;
                btnAuditLogs.Visible = true;
                btnHelp.Visible = true;
            }
        }
        */

        private void _buttonsPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
