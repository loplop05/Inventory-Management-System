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
            this._lblTitle = new System.Windows.Forms.Label();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._btnRefresh = new System.Windows.Forms.Button();
            this._btnReport = new System.Windows.Forms.Button();
            this._btnClose = new System.Windows.Forms.Button();
            this._customerPanel = new System.Windows.Forms.Panel();
            this._lblCustomerPhone = new System.Windows.Forms.Label();
            this._txtCustomerPhone = new System.Windows.Forms.TextBox();
            this._btnAddCustomer = new System.Windows.Forms.Button();
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
            this._totalsPanel = new System.Windows.Forms.Panel();
            this._lblSubtotal = new System.Windows.Forms.Label();
            this._lblTax = new System.Windows.Forms.Label();
            this._lblTotal = new System.Windows.Forms.Label();
            this._btnRemoveItem = new System.Windows.Forms.Button();
            this._btnCompleteOrder = new System.Windows.Forms.Button();
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
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 186F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(1302, 773);
            this._rootLayout.TabIndex = 0;
            // 
            // _topPanel
            // 
            this._topPanel.BackColor = System.Drawing.Color.White;
            this._topPanel.Controls.Add(this._lblTitle);
            this._topPanel.Controls.Add(this._txtSearch);
            this._topPanel.Controls.Add(this._btnRefresh);
            this._topPanel.Controls.Add(this._btnReport);
            this._topPanel.Controls.Add(this._btnClose);
            this._topPanel.Controls.Add(this._customerPanel);
            this._topPanel.Controls.Add(this._paymentPanel);
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topPanel.Location = new System.Drawing.Point(3, 3);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Padding = new System.Windows.Forms.Padding(16, 14, 16, 10);
            this._topPanel.Size = new System.Drawing.Size(1296, 180);
            this._topPanel.TabIndex = 0;
            this._topPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._topPanel_Paint);
            this._topPanel.Resize += new System.EventHandler(this.topPanel_Resize);
            // 
            // _lblTitle
            // 
            this._lblTitle.AutoSize = true;
            this._lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTitle.Location = new System.Drawing.Point(16, 17);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(196, 41);
            this._lblTitle.TabIndex = 0;
            this._lblTitle.Text = "Point of Sale";
            // 
            // _txtSearch
            // 
            this._txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSearch.Location = new System.Drawing.Point(612, 22);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(360, 30);
            this._txtSearch.TabIndex = 1;
            this._txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnRefresh.Location = new System.Drawing.Point(984, 19);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(96, 34);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // _btnReport
            // 
            this._btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnReport.Location = new System.Drawing.Point(1090, 19);
            this._btnReport.Name = "_btnReport";
            this._btnReport.Size = new System.Drawing.Size(120, 34);
            this._btnReport.TabIndex = 3;
            this._btnReport.Text = "Daily Report";
            this._btnReport.UseVisualStyleBackColor = true;
            this._btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // _btnClose
            // 
            this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnClose.Location = new System.Drawing.Point(1220, 19);
            this._btnClose.Name = "_btnClose";
            this._btnClose.Size = new System.Drawing.Size(76, 34);
            this._btnClose.TabIndex = 4;
            this._btnClose.Text = "Close";
            this._btnClose.UseVisualStyleBackColor = true;
            this._btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // _customerPanel
            // 
            this._customerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._customerPanel.Controls.Add(this._lblCustomerPhone);
            this._customerPanel.Controls.Add(this._txtCustomerPhone);
            this._customerPanel.Controls.Add(this._btnAddCustomer);
            this._customerPanel.Controls.Add(this._lblCustomerName);
            this._customerPanel.Location = new System.Drawing.Point(16, 65);
            this._customerPanel.Name = "_customerPanel";
            this._customerPanel.Size = new System.Drawing.Size(1264, 50);
            this._customerPanel.TabIndex = 5;
            // 
            // _lblCustomerPhone
            // 
            this._lblCustomerPhone.AutoSize = true;
            this._lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this._lblCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblCustomerPhone.Location = new System.Drawing.Point(10, 15);
            this._lblCustomerPhone.Name = "_lblCustomerPhone";
            this._lblCustomerPhone.Size = new System.Drawing.Size(85, 20);
            this._lblCustomerPhone.TabIndex = 0;
            this._lblCustomerPhone.Text = "Customer:";
            // 
            // _txtCustomerPhone
            // 
            this._txtCustomerPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtCustomerPhone.Location = new System.Drawing.Point(100, 12);
            this._txtCustomerPhone.Name = "_txtCustomerPhone";
            this._txtCustomerPhone.Size = new System.Drawing.Size(150, 26);
            this._txtCustomerPhone.TabIndex = 1;
            this._txtCustomerPhone.TextChanged += new System.EventHandler(this.txtCustomerPhone_TextChanged);
            // 
            // _btnAddCustomer
            // 
            this._btnAddCustomer.Location = new System.Drawing.Point(260, 10);
            this._btnAddCustomer.Name = "_btnAddCustomer";
            this._btnAddCustomer.Size = new System.Drawing.Size(100, 30);
            this._btnAddCustomer.TabIndex = 2;
            this._btnAddCustomer.Text = "+ New";
            this._btnAddCustomer.UseVisualStyleBackColor = true;
            this._btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // _lblCustomerName
            // 
            this._lblCustomerName.AutoSize = true;
            this._lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblCustomerName.Location = new System.Drawing.Point(370, 15);
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
            this._paymentPanel.Location = new System.Drawing.Point(16, 125);
            this._paymentPanel.Name = "_paymentPanel";
            this._paymentPanel.Size = new System.Drawing.Size(1264, 45);
            this._paymentPanel.TabIndex = 6;
            // 
            // _lblPaymentMethod
            // 
            this._lblPaymentMethod.AutoSize = true;
            this._lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this._lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblPaymentMethod.Location = new System.Drawing.Point(10, 12);
            this._lblPaymentMethod.Name = "_lblPaymentMethod";
            this._lblPaymentMethod.Size = new System.Drawing.Size(90, 20);
            this._lblPaymentMethod.TabIndex = 0;
            this._lblPaymentMethod.Text = "Payment:";
            // 
            // _rbCash
            // 
            this._rbCash.AutoSize = true;
            this._rbCash.Checked = true;
            this._rbCash.Location = new System.Drawing.Point(100, 12);
            this._rbCash.Name = "_rbCash";
            this._rbCash.Size = new System.Drawing.Size(55, 24);
            this._rbCash.TabIndex = 1;
            this._rbCash.TabStop = true;
            this._rbCash.Text = "Cash";
            this._rbCash.UseVisualStyleBackColor = true;
            this._rbCash.CheckedChanged += new System.EventHandler(this.rbPayment_CheckedChanged);
            // 
            // _rbVisa
            // 
            this._rbVisa.AutoSize = true;
            this._rbVisa.Location = new System.Drawing.Point(160, 12);
            this._rbVisa.Name = "_rbVisa";
            this._rbVisa.Size = new System.Drawing.Size(50, 24);
            this._rbVisa.TabIndex = 2;
            this._rbVisa.Text = "Visa";
            this._rbVisa.UseVisualStyleBackColor = true;
            this._rbVisa.CheckedChanged += new System.EventHandler(this.rbPayment_CheckedChanged);
            // 
            // _txtPaymentDetails
            // 
            this._txtPaymentDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtPaymentDetails.Enabled = false;
            this._txtPaymentDetails.Location = new System.Drawing.Point(220, 12);
            this._txtPaymentDetails.Name = "_txtPaymentDetails";
            this._txtPaymentDetails.Size = new System.Drawing.Size(100, 26);
            this._txtPaymentDetails.TabIndex = 3;
            this._txtPaymentDetails.MaxLength = 4;
            // 
            // _lblPaymentDetails
            // 
            this._lblPaymentDetails.AutoSize = true;
            this._lblPaymentDetails.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular);
            this._lblPaymentDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this._lblPaymentDetails.Location = new System.Drawing.Point(330, 15);
            this._lblPaymentDetails.Name = "_lblPaymentDetails";
            this._lblPaymentDetails.Size = new System.Drawing.Size(0, 17);
            this._lblPaymentDetails.TabIndex = 4;
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(3, 139);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._tabsProducts);
            this._splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(14);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._receiptPanel);
            this._splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(0, 14, 14, 14);
            this._splitContainer.Size = new System.Drawing.Size(1296, 631);
            this._splitContainer.SplitterDistance = 820;
            this._splitContainer.TabIndex = 1;
            // 
            // _tabsProducts
            // 
            this._tabsProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabsProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._tabsProducts.Location = new System.Drawing.Point(14, 14);
            this._tabsProducts.Name = "_tabsProducts";
            this._tabsProducts.SelectedIndex = 0;
            this._tabsProducts.Size = new System.Drawing.Size(792, 603);
            this._tabsProducts.TabIndex = 0;
            // 
            // _receiptPanel
            // 
            this._receiptPanel.BackColor = System.Drawing.Color.White;
            this._receiptPanel.Controls.Add(this._gridReceipt);
            this._receiptPanel.Controls.Add(this._lblStatus);
            this._receiptPanel.Controls.Add(this._totalsPanel);
            this._receiptPanel.Controls.Add(this._lblReceiptTitle);
            this._receiptPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptPanel.Location = new System.Drawing.Point(0, 14);
            this._receiptPanel.Name = "_receiptPanel";
            this._receiptPanel.Padding = new System.Windows.Forms.Padding(14);
            this._receiptPanel.Size = new System.Drawing.Size(458, 603);
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
            this._gridReceipt.Location = new System.Drawing.Point(14, 52);
            this._gridReceipt.Name = "_gridReceipt";
            this._gridReceipt.RowHeadersVisible = false;
            this._gridReceipt.RowHeadersWidth = 51;
            this._gridReceipt.RowTemplate.Height = 24;
            this._gridReceipt.Size = new System.Drawing.Size(430, 331);
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
            this._lblStatus.Location = new System.Drawing.Point(14, 383);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(430, 28);
            this._lblStatus.TabIndex = 2;
            this._lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _totalsPanel
            // 
            this._totalsPanel.Controls.Add(this._lblSubtotal);
            this._totalsPanel.Controls.Add(this._lblTax);
            this._totalsPanel.Controls.Add(this._lblTotal);
            this._totalsPanel.Controls.Add(this._btnRemoveItem);
            this._totalsPanel.Controls.Add(this._btnCompleteOrder);
            this._totalsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._totalsPanel.Location = new System.Drawing.Point(14, 411);
            this._totalsPanel.Name = "_totalsPanel";
            this._totalsPanel.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this._totalsPanel.Size = new System.Drawing.Size(430, 178);
            this._totalsPanel.TabIndex = 3;
            this._totalsPanel.Resize += new System.EventHandler(this.totalsPanel_Resize);
            // 
            // _lblSubtotal
            // 
            this._lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblSubtotal.Location = new System.Drawing.Point(0, 0);
            this._lblSubtotal.Name = "_lblSubtotal";
            this._lblSubtotal.Size = new System.Drawing.Size(430, 32);
            this._lblSubtotal.TabIndex = 0;
            this._lblSubtotal.Text = "Subtotal: $0.00";
            this._lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblTax
            // 
            this._lblTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblTax.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblTax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTax.Location = new System.Drawing.Point(0, 34);
            this._lblTax.Name = "_lblTax";
            this._lblTax.Size = new System.Drawing.Size(430, 32);
            this._lblTax.TabIndex = 1;
            this._lblTax.Text = "Tax: $0.00";
            this._lblTax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblTotal
            // 
            this._lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTotal.Location = new System.Drawing.Point(0, 68);
            this._lblTotal.Name = "_lblTotal";
            this._lblTotal.Size = new System.Drawing.Size(430, 40);
            this._lblTotal.TabIndex = 2;
            this._lblTotal.Text = "Total: $0.00";
            this._lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _btnRemoveItem
            // 
            this._btnRemoveItem.Location = new System.Drawing.Point(0, 120);
            this._btnRemoveItem.Name = "_btnRemoveItem";
            this._btnRemoveItem.Size = new System.Drawing.Size(130, 38);
            this._btnRemoveItem.TabIndex = 3;
            this._btnRemoveItem.Text = "Remove Item";
            this._btnRemoveItem.UseVisualStyleBackColor = true;
            this._btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // _btnCompleteOrder
            // 
            this._btnCompleteOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCompleteOrder.Location = new System.Drawing.Point(270, 120);
            this._btnCompleteOrder.Name = "_btnCompleteOrder";
            this._btnCompleteOrder.Size = new System.Drawing.Size(160, 38);
            this._btnCompleteOrder.TabIndex = 4;
            this._btnCompleteOrder.Text = "Complete Order";
            this._btnCompleteOrder.UseVisualStyleBackColor = true;
            this._btnCompleteOrder.Click += new System.EventHandler(this.btnCompleteOrder_Click);
            // 
            // _lblReceiptTitle
            // 
            this._lblReceiptTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblReceiptTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._lblReceiptTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblReceiptTitle.Location = new System.Drawing.Point(14, 14);
            this._lblReceiptTitle.Name = "_lblReceiptTitle";
            this._lblReceiptTitle.Size = new System.Drawing.Size(430, 38);
            this._lblReceiptTitle.TabIndex = 0;
            this._lblReceiptTitle.Text = "Receipt";
            // 
            // frmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 773);
            this.Controls.Add(this._rootLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.Name = "frmPOS";
            this.Text = "Point of Sale";
            this.Load += new System.EventHandler(this.frmPOS_Load);
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
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private System.Windows.Forms.Panel _topPanel;
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Button _btnReport;
        private System.Windows.Forms.Button _btnClose;
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
        private System.Windows.Forms.Label _lblSubtotal;
        private System.Windows.Forms.Label _lblTax;
        private System.Windows.Forms.Label _lblTotal;
        private System.Windows.Forms.Button _btnRemoveItem;
        private System.Windows.Forms.Button _btnCompleteOrder;
        private System.Windows.Forms.Label _lblReceiptTitle;
        private System.Windows.Forms.Panel _customerPanel;
        private System.Windows.Forms.Label _lblCustomerPhone;
        private System.Windows.Forms.TextBox _txtCustomerPhone;
        private System.Windows.Forms.Button _btnAddCustomer;
        private System.Windows.Forms.Label _lblCustomerName;
        private System.Windows.Forms.Panel _paymentPanel;
        private System.Windows.Forms.Label _lblPaymentMethod;
        private System.Windows.Forms.RadioButton _rbCash;
        private System.Windows.Forms.RadioButton _rbVisa;
        private System.Windows.Forms.TextBox _txtPaymentDetails;
        private System.Windows.Forms.Label _lblPaymentDetails;
    }
}
