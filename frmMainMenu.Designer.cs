namespace InventoryManagementSystem
{
    partial class frmMainMenu
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
            this._headerPanel = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._btnExit = new System.Windows.Forms.Button();
            this._buttonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnReceiptSearch = new System.Windows.Forms.Button();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this._mainLayoutPanel.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainLayoutPanel
            // 
            this._mainLayoutPanel.ColumnCount = 1;
            this._mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Controls.Add(this._headerPanel, 0, 0);
            this._mainLayoutPanel.Controls.Add(this._buttonsPanel, 0, 1);
            this._mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainLayoutPanel.Name = "_mainLayoutPanel";
            this._mainLayoutPanel.RowCount = 2;
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Size = new System.Drawing.Size(1216, 592);
            this._mainLayoutPanel.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._lblTitle);
            this._headerPanel.Controls.Add(this._btnExit);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Padding = new System.Windows.Forms.Padding(20);
            this._headerPanel.Size = new System.Drawing.Size(1216, 80);
            this._headerPanel.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this._lblTitle.Location = new System.Drawing.Point(20, 25);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(285, 32);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Inventory Management System";
            // 
            // _btnExit
            // 
            this._btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnExit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._btnExit.Location = new System.Drawing.Point(1110, 20);
            this._btnExit.Name = "_btnExit";
            this._btnExit.Size = new System.Drawing.Size(86, 34);
            this._btnExit.TabIndex = 1;
            this._btnExit.Text = "Exit";
            this._btnExit.UseVisualStyleBackColor = true;
            this._btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.ColumnCount = 3;
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._buttonsPanel.Controls.Add(this.btnCategories, 0, 0);
            this._buttonsPanel.Controls.Add(this.btnSuppliers, 1, 0);
            this._buttonsPanel.Controls.Add(this.btnProducts, 2, 0);
            this._buttonsPanel.Controls.Add(this.btnReceiptSearch, 0, 1);
            this._buttonsPanel.Controls.Add(this.btnPrintReceipt, 1, 1);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.Location = new System.Drawing.Point(0, 80);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Padding = new System.Windows.Forms.Padding(40);
            this._buttonsPanel.RowCount = 2;
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.Size = new System.Drawing.Size(1216, 512);
            this._buttonsPanel.TabIndex = 1;
            // 
            // btnCategories
            // 
            this.btnCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCategories.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCategories.Location = new System.Drawing.Point(43, 43);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(343, 213);
            this.btnCategories.TabIndex = 0;
            this.btnCategories.Text = "Categories";
            this.btnCategories.UseVisualStyleBackColor = true;
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSuppliers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSuppliers.Location = new System.Drawing.Point(392, 43);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(343, 213);
            this.btnSuppliers.TabIndex = 1;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnProducts.Location = new System.Drawing.Point(741, 43);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(432, 213);
            this.btnProducts.TabIndex = 2;
            this.btnProducts.Text = "Products";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnReceiptSearch
            // 
            this.btnReceiptSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReceiptSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReceiptSearch.Location = new System.Drawing.Point(43, 256);
            this.btnReceiptSearch.Name = "btnReceiptSearch";
            this.btnReceiptSearch.Size = new System.Drawing.Size(343, 213);
            this.btnReceiptSearch.TabIndex = 3;
            this.btnReceiptSearch.Text = "Receipt Search";
            this.btnReceiptSearch.UseVisualStyleBackColor = true;
            this.btnReceiptSearch.Click += new System.EventHandler(this.btnReceiptSearch_Click);
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintReceipt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPrintReceipt.Location = new System.Drawing.Point(392, 256);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(343, 213);
            this.btnPrintReceipt.TabIndex = 4;
            this.btnPrintReceipt.Text = "Print Receipt";
            this.btnPrintReceipt.UseVisualStyleBackColor = true;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 592);
            this.Controls.Add(this._mainLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "frmMainMenu";
            this.Text = "Inventory Management System";
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this._mainLayoutPanel.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Button _btnExit;
        private System.Windows.Forms.TableLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnReceiptSearch;
        private System.Windows.Forms.Button btnPrintReceipt;
    }
}
