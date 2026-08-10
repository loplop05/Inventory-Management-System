namespace InventoryManagementSystem
{
    partial class frmDailyReport
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

        private void InitializeComponent()
        {
            this._sidebar = new InventoryManagementSystem.ucSidebarNav();
            this._contentPanel = new System.Windows.Forms.Panel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._lblHeaderTitle = new System.Windows.Forms.Label();
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._summaryPanel = new System.Windows.Forms.TableLayoutPanel();
            this._lblOrders = new System.Windows.Forms.Label();
            this._lblSubtotal = new System.Windows.Forms.Label();
            this._lblTax = new System.Windows.Forms.Label();
            this._lblRevenue = new System.Windows.Forms.Label();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._ordersPanel = new System.Windows.Forms.Panel();
            this._gridOrders = new System.Windows.Forms.DataGridView();
            this._lblOrdersTitle = new System.Windows.Forms.Label();
            this._topProductsPanel = new System.Windows.Forms.Panel();
            this._gridTopProducts = new System.Windows.Forms.DataGridView();
            this._lblTopProductsTitle = new System.Windows.Forms.Label();
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btnClose = new System.Windows.Forms.Button();
            this._btnExportCsv = new System.Windows.Forms.Button();
            this._btnExportHtml = new System.Windows.Forms.Button();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._btnShiftHistory = new System.Windows.Forms.Button();
            this._contextMenuOrders = new System.Windows.Forms.ContextMenuStrip();
            this._menuViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this._menuPrintReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this._menuRefund = new System.Windows.Forms.ToolStripMenuItem();
            this._menuVoid = new System.Windows.Forms.ToolStripMenuItem();
            this._contentPanel.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._rootLayout.SuspendLayout();
            this._summaryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._ordersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gridOrders)).BeginInit();
            this._topProductsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gridTopProducts)).BeginInit();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _sidebar
            // 
            this._sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this._sidebar.Location = new System.Drawing.Point(0, 0);
            this._sidebar.Name = "_sidebar";
            this._sidebar.Size = new System.Drawing.Size(250, 800);
            this._sidebar.TabIndex = 0;
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._headerPanel);
            this._contentPanel.Controls.Add(this._rootLayout);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(250, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1150, 800);
            this._contentPanel.TabIndex = 1;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this._headerPanel.Controls.Add(this._lblHeaderTitle);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1150, 60);
            this._headerPanel.TabIndex = 0;
            // 
            // _lblHeaderTitle
            // 
            this._lblHeaderTitle.AutoSize = true;
            this._lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this._lblHeaderTitle.Location = new System.Drawing.Point(20, 15);
            this._lblHeaderTitle.Name = "_lblHeaderTitle";
            this._lblHeaderTitle.Size = new System.Drawing.Size(180, 32);
            this._lblHeaderTitle.TabIndex = 0;
            this._lblHeaderTitle.Text = "Daily Report";
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Controls.Add(this._summaryPanel, 0, 0);
            this._rootLayout.Controls.Add(this._splitContainer, 0, 1);
            this._rootLayout.Controls.Add(this._buttonsPanel, 0, 2);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 60);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 3;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this._rootLayout.Size = new System.Drawing.Size(1150, 740);
            this._rootLayout.TabIndex = 1;
            // 
            // _summaryPanel
            // 
            this._summaryPanel.ColumnCount = 4;
            this._summaryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._summaryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._summaryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._summaryPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._summaryPanel.Controls.Add(this._lblOrders, 0, 0);
            this._summaryPanel.Controls.Add(this._lblSubtotal, 1, 0);
            this._summaryPanel.Controls.Add(this._lblTax, 2, 0);
            this._summaryPanel.Controls.Add(this._lblRevenue, 3, 0);
            this._summaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summaryPanel.Location = new System.Drawing.Point(3, 3);
            this._summaryPanel.Name = "_summaryPanel";
            this._summaryPanel.RowCount = 1;
            this._summaryPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._summaryPanel.Size = new System.Drawing.Size(1144, 90);
            this._summaryPanel.TabIndex = 0;
            // 
            // _lblOrders
            // 
            this._lblOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lblOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblOrders.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblOrders.ForeColor = System.Drawing.Color.White;
            this._lblOrders.Location = new System.Drawing.Point(6, 6);
            this._lblOrders.Margin = new System.Windows.Forms.Padding(6);
            this._lblOrders.Name = "_lblOrders";
            this._lblOrders.Size = new System.Drawing.Size(244, 78);
            this._lblOrders.TabIndex = 0;
            this._lblOrders.Text = "Orders\r\n0";
            this._lblOrders.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblSubtotal
            // 
            this._lblSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lblSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblSubtotal.ForeColor = System.Drawing.Color.White;
            this._lblSubtotal.Location = new System.Drawing.Point(262, 6);
            this._lblSubtotal.Margin = new System.Windows.Forms.Padding(6);
            this._lblSubtotal.Name = "_lblSubtotal";
            this._lblSubtotal.Size = new System.Drawing.Size(244, 78);
            this._lblSubtotal.TabIndex = 1;
            this._lblSubtotal.Text = "Subtotal\r\n$0.00";
            this._lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblTax
            // 
            this._lblTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lblTax.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblTax.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTax.ForeColor = System.Drawing.Color.White;
            this._lblTax.Location = new System.Drawing.Point(518, 6);
            this._lblTax.Margin = new System.Windows.Forms.Padding(6);
            this._lblTax.Name = "_lblTax";
            this._lblTax.Size = new System.Drawing.Size(245, 78);
            this._lblTax.TabIndex = 2;
            this._lblTax.Text = "Tax\r\n$0.00";
            this._lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblRevenue
            // 
            this._lblRevenue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lblRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblRevenue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblRevenue.ForeColor = System.Drawing.Color.White;
            this._lblRevenue.Location = new System.Drawing.Point(775, 6);
            this._lblRevenue.Margin = new System.Windows.Forms.Padding(6);
            this._lblRevenue.Name = "_lblRevenue";
            this._lblRevenue.Size = new System.Drawing.Size(247, 78);
            this._lblRevenue.TabIndex = 3;
            this._lblRevenue.Text = "Revenue\r\n$0.00";
            this._lblRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(3, 99);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._ordersPanel);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._topProductsPanel);
            this._splitContainer.Size = new System.Drawing.Size(1144, 586);
            this._splitContainer.SplitterDistance = 297;
            this._splitContainer.TabIndex = 1;
            // 
            // _ordersPanel
            // 
            this._ordersPanel.BackColor = System.Drawing.Color.White;
            this._ordersPanel.Controls.Add(this._gridOrders);
            this._ordersPanel.Controls.Add(this._lblOrdersTitle);
            this._ordersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ordersPanel.Location = new System.Drawing.Point(0, 0);
            this._ordersPanel.Name = "_ordersPanel";
            this._ordersPanel.Padding = new System.Windows.Forms.Padding(12);
            this._ordersPanel.Size = new System.Drawing.Size(1144, 297);
            this._ordersPanel.TabIndex = 0;
            this._ordersPanel.AutoScroll = true;
            // 
            // _gridOrders
            // 
            this._gridOrders.AllowUserToAddRows = false;
            this._gridOrders.AllowUserToDeleteRows = false;
            this._gridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridOrders.Location = new System.Drawing.Point(12, 59);
            this._gridOrders.Name = "_gridOrders";
            this._gridOrders.ReadOnly = true;
            this._gridOrders.RowHeadersVisible = false;
            this._gridOrders.RowHeadersWidth = 51;
            this._gridOrders.RowTemplate.Height = 24;
            this._gridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gridOrders.Size = new System.Drawing.Size(1120, 226);
            this._gridOrders.TabIndex = 1;
            this._gridOrders.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._gridOrders_CellMouseClick);
            // 
            // _lblOrdersTitle
            // 
            this._lblOrdersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblOrdersTitle.Location = new System.Drawing.Point(12, 12);
            this._lblOrdersTitle.Name = "_lblOrdersTitle";
            this._lblOrdersTitle.Size = new System.Drawing.Size(1120, 47);
            this._lblOrdersTitle.TabIndex = 0;
            this._lblOrdersTitle.Text = "Today\'s Orders";
            // 
            // _topProductsPanel
            // 
            this._topProductsPanel.BackColor = System.Drawing.Color.White;
            this._topProductsPanel.Controls.Add(this._gridTopProducts);
            this._topProductsPanel.Controls.Add(this._lblTopProductsTitle);
            this._topProductsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topProductsPanel.Location = new System.Drawing.Point(0, 0);
            this._topProductsPanel.Name = "_topProductsPanel";
            this._topProductsPanel.Padding = new System.Windows.Forms.Padding(12);
            this._topProductsPanel.Size = new System.Drawing.Size(1144, 285);
            this._topProductsPanel.TabIndex = 0;
            this._topProductsPanel.AutoScroll = true;
            // 
            // _gridTopProducts
            // 
            this._gridTopProducts.AllowUserToAddRows = false;
            this._gridTopProducts.AllowUserToDeleteRows = false;
            this._gridTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridTopProducts.Location = new System.Drawing.Point(12, 46);
            this._gridTopProducts.Name = "_gridTopProducts";
            this._gridTopProducts.ReadOnly = true;
            this._gridTopProducts.RowHeadersVisible = false;
            this._gridTopProducts.RowHeadersWidth = 51;
            this._gridTopProducts.RowTemplate.Height = 24;
            this._gridTopProducts.Size = new System.Drawing.Size(1120, 227);
            this._gridTopProducts.TabIndex = 1;
            // 
            // _lblTopProductsTitle
            // 
            this._lblTopProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblTopProductsTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._lblTopProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTopProductsTitle.Location = new System.Drawing.Point(12, 12);
            this._lblTopProductsTitle.Name = "_lblTopProductsTitle";
            this._lblTopProductsTitle.Size = new System.Drawing.Size(1120, 34);
            this._lblTopProductsTitle.TabIndex = 0;
            this._lblTopProductsTitle.Text = "Top-Selling Products";
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._btnClose);
            this._buttonsPanel.Controls.Add(this._btnExportCsv);
            this._buttonsPanel.Controls.Add(this._btnExportHtml);
            this._buttonsPanel.Controls.Add(this._btnRefresh);
            this._buttonsPanel.Controls.Add(this._btnShiftHistory);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 691);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(1144, 46);
            this._buttonsPanel.TabIndex = 2;
            // 
            // _btnClose
            // 
            this._btnClose.Location = new System.Drawing.Point(925, 3);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(100, 34);
            this._btnClose.TabIndex = 0;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // _btnExportCsv
            // 
            this._btnExportCsv.Location = new System.Drawing.Point(803, 3);
            this._btnExportCsv.Name = "_btnExportCsv";
            this._btnExportCsv.Size = new System.Drawing.Size(116, 34);
            this._btnExportCsv.TabIndex = 1;
            this._btnExportCsv.Text = "Export CSV";
            this._btnExportCsv.UseVisualStyleBackColor = true;
            this._btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // _btnExportHtml
            // 
            this._btnExportHtml.Location = new System.Drawing.Point(681, 3);
            this._btnExportHtml.Name = "_btnExportHtml";
            this._btnExportHtml.Size = new System.Drawing.Size(116, 34);
            this._btnExportHtml.TabIndex = 2;
            this._btnExportHtml.Text = "Export HTML";
            this._btnExportHtml.UseVisualStyleBackColor = true;
            this._btnExportHtml.Click += new System.EventHandler(this.btnExportHtml_Click);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Location = new System.Drawing.Point(697, 3);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(100, 34);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // _btnShiftHistory
            // 
            this._btnShiftHistory.Location = new System.Drawing.Point(559, 3);
            this._btnShiftHistory.Name = "_btnShiftHistory";
            this._btnShiftHistory.Size = new System.Drawing.Size(132, 34);
            this._btnShiftHistory.TabIndex = 4;
            this._btnShiftHistory.Text = "Shift History";
            this._btnShiftHistory.UseVisualStyleBackColor = true;
            this._btnShiftHistory.Click += new System.EventHandler(this.btnShiftHistory_Click);
            // 
            // frmDailyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this._sidebar);
            this.Controls.Add(this._contentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1200, 650);
            this.Name = "frmDailyReport";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Text = "End-of-Day Report";
            this.Load += new System.EventHandler(this.frmDailyReport_Load);
            this._contentPanel.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._rootLayout.ResumeLayout(false);
            this._summaryPanel.ResumeLayout(false);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._ordersPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._gridOrders)).EndInit();
            this._topProductsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._gridTopProducts)).EndInit();
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private InventoryManagementSystem.ucSidebarNav _sidebar;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _lblHeaderTitle;
        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private System.Windows.Forms.TableLayoutPanel _summaryPanel;
        private System.Windows.Forms.Label _lblOrders;
        private System.Windows.Forms.Label _lblSubtotal;
        private System.Windows.Forms.Label _lblTax;
        private System.Windows.Forms.Label _lblRevenue;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.Panel _ordersPanel;
        private System.Windows.Forms.DataGridView _gridOrders;
        private System.Windows.Forms.Label _lblOrdersTitle;
        private System.Windows.Forms.Panel _topProductsPanel;
        private System.Windows.Forms.DataGridView _gridTopProducts;
        private System.Windows.Forms.Label _lblTopProductsTitle;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Button _btnExportCsv;
        private System.Windows.Forms.Button _btnExportHtml;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Button _btnShiftHistory;
        private System.Windows.Forms.ContextMenuStrip _contextMenuOrders;
        private System.Windows.Forms.ToolStripMenuItem _menuViewDetails;
        private System.Windows.Forms.ToolStripMenuItem _menuPrintReceipt;
        private System.Windows.Forms.ToolStripMenuItem _menuRefund;
        private System.Windows.Forms.ToolStripMenuItem _menuVoid;

    }
}
