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
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.Location = new System.Drawing.Point(761, 187);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(219, 82);
            this.btnAddCategory.TabIndex = 0;
            this.btnAddCategory.Text = "ADD CATEGORY";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCategory.Location = new System.Drawing.Point(761, 311);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(219, 82);
            this.btnDeleteCategory.TabIndex = 1;
            this.btnDeleteCategory.Text = "DELETE CATEGORY";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            // 
            // btnUpdateCategory
            // 
            this.btnUpdateCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCategory.Location = new System.Drawing.Point(761, 442);
            this.btnUpdateCategory.Name = "btnUpdateCategory";
            this.btnUpdateCategory.Size = new System.Drawing.Size(219, 82);
            this.btnUpdateCategory.TabIndex = 2;
            this.btnUpdateCategory.Text = "UPDATE CATEGORY";
            this.btnUpdateCategory.UseVisualStyleBackColor = true;
            // 
            // btnBackToPrevPage
            // 
            this.btnBackToPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToPrevPage.Location = new System.Drawing.Point(778, 35);
            this.btnBackToPrevPage.Name = "btnBackToPrevPage";
            this.btnBackToPrevPage.Size = new System.Drawing.Size(148, 37);
            this.btnBackToPrevPage.TabIndex = 3;
            this.btnBackToPrevPage.Text = "Back";
            this.btnBackToPrevPage.UseVisualStyleBackColor = true;
            this.btnBackToPrevPage.Click += new System.EventHandler(this.btnBackToPrevPage_Click);
            // 
            // DataGVCategories
            // 
            this.DataGVCategories.AllowUserToAddRows = false;
            this.DataGVCategories.AllowUserToDeleteRows = false;
            this.DataGVCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVCategories.Location = new System.Drawing.Point(95, 97);
            this.DataGVCategories.Name = "DataGVCategories";
            this.DataGVCategories.ReadOnly = true;
            this.DataGVCategories.RowHeadersWidth = 51;
            this.DataGVCategories.RowTemplate.Height = 24;
            this.DataGVCategories.Size = new System.Drawing.Size(345, 451);
            this.DataGVCategories.TabIndex = 4;
            this.DataGVCategories.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(95, 54);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(148, 37);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmCategoriesManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 614);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.DataGVCategories);
            this.Controls.Add(this.btnBackToPrevPage);
            this.Controls.Add(this.btnUpdateCategory);
            this.Controls.Add(this.btnDeleteCategory);
            this.Controls.Add(this.btnAddCategory);
            this.Name = "frmCategoriesManagment";
            this.Text = "frmCategoriesManagment";
            this.Load += new System.EventHandler(this.frmCategoriesManagment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.Button btnUpdateCategory;
        private System.Windows.Forms.Button btnBackToPrevPage;
        private System.Windows.Forms.DataGridView DataGVCategories;
        private System.Windows.Forms.Button btnRefresh;
    }
}