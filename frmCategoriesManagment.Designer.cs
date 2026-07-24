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
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnUpdateCategory = new System.Windows.Forms.Button();
            this.btnBackToPrevPage = new System.Windows.Forms.Button();
            this.DataGVCategories = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.labelSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblEmptyState = new System.Windows.Forms.Label();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).BeginInit();
            this.SuspendLayout();
            //
            // btnAddCategory
            //
            this.btnAddCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.Location = new System.Drawing.Point(720, 132);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(220, 70);
            this.btnAddCategory.TabIndex = 5;
            this.btnAddCategory.Text = "Add Category";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            //
            // btnDeleteCategory
            //
            this.btnDeleteCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCategory.Location = new System.Drawing.Point(720, 388);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(220, 70);
            this.btnDeleteCategory.TabIndex = 7;
            this.btnDeleteCategory.Text = "Delete Category";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            //
            // btnUpdateCategory
            //
            this.btnUpdateCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCategory.Location = new System.Drawing.Point(720, 260);
            this.btnUpdateCategory.Name = "btnUpdateCategory";
            this.btnUpdateCategory.Size = new System.Drawing.Size(220, 70);
            this.btnUpdateCategory.TabIndex = 6;
            this.btnUpdateCategory.Text = "Update Category";
            this.btnUpdateCategory.UseVisualStyleBackColor = true;
            this.btnUpdateCategory.Click += new System.EventHandler(this.btnUpdateCategory_Click);
            //
            // btnBackToPrevPage
            //
            this.btnBackToPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToPrevPage.Location = new System.Drawing.Point(820, 23);
            this.btnBackToPrevPage.Name = "btnBackToPrevPage";
            this.btnBackToPrevPage.Size = new System.Drawing.Size(120, 35);
            this.btnBackToPrevPage.TabIndex = 8;
            this.btnBackToPrevPage.Text = "Back";
            this.btnBackToPrevPage.UseVisualStyleBackColor = true;
            this.btnBackToPrevPage.Click += new System.EventHandler(this.btnBackToPrevPage_Click);
            //
            // DataGVCategories
            //
            this.DataGVCategories.AllowUserToAddRows = false;
            this.DataGVCategories.AllowUserToDeleteRows = false;
            this.DataGVCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVCategories.Location = new System.Drawing.Point(70, 93);
            this.DataGVCategories.Name = "DataGVCategories";
            this.DataGVCategories.ReadOnly = true;
            this.DataGVCategories.RowHeadersWidth = 51;
            this.DataGVCategories.RowTemplate.Height = 24;
            this.DataGVCategories.Size = new System.Drawing.Size(590, 415);
            this.DataGVCategories.TabIndex = 4;
            this.DataGVCategories.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            //
            // btnRefresh
            //
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(535, 23);
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
            this.labelSearch.Location = new System.Drawing.Point(70, 31);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(63, 20);
            this.labelSearch.TabIndex = 9;
            this.labelSearch.Text = "Search";
            //
            // txtSearch
            //
            this.txtSearch.Location = new System.Drawing.Point(145, 25);
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
            this.lblEmptyState.Text = "No categories found.";
            this.lblEmptyState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnPreviousPage
            //
            this.btnPreviousPage.Location = new System.Drawing.Point(70, 527);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(110, 35);
            this.btnPreviousPage.TabIndex = 1;
            this.btnPreviousPage.Text = "Previous";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            //
            // btnNextPage
            //
            this.btnNextPage.Location = new System.Drawing.Point(550, 527);
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
            this.lblPageInfo.Location = new System.Drawing.Point(210, 531);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(300, 25);
            this.lblPageInfo.TabIndex = 11;
            this.lblPageInfo.Text = "No results";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // frmCategoriesManagment
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 614);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.lblEmptyState);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.DataGVCategories);
            this.Controls.Add(this.btnBackToPrevPage);
            this.Controls.Add(this.btnUpdateCategory);
            this.Controls.Add(this.btnDeleteCategory);
            this.Controls.Add(this.btnAddCategory);
            this.Name = "frmCategoriesManagment";
            this.Text = "Category Management";
            this.Load += new System.EventHandler(this.frmCategoriesManagment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.Button btnUpdateCategory;
        private System.Windows.Forms.Button btnBackToPrevPage;
        private System.Windows.Forms.DataGridView DataGVCategories;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblEmptyState;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPageInfo;
    }
}
