namespace InventoryManagementSystem
{
    partial class frmCustomerManagement
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
            this._buttonsPanel = new System.Windows.Forms.Panel();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._btnRefresh = new System.Windows.Forms.Button();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnEditCustomer = new System.Windows.Forms.Button();
            this.btnDeleteCustomer = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this._btnPreviousPage = new System.Windows.Forms.Button();
            this._btnNextPage = new System.Windows.Forms.Button();
            this._lblPageInfo = new System.Windows.Forms.Label();
            this._lblEmptyState = new System.Windows.Forms.Label();
            this.DataGVCustomers = new System.Windows.Forms.DataGridView();
            this._buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._txtSearch);
            this._buttonsPanel.Controls.Add(this._btnRefresh);
            this._buttonsPanel.Controls.Add(this.btnAddCustomer);
            this._buttonsPanel.Controls.Add(this.btnEditCustomer);
            this._buttonsPanel.Controls.Add(this.btnDeleteCustomer);
            this._buttonsPanel.Controls.Add(this.btnViewDetails);
            this._buttonsPanel.Controls.Add(this._btnPreviousPage);
            this._buttonsPanel.Controls.Add(this._btnNextPage);
            this._buttonsPanel.Controls.Add(this._lblPageInfo);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._buttonsPanel.Location = new System.Drawing.Point(0, 0);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(1200, 80);
            this._buttonsPanel.TabIndex = 0;
            // 
            // _txtSearch
            // 
            this._txtSearch.Location = new System.Drawing.Point(10, 10);
            this._txtSearch.Name = "_txtSearch";
            // this._txtSearch.PlaceholderText = "Search by phone or name...";
            this._txtSearch.Size = new System.Drawing.Size(250, 23);
            this._txtSearch.TabIndex = 0;
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Location = new System.Drawing.Point(270, 8);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(80, 27);
            this._btnRefresh.TabIndex = 1;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Location = new System.Drawing.Point(360, 8);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(80, 27);
            this.btnAddCustomer.TabIndex = 2;
            this.btnAddCustomer.Text = "Add";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Location = new System.Drawing.Point(450, 8);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(80, 27);
            this.btnEditCustomer.TabIndex = 3;
            this.btnEditCustomer.Text = "Edit";
            this.btnEditCustomer.UseVisualStyleBackColor = true;
            // 
            // btnDeleteCustomer
            // 
            this.btnDeleteCustomer.Location = new System.Drawing.Point(540, 8);
            this.btnDeleteCustomer.Name = "btnDeleteCustomer";
            this.btnDeleteCustomer.Size = new System.Drawing.Size(80, 27);
            this.btnDeleteCustomer.TabIndex = 4;
            this.btnDeleteCustomer.Text = "Delete";
            this.btnDeleteCustomer.UseVisualStyleBackColor = true;
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.Location = new System.Drawing.Point(630, 8);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(100, 27);
            this.btnViewDetails.TabIndex = 5;
            this.btnViewDetails.Text = "View Details";
            this.btnViewDetails.UseVisualStyleBackColor = true;
            // 
            // _btnPreviousPage
            // 
            this._btnPreviousPage.Location = new System.Drawing.Point(10, 45);
            this._btnPreviousPage.Name = "_btnPreviousPage";
            this._btnPreviousPage.Size = new System.Drawing.Size(80, 27);
            this._btnPreviousPage.TabIndex = 6;
            this._btnPreviousPage.Text = "Previous";
            this._btnPreviousPage.UseVisualStyleBackColor = true;
            // 
            // _btnNextPage
            // 
            this._btnNextPage.Location = new System.Drawing.Point(100, 45);
            this._btnNextPage.Name = "_btnNextPage";
            this._btnNextPage.Size = new System.Drawing.Size(80, 27);
            this._btnNextPage.TabIndex = 7;
            this._btnNextPage.Text = "Next";
            this._btnNextPage.UseVisualStyleBackColor = true;
            // 
            // _lblPageInfo
            // 
            this._lblPageInfo.AutoSize = true;
            this._lblPageInfo.Location = new System.Drawing.Point(190, 50);
            this._lblPageInfo.Name = "_lblPageInfo";
            this._lblPageInfo.Size = new System.Drawing.Size(0, 13);
            this._lblPageInfo.TabIndex = 8;
            // 
            // _lblEmptyState
            // 
            this._lblEmptyState.AutoSize = true;
            this._lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this._lblEmptyState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._lblEmptyState.Location = new System.Drawing.Point(500, 300);
            this._lblEmptyState.Name = "_lblEmptyState";
            this._lblEmptyState.Size = new System.Drawing.Size(200, 21);
            this._lblEmptyState.TabIndex = 2;
            this._lblEmptyState.Text = "No customers found";
            this._lblEmptyState.Visible = false;
            // 
            // DataGVCustomers
            // 
            this.DataGVCustomers.AllowUserToAddRows = false;
            this.DataGVCustomers.AllowUserToDeleteRows = false;
            this.DataGVCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGVCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGVCustomers.Location = new System.Drawing.Point(0, 80);
            this.DataGVCustomers.Name = "DataGVCustomers";
            this.DataGVCustomers.ReadOnly = true;
            this.DataGVCustomers.RowHeadersVisible = false;
            this.DataGVCustomers.RowHeadersWidth = 51;
            this.DataGVCustomers.RowTemplate.Height = 24;
            this.DataGVCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGVCustomers.Size = new System.Drawing.Size(1200, 520);
            this.DataGVCustomers.TabIndex = 1;
            // 
            // frmCustomerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.DataGVCustomers);
            this.Controls.Add(this._lblEmptyState);
            this.Controls.Add(this._buttonsPanel);
            this.MinimumSize = new System.Drawing.Size(900, 500);
            this.Name = "frmCustomerManagement";
            this.Text = "Customer Management";
            this.Load += new System.EventHandler(this.frmCustomerManagement_Load);
            this._buttonsPanel.ResumeLayout(false);
            this._buttonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _buttonsPanel;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnEditCustomer;
        private System.Windows.Forms.Button btnDeleteCustomer;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button _btnPreviousPage;
        private System.Windows.Forms.Button _btnNextPage;
        private System.Windows.Forms.Label _lblPageInfo;
        private System.Windows.Forms.Label _lblEmptyState;
        private System.Windows.Forms.DataGridView DataGVCustomers;
    }
}
