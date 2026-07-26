namespace InventoryManagementSystem
{
    partial class frmUpdateCategory
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUpdateCategoryid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Update  Category";
            // 
            // txtUpdateCategoryid
            // 
            this.txtUpdateCategoryid.AcceptsReturn = true;
            this.txtUpdateCategoryid.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateCategoryid.Location = new System.Drawing.Point(178, 275);
            this.txtUpdateCategoryid.Name = "txtUpdateCategoryid";
            this.txtUpdateCategoryid.Size = new System.Drawing.Size(148, 38);
            this.txtUpdateCategoryid.TabIndex = 0;
            this.txtUpdateCategoryid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUpdateCategoryid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUpdateCategoryid_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(131, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enter CategoryID";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(189, 360);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(127, 42);
            this.btnSearch.TabIndex = 1;
                        this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // 
            // frmUpdateCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 590);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUpdateCategoryid);
            this.Controls.Add(this.label1);
            this.Name = "frmUpdateCategory";
            this.Text = "Find Category to Update";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUpdateCategoryid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
    }
}