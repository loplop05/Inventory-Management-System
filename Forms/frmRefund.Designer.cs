namespace InventoryManagementSystem
{
    partial class frmRefund
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
            this.lblOrderInfo = new System.Windows.Forms.Label();
            this.lblOrderTotal = new System.Windows.Forms.Label();
            this.lblRefundType = new System.Windows.Forms.Label();
            this.lblRefundMethod = new System.Windows.Forms.Label();
            this.cboRefundMethod = new System.Windows.Forms.ComboBox();
            this.lblRefundReason = new System.Windows.Forms.Label();
            this.txtRefundReason = new System.Windows.Forms.TextBox();
            this.lblOrderItems = new System.Windows.Forms.Label();
            this.gridOrderItems = new System.Windows.Forms.DataGridView();
            this.lblRefundAmount = new System.Windows.Forms.Label();
            this.btnProcessRefund = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOrderInfo
            // 
            this.lblOrderInfo.AutoSize = true;
            this.lblOrderInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOrderInfo.Location = new System.Drawing.Point(20, 20);
            this.lblOrderInfo.Name = "lblOrderInfo";
            this.lblOrderInfo.Size = new System.Drawing.Size(80, 19);
            this.lblOrderInfo.TabIndex = 0;
            this.lblOrderInfo.Text = "Order Info";
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.AutoSize = true;
            this.lblOrderTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblOrderTotal.Location = new System.Drawing.Point(20, 45);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new System.Drawing.Size(80, 21);
            this.lblOrderTotal.TabIndex = 1;
            this.lblOrderTotal.Text = "$0.00";
            // 
            // lblRefundType
            // 
            this.lblRefundType.AutoSize = true;
            this.lblRefundType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRefundType.Location = new System.Drawing.Point(20, 75);
            this.lblRefundType.Name = "lblRefundType";
            this.lblRefundType.Size = new System.Drawing.Size(71, 17);
            this.lblRefundType.TabIndex = 2;
            this.lblRefundType.Text = "Refund Type";
            // 
            // lblRefundMethod
            // 
            this.lblRefundMethod.AutoSize = true;
            this.lblRefundMethod.Location = new System.Drawing.Point(20, 105);
            this.lblRefundMethod.Name = "lblRefundMethod";
            this.lblRefundMethod.Size = new System.Drawing.Size(85, 17);
            this.lblRefundMethod.TabIndex = 3;
            this.lblRefundMethod.Text = "Refund Method";
            // 
            // cboRefundMethod
            // 
            this.cboRefundMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRefundMethod.FormattingEnabled = true;
            this.cboRefundMethod.Location = new System.Drawing.Point(120, 102);
            this.cboRefundMethod.Name = "cboRefundMethod";
            this.cboRefundMethod.Size = new System.Drawing.Size(200, 25);
            this.cboRefundMethod.TabIndex = 4;
            // 
            // lblRefundReason
            // 
            this.lblRefundReason.AutoSize = true;
            this.lblRefundReason.Location = new System.Drawing.Point(20, 140);
            this.lblRefundReason.Name = "lblRefundReason";
            this.lblRefundReason.Size = new System.Drawing.Size(56, 17);
            this.lblRefundReason.TabIndex = 5;
            this.lblRefundReason.Text = "Reason";
            // 
            // txtRefundReason
            // 
            this.txtRefundReason.Location = new System.Drawing.Point(120, 137);
            this.txtRefundReason.Multiline = true;
            this.txtRefundReason.Name = "txtRefundReason";
            this.txtRefundReason.Size = new System.Drawing.Size(400, 60);
            this.txtRefundReason.TabIndex = 6;
            // 
            // lblOrderItems
            // 
            this.lblOrderItems.AutoSize = true;
            this.lblOrderItems.Location = new System.Drawing.Point(20, 215);
            this.lblOrderItems.Name = "lblOrderItems";
            this.lblOrderItems.Size = new System.Drawing.Size(76, 17);
            this.lblOrderItems.TabIndex = 7;
            this.lblOrderItems.Text = "Order Items";
            // 
            // gridOrderItems
            // 
            this.gridOrderItems.AllowUserToAddRows = false;
            this.gridOrderItems.AllowUserToDeleteRows = false;
            this.gridOrderItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridOrderItems.Location = new System.Drawing.Point(20, 240);
            this.gridOrderItems.Name = "gridOrderItems";
            this.gridOrderItems.ReadOnly = true;
            this.gridOrderItems.RowHeadersVisible = false;
            this.gridOrderItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrderItems.Size = new System.Drawing.Size(500, 200);
            this.gridOrderItems.TabIndex = 8;
            this.gridOrderItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOrderItems_CellValueChanged);
            this.gridOrderItems.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridOrderItems_CurrentCellDirtyStateChanged);
            // 
            // lblRefundAmount
            // 
            this.lblRefundAmount.AutoSize = true;
            this.lblRefundAmount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefundAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblRefundAmount.Location = new System.Drawing.Point(20, 455);
            this.lblRefundAmount.Name = "lblRefundAmount";
            this.lblRefundAmount.Size = new System.Drawing.Size(80, 24);
            this.lblRefundAmount.TabIndex = 9;
            this.lblRefundAmount.Text = "$0.00";
            // 
            // btnProcessRefund
            // 
            this.btnProcessRefund.Location = new System.Drawing.Point(345, 490);
            this.btnProcessRefund.Name = "btnProcessRefund";
            this.btnProcessRefund.Size = new System.Drawing.Size(120, 35);
            this.btnProcessRefund.TabIndex = 10;
            this.btnProcessRefund.Text = "Process Refund";
            this.btnProcessRefund.UseVisualStyleBackColor = true;
            this.btnProcessRefund.Click += new System.EventHandler(this.btnProcessRefund_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(20, 490);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmRefund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 540);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnProcessRefund);
            this.Controls.Add(this.lblRefundAmount);
            this.Controls.Add(this.gridOrderItems);
            this.Controls.Add(this.lblOrderItems);
            this.Controls.Add(this.txtRefundReason);
            this.Controls.Add(this.lblRefundReason);
            this.Controls.Add(this.cboRefundMethod);
            this.Controls.Add(this.lblRefundMethod);
            this.Controls.Add(this.lblRefundType);
            this.Controls.Add(this.lblOrderTotal);
            this.Controls.Add(this.lblOrderInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRefund";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Process Refund";
            this.Load += new System.EventHandler(this.frmRefund_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOrderInfo;
        private System.Windows.Forms.Label lblOrderTotal;
        private System.Windows.Forms.Label lblRefundType;
        private System.Windows.Forms.Label lblRefundMethod;
        private System.Windows.Forms.ComboBox cboRefundMethod;
        private System.Windows.Forms.Label lblRefundReason;
        private System.Windows.Forms.TextBox txtRefundReason;
        private System.Windows.Forms.Label lblOrderItems;
        private System.Windows.Forms.DataGridView gridOrderItems;
        private System.Windows.Forms.Label lblRefundAmount;
        private System.Windows.Forms.Button btnProcessRefund;
        private System.Windows.Forms.Button btnClose;
    }
}
