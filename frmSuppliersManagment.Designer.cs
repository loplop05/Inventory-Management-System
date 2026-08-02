namespace InventoryManagementSystem
{
    partial class frmSuppliersManagment
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
            this._mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._searchPanel = new System.Windows.Forms.Panel();
            this._lblSearch = new System.Windows.Forms.Label();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._gridPanel = new System.Windows.Forms.Panel();
            this.DataGVSuppliers = new System.Windows.Forms.DataGridView();
            this._lblEmptyState = new System.Windows.Forms.Label();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.btnUpdateSupplier = new System.Windows.Forms.Button();
            this.btnDeleteSupplier = new System.Windows.Forms.Button();
            this.btnViewPerformance = new System.Windows.Forms.Button();
            this._paginationPanel = new System.Windows.Forms.Panel();
            this._btnPreviousPage = new System.Windows.Forms.Button();
            this._lblPageInfo = new System.Windows.Forms.Label();
            this._btnNextPage = new System.Windows.Forms.Button();
            this._mainLayoutPanel.SuspendLayout();
            this._searchPanel.SuspendLayout();
            this._gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVSuppliers)).BeginInit();
            this._actionsPanel.SuspendLayout();
            this._paginationPanel.SuspendLayout();
            this.SuspendLayout();
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
            // _lblSearch
            // 
            this._lblSearch.AutoSize = true;
            this._lblSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSearch.Location = new System.Drawing.Point(20, 13);
            this._lblSearch.Name = "_lblSearch";
            this._lblSearch.Size = new System.Drawing.Size(80, 28);
            this._lblSearch.TabIndex = 0;
            this._lblSearch.Text = "Search:";
            this._lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            // 
            // _txtSearch
            // 
            this._txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._txtSearch.Location = new System.Drawing.Point(127, 10);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(400, 34);
            this._txtSearch.TabIndex = 1;
            this._txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnRefresh.Location = new System.Drawing.Point(533, 10);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(107, 34);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // _gridPanel
            // 
            this._gridPanel.AutoScroll = true;
            this._gridPanel.Controls.Add(this.DataGVSuppliers);
            this._gridPanel.Controls.Add(this._lblEmptyState);
            this._gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPanel.Location = new System.Drawing.Point(23, 143);
            this._gridPanel.Name = "_gridPanel";
            this._gridPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this._gridPanel.Size = new System.Drawing.Size(904, 324);
            this._gridPanel.TabIndex = 2;
            // 
            // DataGVSuppliers
            // 
            this.DataGVSuppliers.AllowUserToAddRows = false;
            this.DataGVSuppliers.AllowUserToDeleteRows = false;
            this.DataGVSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGVSuppliers.Location = new System.Drawing.Point(0, 0);
            this.DataGVSuppliers.Name = "DataGVSuppliers";
            this.DataGVSuppliers.ReadOnly = true;
            this.DataGVSuppliers.RowHeadersWidth = 51;
            this.DataGVSuppliers.RowTemplate.Height = 24;
            this.DataGVSuppliers.Size = new System.Drawing.Size(904, 314);
            this.DataGVSuppliers.TabIndex = 0;
            // 
            // _lblEmptyState
            // 
            this._lblEmptyState.AutoSize = true;
            this._lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._lblEmptyState.ForeColor = System.Drawing.Color.Gray;
            this._lblEmptyState.Location = new System.Drawing.Point(350, 140);
            this._lblEmptyState.Name = "_lblEmptyState";
            this._lblEmptyState.Size = new System.Drawing.Size(179, 28);
            this._lblEmptyState.TabIndex = 1;
            this._lblEmptyState.Text = "No suppliers found";
            this._lblEmptyState.Visible = false;
            this._lblEmptyState.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // _actionsPanel
            // 
            this._actionsPanel.Controls.Add(this.btnAddSupplier);
            this._actionsPanel.Controls.Add(this.btnUpdateSupplier);
            this._actionsPanel.Controls.Add(this.btnDeleteSupplier);
            this._actionsPanel.Controls.Add(this.btnViewPerformance);
            this._actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionsPanel.Location = new System.Drawing.Point(23, 473);
            this._actionsPanel.Name = "_actionsPanel";
            this._actionsPanel.Size = new System.Drawing.Size(904, 54);
            this._actionsPanel.TabIndex = 3;
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAddSupplier.Location = new System.Drawing.Point(748, 3);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(153, 51);
            this.btnAddSupplier.TabIndex = 0;
            this.btnAddSupplier.Text = "Add Supplier";
            this.btnAddSupplier.UseVisualStyleBackColor = true;
            this.btnAddSupplier.Click += new System.EventHandler(this.btnAddSupplier_Click);
            // 
            // btnUpdateSupplier
            // 
            this.btnUpdateSupplier.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpdateSupplier.Location = new System.Drawing.Point(592, 3);
            this.btnUpdateSupplier.Name = "btnUpdateSupplier";
            this.btnUpdateSupplier.Size = new System.Drawing.Size(150, 51);
            this.btnUpdateSupplier.TabIndex = 1;
            this.btnUpdateSupplier.Text = "Update Supplier";
            this.btnUpdateSupplier.UseVisualStyleBackColor = true;
            this.btnUpdateSupplier.Click += new System.EventHandler(this.btnUpdateSupplier_Click);
            // 
            // btnDeleteSupplier
            // 
            this.btnDeleteSupplier.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDeleteSupplier.Location = new System.Drawing.Point(436, 3);
            this.btnDeleteSupplier.Name = "btnDeleteSupplier";
            this.btnDeleteSupplier.Size = new System.Drawing.Size(150, 51);
            this.btnDeleteSupplier.TabIndex = 2;
            this.btnDeleteSupplier.Text = "Delete Supplier";
            this.btnDeleteSupplier.UseVisualStyleBackColor = true;
            this.btnDeleteSupplier.Click += new System.EventHandler(this.btnDeleteSupplier_Click);
            // 
            // btnViewPerformance
            // 
            this.btnViewPerformance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnViewPerformance.Location = new System.Drawing.Point(280, 3);
            this.btnViewPerformance.Name = "btnViewPerformance";
            this.btnViewPerformance.Size = new System.Drawing.Size(150, 51);
            this.btnViewPerformance.TabIndex = 3;
            this.btnViewPerformance.Text = "Performance";
            this.btnViewPerformance.UseVisualStyleBackColor = true;
            this.btnViewPerformance.Click += new System.EventHandler(this.btnViewPerformance_Click);
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
            // frmSuppliersManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this._mainLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmSuppliersManagment";
            this.Text = "Suppliers Management";
            this.Load += new System.EventHandler(this.frmSuppliersManagment_Load);
            this._mainLayoutPanel.ResumeLayout(false);
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            this._gridPanel.ResumeLayout(false);
            this._gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVSuppliers)).EndInit();
            this._actionsPanel.ResumeLayout(false);
            this._paginationPanel.ResumeLayout(false);
            this._paginationPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
        private System.Windows.Forms.Panel _searchPanel;
        private System.Windows.Forms.Label _lblSearch;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Panel _gridPanel;
        private System.Windows.Forms.Label _lblEmptyState;
        private System.Windows.Forms.FlowLayoutPanel _actionsPanel;
        private System.Windows.Forms.Panel _paginationPanel;
        private System.Windows.Forms.Button _btnPreviousPage;
        private System.Windows.Forms.Label _lblPageInfo;
        private System.Windows.Forms.Button _btnNextPage;
        private System.Windows.Forms.Button btnAddSupplier;
        private System.Windows.Forms.Button btnUpdateSupplier;
        private System.Windows.Forms.Button btnDeleteSupplier;
        private System.Windows.Forms.Button btnViewPerformance;
        private System.Windows.Forms.DataGridView DataGVSuppliers;
    }
}
