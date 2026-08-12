namespace Inventory1PresentationLayer
{
    partial class frmCustomersDashboard
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
            this._loyaltyPanel = new System.Windows.Forms.Panel();
            this.pnlLoyaltyChart = new System.Windows.Forms.Panel();
            this.lblLoyaltyTitle = new System.Windows.Forms.Label();
            this.gridTopLoyaltyMembers = new System.Windows.Forms.DataGridView();
            this._customerAnalyticsPanel = new System.Windows.Forms.Panel();
            this.gridCustomerAnalytics = new System.Windows.Forms.DataGridView();
            this.lblCustomerAnalyticsTitle = new System.Windows.Forms.Label();
            this._segmentationPanel = new System.Windows.Forms.Panel();
            this.btnRunSegmentation = new System.Windows.Forms.Button();
            this.gridSegmentation = new System.Windows.Forms.DataGridView();
            this.lblSegmentationTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this._loyaltyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopLoyaltyMembers)).BeginInit();
            this._customerAnalyticsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerAnalytics)).BeginInit();
            this._segmentationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSegmentation)).BeginInit();
            this.SuspendLayout();
            // 
            // _loyaltyPanel
            // 
            this._loyaltyPanel.Controls.Add(this.gridTopLoyaltyMembers);
            this._loyaltyPanel.Controls.Add(this.pnlLoyaltyChart);
            this._loyaltyPanel.Controls.Add(this.lblLoyaltyTitle);
            this._loyaltyPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._loyaltyPanel.Location = new System.Drawing.Point(0, 60);
            this._loyaltyPanel.Name = "_loyaltyPanel";
            this._loyaltyPanel.Size = new System.Drawing.Size(800, 250);
            this._loyaltyPanel.TabIndex = 0;
            // 
            // pnlLoyaltyChart
            // 
            this.pnlLoyaltyChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLoyaltyChart.Location = new System.Drawing.Point(0, 30);
            this.pnlLoyaltyChart.Name = "pnlLoyaltyChart";
            this.pnlLoyaltyChart.Size = new System.Drawing.Size(200, 220);
            this.pnlLoyaltyChart.TabIndex = 1;
            // 
            // lblLoyaltyTitle
            // 
            this.lblLoyaltyTitle.AutoSize = true;
            this.lblLoyaltyTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoyaltyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLoyaltyTitle.Location = new System.Drawing.Point(10, 5);
            this.lblLoyaltyTitle.Name = "lblLoyaltyTitle";
            this.lblLoyaltyTitle.Size = new System.Drawing.Size(105, 28);
            this.lblLoyaltyTitle.TabIndex = 0;
            this.lblLoyaltyTitle.Text = "Loyalty Analytics";
            // 
            // gridTopLoyaltyMembers
            // 
            this.gridTopLoyaltyMembers.AllowUserToAddRows = false;
            this.gridTopLoyaltyMembers.AllowUserToDeleteRows = false;
            this.gridTopLoyaltyMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridTopLoyaltyMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopLoyaltyMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTopLoyaltyMembers.Location = new System.Drawing.Point(200, 30);
            this.gridTopLoyaltyMembers.Name = "gridTopLoyaltyMembers";
            this.gridTopLoyaltyMembers.ReadOnly = true;
            this.gridTopLoyaltyMembers.RowHeadersVisible = false;
            this.gridTopLoyaltyMembers.RowHeadersWidth = 51;
            this.gridTopLoyaltyMembers.RowTemplate.Height = 24;
            this.gridTopLoyaltyMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTopLoyaltyMembers.Size = new System.Drawing.Size(600, 220);
            this.gridTopLoyaltyMembers.TabIndex = 2;
            // 
            // _customerAnalyticsPanel
            // 
            this._customerAnalyticsPanel.Controls.Add(this.gridCustomerAnalytics);
            this._customerAnalyticsPanel.Controls.Add(this.lblCustomerAnalyticsTitle);
            this._customerAnalyticsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._customerAnalyticsPanel.Location = new System.Drawing.Point(0, 310);
            this._customerAnalyticsPanel.Name = "_customerAnalyticsPanel";
            this._customerAnalyticsPanel.Size = new System.Drawing.Size(800, 250);
            this._customerAnalyticsPanel.TabIndex = 1;
            // 
            // lblCustomerAnalyticsTitle
            // 
            this.lblCustomerAnalyticsTitle.AutoSize = true;
            this.lblCustomerAnalyticsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCustomerAnalyticsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustomerAnalyticsTitle.Location = new System.Drawing.Point(10, 5);
            this.lblCustomerAnalyticsTitle.Name = "lblCustomerAnalyticsTitle";
            this.lblCustomerAnalyticsTitle.Size = new System.Drawing.Size(146, 28);
            this.lblCustomerAnalyticsTitle.TabIndex = 0;
            this.lblCustomerAnalyticsTitle.Text = "Customer Analytics";
            // 
            // gridCustomerAnalytics
            // 
            this.gridCustomerAnalytics.AllowUserToAddRows = false;
            this.gridCustomerAnalytics.AllowUserToDeleteRows = false;
            this.gridCustomerAnalytics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCustomerAnalytics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomerAnalytics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomerAnalytics.Location = new System.Drawing.Point(0, 30);
            this.gridCustomerAnalytics.Name = "gridCustomerAnalytics";
            this.gridCustomerAnalytics.ReadOnly = true;
            this.gridCustomerAnalytics.RowHeadersVisible = false;
            this.gridCustomerAnalytics.RowHeadersWidth = 51;
            this.gridCustomerAnalytics.RowTemplate.Height = 24;
            this.gridCustomerAnalytics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomerAnalytics.Size = new System.Drawing.Size(800, 220);
            this.gridCustomerAnalytics.TabIndex = 1;
            // 
            // _segmentationPanel
            // 
            this._segmentationPanel.Controls.Add(this.gridSegmentation);
            this._segmentationPanel.Controls.Add(this.lblSegmentationTitle);
            this._segmentationPanel.Controls.Add(this.btnRunSegmentation);
            this._segmentationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._segmentationPanel.Location = new System.Drawing.Point(0, 560);
            this._segmentationPanel.Name = "_segmentationPanel";
            this._segmentationPanel.Padding = new System.Windows.Forms.Padding(20);
            this._segmentationPanel.Size = new System.Drawing.Size(800, 240);
            this._segmentationPanel.TabIndex = 2;
            // 
            // btnRunSegmentation
            // 
            this.btnRunSegmentation.Location = new System.Drawing.Point(20, 20);
            this.btnRunSegmentation.Name = "btnRunSegmentation";
            this.btnRunSegmentation.Size = new System.Drawing.Size(150, 40);
            this.btnRunSegmentation.TabIndex = 0;
            this.btnRunSegmentation.Text = "Run Segmentation";
            this.btnRunSegmentation.UseVisualStyleBackColor = true;
            this.btnRunSegmentation.Click += new System.EventHandler(this.btnRunSegmentation_Click);
            // 
            // lblSegmentationTitle
            // 
            this.lblSegmentationTitle.AutoSize = true;
            this.lblSegmentationTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSegmentationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSegmentationTitle.Location = new System.Drawing.Point(180, 30);
            this.lblSegmentationTitle.Name = "lblSegmentationTitle";
            this.lblSegmentationTitle.Size = new System.Drawing.Size(0, 28);
            this.lblSegmentationTitle.TabIndex = 1;
            // 
            // gridSegmentation
            // 
            this.gridSegmentation.AllowUserToAddRows = false;
            this.gridSegmentation.AllowUserToDeleteRows = false;
            this.gridSegmentation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSegmentation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSegmentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSegmentation.Location = new System.Drawing.Point(20, 70);
            this.gridSegmentation.Name = "gridSegmentation";
            this.gridSegmentation.ReadOnly = true;
            this.gridSegmentation.RowHeadersVisible = false;
            this.gridSegmentation.RowHeadersWidth = 51;
            this.gridSegmentation.RowTemplate.Height = 24;
            this.gridSegmentation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSegmentation.Size = new System.Drawing.Size(760, 150);
            this.gridSegmentation.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(720, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 30);
            this.btnClose.TabIndex = 3;
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
            this.lblTitle.Size = new System.Drawing.Size(175, 37);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Customers Dashboard";
            // 
            // frmCustomersDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this._segmentationPanel);
            this.Controls.Add(this._customerAnalyticsPanel);
            this.Controls.Add(this._loyaltyPanel);
            this.Name = "frmCustomersDashboard";
            this.Text = "Customers Dashboard";
            this._loyaltyPanel.ResumeLayout(false);
            this._loyaltyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopLoyaltyMembers)).EndInit();
            this._customerAnalyticsPanel.ResumeLayout(false);
            this._customerAnalyticsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerAnalytics)).EndInit();
            this._segmentationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSegmentation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _loyaltyPanel;
        private System.Windows.Forms.Panel pnlLoyaltyChart;
        private System.Windows.Forms.Label lblLoyaltyTitle;
        private System.Windows.Forms.DataGridView gridTopLoyaltyMembers;
        private System.Windows.Forms.Panel _customerAnalyticsPanel;
        private System.Windows.Forms.DataGridView gridCustomerAnalytics;
        private System.Windows.Forms.Label lblCustomerAnalyticsTitle;
        private System.Windows.Forms.Panel _segmentationPanel;
        private System.Windows.Forms.Button btnRunSegmentation;
        private System.Windows.Forms.DataGridView gridSegmentation;
        private System.Windows.Forms.Label lblSegmentationTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}
