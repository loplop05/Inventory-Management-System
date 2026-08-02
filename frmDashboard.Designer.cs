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
            this._mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this._statsPanel = new System.Windows.Forms.Panel();
            this._inventoryPanel = new System.Windows.Forms.Panel();
            this.lblInventoryValue = new System.Windows.Forms.Label();
            this.lblTotalStock = new System.Windows.Forms.Label();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.lblInventoryLabel = new System.Windows.Forms.Label();
            this.lblStockLabel = new System.Windows.Forms.Label();
            this.lblProductsLabel = new System.Windows.Forms.Label();
            this._salesPanel = new System.Windows.Forms.Panel();
            this.lblOrderCount = new System.Windows.Forms.Label();
            this.lblTodaySales = new System.Windows.Forms.Label();
            this.lblOrdersLabel = new System.Windows.Forms.Label();
            this.lblSalesLabel = new System.Windows.Forms.Label();
            this._alertsPanel = new System.Windows.Forms.Panel();
            this.lblLowStockCount = new System.Windows.Forms.Label();
            this.lblLowStockLabel = new System.Windows.Forms.Label();
            this.btnViewLowStock = new System.Windows.Forms.Button();
            this._quickActionsPanel = new System.Windows.Forms.Panel();
            this.btnViewReports = new System.Windows.Forms.Button();
            this.btnViewRecentOrders = new System.Windows.Forms.Button();
            this.btnNewSale = new System.Windows.Forms.Button();
            this._bottomPanel = new System.Windows.Forms.TableLayoutPanel();
            this._recentOrdersPanel = new System.Windows.Forms.Panel();
            this.gridRecentOrders = new System.Windows.Forms.DataGridView();
            this.lblRecentOrdersTitle = new System.Windows.Forms.Label();
            this._topProductsPanel = new System.Windows.Forms.Panel();
            this.gridTopProducts = new System.Windows.Forms.DataGridView();
            this.lblTopProductsTitle = new System.Windows.Forms.Label();
            this._lowStockPanel = new System.Windows.Forms.Panel();
            this.gridLowStock = new System.Windows.Forms.DataGridView();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this._mainPanel.SuspendLayout();
            this._statsPanel.SuspendLayout();
            this._salesPanel.SuspendLayout();
            this._alertsPanel.SuspendLayout();
            this._quickActionsPanel.SuspendLayout();
            this._bottomPanel.SuspendLayout();
            this._recentOrdersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecentOrders)).BeginInit();
            this._topProductsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopProducts)).BeginInit();
            this._lowStockPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStock)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.ColumnCount = 2;
            this._mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._mainPanel.Controls.Add(this._statsPanel, 0, 0);
            this._mainPanel.Controls.Add(this._salesPanel, 1, 0);
            this._mainPanel.Controls.Add(this._alertsPanel, 0, 1);
            this._mainPanel.Controls.Add(this._quickActionsPanel, 1, 1);
            this._mainPanel.Controls.Add(this._bottomPanel, 0, 2);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.RowCount = 3;
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.Size = new System.Drawing.Size(800, 450);
            this._mainPanel.TabIndex = 0;
            // 
            // _statsPanel
            // 
            this._statsPanel.Controls.Add(this._inventoryPanel);
            this._statsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statsPanel.Location = new System.Drawing.Point(3, 3);
            this._statsPanel.Name = "_statsPanel";
            this._statsPanel.Size = new System.Drawing.Size(394, 114);
            this._statsPanel.TabIndex = 0;
            // 
            // _inventoryPanel
            // 
            this._inventoryPanel.Controls.Add(this.lblInventoryValue);
            this._inventoryPanel.Controls.Add(this.lblTotalStock);
            this._inventoryPanel.Controls.Add(this.lblTotalProducts);
            this._inventoryPanel.Controls.Add(this.lblInventoryLabel);
            this._inventoryPanel.Controls.Add(this.lblStockLabel);
            this._inventoryPanel.Controls.Add(this.lblProductsLabel);
            this._inventoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._inventoryPanel.Location = new System.Drawing.Point(0, 0);
            this._inventoryPanel.Name = "_inventoryPanel";
            this._inventoryPanel.Size = new System.Drawing.Size(394, 114);
            this._inventoryPanel.TabIndex = 0;
            // 
            // lblInventoryValue
            // 
            this.lblInventoryValue.AutoSize = true;
            this.lblInventoryValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblInventoryValue.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblInventoryValue.Location = new System.Drawing.Point(200, 70);
            this.lblInventoryValue.Name = "lblInventoryValue";
            this.lblInventoryValue.Size = new System.Drawing.Size(89, 30);
            this.lblInventoryValue.TabIndex = 5;
            this.lblInventoryValue.Text = "$0.00";
            // 
            // lblTotalStock
            // 
            this.lblTotalStock.AutoSize = true;
            this.lblTotalStock.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalStock.Location = new System.Drawing.Point(200, 45);
            this.lblTotalStock.Name = "lblTotalStock";
            this.lblTotalStock.Size = new System.Drawing.Size(45, 30);
            this.lblTotalStock.TabIndex = 4;
            this.lblTotalStock.Text = "0";
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTotalProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalProducts.Location = new System.Drawing.Point(200, 20);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(45, 30);
            this.lblTotalProducts.TabIndex = 3;
            this.lblTotalProducts.Text = "0";
            // 
            // lblInventoryLabel
            // 
            this.lblInventoryLabel.AutoSize = true;
            this.lblInventoryLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInventoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblInventoryLabel.Location = new System.Drawing.Point(10, 75);
            this.lblInventoryLabel.Name = "lblInventoryLabel";
            this.lblInventoryLabel.Size = new System.Drawing.Size(90, 15);
            this.lblInventoryLabel.TabIndex = 2;
            this.lblInventoryLabel.Text = "Inventory Value:";
            // 
            // lblStockLabel
            // 
            this.lblStockLabel.AutoSize = true;
            this.lblStockLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStockLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStockLabel.Location = new System.Drawing.Point(10, 50);
            this.lblStockLabel.Name = "lblStockLabel";
            this.lblStockLabel.Size = new System.Drawing.Size(71, 15);
            this.lblStockLabel.TabIndex = 1;
            this.lblStockLabel.Text = "Total Stock:";
            // 
            // lblProductsLabel
            // 
            this.lblProductsLabel.AutoSize = true;
            this.lblProductsLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProductsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblProductsLabel.Location = new System.Drawing.Point(10, 25);
            this.lblProductsLabel.Name = "lblProductsLabel";
            this.lblProductsLabel.Size = new System.Drawing.Size(82, 15);
            this.lblProductsLabel.TabIndex = 0;
            this.lblProductsLabel.Text = "Total Products:";
            // 
            // _salesPanel
            // 
            this._salesPanel.Controls.Add(this.lblOrderCount);
            this._salesPanel.Controls.Add(this.lblTodaySales);
            this._salesPanel.Controls.Add(this.lblOrdersLabel);
            this._salesPanel.Controls.Add(this.lblSalesLabel);
            this._salesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._salesPanel.Location = new System.Drawing.Point(403, 3);
            this._salesPanel.Name = "_salesPanel";
            this._salesPanel.Size = new System.Drawing.Size(394, 114);
            this._salesPanel.TabIndex = 1;
            // 
            // lblOrderCount
            // 
            this.lblOrderCount.AutoSize = true;
            this.lblOrderCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblOrderCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblOrderCount.Location = new System.Drawing.Point(200, 45);
            this.lblOrderCount.Name = "lblOrderCount";
            this.lblOrderCount.Size = new System.Drawing.Size(45, 30);
            this.lblOrderCount.TabIndex = 3;
            this.lblOrderCount.Text = "0";
            // 
            // lblTodaySales
            // 
            this.lblTodaySales.AutoSize = true;
            this.lblTodaySales.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTodaySales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblTodaySales.Location = new System.Drawing.Point(200, 20);
            this.lblTodaySales.Name = "lblTodaySales";
            this.lblTodaySales.Size = new System.Drawing.Size(89, 30);
            this.lblTodaySales.TabIndex = 2;
            this.lblTodaySales.Text = "$0.00";
            // 
            // lblOrdersLabel
            // 
            this.lblOrdersLabel.AutoSize = true;
            this.lblOrdersLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOrdersLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOrdersLabel.Location = new System.Drawing.Point(10, 50);
            this.lblOrdersLabel.Name = "lblOrdersLabel";
            this.lblOrdersLabel.Size = new System.Drawing.Size(72, 15);
            this.lblOrdersLabel.TabIndex = 1;
            this.lblOrdersLabel.Text = "Orders Today:";
            // 
            // lblSalesLabel
            // 
            this.lblSalesLabel.AutoSize = true;
            this.lblSalesLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSalesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSalesLabel.Location = new System.Drawing.Point(10, 25);
            this.lblSalesLabel.Name = "lblSalesLabel";
            this.lblSalesLabel.Size = new System.Drawing.Size(82, 15);
            this.lblSalesLabel.TabIndex = 0;
            this.lblSalesLabel.Text = "Sales Today:";
            // 
            // _alertsPanel
            // 
            this._alertsPanel.Controls.Add(this.lblLowStockCount);
            this._alertsPanel.Controls.Add(this.lblLowStockLabel);
            this._alertsPanel.Controls.Add(this.btnViewLowStock);
            this._alertsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._alertsPanel.Location = new System.Drawing.Point(3, 123);
            this._alertsPanel.Name = "_alertsPanel";
            this._alertsPanel.Size = new System.Drawing.Size(394, 114);
            this._alertsPanel.TabIndex = 2;
            // 
            // lblLowStockCount
            // 
            this.lblLowStockCount.AutoSize = true;
            this.lblLowStockCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLowStockCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblLowStockCount.Location = new System.Drawing.Point(200, 35);
            this.lblLowStockCount.Name = "lblLowStockCount";
            this.lblLowStockCount.Size = new System.Drawing.Size(56, 45);
            this.lblLowStockCount.TabIndex = 2;
            this.lblLowStockCount.Text = "0";
            // 
            // lblLowStockLabel
            // 
            this.lblLowStockLabel.AutoSize = true;
            this.lblLowStockLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLowStockLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLowStockLabel.Location = new System.Drawing.Point(10, 45);
            this.lblLowStockLabel.Name = "lblLowStockLabel";
            this.lblLowStockLabel.Size = new System.Drawing.Size(93, 15);
            this.lblLowStockLabel.TabIndex = 1;
            this.lblLowStockLabel.Text = "Low Stock Items:";
            // 
            // btnViewLowStock
            // 
            this.btnViewLowStock.Location = new System.Drawing.Point(10, 75);
            this.btnViewLowStock.Name = "btnViewLowStock";
            this.btnViewLowStock.Size = new System.Drawing.Size(120, 30);
            this.btnViewLowStock.TabIndex = 0;
            this.btnViewLowStock.Text = "View Products";
            this.btnViewLowStock.UseVisualStyleBackColor = true;
            // 
            // _quickActionsPanel
            // 
            this._quickActionsPanel.Controls.Add(this.btnViewReports);
            this._quickActionsPanel.Controls.Add(this.btnViewRecentOrders);
            this._quickActionsPanel.Controls.Add(this.btnNewSale);
            this._quickActionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._quickActionsPanel.Location = new System.Drawing.Point(403, 123);
            this._quickActionsPanel.Name = "_quickActionsPanel";
            this._quickActionsPanel.Size = new System.Drawing.Size(394, 114);
            this._quickActionsPanel.TabIndex = 3;
            // 
            // btnViewReports
            // 
            this.btnViewReports.Location = new System.Drawing.Point(10, 75);
            this.btnViewReports.Name = "btnViewReports";
            this.btnViewReports.Size = new System.Drawing.Size(120, 30);
            this.btnViewReports.TabIndex = 2;
            this.btnViewReports.Text = "Reports";
            this.btnViewReports.UseVisualStyleBackColor = true;
            // 
            // btnViewRecentOrders
            // 
            this.btnViewRecentOrders.Location = new System.Drawing.Point(140, 75);
            this.btnViewRecentOrders.Name = "btnViewRecentOrders";
            this.btnViewRecentOrders.Size = new System.Drawing.Size(120, 30);
            this.btnViewRecentOrders.TabIndex = 1;
            this.btnViewRecentOrders.Text = "Recent Orders";
            this.btnViewRecentOrders.UseVisualStyleBackColor = true;
            // 
            // btnNewSale
            // 
            this.btnNewSale.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNewSale.Location = new System.Drawing.Point(10, 20);
            this.btnNewSale.Name = "btnNewSale";
            this.btnNewSale.Size = new System.Drawing.Size(250, 45);
            this.btnNewSale.TabIndex = 0;
            this.btnNewSale.Text = "New Sale";
            this.btnNewSale.UseVisualStyleBackColor = true;
            // 
            // _bottomPanel
            // 
            this._bottomPanel.ColumnCount = 3;
            this._bottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._bottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._bottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._bottomPanel.Controls.Add(this._recentOrdersPanel, 0, 0);
            this._bottomPanel.Controls.Add(this._topProductsPanel, 1, 0);
            this._bottomPanel.Controls.Add(this._lowStockPanel, 2, 0);
            this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bottomPanel.Location = new System.Drawing.Point(3, 243);
            this._bottomPanel.Name = "_bottomPanel";
            this._bottomPanel.RowCount = 1;
            this._bottomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._bottomPanel.Size = new System.Drawing.Size(794, 204);
            this._bottomPanel.TabIndex = 4;
            // 
            // _recentOrdersPanel
            // 
            this._recentOrdersPanel.Controls.Add(this.gridRecentOrders);
            this._recentOrdersPanel.Controls.Add(this.lblRecentOrdersTitle);
            this._recentOrdersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._recentOrdersPanel.Location = new System.Drawing.Point(3, 3);
            this._recentOrdersPanel.Name = "_recentOrdersPanel";
            this._recentOrdersPanel.Size = new System.Drawing.Size(256, 198);
            this._recentOrdersPanel.TabIndex = 0;
            // 
            // gridRecentOrders
            // 
            this.gridRecentOrders.AllowUserToAddRows = false;
            this.gridRecentOrders.AllowUserToDeleteRows = false;
            this.gridRecentOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRecentOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRecentOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRecentOrders.Location = new System.Drawing.Point(0, 25);
            this.gridRecentOrders.Name = "gridRecentOrders";
            this.gridRecentOrders.ReadOnly = true;
            this.gridRecentOrders.RowHeadersVisible = false;
            this.gridRecentOrders.RowTemplate.Height = 24;
            this.gridRecentOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRecentOrders.Size = new System.Drawing.Size(256, 173);
            this.gridRecentOrders.TabIndex = 1;
            // 
            // lblRecentOrdersTitle
            // 
            this.lblRecentOrdersTitle.AutoSize = true;
            this.lblRecentOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRecentOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblRecentOrdersTitle.Location = new System.Drawing.Point(10, 5);
            this.lblRecentOrdersTitle.Name = "lblRecentOrdersTitle";
            this.lblRecentOrdersTitle.Size = new System.Drawing.Size(103, 19);
            this.lblRecentOrdersTitle.TabIndex = 0;
            this.lblRecentOrdersTitle.Text = "Recent Orders";
            // 
            // _topProductsPanel
            // 
            this._topProductsPanel.Controls.Add(this.gridTopProducts);
            this._topProductsPanel.Controls.Add(this.lblTopProductsTitle);
            this._topProductsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topProductsPanel.Location = new System.Drawing.Point(265, 3);
            this._topProductsPanel.Name = "_topProductsPanel";
            this._topProductsPanel.Size = new System.Drawing.Size(256, 198);
            this._topProductsPanel.TabIndex = 1;
            // 
            // gridTopProducts
            // 
            this.gridTopProducts.AllowUserToAddRows = false;
            this.gridTopProducts.AllowUserToDeleteRows = false;
            this.gridTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTopProducts.Location = new System.Drawing.Point(0, 25);
            this.gridTopProducts.Name = "gridTopProducts";
            this.gridTopProducts.ReadOnly = true;
            this.gridTopProducts.RowHeadersVisible = false;
            this.gridTopProducts.RowTemplate.Height = 24;
            this.gridTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTopProducts.Size = new System.Drawing.Size(256, 173);
            this.gridTopProducts.TabIndex = 1;
            // 
            // lblTopProductsTitle
            // 
            this.lblTopProductsTitle.AutoSize = true;
            this.lblTopProductsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTopProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTopProductsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTopProductsTitle.Name = "lblTopProductsTitle";
            this.lblTopProductsTitle.Size = new System.Drawing.Size(95, 19);
            this.lblTopProductsTitle.TabIndex = 0;
            this.lblTopProductsTitle.Text = "Top Products";
            // 
            // _lowStockPanel
            // 
            this._lowStockPanel.Controls.Add(this.gridLowStock);
            this._lowStockPanel.Controls.Add(this.lblLowStockTitle);
            this._lowStockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lowStockPanel.Location = new System.Drawing.Point(527, 3);
            this._lowStockPanel.Name = "_lowStockPanel";
            this._lowStockPanel.Size = new System.Drawing.Size(264, 198);
            this._lowStockPanel.TabIndex = 2;
            // 
            // gridLowStock
            // 
            this.gridLowStock.AllowUserToAddRows = false;
            this.gridLowStock.AllowUserToDeleteRows = false;
            this.gridLowStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLowStock.Location = new System.Drawing.Point(0, 25);
            this.gridLowStock.Name = "gridLowStock";
            this.gridLowStock.ReadOnly = true;
            this.gridLowStock.RowHeadersVisible = false;
            this.gridLowStock.RowTemplate.Height = 24;
            this.gridLowStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLowStock.Size = new System.Drawing.Size(264, 173);
            this.gridLowStock.TabIndex = 1;
            // 
            // lblLowStockTitle
            // 
            this.lblLowStockTitle.AutoSize = true;
            this.lblLowStockTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLowStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblLowStockTitle.Location = new System.Drawing.Point(10, 5);
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.Size = new System.Drawing.Size(82, 19);
            this.lblLowStockTitle.TabIndex = 0;
            this.lblLowStockTitle.Text = "Low Stock";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(720, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 30);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this._mainPanel);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "frmDashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this._mainPanel.ResumeLayout(false);
            this._statsPanel.ResumeLayout(false);
            this._statsPanel.PerformLayout();
            this._salesPanel.ResumeLayout(false);
            this._salesPanel.PerformLayout();
            this._alertsPanel.ResumeLayout(false);
            this._alertsPanel.PerformLayout();
            this._quickActionsPanel.ResumeLayout(false);
            this._bottomPanel.ResumeLayout(false);
            this._recentOrdersPanel.ResumeLayout(false);
            this._recentOrdersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecentOrders)).EndInit();
            this._topProductsPanel.ResumeLayout(false);
            this._topProductsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopProducts)).EndInit();
            this._lowStockPanel.ResumeLayout(false);
            this._lowStockPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainPanel;
        private System.Windows.Forms.Panel _statsPanel;
        private System.Windows.Forms.Panel _salesPanel;
        private System.Windows.Forms.Panel _alertsPanel;
        private System.Windows.Forms.Panel _quickActionsPanel;
        private System.Windows.Forms.TableLayoutPanel _bottomPanel;
        private System.Windows.Forms.Panel _recentOrdersPanel;
        private System.Windows.Forms.Panel _topProductsPanel;
        private System.Windows.Forms.Panel _lowStockPanel;
        private System.Windows.Forms.Panel _inventoryPanel;
        private System.Windows.Forms.Label lblInventoryValue;
        private System.Windows.Forms.Label lblTotalStock;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Label lblInventoryLabel;
        private System.Windows.Forms.Label lblStockLabel;
        private System.Windows.Forms.Label lblProductsLabel;
        private System.Windows.Forms.Label lblOrderCount;
        private System.Windows.Forms.Label lblTodaySales;
        private System.Windows.Forms.Label lblOrdersLabel;
        private System.Windows.Forms.Label lblSalesLabel;
        private System.Windows.Forms.Label lblLowStockCount;
        private System.Windows.Forms.Label lblLowStockLabel;
        private System.Windows.Forms.Button btnViewLowStock;
        private System.Windows.Forms.Button btnViewReports;
        private System.Windows.Forms.Button btnViewRecentOrders;
        private System.Windows.Forms.Button btnNewSale;
        private System.Windows.Forms.DataGridView gridRecentOrders;
        private System.Windows.Forms.Label lblRecentOrdersTitle;
        private System.Windows.Forms.DataGridView gridTopProducts;
        private System.Windows.Forms.Label lblTopProductsTitle;
        private System.Windows.Forms.DataGridView gridLowStock;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.Button btnRefresh;
    }
}
