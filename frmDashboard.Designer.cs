namespace InventoryManagementSystem
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this._contentPanel = new System.Windows.Forms.Panel();
            this._sectionTogglePanel = new System.Windows.Forms.Panel();
            this._btnSectionOverview = new System.Windows.Forms.Button();
            this._btnSectionSales = new System.Windows.Forms.Button();
            this._btnSectionInventory = new System.Windows.Forms.Button();
            this._btnSectionCustomers = new System.Windows.Forms.Button();
            this._pnlSectionOverview = new System.Windows.Forms.Panel();
            this._summaryCardsPanel = new System.Windows.Forms.TableLayoutPanel();
            this._cardTodaySales = new System.Windows.Forms.Panel();
            this.pnlSalesSparkline = new System.Windows.Forms.Panel();
            this.lblTodaySalesValue = new System.Windows.Forms.Label();
            this.lblTodaySalesLabel = new System.Windows.Forms.Label();
            this._cardTotalOrders = new System.Windows.Forms.Panel();
            this.pnlOrdersSparkline = new System.Windows.Forms.Panel();
            this.lblTotalOrdersValue = new System.Windows.Forms.Label();
            this.lblTotalOrdersLabel = new System.Windows.Forms.Label();
            this._cardLowStock = new System.Windows.Forms.Panel();
            this.lblLowStockValue = new System.Windows.Forms.Label();
            this.lblLowStockLabel = new System.Windows.Forms.Label();
            this._contentLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._recentActivityPanel = new System.Windows.Forms.Panel();
            this.gridRecentOrders = new System.Windows.Forms.DataGridView();
            this.lblRecentActivityTitle = new System.Windows.Forms.Label();
            this._topProductsPanel = new System.Windows.Forms.Panel();
            this.gridTopProducts = new System.Windows.Forms.DataGridView();
            this.lblTopProductsTitle = new System.Windows.Forms.Label();
            this._pnlSectionSales = new System.Windows.Forms.Panel();
            this._hourlySalesPanel = new System.Windows.Forms.Panel();
            this.pnlHourlyChart = new System.Windows.Forms.Panel();
            this.lblHourlySalesTitle = new System.Windows.Forms.Label();
            this._categoryPanel = new System.Windows.Forms.Panel();
            this.pnlCategoryChart = new System.Windows.Forms.Panel();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this._paymentPanel = new System.Windows.Forms.Panel();
            this.lblPaymentCash = new System.Windows.Forms.Label();
            this.lblPaymentVisa = new System.Windows.Forms.Label();
            this.lblPaymentOther = new System.Windows.Forms.Label();
            this.lblPaymentTitle = new System.Windows.Forms.Label();
            this._forecastPanel = new System.Windows.Forms.Panel();
            this._btnRunForecast = new System.Windows.Forms.Button();
            this._gridForecast = new System.Windows.Forms.DataGridView();
            this._lblForecastTitle = new System.Windows.Forms.Label();
            this._pnlSectionInventory = new System.Windows.Forms.Panel();
            this.gridLowStockProducts = new System.Windows.Forms.DataGridView();
            this.lblInventoryTitle = new System.Windows.Forms.Label();
            this._pnlSectionCustomers = new System.Windows.Forms.Panel();
            this._loyaltyPanel = new System.Windows.Forms.Panel();
            this.pnlLoyaltyChart = new System.Windows.Forms.Panel();
            this.lblLoyaltyTitle = new System.Windows.Forms.Label();
            this.gridTopLoyaltyMembers = new System.Windows.Forms.DataGridView();
            this.lblProfitMargin = new System.Windows.Forms.Label();
            this._customerAnalyticsPanel = new System.Windows.Forms.Panel();
            this.gridCustomerAnalytics = new System.Windows.Forms.DataGridView();
            this.lblCustomerAnalyticsTitle = new System.Windows.Forms.Label();
            this._segmentationPanel = new System.Windows.Forms.Panel();
            this._btnRunSegmentation = new System.Windows.Forms.Button();
            this._gridSegmentation = new System.Windows.Forms.DataGridView();
            this._lblSegmentationTitle = new System.Windows.Forms.Label();
            this._associationsPanel = new System.Windows.Forms.Panel();
            this._gridAssociations = new System.Windows.Forms.DataGridView();
            this._lblAssociationsTitle = new System.Windows.Forms.Label();
            this._contentPanel.SuspendLayout();
            this._sectionTogglePanel.SuspendLayout();
            this._pnlSectionOverview.SuspendLayout();
            this._summaryCardsPanel.SuspendLayout();
            this._cardTodaySales.SuspendLayout();
            this._cardTotalOrders.SuspendLayout();
            this._cardLowStock.SuspendLayout();
            this._contentLayoutPanel.SuspendLayout();
            this._recentActivityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecentOrders)).BeginInit();
            this._topProductsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopProducts)).BeginInit();
            this._pnlSectionSales.SuspendLayout();
            this._hourlySalesPanel.SuspendLayout();
            this._categoryPanel.SuspendLayout();
            this._paymentPanel.SuspendLayout();
            this._forecastPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gridForecast)).BeginInit();
            this._pnlSectionInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStockProducts)).BeginInit();
            this._pnlSectionCustomers.SuspendLayout();
            this._loyaltyPanel.SuspendLayout();
            this._segmentationPanel.SuspendLayout();
            this._associationsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopLoyaltyMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridSegmentation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridAssociations)).BeginInit();
            this._customerAnalyticsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerAnalytics)).BeginInit();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._pnlSectionCustomers);
            this._contentPanel.Controls.Add(this._pnlSectionInventory);
            this._contentPanel.Controls.Add(this._pnlSectionSales);
            this._contentPanel.Controls.Add(this._pnlSectionOverview);
            this._contentPanel.Controls.Add(this._sectionTogglePanel);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1379, 736);
            this._contentPanel.TabIndex = 1;
            // 
            // _sectionTogglePanel
            // 
            this._sectionTogglePanel.Controls.Add(this._btnSectionCustomers);
            this._sectionTogglePanel.Controls.Add(this._btnSectionInventory);
            this._sectionTogglePanel.Controls.Add(this._btnSectionSales);
            this._sectionTogglePanel.Controls.Add(this._btnSectionOverview);
            this._sectionTogglePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._sectionTogglePanel.Location = new System.Drawing.Point(0, 0);
            this._sectionTogglePanel.Name = "_sectionTogglePanel";
            this._sectionTogglePanel.Size = new System.Drawing.Size(1139, 50);
            this._sectionTogglePanel.TabIndex = 0;
            // 
            // _btnSectionOverview
            // 
            this._btnSectionOverview.Location = new System.Drawing.Point(10, 10);
            this._btnSectionOverview.Name = "_btnSectionOverview";
            this._btnSectionOverview.Size = new System.Drawing.Size(100, 30);
            this._btnSectionOverview.TabIndex = 0;
            this._btnSectionOverview.Text = "Overview";
            this._btnSectionOverview.UseVisualStyleBackColor = true;
            // 
            // _btnSectionSales
            // 
            this._btnSectionSales.Location = new System.Drawing.Point(120, 10);
            this._btnSectionSales.Name = "_btnSectionSales";
            this._btnSectionSales.Size = new System.Drawing.Size(120, 30);
            this._btnSectionSales.TabIndex = 1;
            this._btnSectionSales.Text = "Sales Analytics";
            this._btnSectionSales.UseVisualStyleBackColor = true;
            // 
            // _btnSectionInventory
            // 
            this._btnSectionInventory.Location = new System.Drawing.Point(250, 10);
            this._btnSectionInventory.Name = "_btnSectionInventory";
            this._btnSectionInventory.Size = new System.Drawing.Size(100, 30);
            this._btnSectionInventory.TabIndex = 2;
            this._btnSectionInventory.Text = "Inventory";
            this._btnSectionInventory.UseVisualStyleBackColor = true;
            // 
            // _btnSectionCustomers
            // 
            this._btnSectionCustomers.Location = new System.Drawing.Point(360, 10);
            this._btnSectionCustomers.Name = "_btnSectionCustomers";
            this._btnSectionCustomers.Size = new System.Drawing.Size(100, 30);
            this._btnSectionCustomers.TabIndex = 3;
            this._btnSectionCustomers.Text = "Customers";
            this._btnSectionCustomers.UseVisualStyleBackColor = true;
            // 
            // _pnlSectionOverview
            // 
            this._pnlSectionOverview.Controls.Add(this._contentLayoutPanel);
            this._pnlSectionOverview.Controls.Add(this._summaryCardsPanel);
            this._pnlSectionOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlSectionOverview.Location = new System.Drawing.Point(0, 50);
            this._pnlSectionOverview.Name = "_pnlSectionOverview";
            this._pnlSectionOverview.Size = new System.Drawing.Size(1139, 686);
            this._pnlSectionOverview.TabIndex = 0;
            this._pnlSectionOverview.Visible = true;
            // 
            // _summaryCardsPanel
            // 
            this._summaryCardsPanel.ColumnCount = 3;
            this._summaryCardsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._summaryCardsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._summaryCardsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._summaryCardsPanel.Controls.Add(this._cardTodaySales, 0, 0);
            this._summaryCardsPanel.Controls.Add(this._cardTotalOrders, 1, 0);
            this._summaryCardsPanel.Controls.Add(this._cardLowStock, 2, 0);
            this._summaryCardsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._summaryCardsPanel.Location = new System.Drawing.Point(0, 0);
            this._summaryCardsPanel.Name = "_summaryCardsPanel";
            this._summaryCardsPanel.RowCount = 1;
            this._summaryCardsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._summaryCardsPanel.Size = new System.Drawing.Size(1139, 100);
            this._summaryCardsPanel.TabIndex = 0;
            // 
            // _contentLayoutPanel
            // 
            this._contentLayoutPanel.ColumnCount = 2;
            this._contentLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this._contentLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this._contentLayoutPanel.Controls.Add(this._recentActivityPanel, 0, 0);
            this._contentLayoutPanel.Controls.Add(this._topProductsPanel, 0, 1);
            this._contentLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentLayoutPanel.Location = new System.Drawing.Point(0, 100);
            this._contentLayoutPanel.Name = "_contentLayoutPanel";
            this._contentLayoutPanel.RowCount = 2;
            this._contentLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._contentLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._contentLayoutPanel.Size = new System.Drawing.Size(1139, 586);
            this._contentLayoutPanel.TabIndex = 1;
            // 
            // _recentActivityPanel
            // 
            this._recentActivityPanel.Controls.Add(this.gridRecentOrders);
            this._recentActivityPanel.Controls.Add(this.lblRecentActivityTitle);
            this._recentActivityPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._recentActivityPanel.Location = new System.Drawing.Point(3, 3);
            this._recentActivityPanel.Name = "_recentActivityPanel";
            this._recentActivityPanel.Size = new System.Drawing.Size(677, 219);
            this._recentActivityPanel.TabIndex = 0;
            // 
            // gridRecentOrders
            // 
            this.gridRecentOrders.AllowUserToAddRows = false;
            this.gridRecentOrders.AllowUserToDeleteRows = false;
            this.gridRecentOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRecentOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRecentOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRecentOrders.Location = new System.Drawing.Point(0, 30);
            this.gridRecentOrders.Name = "gridRecentOrders";
            this.gridRecentOrders.ReadOnly = true;
            this.gridRecentOrders.RowHeadersVisible = false;
            this.gridRecentOrders.RowHeadersWidth = 51;
            this.gridRecentOrders.RowTemplate.Height = 24;
            this.gridRecentOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRecentOrders.Size = new System.Drawing.Size(677, 189);
            this.gridRecentOrders.TabIndex = 1;
            // 
            // lblRecentActivityTitle
            // 
            this.lblRecentActivityTitle.AutoSize = true;
            this.lblRecentActivityTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecentActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblRecentActivityTitle.Location = new System.Drawing.Point(10, 5);
            this.lblRecentActivityTitle.Name = "lblRecentActivityTitle";
            this.lblRecentActivityTitle.Size = new System.Drawing.Size(157, 28);
            this.lblRecentActivityTitle.TabIndex = 0;
            this.lblRecentActivityTitle.Text = "Recent Activity";
            // 
            // _topProductsPanel
            // 
            this._topProductsPanel.Controls.Add(this.gridTopProducts);
            this._topProductsPanel.Controls.Add(this.lblTopProductsTitle);
            this._topProductsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topProductsPanel.Location = new System.Drawing.Point(3, 296);
            this._topProductsPanel.Name = "_topProductsPanel";
            this._topProductsPanel.Size = new System.Drawing.Size(677, 287);
            this._topProductsPanel.TabIndex = 1;
            // 
            // _pnlSectionSales
            // 
            this._pnlSectionSales.Controls.Add(this._forecastPanel);
            this._pnlSectionSales.Controls.Add(this._paymentPanel);
            this._pnlSectionSales.Controls.Add(this._categoryPanel);
            this._pnlSectionSales.Controls.Add(this._hourlySalesPanel);
            this._pnlSectionSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlSectionSales.Location = new System.Drawing.Point(0, 50);
            this._pnlSectionSales.Name = "_pnlSectionSales";
            this._pnlSectionSales.Size = new System.Drawing.Size(1139, 686);
            this._pnlSectionSales.TabIndex = 1;
            this._pnlSectionSales.Visible = false;
            // 
            // _hourlySalesPanel
            // 
            this._hourlySalesPanel.Controls.Add(this.pnlHourlyChart);
            this._hourlySalesPanel.Controls.Add(this.lblHourlySalesTitle);
            this._hourlySalesPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._hourlySalesPanel.Location = new System.Drawing.Point(0, 0);
            this._hourlySalesPanel.Name = "_hourlySalesPanel";
            this._hourlySalesPanel.Size = new System.Drawing.Size(1139, 250);
            this._hourlySalesPanel.TabIndex = 0;
            // 
            // pnlHourlyChart
            // 
            this.pnlHourlyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHourlyChart.Location = new System.Drawing.Point(0, 30);
            this.pnlHourlyChart.Name = "pnlHourlyChart";
            this.pnlHourlyChart.Size = new System.Drawing.Size(1139, 220);
            this.pnlHourlyChart.TabIndex = 1;
            // 
            // lblHourlySalesTitle
            // 
            this.lblHourlySalesTitle.AutoSize = true;
            this.lblHourlySalesTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHourlySalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHourlySalesTitle.Location = new System.Drawing.Point(10, 5);
            this.lblHourlySalesTitle.Name = "lblHourlySalesTitle";
            this.lblHourlySalesTitle.Size = new System.Drawing.Size(125, 28);
            this.lblHourlySalesTitle.TabIndex = 0;
            this.lblHourlySalesTitle.Text = "Hourly Sales";
            // 
            // _categoryPanel
            // 
            this._categoryPanel.Controls.Add(this.pnlCategoryChart);
            this._categoryPanel.Controls.Add(this.lblCategoryTitle);
            this._categoryPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._categoryPanel.Location = new System.Drawing.Point(0, 250);
            this._categoryPanel.Name = "_categoryPanel";
            this._categoryPanel.Size = new System.Drawing.Size(1139, 250);
            this._categoryPanel.TabIndex = 1;
            // 
            // pnlCategoryChart
            // 
            this.pnlCategoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategoryChart.Location = new System.Drawing.Point(0, 30);
            this.pnlCategoryChart.Name = "pnlCategoryChart";
            this.pnlCategoryChart.Size = new System.Drawing.Size(1139, 220);
            this.pnlCategoryChart.TabIndex = 1;
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCategoryTitle.Location = new System.Drawing.Point(10, 5);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(140, 28);
            this.lblCategoryTitle.TabIndex = 0;
            this.lblCategoryTitle.Text = "Category Performance";
            // 
            // _paymentPanel
            // 
            this._paymentPanel.Controls.Add(this.lblPaymentOther);
            this._paymentPanel.Controls.Add(this.lblPaymentVisa);
            this._paymentPanel.Controls.Add(this.lblPaymentCash);
            this._paymentPanel.Controls.Add(this.lblPaymentTitle);
            this._paymentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._paymentPanel.Location = new System.Drawing.Point(0, 500);
            this._paymentPanel.Name = "_paymentPanel";
            this._paymentPanel.Size = new System.Drawing.Size(1139, 120);
            this._paymentPanel.TabIndex = 2;
            // 
            // lblPaymentTitle
            // 
            this.lblPaymentTitle.AutoSize = true;
            this.lblPaymentTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPaymentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPaymentTitle.Location = new System.Drawing.Point(10, 5);
            this.lblPaymentTitle.Name = "lblPaymentTitle";
            this.lblPaymentTitle.Size = new System.Drawing.Size(115, 28);
            this.lblPaymentTitle.TabIndex = 0;
            this.lblPaymentTitle.Text = "Payment Methods";
            // 
            // lblPaymentCash
            // 
            this.lblPaymentCash.AutoSize = true;
            this.lblPaymentCash.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPaymentCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblPaymentCash.Location = new System.Drawing.Point(10, 40);
            this.lblPaymentCash.Name = "lblPaymentCash";
            this.lblPaymentCash.Size = new System.Drawing.Size(45, 24);
            this.lblPaymentCash.TabIndex = 1;
            this.lblPaymentCash.Text = "Cash: $0";
            // 
            // lblPaymentVisa
            // 
            this.lblPaymentVisa.AutoSize = true;
            this.lblPaymentVisa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPaymentVisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblPaymentVisa.Location = new System.Drawing.Point(10, 70);
            this.lblPaymentVisa.Name = "lblPaymentVisa";
            this.lblPaymentVisa.Size = new System.Drawing.Size(45, 24);
            this.lblPaymentVisa.TabIndex = 2;
            this.lblPaymentVisa.Text = "Visa: $0";
            // 
            // lblPaymentOther
            // 
            this.lblPaymentOther.AutoSize = true;
            this.lblPaymentOther.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPaymentOther.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(85)))), ((int)(((byte)(247)))));
            this.lblPaymentOther.Location = new System.Drawing.Point(10, 100);
            this.lblPaymentOther.Name = "lblPaymentOther";
            this.lblPaymentOther.Size = new System.Drawing.Size(45, 24);
            this.lblPaymentOther.TabIndex = 3;
            this.lblPaymentOther.Text = "Other: $0";
            // 
            // _forecastPanel
            // 
            this._forecastPanel.Controls.Add(this._gridForecast);
            this._forecastPanel.Controls.Add(this._lblForecastTitle);
            this._forecastPanel.Controls.Add(this._btnRunForecast);
            this._forecastPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._forecastPanel.Location = new System.Drawing.Point(0, 500);
            this._forecastPanel.Name = "_forecastPanel";
            this._forecastPanel.Padding = new System.Windows.Forms.Padding(20);
            this._forecastPanel.Size = new System.Drawing.Size(1139, 186);
            this._forecastPanel.TabIndex = 3;
            // 
            // _btnRunForecast
            // 
            this._btnRunForecast.Location = new System.Drawing.Point(20, 20);
            this._btnRunForecast.Name = "_btnRunForecast";
            this._btnRunForecast.Size = new System.Drawing.Size(150, 40);
            this._btnRunForecast.TabIndex = 0;
            this._btnRunForecast.Text = "Run Forecast";
            this._btnRunForecast.UseVisualStyleBackColor = true;
            // 
            // _lblForecastTitle
            // 
            this._lblForecastTitle.AutoSize = true;
            this._lblForecastTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblForecastTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblForecastTitle.Location = new System.Drawing.Point(180, 30);
            this._lblForecastTitle.Name = "_lblForecastTitle";
            this._lblForecastTitle.Size = new System.Drawing.Size(0, 28);
            this._lblForecastTitle.TabIndex = 1;
            // 
            // _gridForecast
            // 
            this._gridForecast.AllowUserToAddRows = false;
            this._gridForecast.AllowUserToDeleteRows = false;
            this._gridForecast.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._gridForecast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridForecast.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridForecast.Location = new System.Drawing.Point(20, 70);
            this._gridForecast.Name = "_gridForecast";
            this._gridForecast.ReadOnly = true;
            this._gridForecast.RowHeadersVisible = false;
            this._gridForecast.RowHeadersWidth = 51;
            this._gridForecast.RowTemplate.Height = 24;
            this._gridForecast.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gridForecast.Size = new System.Drawing.Size(1099, 596);
            this._gridForecast.TabIndex = 2;
            // 
            // _pnlSectionInventory
            // 
            this._pnlSectionInventory.Controls.Add(this._associationsPanel);
            this._pnlSectionInventory.Controls.Add(this.gridLowStockProducts);
            this._pnlSectionInventory.Controls.Add(this.lblInventoryTitle);
            this._pnlSectionInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlSectionInventory.Location = new System.Drawing.Point(0, 50);
            this._pnlSectionInventory.Name = "_pnlSectionInventory";
            this._pnlSectionInventory.Size = new System.Drawing.Size(1139, 686);
            this._pnlSectionInventory.TabIndex = 2;
            this._pnlSectionInventory.Visible = false;
            // 
            // lblInventoryTitle
            // 
            this.lblInventoryTitle.AutoSize = true;
            this.lblInventoryTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInventoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblInventoryTitle.Location = new System.Drawing.Point(10, 5);
            this.lblInventoryTitle.Name = "lblInventoryTitle";
            this.lblInventoryTitle.Size = new System.Drawing.Size(133, 28);
            this.lblInventoryTitle.TabIndex = 0;
            this.lblInventoryTitle.Text = "Low Stock Products";
            // 
            // gridLowStockProducts
            // 
            this.gridLowStockProducts.AllowUserToAddRows = false;
            this.gridLowStockProducts.AllowUserToDeleteRows = false;
            this.gridLowStockProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLowStockProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLowStockProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLowStockProducts.Location = new System.Drawing.Point(0, 30);
            this.gridLowStockProducts.Name = "gridLowStockProducts";
            this.gridLowStockProducts.ReadOnly = true;
            this.gridLowStockProducts.RowHeadersVisible = false;
            this.gridLowStockProducts.RowHeadersWidth = 51;
            this.gridLowStockProducts.RowTemplate.Height = 24;
            this.gridLowStockProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLowStockProducts.Size = new System.Drawing.Size(1139, 656);
            this.gridLowStockProducts.TabIndex = 1;
            // 
            // _associationsPanel
            // 
            this._associationsPanel.Controls.Add(this._gridAssociations);
            this._associationsPanel.Controls.Add(this._lblAssociationsTitle);
            this._associationsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._associationsPanel.Location = new System.Drawing.Point(0, 30);
            this._associationsPanel.Name = "_associationsPanel";
            this._associationsPanel.Padding = new System.Windows.Forms.Padding(20);
            this._associationsPanel.Size = new System.Drawing.Size(1139, 656);
            this._associationsPanel.TabIndex = 2;
            // 
            // _lblAssociationsTitle
            // 
            this._lblAssociationsTitle.AutoSize = true;
            this._lblAssociationsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblAssociationsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblAssociationsTitle.Location = new System.Drawing.Point(20, 20);
            this._lblAssociationsTitle.Name = "_lblAssociationsTitle";
            this._lblAssociationsTitle.Size = new System.Drawing.Size(0, 28);
            this._lblAssociationsTitle.TabIndex = 0;
            // 
            // _gridAssociations
            // 
            this._gridAssociations.AllowUserToAddRows = false;
            this._gridAssociations.AllowUserToDeleteRows = false;
            this._gridAssociations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._gridAssociations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridAssociations.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridAssociations.Location = new System.Drawing.Point(20, 50);
            this._gridAssociations.Name = "_gridAssociations";
            this._gridAssociations.ReadOnly = true;
            this._gridAssociations.RowHeadersVisible = false;
            this._gridAssociations.RowHeadersWidth = 51;
            this._gridAssociations.RowTemplate.Height = 24;
            this._gridAssociations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gridAssociations.Size = new System.Drawing.Size(1099, 586);
            this._gridAssociations.TabIndex = 1;
            // 
            // _pnlSectionCustomers
            // 
            this._pnlSectionCustomers.Controls.Add(this._segmentationPanel);
            this._pnlSectionCustomers.Controls.Add(this._customerAnalyticsPanel);
            this._pnlSectionCustomers.Controls.Add(this._loyaltyPanel);
            this._pnlSectionCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlSectionCustomers.Location = new System.Drawing.Point(0, 50);
            this._pnlSectionCustomers.Name = "_pnlSectionCustomers";
            this._pnlSectionCustomers.Size = new System.Drawing.Size(1139, 686);
            this._pnlSectionCustomers.TabIndex = 3;
            this._pnlSectionCustomers.Visible = false;
            // 
            // _loyaltyPanel
            // 
            this._loyaltyPanel.Controls.Add(this.lblProfitMargin);
            this._loyaltyPanel.Controls.Add(this.pnlLoyaltyChart);
            this._loyaltyPanel.Controls.Add(this.lblLoyaltyTitle);
            this._loyaltyPanel.Controls.Add(this.gridTopLoyaltyMembers);
            this._loyaltyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._loyaltyPanel.Location = new System.Drawing.Point(0, 0);
            this._loyaltyPanel.Name = "_loyaltyPanel";
            this._loyaltyPanel.Size = new System.Drawing.Size(1139, 250);
            this._loyaltyPanel.TabIndex = 0;
            // 
            // pnlLoyaltyChart
            // 
            this.pnlLoyaltyChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLoyaltyChart.Location = new System.Drawing.Point(0, 30);
            this.pnlLoyaltyChart.Name = "pnlLoyaltyChart";
            this.pnlLoyaltyChart.Size = new System.Drawing.Size(250, 220);
            this.pnlLoyaltyChart.TabIndex = 1;
            // 
            // lblLoyaltyTitle
            // 
            this.lblLoyaltyTitle.AutoSize = true;
            this.lblLoyaltyTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoyaltyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLoyaltyTitle.Location = new System.Drawing.Point(10, 5);
            this.lblLoyaltyTitle.Name = "lblLoyaltyTitle";
            this.lblLoyaltyTitle.Size = new System.Drawing.Size(105, 28);
            this.lblLoyaltyTitle.TabIndex = 0;
            this.lblLoyaltyTitle.Text = "Loyalty Analytics";
            // 
            // gridTopLoyaltyMembers
            // 
            this.gridTopLoyaltyMembers.AllowUserToAddRows = false;
            this.gridTopLoyaltyMembers.AllowUserToDeleteRows = false;
            this.gridTopLoyaltyMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTopLoyaltyMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopLoyaltyMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTopLoyaltyMembers.Location = new System.Drawing.Point(250, 30);
            this.gridTopLoyaltyMembers.Name = "gridTopLoyaltyMembers";
            this.gridTopLoyaltyMembers.ReadOnly = true;
            this.gridTopLoyaltyMembers.RowHeadersVisible = false;
            this.gridTopLoyaltyMembers.RowHeadersWidth = 51;
            this.gridTopLoyaltyMembers.RowTemplate.Height = 24;
            this.gridTopLoyaltyMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTopLoyaltyMembers.Size = new System.Drawing.Size(889, 220);
            this.gridTopLoyaltyMembers.TabIndex = 2;
            // 
            // lblProfitMargin
            // 
            this.lblProfitMargin.AutoSize = true;
            this.lblProfitMargin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProfitMargin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblProfitMargin.Location = new System.Drawing.Point(260, 5);
            this.lblProfitMargin.Name = "lblProfitMargin";
            this.lblProfitMargin.Size = new System.Drawing.Size(0, 23);
            this.lblProfitMargin.TabIndex = 3;
            this.lblProfitMargin.Text = "";
            // 
            // _customerAnalyticsPanel
            // 
            this._customerAnalyticsPanel.Controls.Add(this.gridCustomerAnalytics);
            this._customerAnalyticsPanel.Controls.Add(this.lblCustomerAnalyticsTitle);
            this._customerAnalyticsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerAnalyticsPanel.Location = new System.Drawing.Point(0, 250);
            this._customerAnalyticsPanel.Name = "_customerAnalyticsPanel";
            this._customerAnalyticsPanel.Size = new System.Drawing.Size(1139, 436);
            this._customerAnalyticsPanel.TabIndex = 1;
            // 
            // lblCustomerAnalyticsTitle
            // 
            this.lblCustomerAnalyticsTitle.AutoSize = true;
            this.lblCustomerAnalyticsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCustomerAnalyticsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustomerAnalyticsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblCustomerAnalyticsTitle.Name = "lblCustomerAnalyticsTitle";
            this.lblCustomerAnalyticsTitle.Size = new System.Drawing.Size(146, 28);
            this.lblCustomerAnalyticsTitle.TabIndex = 0;
            this.lblCustomerAnalyticsTitle.Text = "Customer Analytics";
            // 
            // _segmentationPanel
            // 
            this._segmentationPanel.Controls.Add(this._gridSegmentation);
            this._segmentationPanel.Controls.Add(this._lblSegmentationTitle);
            this._segmentationPanel.Controls.Add(this._btnRunSegmentation);
            this._segmentationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._segmentationPanel.Location = new System.Drawing.Point(0, 436);
            this._segmentationPanel.Name = "_segmentationPanel";
            this._segmentationPanel.Padding = new System.Windows.Forms.Padding(20);
            this._segmentationPanel.Size = new System.Drawing.Size(1139, 250);
            this._segmentationPanel.TabIndex = 2;
            // 
            // _btnRunSegmentation
            // 
            this._btnRunSegmentation.Location = new System.Drawing.Point(20, 20);
            this._btnRunSegmentation.Name = "_btnRunSegmentation";
            this._btnRunSegmentation.Size = new System.Drawing.Size(150, 40);
            this._btnRunSegmentation.TabIndex = 0;
            this._btnRunSegmentation.Text = "Run Segmentation";
            this._btnRunSegmentation.UseVisualStyleBackColor = true;
            // 
            // _lblSegmentationTitle
            // 
            this._lblSegmentationTitle.AutoSize = true;
            this._lblSegmentationTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSegmentationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblSegmentationTitle.Location = new System.Drawing.Point(180, 30);
            this._lblSegmentationTitle.Name = "_lblSegmentationTitle";
            this._lblSegmentationTitle.Size = new System.Drawing.Size(0, 28);
            this._lblSegmentationTitle.TabIndex = 1;
            // 
            // _gridSegmentation
            // 
            this._gridSegmentation.AllowUserToAddRows = false;
            this._gridSegmentation.AllowUserToDeleteRows = false;
            this._gridSegmentation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._gridSegmentation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridSegmentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridSegmentation.Location = new System.Drawing.Point(20, 70);
            this._gridSegmentation.Name = "_gridSegmentation";
            this._gridSegmentation.ReadOnly = true;
            this._gridSegmentation.RowHeadersVisible = false;
            this._gridSegmentation.RowHeadersWidth = 51;
            this._gridSegmentation.RowTemplate.Height = 24;
            this._gridSegmentation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gridSegmentation.Size = new System.Drawing.Size(1099, 346);
            this._gridSegmentation.TabIndex = 2;
            // 
            // frmDashboard
            // 
            this.gridCustomerAnalytics.AllowUserToAddRows = false;
            this.gridCustomerAnalytics.AllowUserToDeleteRows = false;
            this.gridCustomerAnalytics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCustomerAnalytics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomerAnalytics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomerAnalytics.Location = new System.Drawing.Point(0, 30);
            this.gridCustomerAnalytics.Name = "gridCustomerAnalytics";
            this.gridCustomerAnalytics.ReadOnly = true;
            this.gridCustomerAnalytics.RowHeadersVisible = false;
            this.gridCustomerAnalytics.RowHeadersWidth = 51;
            this.gridCustomerAnalytics.RowTemplate.Height = 24;
            this.gridCustomerAnalytics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomerAnalytics.Size = new System.Drawing.Size(1139, 406);
            this.gridCustomerAnalytics.TabIndex = 1;
            // 
            // _cardTodaySales
            // 
            this._cardTodaySales.Controls.Add(this.pnlSalesSparkline);
            this._cardTodaySales.Controls.Add(this.lblTodaySalesValue);
            this._cardTodaySales.Controls.Add(this.lblTodaySalesLabel);
            this._cardTodaySales.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cardTodaySales.Location = new System.Drawing.Point(3, 3);
            this._cardTodaySales.Name = "_cardTodaySales";
            this._cardTodaySales.Size = new System.Drawing.Size(282, 94);
            this._cardTodaySales.TabIndex = 0;
            // 
            // pnlSalesSparkline
            // 
            this.pnlSalesSparkline.Location = new System.Drawing.Point(10, 60);
            this.pnlSalesSparkline.Name = "pnlSalesSparkline";
            this.pnlSalesSparkline.Size = new System.Drawing.Size(262, 25);
            this.pnlSalesSparkline.TabIndex = 2;
            // 
            // lblTodaySalesValue
            // 
            this.lblTodaySalesValue.AutoSize = true;
            this.lblTodaySalesValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTodaySalesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTodaySalesValue.Location = new System.Drawing.Point(10, 45);
            this.lblTodaySalesValue.Name = "lblTodaySalesValue";
            this.lblTodaySalesValue.Size = new System.Drawing.Size(126, 54);
            this.lblTodaySalesValue.TabIndex = 1;
            this.lblTodaySalesValue.Text = "$0.00";
            // 
            // lblTodaySalesLabel
            // 
            this.lblTodaySalesLabel.AutoSize = true;
            this.lblTodaySalesLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTodaySalesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTodaySalesLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTodaySalesLabel.Name = "lblTodaySalesLabel";
            this.lblTodaySalesLabel.Size = new System.Drawing.Size(108, 23);
            this.lblTodaySalesLabel.TabIndex = 0;
            this.lblTodaySalesLabel.Text = "Today\'s Sales";
            // 
            // _cardTotalOrders
            // 
            this._cardTotalOrders.Controls.Add(this.pnlOrdersSparkline);
            this._cardTotalOrders.Controls.Add(this.lblTotalOrdersValue);
            this._cardTotalOrders.Controls.Add(this.lblTotalOrdersLabel);
            this._cardTotalOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cardTotalOrders.Location = new System.Drawing.Point(291, 3);
            this._cardTotalOrders.Name = "_cardTotalOrders";
            this._cardTotalOrders.Size = new System.Drawing.Size(282, 94);
            this._cardTotalOrders.TabIndex = 1;
            // 
            // pnlOrdersSparkline
            // 
            this.pnlOrdersSparkline.Location = new System.Drawing.Point(10, 60);
            this.pnlOrdersSparkline.Name = "pnlOrdersSparkline";
            this.pnlOrdersSparkline.Size = new System.Drawing.Size(262, 25);
            this.pnlOrdersSparkline.TabIndex = 2;
            // 
            // lblTotalOrdersValue
            // 
            this.lblTotalOrdersValue.AutoSize = true;
            this.lblTotalOrdersValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrdersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTotalOrdersValue.Location = new System.Drawing.Point(10, 45);
            this.lblTotalOrdersValue.Name = "lblTotalOrdersValue";
            this.lblTotalOrdersValue.Size = new System.Drawing.Size(46, 54);
            this.lblTotalOrdersValue.TabIndex = 1;
            this.lblTotalOrdersValue.Text = "0";
            // 
            // lblTotalOrdersLabel
            // 
            this.lblTotalOrdersLabel.AutoSize = true;
            this.lblTotalOrdersLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalOrdersLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotalOrdersLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTotalOrdersLabel.Name = "lblTotalOrdersLabel";
            this.lblTotalOrdersLabel.Size = new System.Drawing.Size(102, 23);
            this.lblTotalOrdersLabel.TabIndex = 0;
            this.lblTotalOrdersLabel.Text = "Total Orders";
            // 
            // _cardLowStock
            // 
            this._cardLowStock.Controls.Add(this.lblLowStockValue);
            this._cardLowStock.Controls.Add(this.lblLowStockLabel);
            this._cardLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cardLowStock.Location = new System.Drawing.Point(579, 3);
            this._cardLowStock.Name = "_cardLowStock";
            this._cardLowStock.Size = new System.Drawing.Size(282, 94);
            this._cardLowStock.TabIndex = 2;
            // 
            // lblLowStockValue
            // 
            this.lblLowStockValue.AutoSize = true;
            this.lblLowStockValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLowStockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblLowStockValue.Location = new System.Drawing.Point(10, 45);
            this.lblLowStockValue.Name = "lblLowStockValue";
            this.lblLowStockValue.Size = new System.Drawing.Size(46, 54);
            this.lblLowStockValue.TabIndex = 1;
            this.lblLowStockValue.Text = "0";
            // 
            // lblLowStockLabel
            // 
            this.lblLowStockLabel.AutoSize = true;
            this.lblLowStockLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLowStockLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLowStockLabel.Location = new System.Drawing.Point(10, 15);
            this.lblLowStockLabel.Name = "lblLowStockLabel";
            this.lblLowStockLabel.Size = new System.Drawing.Size(133, 23);
            this.lblLowStockLabel.TabIndex = 0;
            this.lblLowStockLabel.Text = "Low Stock Alerts";
            // 
            // gridTopProducts
            // 
            this.gridTopProducts.AllowUserToAddRows = false;
            this.gridTopProducts.AllowUserToDeleteRows = false;
            this.gridTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTopProducts.Location = new System.Drawing.Point(0, 30);
            this.gridTopProducts.Name = "gridTopProducts";
            this.gridTopProducts.ReadOnly = true;
            this.gridTopProducts.RowHeadersVisible = false;
            this.gridTopProducts.RowHeadersWidth = 51;
            this.gridTopProducts.RowTemplate.Height = 24;
            this.gridTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTopProducts.Size = new System.Drawing.Size(677, 257);
            this.gridTopProducts.TabIndex = 1;
            // 
            // lblTopProductsTitle
            // 
            this.lblTopProductsTitle.AutoSize = true;
            this.lblTopProductsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTopProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTopProductsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTopProductsTitle.Name = "lblTopProductsTitle";
            this.lblTopProductsTitle.Size = new System.Drawing.Size(112, 28);
            this.lblTopProductsTitle.TabIndex = 0;
            this.lblTopProductsTitle.Text = "Top Products";
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1619, 736);
            this.Controls.Add(this._contentPanel);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "frmDashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this._contentPanel.ResumeLayout(false);
            this._sectionTogglePanel.ResumeLayout(false);
            this._pnlSectionOverview.ResumeLayout(false);
            this._summaryCardsPanel.ResumeLayout(false);
            this._cardTodaySales.ResumeLayout(false);
            this._cardTodaySales.PerformLayout();
            this._cardTotalOrders.ResumeLayout(false);
            this._cardTotalOrders.PerformLayout();
            this._cardLowStock.ResumeLayout(false);
            this._cardLowStock.PerformLayout();
            this._contentLayoutPanel.ResumeLayout(false);
            this._recentActivityPanel.ResumeLayout(false);
            this._recentActivityPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecentOrders)).EndInit();
            this._topProductsPanel.ResumeLayout(false);
            this._topProductsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopProducts)).EndInit();
            this._pnlSectionSales.ResumeLayout(false);
            this._hourlySalesPanel.ResumeLayout(false);
            this._hourlySalesPanel.PerformLayout();
            this._categoryPanel.ResumeLayout(false);
            this._categoryPanel.PerformLayout();
            this._paymentPanel.ResumeLayout(false);
            this._paymentPanel.PerformLayout();
            this._forecastPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._gridForecast)).EndInit();
            this._pnlSectionInventory.ResumeLayout(false);
            this._pnlSectionInventory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStockProducts)).EndInit();
            this._pnlSectionCustomers.ResumeLayout(false);
            this._loyaltyPanel.ResumeLayout(false);
            this._loyaltyPanel.PerformLayout();
            this._segmentationPanel.ResumeLayout(false);
            this._associationsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTopLoyaltyMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridSegmentation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridAssociations)).EndInit();
            this._customerAnalyticsPanel.ResumeLayout(false);
            this._customerAnalyticsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerAnalytics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel _sectionTogglePanel;
        private System.Windows.Forms.Button _btnSectionOverview;
        private System.Windows.Forms.Button _btnSectionSales;
        private System.Windows.Forms.Button _btnSectionInventory;
        private System.Windows.Forms.Button _btnSectionCustomers;
        private System.Windows.Forms.Panel _pnlSectionOverview;
        private System.Windows.Forms.TableLayoutPanel _summaryCardsPanel;
        private System.Windows.Forms.Panel _cardTodaySales;
        private System.Windows.Forms.Label lblTodaySalesValue;
        private System.Windows.Forms.Label lblTodaySalesLabel;
        private System.Windows.Forms.Panel pnlSalesSparkline;
        private System.Windows.Forms.Panel _cardTotalOrders;
        private System.Windows.Forms.Label lblTotalOrdersValue;
        private System.Windows.Forms.Label lblTotalOrdersLabel;
        private System.Windows.Forms.Panel pnlOrdersSparkline;
        private System.Windows.Forms.Panel _cardLowStock;
        private System.Windows.Forms.Label lblLowStockValue;
        private System.Windows.Forms.Label lblLowStockLabel;
        private System.Windows.Forms.TableLayoutPanel _contentLayoutPanel;
        private System.Windows.Forms.Panel _recentActivityPanel;
        private System.Windows.Forms.DataGridView gridRecentOrders;
        private System.Windows.Forms.Label lblRecentActivityTitle;
        private System.Windows.Forms.Panel _topProductsPanel;
        private System.Windows.Forms.DataGridView gridTopProducts;
        private System.Windows.Forms.Label lblTopProductsTitle;
        private System.Windows.Forms.Panel _pnlSectionSales;
        private System.Windows.Forms.Panel _hourlySalesPanel;
        private System.Windows.Forms.Panel pnlHourlyChart;
        private System.Windows.Forms.Label lblHourlySalesTitle;
        private System.Windows.Forms.Panel _categoryPanel;
        private System.Windows.Forms.Panel pnlCategoryChart;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Panel _paymentPanel;
        private System.Windows.Forms.Label lblPaymentCash;
        private System.Windows.Forms.Label lblPaymentVisa;
        private System.Windows.Forms.Label lblPaymentOther;
        private System.Windows.Forms.Label lblPaymentTitle;
        private System.Windows.Forms.Panel _forecastPanel;
        private System.Windows.Forms.Button _btnRunForecast;
        private System.Windows.Forms.DataGridView _gridForecast;
        private System.Windows.Forms.Label _lblForecastTitle;
        private System.Windows.Forms.Panel _pnlSectionInventory;
        private System.Windows.Forms.DataGridView gridLowStockProducts;
        private System.Windows.Forms.Label lblInventoryTitle;
        private System.Windows.Forms.Panel _pnlSectionCustomers;
        private System.Windows.Forms.Panel _loyaltyPanel;
        private System.Windows.Forms.Panel pnlLoyaltyChart;
        private System.Windows.Forms.Label lblLoyaltyTitle;
        private System.Windows.Forms.DataGridView gridTopLoyaltyMembers;
        private System.Windows.Forms.Label lblProfitMargin;
        private System.Windows.Forms.Panel _customerAnalyticsPanel;
        private System.Windows.Forms.DataGridView gridCustomerAnalytics;
        private System.Windows.Forms.Label lblCustomerAnalyticsTitle;
        private System.Windows.Forms.Panel _segmentationPanel;
        private System.Windows.Forms.Button _btnRunSegmentation;
        private System.Windows.Forms.DataGridView _gridSegmentation;
        private System.Windows.Forms.Label _lblSegmentationTitle;
        private System.Windows.Forms.Panel _associationsPanel;
        private System.Windows.Forms.DataGridView _gridAssociations;
        private System.Windows.Forms.Label _lblAssociationsTitle;
    }
}
