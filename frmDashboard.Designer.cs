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
            this._summaryCardsPanel = new System.Windows.Forms.TableLayoutPanel();
            this._cardTodaySales = new System.Windows.Forms.Panel();
            this.lblTodaySalesValue = new System.Windows.Forms.Label();
            this.lblTodaySalesLabel = new System.Windows.Forms.Label();
            this._cardTotalOrders = new System.Windows.Forms.Panel();
            this.lblTotalOrdersValue = new System.Windows.Forms.Label();
            this.lblTotalOrdersLabel = new System.Windows.Forms.Label();
            this._cardLowStock = new System.Windows.Forms.Panel();
            this.lblLowStockValue = new System.Windows.Forms.Label();
            this.lblLowStockLabel = new System.Windows.Forms.Label();
            this._recentActivityPanel = new System.Windows.Forms.Panel();
            this.gridRecentOrders = new System.Windows.Forms.DataGridView();
            this.lblRecentActivityTitle = new System.Windows.Forms.Label();
            this._topProductsPanel = new System.Windows.Forms.Panel();
            this.gridTopProducts = new System.Windows.Forms.DataGridView();
            this.lblTopProductsTitle = new System.Windows.Forms.Label();
            this._contentLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._summaryCardsPanel.SuspendLayout();
            this._cardTodaySales.SuspendLayout();
            this._cardTotalOrders.SuspendLayout();
            this._cardLowStock.SuspendLayout();
            this._recentActivityPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecentOrders)).BeginInit();
            this._topProductsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopProducts)).BeginInit();
            this._contentLayoutPanel.SuspendLayout();
            this._contentPanel.SuspendLayout();
            this.SuspendLayout();
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
            this._summaryCardsPanel.Size = new System.Drawing.Size(1619, 100);
            this._summaryCardsPanel.TabIndex = 0;
            // 
            // _cardTodaySales
            // 
            this._cardTodaySales.Controls.Add(this.lblTodaySalesValue);
            this._cardTodaySales.Controls.Add(this.lblTodaySalesLabel);
            this._cardTodaySales.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cardTodaySales.Location = new System.Drawing.Point(3, 3);
            this._cardTodaySales.Name = "_cardTodaySales";
            this._cardTodaySales.Size = new System.Drawing.Size(533, 94);
            this._cardTodaySales.TabIndex = 0;
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
            this._cardTotalOrders.Controls.Add(this.lblTotalOrdersValue);
            this._cardTotalOrders.Controls.Add(this.lblTotalOrdersLabel);
            this._cardTotalOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cardTotalOrders.Location = new System.Drawing.Point(542, 3);
            this._cardTotalOrders.Name = "_cardTotalOrders";
            this._cardTotalOrders.Size = new System.Drawing.Size(533, 94);
            this._cardTotalOrders.TabIndex = 1;
            // 
            // lblTotalOrdersValue
            // 
            this.lblTotalOrdersValue.AutoSize = true;
            this.lblTotalOrdersValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrdersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblTotalOrdersValue.Location = new System.Drawing.Point(10, 45);
            this.lblTotalOrdersValue.Name = "lblTotalOrdersValue";
            this.lblTotalOrdersValue.Size = new System.Drawing.Size(54, 54);
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
            this.lblTotalOrdersLabel.Size = new System.Drawing.Size(96, 23);
            this.lblTotalOrdersLabel.TabIndex = 0;
            this.lblTotalOrdersLabel.Text = "Total Orders";
            // 
            // _cardLowStock
            // 
            this._cardLowStock.Controls.Add(this.lblLowStockValue);
            this._cardLowStock.Controls.Add(this.lblLowStockLabel);
            this._cardLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cardLowStock.Location = new System.Drawing.Point(1081, 3);
            this._cardLowStock.Name = "_cardLowStock";
            this._cardLowStock.Size = new System.Drawing.Size(535, 94);
            this._cardLowStock.TabIndex = 2;
            this._cardLowStock.Click += new System.EventHandler(this._cardLowStock_Click);
            // 
            // lblLowStockValue
            // 
            this.lblLowStockValue.AutoSize = true;
            this.lblLowStockValue.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblLowStockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblLowStockValue.Location = new System.Drawing.Point(10, 45);
            this.lblLowStockValue.Name = "lblLowStockValue";
            this.lblLowStockValue.Size = new System.Drawing.Size(54, 54);
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
            // _recentActivityPanel
            // 
            this._recentActivityPanel.Controls.Add(this.gridRecentOrders);
            this._recentActivityPanel.Controls.Add(this.lblRecentActivityTitle);
            this._recentActivityPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._recentActivityPanel.Location = new System.Drawing.Point(3, 3);
            this._recentActivityPanel.Name = "_recentActivityPanel";
            this._recentActivityPanel.Size = new System.Drawing.Size(969, 312);
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
            this.gridRecentOrders.Size = new System.Drawing.Size(969, 282);
            this.gridRecentOrders.TabIndex = 1;
            // 
            // lblRecentActivityTitle
            // 
            this.lblRecentActivityTitle.AutoSize = true;
            this.lblRecentActivityTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecentActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblRecentActivityTitle.Location = new System.Drawing.Point(10, 5);
            this.lblRecentActivityTitle.Name = "lblRecentActivityTitle";
            this.lblRecentActivityTitle.Size = new System.Drawing.Size(121, 28);
            this.lblRecentActivityTitle.TabIndex = 0;
            this.lblRecentActivityTitle.Text = "Recent Orders";
            // 
            // _topProductsPanel
            // 
            this._topProductsPanel.Controls.Add(this.gridTopProducts);
            this._topProductsPanel.Controls.Add(this.lblTopProductsTitle);
            this._topProductsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topProductsPanel.Location = new System.Drawing.Point(3, 321);
            this._topProductsPanel.Name = "_topProductsPanel";
            this._topProductsPanel.Size = new System.Drawing.Size(969, 312);
            this._topProductsPanel.TabIndex = 1;
            // 
            // gridTopProducts
            // 
            this.gridTopProducts.AllowUserToAddRows = false;
            this.gridTopProducts.AllowUserToDeleteRows = false;
            this.gridTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTopProducts.Location = new System.Drawing.Point(0, 0);
            this.gridTopProducts.Name = "gridTopProducts";
            this.gridTopProducts.ReadOnly = true;
            this.gridTopProducts.RowHeadersVisible = false;
            this.gridTopProducts.RowHeadersWidth = 51;
            this.gridTopProducts.RowTemplate.Height = 24;
            this.gridTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTopProducts.Size = new System.Drawing.Size(969, 312);
            this.gridTopProducts.TabIndex = 1;
            // 
            // lblTopProductsTitle
            // 
            this.lblTopProductsTitle.AutoSize = true;
            this.lblTopProductsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTopProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTopProductsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblTopProductsTitle.Name = "lblTopProductsTitle";
            this.lblTopProductsTitle.Size = new System.Drawing.Size(135, 28);
            this.lblTopProductsTitle.TabIndex = 0;
            this.lblTopProductsTitle.Text = "Top Products";
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
            this._contentLayoutPanel.Size = new System.Drawing.Size(1619, 636);
            this._contentLayoutPanel.TabIndex = 1;
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._contentLayoutPanel);
            this._contentPanel.Controls.Add(this._summaryCardsPanel);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1619, 736);
            this._contentPanel.TabIndex = 0;
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
            this._summaryCardsPanel.ResumeLayout(false);
            this._cardTodaySales.ResumeLayout(false);
            this._cardTodaySales.PerformLayout();
            this._cardTotalOrders.ResumeLayout(false);
            this._cardTotalOrders.PerformLayout();
            this._cardLowStock.ResumeLayout(false);
            this._cardLowStock.PerformLayout();
            this._recentActivityPanel.ResumeLayout(false);
            this._recentActivityPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecentOrders)).EndInit();
            this._topProductsPanel.ResumeLayout(false);
            this._topProductsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopProducts)).EndInit();
            this._contentLayoutPanel.ResumeLayout(false);
            this._contentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.TableLayoutPanel _summaryCardsPanel;
        private System.Windows.Forms.Panel _cardTodaySales;
        private System.Windows.Forms.Label lblTodaySalesValue;
        private System.Windows.Forms.Label lblTodaySalesLabel;
        private System.Windows.Forms.Panel _cardTotalOrders;
        private System.Windows.Forms.Label lblTotalOrdersValue;
        private System.Windows.Forms.Label lblTotalOrdersLabel;
        private System.Windows.Forms.Panel _cardLowStock;
        private System.Windows.Forms.Label lblLowStockValue;
        private System.Windows.Forms.Label lblLowStockLabel;
        private System.Windows.Forms.Panel _recentActivityPanel;
        private System.Windows.Forms.DataGridView gridRecentOrders;
        private System.Windows.Forms.Label lblRecentActivityTitle;
        private System.Windows.Forms.Panel _topProductsPanel;
        private System.Windows.Forms.DataGridView gridTopProducts;
        private System.Windows.Forms.Label lblTopProductsTitle;
        private System.Windows.Forms.TableLayoutPanel _contentLayoutPanel;
    }
}
