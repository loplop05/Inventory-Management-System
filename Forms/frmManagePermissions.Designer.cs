namespace InventoryManagementSystem
{
    partial class frmManagePermissions
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._mainPanel = new System.Windows.Forms.Panel();
            this._lblUserInfo = new System.Windows.Forms.Label();
            this._lblRoleInfo = new System.Windows.Forms.Label();
            this._lblPermissionsTitle = new System.Windows.Forms.Label();
            this.gridPermissions = new System.Windows.Forms.DataGridView();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPermissions)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._lblUserInfo);
            this._mainPanel.Controls.Add(this._lblRoleInfo);
            this._mainPanel.Controls.Add(this._lblPermissionsTitle);
            this._mainPanel.Controls.Add(this.gridPermissions);
            this._mainPanel.Controls.Add(this._btnSave);
            this._mainPanel.Controls.Add(this._btnCancel);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Padding = new System.Windows.Forms.Padding(20);
            this._mainPanel.Size = new System.Drawing.Size(600, 500);
            this._mainPanel.TabIndex = 0;
            // 
            // _lblUserInfo
            // 
            this._lblUserInfo.AutoSize = true;
            this._lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblUserInfo.Location = new System.Drawing.Point(20, 20);
            this._lblUserInfo.Name = "_lblUserInfo";
            this._lblUserInfo.Size = new System.Drawing.Size(0, 28);
            this._lblUserInfo.TabIndex = 0;
            // 
            // _lblRoleInfo
            // 
            this._lblRoleInfo.AutoSize = true;
            this._lblRoleInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblRoleInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblRoleInfo.Location = new System.Drawing.Point(20, 50);
            this._lblRoleInfo.Name = "_lblRoleInfo";
            this._lblRoleInfo.Size = new System.Drawing.Size(0, 20);
            this._lblRoleInfo.TabIndex = 1;
            // 
            // _lblPermissionsTitle
            // 
            this._lblPermissionsTitle.AutoSize = true;
            this._lblPermissionsTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblPermissionsTitle.Location = new System.Drawing.Point(20, 80);
            this._lblPermissionsTitle.Name = "_lblPermissionsTitle";
            this._lblPermissionsTitle.Size = new System.Drawing.Size(0, 24);
            this._lblPermissionsTitle.TabIndex = 2;
            // 
            // gridPermissions
            // 
            this.gridPermissions.AllowUserToAddRows = false;
            this.gridPermissions.AllowUserToDeleteRows = false;
            this.gridPermissions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPermissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPermissions.Location = new System.Drawing.Point(20, 110);
            this.gridPermissions.Name = "gridPermissions";
            this.gridPermissions.Size = new System.Drawing.Size(540, 300);
            this.gridPermissions.TabIndex = 3;
            // 
            // _btnSave
            // 
            this._btnSave.Location = new System.Drawing.Point(20, 430);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(100, 35);
            this._btnSave.TabIndex = 4;
            this._btnSave.Text = "Save";
            this._btnSave.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(460, 430);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 35);
            this._btnCancel.TabIndex = 5;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmManagePermissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManagePermissions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Permissions";
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPermissions)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.Label _lblUserInfo;
        private System.Windows.Forms.Label _lblRoleInfo;
        private System.Windows.Forms.Label _lblPermissionsTitle;
        private System.Windows.Forms.DataGridView gridPermissions;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnCancel;
    }
}
