namespace InventoryManagementSystem
{
    partial class frmCustomerReceiptHistory
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gridOrders = new System.Windows.Forms.DataGridView();
            this.colOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.panelCustomerInfo = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).BeginInit();
            this.panelCustomerInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phone Number:";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhoneNumber.Location = new System.Drawing.Point(150, 17);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(200, 22);
            this.txtPhoneNumber.TabIndex = 1;
            this.txtPhoneNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPhoneNumber_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(370, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 34);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gridOrders
            // 
            this.gridOrders.AllowUserToAddRows = false;
            this.gridOrders.AllowUserToDeleteRows = false;
            this.gridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrderID,
            this.colOrderDate,
            this.colTotalAmount,
            this.colPaymentMethod});
            this.gridOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrders.Location = new System.Drawing.Point(0, 0);
            this.gridOrders.Name = "gridOrders";
            this.gridOrders.ReadOnly = true;
            this.gridOrders.RowHeadersVisible = false;
            this.gridOrders.RowHeadersWidth = 51;
            this.gridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrders.Size = new System.Drawing.Size(900, 614);
            this.gridOrders.TabIndex = 3;
            this.gridOrders.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOrders_CellDoubleClick);
            // 
            // colOrderID
            // 
            this.colOrderID.DataPropertyName = "OrderID";
            this.colOrderID.HeaderText = "Order ID";
            this.colOrderID.MinimumWidth = 6;
            this.colOrderID.Name = "colOrderID";
            this.colOrderID.ReadOnly = true;
            this.colOrderID.Width = 125;
            // 
            // colOrderDate
            // 
            this.colOrderDate.DataPropertyName = "OrderDate";
            this.colOrderDate.HeaderText = "Date";
            this.colOrderDate.MinimumWidth = 6;
            this.colOrderDate.Name = "colOrderDate";
            this.colOrderDate.ReadOnly = true;
            this.colOrderDate.Width = 125;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.DataPropertyName = "TotalAmount";
            this.colTotalAmount.HeaderText = "Total";
            this.colTotalAmount.MinimumWidth = 6;
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.ReadOnly = true;
            this.colTotalAmount.Width = 125;
            // 
            // colPaymentMethod
            // 
            this.colPaymentMethod.DataPropertyName = "PaymentMethod";
            this.colPaymentMethod.HeaderText = "Payment";
            this.colPaymentMethod.MinimumWidth = 6;
            this.colPaymentMethod.Name = "colPaymentMethod";
            this.colPaymentMethod.ReadOnly = true;
            this.colPaymentMethod.Width = 125;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(661, 523);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 34);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(767, 523);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblInstructions.Location = new System.Drawing.Point(20, 410);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(0, 20);
            this.lblInstructions.TabIndex = 7;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.Location = new System.Drawing.Point(20, 65);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(0, 28);
            this.lblCustomerName.TabIndex = 4;
            // 
            // panelCustomerInfo
            // 
            this.panelCustomerInfo.Controls.Add(this.lblCustomerName);
            this.panelCustomerInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCustomerInfo.Location = new System.Drawing.Point(0, 50);
            this.panelCustomerInfo.Name = "panelCustomerInfo";
            this.panelCustomerInfo.Padding = new System.Windows.Forms.Padding(20, 0, 20, 5);
            this.panelCustomerInfo.Size = new System.Drawing.Size(900, 60);
            this.panelCustomerInfo.TabIndex = 8;
            // 
            // frmCustomerReceiptHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(900, 614);
            this.Controls.Add(this.panelCustomerInfo);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.gridOrders);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomerReceiptHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customer Receipt History";
            this.Load += new System.EventHandler(this.frmCustomerReceiptHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.panelCustomerInfo.ResumeLayout(false);
            this.panelCustomerInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMethod;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Panel panelCustomerInfo;

        public int? SelectedOrderID { get; private set; }
    }
}
