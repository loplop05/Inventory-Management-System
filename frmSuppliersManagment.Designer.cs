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
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.btnDeleteSupplier = new System.Windows.Forms.Button();
            this.btnUpdateSupplier = new System.Windows.Forms.Button();
            this.btnBackToPrevPage = new System.Windows.Forms.Button();
            this.DataGVSuppliers = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.labelSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblEmptyState = new System.Windows.Forms.Label();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVSuppliers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSupplier.Location = new System.Drawing.Point(720, 196);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(220, 70);
            this.btnAddSupplier.TabIndex = 5;
            this.btnAddSupplier.Text = "Add Supplier";
            this.btnAddSupplier.UseVisualStyleBackColor = true;
            this.btnAddSupplier.Click += new System.EventHandler(this.btnAddSupplier_Click);
            // 
            // btnDeleteSupplier
            // 
            this.btnDeleteSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSupplier.Location = new System.Drawing.Point(720, 452);
            this.btnDeleteSupplier.Name = "btnDeleteSupplier";
            this.btnDeleteSupplier.Size = new System.Drawing.Size(220, 70);
            this.btnDeleteSupplier.TabIndex = 7;
            this.btnDeleteSupplier.Text = "Delete Supplier";
            this.btnDeleteSupplier.UseVisualStyleBackColor = true;
            this.btnDeleteSupplier.Click += new System.EventHandler(this.btnDeleteSupplier_Click);
            // 
            // btnUpdateSupplier
            // 
            this.btnUpdateSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateSupplier.Location = new System.Drawing.Point(720, 324);
            this.btnUpdateSupplier.Name = "btnUpdateSupplier";
            this.btnUpdateSupplier.Size = new System.Drawing.Size(220, 70);
            this.btnUpdateSupplier.TabIndex = 6;
            this.btnUpdateSupplier.Text = "Update Supplier";
            this.btnUpdateSupplier.UseVisualStyleBackColor = true;
            this.btnUpdateSupplier.Click += new System.EventHandler(this.btnUpdateSupplier_Click);
            // 
            // btnBackToPrevPage
            // 
            this.btnBackToPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToPrevPage.Location = new System.Drawing.Point(820, 87);
            this.btnBackToPrevPage.Name = "btnBackToPrevPage";
            this.btnBackToPrevPage.Size = new System.Drawing.Size(120, 35);
            this.btnBackToPrevPage.TabIndex = 8;
            this.btnBackToPrevPage.Text = "Back";
            this.btnBackToPrevPage.UseVisualStyleBackColor = true;
            this.btnBackToPrevPage.Click += new System.EventHandler(this.btnBackToPrevPage_Click);
            // 
            // DataGVSuppliers
            // 
            this.DataGVSuppliers.AllowUserToAddRows = false;
            this.DataGVSuppliers.AllowUserToDeleteRows = false;
            this.DataGVSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVSuppliers.Location = new System.Drawing.Point(70, 157);
            this.DataGVSuppliers.Name = "DataGVSuppliers";
            this.DataGVSuppliers.ReadOnly = true;
            this.DataGVSuppliers.RowHeadersWidth = 51;
            this.DataGVSuppliers.RowTemplate.Height = 24;
            this.DataGVSuppliers.Size = new System.Drawing.Size(590, 415);
            this.DataGVSuppliers.TabIndex = 4;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(535, 87);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(125, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh (F5)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSearch.Location = new System.Drawing.Point(70, 95);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(63, 20);
            this.labelSearch.TabIndex = 9;
            this.labelSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(145, 89);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(365, 26);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblEmptyState
            // 
            this.lblEmptyState.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmptyState.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmptyState.Location = new System.Drawing.Point(116, 275);
            this.lblEmptyState.Name = "lblEmptyState";
            this.lblEmptyState.Size = new System.Drawing.Size(500, 40);
            this.lblEmptyState.TabIndex = 10;
            this.lblEmptyState.Text = "No suppliers found.";
            this.lblEmptyState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(70, 591);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(110, 35);
            this.btnPreviousPage.TabIndex = 1;
            this.btnPreviousPage.Text = "Previous";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(550, 591);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(110, 35);
            this.btnNextPage.TabIndex = 2;
            this.btnNextPage.Text = "Next";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfo.Location = new System.Drawing.Point(210, 595);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(300, 25);
            this.lblPageInfo.TabIndex = 11;
            this.lblPageInfo.Text = "No results";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSuppliersManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 678);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.lblEmptyState);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.DataGVSuppliers);
            this.Controls.Add(this.btnBackToPrevPage);
            this.Controls.Add(this.btnUpdateSupplier);
            this.Controls.Add(this.btnDeleteSupplier);
            this.Controls.Add(this.btnAddSupplier);
            this.Name = "frmSuppliersManagment";
            this.Text = "Supplier Management";
            this.Load += new System.EventHandler(this.frmSuppliersManagment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGVSuppliers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddSupplier;
        private System.Windows.Forms.Button btnDeleteSupplier;
        private System.Windows.Forms.Button btnUpdateSupplier;
        private System.Windows.Forms.Button btnBackToPrevPage;
        private System.Windows.Forms.DataGridView DataGVSuppliers;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblEmptyState;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPageInfo;
    }
}
