using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Discount and coupon management system.
    /// Supports percentage discounts, fixed amount coupons, BOGO offers, and customer loyalty points.
    /// </summary>
    public static class clsDiscountSystem
    {
        // ─── Discount Types ────────────────────────────────────────────────────

        public enum DiscountType
        {
            Percentage,
            FixedAmount,
            BuyOneGetOne,
            LoyaltyPoints,
            BulkDiscount
        }

        // ─── Coupon Data Structure ─────────────────────────────────────────────

        public class Coupon
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public DiscountType Type { get; set; }
            public decimal Value { get; set; } // Percentage or fixed amount
            public decimal MinimumPurchase { get; set; }
            public DateTime? ValidFrom { get; set; }
            public DateTime? ValidUntil { get; set; }
            public int MaxUses { get; set; } = -1; // -1 = unlimited
            public int TimesUsed { get; set; }
            public bool IsActive { get; set; } = true;
            public string[] ApplicableCategories { get; set; } // Empty = all categories
            public string[] ApplicableProducts { get; set; } // Empty = all products

            /// <summary>Value formatted for display, based on the discount type.</summary>
            public string ValueText
            {
                get
                {
                    switch (Type)
                    {
                        case DiscountType.Percentage: return Value.ToString("0.##") + "%";
                        case DiscountType.FixedAmount: return clsLanguageManager.CurrencySymbol + " " + Value.ToString("0.00");
                        case DiscountType.LoyaltyPoints: return Value.ToString("0") + " pts";
                        default: return "—";
                    }
                }
            }

            /// <summary>Usage formatted as "used / limit".</summary>
            public string UsageText
            {
                get { return TimesUsed + " / " + (MaxUses > 0 ? MaxUses.ToString() : "∞"); }
            }

            /// <summary>Expiry date formatted for display.</summary>
            public string ExpiryText
            {
                get { return ValidUntil.HasValue ? ValidUntil.Value.ToShortDateString() : "Never"; }
            }

            public bool IsValid()
            {
                if (!IsActive) return false;
                if (MaxUses > 0 && TimesUsed >= MaxUses) return false;
                if (ValidFrom.HasValue && DateTime.Now < ValidFrom.Value) return false;
                if (ValidUntil.HasValue && DateTime.Now > ValidUntil.Value) return false;
                return true;
            }
        }

        // ─── Customer Loyalty ─────────────────────────────────────────────────

        public class CustomerLoyalty
        {
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string Phone { get; set; }
            public int Points { get; set; }
            public decimal TotalSpent { get; set; }
            public string Tier { get; set; } = "Bronze";
            public DateTime LastPurchase { get; set; }

            // Points conversion rate
            public const int PointsPerDinar = 1;
            public const int PointsForDiscount = 100; // 100 points = 1 JOD discount
        }

        // ─── Coupon Management ────────────────────────────────────────────────

        private static readonly Dictionary<string, Coupon> _coupons =
            new Dictionary<string, Coupon>(StringComparer.OrdinalIgnoreCase);

        static clsDiscountSystem()
        {
            InitializeSampleCoupons();
        }

        /// <summary>
        /// Adds a new coupon to the system.
        /// </summary>
        public static bool AddCoupon(Coupon coupon)
        {
            if (coupon == null || string.IsNullOrWhiteSpace(coupon.Code)) return false;

            string code = NormalizeCode(coupon.Code);
            if (_coupons.ContainsKey(code)) return false;

            coupon.Code = code;
            _coupons[code] = coupon;
            return true;
        }

        /// <summary>
        /// Gets a coupon by code.
        /// </summary>
        public static Coupon GetCoupon(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;

            Coupon coupon;
            return _coupons.TryGetValue(NormalizeCode(code), out coupon) ? coupon : null;
        }

        /// <summary>
        /// Validates and returns a coupon if valid.
        /// </summary>
        public static Coupon ValidateCoupon(string code, decimal purchaseAmount)
        {
            string reason;
            return ValidateCoupon(code, purchaseAmount, out reason);
        }

        /// <summary>
        /// Validates a coupon and explains why it was rejected when it is not usable.
        /// </summary>
        public static Coupon ValidateCoupon(string code, decimal purchaseAmount, out string reason)
        {
            reason = string.Empty;

            if (string.IsNullOrWhiteSpace(code))
            {
                reason = "Enter a coupon code.";
                return null;
            }

            Coupon coupon = GetCoupon(code);
            if (coupon == null)
            {
                reason = "Coupon '" + NormalizeCode(code) + "' does not exist.";
                return null;
            }

            if (!coupon.IsActive)
            {
                reason = "Coupon '" + coupon.Code + "' is inactive.";
                return null;
            }

            if (coupon.MaxUses > 0 && coupon.TimesUsed >= coupon.MaxUses)
            {
                reason = "Coupon '" + coupon.Code + "' has reached its usage limit.";
                return null;
            }

            if (coupon.ValidFrom.HasValue && DateTime.Now < coupon.ValidFrom.Value)
            {
                reason = "Coupon '" + coupon.Code + "' is not valid until " + coupon.ValidFrom.Value.ToShortDateString() + ".";
                return null;
            }

            if (coupon.ValidUntil.HasValue && DateTime.Now > coupon.ValidUntil.Value)
            {
                reason = "Coupon '" + coupon.Code + "' expired on " + coupon.ValidUntil.Value.ToShortDateString() + ".";
                return null;
            }

            if (purchaseAmount < coupon.MinimumPurchase)
            {
                reason = "Coupon '" + coupon.Code + "' requires a minimum purchase of " + clsLanguageManager.CurrencySymbol + " " + coupon.MinimumPurchase.ToString("0.00") + ".";
                return null;
            }

            return coupon;
        }

        private static string NormalizeCode(string code)
        {
            return code == null ? string.Empty : code.Trim().ToUpperInvariant();
        }

        /// <summary>
        /// Applies a coupon to a purchase amount.
        /// </summary>
        public static decimal ApplyCoupon(Coupon coupon, decimal originalAmount, List<string> productCategories = null, List<int> productIds = null)
        {
            if (coupon == null || !coupon.IsValid()) return 0;

            decimal discount = 0;

            switch (coupon.Type)
            {
                case DiscountType.Percentage:
                    discount = originalAmount * (coupon.Value / 100);
                    break;

                case DiscountType.FixedAmount:
                    discount = coupon.Value;
                    break;

                case DiscountType.BuyOneGetOne:
                    // This would need product-level application
                    discount = originalAmount * 0.5m; // Simplified: 50% off
                    break;

                case DiscountType.LoyaltyPoints:
                    discount = coupon.Value / CustomerLoyalty.PointsForDiscount;
                    break;

                case DiscountType.BulkDiscount:
                    // Apply based on quantity thresholds
                    if (originalAmount >= 100) discount = originalAmount * 0.15m; // 15% off
                    else if (originalAmount >= 50) discount = originalAmount * 0.10m; // 10% off
                    break;
            }

            // Ensure discount doesn't exceed original amount
            return Math.Min(discount, originalAmount);
        }

        /// <summary>
        /// Increments the usage count for a coupon.
        /// </summary>
        public static void UseCoupon(string code)
        {
            var coupon = GetCoupon(code);
            if (coupon != null)
            {
                coupon.TimesUsed++;
            }
        }

        /// <summary>
        /// Gets all active coupons.
        /// </summary>
        public static List<Coupon> GetAllCoupons()
        {
            return new List<Coupon>(_coupons.Values);
        }

        /// <summary>
        /// Removes a coupon from the system.
        /// </summary>
        public static bool RemoveCoupon(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return false;
            return _coupons.Remove(NormalizeCode(code));
        }

        // ─── Customer Loyalty Management ───────────────────────────────────────

        /// <summary>
        /// Adds or updates a customer's loyalty information (now persisted to database).
        /// </summary>
        public static void UpdateCustomerLoyalty(int customerId, string name, string phone, decimal purchaseAmount)
        {
            string errorMessage;
            InventoryBusinessLayer.clsCustomer.UpdateCustomerLoyalty(customerId, purchaseAmount, out errorMessage);
        }

        /// <summary>
        /// Calculates customer tier based on total spending.
        /// </summary>
        private static string CalculateTier(decimal totalSpent)
        {
            if (totalSpent >= 5000) return "Platinum";
            if (totalSpent >= 2000) return "Gold";
            if (totalSpent >= 500) return "Silver";
            return "Bronze";
        }

        /// <summary>
        /// Gets customer loyalty information from database.
        /// </summary>
        public static CustomerLoyalty GetCustomerLoyalty(int customerId)
        {
            var dt = InventoryBusinessLayer.clsCustomer.GetCustomerByID(customerId);
            if (dt == null || dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new CustomerLoyalty
            {
                CustomerID = customerId,
                CustomerName = row["CustomerName"].ToString(),
                Phone = row["PhoneNumber"].ToString(),
                Points = InventoryBusinessLayer.clsCustomer.GetLoyaltyPoints(customerId),
                TotalSpent = row["TotalSpent"] != DBNull.Value ? Convert.ToDecimal(row["TotalSpent"]) : 0,
                Tier = row["Tier"] != DBNull.Value ? row["Tier"].ToString() : "Bronze",
                LastPurchase = row["LastPurchaseDate"] != DBNull.Value ? Convert.ToDateTime(row["LastPurchaseDate"]) : DateTime.MinValue
            };
        }

        /// <summary>
        /// Redeems loyalty points for a discount (now persisted to database).
        /// </summary>
        public static decimal RedeemLoyaltyPoints(int customerId, int pointsToRedeem)
        {
            string errorMessage;
            if (InventoryBusinessLayer.clsCustomer.RedeemLoyaltyPoints(customerId, pointsToRedeem, out errorMessage))
            {
                return (decimal)pointsToRedeem / CustomerLoyalty.PointsForDiscount;
            }
            return 0;
        }

        /// <summary>
        /// Gets tier-based discount percentage.
        /// </summary>
        public static decimal GetTierDiscount(string tier)
        {
            return InventoryBusinessLayer.clsCustomer.GetTierDiscount(tier);
        }

        // ─── Discount Calculation Helpers ─────────────────────────────────────

        /// <summary>
        /// Calculates final price after applying multiple discounts.
        /// </summary>
        public static decimal CalculateFinalPrice(decimal originalPrice, List<Coupon> coupons, CustomerLoyalty customer = null)
        {
            decimal price = originalPrice;

            // Apply tier discount first
            if (customer != null)
            {
                price -= price * GetTierDiscount(customer.Tier);
            }

            // Apply coupons
            if (coupons != null)
            {
                foreach (var coupon in coupons)
                {
                    if (coupon != null && coupon.IsValid())
                    {
                        price -= ApplyCoupon(coupon, price);
                    }
                }
            }

            // Ensure price doesn't go below zero
            return Math.Max(0, price);
        }

        /// <summary>
        /// Calculates bulk discount based on quantity.
        /// </summary>
        public static decimal CalculateBulkDiscount(int quantity, decimal unitPrice)
        {
            decimal discount = 0;

            if (quantity >= 10)
                discount = 0.15m; // 15% off for 10+ items
            else if (quantity >= 5)
                discount = 0.10m; // 10% off for 5-9 items
            else if (quantity >= 3)
                discount = 0.05m; // 5% off for 3-4 items

            return unitPrice * quantity * discount;
        }

        // ─── Sample Data Initialization ───────────────────────────────────────────

        /// <summary>
        /// Initializes sample coupons for testing.
        /// </summary>
        public static void InitializeSampleCoupons()
        {
            _coupons.Clear();

            AddCoupon(new Coupon
            {
                Code = "SAVE10",
                Description = "10% off your purchase",
                Type = DiscountType.Percentage,
                Value = 10,
                MinimumPurchase = 50,
                MaxUses = 100
            });

            AddCoupon(new Coupon
            {
                Code = "FLAT5",
                Description = "5 JOD off your purchase",
                Type = DiscountType.FixedAmount,
                Value = 5,
                MinimumPurchase = 25,
                MaxUses = 50
            });

            AddCoupon(new Coupon
            {
                Code = "BOGO",
                Description = "Buy One Get One Free",
                Type = DiscountType.BuyOneGetOne,
                Value = 50,
                MinimumPurchase = 20,
                MaxUses = 30
            });

            AddCoupon(new Coupon
            {
                Code = "LOYALTY100",
                Description = "Redeem 100 loyalty points for 1 JOD off",
                Type = DiscountType.LoyaltyPoints,
                Value = 100,
                MinimumPurchase = 10,
                MaxUses = -1
            });
        }

        // ─── UI Helpers ────────────────────────────────────────────────────────

        /// <summary>
        /// Shows the coupon management dialog (browse, add, edit and delete coupons).
        /// </summary>
        public static void ShowCouponManager()
        {
            using (Form managerForm = new Form
            {
                Text = "Coupon Manager",
                Size = new Size(940, 600),
                MinimumSize = new Size(780, 500),
                StartPosition = FormStartPosition.CenterScreen
            })
            {
                clsFormTheme.ApplyFormStyle(managerForm);
                clsFormTheme.CreateHeaderPanel(managerForm, "Coupon Manager", clsFormTheme.Icons.Money);

                TableLayoutPanel mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 3,
                    BackColor = Color.Transparent,
                    Padding = new Padding(16, 12, 16, 16)
                };
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));

                Label lblSummary = new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(2, 0, 2, 6)
                };
                clsFormTheme.ApplyLabelStyle(lblSummary);
                lblSummary.ForeColor = clsFormTheme.TextSecondary;

                DataGridView grid = BuildCouponGrid();

                Panel buttonPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent,
                    Margin = new Padding(0, 10, 0, 0)
                };

                Button btnAdd = CreateDialogButton("Add Coupon");
                clsFormTheme.ApplyPrimaryButtonStyle(btnAdd, clsFormTheme.Icons.Add);

                Button btnEdit = CreateDialogButton("Edit Coupon");
                clsFormTheme.ApplySecondaryButtonStyle(btnEdit, clsFormTheme.Icons.Update);

                Button btnDelete = CreateDialogButton("Delete Coupon");
                clsFormTheme.ApplyDangerButtonStyle(btnDelete, clsFormTheme.Icons.Delete);

                Button btnClose = CreateDialogButton("Close");
                clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);

                FlowLayoutPanel leftButtons = new FlowLayoutPanel
                {
                    Dock = DockStyle.Left,
                    AutoSize = true,
                    WrapContents = false,
                    BackColor = Color.Transparent
                };
                leftButtons.Controls.Add(btnAdd);
                leftButtons.Controls.Add(btnEdit);
                leftButtons.Controls.Add(btnDelete);

                FlowLayoutPanel rightButtons = new FlowLayoutPanel
                {
                    Dock = DockStyle.Right,
                    AutoSize = true,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    BackColor = Color.Transparent
                };
                rightButtons.Controls.Add(btnClose);

                buttonPanel.Controls.Add(leftButtons);
                buttonPanel.Controls.Add(rightButtons);

                mainPanel.Controls.Add(lblSummary, 0, 0);
                mainPanel.Controls.Add(grid, 0, 1);
                mainPanel.Controls.Add(buttonPanel, 0, 2);

                EventHandler refresh = delegate
                {
                    List<Coupon> coupons = GetAllCoupons();
                    grid.DataSource = new BindingList<Coupon>(coupons);
                    lblSummary.Text = coupons.Count == 0
                        ? "No coupons defined yet — click \"Add Coupon\" to create one."
                        : coupons.Count + " coupon(s) — usable from the Point of Sale screen.";
                };

                EventHandler selectionChanged = delegate
                {
                    bool hasSelection = GetSelectedCoupon(grid) != null;
                    btnEdit.Enabled = hasSelection;
                    btnDelete.Enabled = hasSelection;
                };

                grid.SelectionChanged += selectionChanged;

                btnAdd.Click += delegate
                {
                    if (ShowCouponEditor(null))
                        refresh(null, EventArgs.Empty);
                };

                btnEdit.Click += delegate
                {
                    Coupon selected = GetSelectedCoupon(grid);
                    if (selected != null && ShowCouponEditor(selected))
                        refresh(null, EventArgs.Empty);
                };

                grid.CellDoubleClick += delegate(object s, DataGridViewCellEventArgs e)
                {
                    if (e.RowIndex < 0) return;

                    Coupon selected = GetSelectedCoupon(grid);
                    if (selected != null && ShowCouponEditor(selected))
                        refresh(null, EventArgs.Empty);
                };

                btnDelete.Click += delegate
                {
                    Coupon selected = GetSelectedCoupon(grid);
                    if (selected == null) return;

                    if (MessageBox.Show("Delete coupon '" + selected.Code + "'?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        RemoveCoupon(selected.Code);
                        refresh(null, EventArgs.Empty);
                    }
                };

                btnClose.Click += delegate { managerForm.Close(); };

                managerForm.Controls.Add(mainPanel);
                managerForm.CancelButton = btnClose;

                refresh(null, EventArgs.Empty);
                selectionChanged(null, EventArgs.Empty);

                managerForm.ShowDialog();
            }
        }

        private static DataGridView BuildCouponGrid()
        {
            DataGridView grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                Margin = new Padding(0)
            };

            clsFormTheme.ApplyGridStyle(grid);
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DataGridViewCellStyle rightAligned = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight };
            DataGridViewCellStyle centered = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter };

            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Code", HeaderText = "Code", FillWeight = 85 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description", FillWeight = 200 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Type", HeaderText = "Type", FillWeight = 95 });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ValueText", HeaderText = "Value", FillWeight = 65, DefaultCellStyle = rightAligned });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MinimumPurchase", HeaderText = "Min. Purchase", FillWeight = 95, DefaultCellStyle = rightAligned });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UsageText", HeaderText = "Used", FillWeight = 70, DefaultCellStyle = centered });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ExpiryText", HeaderText = "Expires", FillWeight = 85, DefaultCellStyle = centered });
            grid.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsActive", HeaderText = "Active", FillWeight = 55 });

            return grid;
        }

        private static Coupon GetSelectedCoupon(DataGridView grid)
        {
            return grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as Coupon;
        }

        private static Button CreateDialogButton(string text)
        {
            return new Button
            {
                Text = text,
                Size = new Size(150, 36),
                Margin = new Padding(0, 0, 10, 0)
            };
        }

        /// <summary>
        /// Shows the add/edit coupon editor. Returns true when the coupon was saved.
        /// </summary>
        private static bool ShowCouponEditor(Coupon coupon)
        {
            bool isNew = coupon == null;

            using (Form editor = new Form
            {
                Text = isNew ? "Add Coupon" : "Edit Coupon",
                Size = new Size(480, 560),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            })
            {
                clsFormTheme.ApplyFormStyle(editor);
                clsFormTheme.CreateHeaderPanel(editor, isNew ? "Add Coupon" : "Edit Coupon", clsFormTheme.Icons.Money);

                TableLayoutPanel mainPanel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 9,
                    BackColor = Color.Transparent,
                    Padding = new Padding(20, 16, 20, 16)
                };
                mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
                mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                for (int row = 0; row < 8; row++)
                    mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

                TextBox txtCode = CreateEditorTextBox();
                TextBox txtDescription = CreateEditorTextBox();
                TextBox txtValue = CreateEditorTextBox();
                TextBox txtMinPurchase = CreateEditorTextBox();
                TextBox txtMaxUses = CreateEditorTextBox();

                ComboBox cboType = new ComboBox
                {
                    Dock = DockStyle.Fill,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Margin = new Padding(0, 6, 0, 6)
                };
                clsFormTheme.ApplyComboBoxStyle(cboType);
                cboType.Items.AddRange(Enum.GetNames(typeof(DiscountType)));

                CheckBox chkExpires = new CheckBox
                {
                    Text = "Expires on",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    BackColor = Color.Transparent
                };

                DateTimePicker dtpExpiry = new DateTimePicker
                {
                    Format = DateTimePickerFormat.Short,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 8, 0, 8),
                    Enabled = false,
                    Value = DateTime.Today.AddMonths(1)
                };
                chkExpires.CheckedChanged += delegate { dtpExpiry.Enabled = chkExpires.Checked; };

                CheckBox chkActive = new CheckBox
                {
                    Text = "Coupon can be used at the POS",
                    Checked = true,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    BackColor = Color.Transparent
                };

                if (!isNew)
                {
                    txtCode.Text = coupon.Code;
                    txtCode.Enabled = false;
                    txtDescription.Text = coupon.Description;
                    cboType.SelectedItem = coupon.Type.ToString();
                    txtValue.Text = coupon.Value.ToString("0.##");
                    txtMinPurchase.Text = coupon.MinimumPurchase.ToString("0.##");
                    txtMaxUses.Text = coupon.MaxUses.ToString();
                    chkActive.Checked = coupon.IsActive;
                    chkExpires.Checked = coupon.ValidUntil.HasValue;
                    dtpExpiry.Enabled = coupon.ValidUntil.HasValue;

                    if (coupon.ValidUntil.HasValue)
                        dtpExpiry.Value = coupon.ValidUntil.Value;
                }
                else
                {
                    cboType.SelectedIndex = 0;
                    txtValue.Text = "0";
                    txtMinPurchase.Text = "0";
                    txtMaxUses.Text = "-1";
                }

                mainPanel.Controls.Add(CreateEditorLabel("Code:"), 0, 0);
                mainPanel.Controls.Add(txtCode, 1, 0);
                mainPanel.Controls.Add(CreateEditorLabel("Description:"), 0, 1);
                mainPanel.Controls.Add(txtDescription, 1, 1);
                mainPanel.Controls.Add(CreateEditorLabel("Type:"), 0, 2);
                mainPanel.Controls.Add(cboType, 1, 2);
                mainPanel.Controls.Add(CreateEditorLabel("Value:"), 0, 3);
                mainPanel.Controls.Add(txtValue, 1, 3);
                mainPanel.Controls.Add(CreateEditorLabel("Min. Purchase:"), 0, 4);
                mainPanel.Controls.Add(txtMinPurchase, 1, 4);
                mainPanel.Controls.Add(CreateEditorLabel("Max Uses:"), 0, 5);
                mainPanel.Controls.Add(txtMaxUses, 1, 5);
                mainPanel.Controls.Add(chkExpires, 0, 6);
                mainPanel.Controls.Add(dtpExpiry, 1, 6);
                mainPanel.Controls.Add(CreateEditorLabel("Active:"), 0, 7);
                mainPanel.Controls.Add(chkActive, 1, 7);

                Label lblHint = new Label
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 6, 0, 6)
                };
                clsFormTheme.ApplyLabelStyle(lblHint);
                lblHint.Font = clsFormTheme.SmallFont;
                lblHint.ForeColor = clsFormTheme.TextSecondary;

                EventHandler updateHint = delegate
                {
                    lblHint.Text = DescribeValueInput(SelectedType(cboType))
                        + Environment.NewLine
                        + "Max Uses: -1 for unlimited.";
                };
                cboType.SelectedIndexChanged += updateHint;
                updateHint(null, EventArgs.Empty);

                Button btnSave = CreateDialogButton("Save");
                clsFormTheme.ApplyPrimaryButtonStyle(btnSave, clsFormTheme.Icons.Save);

                Button btnCancel = CreateDialogButton("Cancel");
                clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

                FlowLayoutPanel footer = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    FlowDirection = FlowDirection.RightToLeft,
                    Height = 52,
                    Padding = new Padding(20, 8, 20, 8),
                    BackColor = Color.Transparent
                };
                footer.Controls.Add(btnSave);
                footer.Controls.Add(btnCancel);

                Panel hintPanel = new Panel { Dock = DockStyle.Bottom, Height = 44, Padding = new Padding(20, 0, 20, 0), BackColor = Color.Transparent };
                hintPanel.Controls.Add(lblHint);

                btnSave.Click += delegate
                {
                    Coupon saved = BuildCouponFromEditor(coupon, txtCode, txtDescription, cboType, txtValue,
                        txtMinPurchase, txtMaxUses, chkExpires, dtpExpiry, chkActive);

                    if (saved == null) return;

                    editor.DialogResult = DialogResult.OK;
                    editor.Close();
                };

                btnCancel.Click += delegate
                {
                    editor.DialogResult = DialogResult.Cancel;
                    editor.Close();
                };

                editor.Controls.Add(mainPanel);
                editor.Controls.Add(hintPanel);
                editor.Controls.Add(footer);
                editor.AcceptButton = btnSave;
                editor.CancelButton = btnCancel;

                return editor.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Validates the editor inputs and saves the coupon. Returns null when the input is invalid.
        /// </summary>
        private static Coupon BuildCouponFromEditor(Coupon original, TextBox txtCode, TextBox txtDescription,
            ComboBox cboType, TextBox txtValue, TextBox txtMinPurchase, TextBox txtMaxUses,
            CheckBox chkExpires, DateTimePicker dtpExpiry, CheckBox chkActive)
        {
            string code = NormalizeCode(txtCode.Text);

            if (string.IsNullOrEmpty(code))
            {
                ShowValidationError("Enter a coupon code.", txtCode);
                return null;
            }

            if (code.Contains(" "))
            {
                ShowValidationError("Coupon codes cannot contain spaces.", txtCode);
                return null;
            }

            if (original == null && GetCoupon(code) != null)
            {
                ShowValidationError("A coupon with the code '" + code + "' already exists.", txtCode);
                return null;
            }

            DiscountType type = SelectedType(cboType);

            decimal value;
            if (!decimal.TryParse(txtValue.Text, out value) || value < 0)
            {
                ShowValidationError("Value must be a number greater than or equal to zero.", txtValue);
                return null;
            }

            if (type == DiscountType.Percentage && value > 100)
            {
                ShowValidationError("A percentage discount cannot exceed 100.", txtValue);
                return null;
            }

            decimal minimumPurchase;
            if (!decimal.TryParse(txtMinPurchase.Text, out minimumPurchase) || minimumPurchase < 0)
            {
                ShowValidationError("Minimum purchase must be a number greater than or equal to zero.", txtMinPurchase);
                return null;
            }

            int maxUses;
            if (!int.TryParse(txtMaxUses.Text, out maxUses) || maxUses < -1)
            {
                ShowValidationError("Max uses must be a whole number (-1 for unlimited).", txtMaxUses);
                return null;
            }

            Coupon coupon = original ?? new Coupon { Code = code };
            coupon.Description = txtDescription.Text.Trim();
            coupon.Type = type;
            coupon.Value = value;
            coupon.MinimumPurchase = minimumPurchase;
            coupon.MaxUses = maxUses;
            coupon.IsActive = chkActive.Checked;
            coupon.ValidUntil = chkExpires.Checked ? dtpExpiry.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

            if (original == null)
                AddCoupon(coupon);

            return coupon;
        }

        private static DiscountType SelectedType(ComboBox cboType)
        {
            if (cboType.SelectedItem == null)
                return DiscountType.Percentage;

            return (DiscountType)Enum.Parse(typeof(DiscountType), cboType.SelectedItem.ToString());
        }

        private static string DescribeValueInput(DiscountType type)
        {
            switch (type)
            {
                case DiscountType.Percentage: return "Value: percentage taken off the cart (e.g. 10 = 10% off).";
                case DiscountType.FixedAmount: return "Value: fixed amount taken off the cart (e.g. 5 = " + clsLanguageManager.CurrencySymbol + " 5 off).";
                case DiscountType.BuyOneGetOne: return "Value: not used — a flat 50% is taken off the cart.";
                case DiscountType.LoyaltyPoints: return "Value: loyalty points redeemed (" + CustomerLoyalty.PointsForDiscount + " points = " + clsLanguageManager.CurrencySymbol + " 1 off).";
                default: return "Value: not used — the discount is based on the cart total.";
            }
        }

        private static void ShowValidationError(string message, Control controlToFocus)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            controlToFocus.Focus();
        }

        private static Label CreateEditorLabel(string text)
        {
            Label label = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            clsFormTheme.ApplyLabelStyle(label);
            return label;
        }

        private static TextBox CreateEditorTextBox()
        {
            TextBox textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 8, 0, 8)
            };
            clsFormTheme.ApplyTextBoxStyle(textBox);
            return textBox;
        }
    }
}
