namespace InventoryManagementSystem
{
    partial class frmAddSupplier
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
            this.txtBoxSupplierName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxEmail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Supplier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Supplier Name:";
            // 
            // txtBoxSupplierName
            // 
            this.txtBoxSupplierName.AcceptsReturn = true;
            this.txtBoxSupplierName.AllowDrop = true;
            this.txtBoxSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxSupplierName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxSupplierName.Location = new System.Drawing.Point(200, 109);
            this.txtBoxSupplierName.Name = "txtBoxSupplierName";
            this.txtBoxSupplierName.Size = new System.Drawing.Size(200, 26);
            this.txtBoxSupplierName.TabIndex = 0;
            this.txtBoxSupplierName.TextChanged += new System.EventHandler(this.txtBoxSupplierName_TextChanged);
            this.txtBoxSupplierName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxSupplierName_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(140, 320);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(162, 54);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Phone:";
            // 
            // txtBoxPhone
            // 
            this.txtBoxPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPhone.Location = new System.Drawing.Point(200, 169);
            this.txtBoxPhone.Name = "txtBoxPhone";
            this.txtBoxPhone.Size = new System.Drawing.Size(200, 26);
            this.txtBoxPhone.TabIndex = 1;
            this.txtBoxPhone.TextChanged += new System.EventHandler(this.txtBoxPhone_TextChanged);
            this.txtBoxPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxPhone_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Email:";
            // 
            // txtBoxEmail
            // 
            this.txtBoxEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxEmail.Location = new System.Drawing.Point(200, 229);
            this.txtBoxEmail.Name = "txtBoxEmail";
            this.txtBoxEmail.Size = new System.Drawing.Size(200, 26);
            this.txtBoxEmail.TabIndex = 2;
            this.txtBoxEmail.TextChanged += new System.EventHandler(this.txtBoxEmail_TextChanged);
            this.txtBoxEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxEmail_KeyDown);
            // 
            // frmAddSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 450);
            this.Controls.Add(this.txtBoxEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBoxPhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtBoxSupplierName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmAddSupplier";
            this.Text = "Add Supplier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxSupplierName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxPhone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxEmail;
    }
}
