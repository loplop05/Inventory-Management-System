namespace InventoryManagementSystem
{
    partial class frmUpdateProduct
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
            this._contentPanel = new System.Windows.Forms.Panel();
            this._searchPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUpdateProductID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._picPreview = new System.Windows.Forms.PictureBox();
            this._buttonPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this._contentPanel.SuspendLayout();
            this._searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview)).BeginInit();
            this._buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._searchPanel);
            this._contentPanel.Controls.Add(this._buttonPanel);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(500, 350);
            this._contentPanel.TabIndex = 1;
            // 
            // _searchPanel
            // 
            this._searchPanel.ColumnCount = 2;
            this._searchPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this._searchPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._searchPanel.Controls.Add(this.label2, 0, 0);
            this._searchPanel.Controls.Add(this.label3, 0, 1);
            this._searchPanel.Controls.Add(this.txtUpdateProductID, 1, 1);
            this._searchPanel.Controls.Add(this.label4, 0, 2);
            this._searchPanel.Controls.Add(this._picPreview, 1, 2);
            this._searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchPanel.Location = new System.Drawing.Point(0, 0);
            this._searchPanel.Margin = new System.Windows.Forms.Padding(20);
            this._searchPanel.Name = "_searchPanel";
            this._searchPanel.RowCount = 3;
            this._searchPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._searchPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._searchPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._searchPanel.Size = new System.Drawing.Size(500, 260);
            this._searchPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Enter Product ID or Barcode";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(3, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Product ID/Barcode:";
            // 
            // txtUpdateProductID
            // 
            this.txtUpdateProductID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateProductID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUpdateProductID.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUpdateProductID.Location = new System.Drawing.Point(183, 33);
            this.txtUpdateProductID.Name = "txtUpdateProductID";
            this.txtUpdateProductID.Size = new System.Drawing.Size(314, 34);
            this.txtUpdateProductID.TabIndex = 0;
            this.txtUpdateProductID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUpdateProductID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUpdateProductID_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(3, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Preview:";
            // 
            // _picPreview
            // 
            this._picPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this._picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._picPreview.Location = new System.Drawing.Point(183, 83);
            this._picPreview.Name = "_picPreview";
            this._picPreview.Size = new System.Drawing.Size(314, 174);
            this._picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._picPreview.TabIndex = 3;
            this._picPreview.TabStop = false;
            // 
            // _buttonPanel
            // 
            this._buttonPanel.Controls.Add(this.btnCancel);
            this._buttonPanel.Controls.Add(this.btnSearch);
            this._buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonPanel.Location = new System.Drawing.Point(0, 260);
            this._buttonPanel.Name = "_buttonPanel";
            this._buttonPanel.Size = new System.Drawing.Size(500, 90);
            this._buttonPanel.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(160, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(300, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 40);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Find Product";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmUpdateProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this._contentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Product";
            this._contentPanel.ResumeLayout(false);
            this._searchPanel.ResumeLayout(false);
            this._searchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview)).EndInit();
            this._buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.TableLayoutPanel _searchPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUpdateProductID;
        private System.Windows.Forms.PictureBox _picPreview;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel _buttonPanel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCancel;
    }
}
