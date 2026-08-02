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
            this._mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._searchPanel = new System.Windows.Forms.Panel();
            this._lblSearch = new System.Windows.Forms.Label();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._gridPanel = new System.Windows.Forms.Panel();
            this.DataGVProducts = new System.Windows.Forms.DataGridView();
            this._lblEmptyState = new System.Windows.Forms.Label();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnUpdateProduct = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnStockValuationReport = new System.Windows.Forms.Button();
            this._paginationPanel = new System.Windows.Forms.Panel();
            this._btnPreviousPage = new System.Windows.Forms.Button();
            this._lblPageInfo = new System.Windows.Forms.Label();
            this._btnNextPage = new System.Windows.Forms.Button();
            this._mainLayoutPanel.SuspendLayout();
            this._searchPanel.SuspendLayout();
            this._gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVProducts)).BeginInit();
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
            this._searchPanel.Size = new System.Drawing.Size(904, 60);
            this._searchPanel.TabIndex = 1;
            // 
            // _lblSearch
            // 
            this._lblSearch.AutoSize = true;
            this._lblSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSearch.Location = new System.Drawing.Point(0, 15);
            this._lblSearch.Name = "_lblSearch";
            this._lblSearch.Size = new System.Drawing.Size(58, 28);
            this._lblSearch.TabIndex = 0;
            this._lblSearch.Text = "Search:";
            this._lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            // 
            // _txtSearch
            // 
            this._txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._txtSearch.Location = new System.Drawing.Point(64, 13);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(400, 34);
            this._txtSearch.TabIndex = 1;
            this._txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnRefresh.Location = new System.Drawing.Point(470, 13);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(100, 34);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // _gridPanel
            // 
            this._gridPanel.Controls.Add(this.DataGVProducts);
            this._gridPanel.Controls.Add(this._lblEmptyState);
            this._gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPanel.Location = new System.Drawing.Point(23, 143);
            this._gridPanel.Name = "_gridPanel";
            this._gridPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this._gridPanel.Size = new System.Drawing.Size(904, 314);
            this._gridPanel.TabIndex = 2;
            this._gridPanel.AutoScroll = true;
            // 
            // DataGVProducts
            // 
            this.DataGVProducts.AllowUserToAddRows = false;
            this.DataGVProducts.AllowUserToDeleteRows = false;
            this.DataGVProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGVProducts.Location = new System.Drawing.Point(0, 10);
            this.DataGVProducts.Name = "DataGVProducts";
            this.DataGVProducts.ReadOnly = true;
            this.DataGVProducts.RowHeadersWidth = 51;
            this.DataGVProducts.RowTemplate.Height = 24;
            this.DataGVProducts.Size = new System.Drawing.Size(904, 304);
            this.DataGVProducts.TabIndex = 0;
            // 
            // _lblEmptyState
            // 
            this._lblEmptyState.AutoSize = true;
            this._lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._lblEmptyState.ForeColor = System.Drawing.Color.Gray;
            this._lblEmptyState.Location = new System.Drawing.Point(350, 140);
            this._lblEmptyState.Name = "_lblEmptyState";
            this._lblEmptyState.Size = new System.Drawing.Size(204, 28);
            this._lblEmptyState.TabIndex = 1;
            this._lblEmptyState.Text = "No products found";
            this._lblEmptyState.Visible = false;
            this._lblEmptyState.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // _actionsPanel
            // 
            this._actionsPanel.Controls.Add(this.btnAddProduct);
            this._actionsPanel.Controls.Add(this.btnUpdateProduct);
            this._actionsPanel.Controls.Add(this.btnDeleteProduct);
            this._actionsPanel.Controls.Add(this.btnStockValuationReport);
            this._actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionsPanel.Location = new System.Drawing.Point(23, 457);
            this._actionsPanel.Name = "_actionsPanel";
            this._actionsPanel.Size = new System.Drawing.Size(904, 60);
            this._actionsPanel.TabIndex = 3;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAddProduct.Location = new System.Drawing.Point(754, 13);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(150, 34);
            this.btnAddProduct.TabIndex = 0;
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnUpdateProduct
            // 
            this.btnUpdateProduct.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnUpdateProduct.Location = new System.Drawing.Point(598, 13);
            this.btnUpdateProduct.Name = "btnUpdateProduct";
            this.btnUpdateProduct.Size = new System.Drawing.Size(150, 34);
            this.btnUpdateProduct.TabIndex = 1;
            this.btnUpdateProduct.Text = "Update Product";
            this.btnUpdateProduct.UseVisualStyleBackColor = true;
            this.btnUpdateProduct.Click += new System.EventHandler(this.btnUpdateProduct_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDeleteProduct.Location = new System.Drawing.Point(442, 13);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(150, 34);
            this.btnDeleteProduct.TabIndex = 2;
            this.btnDeleteProduct.Text = "Delete Product";
            this.btnDeleteProduct.UseVisualStyleBackColor = true;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // btnStockValuationReport
            // 
            this.btnStockValuationReport.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnStockValuationReport.Location = new System.Drawing.Point(286, 13);
            this.btnStockValuationReport.Name = "btnStockValuationReport";
            this.btnStockValuationReport.Size = new System.Drawing.Size(150, 34);
            this.btnStockValuationReport.TabIndex = 3;
            this.btnStockValuationReport.Text = "Stock Report";
            this.btnStockValuationReport.UseVisualStyleBackColor = true;
            this.btnStockValuationReport.Click += new System.EventHandler(this.btnStockValuationReport_Click);
            // 
            // _paginationPanel
            // 
            this._paginationPanel.Controls.Add(this._btnPreviousPage);
            this._paginationPanel.Controls.Add(this._lblPageInfo);
            this._paginationPanel.Controls.Add(this._btnNextPage);
            this._paginationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paginationPanel.Location = new System.Drawing.Point(23, 517);
            this._paginationPanel.Name = "_paginationPanel";
            this._paginationPanel.Size = new System.Drawing.Size(904, 50);
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
            this._lblPageInfo.Size = new System.Drawing.Size(0, 24);
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
            // frmProductsManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 600);
            this.Controls.Add(this._mainLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "frmProductsManagment";
            this.Text = "Products Management";
            this.Load += new System.EventHandler(this.frmProductsManagment_Load);
            this._mainLayoutPanel.ResumeLayout(false);
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            this._gridPanel.ResumeLayout(false);
            this._gridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVProducts)).EndInit();
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
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnUpdateProduct;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnStockValuationReport;
        private System.Windows.Forms.DataGridView DataGVProducts;
    }
}
