namespace InventoryManagementSystem
{
    partial class frmPrintReceipt
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
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelReceipt = new System.Windows.Forms.Panel();
            this.lblReceiptContent = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelReceipt.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order ID:";
            // 
            // txtOrderID
            // 
            this.txtOrderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrderID.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtOrderID.Location = new System.Drawing.Point(110, 17);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(150, 29);
            this.txtOrderID.TabIndex = 1;
            this.txtOrderID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderID_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(280, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 34);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelReceipt
            // 
            this.panelReceipt.Controls.Add(this.lblReceiptContent);
            this.panelReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReceipt.Location = new System.Drawing.Point(0, 60);
            this.panelReceipt.Name = "panelReceipt";
            this.panelReceipt.Padding = new System.Windows.Forms.Padding(20);
            this.panelReceipt.Size = new System.Drawing.Size(500, 400);
            this.panelReceipt.TabIndex = 3;
            // 
            // lblReceiptContent
            // 
            this.lblReceiptContent.AutoSize = true;
            this.lblReceiptContent.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblReceiptContent.Location = new System.Drawing.Point(20, 10);
            this.lblReceiptContent.Name = "lblReceiptContent";
            this.lblReceiptContent.Size = new System.Drawing.Size(0, 20);
            this.lblReceiptContent.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(20, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 34);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print Receipt";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Enabled = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(360, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 34);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPrintReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 460);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panelReceipt);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtOrderID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Receipt";
            this.Load += new System.EventHandler(this.frmPrintReceipt_Load);
            this.panelReceipt.ResumeLayout(false);
            this.panelReceipt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelReceipt;
        private System.Windows.Forms.Label lblReceiptContent;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
    }
}
