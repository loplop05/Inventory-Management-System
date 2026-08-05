namespace InventoryManagementSystem
{
    partial class ucSidebarNav
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
            this._rootPanel = new System.Windows.Forms.Panel();
            this._navItemsPanel = new System.Windows.Forms.Panel();
            this._supportPanel = new System.Windows.Forms.Panel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._lblSubtitle = new System.Windows.Forms.Label();
            this._rootPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootPanel
            // 
            this._rootPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._rootPanel.Controls.Add(this._navItemsPanel);
            this._rootPanel.Controls.Add(this._supportPanel);
            this._rootPanel.Controls.Add(this._lblTitle);
            this._rootPanel.Controls.Add(this._lblSubtitle);
            this._rootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootPanel.Location = new System.Drawing.Point(0, 0);
            this._rootPanel.Name = "_rootPanel";
            this._rootPanel.Size = new System.Drawing.Size(186, 600);
            this._rootPanel.TabIndex = 0;
            // 
            // _navItemsPanel
            // 
            this._navItemsPanel.AutoScroll = true;
            this._navItemsPanel.BackColor = System.Drawing.Color.PowderBlue;
            this._navItemsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._navItemsPanel.Location = new System.Drawing.Point(0, 0);
            this._navItemsPanel.Name = "_navItemsPanel";
            this._navItemsPanel.Size = new System.Drawing.Size(186, 546);
            this._navItemsPanel.TabIndex = 1;
            this._navItemsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._navItemsPanel_Paint);
            // 
            // _supportPanel
            // 
            this._supportPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._supportPanel.Location = new System.Drawing.Point(0, 546);
            this._supportPanel.Name = "_supportPanel";
            this._supportPanel.Size = new System.Drawing.Size(186, 54);
            this._supportPanel.TabIndex = 2;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.White;
            this._lblTitle.Location = new System.Drawing.Point(16, 16);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(186, 32);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "ElectroPOS Pro";
            // 
            // _lblSubtitle
            // 
            this._lblSubtitle.AutoSize = true;
            this._lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._lblSubtitle.Location = new System.Drawing.Point(16, 41);
            this._lblSubtitle.Name = "_lblSubtitle";
            this._lblSubtitle.Size = new System.Drawing.Size(81, 20);
            this._lblSubtitle.TabIndex = 1;
            this._lblSubtitle.Text = "Main Store";
            // 
            // ucSidebarNav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.Controls.Add(this._rootPanel);
            this.Name = "ucSidebarNav";
            this.Size = new System.Drawing.Size(186, 600);
            this._rootPanel.ResumeLayout(false);
            this._rootPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel _rootPanel;
        private System.Windows.Forms.Panel _navItemsPanel;
        private System.Windows.Forms.Panel _supportPanel;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.Label _lblSubtitle;
    }
}
