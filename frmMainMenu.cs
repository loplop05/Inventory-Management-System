using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

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

            // Style quick search
            clsFormTheme.ApplyTextBoxStyle(_txtQuickSearch);
            _txtQuickSearch.TextChanged += _txtQuickSearch_TextChanged;
            _txtQuickSearch.KeyDown += _txtQuickSearch_KeyDown;
            _txtQuickSearch.Leave += (s, e) => _searchResultsPanel.Visible = false;

            // Apply card-with-accent-bar styling to all tiles
            StyleTile(btnPOS, clsFormTheme.Icons.POS, clsFormTheme.PrimaryColor, "Point of Sale");
            StyleTile(btnReceiptSearch, clsFormTheme.Icons.Search, clsFormTheme.PrimaryColor, "Receipt Search");
            StyleTile(btnPrintReceipt, clsFormTheme.Icons.Print, clsFormTheme.PrimaryColor, "Print Receipt");

            StyleTile(btnProducts, clsFormTheme.Icons.Products, clsFormTheme.SuccessColor, "Products");
            StyleTile(btnCategories, clsFormTheme.Icons.Categories, clsFormTheme.SuccessColor, "Categories");
            StyleTile(btnSuppliers, clsFormTheme.Icons.Suppliers, clsFormTheme.SuccessColor, "Suppliers");
            StyleTile(btnCouponManager, clsFormTheme.Icons.Coupon, clsFormTheme.SuccessColor, "Coupon Manager");

            StyleTile(btnDashboard, clsFormTheme.Icons.Chart, clsFormTheme.InfoColor, "Dashboard");
            StyleTile(btnAdvancedReports, clsFormTheme.Icons.Reports, clsFormTheme.InfoColor, "Advanced Reports");
            StyleTile(btnDailyReport, clsFormTheme.Icons.Calendar, clsFormTheme.InfoColor, "Daily Report");
            StyleTile(btnLowStockAlerts, clsFormTheme.Icons.Warning, clsFormTheme.WarningColor, "Low Stock Alerts");

            StyleTile(btnUserManagement, clsFormTheme.Icons.User, clsFormTheme.HeaderColor, "User Management");
            StyleTile(btnCustomerManagement, clsFormTheme.Icons.Customer, clsFormTheme.HeaderColor, "Customer Management");
            StyleTile(btnCloseShift, clsFormTheme.Icons.Close, clsFormTheme.HeaderColor, "Close Shift");
            StyleTile(btnAuditLogs, clsFormTheme.Icons.AuditLog, clsFormTheme.HeaderColor, "Audit Logs");

            // Set initial theme button state
            UpdateThemeButton();

            // Subscribe to theme changes
            clsFormTheme.ThemeChanged += (s, e) => UpdateThemeButton();

            // Re-apply theme on activation
            Activated += (s, e) => clsFormTheme.ApplyFormStyle(this);

            // Add FormClosing event to check for open shift
            FormClosing += frmMainMenu_FormClosing;
        }

        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Check if user has an open shift
            if (clsUserManagement.CurrentUser != null && !clsUserManagement.IsAdmin)
            {
                try
                {
                    DataTable openShift = clsShift.GetOpenShiftForUser(clsUserManagement.CurrentUser.UserID);
                    
                    if (openShift != null && openShift.Rows.Count > 0)
                    {
                        var result = MessageBox.Show(
                            "You have an open shift. Close it before exiting?",
                            "Open Shift",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
                        
                        if (result == DialogResult.Yes)
                        {
                            e.Cancel = true;
                            btnCloseShift_Click(sender, e);
                        }
                    }
                }
                catch
                {
                    // Don't block closing on error
                }
            }
        }

        private void StyleTile(Button btn, string icon, Color accentColor, string tooltipText)
        {
            btn.Text = "";
            btn.Font = new Font("Segoe UI", 11F);
            btn.BackColor = clsFormTheme.CardColor;
            btn.ForeColor = clsFormTheme.TextPrimary;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            btn.FlatAppearance.BorderSize = 1;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.TextImageRelation = TextImageRelation.ImageAboveText;
            _toolTip.SetToolTip(btn, tooltipText);

            // Custom paint to draw icon circle and text
            btn.Paint += (s, e) =>
            {
                Button button = (Button)s;
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int iconSize = 32;
                int iconCircleSize = 48;
                int centerX = button.Width / 2;
                int iconCircleY = 20;

                // Draw icon circle background
                using (SolidBrush iconBrush = new SolidBrush(accentColor))
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(centerX - iconCircleSize / 2, iconCircleY, iconCircleSize, iconCircleSize);
                    g.FillPath(iconBrush, path);
                }

                // Draw icon
                using (Font iconFont = new Font("Segoe MDL2 Assets", iconSize))
                using (SolidBrush iconTextBrush = new SolidBrush(Color.White))
                {
                    SizeF iconSizeF = g.MeasureString(icon, iconFont);
                    g.DrawString(icon, iconFont, iconTextBrush, 
                        centerX - iconSizeF.Width / 2, 
                        iconCircleY + (iconCircleSize - iconSizeF.Height) / 2 - 2);
                }

                // Draw text
                using (Font textFont = new Font("Segoe UI", 10F, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(clsFormTheme.TextPrimary))
                {
                    string text = tooltipText;
                    SizeF textSize = g.MeasureString(text, textFont);
                    g.DrawString(text, textFont, textBrush, 
                        centerX - textSize.Width / 2, 
                        iconCircleY + iconCircleSize + 10);
                }
            };

            // Hover effect
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = clsFormTheme.FormBackColorAlt;
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = clsFormTheme.CardColor;
            };
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            // Note: Buttons use custom painting with icons, so text is set via StyleTile
            // Localization for tooltips could be added here if needed
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            ApplyLocalization();
            ApplyRoleBasedAccess();
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

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            frmUserManagement frm = new frmUserManagement();
            frm.Show();
        }

        private void btnCustomerManagement_Click(object sender, EventArgs e)
        {
            frmCustomerManagement frm = new frmCustomerManagement();
            frm.ShowDialog();
        }

        private void btnCloseShift_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable openShift = clsShift.GetOpenShiftForUser(clsUserManagement.CurrentUser.UserID);
                
                if (openShift == null || openShift.Rows.Count == 0)
                {
                    clsFormTheme.ShowInfo(this, "You do not have an open shift.", "Shift");
                    return;
                }
                
                int shiftID = Convert.ToInt32(openShift.Rows[0]["ShiftID"]);
                
                using (var closeShiftForm = new frmCloseShift(shiftID))
                {
                    if (closeShiftForm.ShowDialog(this) == DialogResult.OK)
                    {
                        clsFormTheme.ShowSuccess(this, "Shift closed successfully.", "Success");
                    }
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error closing shift: " + ex.Message, "Error");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            clsHelpSystem.ShowHelpForm(clsHelpSystem.Topics.KeyboardShortcuts);
        }

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            clsFormTheme.ToggleTheme();
            UpdateThemeButton();
            clsFormTheme.ApplyFormStyle(this);
        }

        private void UpdateThemeButton()
        {
            btnThemeToggle.Text = clsFormTheme.IsDarkMode ? "☀️" : "🌙";
        }

        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            if (e.KeyCode == Keys.F1)
            {
                btnHelp_Click(sender, e);
            }
            if (e.Control && e.KeyCode == Keys.K)
            {
                _txtQuickSearch.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void _txtQuickSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = _txtQuickSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                _searchResultsPanel.Visible = false;
                return;
            }

            PerformQuickSearch(searchText);
        }

        private void _txtQuickSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _txtQuickSearch.Clear();
                _searchResultsPanel.Visible = false;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter && _lstSearchResults.SelectedIndex >= 0)
            {
                NavigateToSearchResult();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down && _lstSearchResults.Items.Count > 0)
            {
                _lstSearchResults.SelectedIndex = 0;
                _lstSearchResults.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void _lstSearchResults_DoubleClick(object sender, EventArgs e)
        {
            NavigateToSearchResult();
        }

        private class SearchResult
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public int ID { get; set; }
            public string TargetForm { get; set; }

            public override string ToString()
            {
                return $"{Type}: {Name}";
            }
        }

        private void PerformQuickSearch(string searchText)
        {
            _lstSearchResults.Items.Clear();
            var results = new List<SearchResult>();

            try
            {
                // Search products
                string prodError;
                DataTable products = null;
                if (clsProduct.GetAllProducts(out products, out prodError))
                {
                    foreach (DataRow row in products.Rows)
                    {
                        string name = row["ProductName"].ToString();
                        string barcode = row["Barcode"].ToString();
                        if (name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            barcode.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            results.Add(new SearchResult
                            {
                                Type = "Product",
                                Name = name,
                                ID = Convert.ToInt32(row["ProductID"]),
                                TargetForm = "Products"
                            });
                        }
                    }
                }

                // Search customers
                DataTable customers = clsCustomer.GetAllCustomers();
                if (customers != null)
                {
                    foreach (DataRow row in customers.Rows)
                    {
                        string name = row["CustomerName"].ToString();
                        string phone = row["PhoneNumber"].ToString();
                        if (name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            phone.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            results.Add(new SearchResult
                            {
                                Type = "Customer",
                                Name = name,
                                ID = Convert.ToInt32(row["CustomerID"]),
                                TargetForm = "Customers"
                            });
                        }
                    }
                }

                // Search suppliers
                DataTable suppliers = clsSupplier.GetAllSuppliers();
                if (suppliers != null)
                {
                    foreach (DataRow row in suppliers.Rows)
                    {
                        string name = row["SupplierName"].ToString();
                        if (name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            results.Add(new SearchResult
                            {
                                Type = "Supplier",
                                Name = name,
                                ID = Convert.ToInt32(row["SupplierID"]),
                                TargetForm = "Suppliers"
                            });
                        }
                    }
                }

                // Limit results to 20
                if (results.Count > 20)
                    results = results.Take(20).ToList();

                foreach (var result in results)
                {
                    _lstSearchResults.Items.Add(result);
                }

                _searchResultsPanel.Visible = results.Count > 0;
            }
            catch
            {
                _searchResultsPanel.Visible = false;
            }
        }

        private void NavigateToSearchResult()
        {
            if (_lstSearchResults.SelectedItem is SearchResult result)
            {
                _txtQuickSearch.Clear();
                _searchResultsPanel.Visible = false;

                switch (result.TargetForm)
                {
                    case "Products":
                        var productsForm = new frmProductsManagment();
                        productsForm.Show();
                        this.Close();
                        break;
                    case "Customers":
                        var customersForm = new frmCustomerManagement();
                        customersForm.Show();
                        this.Close();
                        break;
                    case "Suppliers":
                        var suppliersForm = new frmSuppliersManagment();
                        suppliersForm.Show();
                        this.Close();
                        break;
                }
            }
        }

        // Role-based access control
        private void ApplyRoleBasedAccess()
        {
            // Only apply role restrictions if a user is logged in
            // If no user is logged in, show all buttons (for testing/development)
            if (clsUserManagement.CurrentUser == null)
            {
                // No user logged in - show all buttons and sections
                SetSectionVisibility(true, true, true, true);
            }
            else if (clsUserManagement.IsCashier)
            {
                // Cashiers only see POS (Sales section)
                btnPOS.Visible = true;
                btnReceiptSearch.Visible = false;
                btnPrintReceipt.Visible = false;
                
                // Hide all other buttons
                btnCategories.Visible = false;
                btnSuppliers.Visible = false;
                btnProducts.Visible = false;
                btnCouponManager.Visible = false;
                btnDashboard.Visible = false;
                btnAdvancedReports.Visible = false;
                btnLowStockAlerts.Visible = false;
                btnDailyReport.Visible = false;
                btnAuditLogs.Visible = false;
                btnUserManagement.Visible = false;
                btnCustomerManagement.Visible = false;
                btnCloseShift.Visible = false;
                btnHelp.Visible = true;
                btnSignOut.Visible = true;

                // Show only Sales section
                SetSectionVisibility(true, false, false, false);
            }
            else if (clsUserManagement.IsManager)
            {
                // Managers see day-to-day operations but not system configuration
                btnPOS.Visible = true;
                btnReceiptSearch.Visible = true;
                btnPrintReceipt.Visible = true;
                btnDashboard.Visible = true;
                btnAdvancedReports.Visible = true;
                btnDailyReport.Visible = true;
                btnLowStockAlerts.Visible = true;
                btnCategories.Visible = true;
                btnSuppliers.Visible = true;
                btnProducts.Visible = true;
                btnCouponManager.Visible = true;
                btnCustomerManagement.Visible = true;
                btnAuditLogs.Visible = true;
                btnCloseShift.Visible = true;
                btnHelp.Visible = true;
                btnSignOut.Visible = true;
                
                // Managers cannot manage users
                btnUserManagement.Visible = false;

                // Show Sales, Catalog, Insights sections (Administration partial)
                SetSectionVisibility(true, true, true, false);
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
                btnUserManagement.Visible = true;
                btnCustomerManagement.Visible = true;
                btnCloseShift.Visible = true;
                btnShiftHistory.Visible = true;
                btnHelp.Visible = true;
                btnSignOut.Visible = true;

                // Show all sections
                SetSectionVisibility(true, true, true, true);
            }

            // Permission-based visibility upgrades for Manager/Cashier
            if (!clsUserManagement.IsAdmin)
            {
                if (clsUserManagement.HasPermission(clsPermissions.ManageUsers))
                    btnUserManagement.Visible = true;
                if (clsUserManagement.HasPermission(clsPermissions.ViewAuditLogs))
                    btnAuditLogs.Visible = true;
            }
        }

        private void SetSectionVisibility(bool showSales, bool showCatalog, bool showInsights, bool showAdministration)
        {
            _sectionSales.Visible = showSales;
            _sectionCatalog.Visible = showCatalog;
            _sectionInsights.Visible = showInsights;
            _sectionAdministration.Visible = showAdministration;
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            // Check if user has an open shift and prompt to close it
            if (clsUserManagement.CurrentUser != null && clsShift.HasOpenShift(clsUserManagement.CurrentUser.UserID))
            {
                var result = MessageBox.Show(
                    "You have an open shift. Do you want to close it before signing out?", 
                    "Open Shift",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    // Get the open shift ID
                    var openShift = clsShiftData.GetOpenShiftForUser(clsUserManagement.CurrentUser.UserID);
                    if (openShift != null && openShift.Rows.Count > 0)
                    {
                        int shiftID = Convert.ToInt32(openShift.Rows[0]["ShiftID"]);
                        using (var closeShiftForm = new frmCloseShift(shiftID))
                        {
                            if (closeShiftForm.ShowDialog() != DialogResult.OK)
                            {
                                // User cancelled closing shift, don't sign out
                                return;
                            }
                        }
                    }
                }
            }

            // Sign out
            clsUserManagement.Logout();
            Application.Restart();
        }

        private void btnShiftHistory_Click(object sender, EventArgs e)
        {
            using (var shiftHistoryForm = new frmShiftHistory())
            {
                shiftHistoryForm.ShowDialog(this);
            }
        }
    }
}
