namespace InventoryManagementSystem
{
    partial class frmDeleteCategory
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtCategoryID = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(83, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Delete Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(74, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter Category ID";
            // 
            // txtCategoryID
            // 
            this.txtCategoryID.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtCategoryID.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategoryID.Location = new System.Drawing.Point(138, 244);
            this.txtCategoryID.Name = "txtCategoryID";
            this.txtCategoryID.Size = new System.Drawing.Size(110, 38);
            this.txtCategoryID.TabIndex = 0;
            this.txtCategoryID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCategoryID.UseWaitCursor = false;
            this.txtCategoryID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryID_KeyDown);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(138, 304);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnDelete.Size = new System.Drawing.Size(117, 35);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmDeleteCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(398, 448);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtCategoryID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmDeleteCategory";
            this.Text = "Delete Category";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCategoryID;
        private System.Windows.Forms.Button btnDelete;
    }
}