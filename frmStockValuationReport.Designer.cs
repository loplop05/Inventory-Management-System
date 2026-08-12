namespace InventoryManagementSystem
{
    partial class frmStockValuationReport
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
            this._buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.DataGVStockValuation = new System.Windows.Forms.DataGridView();
            this.lblTotalStockValue = new System.Windows.Forms.Label();
            this.lblEmptyState = new System.Windows.Forms.Label();
            this._contentPanel.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVStockValuation)).BeginInit();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._buttonsPanel);
            this._contentPanel.Controls.Add(this.DataGVStockValuation);
            this._contentPanel.Controls.Add(this.lblTotalStockValue);
            this._contentPanel.Controls.Add(this.lblEmptyState);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1400, 800);
            this._contentPanel.TabIndex = 1;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this.btnClose);
            this._buttonsPanel.Controls.Add(this.btnRefresh);
            this._buttonsPanel.Controls.Add(this.btnExportCsv);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._buttonsPanel.Location = new System.Drawing.Point(0, 0);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Size = new System.Drawing.Size(1150, 50);
            this._buttonsPanel.TabIndex = 1;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportCsv.Location = new System.Drawing.Point(0, 0);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(120, 34);
            this.btnExportCsv.TabIndex = 0;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(126, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 34);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(232, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 34);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DataGVStockValuation
            // 
            this.DataGVStockValuation.AllowUserToAddRows = false;
            this.DataGVStockValuation.AllowUserToDeleteRows = false;
            this.DataGVStockValuation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVStockValuation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGVStockValuation.Location = new System.Drawing.Point(0, 110);
            this.DataGVStockValuation.Name = "DataGVStockValuation";
            this.DataGVStockValuation.ReadOnly = true;
            this.DataGVStockValuation.RowHeadersWidth = 51;
            this.DataGVStockValuation.RowTemplate.Height = 24;
            this.DataGVStockValuation.Size = new System.Drawing.Size(1150, 640);
            this.DataGVStockValuation.TabIndex = 2;
            // 
            // lblTotalStockValue
            // 
            this.lblTotalStockValue.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalStockValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalStockValue.Location = new System.Drawing.Point(0, 750);
            this.lblTotalStockValue.Name = "lblTotalStockValue";
            this.lblTotalStockValue.Size = new System.Drawing.Size(1150, 32);
            this.lblTotalStockValue.TabIndex = 3;
            this.lblTotalStockValue.Text = "Total Stock Value: 0.00";
            // 
            // lblEmptyState
            // 
            this.lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.lblEmptyState.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmptyState.Location = new System.Drawing.Point(300, 300);
            this.lblEmptyState.Name = "lblEmptyState";
            this.lblEmptyState.Size = new System.Drawing.Size(550, 40);
            this.lblEmptyState.TabIndex = 4;
            this.lblEmptyState.Text = "No products are available for stock valuation.";
            this.lblEmptyState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEmptyState.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // frmStockValuationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this._contentPanel);
            this.MinimumSize = new System.Drawing.Size(1200, 650);
            this.Name = "frmStockValuationReport";
            this.Text = "Stock Valuation Report";
            this.AutoScroll = true;
            this.Load += new System.EventHandler(this.frmStockValuationReport_Load);
            this._contentPanel.ResumeLayout(false);
            this._buttonsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGVStockValuation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.FlowLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView DataGVStockValuation;
        private System.Windows.Forms.Label lblTotalStockValue;
        private System.Windows.Forms.Label lblEmptyState;
    }
}
