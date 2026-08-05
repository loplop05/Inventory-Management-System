namespace InventoryManagementSystem
{
    partial class frmProductsManagment
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
            this._sidebar = new ucSidebarNav();
            this._contentPanel = new System.Windows.Forms.Panel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._lblPageTitle = new System.Windows.Forms.Label();
            this._toolbarPanel = new System.Windows.Forms.Panel();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this._btnAddProduct = new System.Windows.Forms.Button();
            this._gridPanel = new System.Windows.Forms.Panel();
            this.DataGVProducts = new System.Windows.Forms.DataGridView();
            this._lblEmptyState = new System.Windows.Forms.Label();
            this._toolbarPanel.SuspendLayout();
            this._gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVProducts)).BeginInit();
            this._contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _sidebar
            // 
            this._sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this._sidebar.Location = new System.Drawing.Point(0, 0);
            this._sidebar.Name = "_sidebar";
            this._sidebar.Size = new System.Drawing.Size(240, 698);
            this._sidebar.TabIndex = 0;
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._headerPanel);
            this._contentPanel.Controls.Add(this._toolbarPanel);
            this._contentPanel.Controls.Add(this._gridPanel);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(240, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1126, 698);
            this._contentPanel.TabIndex = 1;
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._lblPageTitle);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1126, 60);
            this._headerPanel.TabIndex = 0;
            // 
            // _lblPageTitle
            // 
            this._lblPageTitle.AutoSize = true;
            this._lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblPageTitle.Location = new System.Drawing.Point(20, 15);
            this._lblPageTitle.Name = "_lblPageTitle";
            this._lblPageTitle.Size = new System.Drawing.Size(135, 32);
            this._lblPageTitle.TabIndex = 0;
            this._lblPageTitle.Text = "Inventory";
            // 
            // _toolbarPanel
            // 
            this._toolbarPanel.Controls.Add(this._txtSearch);
            this._toolbarPanel.Controls.Add(this._cmbCategoryFilter);
            this._toolbarPanel.Controls.Add(this._btnAddProduct);
            this._toolbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._toolbarPanel.Location = new System.Drawing.Point(0, 60);
            this._toolbarPanel.Name = "_toolbarPanel";
            this._toolbarPanel.Size = new System.Drawing.Size(1126, 50);
            this._toolbarPanel.TabIndex = 1;
            // 
            // _txtSearch
            // 
            this._txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._txtSearch.Location = new System.Drawing.Point(20, 10);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(300, 28);
            this._txtSearch.TabIndex = 0;
            this._txtSearch.TextChanged += new System.EventHandler(this._txtSearch_TextChanged);
            // 
            // _cmbCategoryFilter
            // 
            this._cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbCategoryFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._cmbCategoryFilter.FormattingEnabled = true;
            this._cmbCategoryFilter.Location = new System.Drawing.Point(330, 10);
            this._cmbCategoryFilter.Name = "_cmbCategoryFilter";
            this._cmbCategoryFilter.Size = new System.Drawing.Size(200, 28);
            this._cmbCategoryFilter.TabIndex = 1;
            this._cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this._cmbCategoryFilter_SelectedIndexChanged);
            // 
            // _btnAddProduct
            // 
            this._btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnAddProduct.Location = new System.Drawing.Point(540, 8);
            this._btnAddProduct.Name = "_btnAddProduct";
            this._btnAddProduct.Size = new System.Drawing.Size(120, 32);
            this._btnAddProduct.TabIndex = 2;
            this._btnAddProduct.Text = "+ Add Product";
            this._btnAddProduct.UseVisualStyleBackColor = true;
            this._btnAddProduct.Click += new System.EventHandler(this._btnAddProduct_Click);
            // 
            // _gridPanel
            // 
            this._gridPanel.Controls.Add(this.DataGVProducts);
            this._gridPanel.Controls.Add(this._lblEmptyState);
            this._gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPanel.Location = new System.Drawing.Point(0, 110);
            this._gridPanel.Name = "_gridPanel";
            this._gridPanel.Padding = new System.Windows.Forms.Padding(20);
            this._gridPanel.Size = new System.Drawing.Size(1126, 588);
            this._gridPanel.TabIndex = 2;
            // 
            // DataGVProducts
            // 
            this.DataGVProducts.AllowUserToAddRows = false;
            this.DataGVProducts.AllowUserToDeleteRows = false;
            this.DataGVProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGVProducts.Location = new System.Drawing.Point(20, 20);
            this.DataGVProducts.Name = "DataGVProducts";
            this.DataGVProducts.ReadOnly = true;
            this.DataGVProducts.RowHeadersVisible = false;
            this.DataGVProducts.RowTemplate.Height = 36;
            this.DataGVProducts.Size = new System.Drawing.Size(1086, 548);
            this.DataGVProducts.TabIndex = 0;
            // 
            // _lblEmptyState
            // 
            this._lblEmptyState.AutoSize = true;
            this._lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._lblEmptyState.ForeColor = System.Drawing.Color.Gray;
            this._lblEmptyState.Location = new System.Drawing.Point(500, 250);
            this._lblEmptyState.Name = "_lblEmptyState";
            this._lblEmptyState.Size = new System.Drawing.Size(126, 28);
            this._lblEmptyState.TabIndex = 1;
            this._lblEmptyState.Text = "No products found";
            this._lblEmptyState.Visible = false;
            // 
            // frmProductsManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this._contentPanel);
            this.Controls.Add(this._sidebar);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "frmProductsManagment";
            this.Text = "Products Management";
            this.Load += new System.EventHandler(this.frmProductsManagment_Load);
            this._toolbarPanel.ResumeLayout(false);
            this._toolbarPanel.PerformLayout();
            this._gridPanel.ResumeLayout(false);
            this._gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVProducts)).EndInit();
            this._contentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ucSidebarNav _sidebar;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _lblPageTitle;
        private System.Windows.Forms.Panel _toolbarPanel;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.ComboBox _cmbCategoryFilter;
        private System.Windows.Forms.Button _btnAddProduct;
        private System.Windows.Forms.Panel _gridPanel;
        private System.Windows.Forms.Label _lblEmptyState;
        private System.Windows.Forms.DataGridView DataGVProducts;
    }
}
