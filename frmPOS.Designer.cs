namespace InventoryManagementSystem
{
    partial class frmPOS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._topPanel = new System.Windows.Forms.Panel();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._btnReport = new System.Windows.Forms.Button();
            this._customerPanel = new System.Windows.Forms.Panel();
            this._lblCustomerPhone = new System.Windows.Forms.Label();
            this._txtCustomerPhone = new System.Windows.Forms.TextBox();
            this._btnAddCustomer = new System.Windows.Forms.Button();
            this._btnViewHistory = new System.Windows.Forms.Button();
            this._lblCustomerName = new System.Windows.Forms.Label();
            this._paymentPanel = new System.Windows.Forms.Panel();
            this._lblPaymentMethod = new System.Windows.Forms.Label();
            this._rbCash = new System.Windows.Forms.RadioButton();
            this._rbVisa = new System.Windows.Forms.RadioButton();
            this._txtPaymentDetails = new System.Windows.Forms.TextBox();
            this._lblPaymentDetails = new System.Windows.Forms.Label();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._tabsProducts = new System.Windows.Forms.TabControl();
            this._receiptPanel = new System.Windows.Forms.Panel();
            this._gridReceipt = new System.Windows.Forms.DataGridView();
            this._colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._lblStatus = new System.Windows.Forms.Label();
            this._lblItemCount = new System.Windows.Forms.Label();
            this._totalsPanel = new System.Windows.Forms.Panel();
            this._lblCouponCaption = new System.Windows.Forms.Label();
            this._txtCoupon = new System.Windows.Forms.TextBox();
            this._btnApplyCoupon = new System.Windows.Forms.Button();
            this._btnRemoveCoupon = new System.Windows.Forms.Button();
            this._btnManualDiscount = new System.Windows.Forms.Button();
            this._lblSubtotal = new System.Windows.Forms.Label();
            this._lblDiscount = new System.Windows.Forms.Label();
            this._lblTax = new System.Windows.Forms.Label();
            this._lblTotal = new System.Windows.Forms.Label();
            this._btnRemoveItem = new System.Windows.Forms.Button();
            this._btnCompleteOrder = new System.Windows.Forms.Button();
            this._btnPrintReceipt = new System.Windows.Forms.Button();
            this._btnClearAll = new System.Windows.Forms.Button();
            this._btnHoldOrder = new System.Windows.Forms.Button();
            this._btnQuickAdd = new System.Windows.Forms.Button();
            this._btnVoidLast = new System.Windows.Forms.Button();
            this._lblReceiptTitle = new System.Windows.Forms.Label();
            this._rootLayout.SuspendLayout();
            this._topPanel.SuspendLayout();
            this._customerPanel.SuspendLayout();
            this._paymentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._receiptPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gridReceipt)).BeginInit();
            this._totalsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 1;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Controls.Add(this._topPanel, 0, 0);
            this._rootLayout.Controls.Add(this._splitContainer, 0, 1);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 2;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(1720, 825);
            this._rootLayout.TabIndex = 0;
            // 
            // _topPanel
            // 
            this._topPanel.BackColor = System.Drawing.Color.White;
            this._topPanel.Controls.Add(this._txtSearch);
            this._topPanel.Controls.Add(this._btnRefresh);
            this._topPanel.Controls.Add(this._btnReport);
            this._topPanel.Controls.Add(this._customerPanel);
            this._topPanel.Controls.Add(this._paymentPanel);
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topPanel.Location = new System.Drawing.Point(3, 3);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Padding = new System.Windows.Forms.Padding(18, 15, 18, 11);
            this._topPanel.Size = new System.Drawing.Size(1714, 122);
            this._topPanel.TabIndex = 0;
            this._topPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._topPanel_Paint);
            this._topPanel.Resize += new System.EventHandler(this.topPanel_Resize);
            // 
            // _txtSearch
            // 
            this._txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSearch.Location = new System.Drawing.Point(932, 23);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(411, 22);
            this._txtSearch.TabIndex = 1;
            this._txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnRefresh.Location = new System.Drawing.Point(1358, 20);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(110, 36);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // _btnReport
            // 
            this._btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnReport.Location = new System.Drawing.Point(1479, 20);
            this._btnReport.Name = "_btnReport";
            this._btnReport.Size = new System.Drawing.Size(137, 36);
            this._btnReport.TabIndex = 3;
            this._btnReport.Text = "Daily Report";
            this._btnReport.UseVisualStyleBackColor = true;
            this._btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // _customerPanel
            // 
            this._customerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._customerPanel.Controls.Add(this._lblCustomerPhone);
            this._customerPanel.Controls.Add(this._txtCustomerPhone);
            this._customerPanel.Controls.Add(this._btnAddCustomer);
            this._customerPanel.Controls.Add(this._btnViewHistory);
            this._customerPanel.Controls.Add(this._lblCustomerName);
            this._customerPanel.Location = new System.Drawing.Point(18, 69);
            this._customerPanel.Name = "_customerPanel";
            this._customerPanel.Size = new System.Drawing.Size(1445, 53);
            this._customerPanel.TabIndex = 5;
            // 
            // _lblCustomerPhone
            // 
            this._lblCustomerPhone.AutoSize = true;
            this._lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblCustomerPhone.Location = new System.Drawing.Point(11, 16);
            this._lblCustomerPhone.Name = "_lblCustomerPhone";
            this._lblCustomerPhone.Size = new System.Drawing.Size(75, 20);
            this._lblCustomerPhone.TabIndex = 0;
            this._lblCustomerPhone.Text = "Customer:";
            // 
            // _txtCustomerPhone
            // 
            this._txtCustomerPhone.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._txtCustomerPhone.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this._txtCustomerPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtCustomerPhone.Location = new System.Drawing.Point(114, 13);
            this._txtCustomerPhone.Name = "_txtCustomerPhone";
            this._txtCustomerPhone.Size = new System.Drawing.Size(171, 22);
            this._txtCustomerPhone.TabIndex = 1;
            this._txtCustomerPhone.TextChanged += new System.EventHandler(this.txtCustomerPhone_TextChanged);
            // 
            // _btnAddCustomer
            // 
            this._btnAddCustomer.Location = new System.Drawing.Point(297, 11);
            this._btnAddCustomer.Name = "_btnAddCustomer";
            this._btnAddCustomer.Size = new System.Drawing.Size(114, 32);
            this._btnAddCustomer.TabIndex = 2;
            this._btnAddCustomer.Text = "+ New";
            this._btnAddCustomer.UseVisualStyleBackColor = true;
            this._btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // _btnViewHistory
            // 
            this._btnViewHistory.Location = new System.Drawing.Point(423, 11);
            this._btnViewHistory.Name = "_btnViewHistory";
            this._btnViewHistory.Size = new System.Drawing.Size(114, 32);
            this._btnViewHistory.TabIndex = 3;
            this._btnViewHistory.Text = "History";
            this._btnViewHistory.UseVisualStyleBackColor = true;
            this._btnViewHistory.Click += new System.EventHandler(this.btnViewHistory_Click);
            // 
            // _lblCustomerName
            // 
            this._lblCustomerName.AutoSize = true;
            this._lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblCustomerName.Location = new System.Drawing.Point(423, 16);
            this._lblCustomerName.Name = "_lblCustomerName";
            this._lblCustomerName.Size = new System.Drawing.Size(0, 20);
            this._lblCustomerName.TabIndex = 3;
            // 
            // _paymentPanel
            // 
            this._paymentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._paymentPanel.Controls.Add(this._lblPaymentMethod);
            this._paymentPanel.Controls.Add(this._rbCash);
            this._paymentPanel.Controls.Add(this._rbVisa);
            this._paymentPanel.Controls.Add(this._txtPaymentDetails);
            this._paymentPanel.Controls.Add(this._lblPaymentDetails);
            this._paymentPanel.Location = new System.Drawing.Point(18, 133);
            this._paymentPanel.Name = "_paymentPanel";
            this._paymentPanel.Size = new System.Drawing.Size(1445, 48);
            this._paymentPanel.TabIndex = 6;
            // 
            // _lblPaymentMethod
            // 
            this._lblPaymentMethod.AutoSize = true;
            this._lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblPaymentMethod.Location = new System.Drawing.Point(11, 13);
            this._lblPaymentMethod.Name = "_lblPaymentMethod";
            this._lblPaymentMethod.Size = new System.Drawing.Size(68, 20);
            this._lblPaymentMethod.TabIndex = 0;
            this._lblPaymentMethod.Text = "Payment:";
            // 
            // _rbCash
            // 
            this._rbCash.AutoSize = true;
            this._rbCash.Checked = true;
            this._rbCash.Location = new System.Drawing.Point(114, 13);
            this._rbCash.Name = "_rbCash";
            this._rbCash.Size = new System.Drawing.Size(59, 20);
            this._rbCash.TabIndex = 1;
            this._rbCash.TabStop = true;
            this._rbCash.Text = "Cash";
            this._rbCash.UseVisualStyleBackColor = true;
            this._rbCash.CheckedChanged += new System.EventHandler(this.rbPayment_CheckedChanged);
            // 
            // _rbVisa
            // 
            this._rbVisa.AutoSize = true;
            this._rbVisa.Location = new System.Drawing.Point(199, 13);
            this._rbVisa.Name = "_rbVisa";
            this._rbVisa.Size = new System.Drawing.Size(55, 20);
            this._rbVisa.TabIndex = 2;
            this._rbVisa.Text = "Visa";
            this._rbVisa.UseVisualStyleBackColor = true;
            this._rbVisa.CheckedChanged += new System.EventHandler(this.rbPayment_CheckedChanged);
            // 
            // _txtPaymentDetails
            // 
            this._txtPaymentDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtPaymentDetails.Enabled = false;
            this._txtPaymentDetails.Location = new System.Drawing.Point(297, 9);
            this._txtPaymentDetails.MaxLength = 4;
            this._txtPaymentDetails.Name = "_txtPaymentDetails";
            this._txtPaymentDetails.Size = new System.Drawing.Size(114, 22);
            this._txtPaymentDetails.TabIndex = 3;
            // 
            // _lblPaymentDetails
            // 
            this._lblPaymentDetails.AutoSize = true;
            this._lblPaymentDetails.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._lblPaymentDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this._lblPaymentDetails.Location = new System.Drawing.Point(377, 16);
            this._lblPaymentDetails.Name = "_lblPaymentDetails";
            this._lblPaymentDetails.Size = new System.Drawing.Size(0, 19);
            this._lblPaymentDetails.TabIndex = 4;
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(3, 131);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._tabsProducts);
            this._splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(16, 15, 16, 15);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._receiptPanel);
            this._splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 15, 16, 15);
            this._splitContainer.Size = new System.Drawing.Size(1714, 691);
            this._splitContainer.SplitterDistance = 965;
            this._splitContainer.SplitterWidth = 5;
            this._splitContainer.TabIndex = 1;
            // 
            // _tabsProducts
            // 
            this._tabsProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabsProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._tabsProducts.Location = new System.Drawing.Point(16, 15);
            this._tabsProducts.Name = "_tabsProducts";
            this._tabsProducts.SelectedIndex = 0;
            this._tabsProducts.Size = new System.Drawing.Size(933, 661);
            this._tabsProducts.TabIndex = 0;
            // 
            // _receiptPanel
            // 
            this._receiptPanel.BackColor = System.Drawing.Color.White;
            this._receiptPanel.Controls.Add(this._gridReceipt);
            this._receiptPanel.Controls.Add(this._lblStatus);
            this._receiptPanel.Controls.Add(this._lblItemCount);
            this._receiptPanel.Controls.Add(this._totalsPanel);
            this._receiptPanel.Controls.Add(this._lblReceiptTitle);
            this._receiptPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptPanel.Location = new System.Drawing.Point(0, 15);
            this._receiptPanel.Name = "_receiptPanel";
            this._receiptPanel.Padding = new System.Windows.Forms.Padding(16, 15, 16, 15);
            this._receiptPanel.Size = new System.Drawing.Size(728, 661);
            this._receiptPanel.TabIndex = 0;
            // 
            // _gridReceipt
            // 
            this._gridReceipt.AllowUserToAddRows = false;
            this._gridReceipt.AllowUserToDeleteRows = false;
            this._gridReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gridReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._colProductName,
            this._colQuantity,
            this._colUnitPrice,
            this._colSubtotal});
            this._gridReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridReceipt.Location = new System.Drawing.Point(16, 58);
            this._gridReceipt.Name = "_gridReceipt";
            this._gridReceipt.RowHeadersVisible = false;
            this._gridReceipt.RowHeadersWidth = 51;
            this._gridReceipt.RowTemplate.Height = 24;
            this._gridReceipt.Size = new System.Drawing.Size(696, 272);
            this._gridReceipt.TabIndex = 1;
            this._gridReceipt.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridReceipt_CellEndEdit);
            this._gridReceipt.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridReceipt_CellValidating);
            this._gridReceipt.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridReceipt_DataError);
            // 
            // _colProductName
            // 
            this._colProductName.DataPropertyName = "ProductName";
            this._colProductName.FillWeight = 150F;
            this._colProductName.HeaderText = "Item";
            this._colProductName.MinimumWidth = 6;
            this._colProductName.Name = "_colProductName";
            this._colProductName.ReadOnly = true;
            this._colProductName.Width = 125;
            // 
            // _colQuantity
            // 
            this._colQuantity.DataPropertyName = "Quantity";
            this._colQuantity.FillWeight = 45F;
            this._colQuantity.HeaderText = "Qty";
            this._colQuantity.MinimumWidth = 6;
            this._colQuantity.Name = "_colQuantity";
            this._colQuantity.Width = 58;
            // 
            // _colUnitPrice
            // 
            this._colUnitPrice.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle1.Format = "C2";
            this._colUnitPrice.DefaultCellStyle = dataGridViewCellStyle1;
            this._colUnitPrice.FillWeight = 70F;
            this._colUnitPrice.HeaderText = "Price";
            this._colUnitPrice.MinimumWidth = 6;
            this._colUnitPrice.Name = "_colUnitPrice";
            this._colUnitPrice.ReadOnly = true;
            this._colUnitPrice.Width = 125;
            // 
            // _colSubtotal
            // 
            this._colSubtotal.DataPropertyName = "Subtotal";
            dataGridViewCellStyle2.Format = "C2";
            this._colSubtotal.DefaultCellStyle = dataGridViewCellStyle2;
            this._colSubtotal.FillWeight = 80F;
            this._colSubtotal.HeaderText = "Subtotal";
            this._colSubtotal.MinimumWidth = 6;
            this._colSubtotal.Name = "_colSubtotal";
            this._colSubtotal.ReadOnly = true;
            this._colSubtotal.Width = 125;
            // 
            // _lblStatus
            // 
            this._lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this._lblStatus.Location = new System.Drawing.Point(16, 330);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(696, 30);
            this._lblStatus.TabIndex = 2;
            this._lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblItemCount
            // 
            this._lblItemCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblItemCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblItemCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this._lblItemCount.Location = new System.Drawing.Point(560, 58);
            this._lblItemCount.Name = "_lblItemCount";
            this._lblItemCount.Size = new System.Drawing.Size(152, 20);
            this._lblItemCount.TabIndex = 4;
            this._lblItemCount.Text = "0 items";
            this._lblItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _totalsPanel
            // 
            this._totalsPanel.Controls.Add(this._lblCouponCaption);
            this._totalsPanel.Controls.Add(this._txtCoupon);
            this._totalsPanel.Controls.Add(this._btnApplyCoupon);
            this._totalsPanel.Controls.Add(this._btnRemoveCoupon);
            this._totalsPanel.Controls.Add(this._btnManualDiscount);
            this._totalsPanel.Controls.Add(this._lblSubtotal);
            this._totalsPanel.Controls.Add(this._lblDiscount);
            this._totalsPanel.Controls.Add(this._lblTax);
            this._totalsPanel.Controls.Add(this._lblTotal);
            this._totalsPanel.Controls.Add(this._btnRemoveItem);
            this._totalsPanel.Controls.Add(this._btnCompleteOrder);
            this._totalsPanel.Controls.Add(this._btnPrintReceipt);
            this._totalsPanel.Controls.Add(this._btnClearAll);
            this._totalsPanel.Controls.Add(this._btnHoldOrder);
            this._totalsPanel.Controls.Add(this._btnQuickAdd);
            this._totalsPanel.Controls.Add(this._btnVoidLast);
            this._totalsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._totalsPanel.Location = new System.Drawing.Point(16, 360);
            this._totalsPanel.Name = "_totalsPanel";
            this._totalsPanel.Padding = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this._totalsPanel.Size = new System.Drawing.Size(696, 286);
            this._totalsPanel.TabIndex = 3;
            this._totalsPanel.Resize += new System.EventHandler(this.totalsPanel_Resize);
            // 
            // _lblCouponCaption
            // 
            this._lblCouponCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblCouponCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblCouponCaption.Location = new System.Drawing.Point(0, 9);
            this._lblCouponCaption.Name = "_lblCouponCaption";
            this._lblCouponCaption.Size = new System.Drawing.Size(80, 30);
            this._lblCouponCaption.TabIndex = 0;
            this._lblCouponCaption.Text = "Coupon:";
            this._lblCouponCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtCoupon
            // 
            this._txtCoupon.Location = new System.Drawing.Point(85, 11);
            this._txtCoupon.MaxLength = 30;
            this._txtCoupon.Name = "_txtCoupon";
            this._txtCoupon.Size = new System.Drawing.Size(159, 22);
            this._txtCoupon.TabIndex = 1;
            // 
            // _btnApplyCoupon
            // 
            this._btnApplyCoupon.Location = new System.Drawing.Point(254, 9);
            this._btnApplyCoupon.Name = "_btnApplyCoupon";
            this._btnApplyCoupon.Size = new System.Drawing.Size(137, 34);
            this._btnApplyCoupon.TabIndex = 2;
            this._btnApplyCoupon.Text = "Apply";
            this._btnApplyCoupon.UseVisualStyleBackColor = true;
            this._btnApplyCoupon.Click += new System.EventHandler(this.btnApplyCoupon_Click);
            // 
            // _btnRemoveCoupon
            // 
            this._btnRemoveCoupon.Location = new System.Drawing.Point(400, 9);
            this._btnRemoveCoupon.Name = "_btnRemoveCoupon";
            this._btnRemoveCoupon.Size = new System.Drawing.Size(137, 34);
            this._btnRemoveCoupon.TabIndex = 3;
            this._btnRemoveCoupon.Text = "Remove";
            this._btnRemoveCoupon.UseVisualStyleBackColor = true;
            this._btnRemoveCoupon.Click += new System.EventHandler(this.btnRemoveCoupon_Click);
            // 
            // _btnManualDiscount
            // 
            this._btnManualDiscount.Location = new System.Drawing.Point(543, 9);
            this._btnManualDiscount.Name = "_btnManualDiscount";
            this._btnManualDiscount.Size = new System.Drawing.Size(137, 34);
            this._btnManualDiscount.TabIndex = 4;
            this._btnManualDiscount.Text = "Add Discount";
            this._btnManualDiscount.UseVisualStyleBackColor = true;
            this._btnManualDiscount.Click += new System.EventHandler(this.btnManualDiscount_Click);
            // 
            // _lblSubtotal
            // 
            this._lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblSubtotal.Location = new System.Drawing.Point(0, 51);
            this._lblSubtotal.Name = "_lblSubtotal";
            this._lblSubtotal.Size = new System.Drawing.Size(690, 32);
            this._lblSubtotal.TabIndex = 4;
            this._lblSubtotal.Text = "Subtotal: $0.00";
            this._lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDiscount
            // 
            this._lblDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblDiscount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this._lblDiscount.Location = new System.Drawing.Point(0, 85);
            this._lblDiscount.Name = "_lblDiscount";
            this._lblDiscount.Size = new System.Drawing.Size(690, 30);
            this._lblDiscount.TabIndex = 5;
            this._lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lblDiscount.Visible = false;
            // 
            // _lblTax
            // 
            this._lblTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblTax.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblTax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTax.Location = new System.Drawing.Point(0, 117);
            this._lblTax.Name = "_lblTax";
            this._lblTax.Size = new System.Drawing.Size(690, 32);
            this._lblTax.TabIndex = 6;
            this._lblTax.Text = "Tax: $0.00";
            this._lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblTotal
            // 
            this._lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTotal.Location = new System.Drawing.Point(0, 151);
            this._lblTotal.Name = "_lblTotal";
            this._lblTotal.Size = new System.Drawing.Size(690, 43);
            this._lblTotal.TabIndex = 7;
            this._lblTotal.Text = "Total: $0.00";
            this._lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _btnRemoveItem
            // 
            this._btnRemoveItem.Location = new System.Drawing.Point(7, 183);
            this._btnRemoveItem.Name = "_btnRemoveItem";
            this._btnRemoveItem.Size = new System.Drawing.Size(149, 41);
            this._btnRemoveItem.TabIndex = 8;
            this._btnRemoveItem.Text = "Remove Item";
            this._btnRemoveItem.UseVisualStyleBackColor = true;
            this._btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // _btnCompleteOrder
            // 
            this._btnCompleteOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCompleteOrder.Location = new System.Drawing.Point(273, 201);
            this._btnCompleteOrder.Name = "_btnCompleteOrder";
            this._btnCompleteOrder.Size = new System.Drawing.Size(183, 41);
            this._btnCompleteOrder.TabIndex = 9;
            this._btnCompleteOrder.Text = "Complete Order";
            this._btnCompleteOrder.UseVisualStyleBackColor = true;
            this._btnCompleteOrder.Click += new System.EventHandler(this.btnCompleteOrder_Click);
            // 
            // _btnPrintReceipt
            // 
            this._btnPrintReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnPrintReceipt.Location = new System.Drawing.Point(273, 245);
            this._btnPrintReceipt.Name = "_btnPrintReceipt";
            this._btnPrintReceipt.Size = new System.Drawing.Size(183, 41);
            this._btnPrintReceipt.TabIndex = 10;
            this._btnPrintReceipt.Text = "Print Receipt";
            this._btnPrintReceipt.UseVisualStyleBackColor = true;
            this._btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // _btnClearAll
            // 
            this._btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClearAll.Location = new System.Drawing.Point(467, 245);
            this._btnClearAll.Name = "_btnClearAll";
            this._btnClearAll.Size = new System.Drawing.Size(149, 41);
            this._btnClearAll.TabIndex = 11;
            this._btnClearAll.Text = "Clear All";
            this._btnClearAll.UseVisualStyleBackColor = true;
            this._btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // _btnHoldOrder
            // 
            this._btnHoldOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnHoldOrder.Location = new System.Drawing.Point(273, 245);
            this._btnHoldOrder.Name = "_btnHoldOrder";
            this._btnHoldOrder.Size = new System.Drawing.Size(183, 41);
            this._btnHoldOrder.TabIndex = 12;
            this._btnHoldOrder.Text = "Hold Order";
            this._btnHoldOrder.UseVisualStyleBackColor = true;
            this._btnHoldOrder.Click += new System.EventHandler(this.btnHoldOrder_Click);
            // 
            // _btnQuickAdd
            // 
            this._btnQuickAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnQuickAdd.Location = new System.Drawing.Point(0, 245);
            this._btnQuickAdd.Name = "_btnQuickAdd";
            this._btnQuickAdd.Size = new System.Drawing.Size(130, 41);
            this._btnQuickAdd.TabIndex = 13;
            this._btnQuickAdd.Text = "Quick Add";
            this._btnQuickAdd.UseVisualStyleBackColor = true;
            this._btnQuickAdd.Click += new System.EventHandler(this.btnQuickAdd_Click);
            // 
            // _btnVoidLast
            // 
            this._btnVoidLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnVoidLast.Location = new System.Drawing.Point(140, 245);
            this._btnVoidLast.Name = "_btnVoidLast";
            this._btnVoidLast.Size = new System.Drawing.Size(130, 41);
            this._btnVoidLast.TabIndex = 14;
            this._btnVoidLast.Text = "Void Last";
            this._btnVoidLast.UseVisualStyleBackColor = true;
            this._btnVoidLast.Click += new System.EventHandler(this.btnVoidLast_Click);
            // 
            // _lblReceiptTitle
            // 
            this._lblReceiptTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblReceiptTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblReceiptTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblReceiptTitle.Location = new System.Drawing.Point(16, 15);
            this._lblReceiptTitle.Name = "_lblReceiptTitle";
            this._lblReceiptTitle.Size = new System.Drawing.Size(696, 43);
            this._lblReceiptTitle.TabIndex = 0;
            this._lblReceiptTitle.Text = "Receipt";
            this._lblReceiptTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1720, 825);
            this.Controls.Add(this._rootLayout);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "frmPOS";
            this.Text = "Point of Sale (POS)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._rootLayout.ResumeLayout(false);
            this._topPanel.ResumeLayout(false);
            this._topPanel.PerformLayout();
            this._customerPanel.ResumeLayout(false);
            this._customerPanel.PerformLayout();
            this._paymentPanel.ResumeLayout(false);
            this._paymentPanel.PerformLayout();
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._receiptPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._gridReceipt)).EndInit();
            this._totalsPanel.ResumeLayout(false);
            this._totalsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private System.Windows.Forms.Panel _topPanel;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Button _btnReport;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.TabControl _tabsProducts;
        private System.Windows.Forms.Panel _receiptPanel;
        private System.Windows.Forms.DataGridView _gridReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colSubtotal;
        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.Panel _totalsPanel;
        private System.Windows.Forms.Label _lblCouponCaption;
        private System.Windows.Forms.TextBox _txtCoupon;
        private System.Windows.Forms.Button _btnApplyCoupon;
        private System.Windows.Forms.Button _btnRemoveCoupon;
        private System.Windows.Forms.Button _btnManualDiscount;
        private System.Windows.Forms.Label _lblSubtotal;
        private System.Windows.Forms.Label _lblDiscount;
        private System.Windows.Forms.Label _lblTax;
        private System.Windows.Forms.Label _lblTotal;
        private System.Windows.Forms.Button _btnRemoveItem;
        private System.Windows.Forms.Button _btnCompleteOrder;
        private System.Windows.Forms.Button _btnPrintReceipt;
        private System.Windows.Forms.Label _lblReceiptTitle;
        private System.Windows.Forms.Label _lblItemCount;
        private System.Windows.Forms.Button _btnClearAll;
        private System.Windows.Forms.Button _btnHoldOrder;
        private System.Windows.Forms.Button _btnQuickAdd;
        private System.Windows.Forms.Button _btnVoidLast;
        private System.Windows.Forms.Panel _customerPanel;
        private System.Windows.Forms.Label _lblCustomerPhone;
        private System.Windows.Forms.TextBox _txtCustomerPhone;
        private System.Windows.Forms.Button _btnAddCustomer;
        private System.Windows.Forms.Button _btnViewHistory;
        private System.Windows.Forms.Label _lblCustomerName;
        private System.Windows.Forms.Panel _paymentPanel;
        private System.Windows.Forms.Label _lblPaymentMethod;
        private System.Windows.Forms.RadioButton _rbCash;
        private System.Windows.Forms.RadioButton _rbVisa;
        private System.Windows.Forms.TextBox _txtPaymentDetails;
        private System.Windows.Forms.Label _lblPaymentDetails;
    }
}
