namespace InventoryManagementSystem
{
    partial class frmManualDiscount
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

        private void InitializeComponent()
        {
            this.lblDiscountType = new System.Windows.Forms.Label();
            this.rbPercentage = new System.Windows.Forms.RadioButton();
            this.rbFixedAmount = new System.Windows.Forms.RadioButton();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.txtDiscountValue = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDiscountType
            // 
            this.lblDiscountType.AutoSize = true;
            this.lblDiscountType.Location = new System.Drawing.Point(30, 100);
            this.lblDiscountType.Name = "lblDiscountType";
            this.lblDiscountType.Size = new System.Drawing.Size(90, 20);
            this.lblDiscountType.TabIndex = 0;
            this.lblDiscountType.Text = "Discount Type:";
            // 
            // rbPercentage
            // 
            this.rbPercentage.AutoSize = true;
            this.rbPercentage.Location = new System.Drawing.Point(150, 97);
            this.rbPercentage.Name = "rbPercentage";
            this.rbPercentage.Size = new System.Drawing.Size(100, 24);
            this.rbPercentage.TabIndex = 1;
            this.rbPercentage.TabStop = true;
            this.rbPercentage.Text = "Percentage";
            this.rbPercentage.UseVisualStyleBackColor = true;
            // 
            // rbFixedAmount
            // 
            this.rbFixedAmount.AutoSize = true;
            this.rbFixedAmount.Location = new System.Drawing.Point(270, 97);
            this.rbFixedAmount.Name = "rbFixedAmount";
            this.rbFixedAmount.Size = new System.Drawing.Size(110, 24);
            this.rbFixedAmount.TabIndex = 2;
            this.rbFixedAmount.TabStop = true;
            this.rbFixedAmount.Text = "Fixed Amount";
            this.rbFixedAmount.UseVisualStyleBackColor = true;
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.AutoSize = true;
            this.lblDiscountValue.Location = new System.Drawing.Point(30, 150);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(100, 20);
            this.lblDiscountValue.TabIndex = 3;
            this.lblDiscountValue.Text = "Discount Value:";
            // 
            // txtDiscountValue
            // 
            this.txtDiscountValue.Location = new System.Drawing.Point(150, 147);
            this.txtDiscountValue.MaxLength = 10;
            this.txtDiscountValue.Name = "txtDiscountValue";
            this.txtDiscountValue.Size = new System.Drawing.Size(150, 27);
            this.txtDiscountValue.TabIndex = 4;
            this.txtDiscountValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountValue_KeyPress);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(230, 220);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 38);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(340, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmManualDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 280);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtDiscountValue);
            this.Controls.Add(this.lblDiscountValue);
            this.Controls.Add(this.rbFixedAmount);
            this.Controls.Add(this.rbPercentage);
            this.Controls.Add(this.lblDiscountType);
            this.KeyPreview = true;
            this.Name = "frmManualDiscount";
            this.Text = "Manual Discount";
            this.Load += new System.EventHandler(this.frmManualDiscount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblDiscountType;
        private System.Windows.Forms.RadioButton rbPercentage;
        private System.Windows.Forms.RadioButton rbFixedAmount;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.TextBox txtDiscountValue;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}
