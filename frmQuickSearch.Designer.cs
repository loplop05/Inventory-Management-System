namespace InventoryManagementSystem
{
    partial class frmQuickSearch
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
            this.panelResults = new System.Windows.Forms.Panel();
            this.gridResults = new System.Windows.Forms.DataGridView();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblSearchType = new System.Windows.Forms.Label();
            this.cmbSearchType = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this._contentPanel.SuspendLayout();
            this.panelResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            this.panelSearch.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this.panelResults);
            this._contentPanel.Controls.Add(this.panelSearch);
            this._contentPanel.Controls.Add(this.panelButtons);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(700, 500);
            this._contentPanel.TabIndex = 0;
            // 
            // panelResults
            // 
            this.panelResults.Controls.Add(this.gridResults);
            this.panelResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResults.Location = new System.Drawing.Point(0, 80);
            this.panelResults.Name = "panelResults";
            this.panelResults.Size = new System.Drawing.Size(700, 380);
            this.panelResults.TabIndex = 2;
            // 
            // gridResults
            // 
            this.gridResults.AllowUserToAddRows = false;
            this.gridResults.AllowUserToDeleteRows = false;
            this.gridResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResults.Location = new System.Drawing.Point(0, 0);
            this.gridResults.Name = "gridResults";
            this.gridResults.ReadOnly = true;
            this.gridResults.RowHeadersVisible = false;
            this.gridResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridResults.Size = new System.Drawing.Size(700, 380);
            this.gridResults.TabIndex = 0;
            this.gridResults.DoubleClick += new System.EventHandler(this.gridResults_DoubleClick);
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.lblSearchType);
            this.panelSearch.Controls.Add(this.cmbSearchType);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(700, 80);
            this.panelSearch.TabIndex = 1;
            // 
            // lblSearchType
            // 
            this.lblSearchType.AutoSize = true;
            this.lblSearchType.Location = new System.Drawing.Point(10, 45);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(83, 19);
            this.lblSearchType.TabIndex = 3;
            this.lblSearchType.Text = "Search Type:";
            // 
            // cmbSearchType
            // 
            this.cmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchType.FormattingEnabled = true;
            this.cmbSearchType.Items.AddRange(new object[] { "Products", "Customers", "Categories", "Suppliers" });
            this.cmbSearchType.Location = new System.Drawing.Point(100, 42);
            this.cmbSearchType.Name = "cmbSearchType";
            this.cmbSearchType.Size = new System.Drawing.Size(150, 27);
            this.cmbSearchType.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(100, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(580, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 13);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(56, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Controls.Add(this.btnSearch);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 460);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(700, 40);
            this.panelButtons.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(610, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(520, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmQuickSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this._contentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuickSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick Search";
            this.Load += new System.EventHandler(this.frmQuickSearch_Load);
            this._contentPanel.ResumeLayout(false);
            this.panelResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.Panel panelResults;
        private System.Windows.Forms.DataGridView gridResults;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblSearchType;
        private System.Windows.Forms.ComboBox cmbSearchType;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSearch;
    }
}
