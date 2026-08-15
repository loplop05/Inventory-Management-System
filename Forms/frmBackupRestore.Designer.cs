namespace InventoryManagementSystem
{
    partial class frmBackupRestore
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
            this.panelBackup = new System.Windows.Forms.Panel();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.lblBackupInfo = new System.Windows.Forms.Label();
            this.panelRestore = new System.Windows.Forms.Panel();
            this.btnBrowseRestore = new System.Windows.Forms.Button();
            this.txtRestorePath = new System.Windows.Forms.TextBox();
            this.lblRestorePath = new System.Windows.Forms.Label();
            this.panelBackupPath = new System.Windows.Forms.Panel();
            this.btnBrowseBackup = new System.Windows.Forms.Button();
            this.txtBackupPath = new System.Windows.Forms.TextBox();
            this.lblBackupPath = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this._contentPanel.SuspendLayout();
            this.panelBackup.SuspendLayout();
            this.panelRestore.SuspendLayout();
            this.panelBackupPath.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this.panelBackup);
            this._contentPanel.Controls.Add(this.panelRestore);
            this._contentPanel.Controls.Add(this.panelBackupPath);
            this._contentPanel.Controls.Add(this.panelButtons);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(600, 350);
            this._contentPanel.TabIndex = 0;
            // 
            // panelBackup
            // 
            this.panelBackup.Controls.Add(this.btnRestore);
            this.panelBackup.Controls.Add(this.btnBackup);
            this.panelBackup.Controls.Add(this.lblBackupInfo);
            this.panelBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBackup.Location = new System.Drawing.Point(0, 100);
            this.panelBackup.Name = "panelBackup";
            this.panelBackup.Size = new System.Drawing.Size(600, 60);
            this.panelBackup.TabIndex = 2;
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Location = new System.Drawing.Point(510, 15);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(80, 35);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(10, 15);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(100, 35);
            this.btnBackup.TabIndex = 1;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // lblBackupInfo
            // 
            this.lblBackupInfo.AutoSize = true;
            this.lblBackupInfo.Location = new System.Drawing.Point(120, 23);
            this.lblBackupInfo.Name = "lblBackupInfo";
            this.lblBackupInfo.Size = new System.Drawing.Size(279, 19);
            this.lblBackupInfo.TabIndex = 0;
            this.lblBackupInfo.Text = "Backup or restore the entire database";
            // 
            // panelRestore
            // 
            this.panelRestore.Controls.Add(this.btnBrowseRestore);
            this.panelRestore.Controls.Add(this.txtRestorePath);
            this.panelRestore.Controls.Add(this.lblRestorePath);
            this.panelRestore.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRestore.Location = new System.Drawing.Point(0, 160);
            this.panelRestore.Name = "panelRestore";
            this.panelRestore.Size = new System.Drawing.Size(600, 60);
            this.panelRestore.TabIndex = 3;
            // 
            // btnBrowseRestore
            // 
            this.btnBrowseRestore.Location = new System.Drawing.Point(510, 15);
            this.btnBrowseRestore.Name = "btnBrowseRestore";
            this.btnBrowseRestore.Size = new System.Drawing.Size(80, 30);
            this.btnBrowseRestore.TabIndex = 2;
            this.btnBrowseRestore.Text = "Browse...";
            this.btnBrowseRestore.UseVisualStyleBackColor = true;
            this.btnBrowseRestore.Click += new System.EventHandler(this.btnBrowseRestore_Click);
            // 
            // txtRestorePath
            // 
            this.txtRestorePath.Location = new System.Drawing.Point(120, 17);
            this.txtRestorePath.Name = "txtRestorePath";
            this.txtRestorePath.Size = new System.Drawing.Size(380, 27);
            this.txtRestorePath.TabIndex = 1;
            this.txtRestorePath.ReadOnly = true;
            // 
            // lblRestorePath
            // 
            this.lblRestorePath.AutoSize = true;
            this.lblRestorePath.Location = new System.Drawing.Point(10, 20);
            this.lblRestorePath.Name = "lblRestorePath";
            this.lblRestorePath.Size = new System.Drawing.Size(92, 19);
            this.lblRestorePath.TabIndex = 0;
            this.lblRestorePath.Text = "Restore File:";
            // 
            // panelBackupPath
            // 
            this.panelBackupPath.Controls.Add(this.btnBrowseBackup);
            this.panelBackupPath.Controls.Add(this.txtBackupPath);
            this.panelBackupPath.Controls.Add(this.lblBackupPath);
            this.panelBackupPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBackupPath.Location = new System.Drawing.Point(0, 0);
            this.panelBackupPath.Name = "panelBackupPath";
            this.panelBackupPath.Size = new System.Drawing.Size(600, 100);
            this.panelBackupPath.TabIndex = 1;
            // 
            // btnBrowseBackup
            // 
            this.btnBrowseBackup.Location = new System.Drawing.Point(510, 60);
            this.btnBrowseBackup.Name = "btnBrowseBackup";
            this.btnBrowseBackup.Size = new System.Drawing.Size(80, 30);
            this.btnBrowseBackup.TabIndex = 2;
            this.btnBrowseBackup.Text = "Browse...";
            this.btnBrowseBackup.UseVisualStyleBackColor = true;
            this.btnBrowseBackup.Click += new System.EventHandler(this.btnBrowseBackup_Click);
            // 
            // txtBackupPath
            // 
            this.txtBackupPath.Location = new System.Drawing.Point(120, 62);
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.Size = new System.Drawing.Size(380, 27);
            this.txtBackupPath.TabIndex = 1;
            this.txtBackupPath.ReadOnly = true;
            // 
            // lblBackupPath
            // 
            this.lblBackupPath.AutoSize = true;
            this.lblBackupPath.Location = new System.Drawing.Point(10, 65);
            this.lblBackupPath.Name = "lblBackupPath";
            this.lblBackupPath.Size = new System.Drawing.Size(82, 19);
            this.lblBackupPath.TabIndex = 0;
            this.lblBackupPath.Text = "Backup To:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 310);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(600, 40);
            this.panelButtons.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(510, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmBackupRestore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 350);
            this.Controls.Add(this._contentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackupRestore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Backup & Restore";
            this.Load += new System.EventHandler(this.frmBackupRestore_Load);
            this._contentPanel.ResumeLayout(false);
            this.panelBackup.ResumeLayout(false);
            this.panelBackup.PerformLayout();
            this.panelRestore.ResumeLayout(false);
            this.panelRestore.PerformLayout();
            this.panelBackupPath.ResumeLayout(false);
            this.panelBackupPath.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel panelBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblBackupInfo;
        private System.Windows.Forms.Panel panelRestore;
        private System.Windows.Forms.Button btnBrowseRestore;
        private System.Windows.Forms.TextBox txtRestorePath;
        private System.Windows.Forms.Label lblRestorePath;
        private System.Windows.Forms.Panel panelBackupPath;
        private System.Windows.Forms.Button btnBrowseBackup;
        private System.Windows.Forms.TextBox txtBackupPath;
        private System.Windows.Forms.Label lblBackupPath;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnClose;
    }
}
