namespace InventoryManagementSystem
{
    partial class frmUserManagement
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
            this.DataGVUsers = new System.Windows.Forms.DataGridView();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._btnRefresh = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.btnDeactivateUser = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnManagePermissions = new System.Windows.Forms.Button();
            this._lblPageInfo = new System.Windows.Forms.Label();
            this._btnPreviousPage = new System.Windows.Forms.Button();
            this._btnNextPage = new System.Windows.Forms.Button();
            this._lblEmptyState = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGVUsers
            // 
            this.DataGVUsers.AllowUserToAddRows = false;
            this.DataGVUsers.AllowUserToDeleteRows = false;
            this.DataGVUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGVUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGVUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVUsers.Location = new System.Drawing.Point(16, 80);
            this.DataGVUsers.Name = "DataGVUsers";
            this.DataGVUsers.ReadOnly = true;
            this.DataGVUsers.RowHeadersVisible = false;
            this.DataGVUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGVUsers.Size = new System.Drawing.Size(760, 350);
            this.DataGVUsers.TabIndex = 0;
            // 
            // _txtSearch
            // 
            this._txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSearch.Location = new System.Drawing.Point(16, 50);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(300, 27);
            this._txtSearch.TabIndex = 1;
            this._txtSearch.TextChanged += new System.EventHandler(this._txtSearch_TextChanged);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnRefresh.Location = new System.Drawing.Point(680, 50);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(96, 27);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this._btnRefresh_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddUser.Location = new System.Drawing.Point(16, 440);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 38);
            this.btnAddUser.TabIndex = 3;
            this.btnAddUser.Text = "Add";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditUser.Location = new System.Drawing.Point(122, 440);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(100, 38);
            this.btnEditUser.TabIndex = 4;
            this.btnEditUser.Text = "Edit";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnDeactivateUser
            // 
            this.btnDeactivateUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeactivateUser.Location = new System.Drawing.Point(228, 440);
            this.btnDeactivateUser.Name = "btnDeactivateUser";
            this.btnDeactivateUser.Size = new System.Drawing.Size(120, 38);
            this.btnDeactivateUser.TabIndex = 5;
            this.btnDeactivateUser.Text = "Deactivate";
            this.btnDeactivateUser.UseVisualStyleBackColor = true;
            this.btnDeactivateUser.Click += new System.EventHandler(this.btnDeactivateUser_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangePassword.Location = new System.Drawing.Point(354, 440);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(140, 38);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // btnManagePermissions
            // 
            this.btnManagePermissions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnManagePermissions.Location = new System.Drawing.Point(500, 440);
            this.btnManagePermissions.Name = "btnManagePermissions";
            this.btnManagePermissions.Size = new System.Drawing.Size(140, 38);
            this.btnManagePermissions.TabIndex = 7;
            this.btnManagePermissions.Text = "Permissions";
            this.btnManagePermissions.UseVisualStyleBackColor = true;
            this.btnManagePermissions.Click += new System.EventHandler(this.btnManagePermissions_Click);
            // 
            // _lblPageInfo
            // 
            this._lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._lblPageInfo.AutoSize = true;
            this._lblPageInfo.Location = new System.Drawing.Point(620, 450);
            this._lblPageInfo.Name = "_lblPageInfo";
            this._lblPageInfo.Size = new System.Drawing.Size(59, 20);
            this._lblPageInfo.TabIndex = 8;
            this._lblPageInfo.Text = "Page 1 of 1";
            // 
            // _btnPreviousPage
            // 
            this._btnPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnPreviousPage.Location = new System.Drawing.Point(520, 445);
            this._btnPreviousPage.Name = "_btnPreviousPage";
            this._btnPreviousPage.Size = new System.Drawing.Size(80, 30);
            this._btnPreviousPage.TabIndex = 9;
            this._btnPreviousPage.Text = "\u2039  Prev";
            this._btnPreviousPage.UseVisualStyleBackColor = true;
            this._btnPreviousPage.Click += new System.EventHandler(this._btnPreviousPage_Click);
            // 
            // _btnNextPage
            // 
            this._btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnNextPage.Location = new System.Drawing.Point(606, 445);
            this._btnNextPage.Name = "_btnNextPage";
            this._btnNextPage.Size = new System.Drawing.Size(80, 30);
            this._btnNextPage.TabIndex = 10;
            this._btnNextPage.Text = "Next  \u203A";
            this._btnNextPage.UseVisualStyleBackColor = true;
            this._btnNextPage.Click += new System.EventHandler(this._btnNextPage_Click);
            // 
            // _lblEmptyState
            // 
            this._lblEmptyState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this._lblEmptyState.ForeColor = System.Drawing.Color.Gray;
            this._lblEmptyState.Location = new System.Drawing.Point(16, 80);
            this._lblEmptyState.Name = "_lblEmptyState";
            this._lblEmptyState.Size = new System.Drawing.Size(760, 350);
            this._lblEmptyState.TabIndex = 10;
            this._lblEmptyState.Text = "No users found.";
            this._lblEmptyState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lblEmptyState.Visible = false;
            // 
            // frmUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 500);
            this.Controls.Add(this._lblEmptyState);
            this.Controls.Add(this._btnNextPage);
            this.Controls.Add(this._btnPreviousPage);
            this.Controls.Add(this._lblPageInfo);
            this.Controls.Add(this.btnManagePermissions);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnDeactivateUser);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this._btnRefresh);
            this.Controls.Add(this._txtSearch);
            this.Controls.Add(this.DataGVUsers);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "frmUserManagement";
            this.Text = "User Management";
            this.Load += new System.EventHandler(this.frmUserManagement_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmUserManagement_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGVUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView DataGVUsers;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.Button btnDeactivateUser;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnManagePermissions;
        private System.Windows.Forms.Label _lblPageInfo;
        private System.Windows.Forms.Button _btnPreviousPage;
        private System.Windows.Forms.Button _btnNextPage;
        private System.Windows.Forms.Label _lblEmptyState;
    }
}
