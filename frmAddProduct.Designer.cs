namespace InventoryManagementSystem
{
    partial class frmAddProduct
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
            this.txtBoxProductName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxQuantity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxBarcode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(180, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Product";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Product Name:";
            // 
            // txtBoxProductName
            // 
            this.txtBoxProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxProductName.Location = new System.Drawing.Point(200, 79);
            this.txtBoxProductName.Name = "txtBoxProductName";
            this.txtBoxProductName.Size = new System.Drawing.Size(200, 26);
            this.txtBoxProductName.TabIndex = 0;
            this.txtBoxProductName.TextChanged += new System.EventHandler(this.txtBoxProductName_TextChanged);
            this.txtBoxProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxProductName_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(190, 450);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(162, 54);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Price:";
            // 
            // txtBoxPrice
            // 
            this.txtBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPrice.Location = new System.Drawing.Point(200, 119);
            this.txtBoxPrice.Name = "txtBoxPrice";
            this.txtBoxPrice.Size = new System.Drawing.Size(200, 26);
            this.txtBoxPrice.TabIndex = 1;
            this.txtBoxPrice.TextChanged += new System.EventHandler(this.txtBoxPrice_TextChanged);
            this.txtBoxPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxPrice_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Quantity:";
            // 
            // txtBoxQuantity
            // 
            this.txtBoxQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxQuantity.Location = new System.Drawing.Point(200, 159);
            this.txtBoxQuantity.Name = "txtBoxQuantity";
            this.txtBoxQuantity.Size = new System.Drawing.Size(200, 26);
            this.txtBoxQuantity.TabIndex = 2;
            this.txtBoxQuantity.TextChanged += new System.EventHandler(this.txtBoxQuantity_TextChanged);
            this.txtBoxQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxQuantity_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "Barcode:";
            // 
            // txtBoxBarcode
            // 
            this.txtBoxBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxBarcode.Location = new System.Drawing.Point(200, 199);
            this.txtBoxBarcode.Name = "txtBoxBarcode";
            this.txtBoxBarcode.Size = new System.Drawing.Size(200, 26);
            this.txtBoxBarcode.TabIndex = 3;
            this.txtBoxBarcode.TextChanged += new System.EventHandler(this.txtBoxBarcode_TextChanged);
            this.txtBoxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxBarcode_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(200, 239);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 28);
            this.cmbCategory.TabIndex = 4;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            this.cmbCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategory_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "Supplier:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(200, 279);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(200, 28);
            this.cmbSupplier.TabIndex = 5;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            this.cmbSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplier_KeyDown);
            // 
            // frmAddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 550);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBoxBarcode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBoxQuantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBoxPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtBoxProductName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmAddProduct";
            this.Text = "Add Product";
            this.Load += new System.EventHandler(this.frmAddProduct_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxProductName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxQuantity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxBarcode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSupplier;
    }
}
