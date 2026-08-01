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
        private clsDiscountSystem.Coupon _appliedCoupon;

        // Debounce timer for search
        private System.Windows.Forms.Timer _searchDebounceTimer;
        private const int SearchDebounceMs = 300;

        public frmPOS()
        {
            InitializeComponent();

            // Initialize debounce timer
            _searchDebounceTimer = new System.Windows.Forms.Timer();
            _searchDebounceTimer.Interval = SearchDebounceMs;
            _searchDebounceTimer.Tick += SearchDebounceTimer_Tick;

            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Point of Sale", clsFormTheme.Icons.POS);
            clsFormTheme.ApplyTextBoxStyle(_txtSearch);
            clsFormTheme.ApplyTextBoxStyle(_txtCustomerPhone);
            clsFormTheme.ApplyTextBoxStyle(_txtPaymentDetails);
            clsFormTheme.ApplyTextBoxStyle(_txtCoupon);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAddCustomer, clsFormTheme.Icons.User);
            clsFormTheme.ApplySecondaryButtonStyle(_btnViewHistory, clsFormTheme.Icons.Search);

            // ── Coupon ─────────────────────────────────────────────────────────
            clsFormTheme.ApplySuccessButtonStyle(_btnApplyCoupon, clsFormTheme.Icons.Check);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRemoveCoupon, clsFormTheme.Icons.Cancel);
            _btnRemoveCoupon.Enabled = false;

            // ── Toolbar buttons ────────────────────────────────────────────────
            clsFormTheme.ApplySecondaryButtonStyle(_btnRefresh, clsFormTheme.Icons.Refresh);

            clsFormTheme.ApplySecondaryButtonStyle(_btnReport, clsFormTheme.Icons.Reports);

            clsFormTheme.ApplyDangerButtonStyle(_btnRemoveItem, clsFormTheme.Icons.Delete);

            clsFormTheme.ApplySuccessButtonStyle(_btnCompleteOrder, clsFormTheme.Icons.Money);

            clsFormTheme.ApplySecondaryButtonStyle(_btnPrintReceipt, clsFormTheme.Icons.Print);
            _btnPrintReceipt.Enabled = false;

            // ── New POS improvement buttons ─────────────────────────────────────
            clsFormTheme.ApplyDangerButtonStyle(_btnClearAll, clsFormTheme.Icons.Delete);
            clsFormTheme.ApplySecondaryButtonStyle(_btnHoldOrder, clsFormTheme.Icons.Save);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnQuickAdd, clsFormTheme.Icons.Add);
            clsFormTheme.ApplySecondaryButtonStyle(_btnVoidLast, clsFormTheme.Icons.Cancel);

            // ── Receipt grid ───────────────────────────────────────────────────
            clsFormTheme.ApplyGridStyle(_gridReceipt);
            _gridReceipt.AutoGenerateColumns = false;
            _gridReceipt.DataSource = _receiptItems;
            _receiptItems.ListChanged += ReceiptItems_ListChanged;

            KeyDown += frmPOS_KeyDown;
            KeyPress += frmPOS_KeyPress;

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
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
            ApplyLocalization();

            // Subscribe to barcode scanner events
            clsBarcodeScanner.BarcodeScanned += BarcodeScanned;
            clsBarcodeScanner.ProductFound += BarcodeProductFound;
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
                TabPage emptyPage = new TabPage(clsLanguageManager.GetString("No Products"));
                Label empty = new Label
                {
                    Text = clsLanguageManager.GetString("No products match your search."),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font(clsFormTheme.MainFontName, 14F, FontStyle.Bold),
                    ForeColor = clsFormTheme.TextMuted
                };
                emptyPage.Controls.Add(empty);
                _tabsProducts.TabPages.Add(emptyPage);
            }

            _lblStatus.Text = _productsTable.Rows.Count + " " + clsLanguageManager.GetString("products available");
        }

        private DataRow[] GetFilteredRows()
        {
            string search = _txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(search))
                return _productsTable.AsEnumerable().ToArray();

            // Check if search is a numeric barcode (exact match for barcodes)
            if (search.All(char.IsDigit))
            {
                var exactBarcodeMatch = _productsTable.AsEnumerable()
                    .Where(row => row["Barcode"].ToString().Trim() == search)
                    .ToArray();
                
                if (exactBarcodeMatch.Length > 0)
                    return exactBarcodeMatch;
            }

            // Otherwise, do partial match on name, category, supplier, and barcode
            return _productsTable.AsEnumerable()
                .Where(row =>
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
                catch (Exception ex)
                {
                    clsAuditLog.LogError("frmPOS.CreateProductTile", ex);
                    _lblStatus.Text = clsLanguageManager.GetString("Unable to load product image.");
                }
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
                Text = inStock ? clsLanguageManager.GetString("Stock:") + " " + quantity : clsLanguageManager.GetString("Out of stock"),
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
                clsFormTheme.ApplyPrimaryButtonStyle(addButton, clsFormTheme.Icons.Add);
            }
            else
            {
                addButton.BackColor = Color.FromArgb(226, 232, 240);  // Slate 200
                addButton.ForeColor = clsFormTheme.TextMuted;
                addButton.FlatStyle = FlatStyle.Flat;
                addButton.FlatAppearance.BorderSize = 0;
                clsFormTheme.ApplyPrimaryButtonStyle(addButton, clsFormTheme.Icons.Warning);
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
            decimal discount = GetCouponDiscount(subtotal);
            decimal taxableAmount = subtotal - discount;
            decimal tax = Math.Round(taxableAmount * TaxRate, 2);
            decimal total = taxableAmount + tax;

            _lblSubtotal.Text = clsLanguageManager.GetString("Subtotal:") + " " + subtotal.ToString("C2");
            _lblDiscount.Visible = _appliedCoupon != null;
            _lblDiscount.Text = _appliedCoupon == null
                ? string.Empty
                : clsLanguageManager.GetString("Discount:") + " (" + _appliedCoupon.Code + "): -" + discount.ToString("C2");
            _lblTax.Text = clsLanguageManager.GetString("Tax (7%):") + " " + tax.ToString("C2");
            _lblTotal.Text = clsLanguageManager.GetString("Total:") + " " + total.ToString("C2");

            _lblItemCount.Text = _receiptItems.Count + " item" + (_receiptItems.Count != 1 ? "s" : "");

            _btnCompleteOrder.Enabled = _receiptItems.Count > 0;
            _btnRemoveItem.Enabled = _receiptItems.Count > 0;
            _gridReceipt.Refresh();
        }

        /// <summary>
        /// Returns the discount for the applied coupon, dropping the coupon when the
        /// receipt no longer satisfies its conditions (e.g. items were removed).
        /// </summary>
        private decimal GetCouponDiscount(decimal subtotal)
        {
            if (_appliedCoupon == null)
                return 0;

            string reason;
            if (clsDiscountSystem.ValidateCoupon(_appliedCoupon.Code, subtotal, out reason) == null)
            {
                ClearCoupon();
                _lblStatus.Text = reason;
                return 0;
            }

            return Math.Round(clsDiscountSystem.ApplyCoupon(_appliedCoupon, subtotal), 2);
        }

        private void ClearCoupon()
        {
            _appliedCoupon = null;
            _txtCoupon.Text = "";
            _txtCoupon.Enabled = true;
            _btnApplyCoupon.Enabled = true;
            _btnRemoveCoupon.Enabled = false;
            _lblDiscount.Visible = false;
            _lblDiscount.Text = string.Empty;
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
                MessageBox.Show("Receipt is empty.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            decimal discount = GetCouponDiscount(_receiptItems.Sum(item => item.Subtotal));
            string couponCode = _appliedCoupon == null ? null : _appliedCoupon.Code;

            int orderID;
            string errorMessage;
            bool saved = clsPOS.CompleteOrder(BuildOrderItemsTable(), TaxRate, _selectedCustomerID, paymentMethod, paymentDetails, discount, couponCode, out orderID, out errorMessage);

            if (!saved)
            {
                MessageBox.Show("Order failed: " + errorMessage, "POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadProducts();
                return;
            }

            if (couponCode != null)
                clsDiscountSystem.UseCoupon(couponCode);

            _receiptItems.Clear();
            ClearCoupon();
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
            _btnAddCustomer.Text = clsLanguageManager.GetString("+ New");
            _rbCash.Checked = true;
            _txtPaymentDetails.Text = "";
            _txtPaymentDetails.Enabled = false;
            _lblPaymentDetails.Text = "";
        }

        // ── Event handlers ─────────────────────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Reset debounce timer on each keystroke
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private void SearchDebounceTimer_Tick(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
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
                DataRow row = customer.Rows[0];
                _selectedCustomerID = Convert.ToInt32(row["CustomerID"]);
                _selectedCustomerName = row["CustomerName"].ToString();
                _lblCustomerName.Text = _selectedCustomerName;
                _lblCustomerName.ForeColor = Color.FromArgb(44, 62, 80);
                _btnAddCustomer.Text = clsLanguageManager.GetString("Change");

                // Display loyalty info
                int points = row["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(row["LoyaltyPoints"]) : 0;
                string tier = row["Tier"] != DBNull.Value ? row["Tier"].ToString() : "Bronze";
                decimal totalSpent = row["TotalSpent"] != DBNull.Value ? Convert.ToDecimal(row["TotalSpent"]) : 0;
                
                // Show loyalty info in a tooltip or additional label if available
                // For now, append to customer name display
                _lblCustomerName.Text += $" ({tier} - {points} pts)";
            }
            else
            {
                _selectedCustomerID = null;
                _selectedCustomerName = "";
                _lblCustomerName.Text = clsLanguageManager.GetString("New customer");
                _lblCustomerName.ForeColor = Color.FromArgb(96, 125, 139);
                _btnAddCustomer.Text = clsLanguageManager.GetString("+ New");
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
                    _btnAddCustomer.Text = clsLanguageManager.GetString("Change");
                }
            }
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            if (!_selectedCustomerID.HasValue)
            {
                MessageBox.Show("Please select a customer first by entering their phone number.", "Customer History", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _txtCustomerPhone.Focus();
                return;
            }

            DataTable orderHistory = clsCustomer.GetCustomerOrders(_selectedCustomerID.Value);
            
            if (orderHistory == null || orderHistory.Rows.Count == 0)
            {
                MessageBox.Show($"No purchase history found for {_selectedCustomerName}.", "Customer History", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Show history in a dialog
            using (Form historyForm = new Form
            {
                Text = $"Purchase History - {_selectedCustomerName}",
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            })
            {
                clsFormTheme.ApplyFormStyle(historyForm);
                clsFormTheme.CreateHeaderPanel(historyForm, "Customer Purchase History", clsFormTheme.Icons.Reports);

                var mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 2,
                    Padding = new Padding(20)
                };
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

                var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoGenerateColumns = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    DataSource = orderHistory
                };

                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "OrderID",
                    HeaderText = "Order ID",
                    Width = 70
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "OrderDate",
                    HeaderText = "Date",
                    Width = 130
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TotalAmount",
                    HeaderText = "Total",
                    Width = 80,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00", Alignment = DataGridViewContentAlignment.MiddleRight }
                });
                grid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PaymentMethod",
                    HeaderText = "Payment",
                    Width = 80
                });

                clsFormTheme.ApplyGridStyle(grid);

                var btnClose = new Button
                {
                    Text = "Close",
                    Width = 100,
                    Height = 35,
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right
                };
                clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
                btnClose.Click += (s, args) => historyForm.Close();

                var btnPanel = new Panel { Dock = DockStyle.Fill };
                btnPanel.Controls.Add(btnClose);
                btnClose.Location = new Point(540, 5);

                mainPanel.Controls.Add(grid, 0, 0);
                mainPanel.Controls.Add(btnPanel, 0, 1);

                historyForm.Controls.Add(mainPanel);
                historyForm.ShowDialog();
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

        private void btnApplyCoupon_Click(object sender, EventArgs e)
        {
            if (_receiptItems.Count == 0)
            {
                MessageBox.Show("Add items to the receipt before applying a coupon.", "Coupon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal subtotal = _receiptItems.Sum(item => item.Subtotal);

            string reason;
            clsDiscountSystem.Coupon coupon = clsDiscountSystem.ValidateCoupon(_txtCoupon.Text, subtotal, out reason);

            if (coupon == null)
            {
                MessageBox.Show(reason, "Coupon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtCoupon.Focus();
                return;
            }

            _appliedCoupon = coupon;
            _txtCoupon.Text = coupon.Code;
            _txtCoupon.Enabled = false;
            _btnApplyCoupon.Enabled = false;
            _btnRemoveCoupon.Enabled = true;
            _lblStatus.Text = "Coupon " + coupon.Code + " applied.";

            RefreshReceiptTotals();
        }

        private void btnRemoveCoupon_Click(object sender, EventArgs e)
        {
            ClearCoupon();
            RefreshReceiptTotals();
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
            _btnCompleteOrder.Enabled = false;
            try
            {
                CompleteOrder();
            }
            finally
            {
                _btnCompleteOrder.Enabled = true;
            }
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (_lastCompletedOrderID == -1)
            {
                MessageBox.Show("Please complete an order first to print the receipt.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmPrintReceipt printForm = new frmPrintReceipt())
            {
                printForm.OrderIDTextBox.Text = _lastCompletedOrderID.ToString();
                printForm.SearchOrder();
                printForm.ShowDialog(this);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (_receiptItems.Count == 0)
            {
                MessageBox.Show("Receipt is already empty.", "Clear All", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to clear all items from the receipt?", "Clear All", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                _receiptItems.Clear();
                ClearCoupon();
                RefreshReceiptTotals();
                _lblStatus.Text = "Receipt cleared.";
            }
        }

        private void btnHoldOrder_Click(object sender, EventArgs e)
        {
            if (_receiptItems.Count == 0)
            {
                MessageBox.Show("Add items to the receipt before holding an order.", "Hold Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // For now, just show a message - this could be expanded to save held orders
            MessageBox.Show($"Order held with {_receiptItems.Count} items.\n\nThis feature can be expanded to save and retrieve held orders.", 
                "Hold Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuickAdd_Click(object sender, EventArgs e)
        {
            // Show a dialog to quickly add a product by name or barcode
            using (Form quickAddForm = new Form())
            {
                quickAddForm.Text = "Quick Add Product";
                quickAddForm.Size = new Size(400, 200);
                quickAddForm.StartPosition = FormStartPosition.CenterParent;
                quickAddForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                quickAddForm.MaximizeBox = false;
                quickAddForm.MinimizeBox = false;

                clsFormTheme.ApplyFormStyle(quickAddForm);

                var layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    Padding = new Padding(20)
                };
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                var lblPrompt = new Label
                {
                    Text = "Enter product name or barcode:",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                var txtInput = new TextBox
                {
                    Dock = DockStyle.Fill
                };
                clsFormTheme.ApplyTextBoxStyle(txtInput);

                var btnPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft
                };

                var btnAdd = new Button
                {
                    Text = "Add",
                    Size = new Size(80, 30),
                    Margin = new Padding(5)
                };
                clsFormTheme.ApplySuccessButtonStyle(btnAdd, clsFormTheme.Icons.Add);

                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Size = new Size(80, 30),
                    Margin = new Padding(5)
                };
                clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

                btnPanel.Controls.Add(btnCancel);
                btnPanel.Controls.Add(btnAdd);

                layout.Controls.Add(lblPrompt, 0, 0);
                layout.Controls.Add(txtInput, 0, 1);
                layout.Controls.Add(btnPanel, 0, 2);

                quickAddForm.Controls.Add(layout);
                txtInput.Focus();

                btnAdd.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtInput.Text))
                    {
                        MessageBox.Show("Please enter a product name or barcode.", "Quick Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string input = txtInput.Text.Trim();
                    DataRow[] matches = _productsTable.AsEnumerable()
                        .Where(row => row["ProductName"].ToString().ToLower().Contains(input.ToLower()) ||
                                     row["Barcode"].ToString().Trim() == input)
                        .ToArray();

                    if (matches.Length == 0)
                    {
                        MessageBox.Show("Product not found.", "Quick Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (matches.Length == 1)
                    {
                        DataRow product = matches[0];
                        int productID = Convert.ToInt32(product["ProductID"]);
                        string productName = product["ProductName"].ToString();
                        decimal price = Convert.ToDecimal(product["Price"]);
                        int availableStock = Convert.ToInt32(product["Quantity"]);

                        AddToReceipt(productID, productName, price, availableStock);
                        quickAddForm.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        // Multiple matches - show selection dialog
                        MessageBox.Show($"Found {matches.Length} matches. Please use the product list to select.", "Quick Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };

                btnCancel.Click += (s, args) => quickAddForm.DialogResult = DialogResult.Cancel;
                txtInput.KeyDown += (s, args) =>
                {
                    if (args.KeyCode == Keys.Enter)
                        btnAdd.PerformClick();
                };

                quickAddForm.ShowDialog(this);
            }
        }

        private void btnVoidLast_Click(object sender, EventArgs e)
        {
            if (_receiptItems.Count == 0)
            {
                MessageBox.Show("Receipt is empty.", "Void Last", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Remove the last item added
            var lastItem = _receiptItems.Last();
            _receiptItems.Remove(lastItem);
            RefreshReceiptTotals();
            _lblStatus.Text = "Last item voided.";
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
            _lblDiscount.Width = panel.ClientSize.Width;
            _lblTax.Width = panel.ClientSize.Width;
            _lblTotal.Width = panel.ClientSize.Width;
            _btnCompleteOrder.Left = panel.ClientSize.Width - _btnCompleteOrder.Width;
        }

        private void topPanel_Resize(object sender, EventArgs e)
        {
            int right = _topPanel.ClientSize.Width - 16;
            _btnReport.Left = right - _btnReport.Width;
            _btnRefresh.Left = _btnReport.Left - _btnRefresh.Width - 10;
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            // F2 - Quick Add
            if (e.KeyCode == Keys.F2)
            {
                btnQuickAdd_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // F4 - Complete Order
            else if (e.KeyCode == Keys.F4)
            {
                if (_btnCompleteOrder.Enabled)
                    btnCompleteOrder_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // F5 - Refresh Products
            else if (e.KeyCode == Keys.F5)
            {
                btnRefresh_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // Delete - Remove selected item
            else if (e.KeyCode == Keys.Delete && _gridReceipt.Focused)
            {
                btnRemoveItem_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // Escape - Clear all (with confirmation) if receipt has items, otherwise close form
            else if (e.KeyCode == Keys.Escape)
            {
                if (_receiptItems.Count > 0)
                {
                    btnClearAll_Click(null, null);
                }
                else
                {
                    Close();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // Ctrl+Enter to complete order
            else if (e.Control && e.KeyCode == Keys.Enter)
            {
                if (_btnCompleteOrder.Enabled)
                    btnCompleteOrder_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void frmPOS_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Process barcode scanner input ONLY when not typing in a textbox
            // This prevents interference with normal typing in coupon/search/payment fields
            if (!(this.ActiveControl is TextBox))
            {
                clsBarcodeScanner.ProcessKeyPress(e, this.ActiveControl);
            }
        }

        private void BarcodeScanned(string barcode)
        {
            // Search for product by barcode and add to receipt
            DataRow[] matches = _productsTable.AsEnumerable()
                .Where(row => row["Barcode"].ToString().Trim() == barcode)
                .ToArray();

            if (matches.Length > 0)
            {
                DataRow product = matches[0];
                int productID = Convert.ToInt32(product["ProductID"]);
                string productName = product["ProductName"].ToString();
                decimal price = Convert.ToDecimal(product["Price"]);
                int availableStock = Convert.ToInt32(product["Quantity"]);
                string barcodeValue = product["Barcode"].ToString();

                // Add to cache for fast-path next time
                clsBarcodeScanner.AddToCache(barcodeValue, productID, productName, price);

                AddToReceipt(productID, productName, price, availableStock);
            }
            else
            {
                MessageBox.Show("Product not found for barcode: " + barcode, "Barcode Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BarcodeProductFound(int productID, string productName, decimal price)
        {
            // Fast-path: product found in cache, need to get available stock from products table
            DataRow[] matches = _productsTable.AsEnumerable()
                .Where(row => Convert.ToInt32(row["ProductID"]) == productID)
                .ToArray();

            int availableStock = 0;
            if (matches.Length > 0)
            {
                availableStock = Convert.ToInt32(matches[0]["Quantity"]);
            }

            // Add to receipt
            AddToReceipt(productID, productName, price, availableStock);
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Point of Sale (POS)");
        }

        private void _topPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class ReceiptItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int AvailableStock { get; set; }

        public decimal Subtotal => Quantity * UnitPrice;
    }
}