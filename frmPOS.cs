using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public class frmPOS : Form
    {
        private const decimal TaxRate = 0.07m;

        private readonly BindingList<ReceiptItem> _receiptItems = new BindingList<ReceiptItem>();
        private DataTable _productsTable = new DataTable();

        private TableLayoutPanel _rootLayout;
        private Panel _topPanel;
        private TextBox _txtSearch;
        private Button _btnRefresh;
        private Button _btnReport;
        private SplitContainer _splitContainer;
        private TabControl _tabsProducts;
        private Panel _receiptPanel;
        private DataGridView _gridReceipt;
        private Label _lblSubtotal;
        private Label _lblTax;
        private Label _lblTotal;
        private Label _lblStatus;
        private Button _btnRemoveItem;
        private Button _btnCompleteOrder;
        private Button _btnClose;

        public frmPOS()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh);
            clsFormTheme.ApplySecondaryButtonStyle(_btnReport);
            clsFormTheme.ApplyDangerButtonStyle(_btnRemoveItem);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnCompleteOrder);
            clsFormTheme.ApplySecondaryButtonStyle(_btnClose);
            clsFormTheme.ApplyGridStyle(_gridReceipt);

            _gridReceipt.AutoGenerateColumns = false;
            _gridReceipt.DataSource = _receiptItems;
            _receiptItems.ListChanged += ReceiptItems_ListChanged;

            KeyDown += frmPOS_KeyDown;
        }

        private void InitializeComponent()
        {
            Text = "Point of Sale";
            Width = 1320;
            Height = 820;
            MinimumSize = new Size(1100, 650);
            Font = new Font("Segoe UI", 10F);

            _rootLayout = new TableLayoutPanel();
            _rootLayout.Dock = DockStyle.Fill;
            _rootLayout.ColumnCount = 1;
            _rootLayout.RowCount = 2;
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _topPanel = new Panel();
            _topPanel.Dock = DockStyle.Fill;
            _topPanel.Padding = new Padding(16, 14, 16, 10);
            _topPanel.BackColor = Color.White;

            Label title = new Label();
            title.Text = "Point of Sale";
            title.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            title.ForeColor = clsFormTheme.HeaderColor;
            title.AutoSize = true;
            title.Location = new Point(16, 17);

            _txtSearch = new TextBox();
            _txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _txtSearch.Width = 360;
            _txtSearch.Location = new Point(620, 22);
            _txtSearch.TextChanged += txtSearch_TextChanged;

            _btnRefresh = new Button();
            _btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnRefresh.Text = "Refresh";
            _btnRefresh.Size = new Size(96, 34);
            _btnRefresh.Location = new Point(992, 19);
            _btnRefresh.Click += btnRefresh_Click;

            _btnReport = new Button();
            _btnReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnReport.Text = "Daily Report";
            _btnReport.Size = new Size(120, 34);
            _btnReport.Location = new Point(1098, 19);
            _btnReport.Click += btnReport_Click;

            _btnClose = new Button();
            _btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnClose.Text = "Close";
            _btnClose.Size = new Size(76, 34);
            _btnClose.Location = new Point(1228, 19);
            _btnClose.Click += btnClose_Click;

            _topPanel.Resize += topPanel_Resize;
            _topPanel.Controls.Add(title);
            _topPanel.Controls.Add(_txtSearch);
            _topPanel.Controls.Add(_btnRefresh);
            _topPanel.Controls.Add(_btnReport);
            _topPanel.Controls.Add(_btnClose);

            _splitContainer = new SplitContainer();
            _splitContainer.Dock = DockStyle.Fill;
            _splitContainer.SplitterDistance = 820;
            _splitContainer.Panel1.Padding = new Padding(14);
            _splitContainer.Panel2.Padding = new Padding(0, 14, 14, 14);

            _tabsProducts = new TabControl();
            _tabsProducts.Dock = DockStyle.Fill;
            _tabsProducts.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            _receiptPanel = new Panel();
            _receiptPanel.Dock = DockStyle.Fill;
            _receiptPanel.BackColor = Color.White;
            _receiptPanel.Padding = new Padding(14);

            Label receiptTitle = new Label();
            receiptTitle.Text = "Receipt";
            receiptTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            receiptTitle.ForeColor = clsFormTheme.HeaderColor;
            receiptTitle.Dock = DockStyle.Top;
            receiptTitle.Height = 38;

            _gridReceipt = new DataGridView();
            _gridReceipt.Dock = DockStyle.Fill;
            _gridReceipt.AllowUserToAddRows = false;
            _gridReceipt.AllowUserToDeleteRows = false;
            _gridReceipt.RowHeadersVisible = false;
            _gridReceipt.CellValidating += gridReceipt_CellValidating;
            _gridReceipt.CellEndEdit += gridReceipt_CellEndEdit;
            _gridReceipt.DataError += gridReceipt_DataError;

            _gridReceipt.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Item",
                ReadOnly = true,
                FillWeight = 150
            });
            _gridReceipt.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Qty",
                Width = 58,
                FillWeight = 45
            });
            _gridReceipt.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Price",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                FillWeight = 70
            });
            _gridReceipt.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Subtotal",
                HeaderText = "Subtotal",
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" },
                FillWeight = 80
            });

            Panel totalsPanel = new Panel();
            totalsPanel.Dock = DockStyle.Bottom;
            totalsPanel.Height = 178;
            totalsPanel.Padding = new Padding(0, 12, 0, 0);

            _lblSubtotal = CreateTotalLabel("Subtotal: $0.00", 0);
            _lblTax = CreateTotalLabel("Tax: $0.00", 34);
            _lblTotal = CreateTotalLabel("Total: $0.00", 68);
            _lblTotal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            _btnRemoveItem = new Button();
            _btnRemoveItem.Text = "Remove Item";
            _btnRemoveItem.Size = new Size(130, 38);
            _btnRemoveItem.Location = new Point(0, 120);
            _btnRemoveItem.Click += btnRemoveItem_Click;

            _btnCompleteOrder = new Button();
            _btnCompleteOrder.Text = "Complete Order";
            _btnCompleteOrder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnCompleteOrder.Size = new Size(160, 38);
            _btnCompleteOrder.Location = new Point(300, 120);
            _btnCompleteOrder.Click += btnCompleteOrder_Click;

            totalsPanel.Resize += totalsPanel_Resize;
            totalsPanel.Controls.Add(_lblSubtotal);
            totalsPanel.Controls.Add(_lblTax);
            totalsPanel.Controls.Add(_lblTotal);
            totalsPanel.Controls.Add(_btnRemoveItem);
            totalsPanel.Controls.Add(_btnCompleteOrder);

            _lblStatus = new Label();
            _lblStatus.Dock = DockStyle.Bottom;
            _lblStatus.Height = 28;
            _lblStatus.ForeColor = Color.FromArgb(96, 125, 139);
            _lblStatus.TextAlign = ContentAlignment.MiddleLeft;

            _receiptPanel.Controls.Add(_gridReceipt);
            _receiptPanel.Controls.Add(_lblStatus);
            _receiptPanel.Controls.Add(totalsPanel);
            _receiptPanel.Controls.Add(receiptTitle);

            _splitContainer.Panel1.Controls.Add(_tabsProducts);
            _splitContainer.Panel2.Controls.Add(_receiptPanel);

            _rootLayout.Controls.Add(_topPanel, 0, 0);
            _rootLayout.Controls.Add(_splitContainer, 0, 1);
            Controls.Add(_rootLayout);

            Load += frmPOS_Load;
        }

        private Label CreateTotalLabel(string text, int top)
        {
            return new Label
            {
                Text = text,
                Top = top,
                Left = 0,
                Width = 420,
                Height = 32,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = clsFormTheme.HeaderColor,
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            string errorMessage;
            if (!clsPOS.EnsurePosSetupAndSampleData(out errorMessage))
            {
                MessageBox.Show("POS setup failed: " + errorMessage, "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            LoadProducts();
            RefreshReceiptTotals();
        }

        private void LoadProducts()
        {
            _productsTable = clsPOS.GetProductsForPOS();
            BuildProductTabs();
        }

        private void BuildProductTabs()
        {
            _tabsProducts.TabPages.Clear();

            DataRow[] filteredRows = GetFilteredRows();
            string[] categories = filteredRows
                .Select(row => row["CategoryName"].ToString())
                .Distinct()
                .OrderBy(name => name)
                .ToArray();

            foreach (string category in categories)
            {
                TabPage page = new TabPage(category);
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.Dock = DockStyle.Fill;
                panel.AutoScroll = true;
                panel.BackColor = clsFormTheme.FormBackColor;
                panel.Padding = new Padding(10);

                foreach (DataRow row in filteredRows.Where(r => r["CategoryName"].ToString() == category))
                    panel.Controls.Add(CreateProductTile(row));

                page.Controls.Add(panel);
                _tabsProducts.TabPages.Add(page);
            }

            if (_tabsProducts.TabPages.Count == 0)
            {
                TabPage emptyPage = new TabPage("No Products");
                Label empty = new Label();
                empty.Text = "No products match your search.";
                empty.Dock = DockStyle.Fill;
                empty.TextAlign = ContentAlignment.MiddleCenter;
                empty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                empty.ForeColor = clsFormTheme.SecondaryColor;
                emptyPage.Controls.Add(empty);
                _tabsProducts.TabPages.Add(emptyPage);
            }

            _lblStatus.Text = _productsTable.Rows.Count + " products available";
        }

        private DataRow[] GetFilteredRows()
        {
            string search = _txtSearch.Text.Trim();

            return _productsTable.AsEnumerable()
                .Where(row =>
                    string.IsNullOrWhiteSpace(search) ||
                    row["ProductName"].ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    row["CategoryName"].ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    row["SupplierName"].ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    row["Barcode"].ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToArray();
        }

        private Control CreateProductTile(DataRow row)
        {
            int productID = Convert.ToInt32(row["ProductID"]);
            string productName = row["ProductName"].ToString();
            string supplierName = row["SupplierName"].ToString();
            decimal price = Convert.ToDecimal(row["Price"]);
            int quantity = Convert.ToInt32(row["Quantity"]);
            string imagePath = row["ImagePath"] == DBNull.Value ? "" : row["ImagePath"].ToString();

            Panel tile = new Panel();
            tile.Width = 178;
            tile.Height = 252;
            tile.Margin = new Padding(8);
            tile.BackColor = Color.White;
            tile.BorderStyle = BorderStyle.FixedSingle;

            PictureBox picture = new PictureBox();
            picture.Width = 96;
            picture.Height = 96;
            picture.Location = new Point(41, 12);
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.BackColor = Color.FromArgb(245, 248, 250);

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                try
                {
                    picture.LoadAsync(imagePath);
                }
                catch
                {
                }
            }

            Label name = new Label();
            name.Text = productName;
            name.Location = new Point(10, 116);
            name.Size = new Size(158, 40);
            name.TextAlign = ContentAlignment.MiddleCenter;
            name.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            name.ForeColor = clsFormTheme.HeaderColor;

            Label supplier = new Label();
            supplier.Text = supplierName;
            supplier.Location = new Point(10, 157);
            supplier.Size = new Size(158, 22);
            supplier.TextAlign = ContentAlignment.MiddleCenter;
            supplier.Font = new Font("Segoe UI", 8F);
            supplier.ForeColor = clsFormTheme.SecondaryColor;

            Label priceLabel = new Label();
            priceLabel.Text = price.ToString("C2");
            priceLabel.Location = new Point(10, 181);
            priceLabel.Size = new Size(76, 24);
            priceLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            priceLabel.ForeColor = clsFormTheme.PrimaryColor;

            Label stockLabel = new Label();
            stockLabel.Text = "Stock: " + quantity;
            stockLabel.Location = new Point(88, 181);
            stockLabel.Size = new Size(80, 24);
            stockLabel.TextAlign = ContentAlignment.MiddleRight;
            stockLabel.ForeColor = quantity > 0 ? Color.DarkGreen : clsFormTheme.DangerColor;

            Button addButton = new Button();
            addButton.Text = "Add to Receipt";
            addButton.Location = new Point(20, 212);
            addButton.Size = new Size(138, 30);
            addButton.Enabled = quantity > 0;
            clsFormTheme.ApplyPrimaryButtonStyle(addButton);
            addButton.Click += delegate
            {
                AddToReceipt(productID, productName, price, quantity);
            };

            tile.Controls.Add(picture);
            tile.Controls.Add(name);
            tile.Controls.Add(supplier);
            tile.Controls.Add(priceLabel);
            tile.Controls.Add(stockLabel);
            tile.Controls.Add(addButton);

            return tile;
        }

        private void AddToReceipt(int productID, string productName, decimal unitPrice, int availableStock)
        {
            ReceiptItem existing = _receiptItems.FirstOrDefault(item => item.ProductID == productID);

            if (existing != null)
            {
                if (existing.Quantity + 1 > availableStock)
                {
                    MessageBox.Show("Not enough stock available for " + productName + ".", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                existing.Quantity++;
            }
            else
            {
                _receiptItems.Add(new ReceiptItem
                {
                    ProductID = productID,
                    ProductName = productName,
                    Quantity = 1,
                    UnitPrice = unitPrice,
                    AvailableStock = availableStock
                });
            }

            RefreshReceiptTotals();
        }

        private void RefreshReceiptTotals()
        {
            decimal subtotal = _receiptItems.Sum(item => item.Subtotal);
            decimal tax = Math.Round(subtotal * TaxRate, 2);
            decimal total = subtotal + tax;

            _lblSubtotal.Text = "Subtotal: " + subtotal.ToString("C2");
            _lblTax.Text = "Tax: " + tax.ToString("C2");
            _lblTotal.Text = "Total: " + total.ToString("C2");
            _btnCompleteOrder.Enabled = _receiptItems.Count > 0;
            _btnRemoveItem.Enabled = _receiptItems.Count > 0;
            _gridReceipt.Refresh();
        }

        private DataTable BuildOrderItemsTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID", typeof(int));
            table.Columns.Add("ProductName", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            table.Columns.Add("UnitPrice", typeof(decimal));

            foreach (ReceiptItem item in _receiptItems)
                table.Rows.Add(item.ProductID, item.ProductName, item.Quantity, item.UnitPrice);

            return table;
        }

        private void CompleteOrder()
        {
            if (_receiptItems.Count == 0)
            {
                MessageBox.Show("Receipt is empty.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (ReceiptItem item in _receiptItems)
            {
                if (item.Quantity <= 0 || item.Quantity > item.AvailableStock)
                {
                    MessageBox.Show("Invalid quantity for " + item.ProductName + ".", "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            int orderID;
            string errorMessage;
            bool saved = clsPOS.CompleteOrder(BuildOrderItemsTable(), TaxRate, out orderID, out errorMessage);

            if (!saved)
            {
                MessageBox.Show("Order failed: " + errorMessage, "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadProducts();
                return;
            }

            _receiptItems.Clear();
            RefreshReceiptTotals();
            LoadProducts();
            MessageBox.Show("Order #" + orderID + " completed successfully.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BuildProductTabs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            using (frmDailyReport report = new frmDailyReport())
            {
                report.ShowDialog(this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (_gridReceipt.CurrentRow == null)
                return;

            ReceiptItem item = _gridReceipt.CurrentRow.DataBoundItem as ReceiptItem;
            if (item == null)
                return;

            _receiptItems.Remove(item);
            RefreshReceiptTotals();
        }

        private void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            CompleteOrder();
        }

        private void gridReceipt_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_gridReceipt.Columns[e.ColumnIndex].DataPropertyName != "Quantity" || e.RowIndex < 0)
                return;

            int quantity;
            if (!int.TryParse(Convert.ToString(e.FormattedValue), out quantity) || quantity <= 0)
            {
                e.Cancel = true;
                MessageBox.Show("Quantity must be greater than zero.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ReceiptItem item = _gridReceipt.Rows[e.RowIndex].DataBoundItem as ReceiptItem;
            if (item != null && quantity > item.AvailableStock)
            {
                e.Cancel = true;
                MessageBox.Show("Quantity exceeds available stock.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gridReceipt_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            RefreshReceiptTotals();
        }

        private void gridReceipt_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Please enter a valid value.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ReceiptItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            RefreshReceiptTotals();
        }

        private void totalsPanel_Resize(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null)
                return;

            _lblSubtotal.Width = panel.ClientSize.Width;
            _lblTax.Width = panel.ClientSize.Width;
            _lblTotal.Width = panel.ClientSize.Width;
            _btnCompleteOrder.Left = panel.ClientSize.Width - _btnCompleteOrder.Width;
        }

        private void topPanel_Resize(object sender, EventArgs e)
        {
            int right = _topPanel.ClientSize.Width - 16;
            _btnClose.Left = right - _btnClose.Width;
            _btnReport.Left = _btnClose.Left - _btnReport.Width - 10;
            _btnRefresh.Left = _btnReport.Left - _btnRefresh.Width - 10;
            _txtSearch.Left = _btnRefresh.Left - _txtSearch.Width - 12;
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadProducts();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.Control && e.KeyCode == Keys.Enter)
            {
                CompleteOrder();
                e.SuppressKeyPress = true;
            }
        }

        private class ReceiptItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public int AvailableStock { get; set; }
            public decimal Subtotal
            {
                get { return Quantity * UnitPrice; }
            }
        }
    }
}
