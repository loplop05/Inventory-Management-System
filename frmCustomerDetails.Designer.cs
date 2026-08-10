namespace InventoryManagementSystem
{
    partial class frmCustomerDetails
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this._customerInfoPanel = new System.Windows.Forms.Panel();
            this._lblCustomerName = new System.Windows.Forms.Label();
            this._lblPhoneNumber = new System.Windows.Forms.Label();
            this._lblTier = new System.Windows.Forms.Label();
            this._lblPoints = new System.Windows.Forms.Label();
            this._lblTotalSpent = new System.Windows.Forms.Label();
            this._lblLastPurchase = new System.Windows.Forms.Label();
            this._loyaltyPanel = new System.Windows.Forms.Panel();
            this._lblLoyaltyTitle = new System.Windows.Forms.Label();
            this._lblNextTier = new System.Windows.Forms.Label();
            this._lblAmountToNextTier = new System.Windows.Forms.Label();
            this._lblDiscountAvailable = new System.Windows.Forms.Label();
            this._ordersPanel = new System.Windows.Forms.Panel();
            this._lblOrdersTitle = new System.Windows.Forms.Label();
            this.gridCustomerOrders = new System.Windows.Forms.DataGridView();
            this._btnClose = new System.Windows.Forms.Button();
            this._btnAdjustPoints = new System.Windows.Forms.Button();
            this._mainPanel.SuspendLayout();
            this._customerInfoPanel.SuspendLayout();
            this._loyaltyPanel.SuspendLayout();
            this._ordersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.ColumnCount = 1;
            this._mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.Controls.Add(this._customerInfoPanel, 0, 0);
            this._mainPanel.Controls.Add(this._loyaltyPanel, 0, 1);
            this._mainPanel.Controls.Add(this._ordersPanel, 0, 2);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.RowCount = 4;
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this._mainPanel.Size = new System.Drawing.Size(800, 600);
            this._mainPanel.TabIndex = 0;
            // 
            // _customerInfoPanel
            // 
            this._customerInfoPanel.Controls.Add(this._lblCustomerName);
            this._customerInfoPanel.Controls.Add(this._lblPhoneNumber);
            this._customerInfoPanel.Controls.Add(this._lblTier);
            this._customerInfoPanel.Controls.Add(this._lblPoints);
            this._customerInfoPanel.Controls.Add(this._lblTotalSpent);
            this._customerInfoPanel.Controls.Add(this._lblLastPurchase);
            this._customerInfoPanel.Controls.Add(this._lblOrderCount);
            this._customerInfoPanel.Controls.Add(this._lblDaysSinceLastPurchase);
            this._customerInfoPanel.Controls.Add(this._lblRepeatBuyerBadge);
            this._customerInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerInfoPanel.Location = new System.Drawing.Point(3, 3);
            this._customerInfoPanel.Name = "_customerInfoPanel";
            this._customerInfoPanel.Size = new System.Drawing.Size(794, 94);
            this._customerInfoPanel.TabIndex = 0;
            // 
            // _lblCustomerName
            // 
            this._lblCustomerName.AutoSize = true;
            this._lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblCustomerName.Location = new System.Drawing.Point(10, 10);
            this._lblCustomerName.Name = "_lblCustomerName";
            this._lblCustomerName.Size = new System.Drawing.Size(0, 31);
            this._lblCustomerName.TabIndex = 0;
            // 
            // _lblPhoneNumber
            // 
            this._lblPhoneNumber.AutoSize = true;
            this._lblPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblPhoneNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblPhoneNumber.Location = new System.Drawing.Point(10, 45);
            this._lblPhoneNumber.Name = "_lblPhoneNumber";
            this._lblPhoneNumber.Size = new System.Drawing.Size(0, 20);
            this._lblPhoneNumber.TabIndex = 1;
            // 
            // _lblTier
            // 
            this._lblTier.AutoSize = true;
            this._lblTier.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblTier.Location = new System.Drawing.Point(10, 70);
            this._lblTier.Name = "_lblTier";
            this._lblTier.Size = new System.Drawing.Size(0, 20);
            this._lblTier.TabIndex = 2;
            // 
            // _lblPoints
            // 
            this._lblPoints.AutoSize = true;
            this._lblPoints.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblPoints.Location = new System.Drawing.Point(200, 70);
            this._lblPoints.Name = "_lblPoints";
            this._lblPoints.Size = new System.Drawing.Size(0, 20);
            this._lblPoints.TabIndex = 3;
            // 
            // _lblTotalSpent
            // 
            this._lblTotalSpent.AutoSize = true;
            this._lblTotalSpent.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblTotalSpent.Location = new System.Drawing.Point(400, 70);
            this._lblTotalSpent.Name = "_lblTotalSpent";
            this._lblTotalSpent.Size = new System.Drawing.Size(0, 20);
            this._lblTotalSpent.TabIndex = 4;
            // 
            // _lblLastPurchase
            // 
            this._lblLastPurchase.AutoSize = true;
            this._lblLastPurchase.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblLastPurchase.Location = new System.Drawing.Point(600, 70);
            this._lblLastPurchase.Name = "_lblLastPurchase";
            this._lblLastPurchase.Size = new System.Drawing.Size(0, 20);
            this._lblLastPurchase.TabIndex = 5;
            // 
            // _lblOrderCount
            // 
            this._lblOrderCount.AutoSize = true;
            this._lblOrderCount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblOrderCount.Location = new System.Drawing.Point(10, 95);
            this._lblOrderCount.Name = "_lblOrderCount";
            this._lblOrderCount.Size = new System.Drawing.Size(0, 20);
            this._lblOrderCount.TabIndex = 6;
            // 
            // _lblDaysSinceLastPurchase
            // 
            this._lblDaysSinceLastPurchase.AutoSize = true;
            this._lblDaysSinceLastPurchase.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._lblDaysSinceLastPurchase.Location = new System.Drawing.Point(200, 95);
            this._lblDaysSinceLastPurchase.Name = "_lblDaysSinceLastPurchase";
            this._lblDaysSinceLastPurchase.Size = new System.Drawing.Size(0, 20);
            this._lblDaysSinceLastPurchase.TabIndex = 7;
            // 
            // _lblRepeatBuyerBadge
            // 
            this._lblRepeatBuyerBadge.AutoSize = true;
            this._lblRepeatBuyerBadge.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblRepeatBuyerBadge.Location = new System.Drawing.Point(400, 95);
            this._lblRepeatBuyerBadge.Name = "_lblRepeatBuyerBadge";
            this._lblRepeatBuyerBadge.Size = new System.Drawing.Size(0, 20);
            this._lblRepeatBuyerBadge.TabIndex = 8;
            this._lblRepeatBuyerBadge.Visible = false;
            // 
            // _loyaltyPanel
            // 
            this._loyaltyPanel.Controls.Add(this._lblLoyaltyTitle);
            this._loyaltyPanel.Controls.Add(this._lblNextTier);
            this._loyaltyPanel.Controls.Add(this._lblAmountToNextTier);
            this._loyaltyPanel.Controls.Add(this._lblDiscountAvailable);
            this._loyaltyPanel.Controls.Add(this._lblTierProgress);
            this._loyaltyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._loyaltyPanel.Location = new System.Drawing.Point(3, 103);
            this._loyaltyPanel.Name = "_loyaltyPanel";
            this._loyaltyPanel.Size = new System.Drawing.Size(794, 114);
            this._loyaltyPanel.TabIndex = 1;
            // 
            // _lblLoyaltyTitle
            // 
            this._lblLoyaltyTitle.AutoSize = true;
            this._lblLoyaltyTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblLoyaltyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblLoyaltyTitle.Location = new System.Drawing.Point(10, 10);
            this._lblLoyaltyTitle.Name = "_lblLoyaltyTitle";
            this._lblLoyaltyTitle.Size = new System.Drawing.Size(0, 28);
            this._lblLoyaltyTitle.TabIndex = 0;
            // 
            // _lblNextTier
            // 
            this._lblNextTier.AutoSize = true;
            this._lblNextTier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblNextTier.Location = new System.Drawing.Point(10, 45);
            this._lblNextTier.Name = "_lblNextTier";
            this._lblNextTier.Size = new System.Drawing.Size(0, 19);
            this._lblNextTier.TabIndex = 1;
            // 
            // _lblAmountToNextTier
            // 
            this._lblAmountToNextTier.AutoSize = true;
            this._lblAmountToNextTier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblAmountToNextTier.Location = new System.Drawing.Point(10, 65);
            this._lblAmountToNextTier.Name = "_lblAmountToNextTier";
            this._lblAmountToNextTier.Size = new System.Drawing.Size(0, 19);
            this._lblAmountToNextTier.TabIndex = 2;
            // 
            // _lblDiscountAvailable
            // 
            this._lblDiscountAvailable.AutoSize = true;
            this._lblDiscountAvailable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblDiscountAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this._lblDiscountAvailable.Location = new System.Drawing.Point(10, 90);
            this._lblDiscountAvailable.Name = "_lblDiscountAvailable";
            this._lblDiscountAvailable.Size = new System.Drawing.Size(0, 19);
            this._lblDiscountAvailable.TabIndex = 3;
            // 
            // _lblTierProgress
            // 
            this._lblTierProgress.AutoSize = true;
            this._lblTierProgress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._lblTierProgress.Location = new System.Drawing.Point(10, 110);
            this._lblTierProgress.Name = "_lblTierProgress";
            this._lblTierProgress.Size = new System.Drawing.Size(0, 19);
            this._lblTierProgress.TabIndex = 4;
            // 
            // _ordersPanel
            // 
            this._ordersPanel.Controls.Add(this._lblOrdersTitle);
            this._ordersPanel.Controls.Add(this.gridCustomerOrders);
            this._ordersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ordersPanel.Location = new System.Drawing.Point(3, 223);
            this._ordersPanel.Name = "_ordersPanel";
            this._ordersPanel.Size = new System.Drawing.Size(794, 324);
            this._ordersPanel.TabIndex = 2;
            // 
            // _lblOrdersTitle
            // 
            this._lblOrdersTitle.AutoSize = true;
            this._lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._lblOrdersTitle.Location = new System.Drawing.Point(10, 10);
            this._lblOrdersTitle.Name = "_lblOrdersTitle";
            this._lblOrdersTitle.Size = new System.Drawing.Size(0, 28);
            this._lblOrdersTitle.TabIndex = 0;
            // 
            // gridCustomerOrders
            // 
            this.gridCustomerOrders.AllowUserToAddRows = false;
            this.gridCustomerOrders.AllowUserToDeleteRows = false;
            this.gridCustomerOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCustomerOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomerOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomerOrders.Location = new System.Drawing.Point(0, 40);
            this.gridCustomerOrders.Name = "gridCustomerOrders";
            this.gridCustomerOrders.ReadOnly = true;
            this.gridCustomerOrders.RowHeadersVisible = false;
            this.gridCustomerOrders.RowHeadersWidth = 51;
            this.gridCustomerOrders.RowTemplate.Height = 24;
            this.gridCustomerOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomerOrders.Size = new System.Drawing.Size(794, 284);
            this.gridCustomerOrders.TabIndex = 1;
            // 
            // _btnClose
            // 
            this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClose.Location = new System.Drawing.Point(690, 560);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(100, 35);
            this._btnClose.TabIndex = 3;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = true;
            // 
            // _btnAdjustPoints
            // 
            this._btnAdjustPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAdjustPoints.Location = new System.Drawing.Point(580, 560);
            this._btnAdjustPoints.Name = "_btnAdjustPoints";
            this._btnAdjustPoints.Size = new System.Drawing.Size(100, 35);
            this._btnAdjustPoints.TabIndex = 4;
            this._btnAdjustPoints.Text = "Adjust Points";
            this._btnAdjustPoints.UseVisualStyleBackColor = true;
            // 
            // frmCustomerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this._btnAdjustPoints);
            this.Controls.Add(this._btnClose);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmCustomerDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Customer Details";
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            this._customerInfoPanel.ResumeLayout(false);
            this._customerInfoPanel.PerformLayout();
            this._loyaltyPanel.ResumeLayout(false);
            this._loyaltyPanel.PerformLayout();
            this._ordersPanel.ResumeLayout(false);
            this._ordersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomerOrders)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel _mainPanel;
        private System.Windows.Forms.Panel _customerInfoPanel;
        private System.Windows.Forms.Label _lblCustomerName;
        private System.Windows.Forms.Label _lblPhoneNumber;
        private System.Windows.Forms.Label _lblTier;
        private System.Windows.Forms.Label _lblPoints;
        private System.Windows.Forms.Label _lblTotalSpent;
        private System.Windows.Forms.Label _lblLastPurchase;
        private System.Windows.Forms.Label _lblOrderCount;
        private System.Windows.Forms.Label _lblDaysSinceLastPurchase;
        private System.Windows.Forms.Label _lblRepeatBuyerBadge;
        private System.Windows.Forms.Panel _loyaltyPanel;
        private System.Windows.Forms.Label _lblLoyaltyTitle;
        private System.Windows.Forms.Label _lblNextTier;
        private System.Windows.Forms.Label _lblAmountToNextTier;
        private System.Windows.Forms.Label _lblDiscountAvailable;
        private System.Windows.Forms.Label _lblTierProgress;
        private System.Windows.Forms.Panel _ordersPanel;
        private System.Windows.Forms.Label _lblOrdersTitle;
        private System.Windows.Forms.DataGridView gridCustomerOrders;
        private System.Windows.Forms.Button _btnClose;
        private System.Windows.Forms.Button _btnAdjustPoints;
    }
}
