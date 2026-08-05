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
            this._headerPanel = new System.Windows.Forms.Panel();
            this._lblPageTitle = new System.Windows.Forms.Label();
            this._contentPanel = new System.Windows.Forms.Panel();
            this._fieldsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxProductName = new System.Windows.Forms.TextBox();
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
            this._buttonPanel = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._headerPanel.SuspendLayout();
            this._contentPanel.SuspendLayout();
            this._fieldsPanel.SuspendLayout();
            this._buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._lblPageTitle);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(600, 60);
            this._headerPanel.TabIndex = 0;
            // 
            // _lblPageTitle
            // 
            this._lblPageTitle.AutoSize = true;
            this._lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblPageTitle.Location = new System.Drawing.Point(20, 15);
            this._lblPageTitle.Name = "_lblPageTitle";
            this._lblPageTitle.Size = new System.Drawing.Size(127, 32);
            this._lblPageTitle.TabIndex = 0;
            this._lblPageTitle.Text = "Add Product";
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._fieldsPanel);
            this._contentPanel.Controls.Add(this._buttonPanel);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 60);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(600, 440);
            this._contentPanel.TabIndex = 1;
            // 
            // _fieldsPanel
            // 
            this._fieldsPanel.ColumnCount = 2;
            this._fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._fieldsPanel.Controls.Add(this.label2, 0, 0);
            this._fieldsPanel.Controls.Add(this.txtBoxProductName, 1, 0);
            this._fieldsPanel.Controls.Add(this.label3, 0, 1);
            this._fieldsPanel.Controls.Add(this.txtBoxPrice, 1, 1);
            this._fieldsPanel.Controls.Add(this.label4, 0, 2);
            this._fieldsPanel.Controls.Add(this.txtBoxQuantity, 1, 2);
            this._fieldsPanel.Controls.Add(this.label5, 0, 3);
            this._fieldsPanel.Controls.Add(this.txtBoxBarcode, 1, 3);
            this._fieldsPanel.Controls.Add(this.label6, 0, 4);
            this._fieldsPanel.Controls.Add(this.cmbCategory, 1, 4);
            this._fieldsPanel.Controls.Add(this.label7, 0, 5);
            this._fieldsPanel.Controls.Add(this.cmbSupplier, 1, 5);
            this._fieldsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fieldsPanel.Location = new System.Drawing.Point(0, 0);
            this._fieldsPanel.Margin = new System.Windows.Forms.Padding(20);
            this._fieldsPanel.Name = "_fieldsPanel";
            this._fieldsPanel.RowCount = 6;
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.Size = new System.Drawing.Size(600, 350);
            this._fieldsPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Product Name:";
            // 
            // txtBoxProductName
            // 
            this.txtBoxProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxProductName.Location = new System.Drawing.Point(123, 3);
            this.txtBoxProductName.Margin = new System.Windows.Forms.Padding(3);
            this.txtBoxProductName.Name = "txtBoxProductName";
            this.txtBoxProductName.Size = new System.Drawing.Size(404, 27);
            this.txtBoxProductName.TabIndex = 0;
            this.txtBoxProductName.TextChanged += new System.EventHandler(this.txtBoxProductName_TextChanged);
            this.txtBoxProductName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxProductName_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Price:";
            // 
            // txtBoxPrice
            // 
            this.txtBoxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxPrice.Location = new System.Drawing.Point(123, 43);
            this.txtBoxPrice.Margin = new System.Windows.Forms.Padding(3);
            this.txtBoxPrice.Name = "txtBoxPrice";
            this.txtBoxPrice.Size = new System.Drawing.Size(404, 27);
            this.txtBoxPrice.TabIndex = 1;
            this.txtBoxPrice.TextChanged += new System.EventHandler(this.txtBoxPrice_TextChanged);
            this.txtBoxPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxPrice_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Quantity:";
            // 
            // txtBoxQuantity
            // 
            this.txtBoxQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxQuantity.Location = new System.Drawing.Point(123, 83);
            this.txtBoxQuantity.Margin = new System.Windows.Forms.Padding(3);
            this.txtBoxQuantity.Name = "txtBoxQuantity";
            this.txtBoxQuantity.Size = new System.Drawing.Size(404, 27);
            this.txtBoxQuantity.TabIndex = 2;
            this.txtBoxQuantity.TextChanged += new System.EventHandler(this.txtBoxQuantity_TextChanged);
            this.txtBoxQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxQuantity_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Barcode:";
            // 
            // txtBoxBarcode
            // 
            this.txtBoxBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxBarcode.Location = new System.Drawing.Point(123, 123);
            this.txtBoxBarcode.Margin = new System.Windows.Forms.Padding(3);
            this.txtBoxBarcode.Name = "txtBoxBarcode";
            this.txtBoxBarcode.Size = new System.Drawing.Size(404, 27);
            this.txtBoxBarcode.TabIndex = 3;
            this.txtBoxBarcode.TextChanged += new System.EventHandler(this.txtBoxBarcode_TextChanged);
            this.txtBoxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxBarcode_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(123, 163);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(3);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(404, 28);
            this.cmbCategory.TabIndex = 4;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            this.cmbCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategory_KeyDown);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Supplier:";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(123, 203);
            this.cmbSupplier.Margin = new System.Windows.Forms.Padding(3);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(404, 28);
            this.cmbSupplier.TabIndex = 5;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            this.cmbSupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSupplier_KeyDown);
            // 
            // _buttonPanel
            // 
            this._buttonPanel.Controls.Add(this.btnCancel);
            this._buttonPanel.Controls.Add(this.btnAdd);
            this._buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonPanel.Location = new System.Drawing.Point(0, 350);
            this._buttonPanel.Name = "_buttonPanel";
            this._buttonPanel.Size = new System.Drawing.Size(600, 90);
            this._buttonPanel.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(400, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Product";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(260, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(this._contentPanel);
            this.Controls.Add(this._headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Product";
            this.Load += new System.EventHandler(this.frmAddProduct_Load);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._contentPanel.ResumeLayout(false);
            this._fieldsPanel.ResumeLayout(false);
            this._fieldsPanel.PerformLayout();
            this._buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _lblPageTitle;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.TableLayoutPanel _fieldsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxProductName;
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
        private System.Windows.Forms.Panel _buttonPanel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
    }
}
