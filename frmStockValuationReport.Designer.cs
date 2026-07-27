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
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.DataGVStockValuation = new System.Windows.Forms.DataGridView();
            this.lblTotalStockValue = new System.Windows.Forms.Label();
            this.lblEmptyState = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGVStockValuation)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(55, 94);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(285, 32);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Stock Valuation Report";
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCsv.Location = new System.Drawing.Point(536, 89);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(122, 38);
            this.btnExportCsv.TabIndex = 0;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(672, 89);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 38);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh (F5)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(816, 89);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 38);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close (Esc)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DataGVStockValuation
            // 
            this.DataGVStockValuation.AllowUserToAddRows = false;
            this.DataGVStockValuation.AllowUserToDeleteRows = false;
            this.DataGVStockValuation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGVStockValuation.Location = new System.Drawing.Point(55, 155);
            this.DataGVStockValuation.Name = "DataGVStockValuation";
            this.DataGVStockValuation.ReadOnly = true;
            this.DataGVStockValuation.RowHeadersWidth = 51;
            this.DataGVStockValuation.RowTemplate.Height = 24;
            this.DataGVStockValuation.Size = new System.Drawing.Size(891, 386);
            this.DataGVStockValuation.TabIndex = 3;
            // 
            // lblTotalStockValue
            // 
            this.lblTotalStockValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStockValue.Location = new System.Drawing.Point(55, 569);
            this.lblTotalStockValue.Name = "lblTotalStockValue";
            this.lblTotalStockValue.Size = new System.Drawing.Size(500, 32);
            this.lblTotalStockValue.TabIndex = 4;
            this.lblTotalStockValue.Text = "Total Stock Value: 0.00";
            // 
            // lblEmptyState
            // 
            this.lblEmptyState.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmptyState.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmptyState.Location = new System.Drawing.Point(210, 260);
            this.lblEmptyState.Name = "lblEmptyState";
            this.lblEmptyState.Size = new System.Drawing.Size(580, 40);
            this.lblEmptyState.TabIndex = 5;
            this.lblEmptyState.Text = "No products are available for stock valuation.";
            this.lblEmptyState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmStockValuationReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 650);
            this.Controls.Add(this.lblEmptyState);
            this.Controls.Add(this.lblTotalStockValue);
            this.Controls.Add(this.DataGVStockValuation);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExportCsv);
            this.Controls.Add(this.labelTitle);
            this.Name = "frmStockValuationReport";
            this.Text = "Stock Valuation Report";
            this.Load += new System.EventHandler(this.frmStockValuationReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGVStockValuation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView DataGVStockValuation;
        private System.Windows.Forms.Label lblTotalStockValue;
        private System.Windows.Forms.Label lblEmptyState;
    }
}
