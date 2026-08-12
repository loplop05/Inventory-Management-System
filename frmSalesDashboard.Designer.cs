namespace Inventory1PresentationLayer
{
    partial class frmSalesDashboard
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
            this._hourlySalesPanel = new System.Windows.Forms.Panel();
            this.pnlHourlyChart = new System.Windows.Forms.Panel();
            this.lblHourlySalesTitle = new System.Windows.Forms.Label();
            this._categoryPanel = new System.Windows.Forms.Panel();
            this.pnlCategoryChart = new System.Windows.Forms.Panel();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this._paymentPanel = new System.Windows.Forms.Panel();
            this.lblPaymentCash = new System.Windows.Forms.Label();
            this.lblPaymentVisa = new System.Windows.Forms.Label();
            this.lblPaymentOther = new System.Windows.Forms.Label();
            this.lblPaymentTitle = new System.Windows.Forms.Label();
            this._forecastPanel = new System.Windows.Forms.Panel();
            this.btnRunForecast = new System.Windows.Forms.Button();
            this.gridForecast = new System.Windows.Forms.DataGridView();
            this.lblForecastTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this._hourlySalesPanel.SuspendLayout();
            this._categoryPanel.SuspendLayout();
            this._paymentPanel.SuspendLayout();
            this._forecastPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridForecast)).BeginInit();
            this.SuspendLayout();
            // 
            // _hourlySalesPanel
            // 
            this._hourlySalesPanel.Controls.Add(this.pnlHourlyChart);
            this._hourlySalesPanel.Controls.Add(this.lblHourlySalesTitle);
            this._hourlySalesPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._hourlySalesPanel.Location = new System.Drawing.Point(0, 60);
            this._hourlySalesPanel.Name = "_hourlySalesPanel";
            this._hourlySalesPanel.Size = new System.Drawing.Size(800, 200);
            this._hourlySalesPanel.TabIndex = 0;
            // 
            // pnlHourlyChart
            // 
            this.pnlHourlyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHourlyChart.Location = new System.Drawing.Point(0, 30);
            this.pnlHourlyChart.Name = "pnlHourlyChart";
            this.pnlHourlyChart.Size = new System.Drawing.Size(800, 170);
            this.pnlHourlyChart.TabIndex = 1;
            // 
            // lblHourlySalesTitle
            // 
            this.lblHourlySalesTitle.AutoSize = true;
            this.lblHourlySalesTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHourlySalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHourlySalesTitle.Location = new System.Drawing.Point(10, 5);
            this.lblHourlySalesTitle.Name = "lblHourlySalesTitle";
            this.lblHourlySalesTitle.Size = new System.Drawing.Size(125, 28);
            this.lblHourlySalesTitle.TabIndex = 0;
            this.lblHourlySalesTitle.Text = "Hourly Sales";
            // 
            // _categoryPanel
            // 
            this._categoryPanel.Controls.Add(this.pnlCategoryChart);
            this._categoryPanel.Controls.Add(this.lblCategoryTitle);
            this._categoryPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._categoryPanel.Location = new System.Drawing.Point(0, 260);
            this._categoryPanel.Name = "_categoryPanel";
            this._categoryPanel.Size = new System.Drawing.Size(800, 200);
            this._categoryPanel.TabIndex = 1;
            // 
            // pnlCategoryChart
            // 
            this.pnlCategoryChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategoryChart.Location = new System.Drawing.Point(0, 30);
            this.pnlCategoryChart.Name = "pnlCategoryChart";
            this.pnlCategoryChart.Size = new System.Drawing.Size(800, 170);
            this.pnlCategoryChart.TabIndex = 1;
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCategoryTitle.Location = new System.Drawing.Point(10, 5);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(140, 28);
            this.lblCategoryTitle.TabIndex = 0;
            this.lblCategoryTitle.Text = "Category Performance";
            // 
            // _paymentPanel
            // 
            this._paymentPanel.Controls.Add(this.lblPaymentOther);
            this._paymentPanel.Controls.Add(this.lblPaymentVisa);
            this._paymentPanel.Controls.Add(this.lblPaymentCash);
            this._paymentPanel.Controls.Add(this.lblPaymentTitle);
            this._paymentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._paymentPanel.Location = new System.Drawing.Point(0, 460);
            this._paymentPanel.Name = "_paymentPanel";
            this._paymentPanel.Size = new System.Drawing.Size(800, 120);
            this._paymentPanel.TabIndex = 2;
            // 
            // lblPaymentTitle
            // 
            this.lblPaymentTitle.AutoSize = true;
            this.lblPaymentTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPaymentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPaymentTitle.Location = new System.Drawing.Point(10, 5);
            this.lblPaymentTitle.Name = "lblPaymentTitle";
            this.lblPaymentTitle.Size = new System.Drawing.Size(115, 28);
            this.lblPaymentTitle.TabIndex = 0;
            this.lblPaymentTitle.Text = "Payment Methods";
            // 
            // lblPaymentCash
            // 
            this.lblPaymentCash.AutoSize = true;
            this.lblPaymentCash.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPaymentCash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblPaymentCash.Location = new System.Drawing.Point(10, 40);
            this.lblPaymentCash.Name = "lblPaymentCash";
            this.lblPaymentCash.Size = new System.Drawing.Size(45, 24);
            this.lblPaymentCash.TabIndex = 1;
            this.lblPaymentCash.Text = "Cash: $0";
            // 
            // lblPaymentVisa
            // 
            this.lblPaymentVisa.AutoSize = true;
            this.lblPaymentVisa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPaymentVisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblPaymentVisa.Location = new System.Drawing.Point(10, 70);
            this.lblPaymentVisa.Name = "lblPaymentVisa";
            this.lblPaymentVisa.Size = new System.Drawing.Size(45, 24);
            this.lblPaymentVisa.TabIndex = 2;
            this.lblPaymentVisa.Text = "Visa: $0";
            // 
            // lblPaymentOther
            // 
            this.lblPaymentOther.AutoSize = true;
            this.lblPaymentOther.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPaymentOther.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(85)))), ((int)(((byte)(247)))));
            this.lblPaymentOther.Location = new System.Drawing.Point(10, 100);
            this.lblPaymentOther.Name = "lblPaymentOther";
            this.lblPaymentOther.Size = new System.Drawing.Size(45, 24);
            this.lblPaymentOther.TabIndex = 3;
            this.lblPaymentOther.Text = "Other: $0";
            // 
            // _forecastPanel
            // 
            this._forecastPanel.Controls.Add(this.gridForecast);
            this._forecastPanel.Controls.Add(this.lblForecastTitle);
            this._forecastPanel.Controls.Add(this.btnRunForecast);
            this._forecastPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._forecastPanel.Location = new System.Drawing.Point(0, 580);
            this._forecastPanel.Name = "_forecastPanel";
            this._forecastPanel.Padding = new System.Windows.Forms.Padding(20);
            this._forecastPanel.Size = new System.Drawing.Size(800, 220);
            this._forecastPanel.TabIndex = 3;
            // 
            // btnRunForecast
            // 
            this.btnRunForecast.Location = new System.Drawing.Point(20, 20);
            this.btnRunForecast.Name = "btnRunForecast";
            this.btnRunForecast.Size = new System.Drawing.Size(150, 40);
            this.btnRunForecast.TabIndex = 0;
            this.btnRunForecast.Text = "Run Forecast";
            this.btnRunForecast.UseVisualStyleBackColor = true;
            this.btnRunForecast.Click += new System.EventHandler(this.btnRunForecast_Click);
            // 
            // lblForecastTitle
            // 
            this.lblForecastTitle.AutoSize = true;
            this.lblForecastTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblForecastTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblForecastTitle.Location = new System.Drawing.Point(180, 30);
            this.lblForecastTitle.Name = "lblForecastTitle";
            this.lblForecastTitle.Size = new System.Drawing.Size(0, 28);
            this.lblForecastTitle.TabIndex = 1;
            // 
            // gridForecast
            // 
            this.gridForecast.AllowUserToAddRows = false;
            this.gridForecast.AllowUserToDeleteRows = false;
            this.gridForecast.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridForecast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridForecast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridForecast.Location = new System.Drawing.Point(20, 70);
            this.gridForecast.Name = "gridForecast";
            this.gridForecast.ReadOnly = true;
            this.gridForecast.RowHeadersVisible = false;
            this.gridForecast.RowHeadersWidth = 51;
            this.gridForecast.RowTemplate.Height = 24;
            this.gridForecast.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridForecast.Size = new System.Drawing.Size(760, 130);
            this.gridForecast.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(720, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 30);
            this.btnClose.TabIndex = 4;
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
            this.lblTitle.Size = new System.Drawing.Size(132, 37);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Sales Dashboard";
            // 
            // frmSalesDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this._forecastPanel);
            this.Controls.Add(this._paymentPanel);
            this.Controls.Add(this._categoryPanel);
            this.Controls.Add(this._hourlySalesPanel);
            this.Name = "frmSalesDashboard";
            this.Text = "Sales Dashboard";
            this._hourlySalesPanel.ResumeLayout(false);
            this._hourlySalesPanel.PerformLayout();
            this._categoryPanel.ResumeLayout(false);
            this._categoryPanel.PerformLayout();
            this._paymentPanel.ResumeLayout(false);
            this._paymentPanel.PerformLayout();
            this._forecastPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridForecast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _hourlySalesPanel;
        private System.Windows.Forms.Panel pnlHourlyChart;
        private System.Windows.Forms.Label lblHourlySalesTitle;
        private System.Windows.Forms.Panel _categoryPanel;
        private System.Windows.Forms.Panel pnlCategoryChart;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Panel _paymentPanel;
        private System.Windows.Forms.Label lblPaymentCash;
        private System.Windows.Forms.Label lblPaymentVisa;
        private System.Windows.Forms.Label lblPaymentOther;
        private System.Windows.Forms.Label lblPaymentTitle;
        private System.Windows.Forms.Panel _forecastPanel;
        private System.Windows.Forms.Button btnRunForecast;
        private System.Windows.Forms.DataGridView gridForecast;
        private System.Windows.Forms.Label lblForecastTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
    }
}
