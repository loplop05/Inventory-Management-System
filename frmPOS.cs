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
        private string _orderNotes = "";

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

            // Wire sidebar navigation
            _sidebar.NavigationRequested += OnSidebarNavigation;
            _sidebar.SetActive("POS");

            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyTextBoxStyle(_txtCustomerPhone);
            clsFormTheme.ApplyTextBoxStyle(_txtPaymentDetails);
            clsFormTheme.ApplyTextBoxStyle(_txtBarcode);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAddCustomer);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAddByBarcode);

            // ── Toolbar buttons ────────────────────────────────────────────────
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh);
            SetIconButtonText(_btnRefresh, clsFormTheme.Icons.Refresh, "Refresh", 14F, 10F, FontStyle.Regular);

            clsFormTheme.ApplyDangerButtonStyle(_btnRemoveItem);
            SetIconButtonText(_btnRemoveItem, clsFormTheme.Icons.Delete, "Remove", 14F, 10F, FontStyle.Regular);

            clsFormTheme.ApplySuccessButtonStyle(_btnCompleteOrder);
            SetIconButtonText(_btnCompleteOrder, clsFormTheme.Icons.Money, "Complete Order", 16F, 12F, FontStyle.Bold);

            // ── Receipt grid ───────────────────────────────────────────────────
            clsFormTheme.ApplyGridStyle(_gridReceipt);
            _gridReceipt.AutoGenerateColumns = false;
            _gridReceipt.DataSource = _receiptItems;
            _receiptItems.ListChanged += ReceiptItems_ListChanged;

            KeyDown += frmPOS_KeyDown;
        }

        private void OnSidebarNavigation(string screenKey)
        {
            switch (screenKey)
            {
                case "Dashboard":
                    var dashboardForm = new frmDashboard();
                    dashboardForm.Show();
                    this.Close();
                    break;
                case "POS":
                    // Already on POS
                    break;
                case "Inventory":
                    var inventoryForm = new frmProductsManagment();
                    inventoryForm.Show();
                    this.Close();
                    break;
                case "Orders":
                    var receiptForm = new frmReceiptSearch();
                    receiptForm.Show();
                    this.Close();
                    break;
                case "Reports":
                    var reportForm = new frmDailyReport();
                    reportForm.Show();
                    this.Close();
                    break;
                case "Support":
                    // Help system integration - to be implemented
                    break;
            }
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
        }

        private void LoadProducts()
        {
            string errorMessage;
            if (!clsPOS.GetProductsForPOS(out _productsTable, out errorMessage))
            {
                MessageBox.Show("Failed to load products: " + errorMessage, "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            // Store product data in Tag for context menu access
            tile.Tag = new { ProductID = productID, ProductName = productName, SupplierName = supplierName, Price = price, Quantity = quantity };

            // Wire up context menu for right-click
            tile.ContextMenuStrip = _contextMenuProduct;

            // Wire up hover for product preview
            tile.MouseEnter += (s, e) => ShowProductPreview(tile);
            tile.MouseLeave += (s, e) => HideProductPreview();

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

        private void _txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddProductByBarcode();
                e.SuppressKeyPress = true;
            }
        }

        private void _btnAddByBarcode_Click(object sender, EventArgs e)
        {
            AddProductByBarcode();
        }

        private void AddProductByBarcode()
        {
            string barcode = _txtBarcode.Text.Trim();
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return;
            }

            DataRow[] matchingRows = _productsTable.AsEnumerable()
                .Where(row => row["Barcode"].ToString().Equals(barcode, StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (matchingRows.Length == 0)
            {
                MessageBox.Show("Product not found with barcode: " + barcode, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtBarcode.SelectAll();
                _txtBarcode.Focus();
                return;
            }

            DataRow productRow = matchingRows[0];
            int productID = Convert.ToInt32(productRow["ProductID"]);
            string productName = productRow["ProductName"].ToString();
            decimal price = Convert.ToDecimal(productRow["Price"]);
            int quantity = Convert.ToInt32(productRow["Quantity"]);

            if (quantity <= 0)
            {
                MessageBox.Show("Product is out of stock: " + productName, "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtBarcode.SelectAll();
                _txtBarcode.Focus();
                return;
            }

            AddToReceipt(productID, productName, price, quantity);
            _txtBarcode.Clear();
            _txtBarcode.Focus();
        }

        private void RefreshReceiptTotals()
        {
            decimal subtotal = _receiptItems.Sum(item => item.Subtotal);

            // Apply manual discount
            decimal manualDiscount = 0;
            if (_manualDiscountType == "percentage")
            {
                manualDiscount = subtotal * (_manualDiscountAmount / 100m);
            }
            else if (_manualDiscountType == "fixed")
            {
                manualDiscount = _manualDiscountAmount;
            }

            // Apply coupon discount (only if no manual discount, or combine as needed)
            decimal couponDiscount = _couponDiscountAmount;

            decimal totalDiscount = manualDiscount + couponDiscount;
            decimal discountedSubtotal = Math.Max(0, subtotal - totalDiscount);
            decimal tax = Math.Round(discountedSubtotal * TaxRate, 2);
            decimal total = discountedSubtotal + tax;

            _lblSubtotal.Text = "Subtotal: " + subtotal.ToString("C2");

            // Show discount info if applicable
            if (totalDiscount > 0)
            {
                string discountText = "Discount: -" + totalDiscount.ToString("C2");
                if (!string.IsNullOrEmpty(_appliedCouponCode))
                    discountText += " (" + _appliedCouponCode + ")";
                _lblTax.Text = discountText + Environment.NewLine + "Tax (7%): " + tax.ToString("C2");
            }
            else
            {
                _lblTax.Text = "Tax (7%): " + tax.ToString("C2");
            }

            _lblTotal.Text = "Total: " + total.ToString("C2");

            _btnCompleteOrder.Enabled = _receiptItems.Count > 0;
            _btnRemoveItem.Enabled = _receiptItems.Count > 0;
            _gridReceipt.Refresh();
        }

        public void ClearDiscounts()
        {
            _manualDiscountAmount = 0;
            _manualDiscountType = "";
            _appliedCouponCode = "";
            _couponDiscountAmount = 0;
        }

        public void ApplyManualDiscount(decimal amount, string type)
        {
            _manualDiscountAmount = amount;
            _manualDiscountType = type;
            RefreshReceiptTotals();
        }

        public void ApplyCoupon(string code, decimal amount)
        {
            _appliedCouponCode = code;
            _couponDiscountAmount = amount;
            RefreshReceiptTotals();
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

        /// <summary>
        /// Builds the receipt-shaped DataTables (with Subtotal columns) that
        /// clsPrintHelper.PrintReceipt / DrawReceipt expect, and sends the receipt to print.
        /// Must be called BEFORE _receiptItems is cleared.
        /// </summary>
        private void PrintCompletedReceipt(int orderID, decimal subtotal, decimal tax, decimal total, string customerName)
        {
            DataTable orderDetails = new DataTable();
            orderDetails.Columns.Add("OrderID", typeof(int));
            orderDetails.Columns.Add("OrderDate", typeof(DateTime));
            orderDetails.Columns.Add("Subtotal", typeof(decimal));
            orderDetails.Columns.Add("Tax", typeof(decimal));
            orderDetails.Columns.Add("TotalAmount", typeof(decimal));
            orderDetails.Rows.Add(orderID, DateTime.Now, subtotal, tax, total);

            DataTable orderItems = new DataTable();
            orderItems.Columns.Add("ProductName", typeof(string));
            orderItems.Columns.Add("Quantity", typeof(int));
            orderItems.Columns.Add("UnitPrice", typeof(decimal));
            orderItems.Columns.Add("Subtotal", typeof(decimal));

            foreach (ReceiptItem item in _receiptItems)
                orderItems.Rows.Add(item.ProductName, item.Quantity, item.UnitPrice, item.Subtotal);

            clsPrintHelper.PrintReceipt(orderDetails, orderItems, customerName);
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
            if (_cbVisa.Checked && string.IsNullOrWhiteSpace(_txtPaymentDetails.Text))
            {
                MessageBox.Show("Please enter the last 4 digits of the card.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPaymentDetails.Focus();
                return;
            }

            if (_cbVisa.Checked && _txtPaymentDetails.Text.Length != 4)
            {
                MessageBox.Show("Please enter exactly 4 digits for the card.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtPaymentDetails.Focus();
                return;
            }

            string paymentMethod = _cbCash.Checked && !_cbVisa.Checked ? "Cash" : 
                                   _cbVisa.Checked && !_cbCash.Checked ? "Visa" : "Split";
            string paymentDetails = _cbCash.Checked && !_cbVisa.Checked ? null : "****" + _txtPaymentDetails.Text;

            int orderID;
            string errorMessage;

            // Compute the same totals shown on screen, before we clear the cart.
            decimal subtotal = _receiptItems.Sum(item => item.Subtotal);
            decimal manualDiscount = _manualDiscountType == "percentage" ? subtotal * (_manualDiscountAmount / 100m)
                                    : _manualDiscountType == "fixed" ? _manualDiscountAmount : 0;
            decimal totalDiscount = manualDiscount + _couponDiscountAmount;
            decimal discountedSubtotal = Math.Max(0, subtotal - totalDiscount);
            decimal tax = Math.Round(discountedSubtotal * TaxRate, 2);
            decimal total = discountedSubtotal + tax;

            // Check for split payment
            if (_cbCash.Checked && _cbVisa.Checked)
            {
                // Split payment - ask for amounts
                using (frmInputBox cashInput = new frmInputBox($"Total: {total:C2}\n\nEnter Cash amount (default {total:C2}):", "Split Payment - Cash"))
                {
                    if (cashInput.ShowDialog(this) != DialogResult.OK)
                        return;
                    
                    decimal cashAmount;
                    string cashInputValue = string.IsNullOrWhiteSpace(cashInput.InputValue) ? total.ToString("0.00") : cashInput.InputValue;
                    if (!decimal.TryParse(cashInputValue, out cashAmount) || cashAmount < 0 || cashAmount > total)
                    {
                        clsFormTheme.ShowError(this, "Invalid cash amount", "Split Payment");
                        return;
                    }
                    
                    decimal cardAmount = total - cashAmount;
                    if (cardAmount > 0)
                    {
                        using (frmInputBox cardInput = new frmInputBox($"Card amount: {cardAmount:C2}\n\nEnter last 4 digits of card:", "Split Payment - Card"))
                        {
                            if (cardInput.ShowDialog(this) != DialogResult.OK)
                                return;
                            
                            if (cardInput.InputValue.Length != 4)
                            {
                                clsFormTheme.ShowError(this, "Please enter exactly 4 digits", "Split Payment");
                                return;
                            }
                            
                            paymentMethod = "Split";
                            paymentDetails = $"Cash:{cashAmount:0.00}|Card:{cardAmount:0.00}|****{cardInput.InputValue}";
                        }
                    }
                    else
                    {
                        paymentMethod = "Cash";
                        paymentDetails = null;
                    }
                }
            }

            bool saved = clsPOS.CompleteOrder(BuildOrderItemsTable(), TaxRate, _selectedCustomerID, paymentMethod, paymentDetails, out orderID, out errorMessage);

            if (!saved)
            {
                MessageBox.Show("Order failed: " + errorMessage, "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadProducts();
                return;
            }

            string customerNameForReceipt = string.IsNullOrWhiteSpace(_selectedCustomerName) ? "Walk-in Customer" : _selectedCustomerName;

            // Print BEFORE clearing _receiptItems, since PrintCompletedReceipt reads from it.
            DialogResult printChoice = MessageBox.Show(
    "Order #" + orderID + " completed successfully.\n\nPrint receipt?",
    "Print Receipt",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Question);

            if (printChoice == DialogResult.Yes)
            {
                PrintCompletedReceipt(orderID, subtotal, tax, total, customerNameForReceipt);
            }
            _receiptItems.Clear();
            RefreshReceiptTotals();
            LoadProducts();
            ClearCustomerInfo();
            MessageBox.Show("Order #" + orderID + " completed successfully.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearCustomerInfo()
        {
            _selectedCustomerID = null;
            _selectedCustomerName = "";
            _txtCustomerPhone.Text = "";
            _lblCustomerName.Text = "";
            _btnAddCustomer.Text = "+ New";
            _cbCash.Checked = true;
            _cbVisa.Checked = false;
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

                // Get loyalty information
                int loyaltyPoints = customer.Rows[0]["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(customer.Rows[0]["LoyaltyPoints"]) : 0;
                string tier = customer.Rows[0]["Tier"] != DBNull.Value ? customer.Rows[0]["Tier"].ToString() : "Bronze";
                decimal discountAvailable = clsLoyalty.CalculateDiscountFromPoints(loyaltyPoints);

                _lblCustomerName.Text = _selectedCustomerName + " | " + tier + " | " + loyaltyPoints + " pts ($" + discountAvailable.ToString("F2") + ")";
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

        private void cbPayment_CheckedChanged(object sender, EventArgs e)
        {
            _txtPaymentDetails.Enabled = _cbVisa.Checked;
            if (_cbCash.Checked && !_cbVisa.Checked)
            {
                _txtPaymentDetails.Text = "";
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

        private ReceiptItem _lastRemovedItem = null;

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (_gridReceipt.CurrentRow == null)
                return;

            ReceiptItem item = _gridReceipt.CurrentRow.DataBoundItem as ReceiptItem;
            if (item == null)
                return;

            // Store for potential undo
            _lastRemovedItem = item;

            _receiptItems.Remove(item);
            RefreshReceiptTotals();

            // Show toast with undo option
            clsFormTheme.ShowToastWithUndo(this, 
                $"{item.ProductName} removed from receipt", 
                "Item Removed", 
                5000, 
                UndoRemoveItem);
        }

        private void UndoRemoveItem()
        {
            if (_lastRemovedItem != null)
            {
                // Check if we can still add it back (stock might have changed)
                var currentProduct = _productsTable.AsEnumerable()
                    .FirstOrDefault(row => Convert.ToInt32(row["ProductID"]) == _lastRemovedItem.ProductID);
                
                if (currentProduct != null)
                {
                    int currentStock = Convert.ToInt32(currentProduct["Quantity"]);
                    int newQuantity = Math.Min(_lastRemovedItem.Quantity, currentStock);
                    
                    if (newQuantity > 0)
                    {
                        _lastRemovedItem.AvailableStock = currentStock;
                        _lastRemovedItem.Quantity = newQuantity;
                        _receiptItems.Add(_lastRemovedItem);
                        RefreshReceiptTotals();
                        clsFormTheme.ShowToastSuccess(this, "Item restored to receipt", "Undo Successful");
                    }
                    else
                    {
                        clsFormTheme.ShowToastWarning(this, "Cannot restore - no stock available", "Undo Failed");
                    }
                }
                
                _lastRemovedItem = null;
            }
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
            if (_gridReceipt.Columns[e.ColumnIndex].DataPropertyName != "Quantity" || e.RowIndex < 0)
                return;

            RefreshReceiptTotals();
        }

        private void gridReceipt_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Show context menu on right-click
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                _gridReceipt.ClearSelection();
                _gridReceipt.Rows[e.RowIndex].Selected = true;
                _contextMenuReceipt.Show(Cursor.Position);
            }
        }

        private void menuItemEditQty_Click(object sender, EventArgs e)
        {
            if (_gridReceipt.CurrentRow == null)
                return;

            _gridReceipt.BeginEdit(true);
            _gridReceipt.CurrentCell = _gridReceipt.Rows[_gridReceipt.CurrentRow.Index].Cells[_colQuantity.Index];
        }

        private void menuItemApplyDiscount_Click(object sender, EventArgs e)
        {
            // TODO: Implement item discount logic
            // This should reuse existing manual discount functionality
            clsFormTheme.ShowInfo(this, "Item Discount feature coming soon");
        }

        private void menuItemRemove_Click(object sender, EventArgs e)
        {
            btnRemoveItem_Click(sender, e);
        }

        private void menuItemDuplicate_Click(object sender, EventArgs e)
        {
            if (_gridReceipt.CurrentRow == null)
                return;

            int rowIndex = _gridReceipt.CurrentRow.Index;
            if (rowIndex >= 0 && rowIndex < _receiptItems.Count)
            {
                var item = _receiptItems[rowIndex];
                int newQuantity = Math.Min(item.Quantity, item.AvailableStock - item.Quantity);

                if (newQuantity > 0)
                {
                    AddToReceipt(item.ProductID, item.ProductName, item.UnitPrice, newQuantity);
                }
                else
                {
                    clsFormTheme.ShowWarning(this, "Not enough stock to duplicate this item", "Cannot Duplicate");
                }
            }
        }

        private void menuItemAddNote_Click(object sender, EventArgs e)
        {
            using (frmInputBox inputBox = new frmInputBox("Enter order notes:", "Order Notes"))
            {
                if (inputBox.ShowDialog(this) == DialogResult.OK)
                {
                    _orderNotes = inputBox.InputValue;
                    clsFormTheme.ShowToastSuccess(this, "Order notes updated", "Notes Saved");
                }
            }
        }

        private void menuItemProductAddToReceipt_Click(object sender, EventArgs e)
        {
            if (_contextMenuProduct.SourceControl == null)
                return;

            Panel tile = _contextMenuProduct.SourceControl as Panel;
            if (tile == null || tile.Tag == null)
                return;

            var productData = (dynamic)tile.Tag;
            AddToReceipt(productData.ProductID, productData.ProductName, productData.Price, productData.Quantity);
        }

        private void menuItemProductViewDetails_Click(object sender, EventArgs e)
        {
            if (_contextMenuProduct.SourceControl == null)
                return;

            Panel tile = _contextMenuProduct.SourceControl as Panel;
            if (tile == null || tile.Tag == null)
                return;

            var productData = (dynamic)tile.Tag;
            clsFormTheme.ShowInfo(this, 
                $"Product: {productData.ProductName}\n" +
                $"Supplier: {productData.SupplierName}\n" +
                $"Price: {productData.Price:C2}\n" +
                $"Stock: {productData.Quantity}",
                "Product Details");
        }

        private void menuItemProductEdit_Click(object sender, EventArgs e)
        {
            if (_contextMenuProduct.SourceControl == null)
                return;

            Panel tile = _contextMenuProduct.SourceControl as Panel;
            if (tile == null || tile.Tag == null)
                return;

            var productData = (dynamic)tile.Tag;
            
            // Open the product edit form
            using (frmUpdateProduct updateForm = new frmUpdateProduct())
            {
                // Pre-select the product
                // Note: frmUpdateProduct needs to support pre-selection
                clsFormTheme.ShowInfo(this, "Edit Product feature - navigate to Product Management to edit this product", "Edit Product");
            }
        }

        private Panel _previewPanel = null;

        private void ShowProductPreview(Panel tile)
        {
            if (tile.Tag == null)
                return;

            var productData = (dynamic)tile.Tag;

            // Create preview panel if it doesn't exist
            if (_previewPanel == null)
            {
                _previewPanel = new Panel
                {
                    BackColor = Color.FromArgb(255, 255, 255),
                    BorderStyle = BorderStyle.FixedSingle,
                    Size = new Size(200, 120),
                    Location = new Point(tile.Right + 5, tile.Top),
                    Visible = false
                };

                Label lblName = new Label
                {
                    Text = "",
                    Location = new Point(10, 10),
                    Size = new Size(180, 20),
                    Font = new Font(clsFormTheme.MainFontName, 9F, FontStyle.Bold),
                    ForeColor = clsFormTheme.HeaderColor
                };

                Label lblSupplier = new Label
                {
                    Text = "",
                    Location = new Point(10, 35),
                    Size = new Size(180, 20),
                    Font = new Font(clsFormTheme.MainFontName, 8F),
                    ForeColor = clsFormTheme.TextSecondary
                };

                Label lblPrice = new Label
                {
                    Text = "",
                    Location = new Point(10, 60),
                    Size = new Size(180, 20),
                    Font = new Font(clsFormTheme.MainFontName, 9F, FontStyle.Bold),
                    ForeColor = clsFormTheme.PrimaryColor
                };

                Label lblStock = new Label
                {
                    Text = "",
                    Location = new Point(10, 85),
                    Size = new Size(180, 20),
                    Font = new Font(clsFormTheme.MainFontName, 8F),
                    ForeColor = clsFormTheme.SuccessColor
                };

                _previewPanel.Controls.Add(lblName);
                _previewPanel.Controls.Add(lblSupplier);
                _previewPanel.Controls.Add(lblPrice);
                _previewPanel.Controls.Add(lblStock);
                _previewPanel.Tag = new { Name = lblName, Supplier = lblSupplier, Price = lblPrice, Stock = lblStock };

                _splitContainer.Panel1.Controls.Add(_previewPanel);
                _previewPanel.BringToFront();
            }

            // Update preview content
            var labels = (dynamic)_previewPanel.Tag;
            labels.Name.Text = productData.ProductName;
            labels.Supplier.Text = "Supplier: " + productData.SupplierName;
            labels.Price.Text = "Price: " + productData.Price.ToString("C2");
            labels.Stock.Text = "Stock: " + productData.Quantity;

            // Position and show
            _previewPanel.Location = new Point(tile.Right + 5, tile.Top);
            _previewPanel.Visible = true;
        }

        private void HideProductPreview()
        {
            if (_previewPanel != null)
            {
                _previewPanel.Visible = false;
            }
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
            _btnRefresh.Left = right - _btnRefresh.Width;
            _txtSearch.Left = _btnRefresh.Left - _txtSearch.Width - 12;
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            // F2/F3 - Focus search box
            if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.F3)
            {
                _txtSearch.Focus();
                e.SuppressKeyPress = true;
            }
            // F4 - Complete order
            else if (e.KeyCode == Keys.F4)
            {
                CompleteOrder();
                e.SuppressKeyPress = true;
            }
            // F5 - Refresh products
            else if (e.KeyCode == Keys.F5)
            {
                LoadProducts();
                e.SuppressKeyPress = true;
            }
            // Delete - Remove selected receipt line
            else if (e.KeyCode == Keys.Delete)
            {
                btnRemoveItem_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            // Plus - Increase quantity of selected line
            else if (e.KeyCode == Keys.Add || (e.Shift && e.KeyCode == Keys.Oemplus))
            {
                AdjustSelectedLineQuantity(1);
                e.SuppressKeyPress = true;
            }
            // Minus - Decrease quantity of selected line
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                AdjustSelectedLineQuantity(-1);
                e.SuppressKeyPress = true;
            }
            // Ctrl+H - Hold order (not yet implemented - placeholder)
            else if (e.Control && e.KeyCode == Keys.H)
            {
                // TODO: Implement HoldOrder functionality
                clsFormTheme.ShowInfo(this, "Hold Order feature coming soon");
                e.SuppressKeyPress = true;
            }
            // Escape - Close form
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            // Ctrl+Enter - Complete order (existing shortcut)
            else if (e.Control && e.KeyCode == Keys.Enter)
            {
                CompleteOrder();
                e.SuppressKeyPress = true;
            }
        }

        private void AdjustSelectedLineQuantity(int delta)
        {
            if (_gridReceipt.CurrentRow == null)
                return;

            int rowIndex = _gridReceipt.CurrentRow.Index;
            if (rowIndex >= 0 && rowIndex < _receiptItems.Count)
            {
                var item = _receiptItems[rowIndex];
                int newQuantity = item.Quantity + delta;

                if (newQuantity > 0 && newQuantity <= item.AvailableStock)
                {
                    item.Quantity = newQuantity;
                    _gridReceipt.Refresh();
                    RefreshReceiptTotals();
                }
                else if (newQuantity <= 0)
                {
                    // If quantity goes to 0 or below, remove the item
                    btnRemoveItem_Click(null, null);
                }
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

        // Discount tracking
        private decimal _manualDiscountAmount = 0;
        private string _manualDiscountType = ""; // "percentage" or "fixed"
        private string _appliedCouponCode = "";
        private decimal _couponDiscountAmount = 0;

        private void _topPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        // ── Actions menu handlers ───────────────────────────────────────────────

        private void button1_Click(object sender, EventArgs e)
        {
            using (frmPOSActions actionsForm = new frmPOSActions())
            {
                // Pass receipt data to actions form
                actionsForm.ReceiptItems = _receiptItems;
                actionsForm.ReceiptGrid = _gridReceipt;
                actionsForm.RefreshTotals = RefreshReceiptTotals;
                actionsForm.ClearCustomerInfo = ClearCustomerInfo;
                actionsForm.SelectedCustomerID = _selectedCustomerID;
                actionsForm.ProductsTable = _productsTable;
                actionsForm.ApplyManualDiscount = ApplyManualDiscount;
                actionsForm.ApplyCoupon = ApplyCoupon;
                actionsForm.ClearDiscounts = ClearDiscounts;

                actionsForm.ShowDialog(this);

                // Refresh after actions form closes
                if (actionsForm.DialogResult == DialogResult.OK)
                {
                    RefreshReceiptTotals();
                    LoadProducts();
                }
            }
        }
    }
}