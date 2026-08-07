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
            this._contentPanel = new System.Windows.Forms.Panel();
            this._footerPanel = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this._sectionAdministration = new System.Windows.Forms.Panel();
            this._lblAdministration = new System.Windows.Forms.Label();
            this._flowAdministration = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAuditLogs = new System.Windows.Forms.Button();
            this.btnUserManagement = new System.Windows.Forms.Button();
            this.btnCustomerManagement = new System.Windows.Forms.Button();
            this._sectionInsights = new System.Windows.Forms.Panel();
            this._lblInsights = new System.Windows.Forms.Label();
            this._flowInsights = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLowStockAlerts = new System.Windows.Forms.Button();
            this.btnDailyReport = new System.Windows.Forms.Button();
            this.btnAdvancedReports = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this._sectionCatalog = new System.Windows.Forms.Panel();
            this._lblCatalog = new System.Windows.Forms.Label();
            this._flowCatalog = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCouponManager = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this._sectionSales = new System.Windows.Forms.Panel();
            this._lblSales = new System.Windows.Forms.Label();
            this._flowSales = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this.btnReceiptSearch = new System.Windows.Forms.Button();
            this.btnPOS = new System.Windows.Forms.Button();
            this._contentPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this._sectionAdministration.SuspendLayout();
            this._flowAdministration.SuspendLayout();
            this._sectionInsights.SuspendLayout();
            this._flowInsights.SuspendLayout();
            this._sectionCatalog.SuspendLayout();
            this._flowCatalog.SuspendLayout();
            this._sectionSales.SuspendLayout();
            this._flowSales.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.AutoScroll = true;
            this._contentPanel.Controls.Add(this._footerPanel);
            this._contentPanel.Controls.Add(this._sectionAdministration);
            this._contentPanel.Controls.Add(this._sectionInsights);
            this._contentPanel.Controls.Add(this._sectionCatalog);
            this._contentPanel.Controls.Add(this._sectionSales);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Padding = new System.Windows.Forms.Padding(20);
            this._contentPanel.Size = new System.Drawing.Size(1260, 720);
            // 
            // _footerPanel
            // 
            this._footerPanel.Controls.Add(this.btnHelp);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(20, 680);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this._footerPanel.Size = new System.Drawing.Size(1220, 20);
            this._footerPanel.TabIndex = 4;
            // 
            // btnHelp
            // 
            this.btnHelp.AutoSize = true;
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHelp.ForeColor = clsFormTheme.TextSecondary;
            this.btnHelp.Location = new System.Drawing.Point(0, 10);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(30, 20);
            this.btnHelp.TabIndex = 0;
            this.btnHelp.Text = "? Help";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // _sectionAdministration
            // 
            this._sectionAdministration.Controls.Add(this._lblAdministration);
            this._sectionAdministration.Controls.Add(this._flowAdministration);
            this._sectionAdministration.Dock = System.Windows.Forms.DockStyle.Top;
            this._sectionAdministration.Location = new System.Drawing.Point(20, 20);
            this._sectionAdministration.Name = "_sectionAdministration";
            this._sectionAdministration.Size = new System.Drawing.Size(1220, 150);
            this._sectionAdministration.TabIndex = 3;
            // 
            // _lblAdministration
            // 
            this._lblAdministration.AutoSize = true;
            this._lblAdministration.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblAdministration.ForeColor = clsFormTheme.TextSecondary;
            this._lblAdministration.Location = new System.Drawing.Point(0, 0);
            this._lblAdministration.Name = "_lblAdministration";
            this._lblAdministration.Size = new System.Drawing.Size(104, 20);
            this._lblAdministration.TabIndex = 0;
            this._lblAdministration.Text = "ADMINISTRATION";
            // 
            // _flowAdministration
            // 
            this._flowAdministration.AutoSize = true;
            this._flowAdministration.Controls.Add(this.btnAuditLogs);
            this._flowAdministration.Controls.Add(this.btnUserManagement);
            this._flowAdministration.Controls.Add(this.btnCustomerManagement);
            this._flowAdministration.Dock = System.Windows.Forms.DockStyle.Top;
            this._flowAdministration.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._flowAdministration.Location = new System.Drawing.Point(0, 25);
            this._flowAdministration.Name = "_flowAdministration";
            this._flowAdministration.Size = new System.Drawing.Size(1220, 120);
            this._flowAdministration.TabIndex = 1;
            this._flowAdministration.WrapContents = false;
            // 
            // btnAuditLogs
            // 
            this.btnAuditLogs.BackColor = clsFormTheme.CardColor;
            this.btnAuditLogs.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnAuditLogs.FlatAppearance.BorderSize = 1;
            this.btnAuditLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuditLogs.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAuditLogs.ForeColor = clsFormTheme.TextPrimary;
            this.btnAuditLogs.Location = new System.Drawing.Point(3, 3);
            this.btnAuditLogs.Name = "btnAuditLogs";
            this.btnAuditLogs.Size = new System.Drawing.Size(200, 110);
            this.btnAuditLogs.TabIndex = 2;
            this.btnAuditLogs.Text = "Audit Logs";
            this.btnAuditLogs.UseVisualStyleBackColor = false;
            this.btnAuditLogs.Click += new System.EventHandler(this.btnAuditLogs_Click);
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.BackColor = clsFormTheme.CardColor;
            this.btnUserManagement.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnUserManagement.FlatAppearance.BorderSize = 1;
            this.btnUserManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserManagement.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnUserManagement.ForeColor = clsFormTheme.TextPrimary;
            this.btnUserManagement.Location = new System.Drawing.Point(209, 3);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.Size = new System.Drawing.Size(200, 110);
            this.btnUserManagement.TabIndex = 1;
            this.btnUserManagement.Text = "User Management";
            this.btnUserManagement.UseVisualStyleBackColor = false;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // btnCustomerManagement
            // 
            this.btnCustomerManagement.BackColor = clsFormTheme.CardColor;
            this.btnCustomerManagement.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnCustomerManagement.FlatAppearance.BorderSize = 1;
            this.btnCustomerManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerManagement.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCustomerManagement.ForeColor = clsFormTheme.TextPrimary;
            this.btnCustomerManagement.Location = new System.Drawing.Point(415, 3);
            this.btnCustomerManagement.Name = "btnCustomerManagement";
            this.btnCustomerManagement.Size = new System.Drawing.Size(200, 110);
            this.btnCustomerManagement.TabIndex = 0;
            this.btnCustomerManagement.Text = "Customer Management";
            this.btnCustomerManagement.UseVisualStyleBackColor = false;
            this.btnCustomerManagement.Click += new System.EventHandler(this.btnCustomerManagement_Click);
            // 
            // _sectionInsights
            // 
            this._sectionInsights.Controls.Add(this._lblInsights);
            this._sectionInsights.Controls.Add(this._flowInsights);
            this._sectionInsights.Dock = System.Windows.Forms.DockStyle.Top;
            this._sectionInsights.Location = new System.Drawing.Point(20, 170);
            this._sectionInsights.Name = "_sectionInsights";
            this._sectionInsights.Size = new System.Drawing.Size(1220, 150);
            this._sectionInsights.TabIndex = 2;
            // 
            // _lblInsights
            // 
            this._lblInsights.AutoSize = true;
            this._lblInsights.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblInsights.ForeColor = clsFormTheme.TextSecondary;
            this._lblInsights.Location = new System.Drawing.Point(0, 0);
            this._lblInsights.Name = "_lblInsights";
            this._lblInsights.Size = new System.Drawing.Size(165, 20);
            this._lblInsights.TabIndex = 0;
            this._lblInsights.Text = "INSIGHTS & REPORTS";
            // 
            // _flowInsights
            // 
            this._flowInsights.AutoSize = true;
            this._flowInsights.Controls.Add(this.btnLowStockAlerts);
            this._flowInsights.Controls.Add(this.btnDailyReport);
            this._flowInsights.Controls.Add(this.btnAdvancedReports);
            this._flowInsights.Controls.Add(this.btnDashboard);
            this._flowInsights.Dock = System.Windows.Forms.DockStyle.Top;
            this._flowInsights.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._flowInsights.Location = new System.Drawing.Point(0, 25);
            this._flowInsights.Name = "_flowInsights";
            this._flowInsights.Size = new System.Drawing.Size(1220, 120);
            this._flowInsights.TabIndex = 1;
            this._flowInsights.WrapContents = false;
            // 
            // btnLowStockAlerts
            // 
            this.btnLowStockAlerts.BackColor = clsFormTheme.CardColor;
            this.btnLowStockAlerts.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnLowStockAlerts.FlatAppearance.BorderSize = 1;
            this.btnLowStockAlerts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLowStockAlerts.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLowStockAlerts.ForeColor = clsFormTheme.TextPrimary;
            this.btnLowStockAlerts.Location = new System.Drawing.Point(3, 3);
            this.btnLowStockAlerts.Name = "btnLowStockAlerts";
            this.btnLowStockAlerts.Size = new System.Drawing.Size(200, 110);
            this.btnLowStockAlerts.TabIndex = 3;
            this.btnLowStockAlerts.Text = "Low Stock Alerts";
            this.btnLowStockAlerts.UseVisualStyleBackColor = false;
            this.btnLowStockAlerts.Click += new System.EventHandler(this.btnLowStockAlerts_Click);
            // 
            // btnDailyReport
            // 
            this.btnDailyReport.BackColor = clsFormTheme.CardColor;
            this.btnDailyReport.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnDailyReport.FlatAppearance.BorderSize = 1;
            this.btnDailyReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDailyReport.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDailyReport.ForeColor = clsFormTheme.TextPrimary;
            this.btnDailyReport.Location = new System.Drawing.Point(209, 3);
            this.btnDailyReport.Name = "btnDailyReport";
            this.btnDailyReport.Size = new System.Drawing.Size(200, 110);
            this.btnDailyReport.TabIndex = 2;
            this.btnDailyReport.Text = "Daily Report";
            this.btnDailyReport.UseVisualStyleBackColor = false;
            this.btnDailyReport.Click += new System.EventHandler(this.btnDailyReport_Click);
            // 
            // btnAdvancedReports
            // 
            this.btnAdvancedReports.BackColor = clsFormTheme.CardColor;
            this.btnAdvancedReports.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnAdvancedReports.FlatAppearance.BorderSize = 1;
            this.btnAdvancedReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdvancedReports.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnAdvancedReports.ForeColor = clsFormTheme.TextPrimary;
            this.btnAdvancedReports.Location = new System.Drawing.Point(415, 3);
            this.btnAdvancedReports.Name = "btnAdvancedReports";
            this.btnAdvancedReports.Size = new System.Drawing.Size(200, 110);
            this.btnAdvancedReports.TabIndex = 1;
            this.btnAdvancedReports.Text = "Advanced Reports";
            this.btnAdvancedReports.UseVisualStyleBackColor = false;
            this.btnAdvancedReports.Click += new System.EventHandler(this.btnAdvancedReports_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = clsFormTheme.CardColor;
            this.btnDashboard.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnDashboard.FlatAppearance.BorderSize = 1;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDashboard.ForeColor = clsFormTheme.TextPrimary;
            this.btnDashboard.Location = new System.Drawing.Point(621, 3);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(200, 110);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // _sectionCatalog
            // 
            this._sectionCatalog.Controls.Add(this._lblCatalog);
            this._sectionCatalog.Controls.Add(this._flowCatalog);
            this._sectionCatalog.Dock = System.Windows.Forms.DockStyle.Top;
            this._sectionCatalog.Location = new System.Drawing.Point(20, 320);
            this._sectionCatalog.Name = "_sectionCatalog";
            this._sectionCatalog.Size = new System.Drawing.Size(1220, 150);
            this._sectionCatalog.TabIndex = 1;
            // 
            // _lblCatalog
            // 
            this._lblCatalog.AutoSize = true;
            this._lblCatalog.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblCatalog.ForeColor = clsFormTheme.TextSecondary;
            this._lblCatalog.Location = new System.Drawing.Point(0, 0);
            this._lblCatalog.Name = "_lblCatalog";
            this._lblCatalog.Size = new System.Drawing.Size(70, 20);
            this._lblCatalog.TabIndex = 0;
            this._lblCatalog.Text = "CATALOG";
            // 
            // _flowCatalog
            // 
            this._flowCatalog.AutoSize = true;
            this._flowCatalog.Controls.Add(this.btnCouponManager);
            this._flowCatalog.Controls.Add(this.btnSuppliers);
            this._flowCatalog.Controls.Add(this.btnCategories);
            this._flowCatalog.Controls.Add(this.btnProducts);
            this._flowCatalog.Dock = System.Windows.Forms.DockStyle.Top;
            this._flowCatalog.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._flowCatalog.Location = new System.Drawing.Point(0, 25);
            this._flowCatalog.Name = "_flowCatalog";
            this._flowCatalog.Size = new System.Drawing.Size(1220, 120);
            this._flowCatalog.TabIndex = 1;
            this._flowCatalog.WrapContents = false;
            // 
            // btnCouponManager
            // 
            this.btnCouponManager.BackColor = clsFormTheme.CardColor;
            this.btnCouponManager.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnCouponManager.FlatAppearance.BorderSize = 1;
            this.btnCouponManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCouponManager.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCouponManager.ForeColor = clsFormTheme.TextPrimary;
            this.btnCouponManager.Location = new System.Drawing.Point(3, 3);
            this.btnCouponManager.Name = "btnCouponManager";
            this.btnCouponManager.Size = new System.Drawing.Size(200, 110);
            this.btnCouponManager.TabIndex = 3;
            this.btnCouponManager.Text = "Coupon Manager";
            this.btnCouponManager.UseVisualStyleBackColor = false;
            this.btnCouponManager.Click += new System.EventHandler(this.btnCouponManager_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BackColor = clsFormTheme.CardColor;
            this.btnSuppliers.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnSuppliers.FlatAppearance.BorderSize = 1;
            this.btnSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuppliers.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnSuppliers.ForeColor = clsFormTheme.TextPrimary;
            this.btnSuppliers.Location = new System.Drawing.Point(209, 3);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(200, 110);
            this.btnSuppliers.TabIndex = 2;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.UseVisualStyleBackColor = false;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnCategories
            // 
            this.btnCategories.BackColor = clsFormTheme.CardColor;
            this.btnCategories.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnCategories.FlatAppearance.BorderSize = 1;
            this.btnCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCategories.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCategories.ForeColor = clsFormTheme.TextPrimary;
            this.btnCategories.Location = new System.Drawing.Point(415, 3);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(200, 110);
            this.btnCategories.TabIndex = 1;
            this.btnCategories.Text = "Categories";
            this.btnCategories.UseVisualStyleBackColor = false;
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.BackColor = clsFormTheme.CardColor;
            this.btnProducts.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnProducts.FlatAppearance.BorderSize = 1;
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnProducts.ForeColor = clsFormTheme.TextPrimary;
            this.btnProducts.Location = new System.Drawing.Point(621, 3);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(200, 110);
            this.btnProducts.TabIndex = 0;
            this.btnProducts.Text = "Products";
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // _sectionSales
            // 
            this._sectionSales.Controls.Add(this._lblSales);
            this._sectionSales.Controls.Add(this._flowSales);
            this._sectionSales.Dock = System.Windows.Forms.DockStyle.Top;
            this._sectionSales.Location = new System.Drawing.Point(20, 470);
            this._sectionSales.Name = "_sectionSales";
            this._sectionSales.Size = new System.Drawing.Size(1220, 150);
            this._sectionSales.TabIndex = 0;
            // 
            // _lblSales
            // 
            this._lblSales.AutoSize = true;
            this._lblSales.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblSales.ForeColor = clsFormTheme.TextSecondary;
            this._lblSales.Location = new System.Drawing.Point(0, 0);
            this._lblSales.Name = "_lblSales";
            this._lblSales.Size = new System.Drawing.Size(136, 20);
            this._lblSales.TabIndex = 0;
            this._lblSales.Text = "SALES & ORDERS";
            // 
            // _flowSales
            // 
            this._flowSales.AutoSize = true;
            this._flowSales.Controls.Add(this.btnPrintReceipt);
            this._flowSales.Controls.Add(this.btnReceiptSearch);
            this._flowSales.Controls.Add(this.btnPOS);
            this._flowSales.Dock = System.Windows.Forms.DockStyle.Top;
            this._flowSales.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._flowSales.Location = new System.Drawing.Point(0, 25);
            this._flowSales.Name = "_flowSales";
            this._flowSales.Size = new System.Drawing.Size(1220, 120);
            this._flowSales.TabIndex = 1;
            this._flowSales.WrapContents = false;
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.BackColor = clsFormTheme.CardColor;
            this.btnPrintReceipt.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnPrintReceipt.FlatAppearance.BorderSize = 1;
            this.btnPrintReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintReceipt.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnPrintReceipt.ForeColor = clsFormTheme.TextPrimary;
            this.btnPrintReceipt.Location = new System.Drawing.Point(3, 3);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(200, 110);
            this.btnPrintReceipt.TabIndex = 2;
            this.btnPrintReceipt.Text = "Print Receipt";
            this.btnPrintReceipt.UseVisualStyleBackColor = false;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // btnReceiptSearch
            // 
            this.btnReceiptSearch.BackColor = clsFormTheme.CardColor;
            this.btnReceiptSearch.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnReceiptSearch.FlatAppearance.BorderSize = 1;
            this.btnReceiptSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceiptSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnReceiptSearch.ForeColor = clsFormTheme.TextPrimary;
            this.btnReceiptSearch.Location = new System.Drawing.Point(209, 3);
            this.btnReceiptSearch.Name = "btnReceiptSearch";
            this.btnReceiptSearch.Size = new System.Drawing.Size(200, 110);
            this.btnReceiptSearch.TabIndex = 1;
            this.btnReceiptSearch.Text = "Receipt Search";
            this.btnReceiptSearch.UseVisualStyleBackColor = false;
            this.btnReceiptSearch.Click += new System.EventHandler(this.btnReceiptSearch_Click);
            // 
            // btnPOS
            // 
            this.btnPOS.BackColor = clsFormTheme.CardColor;
            this.btnPOS.FlatAppearance.BorderColor = clsFormTheme.CardBorderColor;
            this.btnPOS.FlatAppearance.BorderSize = 1;
            this.btnPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPOS.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnPOS.ForeColor = clsFormTheme.TextPrimary;
            this.btnPOS.Location = new System.Drawing.Point(415, 3);
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Size = new System.Drawing.Size(200, 110);
            this.btnPOS.TabIndex = 0;
            this.btnPOS.Text = "Point of Sale";
            this.btnPOS.UseVisualStyleBackColor = false;
            this.btnPOS.Click += new System.EventHandler(this.btnPOS_Click);
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 720);
            this.Controls.Add(this._contentPanel);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "frmMainMenu";
            this.Text = "Inventory Management System";
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this._contentPanel.ResumeLayout(false);
            this._contentPanel.PerformLayout();
            this._footerPanel.ResumeLayout(false);
            this._sectionAdministration.ResumeLayout(false);
            this._sectionAdministration.PerformLayout();
            this._flowAdministration.ResumeLayout(false);
            this._flowAdministration.PerformLayout();
            this._sectionInsights.ResumeLayout(false);
            this._sectionInsights.PerformLayout();
            this._flowInsights.ResumeLayout(false);
            this._flowInsights.PerformLayout();
            this._sectionCatalog.ResumeLayout(false);
            this._sectionCatalog.PerformLayout();
            this._flowCatalog.ResumeLayout(false);
            this._flowCatalog.PerformLayout();
            this._sectionSales.ResumeLayout(false);
            this._sectionSales.PerformLayout();
            this._flowSales.ResumeLayout(false);
            this._flowSales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel _footerPanel;
        private System.Windows.Forms.Panel _sectionAdministration;
        private System.Windows.Forms.Label _lblAdministration;
        private System.Windows.Forms.FlowLayoutPanel _flowAdministration;
        private System.Windows.Forms.Panel _sectionInsights;
        private System.Windows.Forms.Label _lblInsights;
        private System.Windows.Forms.FlowLayoutPanel _flowInsights;
        private System.Windows.Forms.Panel _sectionCatalog;
        private System.Windows.Forms.Label _lblCatalog;
        private System.Windows.Forms.FlowLayoutPanel _flowCatalog;
        private System.Windows.Forms.Panel _sectionSales;
        private System.Windows.Forms.Label _lblSales;
        private System.Windows.Forms.FlowLayoutPanel _flowSales;
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
