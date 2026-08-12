namespace Inventory1PresentationLayer
{
    partial class frmInventoryDashboard
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this._lowStockPanel = new System.Windows.Forms.Panel();
            this.gridLowStockProducts = new System.Windows.Forms.DataGridView();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this._associationsPanel = new System.Windows.Forms.Panel();
            this.gridAssociations = new System.Windows.Forms.DataGridView();
            this.lblAssociationsTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this._lowStockPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStockProducts)).BeginInit();
            this._associationsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAssociations)).BeginInit();
            this.SuspendLayout();
            // 
            // _lowStockPanel
            // 
            this._lowStockPanel.Controls.Add(this.gridLowStockProducts);
            this._lowStockPanel.Controls.Add(this.lblLowStockTitle);
            this._lowStockPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._lowStockPanel.Location = new System.Drawing.Point(0, 60);
            this._lowStockPanel.Name = "_lowStockPanel";
            this._lowStockPanel.Size = new System.Drawing.Size(800, 350);
            this._lowStockPanel.TabIndex = 0;
            // 
            // lblLowStockTitle
            // 
            this.lblLowStockTitle.AutoSize = true;
            this.lblLowStockTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLowStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLowStockTitle.Location = new System.Drawing.Point(10, 5);
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.Size = new System.Drawing.Size(133, 28);
            this.lblLowStockTitle.TabIndex = 0;
            this.lblLowStockTitle.Text = "Low Stock Products";
            // 
            // gridLowStockProducts
            // 
            this.gridLowStockProducts.AllowUserToAddRows = false;
            this.gridLowStockProducts.AllowUserToDeleteRows = false;
            this.gridLowStockProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLowStockProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLowStockProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLowStockProducts.Location = new System.Drawing.Point(0, 30);
            this.gridLowStockProducts.Name = "gridLowStockProducts";
            this.gridLowStockProducts.ReadOnly = true;
            this.gridLowStockProducts.RowHeadersVisible = false;
            this.gridLowStockProducts.RowHeadersWidth = 51;
            this.gridLowStockProducts.RowTemplate.Height = 24;
            this.gridLowStockProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLowStockProducts.Size = new System.Drawing.Size(800, 320);
            this.gridLowStockProducts.TabIndex = 1;
            // 
            // _associationsPanel
            // 
            this._associationsPanel.Controls.Add(this.gridAssociations);
            this._associationsPanel.Controls.Add(this.lblAssociationsTitle);
            this._associationsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._associationsPanel.Location = new System.Drawing.Point(0, 410);
            this._associationsPanel.Name = "_associationsPanel";
            this._associationsPanel.Padding = new System.Windows.Forms.Padding(20);
            this._associationsPanel.Size = new System.Drawing.Size(800, 390);
            this._associationsPanel.TabIndex = 1;
            // 
            // lblAssociationsTitle
            // 
            this.lblAssociationsTitle.AutoSize = true;
            this.lblAssociationsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAssociationsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAssociationsTitle.Location = new System.Drawing.Point(20, 20);
            this.lblAssociationsTitle.Name = "lblAssociationsTitle";
            this.lblAssociationsTitle.Size = new System.Drawing.Size(0, 28);
            this.lblAssociationsTitle.TabIndex = 0;
            // 
            // gridAssociations
            // 
            this.gridAssociations.AllowUserToAddRows = false;
            this.gridAssociations.AllowUserToDeleteRows = false;
            this.gridAssociations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridAssociations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAssociations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAssociations.Location = new System.Drawing.Point(20, 50);
            this.gridAssociations.Name = "gridAssociations";
            this.gridAssociations.ReadOnly = true;
            this.gridAssociations.RowHeadersVisible = false;
            this.gridAssociations.RowHeadersWidth = 51;
            this.gridAssociations.RowTemplate.Height = 24;
            this.gridAssociations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridAssociations.Size = new System.Drawing.Size(760, 320);
            this.gridAssociations.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(720, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(185, 37);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Inventory Dashboard";
            // 
            // frmInventoryDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this._associationsPanel);
            this.Controls.Add(this._lowStockPanel);
            this.Name = "frmInventoryDashboard";
            this.Text = "Inventory Dashboard";
            this._lowStockPanel.ResumeLayout(false);
            this._lowStockPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStockProducts)).EndInit();
            this._associationsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAssociations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _lowStockPanel;
        private System.Windows.Forms.DataGridView gridLowStockProducts;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.Panel _associationsPanel;
        private System.Windows.Forms.DataGridView gridAssociations;
        private System.Windows.Forms.Label lblAssociationsTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}
