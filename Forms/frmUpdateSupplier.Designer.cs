namespace InventoryManagementSystem
{
    partial class frmUpdateSupplier
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUpdateSupplierID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Update Supplier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enter Supplier ID";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.txtUpdateSupplierID, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 110);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(20, 0, 20, 80);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(518, 590);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Supplier ID:";
            // 
            // txtUpdateSupplierID
            // 
            this.txtUpdateSupplierID.AcceptsReturn = true;
            this.txtUpdateSupplierID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUpdateSupplierID.Location = new System.Drawing.Point(123, 3);
            this.txtUpdateSupplierID.Margin = new System.Windows.Forms.Padding(3);
            this.txtUpdateSupplierID.Name = "txtUpdateSupplierID";
            this.txtUpdateSupplierID.Size = new System.Drawing.Size(372, 27);
            this.txtUpdateSupplierID.TabIndex = 0;
            this.txtUpdateSupplierID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUpdateSupplierID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUpdateSupplierID_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(189, 360);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(127, 42);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmUpdateSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 590);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUpdateSupplier";
            this.Text = "Find Supplier to Update";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUpdateSupplierID;
        private System.Windows.Forms.Button btnSearch;
    }
}
