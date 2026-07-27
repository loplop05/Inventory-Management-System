using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmPOS : Form
    {
        private const decimal TaxRate = 0.07m;

        private readonly BindingList<ReceiptItem> _receiptItems = new BindingList<ReceiptItem>();
        private DataTable _productsTable = new DataTable();
        private int? _selectedCustomerID = null;
        private string _selectedCustomerName = "";
        private int _lastCompletedOrderID = -1;

        // ── Icon+label button rendering ─────────────────────────────────────
        private class IconButtonInfo
        {
            public string Icon;
            public string Label;
            public float IconFontSize;
            public float TextFontSize;
            public FontStyle TextStyle;
        }

        private readonly Dictionary<Button, IconButtonInfo> _iconButtons =
            new Dictionary<Button, IconButtonInfo>();

        private void SetIconButtonText(Button btn, string icon, string label,
            float iconFontSize = 12F, float textFontSize = 10F, FontStyle textStyle = FontStyle.Bold)
        {
            btn.Text = "";
            _iconButtons[btn] = new IconButtonInfo
            {
                Icon = icon,
                Label = label,
                IconFontSize = iconFontSize,
                TextFontSize = textFontSize,
                TextStyle = textStyle
            };
            btn.Paint -= IconButton_Paint; // avoid double subscribe if reused
            btn.Paint += IconButton_Paint;
        }

        private void IconButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            IconButtonInfo info;
            if (btn == null || !_iconButtons.TryGetValue(btn, out info))
                return;

            using (Font iconFont = new Font(clsFormTheme.IconFontName, info.IconFontSize))
            using (Font textFont = new Font(clsFormTheme.MainFontName, info.TextFontSize, info.TextStyle))
            using (SolidBrush brush = new SolidBrush(btn.Enabled ? btn.ForeColor : clsFormTheme.TextMuted))
            {
                SizeF iconSize = e.Graphics.MeasureString(info.Icon, iconFont);
                SizeF textSize = e.Graphics.MeasureString(info.Label, textFont);

                const float gap = 6f;
                float totalWidth = iconSize.Width + gap + textSize.Width;
                float startX = (btn.ClientSize.Width - totalWidth) / 2f;
                float centerY = btn.ClientSize.Height / 2f;

                var iconRect = new RectangleF(startX, centerY - iconSize.Height / 2f, iconSize.Width, iconSize.Height);
                var textRect = new RectangleF(startX + iconSize.Width + gap, centerY - textSize.Height / 2f, textSize.Width, textSize.Height);

                e.Graphics.DrawString(info.Icon, iconFont, brush, iconRect);
                e.Graphics.DrawString(info.Label, textFont, brush, textRect);
            }
        }

        private void PurgeDisposedIconButtons()
        {
            foreach (Button key in _iconButtons.Keys.Where(b => b.IsDisposed).ToList())
                _iconButtons.Remove(key);
        }

        public frmPOS()
        {
            InitializeComponent();

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyTextBoxStyle(_txtCustomerPhone);
            clsFormTheme.ApplyTextBoxStyle(_txtPaymentDetails);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAddCustomer);

            // ── Toolbar buttons ────────────────────────────────────────────────
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh);
            SetIconButtonText(_btnRefresh, clsFormTheme.Icons.Refresh, "Refresh", 14F, 10F, FontStyle.Regular);

            clsFormTheme.ApplySecondaryButtonStyle(_btnReport);
            SetIconButtonText(_btnReport, clsFormTheme.Icons.Reports, "Report", 14F, 10F, FontStyle.Regular);

            clsFormTheme.ApplyDangerButtonStyle(_btnRemoveItem);
            SetIconButtonText(_btnRemoveItem, clsFormTheme.Icons.Delete, "Remove", 14F, 10F, FontStyle.Regular);

            clsFormTheme.ApplySuccessButtonStyle(_btnCompleteOrder);
            SetIconButtonText(_btnCompleteOrder, clsFormTheme.Icons.Money, "Complete Order", 16F, 12F, FontStyle.Bold);

            clsFormTheme.ApplySecondaryButtonStyle(_btnPrintReceipt);
            SetIconButtonText(_btnPrintReceipt, clsFormTheme.Icons.Print, "Print Receipt", 14F, 10F, FontStyle.Regular);
            _btnPrintReceipt.Enabled = false;

            clsFormTheme.ApplySecondaryButtonStyle(_btnClose);
            SetIconButtonText(_btnClose, clsFormTheme.Icons.Exit, "Close", 14F, 10F, FontStyle.Regular);

            // ── Receipt grid ───────────────────────────────────────────────────
            clsFormTheme.ApplyGridStyle(_gridReceipt);
            _gridReceipt.AutoGenerateColumns = false;
            _gridReceipt.DataSource = _receiptItems;
            _receiptItems.ListChanged += ReceiptItems_ListChanged;

            KeyDown += frmPOS_KeyDown;
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
            ClearCustomerInfo();
            LoadCustomerPhoneAutoComplete();
        }

        private void LoadCustomerPhoneAutoComplete()
        {
            DataTable customers = clsCustomer.GetAllCustomers();
            AutoCompleteStringCollection phoneNumbers = new AutoCompleteStringCollection();

            foreach (DataRow row in customers.Rows)
            {
                string phone = row["PhoneNumber"].ToString();
                if (!string.IsNullOrEmpty(phone))
                {
                    phoneNumbers.Add(phone);
                }
            }

            _txtCustomerPhone.AutoCompleteCustomSource = phoneNumbers;
        }

        private void LoadProducts()
        {
            _productsTable = clsPOS.GetProductsForPOS();
            BuildProductTabs();
        }

        private void BuildProductTabs()
        {
            PurgeDisposedIconButtons();
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
                FlowLayoutPanel panel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = clsFormTheme.FormBackColor,
                    Padding = new Padding(12)
                };

                foreach (DataRow row in filteredRows.Where(r => r["CategoryName"].ToString() == category))
                    panel.Controls.Add(CreateProductTile(row));

                page.Controls.Add(panel);
                _tabsProducts.TabPages.Add(page);
            }

            if (_tabsProducts.TabPages.Count == 0)
            {
                TabPage emptyPage = new TabPage("No Products");
                Label empty = new Label
                {
                    Text = "No products match your search.",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(clsFormTheme.MainFontName, 14F, FontStyle.Bold),
                    ForeColor = clsFormTheme.TextMuted
                };
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
            bool inStock = quantity > 0;

            // ── Tile container ─────────────────────────────────────────────────
            Panel tile = new Panel
            {
                Width = 182,
                Height = 260,
                Margin = new Padding(8),
                BackColor = clsFormTheme.CardColor,
                BorderStyle = BorderStyle.None
            };

            clsFormTheme.StyleProductTile(tile, inStock);

            // ── Product image ──────────────────────────────────────────────────
            PictureBox picture = new PictureBox
            {
                Width = 100,
                Height = 100,
                Location = new Point(41, 16),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(241, 245, 249)  // Slate 100
            };

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                try { picture.LoadAsync(imagePath); }
                catch { /* ignore load errors */ }
            }

            // ── Product name ───────────────────────────────────────────────────
            Label name = new Label
            {
                Text = productName,
                Location = new Point(8, 124),
                Size = new Size(166, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(clsFormTheme.MainFontName, 9F, FontStyle.Bold),
                ForeColor = clsFormTheme.HeaderColor
            };

            // ── Supplier name ──────────────────────────────────────────────────
            Label supplier = new Label
            {
                Text = supplierName,
                Location = new Point(8, 164),
                Size = new Size(166, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(clsFormTheme.MainFontName, 8F),
                ForeColor = clsFormTheme.TextSecondary
            };

            // ── Price label ────────────────────────────────────────────────────
            Label priceLabel = new Label
            {
                Text = price.ToString("C2"),
                Location = new Point(8, 186),
                Size = new Size(90, 24),
                Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold),
                ForeColor = clsFormTheme.PrimaryColor
            };

            // ── Stock label ────────────────────────────────────────────────────
            Label stockLabel = new Label
            {
                Text = inStock ? "Stock: " + quantity : "Out of stock",
                Location = new Point(88, 186),
                Size = new Size(86, 24),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font(clsFormTheme.MainFontName, 8F, FontStyle.Bold),
                ForeColor = inStock ? clsFormTheme.SuccessColor : clsFormTheme.DangerColor
            };

            // ── Add to receipt button ──────────────────────────────────────────
            Button addButton = new Button
            {
                Location = new Point(12, 218),
                Size = new Size(158, 30),
                Enabled = inStock
            };

            if (inStock)
            {
                clsFormTheme.ApplyPrimaryButtonStyle(addButton);
                SetIconButtonText(addButton, clsFormTheme.Icons.Add, "Add", 13F, 10F, FontStyle.Bold);
            }
            else
            {
                addButton.BackColor = Color.FromArgb(226, 232, 240);  // Slate 200
                addButton.ForeColor = clsFormTheme.TextMuted;
                addButton.FlatStyle = FlatStyle.Flat;
                addButton.FlatAppearance.BorderSize = 0;
                SetIconButtonText(addButton, clsFormTheme.Icons.Warning, "No Stock", 13F, 10F, FontStyle.Bold);
            }

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
            _lblTax.Text = "Tax (7%): " + tax.ToString("C2");
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

            // Validate payment details if Visa is selected
            if (_rbVisa.Checked && string.IsNullOrWhiteSpace(_txtPaymentDetails.Text))
            {
                MessageBox.Show("Please enter the last 4 digits of the card.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPaymentDetails.Focus();
                return;
            }

            if (_rbVisa.Checked && _txtPaymentDetails.Text.Length != 4)
            {
                MessageBox.Show("Please enter exactly 4 digits for the card.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPaymentDetails.Focus();
                return;
            }

            string paymentMethod = _rbCash.Checked ? "Cash" : "Visa";
            string paymentDetails = _rbCash.Checked ? null : "****" + _txtPaymentDetails.Text;

            int orderID;
            string errorMessage;
            bool saved = clsPOS.CompleteOrder(BuildOrderItemsTable(), TaxRate, _selectedCustomerID, paymentMethod, paymentDetails, out orderID, out errorMessage);

            if (!saved)
            {
                MessageBox.Show("Order failed: " + errorMessage, "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadProducts();
                return;
            }

            _receiptItems.Clear();
            RefreshReceiptTotals();
            LoadProducts();
            ClearCustomerInfo();
            _lastCompletedOrderID = orderID;
            _btnPrintReceipt.Enabled = true;
            MessageBox.Show("Order #" + orderID + " completed successfully.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearCustomerInfo()
        {
            _selectedCustomerID = null;
            _selectedCustomerName = "";
            _txtCustomerPhone.Text = "";
            _lblCustomerName.Text = "";
            _btnAddCustomer.Text = "+ New";
            _rbCash.Checked = true;
            _txtPaymentDetails.Text = "";
            _txtPaymentDetails.Enabled = false;
            _lblPaymentDetails.Text = "";
        }

        // ── Event handlers ─────────────────────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BuildProductTabs();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void txtCustomerPhone_TextChanged(object sender, EventArgs e)
        {
            string phoneNumber = _txtCustomerPhone.Text.Trim();

            if (string.IsNullOrEmpty(phoneNumber))
            {
                ClearCustomerInfo();
                return;
            }

            // Auto-lookup customer when phone number is entered
            DataTable customer = clsCustomer.GetCustomerByPhone(phoneNumber);
            if (customer != null && customer.Rows.Count > 0)
            {
                _selectedCustomerID = Convert.ToInt32(customer.Rows[0]["CustomerID"]);
                _selectedCustomerName = customer.Rows[0]["CustomerName"].ToString();
                _lblCustomerName.Text = _selectedCustomerName;
                _lblCustomerName.ForeColor = Color.FromArgb(44, 62, 80);
                _btnAddCustomer.Text = "Change";
            }
            else
            {
                _selectedCustomerID = null;
                _selectedCustomerName = "";
                _lblCustomerName.Text = "New customer";
                _lblCustomerName.ForeColor = Color.FromArgb(96, 125, 139);
                _btnAddCustomer.Text = "+ New";
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            using (frmAddCustomer addCustomerForm = new frmAddCustomer())
            {
                // Pre-fill phone number if entered
                if (!string.IsNullOrWhiteSpace(_txtCustomerPhone.Text))
                {
                    addCustomerForm.PhoneNumber = _txtCustomerPhone.Text.Trim();
                }

                DialogResult result = addCustomerForm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    _selectedCustomerID = addCustomerForm.CustomerID;
                    _selectedCustomerName = addCustomerForm.CustomerName;
                    _txtCustomerPhone.Text = addCustomerForm.PhoneNumber;
                    _lblCustomerName.Text = _selectedCustomerName;
                    _lblCustomerName.ForeColor = Color.FromArgb(44, 62, 80);
                    _btnAddCustomer.Text = "Change";
                }
            }
        }

        private void rbPayment_CheckedChanged(object sender, EventArgs e)
        {
            _txtPaymentDetails.Enabled = _rbVisa.Checked;
            if (_rbCash.Checked)
            {
                _txtPaymentDetails.Text = "";
                _lblPaymentDetails.Text = "";
            }
            else
            {
                _txtPaymentDetails.Focus();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            using (frmDailyReport report = new frmDailyReport())
                report.ShowDialog(this);
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

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (_lastCompletedOrderID == -1)
            {
                MessageBox.Show("Please complete an order first to print the receipt.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (frmPrintReceipt printForm = new frmPrintReceipt())
            {
                printForm.OrderIDTextBox.Text = _lastCompletedOrderID.ToString();
                printForm.SearchOrder();
                printForm.ShowDialog(this);
            }
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

            public decimal Subtotal => Quantity * UnitPrice;
        }

        private void _topPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}