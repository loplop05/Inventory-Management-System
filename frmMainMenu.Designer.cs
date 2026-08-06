namespace InventoryManagementSystem
{
    partial class frmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._buttonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnReceiptSearch = new System.Windows.Forms.Button();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnAdvancedReports = new System.Windows.Forms.Button();
            this.btnLowStockAlerts = new System.Windows.Forms.Button();
            this.btnCouponManager = new System.Windows.Forms.Button();
            this.btnPOS = new System.Windows.Forms.Button();
            this.btnDailyReport = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnAuditLogs = new System.Windows.Forms.Button();
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.btnCustomerManagement = new System.Windows.Forms.Button();
            this._mainLayoutPanel.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainLayoutPanel
            // 
            this._mainLayoutPanel.ColumnCount = 1;
            this._mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Controls.Add(this._buttonsPanel, 0, 0);
            this._mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainLayoutPanel.Name = "_mainLayoutPanel";
            this._mainLayoutPanel.RowCount = 1;
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Size = new System.Drawing.Size(1260, 720);
            this._mainLayoutPanel.TabIndex = 0;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.ColumnCount = 3;
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._buttonsPanel.Controls.Add(this.btnCategories, 0, 0);
            this._buttonsPanel.Controls.Add(this.btnSuppliers, 1, 0);
            this._buttonsPanel.Controls.Add(this.btnProducts, 2, 0);
            this._buttonsPanel.Controls.Add(this.btnReceiptSearch, 0, 1);
            this._buttonsPanel.Controls.Add(this.btnPrintReceipt, 1, 1);
            this._buttonsPanel.Controls.Add(this.btnDashboard, 2, 1);
            this._buttonsPanel.Controls.Add(this.btnAdvancedReports, 0, 2);
            this._buttonsPanel.Controls.Add(this.btnLowStockAlerts, 1, 2);
            this._buttonsPanel.Controls.Add(this.btnCouponManager, 2, 2);
            this._buttonsPanel.Controls.Add(this.btnPOS, 0, 3);
            this._buttonsPanel.Controls.Add(this.btnDailyReport, 1, 3);
            this._buttonsPanel.Controls.Add(this.btnAuditLogs, 2, 3);
            this._buttonsPanel.Controls.Add(this.btnUserManagement, 1, 4);
            this._buttonsPanel.Controls.Add(this.btnCustomerManagement, 2, 4);
            this._buttonsPanel.Controls.Add(this.btnHelp, 0, 4);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 3);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Padding = new System.Windows.Forms.Padding(40);
            this._buttonsPanel.RowCount = 5;
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._buttonsPanel.Size = new System.Drawing.Size(1254, 714);
            this._buttonsPanel.TabIndex = 1;
            this._buttonsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._buttonsPanel_Paint);
            // 
            // btnCategories
            // 
            this.btnCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCategories.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCategories.Location = new System.Drawing.Point(43, 43);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(385, 271);
            this.btnCategories.TabIndex = 0;
            this.btnCategories.Text = "Categories";
            this.btnCategories.UseVisualStyleBackColor = true;
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSuppliers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSuppliers.Location = new System.Drawing.Point(434, 43);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(385, 271);
            this.btnSuppliers.TabIndex = 1;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnProducts.Location = new System.Drawing.Point(825, 43);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(386, 271);
            this.btnProducts.TabIndex = 2;
            this.btnProducts.Text = "Products";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnReceiptSearch
            // 
            this.btnReceiptSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReceiptSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReceiptSearch.Location = new System.Drawing.Point(43, 320);
            this.btnReceiptSearch.Name = "btnReceiptSearch";
            this.btnReceiptSearch.Size = new System.Drawing.Size(385, 271);
            this.btnReceiptSearch.TabIndex = 3;
            this.btnReceiptSearch.Text = "Receipt Search";
            this.btnReceiptSearch.UseVisualStyleBackColor = true;
            this.btnReceiptSearch.Click += new System.EventHandler(this.btnReceiptSearch_Click);
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintReceipt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPrintReceipt.Location = new System.Drawing.Point(434, 320);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(385, 271);
            this.btnPrintReceipt.TabIndex = 4;
            this.btnPrintReceipt.Text = "Print Receipt";
            this.btnPrintReceipt.UseVisualStyleBackColor = true;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDashboard.Location = new System.Drawing.Point(825, 320);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(386, 271);
            this.btnDashboard.TabIndex = 5;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnAdvancedReports
            // 
            this.btnAdvancedReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdvancedReports.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAdvancedReports.Location = new System.Drawing.Point(43, 597);
            this.btnAdvancedReports.Name = "btnAdvancedReports";
            this.btnAdvancedReports.Size = new System.Drawing.Size(385, 271);
            this.btnAdvancedReports.TabIndex = 6;
            this.btnAdvancedReports.Text = "Advanced Reports";
            this.btnAdvancedReports.UseVisualStyleBackColor = true;
            this.btnAdvancedReports.Click += new System.EventHandler(this.btnAdvancedReports_Click);
            // 
            // btnLowStockAlerts
            // 
            this.btnLowStockAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLowStockAlerts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLowStockAlerts.Location = new System.Drawing.Point(434, 597);
            this.btnLowStockAlerts.Name = "btnLowStockAlerts";
            this.btnLowStockAlerts.Size = new System.Drawing.Size(385, 271);
            this.btnLowStockAlerts.TabIndex = 7;
            this.btnLowStockAlerts.Text = "Low Stock Alerts";
            this.btnLowStockAlerts.UseVisualStyleBackColor = true;
            this.btnLowStockAlerts.Click += new System.EventHandler(this.btnLowStockAlerts_Click);
            // 
            // btnCouponManager
            // 
            this.btnCouponManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCouponManager.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCouponManager.Location = new System.Drawing.Point(825, 597);
            this.btnCouponManager.Name = "btnCouponManager";
            this.btnCouponManager.Size = new System.Drawing.Size(386, 271);
            this.btnCouponManager.TabIndex = 8;
            this.btnCouponManager.Text = "Coupon Manager";
            this.btnCouponManager.UseVisualStyleBackColor = true;
            this.btnCouponManager.Click += new System.EventHandler(this.btnCouponManager_Click);
            // 
            // btnPOS
            // 
            this.btnPOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPOS.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPOS.Location = new System.Drawing.Point(43, 597);
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Size = new System.Drawing.Size(385, 271);
            this.btnPOS.TabIndex = 9;
            this.btnPOS.Text = "Point of Sale";
            this.btnPOS.UseVisualStyleBackColor = true;
            this.btnPOS.Click += new System.EventHandler(this.btnPOS_Click);
            // 
            // btnDailyReport
            // 
            this.btnDailyReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDailyReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDailyReport.Location = new System.Drawing.Point(434, 597);
            this.btnDailyReport.Name = "btnDailyReport";
            this.btnDailyReport.Size = new System.Drawing.Size(385, 271);
            this.btnDailyReport.TabIndex = 10;
            this.btnDailyReport.Text = "Daily Report";
            this.btnDailyReport.UseVisualStyleBackColor = true;
            this.btnDailyReport.Click += new System.EventHandler(this.btnDailyReport_Click);
            // 
            // btnAuditLogs
            // 
            this.btnAuditLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAuditLogs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAuditLogs.Location = new System.Drawing.Point(825, 597);
            this.btnAuditLogs.Name = "btnAuditLogs";
            this.btnAuditLogs.Size = new System.Drawing.Size(386, 271);
            this.btnAuditLogs.TabIndex = 11;
            this.btnAuditLogs.Text = "Audit Logs";
            this.btnAuditLogs.UseVisualStyleBackColor = true;
            this.btnAuditLogs.Click += new System.EventHandler(this.btnAuditLogs_Click);
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUserManagement.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnUserManagement.Location = new System.Drawing.Point(434, 597);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(385, 271);
            this.btnUserManagement.TabIndex = 13;
            this.btnUserManagement.Text = "User Management";
            this.btnUserManagement.UseVisualStyleBackColor = true;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // btnCustomerManagement
            // 
            this.btnCustomerManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCustomerManagement.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCustomerManagement.Location = new System.Drawing.Point(826, 597);
            this.btnCustomerManagement.Name = "btnCustomerManagement";
            this.btnCustomerManagement.Size = new System.Drawing.Size(385, 271);
            this.btnCustomerManagement.TabIndex = 14;
            this.btnCustomerManagement.Text = "Customer Management";
            this.btnCustomerManagement.UseVisualStyleBackColor = true;
            this.btnCustomerManagement.Click += new System.EventHandler(this.btnCustomerManagement_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHelp.Location = new System.Drawing.Point(43, 597);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(385, 271);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 720);
            this.Controls.Add(this._mainLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "frmMainMenu";
            this.Text = "Inventory Management System";
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this._mainLayoutPanel.ResumeLayout(false);
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnReceiptSearch;
        private System.Windows.Forms.Button btnPrintReceipt;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnAdvancedReports;
        private System.Windows.Forms.Button btnLowStockAlerts;
        private System.Windows.Forms.Button btnCouponManager;
        private System.Windows.Forms.Button btnPOS;
        private System.Windows.Forms.Button btnDailyReport;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnAuditLogs;
        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Button btnCustomerManagement;
    }
}
