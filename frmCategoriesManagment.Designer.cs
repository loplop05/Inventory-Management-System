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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnBackToPrevPage = new System.Windows.Forms.Button();
            this.DataGVCategories = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(985, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 82);
            this.button1.TabIndex = 0;
            this.button1.Text = "ADD CATEGORY";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(985, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(219, 82);
            this.button2.TabIndex = 1;
            this.button2.Text = "DELETE CATEGORY";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(985, 428);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(219, 82);
            this.button3.TabIndex = 2;
            this.button3.Text = "UPDATE CATEGORY";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnBackToPrevPage
            // 
            this.btnBackToPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToPrevPage.Location = new System.Drawing.Point(1056, 21);
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
            this.DataGVCategories.Location = new System.Drawing.Point(101, 101);
            this.DataGVCategories.Name = "DataGVCategories";
            this.DataGVCategories.ReadOnly = true;
            this.DataGVCategories.RowHeadersWidth = 51;
            this.DataGVCategories.RowTemplate.Height = 24;
            this.DataGVCategories.Size = new System.Drawing.Size(317, 447);
            this.DataGVCategories.TabIndex = 4;
            this.DataGVCategories.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // frmCategoriesManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 614);
            this.Controls.Add(this.DataGVCategories);
            this.Controls.Add(this.btnBackToPrevPage);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmCategoriesManagment";
            this.Text = "frmCategoriesManagment";
            this.Load += new System.EventHandler(this.frmCategoriesManagment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGVCategories)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnBackToPrevPage;
        private System.Windows.Forms.DataGridView DataGVCategories;
    }
}