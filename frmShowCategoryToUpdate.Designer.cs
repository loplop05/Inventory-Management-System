namespace InventoryManagementSystem
{
    partial class frmShowCategoryToUpdate
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
            this.lblCategory = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxNewCategory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category :";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.BackColor = System.Drawing.Color.Red;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(274, 62);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(33, 36);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Name :";
            // 
            // txtBoxNewCategory
            // 
            this.txtBoxNewCategory.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxNewCategory.Location = new System.Drawing.Point(231, 182);
            this.txtBoxNewCategory.Name = "txtBoxNewCategory";
            this.txtBoxNewCategory.Size = new System.Drawing.Size(188, 46);
            this.txtBoxNewCategory.TabIndex = 0;
            // 
            // frmShowCategoryToUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 508);
            this.Controls.Add(this.txtBoxNewCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.label1);
            this.Name = "frmShowCategoryToUpdate";
            this.Text = "frmShowCategoryToUpdate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxNewCategory;
    }
}