namespace InventoryManagementSystem
{
    partial class frmExchange
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
            this.panelOriginalOrder = new System.Windows.Forms.Panel();
            this.lblOriginalOrderInfo = new System.Windows.Forms.Label();
            this.gridOriginalItems = new System.Windows.Forms.DataGridView();
            this.colOrigProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelExchange = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbExchangeItem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExchangeQuantity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExchangeReason = new System.Windows.Forms.TextBox();
            this.btnProcessExchange = new System.Windows.Forms.Button();
            this.lblExchangePolicy = new System.Windows.Forms.Label();
            this.panelNewOrder = new System.Windows.Forms.Panel();
            this.lblNewOrderInfo = new System.Windows.Forms.Label();
            this.gridNewItems = new System.Windows.Forms.DataGridView();
            this.colNewProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNewQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNewUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPriceDifference = new System.Windows.Forms.Label();
            this.btnRemoveExchange = new System.Windows.Forms.Button();
            this.btnConfirmExchange = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelOriginalOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOriginalItems)).BeginInit();
            this.panelExchange.SuspendLayout();
            this.panelNewOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNewItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOriginalOrder
            // 
            this.panelOriginalOrder.Controls.Add(this.lblOriginalOrderInfo);
            this.panelOriginalOrder.Controls.Add(this.gridOriginalItems);
            this.panelOriginalOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOriginalOrder.Location = new System.Drawing.Point(0, 60);
            this.panelOriginalOrder.Name = "panelOriginalOrder";
            this.panelOriginalOrder.Padding = new System.Windows.Forms.Padding(20);
            this.panelOriginalOrder.Size = new System.Drawing.Size(900, 200);
            this.panelOriginalOrder.TabIndex = 0;
            this.panelOriginalOrder.AutoScroll = true;
            // 
            // lblOriginalOrderInfo
            // 
            this.lblOriginalOrderInfo.AutoSize = true;
            this.lblOriginalOrderInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOriginalOrderInfo.Location = new System.Drawing.Point(20, 10);
            this.lblOriginalOrderInfo.Name = "lblOriginalOrderInfo";
            this.lblOriginalOrderInfo.Size = new System.Drawing.Size(0, 28);
            this.lblOriginalOrderInfo.TabIndex = 0;
            // 
            // gridOriginalItems
            // 
            this.gridOriginalItems.AllowUserToAddRows = false;
            this.gridOriginalItems.AllowUserToDeleteRows = false;
            this.gridOriginalItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOriginalItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrigProductName,
            this.colOrigQuantity,
            this.colOrigUnitPrice});
            this.gridOriginalItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOriginalItems.Location = new System.Drawing.Point(20, 20);
            this.gridOriginalItems.Name = "gridOriginalItems";
            this.gridOriginalItems.ReadOnly = true;
            this.gridOriginalItems.RowHeadersVisible = false;
            this.gridOriginalItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOriginalItems.Size = new System.Drawing.Size(860, 180);
            this.gridOriginalItems.TabIndex = 1;
            // 
            // colOrigProductName
            // 
            this.colOrigProductName.DataPropertyName = "ProductName";
            this.colOrigProductName.HeaderText = "Product";
            this.colOrigProductName.Name = "colOrigProductName";
            this.colOrigProductName.ReadOnly = true;
            // 
            // colOrigQuantity
            // 
            this.colOrigQuantity.DataPropertyName = "Quantity";
            this.colOrigQuantity.HeaderText = "Qty";
            this.colOrigQuantity.Name = "colOrigQuantity";
            this.colOrigQuantity.ReadOnly = true;
            // 
            // colOrigUnitPrice
            // 
            this.colOrigUnitPrice.DataPropertyName = "UnitPrice";
            this.colOrigUnitPrice.HeaderText = "Price";
            this.colOrigUnitPrice.Name = "colOrigUnitPrice";
            this.colOrigUnitPrice.ReadOnly = true;
            // 
            // panelExchange
            // 
            this.panelExchange.Controls.Add(this.btnConfirmExchange);
            this.panelExchange.Controls.Add(this.btnRemoveExchange);
            this.panelExchange.Controls.Add(this.label2);
            this.panelExchange.Controls.Add(this.cmbExchangeItem);
            this.panelExchange.Controls.Add(this.label3);
            this.panelExchange.Controls.Add(this.txtExchangeQuantity);
            this.panelExchange.Controls.Add(this.label4);
            this.panelExchange.Controls.Add(this.txtExchangeReason);
            this.panelExchange.Controls.Add(this.btnProcessExchange);
            this.panelExchange.Controls.Add(this.lblExchangePolicy);
            this.panelExchange.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExchange.Location = new System.Drawing.Point(0, 260);
            this.panelExchange.Name = "panelExchange";
            this.panelExchange.Padding = new System.Windows.Forms.Padding(20);
            this.panelExchange.Size = new System.Drawing.Size(900, 140);
            this.panelExchange.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Item to Exchange:";
            // 
            // cmbExchangeItem
            // 
            this.cmbExchangeItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExchangeItem.FormattingEnabled = true;
            this.cmbExchangeItem.Location = new System.Drawing.Point(120, 12);
            this.cmbExchangeItem.Name = "cmbExchangeItem";
            this.cmbExchangeItem.Size = new System.Drawing.Size(300, 28);
            this.cmbExchangeItem.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Quantity:";
            // 
            // txtExchangeQuantity
            // 
            this.txtExchangeQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExchangeQuantity.Location = new System.Drawing.Point(120, 47);
            this.txtExchangeQuantity.Name = "txtExchangeQuantity";
            this.txtExchangeQuantity.Size = new System.Drawing.Size(100, 26);
            this.txtExchangeQuantity.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Reason:";
            // 
            // txtExchangeReason
            // 
            this.txtExchangeReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExchangeReason.Location = new System.Drawing.Point(120, 82);
            this.txtExchangeReason.Name = "txtExchangeReason";
            this.txtExchangeReason.Size = new System.Drawing.Size(300, 26);
            this.txtExchangeReason.TabIndex = 5;
            // 
            // btnProcessExchange
            // 
            this.btnProcessExchange.Location = new System.Drawing.Point(440, 47);
            this.btnProcessExchange.Name = "btnProcessExchange";
            this.btnProcessExchange.Size = new System.Drawing.Size(120, 34);
            this.btnProcessExchange.TabIndex = 6;
            this.btnProcessExchange.Text = "Add to Exchange";
            this.btnProcessExchange.UseVisualStyleBackColor = true;
            this.btnProcessExchange.Click += new System.EventHandler(this.btnProcessExchange_Click);
            // 
            // lblExchangePolicy
            // 
            this.lblExchangePolicy.AutoSize = true;
            this.lblExchangePolicy.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblExchangePolicy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.lblExchangePolicy.Location = new System.Drawing.Point(20, 115);
            this.lblExchangePolicy.Name = "lblExchangePolicy";
            this.lblExchangePolicy.Size = new System.Drawing.Size(0, 20);
            this.lblExchangePolicy.TabIndex = 7;
            // 
            // btnRemoveExchange
            // 
            this.btnRemoveExchange.Location = new System.Drawing.Point(570, 47);
            this.btnRemoveExchange.Name = "btnRemoveExchange";
            this.btnRemoveExchange.Size = new System.Drawing.Size(100, 34);
            this.btnRemoveExchange.TabIndex = 8;
            this.btnRemoveExchange.Text = "Remove";
            this.btnRemoveExchange.UseVisualStyleBackColor = true;
            this.btnRemoveExchange.Click += new System.EventHandler(this.btnRemoveExchange_Click);
            // 
            // btnConfirmExchange
            // 
            this.btnConfirmExchange.Location = new System.Drawing.Point(680, 47);
            this.btnConfirmExchange.Name = "btnConfirmExchange";
            this.btnConfirmExchange.Size = new System.Drawing.Size(180, 34);
            this.btnConfirmExchange.TabIndex = 9;
            this.btnConfirmExchange.Text = "Confirm Exchange";
            this.btnConfirmExchange.UseVisualStyleBackColor = true;
            this.btnConfirmExchange.Click += new System.EventHandler(this.btnConfirmExchange_Click);
            // 
            // panelNewOrder
            // 
            this.panelNewOrder.Controls.Add(this.lblPriceDifference);
            this.panelNewOrder.Controls.Add(this.lblNewOrderInfo);
            this.panelNewOrder.Controls.Add(this.gridNewItems);
            this.panelNewOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNewOrder.Location = new System.Drawing.Point(0, 400);
            this.panelNewOrder.Name = "panelNewOrder";
            this.panelNewOrder.Padding = new System.Windows.Forms.Padding(20);
            this.panelNewOrder.Size = new System.Drawing.Size(900, 200);
            this.panelNewOrder.TabIndex = 2;
            // 
            // lblNewOrderInfo
            // 
            this.lblNewOrderInfo.AutoSize = true;
            this.lblNewOrderInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNewOrderInfo.Location = new System.Drawing.Point(20, 10);
            this.lblNewOrderInfo.Name = "lblNewOrderInfo";
            this.lblNewOrderInfo.Size = new System.Drawing.Size(0, 28);
            this.lblNewOrderInfo.TabIndex = 0;
            // 
            // gridNewItems
            // 
            this.gridNewItems.AllowUserToAddRows = false;
            this.gridNewItems.AllowUserToDeleteRows = false;
            this.gridNewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridNewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNewProductName,
            this.colNewQuantity,
            this.colNewUnitPrice});
            this.gridNewItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridNewItems.Location = new System.Drawing.Point(20, 20);
            this.gridNewItems.Name = "gridNewItems";
            this.gridNewItems.ReadOnly = true;
            this.gridNewItems.RowHeadersVisible = false;
            this.gridNewItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridNewItems.Size = new System.Drawing.Size(860, 180);
            this.gridNewItems.TabIndex = 1;
            // 
            // colNewProductName
            // 
            this.colNewProductName.DataPropertyName = "ProductName";
            this.colNewProductName.HeaderText = "Product";
            this.colNewProductName.Name = "colNewProductName";
            this.colNewProductName.ReadOnly = true;
            // 
            // colNewQuantity
            // 
            this.colNewQuantity.DataPropertyName = "Quantity";
            this.colNewQuantity.HeaderText = "Qty";
            this.colNewQuantity.Name = "colNewQuantity";
            this.colNewQuantity.ReadOnly = true;
            // 
            // colNewUnitPrice
            // 
            this.colNewUnitPrice.DataPropertyName = "UnitPrice";
            this.colNewUnitPrice.HeaderText = "Price";
            this.colNewUnitPrice.Name = "colNewUnitPrice";
            this.colNewUnitPrice.ReadOnly = true;
            // 
            // lblPriceDifference
            // 
            this.lblPriceDifference.AutoSize = true;
            this.lblPriceDifference.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPriceDifference.Location = new System.Drawing.Point(20, 10);
            this.lblPriceDifference.Name = "lblPriceDifference";
            this.lblPriceDifference.Size = new System.Drawing.Size(0, 32);
            this.lblPriceDifference.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(780, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 34);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelNewOrder);
            this.Controls.Add(this.panelExchange);
            this.Controls.Add(this.panelOriginalOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExchange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Exchange";
            this.Load += new System.EventHandler(this.frmExchange_Load);
            this.panelOriginalOrder.ResumeLayout(false);
            this.panelOriginalOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOriginalItems)).EndInit();
            this.panelExchange.ResumeLayout(false);
            this.panelExchange.PerformLayout();
            this.panelNewOrder.ResumeLayout(false);
            this.panelNewOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNewItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOriginalOrder;
        private System.Windows.Forms.Label lblOriginalOrderInfo;
        private System.Windows.Forms.DataGridView gridOriginalItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigUnitPrice;
        private System.Windows.Forms.Panel panelExchange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbExchangeItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExchangeQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExchangeReason;
        private System.Windows.Forms.Button btnProcessExchange;
        private System.Windows.Forms.Label lblExchangePolicy;
        private System.Windows.Forms.Panel panelNewOrder;
        private System.Windows.Forms.Label lblNewOrderInfo;
        private System.Windows.Forms.DataGridView gridNewItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNewUnitPrice;
        private System.Windows.Forms.Label lblPriceDifference;
        private System.Windows.Forms.Button btnRemoveExchange;
        private System.Windows.Forms.Button btnConfirmExchange;
        private System.Windows.Forms.Button btnClose;
    }
}
