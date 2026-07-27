namespace InventoryManagementSystem
{
    partial class frmReceiptSearch
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
            this._mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._btnClose = new System.Windows.Forms.Button();
            this._searchPanel = new System.Windows.Forms.Panel();
            this._lblOrderID = new System.Windows.Forms.Label();
            this._txtOrderID = new System.Windows.Forms.TextBox();
            this._btnSearch = new System.Windows.Forms.Button();
            this._btnViewByPhone = new System.Windows.Forms.Button();
            this._panelOrderDetails = new System.Windows.Forms.Panel();
            this._lblOrderInfo = new System.Windows.Forms.Label();
            this._gridOrderItems = new System.Windows.Forms.DataGridView();
            this._panelCustomerInfo = new System.Windows.Forms.Panel();
            this._lblCustomerName = new System.Windows.Forms.Label();
            this._lblCustomerPhone = new System.Windows.Forms.Label();
            this._lblPaymentInfo = new System.Windows.Forms.Label();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btnExchange = new System.Windows.Forms.Button();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._mainLayoutPanel.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._searchPanel.SuspendLayout();
            this._panelOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gridOrderItems)).BeginInit();
            this._panelCustomerInfo.SuspendLayout();
            this._actionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainLayoutPanel
            // 
            this._mainLayoutPanel.ColumnCount = 1;
            this._mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Controls.Add(this._headerPanel, 0, 0);
            this._mainLayoutPanel.Controls.Add(this._searchPanel, 0, 1);
            this._mainLayoutPanel.Controls.Add(this._panelOrderDetails, 0, 2);
            this._mainLayoutPanel.Controls.Add(this._actionsPanel, 0, 3);
            this._mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainLayoutPanel.Name = "_mainLayoutPanel";
            this._mainLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this._mainLayoutPanel.RowCount = 4;
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainLayoutPanel.Size = new System.Drawing.Size(800, 600);
            this._mainLayoutPanel.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._lblTitle);
            this._headerPanel.Controls.Add(this._btnClose);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(23, 23);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(754, 60);
            this._headerPanel.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this._lblTitle.Location = new System.Drawing.Point(0, 15);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(145, 31);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Receipt Search";
            // 
            // _btnClose
            // 
            this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnClose.Location = new System.Drawing.Point(668, 13);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(86, 34);
            this._btnClose.TabIndex = 1;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // _searchPanel
            // 
            this._searchPanel.Controls.Add(this._lblOrderID);
            this._searchPanel.Controls.Add(this._txtOrderID);
            this._searchPanel.Controls.Add(this._btnSearch);
            this._searchPanel.Controls.Add(this._btnViewByPhone);
            this._searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchPanel.Location = new System.Drawing.Point(23, 83);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.Size = new System.Drawing.Size(754, 60);
            this._searchPanel.TabIndex = 1;
            // 
            // _lblOrderID
            // 
            this._lblOrderID.AutoSize = true;
            this._lblOrderID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblOrderID.Location = new System.Drawing.Point(0, 15);
            this._lblOrderID.Name = "_lblOrderID";
            this._lblOrderID.Size = new System.Drawing.Size(75, 28);
            this._lblOrderID.TabIndex = 0;
            this._lblOrderID.Text = "Order ID:";
            // 
            // _txtOrderID
            // 
            this._txtOrderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtOrderID.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._txtOrderID.Location = new System.Drawing.Point(81, 13);
            this._txtOrderID.Name = "_txtOrderID";
            this._txtOrderID.Size = new System.Drawing.Size(150, 34);
            this._txtOrderID.TabIndex = 1;
            this._txtOrderID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderID_KeyDown);
            // 
            // _btnSearch
            // 
            this._btnSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnSearch.Location = new System.Drawing.Point(237, 13);
            this._btnSearch.Name = "_btnSearch";
            this._btnSearch.Size = new System.Drawing.Size(100, 34);
            this._btnSearch.TabIndex = 2;
            this._btnSearch.Text = "Search";
            this._btnSearch.UseVisualStyleBackColor = true;
            this._btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // _btnViewByPhone
            // 
            this._btnViewByPhone.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnViewByPhone.Location = new System.Drawing.Point(343, 13);
            this._btnViewByPhone.Name = "_btnViewByPhone";
            this._btnViewByPhone.Size = new System.Drawing.Size(140, 34);
            this._btnViewByPhone.TabIndex = 3;
            this._btnViewByPhone.Text = "View by Phone";
            this._btnViewByPhone.UseVisualStyleBackColor = true;
            this._btnViewByPhone.Click += new System.EventHandler(this.btnViewByPhone_Click);
            // 
            // _panelOrderDetails
            // 
            this._panelOrderDetails.Controls.Add(this._lblOrderInfo);
            this._panelOrderDetails.Controls.Add(this._gridOrderItems);
            this._panelOrderDetails.Controls.Add(this._panelCustomerInfo);
            this._panelOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelOrderDetails.Location = new System.Drawing.Point(23, 143);
            this._panelOrderDetails.Name = "_panelOrderDetails";
            this._panelOrderDetails.Padding = new System.Windows.Forms.Padding(20);
            this._panelOrderDetails.Size = new System.Drawing.Size(754, 374);
            this._panelOrderDetails.TabIndex = 2;
            // 
            // _lblOrderInfo
            // 
            this._lblOrderInfo.AutoSize = true;
            this._lblOrderInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblOrderInfo.Location = new System.Drawing.Point(20, 10);
            this._lblOrderInfo.Name = "_lblOrderInfo";
            this._lblOrderInfo.Size = new System.Drawing.Size(0, 28);
            this._lblOrderInfo.TabIndex = 0;
            // 
            // _gridOrderItems
            // 
            this._gridOrderItems.AllowUserToAddRows = false;
            this._gridOrderItems.AllowUserToDeleteRows = false;
            this._gridOrderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridOrderItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductName,
            this.colQuantity,
            this.colUnitPrice,
            this.colSubtotal});
            this._gridOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridOrderItems.Location = new System.Drawing.Point(20, 50);
            this._gridOrderItems.Name = "gridOrderItems";
            this._gridOrderItems.ReadOnly = true;
            this._gridOrderItems.RowHeadersVisible = false;
            this._gridOrderItems.Size = new System.Drawing.Size(714, 304);
            this._gridOrderItems.TabIndex = 1;
            // 
            // colProductName
            // 
            this.colProductName.DataPropertyName = "ProductName";
            this.colProductName.HeaderText = "Product";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.HeaderText = "Qty";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.DataPropertyName = "UnitPrice";
            this.colUnitPrice.HeaderText = "Price";
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.ReadOnly = true;
            // 
            // colSubtotal
            // 
            this.colSubtotal.DataPropertyName = "Subtotal";
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            // 
            // _panelCustomerInfo
            // 
            this._panelCustomerInfo.Controls.Add(this._lblCustomerName);
            this._panelCustomerInfo.Controls.Add(this._lblCustomerPhone);
            this._panelCustomerInfo.Controls.Add(this._lblPaymentInfo);
            this._panelCustomerInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panelCustomerInfo.Location = new System.Drawing.Point(0, 294);
            this._panelCustomerInfo.Name = "_panelCustomerInfo";
            this._panelCustomerInfo.Padding = new System.Windows.Forms.Padding(20);
            this._panelCustomerInfo.Size = new System.Drawing.Size(754, 80);
            this._panelCustomerInfo.TabIndex = 2;
            // 
            // _lblCustomerName
            // 
            this._lblCustomerName.AutoSize = true;
            this._lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblCustomerName.Location = new System.Drawing.Point(20, 10);
            this._lblCustomerName.Name = "_lblCustomerName";
            this._lblCustomerName.Size = new System.Drawing.Size(0, 24);
            this._lblCustomerName.TabIndex = 0;
            // 
            // _lblCustomerPhone
            // 
            this._lblCustomerPhone.AutoSize = true;
            this._lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this._lblCustomerPhone.Location = new System.Drawing.Point(20, 35);
            this._lblCustomerPhone.Name = "_lblCustomerPhone";
            this._lblCustomerPhone.Size = new System.Drawing.Size(0, 22);
            this._lblCustomerPhone.TabIndex = 1;
            // 
            // _lblPaymentInfo
            // 
            this._lblPaymentInfo.AutoSize = true;
            this._lblPaymentInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this._lblPaymentInfo.Location = new System.Drawing.Point(20, 55);
            this._lblPaymentInfo.Name = "_lblPaymentInfo";
            this._lblPaymentInfo.Size = new System.Drawing.Size(0, 22);
            this._lblPaymentInfo.TabIndex = 2;
            // 
            // _actionsPanel
            // 
            this._actionsPanel.Controls.Add(this._btnExchange);
            this._actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionsPanel.Location = new System.Drawing.Point(23, 517);
            this._actionsPanel.Name = "_actionsPanel";
            this._actionsPanel.Size = new System.Drawing.Size(754, 60);
            this._actionsPanel.TabIndex = 3;
            // 
            // _btnExchange
            // 
            this._btnExchange.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnExchange.Location = new System.Drawing.Point(654, 13);
            this._btnExchange.Name = "_btnExchange";
            this._btnExchange.Size = new System.Drawing.Size(100, 34);
            this._btnExchange.TabIndex = 0;
            this._btnExchange.Text = "Exchange";
            this._btnExchange.UseVisualStyleBackColor = true;
            this._btnExchange.Click += new System.EventHandler(this.btnExchange_Click);
            // 
            // frmReceiptSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this._mainLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmReceiptSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Receipt";
            this.Load += new System.EventHandler(this.frmReceiptSearch_Load);
            this._mainLayoutPanel.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            this._panelOrderDetails.ResumeLayout(false);
            this._panelOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gridOrderItems)).EndInit();
            this._panelCustomerInfo.ResumeLayout(false);
            this._panelCustomerInfo.PerformLayout();
            this._actionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Panel _searchPanel;
        private System.Windows.Forms.Label _lblOrderID;
        private System.Windows.Forms.TextBox _txtOrderID;
        private System.Windows.Forms.Button _btnSearch;
        private System.Windows.Forms.Button _btnViewByPhone;
        private System.Windows.Forms.Panel _panelOrderDetails;
        private System.Windows.Forms.Label _lblOrderInfo;
        private System.Windows.Forms.DataGridView _gridOrderItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.Panel _panelCustomerInfo;
        private System.Windows.Forms.Label _lblCustomerName;
        private System.Windows.Forms.Label _lblCustomerPhone;
        private System.Windows.Forms.Label _lblPaymentInfo;
        private System.Windows.Forms.FlowLayoutPanel _actionsPanel;
        private System.Windows.Forms.Button _btnExchange;
    }
}
