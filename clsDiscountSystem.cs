using System;
using System.Collections.Generic;
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
            public const int PointsPerDollar = 1;
            public const int PointsForDiscount = 100; // 100 points = $1 discount
        }

        // ─── Coupon Management ────────────────────────────────────────────────

        private static readonly Dictionary<string, Coupon> _coupons = new Dictionary<string, Coupon>();

        /// <summary>
        /// Adds a new coupon to the system.
        /// </summary>
        public static bool AddCoupon(Coupon coupon)
        {
            if (coupon == null || string.IsNullOrWhiteSpace(coupon.Code)) return false;
            if (_coupons.ContainsKey(coupon.Code.ToUpper())) return false;

            _coupons[coupon.Code.ToUpper()] = coupon;
            return true;
        }

        /// <summary>
        /// Gets a coupon by code.
        /// </summary>
        public static Coupon GetCoupon(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;
            return _coupons.ContainsKey(code.ToUpper()) ? _coupons[code.ToUpper()] : null;
        }

        /// <summary>
        /// Validates and returns a coupon if valid.
        /// </summary>
        public static Coupon ValidateCoupon(string code, decimal purchaseAmount)
        {
            var coupon = GetCoupon(code);
            if (coupon == null || !coupon.IsValid()) return null;
            if (purchaseAmount < coupon.MinimumPurchase) return null;

            return coupon;
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
            return _coupons.Remove(code.ToUpper());
        }

        // ─── Customer Loyalty Management ───────────────────────────────────────

        private static readonly Dictionary<int, CustomerLoyalty> _customers = new Dictionary<int, CustomerLoyalty>();

        /// <summary>
        /// Adds or updates a customer's loyalty information.
        /// </summary>
        public static void UpdateCustomerLoyalty(int customerId, string name, string phone, decimal purchaseAmount)
        {
            if (!_customers.ContainsKey(customerId))
            {
                _customers[customerId] = new CustomerLoyalty
                {
                    CustomerID = customerId,
                    CustomerName = name,
                    Phone = phone
                };
            }

            var customer = _customers[customerId];
            customer.TotalSpent += purchaseAmount;
            customer.Points += (int)(purchaseAmount * CustomerLoyalty.PointsPerDollar);
            customer.LastPurchase = DateTime.Now;
            customer.Tier = CalculateTier(customer.TotalSpent);
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
        /// Gets customer loyalty information.
        /// </summary>
        public static CustomerLoyalty GetCustomerLoyalty(int customerId)
        {
            return _customers.ContainsKey(customerId) ? _customers[customerId] : null;
        }

        /// <summary>
        /// Redeems loyalty points for a discount.
        /// </summary>
        public static decimal RedeemLoyaltyPoints(int customerId, int pointsToRedeem)
        {
            var customer = GetCustomerLoyalty(customerId);
            if (customer == null || customer.Points < pointsToRedeem) return 0;

            customer.Points -= pointsToRedeem;
            return pointsToRedeem / CustomerLoyalty.PointsForDiscount;
        }

        /// <summary>
        /// Gets tier-based discount percentage.
        /// </summary>
        public static decimal GetTierDiscount(string tier)
        {
            switch (tier)
            {
                case "Platinum": return 0.05m; // 5%
                case "Gold": return 0.03m; // 3%
                case "Silver": return 0.02m; // 2%
                default: return 0m;
            }
        }

        // ─── Discount Calculation Helpers ─────────────────────────────────────

        /// <summary>
        /// Calculates final price after applying multiple discounts.
        /// </summary>
        public static decimal CalculateFinalPrice(decimal originalPrice, List<Coupon> coupons, CustomerLoyalty customer = null)
        {
            decimal price = originalPrice;
            decimal totalDiscount = 0;

            // Apply tier discount first
            if (customer != null)
            {
                decimal tierDiscount = price * GetTierDiscount(customer.Tier);
                totalDiscount += tierDiscount;
                price -= tierDiscount;
            }

            // Apply coupons
            foreach (var coupon in coupons)
            {
                if (coupon != null && coupon.IsValid())
                {
                    decimal couponDiscount = ApplyCoupon(coupon, price);
                    totalDiscount += couponDiscount;
                    price -= couponDiscount;
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
                Description = "$5 off your purchase",
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
                Description = "Redeem 100 loyalty points for $1 off",
                Type = DiscountType.LoyaltyPoints,
                Value = 100,
                MinimumPurchase = 10,
                MaxUses = -1
            });
        }

        // ─── UI Helpers ────────────────────────────────────────────────────────

        /// <summary>
        /// Shows a coupon application dialog.
        /// </summary>
        public static Coupon ShowCouponDialog(decimal currentTotal)
        {
            var dialog = new Form
            {
                Text = "Apply Coupon",
                Size = new Size(400, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            clsFormTheme.ApplyFormStyle(dialog);
            clsFormTheme.CreateHeaderPanel(dialog, "Apply Coupon", clsFormTheme.Icons.Chart);

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(20)
            };

            mainPanel.Controls.Add(new Label { Text = "Current Total:", Anchor = AnchorStyles.Left }, 0, 0);
            mainPanel.Controls.Add(new Label { Text = currentTotal.ToString("C"), Anchor = AnchorStyles.Left, ForeColor = clsFormTheme.PrimaryColor, Font = new Font("Segoe UI", 10F, FontStyle.Bold) }, 1, 0);

            mainPanel.Controls.Add(new Label { Text = "Coupon Code:", Anchor = AnchorStyles.Left }, 0, 1);
            var txtCode = new TextBox { Dock = DockStyle.Fill };
            mainPanel.Controls.Add(txtCode, 1, 1);

            mainPanel.Controls.Add(new Label { Text = string.Empty }, 0, 2);
            var lblResult = new Label { Dock = DockStyle.Fill, ForeColor = clsFormTheme.SuccessColor, Font = new Font("Segoe UI", 9F) };
            mainPanel.Controls.Add(lblResult, 1, 2);

            var btnApply = new Button { Text = "Apply", Height = 35, Margin = new Padding(0, 10, 0, 0) };
            clsFormTheme.ApplyPrimaryButtonStyle(btnApply, clsFormTheme.Icons.Check);
            btnApply.Click += (s, e) =>
            {
                var coupon = ValidateCoupon(txtCode.Text, currentTotal);
                if (coupon != null)
                {
                    decimal discount = ApplyCoupon(coupon, currentTotal);
                    lblResult.Text = $"Valid! Discount: {discount:C}";
                    dialog.Tag = coupon;
                    dialog.DialogResult = DialogResult.OK;
                    dialog.Close();
                }
                else
                {
                    lblResult.Text = "Invalid or expired coupon";
                    lblResult.ForeColor = clsFormTheme.DangerColor;
                }
            };

            var btnCancel = new Button { Text = "Cancel", Height = 35, Margin = new Padding(0, 10, 0, 0) };
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);
            btnCancel.Click += (s, e) => dialog.Close();

            mainPanel.Controls.Add(btnApply, 0, 3);
            mainPanel.Controls.Add(btnCancel, 1, 3);

            dialog.Controls.Add(mainPanel);
            dialog.AcceptButton = btnApply;
            dialog.CancelButton = btnCancel;

            return dialog.ShowDialog() == DialogResult.OK ? dialog.Tag as Coupon : null;
        }

        /// <summary>
        /// Shows a coupon management dialog.
        /// </summary>
        public static void ShowCouponManager()
        {
            var managerForm = new Form
            {
                Text = "Coupon Manager",
                Size = new Size(700, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            clsFormTheme.ApplyFormStyle(managerForm);
            clsFormTheme.CreateHeaderPanel(managerForm, "Coupon Manager", clsFormTheme.Icons.Chart);

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            // Coupon grid
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = GetAllCoupons()
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Code", HeaderText = "Code" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Description" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Type", HeaderText = "Type" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Value", HeaderText = "Value" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TimesUsed", HeaderText = "Used" });
            grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaxUses", HeaderText = "Max Uses" });
            grid.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsActive", HeaderText = "Active" });

            clsFormTheme.ApplyGridStyle(grid);

            // Button panel
            var buttonPanel = new Panel { Dock = DockStyle.Fill };

            var btnAdd = new Button { Text = "Add Coupon", Size = new Size(120, 30), Location = new Point(10, 10) };
            clsFormTheme.ApplyPrimaryButtonStyle(btnAdd, clsFormTheme.Icons.Add);
            btnAdd.Click += (s, e) => ShowCouponEditor(null, grid);

            var btnEdit = new Button { Text = "Edit Coupon", Size = new Size(120, 30), Location = new Point(140, 10) };
            clsFormTheme.ApplySecondaryButtonStyle(btnEdit, clsFormTheme.Icons.Update);
            btnEdit.Click += (s, e) =>
            {
                if (grid.SelectedRows.Count > 0)
                {
                    var coupon = grid.SelectedRows[0].DataBoundItem as Coupon;
                    ShowCouponEditor(coupon, grid);
                }
            };

            var btnDelete = new Button { Text = "Delete Coupon", Size = new Size(120, 30), Location = new Point(270, 10) };
            clsFormTheme.ApplyDangerButtonStyle(btnDelete, clsFormTheme.Icons.Delete);
            btnDelete.Click += (s, e) =>
            {
                if (grid.SelectedRows.Count > 0)
                {
                    var coupon = grid.SelectedRows[0].DataBoundItem as Coupon;
                    if (MessageBox.Show($"Delete coupon '{coupon.Code}'?", "Confirm", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        RemoveCoupon(coupon.Code);
                        grid.DataSource = GetAllCoupons();
                    }
                }
            };

            var btnClose = new Button { Text = "Close", Size = new Size(100, 30), Location = new Point(590, 10) };
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
            btnClose.Click += (s, e) => managerForm.Close();

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);
            buttonPanel.Controls.Add(btnClose);

            mainPanel.Controls.Add(grid, 0, 0);
            mainPanel.Controls.Add(buttonPanel, 0, 1);

            managerForm.Controls.Add(mainPanel);
            managerForm.ShowDialog();
        }

        private static void ShowCouponEditor(Coupon coupon, DataGridView grid)
        {
            var editor = new Form
            {
                Text = coupon == null ? "Add Coupon" : "Edit Coupon",
                Size = new Size(400, 350),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            };

            clsFormTheme.ApplyFormStyle(editor);

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 8,
                Padding = new Padding(20)
            };

            var txtCode = new TextBox { Dock = DockStyle.Fill };
            var txtDescription = new TextBox { Dock = DockStyle.Fill };
            var cboType = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cboType.Items.AddRange(Enum.GetNames(typeof(DiscountType)));
            var txtValue = new TextBox { Dock = DockStyle.Fill };
            var txtMinPurchase = new TextBox { Dock = DockStyle.Fill };
            var txtMaxUses = new TextBox { Dock = DockStyle.Fill };
            var chkActive = new CheckBox { Checked = true };

            if (coupon != null)
            {
                txtCode.Text = coupon.Code;
                txtCode.Enabled = false;
                txtDescription.Text = coupon.Description;
                cboType.SelectedItem = coupon.Type.ToString();
                txtValue.Text = coupon.Value.ToString();
                txtMinPurchase.Text = coupon.MinimumPurchase.ToString();
                txtMaxUses.Text = coupon.MaxUses.ToString();
                chkActive.Checked = coupon.IsActive;
            }

            mainPanel.Controls.Add(new Label { Text = "Code:" }, 0, 0);
            mainPanel.Controls.Add(txtCode, 1, 0);
            mainPanel.Controls.Add(new Label { Text = "Description:" }, 0, 1);
            mainPanel.Controls.Add(txtDescription, 1, 1);
            mainPanel.Controls.Add(new Label { Text = "Type:" }, 0, 2);
            mainPanel.Controls.Add(cboType, 1, 2);
            mainPanel.Controls.Add(new Label { Text = "Value:" }, 0, 3);
            mainPanel.Controls.Add(txtValue, 1, 3);
            mainPanel.Controls.Add(new Label { Text = "Min Purchase:" }, 0, 4);
            mainPanel.Controls.Add(txtMinPurchase, 1, 4);
            mainPanel.Controls.Add(new Label { Text = "Max Uses:" }, 0, 5);
            mainPanel.Controls.Add(txtMaxUses, 1, 5);
            mainPanel.Controls.Add(new Label { Text = "Active:" }, 0, 6);
            mainPanel.Controls.Add(chkActive, 1, 6);

            var btnSave = new Button { Text = "Save", Height = 35, Margin = new Padding(0, 10, 0, 0) };
            clsFormTheme.ApplyPrimaryButtonStyle(btnSave, clsFormTheme.Icons.Save);
            btnSave.Click += (s, e) =>
            {
                try
                {
                    var newCoupon = new Coupon
                    {
                        Code = txtCode.Text.ToUpper(),
                        Description = txtDescription.Text,
                        Type = (DiscountType)Enum.Parse(typeof(DiscountType), cboType.SelectedItem.ToString()),
                        Value = decimal.Parse(txtValue.Text),
                        MinimumPurchase = decimal.Parse(txtMinPurchase.Text),
                        MaxUses = int.Parse(txtMaxUses.Text),
                        IsActive = chkActive.Checked
                    };

                    if (coupon == null)
                    {
                        AddCoupon(newCoupon);
                    }
                    else
                    {
                        _coupons[coupon.Code.ToUpper()] = newCoupon;
                    }

                    grid.DataSource = GetAllCoupons();
                    editor.DialogResult = DialogResult.OK;
                    editor.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid input: " + ex.Message, "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            var btnCancel = new Button { Text = "Cancel", Height = 35, Margin = new Padding(0, 10, 0, 0) };
            clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);
            btnCancel.Click += (s, e) => editor.Close();

            mainPanel.Controls.Add(btnSave, 0, 7);
            mainPanel.Controls.Add(btnCancel, 1, 7);

            editor.Controls.Add(mainPanel);
            editor.ShowDialog();
        }
    }
}
