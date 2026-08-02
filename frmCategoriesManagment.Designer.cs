namespace InventoryManagementSystem
{
    partial class frmCategoriesManagment
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
            this._paginationPanel = new System.Windows.Forms.Panel();
            this._btnNextPage = new System.Windows.Forms.Button();
            this._lblPageInfo = new System.Windows.Forms.Label();
            this._btnPreviousPage = new System.Windows.Forms.Button();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnUpdateCategory = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this._gridPanel = new System.Windows.Forms.Panel();
            this._lblEmptyState = new System.Windows.Forms.Label();
            this.DataGVCategories = new System.Windows.Forms.DataGridView();
            this._searchPanel = new System.Windows.Forms.Panel();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._lblSearch = new System.Windows.Forms.Label();
            this._mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._paginationPanel.SuspendLayout();
            this._actionsPanel.SuspendLayout();
            this._gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).BeginInit();
            this._searchPanel.SuspendLayout();
            this._mainLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _paginationPanel
            // 
            this._paginationPanel.Controls.Add(this._btnPreviousPage);
            this._paginationPanel.Controls.Add(this._lblPageInfo);
            this._paginationPanel.Controls.Add(this._btnNextPage);
            this._paginationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paginationPanel.Location = new System.Drawing.Point(23, 533);
            this._paginationPanel.Name = "_paginationPanel";
            this._paginationPanel.Size = new System.Drawing.Size(904, 44);
            this._paginationPanel.TabIndex = 4;
            // 
            // _btnNextPage
            // 
            this._btnNextPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnNextPage.Location = new System.Drawing.Point(112, 8);
            this._btnNextPage.Name = "_btnNextPage";
            this._btnNextPage.Size = new System.Drawing.Size(100, 34);
            this._btnNextPage.TabIndex = 2;
            this._btnNextPage.Text = "Next";
            this._btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
            this._btnNextPage.UseVisualStyleBackColor = true;
            this._btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // _lblPageInfo
            // 
            this._lblPageInfo.AutoSize = true;
            this._lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblPageInfo.Location = new System.Drawing.Point(106, 15);
            this._lblPageInfo.Name = "_lblPageInfo";
            this._lblPageInfo.Size = new System.Drawing.Size(95, 23);
            this._lblPageInfo.TabIndex = 1;
            this._lblPageInfo.Text = "Page 1 of 1";
            this._lblPageInfo.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // _btnPreviousPage
            // 
            this._btnPreviousPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnPreviousPage.Location = new System.Drawing.Point(0, 8);
            this._btnPreviousPage.Name = "_btnPreviousPage";
            this._btnPreviousPage.Size = new System.Drawing.Size(100, 34);
            this._btnPreviousPage.TabIndex = 0;
            this._btnPreviousPage.Text = "Previous";
            this._btnPreviousPage.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
            this._btnPreviousPage.UseVisualStyleBackColor = true;
            this._btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // _actionsPanel
            // 
            this._actionsPanel.Controls.Add(this.btnAddCategory);
            this._actionsPanel.Controls.Add(this.btnUpdateCategory);
            this._actionsPanel.Controls.Add(this.btnDeleteCategory);
            this._actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionsPanel.Location = new System.Drawing.Point(23, 473);
            this._actionsPanel.Name = "_actionsPanel";
            this._actionsPanel.Size = new System.Drawing.Size(904, 54);
            this._actionsPanel.TabIndex = 3;
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDeleteCategory.Location = new System.Drawing.Point(439, 3);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(150, 34);
            this.btnDeleteCategory.TabIndex = 2;
            this.btnDeleteCategory.Text = "Delete Category";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnUpdateCategory
            // 
            this.btnUpdateCategory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpdateCategory.Location = new System.Drawing.Point(595, 3);
            this.btnUpdateCategory.Name = "btnUpdateCategory";
            this.btnUpdateCategory.Size = new System.Drawing.Size(150, 34);
            this.btnUpdateCategory.TabIndex = 1;
            this.btnUpdateCategory.Text = "Update Category";
            this.btnUpdateCategory.UseVisualStyleBackColor = true;
            this.btnUpdateCategory.Click += new System.EventHandler(this.btnUpdateCategory_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAddCategory.Location = new System.Drawing.Point(751, 3);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(150, 34);
            this.btnAddCategory.TabIndex = 0;
            this.btnAddCategory.Text = "Add Category";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // _gridPanel
            // 
            this._gridPanel.AutoScroll = true;
            this._gridPanel.Controls.Add(this.DataGVCategories);
            this._gridPanel.Controls.Add(this._lblEmptyState);
            this._gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPanel.Location = new System.Drawing.Point(23, 143);
            this._gridPanel.Name = "_gridPanel";
            this._gridPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this._gridPanel.Size = new System.Drawing.Size(904, 324);
            this._gridPanel.TabIndex = 2;
            // 
            // _lblEmptyState
            // 
            this._lblEmptyState.AutoSize = true;
            this._lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._lblEmptyState.ForeColor = System.Drawing.Color.Gray;
            this._lblEmptyState.Location = new System.Drawing.Point(350, 140);
            this._lblEmptyState.Name = "_lblEmptyState";
            this._lblEmptyState.Size = new System.Drawing.Size(191, 28);
            this._lblEmptyState.TabIndex = 1;
            this._lblEmptyState.Text = "No categories found";
            this._lblEmptyState.Visible = false;
            this._lblEmptyState.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // DataGVCategories
            // 
            this.DataGVCategories.AllowUserToAddRows = false;
            this.DataGVCategories.AllowUserToDeleteRows = false;
            this.DataGVCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGVCategories.Location = new System.Drawing.Point(0, 10);
            this.DataGVCategories.Name = "DataGVCategories";
            this.DataGVCategories.ReadOnly = true;
            this.DataGVCategories.RowHeadersWidth = 51;
            this.DataGVCategories.RowTemplate.Height = 24;
            this.DataGVCategories.Size = new System.Drawing.Size(904, 304);
            this.DataGVCategories.TabIndex = 0;
            // 
            // _searchPanel
            // 
            this._searchPanel.Controls.Add(this._lblSearch);
            this._searchPanel.Controls.Add(this._txtSearch);
            this._searchPanel.Controls.Add(this._btnRefresh);
            this._searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchPanel.Location = new System.Drawing.Point(23, 83);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.Size = new System.Drawing.Size(904, 54);
            this._searchPanel.TabIndex = 1;
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnRefresh.Location = new System.Drawing.Point(537, 13);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(100, 34);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // _txtSearch
            // 
            this._txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._txtSearch.Location = new System.Drawing.Point(131, 13);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(400, 34);
            this._txtSearch.TabIndex = 1;
            this._txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // _lblSearch
            // 
            this._lblSearch.AutoSize = true;
            this._lblSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSearch.Location = new System.Drawing.Point(20, 15);
            this._lblSearch.Name = "_lblSearch";
            this._lblSearch.Size = new System.Drawing.Size(80, 28);
            this._lblSearch.TabIndex = 0;
            this._lblSearch.Text = "Search:";
            this._lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            // 
            // _mainLayoutPanel
            // 
            this._mainLayoutPanel.ColumnCount = 1;
            this._mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Controls.Add(this._searchPanel, 0, 0);
            this._mainLayoutPanel.Controls.Add(this._gridPanel, 0, 1);
            this._mainLayoutPanel.Controls.Add(this._actionsPanel, 0, 2);
            this._mainLayoutPanel.Controls.Add(this._paginationPanel, 0, 3);
            this._mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainLayoutPanel.Name = "_mainLayoutPanel";
            this._mainLayoutPanel.Padding = new System.Windows.Forms.Padding(20);
            this._mainLayoutPanel.RowCount = 4;
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._mainLayoutPanel.Size = new System.Drawing.Size(950, 600);
            this._mainLayoutPanel.TabIndex = 0;
            // 
            // frmCategoriesManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this._mainLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmCategoriesManagment";
            this.Text = "Categories Management";
            this.Load += new System.EventHandler(this.frmCategoriesManagment_Load);
            this._paginationPanel.ResumeLayout(false);
            this._paginationPanel.PerformLayout();
            this._actionsPanel.ResumeLayout(false);
            this._gridPanel.ResumeLayout(false);
            this._gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).EndInit();
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            this._mainLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _paginationPanel;
        private System.Windows.Forms.Button _btnPreviousPage;
        private System.Windows.Forms.Label _lblPageInfo;
        private System.Windows.Forms.Button _btnNextPage;
        private System.Windows.Forms.FlowLayoutPanel _actionsPanel;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnUpdateCategory;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.Panel _gridPanel;
        private System.Windows.Forms.DataGridView DataGVCategories;
        private System.Windows.Forms.Label _lblEmptyState;
        private System.Windows.Forms.Panel _searchPanel;
        private System.Windows.Forms.Label _lblSearch;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
    }
}
