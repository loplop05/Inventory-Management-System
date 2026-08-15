namespace InventoryManagementSystem
{
    partial class frmPOSActions
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
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._lblTitle = new System.Windows.Forms.Label();
            this._buttonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this._btnAddDiscount = new System.Windows.Forms.Button();
            this._btnApplyCoupon = new System.Windows.Forms.Button();
            this._btnRedeemPoints = new System.Windows.Forms.Button();
            this._btnVoidItem = new System.Windows.Forms.Button();
            this._btnVoidOrder = new System.Windows.Forms.Button();
            this._btnHoldOrder = new System.Windows.Forms.Button();
            this._btnRetrieveHeld = new System.Windows.Forms.Button();
            this._btnClose = new System.Windows.Forms.Button();
            this._rootLayout.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Controls.Add(this._lblTitle, 0, 0);
            this._rootLayout.Controls.Add(this._buttonsPanel, 0, 1);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 2;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(500, 520);
            this._rootLayout.TabIndex = 0;
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTitle.Location = new System.Drawing.Point(3, 0);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(84, 46);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Actions";
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.ColumnCount = 2;
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._buttonsPanel.Controls.Add(this._btnAddDiscount, 0, 0);
            this._buttonsPanel.Controls.Add(this._btnApplyCoupon, 1, 0);
            this._buttonsPanel.Controls.Add(this._btnRedeemPoints, 0, 1);
            this._buttonsPanel.Controls.Add(this._btnVoidItem, 1, 1);
            this._buttonsPanel.Controls.Add(this._btnVoidOrder, 0, 2);
            this._buttonsPanel.Controls.Add(this._btnHoldOrder, 1, 2);
            this._buttonsPanel.Controls.Add(this._btnRetrieveHeld, 0, 3);
            this._buttonsPanel.Controls.Add(this._btnClose, 1, 3);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 70);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Padding = new System.Windows.Forms.Padding(15);
            this._buttonsPanel.RowCount = 4;
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._buttonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this._buttonsPanel.Size = new System.Drawing.Size(494, 450);
            this._buttonsPanel.TabIndex = 1;
            // 
            // _btnAddDiscount
            // 
            this._btnAddDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnAddDiscount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnAddDiscount.Location = new System.Drawing.Point(18, 18);
            this._btnAddDiscount.Name = "_btnAddDiscount";
            this._btnAddDiscount.Size = new System.Drawing.Size(221, 94);
            this._btnAddDiscount.TabIndex = 0;
            this._btnAddDiscount.Text = "Add Discount";
            this._btnAddDiscount.UseVisualStyleBackColor = true;
            this._btnAddDiscount.Click += new System.EventHandler(this._btnAddDiscount_Click);
            // 
            // _btnApplyCoupon
            // 
            this._btnApplyCoupon.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnApplyCoupon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnApplyCoupon.Location = new System.Drawing.Point(255, 18);
            this._btnApplyCoupon.Name = "_btnApplyCoupon";
            this._btnApplyCoupon.Size = new System.Drawing.Size(221, 94);
            this._btnApplyCoupon.TabIndex = 1;
            this._btnApplyCoupon.Text = "Apply Coupon";
            this._btnApplyCoupon.UseVisualStyleBackColor = true;
            this._btnApplyCoupon.Click += new System.EventHandler(this._btnApplyCoupon_Click);
            // 
            // _btnRedeemPoints
            // 
            this._btnRedeemPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnRedeemPoints.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnRedeemPoints.Location = new System.Drawing.Point(18, 130);
            this._btnRedeemPoints.Name = "_btnRedeemPoints";
            this._btnRedeemPoints.Size = new System.Drawing.Size(221, 94);
            this._btnRedeemPoints.TabIndex = 2;
            this._btnRedeemPoints.Text = "Redeem Points";
            this._btnRedeemPoints.UseVisualStyleBackColor = true;
            this._btnRedeemPoints.Click += new System.EventHandler(this._btnRedeemPoints_Click);
            // 
            // _btnVoidItem
            // 
            this._btnVoidItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnVoidItem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnVoidItem.Location = new System.Drawing.Point(255, 130);
            this._btnVoidItem.Name = "_btnVoidItem";
            this._btnVoidItem.Size = new System.Drawing.Size(221, 94);
            this._btnVoidItem.TabIndex = 3;
            this._btnVoidItem.Text = "Void Item";
            this._btnVoidItem.UseVisualStyleBackColor = true;
            this._btnVoidItem.Click += new System.EventHandler(this._btnVoidItem_Click);
            // 
            // _btnVoidOrder
            // 
            this._btnVoidOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnVoidOrder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnVoidOrder.Location = new System.Drawing.Point(18, 242);
            this._btnVoidOrder.Name = "_btnVoidOrder";
            this._btnVoidOrder.Size = new System.Drawing.Size(221, 94);
            this._btnVoidOrder.TabIndex = 4;
            this._btnVoidOrder.Text = "Void Order";
            this._btnVoidOrder.UseVisualStyleBackColor = true;
            this._btnVoidOrder.Click += new System.EventHandler(this._btnVoidOrder_Click);
            // 
            // _btnHoldOrder
            // 
            this._btnHoldOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnHoldOrder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnHoldOrder.Location = new System.Drawing.Point(255, 242);
            this._btnHoldOrder.Name = "_btnHoldOrder";
            this._btnHoldOrder.Size = new System.Drawing.Size(221, 94);
            this._btnHoldOrder.TabIndex = 5;
            this._btnHoldOrder.Text = "Hold Order";
            this._btnHoldOrder.UseVisualStyleBackColor = true;
            this._btnHoldOrder.Click += new System.EventHandler(this._btnHoldOrder_Click);
            // 
            // _btnRetrieveHeld
            // 
            this._btnRetrieveHeld.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnRetrieveHeld.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnRetrieveHeld.Location = new System.Drawing.Point(18, 354);
            this._btnRetrieveHeld.Name = "_btnRetrieveHeld";
            this._btnRetrieveHeld.Size = new System.Drawing.Size(221, 94);
            this._btnRetrieveHeld.TabIndex = 6;
            this._btnRetrieveHeld.Text = "Retrieve Held";
            this._btnRetrieveHeld.UseVisualStyleBackColor = true;
            this._btnRetrieveHeld.Click += new System.EventHandler(this._btnRetrieveHeld_Click);
            // 
            // _btnClose
            // 
            this._btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this._btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnClose.Location = new System.Drawing.Point(255, 354);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(221, 94);
            this._btnClose.TabIndex = 7;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
            // 
            // frmPOSActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 520);
            this.Controls.Add(this._rootLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPOSActions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "POS Actions";
            this._rootLayout.ResumeLayout(false);
            this._rootLayout.PerformLayout();
            this._buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.TableLayoutPanel _buttonsPanel;
        private System.Windows.Forms.Button _btnAddDiscount;
        private System.Windows.Forms.Button _btnApplyCoupon;
        private System.Windows.Forms.Button _btnRedeemPoints;
        private System.Windows.Forms.Button _btnVoidItem;
        private System.Windows.Forms.Button _btnVoidOrder;
        private System.Windows.Forms.Button _btnHoldOrder;
        private System.Windows.Forms.Button _btnRetrieveHeld;
        private System.Windows.Forms.Button _btnClose;
    }
}
