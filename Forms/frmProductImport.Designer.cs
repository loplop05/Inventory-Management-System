namespace InventoryManagementSystem
{
    partial class frmProductImport
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnDownloadTemplate = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this._contentPanel.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this.panelButtons);
            this._contentPanel.Controls.Add(this.panelInfo);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(600, 300);
            this._contentPanel.TabIndex = 0;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Controls.Add(this.btnImport);
            this.panelButtons.Controls.Add(this.btnDownloadTemplate);
            this.panelButtons.Controls.Add(this.btnBrowse);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 230);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(600, 70);
            this.panelButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(510, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(420, 20);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(80, 35);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnDownloadTemplate
            // 
            this.btnDownloadTemplate.Location = new System.Drawing.Point(10, 20);
            this.btnDownloadTemplate.Name = "btnDownloadTemplate";
            this.btnDownloadTemplate.Size = new System.Drawing.Size(140, 35);
            this.btnDownloadTemplate.TabIndex = 1;
            this.btnDownloadTemplate.Text = "Download Template";
            this.btnDownloadTemplate.UseVisualStyleBackColor = true;
            this.btnDownloadTemplate.Click += new System.EventHandler(this.btnDownloadTemplate_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(160, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(100, 35);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.lblInfo);
            this.panelInfo.Controls.Add(this.txtFilePath);
            this.panelInfo.Controls.Add(this.lblFilePath);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(600, 230);
            this.panelInfo.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(10, 80);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(580, 140);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "CSV Format:\r\nProductName,CategoryID,SupplierID,Price,Quantity,Description,Barcode\r\n\r\nNotes:\r\n- First row is header (will be skipped)\r\n- CategoryID and SupplierID must exist in database\r\n- Price and Quantity must be valid numbers\r\n- Duplicate product names will be skipped";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(120, 30);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(470, 27);
            this.txtFilePath.TabIndex = 1;
            this.txtFilePath.ReadOnly = true;
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(10, 33);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(67, 19);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "CSV File:";
            // 
            // frmProductImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this._contentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProductImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk Product Import";
            this.Load += new System.EventHandler(this.frmProductImport_Load);
            this._contentPanel.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnDownloadTemplate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblFilePath;
    }
}
