namespace InventoryManagementSystem
{
    partial class frmShowProductToUpdate
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProductID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxNewProductName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxNewPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxNewQuantity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoxNewBarcode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbNewCategory = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbNewSupplier = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this._picPreview = new System.Windows.Forms.PictureBox();
            this._btnBrowseImage = new System.Windows.Forms.Button();
            this._buttonPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this._headerPanel.SuspendLayout();
            this._contentPanel.SuspendLayout();
            this._fieldsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview)).BeginInit();
            this._buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _headerPanel
            // 
            this._headerPanel.Controls.Add(this._lblPageTitle);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(500, 60);
            this._headerPanel.TabIndex = 0;
            // 
            // _lblPageTitle
            // 
            this._lblPageTitle.AutoSize = true;
            this._lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblPageTitle.Location = new System.Drawing.Point(20, 15);
            this._lblPageTitle.Name = "_lblPageTitle";
            this._lblPageTitle.Size = new System.Drawing.Size(241, 41);
            this._lblPageTitle.TabIndex = 0;
            this._lblPageTitle.Text = "Update Product";
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._fieldsPanel);
            this._contentPanel.Controls.Add(this._buttonPanel);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 60);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(500, 660);
            this._contentPanel.TabIndex = 1;
            // 
            // _fieldsPanel
            // 
            this._fieldsPanel.ColumnCount = 2;
            this._fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._fieldsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._fieldsPanel.Controls.Add(this.label1, 0, 0);
            this._fieldsPanel.Controls.Add(this.lblProductName, 1, 0);
            this._fieldsPanel.Controls.Add(this.label3, 0, 1);
            this._fieldsPanel.Controls.Add(this.lblProductID, 1, 1);
            this._fieldsPanel.Controls.Add(this.label2, 0, 2);
            this._fieldsPanel.Controls.Add(this.txtBoxNewProductName, 1, 2);
            this._fieldsPanel.Controls.Add(this.label4, 0, 3);
            this._fieldsPanel.Controls.Add(this.txtBoxNewPrice, 1, 3);
            this._fieldsPanel.Controls.Add(this.label5, 0, 4);
            this._fieldsPanel.Controls.Add(this.txtBoxNewQuantity, 1, 4);
            this._fieldsPanel.Controls.Add(this.label6, 0, 5);
            this._fieldsPanel.Controls.Add(this.txtBoxNewBarcode, 1, 5);
            this._fieldsPanel.Controls.Add(this.label7, 0, 6);
            this._fieldsPanel.Controls.Add(this.cmbNewCategory, 1, 6);
            this._fieldsPanel.Controls.Add(this.label8, 0, 7);
            this._fieldsPanel.Controls.Add(this.cmbNewSupplier, 1, 7);
            this._fieldsPanel.Controls.Add(this.label9, 0, 8);
            this._fieldsPanel.Controls.Add(this._picPreview, 1, 8);
            this._fieldsPanel.Controls.Add(this._btnBrowseImage, 1, 9);
            this._fieldsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fieldsPanel.Location = new System.Drawing.Point(0, 0);
            this._fieldsPanel.Margin = new System.Windows.Forms.Padding(30);
            this._fieldsPanel.Name = "_fieldsPanel";
            this._fieldsPanel.RowCount = 10;
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this._fieldsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._fieldsPanel.Size = new System.Drawing.Size(500, 600);
            this._fieldsPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product:";
            // 
            // lblProductName
            // 
            this.lblProductName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductName.Location = new System.Drawing.Point(153, 6);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(21, 28);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "?";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Product ID:";
            // 
            // lblProductID
            // 
            this.lblProductID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProductID.AutoSize = true;
            this.lblProductID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductID.Location = new System.Drawing.Point(153, 46);
            this.lblProductID.Name = "lblProductID";
            this.lblProductID.Size = new System.Drawing.Size(21, 28);
            this.lblProductID.TabIndex = 4;
            this.lblProductID.Text = "?";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(3, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Name:";
            // 
            // txtBoxNewProductName
            // 
            this.txtBoxNewProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxNewProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxNewProductName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBoxNewProductName.Location = new System.Drawing.Point(153, 83);
            this.txtBoxNewProductName.Name = "txtBoxNewProductName";
            this.txtBoxNewProductName.Size = new System.Drawing.Size(344, 30);
            this.txtBoxNewProductName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(3, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "New Price:";
            // 
            // txtBoxNewPrice
            // 
            this.txtBoxNewPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxNewPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxNewPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBoxNewPrice.Location = new System.Drawing.Point(153, 128);
            this.txtBoxNewPrice.Name = "txtBoxNewPrice";
            this.txtBoxNewPrice.Size = new System.Drawing.Size(344, 30);
            this.txtBoxNewPrice.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(3, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "New Quantity:";
            // 
            // txtBoxNewQuantity
            // 
            this.txtBoxNewQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxNewQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxNewQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBoxNewQuantity.Location = new System.Drawing.Point(153, 173);
            this.txtBoxNewQuantity.Name = "txtBoxNewQuantity";
            this.txtBoxNewQuantity.Size = new System.Drawing.Size(344, 30);
            this.txtBoxNewQuantity.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.Location = new System.Drawing.Point(3, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 23);
            this.label6.TabIndex = 9;
            this.label6.Text = "New Barcode:";
            // 
            // txtBoxNewBarcode
            // 
            this.txtBoxNewBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxNewBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBoxNewBarcode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBoxNewBarcode.Location = new System.Drawing.Point(153, 218);
            this.txtBoxNewBarcode.Name = "txtBoxNewBarcode";
            this.txtBoxNewBarcode.Size = new System.Drawing.Size(344, 30);
            this.txtBoxNewBarcode.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(3, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 23);
            this.label7.TabIndex = 11;
            this.label7.Text = "New Category:";
            // 
            // cmbNewCategory
            // 
            this.cmbNewCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbNewCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNewCategory.FormattingEnabled = true;
            this.cmbNewCategory.Location = new System.Drawing.Point(153, 263);
            this.cmbNewCategory.Name = "cmbNewCategory";
            this.cmbNewCategory.Size = new System.Drawing.Size(344, 31);
            this.cmbNewCategory.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(3, 314);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 23);
            this.label8.TabIndex = 13;
            this.label8.Text = "New Supplier:";
            // 
            // cmbNewSupplier
            // 
            this.cmbNewSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbNewSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNewSupplier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNewSupplier.FormattingEnabled = true;
            this.cmbNewSupplier.Location = new System.Drawing.Point(153, 308);
            this.cmbNewSupplier.Name = "cmbNewSupplier";
            this.cmbNewSupplier.Size = new System.Drawing.Size(344, 31);
            this.cmbNewSupplier.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 402);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Image:";
            // 
            // _picPreview
            // 
            this._picPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this._picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._picPreview.Location = new System.Drawing.Point(153, 349);
            this._picPreview.Name = "_picPreview";
            this._picPreview.Size = new System.Drawing.Size(344, 144);
            this._picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._picPreview.TabIndex = 15;
            this._picPreview.TabStop = false;
            // 
            // _btnBrowseImage
            // 
            this._btnBrowseImage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._btnBrowseImage.Location = new System.Drawing.Point(153, 497);
            this._btnBrowseImage.Name = "_btnBrowseImage";
            this._btnBrowseImage.Size = new System.Drawing.Size(180, 65);
            this._btnBrowseImage.TabIndex = 16;
            this._btnBrowseImage.Text = "Choose Image";
            this._btnBrowseImage.UseVisualStyleBackColor = true;
            this._btnBrowseImage.Click += new System.EventHandler(this._btnBrowseImage_Click);
            // 
            // _buttonPanel
            // 
            this._buttonPanel.Controls.Add(this.btnCancel);
            this._buttonPanel.Controls.Add(this.btnUpdate);
            this._buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonPanel.Location = new System.Drawing.Point(0, 600);
            this._buttonPanel.Name = "_buttonPanel";
            this._buttonPanel.Size = new System.Drawing.Size(500, 60);
            this._buttonPanel.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(220, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(360, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 40);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmShowProductToUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 720);
            this.Controls.Add(this._contentPanel);
            this.Controls.Add(this._headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowProductToUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Product";
            this.Load += new System.EventHandler(this.frmShowProductToUpdate_Load);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._contentPanel.ResumeLayout(false);
            this._fieldsPanel.ResumeLayout(false);
            this._fieldsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview)).EndInit();
            this._buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Label _lblPageTitle;
        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.TableLayoutPanel _fieldsPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProductID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxNewProductName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxNewPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxNewQuantity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxNewBarcode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbNewCategory;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbNewSupplier;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox _picPreview;
        private System.Windows.Forms.Button _btnBrowseImage;
        private System.Windows.Forms.Panel _buttonPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
    }
}
