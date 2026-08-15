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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this._contentPanel = new System.Windows.Forms.Panel();
            this._mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._topPanel = new System.Windows.Forms.Panel();
            this._paymentPanel = new System.Windows.Forms.Panel();
            this._lblPaymentMethod = new System.Windows.Forms.Label();
            this._cbCash = new System.Windows.Forms.CheckBox();
            this._cbVisa = new System.Windows.Forms.CheckBox();
            this._txtPaymentDetails = new System.Windows.Forms.TextBox();
            this._lblPaymentDetails = new System.Windows.Forms.Label();
            this._customerPanel = new System.Windows.Forms.Panel();
            this._lblCustomerPhone = new System.Windows.Forms.Label();
            this._txtCustomerPhone = new System.Windows.Forms.TextBox();
            this._btnAddCustomer = new System.Windows.Forms.Button();
            this._lblCustomerName = new System.Windows.Forms.Label();
            this._barcodePanel = new System.Windows.Forms.Panel();
            this._btnAddByBarcode = new System.Windows.Forms.Button();
            this._txtBarcode = new System.Windows.Forms.TextBox();
            this._lblBarcode = new System.Windows.Forms.Label();
            this._txtSearch = new System.Windows.Forms.TextBox();
            this._btnRefresh = new System.Windows.Forms.Button();
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
            this.button1 = new System.Windows.Forms.Button();
            this._lblSubtotal = new System.Windows.Forms.Label();
            this._lblTax = new System.Windows.Forms.Label();
            this._lblTotal = new System.Windows.Forms.Label();
            this._btnRemoveItem = new System.Windows.Forms.Button();
            this._btnCompleteOrder = new System.Windows.Forms.Button();
            this._lblReceiptTitle = new System.Windows.Forms.Label();
            this._contextMenuReceipt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemEditQty = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemApplyDiscount = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemAddNote = new System.Windows.Forms.ToolStripMenuItem();
            this._contextMenuProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuItemProductAddToReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemProductViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this._menuItemProductEdit = new System.Windows.Forms.ToolStripMenuItem();
            this._contentPanel.SuspendLayout();
            this._mainLayoutPanel.SuspendLayout();
            this._topPanel.SuspendLayout();
            this._paymentPanel.SuspendLayout();
            this._customerPanel.SuspendLayout();
            this._barcodePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._receiptPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gridReceipt)).BeginInit();
            this._totalsPanel.SuspendLayout();
            this._contextMenuReceipt.SuspendLayout();
            this._contextMenuProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this._mainLayoutPanel);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(0, 0);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Size = new System.Drawing.Size(1473, 749);
            this._contentPanel.TabIndex = 1;
            // 
            // _mainLayoutPanel
            // 
            this._mainLayoutPanel.ColumnCount = 1;
            this._mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Controls.Add(this._topPanel, 0, 0);
            this._mainLayoutPanel.Controls.Add(this._splitContainer, 0, 1);
            this._mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._mainLayoutPanel.Name = "_mainLayoutPanel";
            this._mainLayoutPanel.RowCount = 2;
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this._mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._mainLayoutPanel.Size = new System.Drawing.Size(1473, 749);
            this._mainLayoutPanel.TabIndex = 1;
            // 
            // _topPanel
            // 
            this._topPanel.BackColor = System.Drawing.Color.White;
            this._topPanel.Controls.Add(this._paymentPanel);
            this._topPanel.Controls.Add(this._customerPanel);
            this._topPanel.Controls.Add(this._barcodePanel);
            this._topPanel.Controls.Add(this._txtSearch);
            this._topPanel.Controls.Add(this._btnRefresh);
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._topPanel.Location = new System.Drawing.Point(3, 3);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this._topPanel.Size = new System.Drawing.Size(1467, 129);
            this._topPanel.TabIndex = 0;
            this._topPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._topPanel_Paint);
            this._topPanel.Resize += new System.EventHandler(this.topPanel_Resize);
            // 
            // _paymentPanel
            // 
            this._paymentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._paymentPanel.Controls.Add(this._lblPaymentMethod);
            this._paymentPanel.Controls.Add(this._cbCash);
            this._paymentPanel.Controls.Add(this._cbVisa);
            this._paymentPanel.Controls.Add(this._txtPaymentDetails);
            this._paymentPanel.Controls.Add(this._lblPaymentDetails);
            this._paymentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._paymentPanel.Location = new System.Drawing.Point(16, 98);
            this._paymentPanel.Name = "_paymentPanel";
            this._paymentPanel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this._paymentPanel.Size = new System.Drawing.Size(1435, 45);
            this._paymentPanel.TabIndex = 6;
            // 
            // _lblPaymentMethod
            // 
            this._lblPaymentMethod.AutoSize = true;
            this._lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblPaymentMethod.Location = new System.Drawing.Point(30, 5);
            this._lblPaymentMethod.Name = "_lblPaymentMethod";
            this._lblPaymentMethod.Size = new System.Drawing.Size(68, 20);
            this._lblPaymentMethod.TabIndex = 0;
            this._lblPaymentMethod.Text = "Payment:";
            // 
            // _cbCash
            // 
            this._cbCash.AutoSize = true;
            this._cbCash.Checked = true;
            this._cbCash.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cbCash.Location = new System.Drawing.Point(120, 5);
            this._cbCash.Name = "_cbCash";
            this._cbCash.Size = new System.Drawing.Size(69, 27);
            this._cbCash.TabIndex = 1;
            this._cbCash.Text = "Cash";
            this._cbCash.UseVisualStyleBackColor = true;
            this._cbCash.CheckedChanged += new System.EventHandler(this.cbPayment_CheckedChanged);
            // 
            // _cbVisa
            // 
            this._cbVisa.AutoSize = true;
            this._cbVisa.Location = new System.Drawing.Point(221, 5);
            this._cbVisa.Name = "_cbVisa";
            this._cbVisa.Size = new System.Drawing.Size(63, 27);
            this._cbVisa.TabIndex = 2;
            this._cbVisa.Text = "Visa";
            this._cbVisa.UseVisualStyleBackColor = true;
            this._cbVisa.CheckedChanged += new System.EventHandler(this.cbPayment_CheckedChanged);
            // 
            // _txtPaymentDetails
            // 
            this._txtPaymentDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtPaymentDetails.Enabled = false;
            this._txtPaymentDetails.Location = new System.Drawing.Point(329, 2);
            this._txtPaymentDetails.MaxLength = 4;
            this._txtPaymentDetails.Name = "_txtPaymentDetails";
            this._txtPaymentDetails.Size = new System.Drawing.Size(100, 30);
            this._txtPaymentDetails.TabIndex = 3;
            // 
            // _lblPaymentDetails
            // 
            this._lblPaymentDetails.AutoSize = true;
            this._lblPaymentDetails.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._lblPaymentDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this._lblPaymentDetails.Location = new System.Drawing.Point(350, 8);
            this._lblPaymentDetails.Name = "_lblPaymentDetails";
            this._lblPaymentDetails.Size = new System.Drawing.Size(0, 19);
            this._lblPaymentDetails.TabIndex = 4;
            // 
            // _customerPanel
            // 
            this._customerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._customerPanel.Controls.Add(this._lblCustomerPhone);
            this._customerPanel.Controls.Add(this._txtCustomerPhone);
            this._customerPanel.Controls.Add(this._btnAddCustomer);
            this._customerPanel.Controls.Add(this._lblCustomerName);
            this._customerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._customerPanel.Location = new System.Drawing.Point(16, 52);
            this._customerPanel.Name = "_customerPanel";
            this._customerPanel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this._customerPanel.Size = new System.Drawing.Size(1435, 46);
            this._customerPanel.TabIndex = 5;
            // 
            // _lblCustomerPhone
            // 
            this._lblCustomerPhone.AutoSize = true;
            this._lblCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblCustomerPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblCustomerPhone.Location = new System.Drawing.Point(15, 9);
            this._lblCustomerPhone.Name = "_lblCustomerPhone";
            this._lblCustomerPhone.Size = new System.Drawing.Size(75, 20);
            this._lblCustomerPhone.TabIndex = 0;
            this._lblCustomerPhone.Text = "Customer:";
            // 
            // _txtCustomerPhone
            // 
            this._txtCustomerPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtCustomerPhone.Location = new System.Drawing.Point(105, 6);
            this._txtCustomerPhone.Name = "_txtCustomerPhone";
            this._txtCustomerPhone.Size = new System.Drawing.Size(150, 30);
            this._txtCustomerPhone.TabIndex = 1;
            this._txtCustomerPhone.TextChanged += new System.EventHandler(this.txtCustomerPhone_TextChanged);
            // 
            // _btnAddCustomer
            // 
            this._btnAddCustomer.Location = new System.Drawing.Point(268, 6);
            this._btnAddCustomer.Name = "_btnAddCustomer";
            this._btnAddCustomer.Size = new System.Drawing.Size(107, 32);
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
            this._lblCustomerName.Location = new System.Drawing.Point(375, 9);
            this._lblCustomerName.Name = "_lblCustomerName";
            this._lblCustomerName.Size = new System.Drawing.Size(0, 20);
            this._lblCustomerName.TabIndex = 3;
            // 
            // _barcodePanel
            // 
            this._barcodePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._barcodePanel.Controls.Add(this._btnAddByBarcode);
            this._barcodePanel.Controls.Add(this._txtBarcode);
            this._barcodePanel.Controls.Add(this._lblBarcode);
            this._barcodePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._barcodePanel.Location = new System.Drawing.Point(16, 10);
            this._barcodePanel.Name = "_barcodePanel";
            this._barcodePanel.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this._barcodePanel.Size = new System.Drawing.Size(1435, 42);
            this._barcodePanel.TabIndex = 7;
            // 
            // _btnAddByBarcode
            // 
            this._btnAddByBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAddByBarcode.Location = new System.Drawing.Point(1319, 3);
            this._btnAddByBarcode.Name = "_btnAddByBarcode";
            this._btnAddByBarcode.Size = new System.Drawing.Size(113, 36);
            this._btnAddByBarcode.TabIndex = 2;
            this._btnAddByBarcode.Text = "Add";
            this._btnAddByBarcode.UseVisualStyleBackColor = true;
            this._btnAddByBarcode.Click += new System.EventHandler(this._btnAddByBarcode_Click);
            // 
            // _txtBarcode
            // 
            this._txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtBarcode.Font = new System.Drawing.Font("Segoe UI", 14.2F);
            this._txtBarcode.Location = new System.Drawing.Point(772, 0);
            this._txtBarcode.Name = "_txtBarcode";
            this._txtBarcode.Size = new System.Drawing.Size(541, 39);
            this._txtBarcode.TabIndex = 1;
            this._txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this._txtBarcode_KeyDown);
            // 
            // _lblBarcode
            // 
            this._lblBarcode.AutoSize = true;
            this._lblBarcode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lblBarcode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._lblBarcode.Location = new System.Drawing.Point(664, 11);
            this._lblBarcode.Name = "_lblBarcode";
            this._lblBarcode.Size = new System.Drawing.Size(67, 20);
            this._lblBarcode.TabIndex = 0;
            this._lblBarcode.Text = "Barcode:";
            // 
            // _txtSearch
            // 
            this._txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSearch.Location = new System.Drawing.Point(1041, 10);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Size = new System.Drawing.Size(300, 30);
            this._txtSearch.TabIndex = 1;
            this._txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // _btnRefresh
            // 
            this._btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnRefresh.Location = new System.Drawing.Point(1351, 7);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(100, 34);
            this._btnRefresh.TabIndex = 2;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.UseVisualStyleBackColor = true;
            this._btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(3, 138);
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
            this._splitContainer.Size = new System.Drawing.Size(1467, 608);
            this._splitContainer.SplitterDistance = 911;
            this._splitContainer.TabIndex = 1;
            // 
            // _tabsProducts
            // 
            this._tabsProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabsProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._tabsProducts.Location = new System.Drawing.Point(14, 14);
            this._tabsProducts.Name = "_tabsProducts";
            this._tabsProducts.SelectedIndex = 0;
            this._tabsProducts.Size = new System.Drawing.Size(883, 580);
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
            this._receiptPanel.Size = new System.Drawing.Size(538, 580);
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
            this._gridReceipt.Size = new System.Drawing.Size(510, 286);
            this._gridReceipt.TabIndex = 1;
            this._gridReceipt.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridReceipt_CellEndEdit);
            this._gridReceipt.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridReceipt_CellMouseClick);
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
            this._lblStatus.Location = new System.Drawing.Point(14, 338);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(510, 28);
            this._lblStatus.TabIndex = 2;
            this._lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _totalsPanel
            // 
            this._totalsPanel.Controls.Add(this.button1);
            this._totalsPanel.Controls.Add(this._lblSubtotal);
            this._totalsPanel.Controls.Add(this._lblTax);
            this._totalsPanel.Controls.Add(this._lblTotal);
            this._totalsPanel.Controls.Add(this._btnRemoveItem);
            this._totalsPanel.Controls.Add(this._btnCompleteOrder);
            this._totalsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._totalsPanel.Location = new System.Drawing.Point(14, 366);
            this._totalsPanel.Name = "_totalsPanel";
            this._totalsPanel.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this._totalsPanel.Size = new System.Drawing.Size(510, 200);
            this._totalsPanel.TabIndex = 3;
            this._totalsPanel.Resize += new System.EventHandler(this.totalsPanel_Resize);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 56);
            this.button1.TabIndex = 5;
            this.button1.Text = "Actions ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _lblSubtotal
            // 
            this._lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblSubtotal.Location = new System.Drawing.Point(208, 2);
            this._lblSubtotal.Name = "_lblSubtotal";
            this._lblSubtotal.Size = new System.Drawing.Size(298, 32);
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
            this._lblTax.Location = new System.Drawing.Point(255, 34);
            this._lblTax.Name = "_lblTax";
            this._lblTax.Size = new System.Drawing.Size(211, 32);
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
            this._lblTotal.Location = new System.Drawing.Point(208, 66);
            this._lblTotal.Name = "_lblTotal";
            this._lblTotal.Size = new System.Drawing.Size(258, 40);
            this._lblTotal.TabIndex = 2;
            this._lblTotal.Text = "Total: $0.00";
            this._lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _btnRemoveItem
            // 
            this._btnRemoveItem.Location = new System.Drawing.Point(114, 129);
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
            this._btnCompleteOrder.Location = new System.Drawing.Point(346, 129);
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
            this._lblReceiptTitle.Size = new System.Drawing.Size(510, 38);
            this._lblReceiptTitle.TabIndex = 0;
            this._lblReceiptTitle.Text = "Receipt";
            // 
            // _contextMenuReceipt
            // 
            this._contextMenuReceipt.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._contextMenuReceipt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemEditQty,
            this._menuItemApplyDiscount,
            this._menuItemRemove,
            this._menuItemDuplicate,
            this._menuItemAddNote});
            this._contextMenuReceipt.Name = "_contextMenuReceipt";
            this._contextMenuReceipt.Size = new System.Drawing.Size(214, 124);
            // 
            // _menuItemEditQty
            // 
            this._menuItemEditQty.Name = "_menuItemEditQty";
            this._menuItemEditQty.Size = new System.Drawing.Size(213, 24);
            this._menuItemEditQty.Text = "Edit Quantity";
            this._menuItemEditQty.Click += new System.EventHandler(this.menuItemEditQty_Click);
            // 
            // _menuItemApplyDiscount
            // 
            this._menuItemApplyDiscount.Name = "_menuItemApplyDiscount";
            this._menuItemApplyDiscount.Size = new System.Drawing.Size(213, 24);
            this._menuItemApplyDiscount.Text = "Apply Item Discount";
            this._menuItemApplyDiscount.Click += new System.EventHandler(this.menuItemApplyDiscount_Click);
            // 
            // _menuItemRemove
            // 
            this._menuItemRemove.Name = "_menuItemRemove";
            this._menuItemRemove.Size = new System.Drawing.Size(213, 24);
            this._menuItemRemove.Text = "Remove Item";
            this._menuItemRemove.Click += new System.EventHandler(this.menuItemRemove_Click);
            // 
            // _menuItemDuplicate
            // 
            this._menuItemDuplicate.Name = "_menuItemDuplicate";
            this._menuItemDuplicate.Size = new System.Drawing.Size(213, 24);
            this._menuItemDuplicate.Text = "Duplicate Item";
            this._menuItemDuplicate.Click += new System.EventHandler(this.menuItemDuplicate_Click);
            // 
            // _menuItemAddNote
            // 
            this._menuItemAddNote.Name = "_menuItemAddNote";
            this._menuItemAddNote.Size = new System.Drawing.Size(213, 24);
            this._menuItemAddNote.Text = "Add Note";
            this._menuItemAddNote.Click += new System.EventHandler(this.menuItemAddNote_Click);
            // 
            // _contextMenuProduct
            // 
            this._contextMenuProduct.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._contextMenuProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuItemProductAddToReceipt,
            this._menuItemProductViewDetails,
            this._menuItemProductEdit});
            this._contextMenuProduct.Name = "_contextMenuProduct";
            this._contextMenuProduct.Size = new System.Drawing.Size(179, 76);
            // 
            // _menuItemProductAddToReceipt
            // 
            this._menuItemProductAddToReceipt.Name = "_menuItemProductAddToReceipt";
            this._menuItemProductAddToReceipt.Size = new System.Drawing.Size(178, 24);
            this._menuItemProductAddToReceipt.Text = "Add to Receipt";
            this._menuItemProductAddToReceipt.Click += new System.EventHandler(this.menuItemProductAddToReceipt_Click);
            // 
            // _menuItemProductViewDetails
            // 
            this._menuItemProductViewDetails.Name = "_menuItemProductViewDetails";
            this._menuItemProductViewDetails.Size = new System.Drawing.Size(178, 24);
            this._menuItemProductViewDetails.Text = "View Details";
            this._menuItemProductViewDetails.Click += new System.EventHandler(this.menuItemProductViewDetails_Click);
            // 
            // _menuItemProductEdit
            // 
            this._menuItemProductEdit.Name = "_menuItemProductEdit";
            this._menuItemProductEdit.Size = new System.Drawing.Size(178, 24);
            this._menuItemProductEdit.Text = "Edit Product";
            this._menuItemProductEdit.Click += new System.EventHandler(this.menuItemProductEdit_Click);
            // 
            // frmPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1473, 749);
            this.Controls.Add(this._contentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "frmPOS";
            this.Text = "Point of Sale";
            this.Load += new System.EventHandler(this.frmPOS_Load);
            this._contentPanel.ResumeLayout(false);
            this._mainLayoutPanel.ResumeLayout(false);
            this._topPanel.ResumeLayout(false);
            this._topPanel.PerformLayout();
            this._paymentPanel.ResumeLayout(false);
            this._paymentPanel.PerformLayout();
            this._customerPanel.ResumeLayout(false);
            this._customerPanel.PerformLayout();
            this._barcodePanel.ResumeLayout(false);
            this._barcodePanel.PerformLayout();
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._receiptPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._gridReceipt)).EndInit();
            this._totalsPanel.ResumeLayout(false);
            this._contextMenuReceipt.ResumeLayout(false);
            this._contextMenuProduct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel _contentPanel;
        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
        private System.Windows.Forms.Panel _topPanel;
        private System.Windows.Forms.TextBox _txtSearch;
        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.TabControl _tabsProducts;
        private System.Windows.Forms.Panel _receiptPanel;
        private System.Windows.Forms.DataGridView _gridReceipt;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colUnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colSubtotal;
        private System.Windows.Forms.ContextMenuStrip _contextMenuReceipt;
        private System.Windows.Forms.ToolStripMenuItem _menuItemEditQty;
        private System.Windows.Forms.ToolStripMenuItem _menuItemApplyDiscount;
        private System.Windows.Forms.ToolStripMenuItem _menuItemRemove;
        private System.Windows.Forms.ToolStripMenuItem _menuItemDuplicate;
        private System.Windows.Forms.ToolStripMenuItem _menuItemAddNote;
        private System.Windows.Forms.ContextMenuStrip _contextMenuProduct;
        private System.Windows.Forms.ToolStripMenuItem _menuItemProductAddToReceipt;
        private System.Windows.Forms.ToolStripMenuItem _menuItemProductViewDetails;
        private System.Windows.Forms.ToolStripMenuItem _menuItemProductEdit;
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
        private System.Windows.Forms.CheckBox _cbCash;
        private System.Windows.Forms.CheckBox _cbVisa;
        private System.Windows.Forms.TextBox _txtPaymentDetails;
        private System.Windows.Forms.Label _lblPaymentDetails;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel _barcodePanel;
        private System.Windows.Forms.Label _lblBarcode;
        private System.Windows.Forms.TextBox _txtBarcode;
        private System.Windows.Forms.Button _btnAddByBarcode;
    }
}
