namespace InventoryManagementSystem
{
    partial class frmShowSupplierToUpdate
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
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxNewSupplierName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSupplierID = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxNewPhone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxNewEmail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Supplier:";
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.BackColor = System.Drawing.Color.White;
            this.lblSupplierName.Location = new System.Drawing.Point(274, 62);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(33, 36);
            this.lblSupplierName.TabIndex = 1;
            this.lblSupplierName.Text = "?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Name :";
            // 
            // txtBoxNewSupplierName
            // 
            this.txtBoxNewSupplierName.Location = new System.Drawing.Point(231, 200);
            this.txtBoxNewSupplierName.Name = "txtBoxNewSupplierName";
            this.txtBoxNewSupplierName.Size = new System.Drawing.Size(188, 26);
            this.txtBoxNewSupplierName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Supplier ID:";
            // 
            // lblSupplierID
            // 
            this.lblSupplierID.AutoSize = true;
            this.lblSupplierID.BackColor = System.Drawing.Color.White;
            this.lblSupplierID.Location = new System.Drawing.Point(274, 127);
            this.lblSupplierID.Name = "lblSupplierID";
            this.lblSupplierID.Size = new System.Drawing.Size(33, 36);
            this.lblSupplierID.TabIndex = 4;
            this.lblSupplierID.Text = "?";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(159, 400);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(131, 53);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 32);
            this.label4.TabIndex = 5;
            this.label4.Text = "New Phone:";
            // 
            // txtBoxNewPhone
            // 
            this.txtBoxNewPhone.Location = new System.Drawing.Point(231, 260);
            this.txtBoxNewPhone.Name = "txtBoxNewPhone";
            this.txtBoxNewPhone.Size = new System.Drawing.Size(188, 26);
            this.txtBoxNewPhone.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 320);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 32);
            this.label5.TabIndex = 7;
            this.label5.Text = "New Email:";
            // 
            // txtBoxNewEmail
            // 
            this.txtBoxNewEmail.Location = new System.Drawing.Point(231, 320);
            this.txtBoxNewEmail.Name = "txtBoxNewEmail";
            this.txtBoxNewEmail.Size = new System.Drawing.Size(188, 26);
            this.txtBoxNewEmail.TabIndex = 2;
            // 
            // frmShowSupplierToUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 508);
            this.Controls.Add(this.txtBoxNewEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBoxNewPhone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblSupplierID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxNewSupplierName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSupplierName);
            this.Controls.Add(this.label1);
            this.Name = "frmShowSupplierToUpdate";
            this.Text = "Update Supplier";
            this.Load += new System.EventHandler(this.frmShowSupplierToUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxNewSupplierName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSupplierID;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxNewPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxNewEmail;
    }
}
