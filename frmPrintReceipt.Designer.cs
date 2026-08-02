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
            this._mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._searchPanel = new System.Windows.Forms.Panel();
            this._lblOrderID = new System.Windows.Forms.Label();
            this._txtOrderID = new System.Windows.Forms.TextBox();
            this._btnSearch = new System.Windows.Forms.Button();
            this._panelReceipt = new System.Windows.Forms.Panel();
            this._lblReceiptPreview = new System.Windows.Forms.Label();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._btnPrint = new System.Windows.Forms.Button();
            this._btnVoid = new System.Windows.Forms.Button();
            this._mainLayoutPanel.SuspendLayout();
            this._searchPanel.SuspendLayout();
            this._panelReceipt.SuspendLayout();
            this._actionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainLayoutPanel
            // 
            this._mainLayoutPanel.ColumnCount = 1;
            this._mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Controls.Add(this._searchPanel, 0, 0);
            this._mainLayoutPanel.Controls.Add(this._panelReceipt, 0, 1);
            this._mainLayoutPanel.Controls.Add(this._actionsPanel, 0, 2);
            this._mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainLayoutPanel.Name = "_mainLayoutPanel";
            this._mainLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this._mainLayoutPanel.RowCount = 3;
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainLayoutPanel.Size = new System.Drawing.Size(500, 460);
            this._mainLayoutPanel.TabIndex = 0;
            // 
            // _searchPanel
            // 
            this._searchPanel.Controls.Add(this._lblOrderID);
            this._searchPanel.Controls.Add(this._txtOrderID);
            this._searchPanel.Controls.Add(this._btnSearch);
            this._searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchPanel.Location = new System.Drawing.Point(23, 83);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.Size = new System.Drawing.Size(454, 54);
            this._searchPanel.TabIndex = 1;
            // 
            // _lblOrderID
            // 
            this._lblOrderID.AutoSize = true;
            this._lblOrderID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblOrderID.Location = new System.Drawing.Point(0, 15);
            this._lblOrderID.Name = "_lblOrderID";
            this._lblOrderID.Size = new System.Drawing.Size(98, 28);
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
            // _panelReceipt
            // 
            this._panelReceipt.AutoScroll = true;
            this._panelReceipt.BackColor = System.Drawing.Color.White;
            this._panelReceipt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelReceipt.Controls.Add(this._lblReceiptPreview);
            this._panelReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelReceipt.Location = new System.Drawing.Point(23, 143);
            this._panelReceipt.Name = "_panelReceipt";
            this._panelReceipt.Size = new System.Drawing.Size(454, 234);
            this._panelReceipt.TabIndex = 2;
            // 
            // _lblReceiptPreview
            // 
            this._lblReceiptPreview.AutoSize = true;
            this._lblReceiptPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblReceiptPreview.Font = new System.Drawing.Font("Consolas", 9F);
            this._lblReceiptPreview.Location = new System.Drawing.Point(0, 0);
            this._lblReceiptPreview.Name = "_lblReceiptPreview";
            this._lblReceiptPreview.Padding = new System.Windows.Forms.Padding(10);
            this._lblReceiptPreview.Size = new System.Drawing.Size(332, 38);
            this._lblReceiptPreview.TabIndex = 0;
            this._lblReceiptPreview.Text = "Enter an Order ID to view the receipt.";
            // 
            // _actionsPanel
            // 
            this._actionsPanel.Controls.Add(this._btnVoid);
            this._actionsPanel.Controls.Add(this._btnPrint);
            this._actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionsPanel.Location = new System.Drawing.Point(23, 383);
            this._actionsPanel.Name = "_actionsPanel";
            this._actionsPanel.Size = new System.Drawing.Size(454, 54);
            this._actionsPanel.TabIndex = 3;
            // 
            // _btnPrint
            // 
            this._btnPrint.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnPrint.Location = new System.Drawing.Point(0, 0);
            this._btnPrint.Name = "_btnPrint";
            this._btnPrint.Size = new System.Drawing.Size(100, 34);
            this._btnPrint.TabIndex = 0;
            this._btnPrint.Text = "Print";
            this._btnPrint.UseVisualStyleBackColor = true;
            this._btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // _btnVoid
            // 
            this._btnVoid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnVoid.Location = new System.Drawing.Point(110, 0);
            this._btnVoid.Name = "_btnVoid";
            this._btnVoid.Size = new System.Drawing.Size(100, 34);
            this._btnVoid.TabIndex = 1;
            this._btnVoid.Text = "Void";
            this._btnVoid.UseVisualStyleBackColor = true;
            this._btnVoid.Click += new System.EventHandler(this.btnVoid_Click);
            // 
            // frmPrintReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 460);
            this.Controls.Add(this._mainLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Receipt";
            this.Load += new System.EventHandler(this.frmPrintReceipt_Load);
            this._mainLayoutPanel.ResumeLayout(false);
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            this._panelReceipt.ResumeLayout(false);
            this._panelReceipt.PerformLayout();
            this._actionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
        private System.Windows.Forms.Panel _searchPanel;
        private System.Windows.Forms.Label _lblOrderID;
        private System.Windows.Forms.TextBox _txtOrderID;
        private System.Windows.Forms.Button _btnSearch;
        private System.Windows.Forms.Panel _panelReceipt;
        private System.Windows.Forms.Label _lblReceiptPreview;
        private System.Windows.Forms.FlowLayoutPanel _actionsPanel;
        private System.Windows.Forms.Button _btnPrint;
        private System.Windows.Forms.Button _btnVoid;
    }
}
