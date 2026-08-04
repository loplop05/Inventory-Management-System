using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Low stock alert management system.
    /// Monitors inventory levels and provides alerts when products fall below threshold.
    /// </summary>
    public static class clsLowStockAlerts
    {
        // ─── Alert Configuration ────────────────────────────────────────────────

        private static int _defaultThreshold = 5;
        private static bool _alertsEnabled = true;
        private static System.Windows.Forms.Timer _checkTimer;
        private static Form _alertForm;

        /// <summary>Default threshold for low stock alerts.</summary>
        public static int DefaultThreshold
        {
            get => _defaultThreshold;
            set => _defaultThreshold = value > 0 ? value : 5;
        }

        /// <summary>Whether low stock alerts are enabled.</summary>
        public static bool AlertsEnabled
        {
            get => _alertsEnabled;
            set => _alertsEnabled = value;
        }

        // ─── Alert Data ────────────────────────────────────────────────────────

        public class LowStockItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int CurrentStock { get; set; }
            public int Threshold { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
            public string Supplier { get; set; }

            public int StockNeeded => Math.Max(0, Threshold - CurrentStock);
            public bool IsOutOfStock => CurrentStock == 0;
            public bool IsCritical => CurrentStock == 0 || CurrentStock < Threshold / 2;
        }

        // ─── Alert Checking ────────────────────────────────────────────────────

        /// <summary>
        /// Gets all products with stock below threshold.
        /// </summary>
        public static List<LowStockItem> GetLowStockItems(int threshold = -1)
        {
            var items = new List<LowStockItem>();
            int effectiveThreshold = threshold > 0 ? threshold : _defaultThreshold;

            try
            {
                string errorMessage;
                DataTable products;
                if (!clsProduct.GetAllProducts(out products, out errorMessage))
                {
                    return items;
                }
                if (products == null) return items;

                foreach (DataRow row in products.Rows)
                {
                    int quantity = Convert.ToInt32(row["Quantity"]);

                    if (quantity < effectiveThreshold)
                    {
                        items.Add(new LowStockItem
                        {
                            ProductID = Convert.ToInt32(row["ProductID"]),
                            ProductName = row["ProductName"].ToString(),
                            CurrentStock = quantity,
                            Threshold = effectiveThreshold,
                            Price = Convert.ToDecimal(row["Price"]),
                            Category = row["CategoryName"]?.ToString() ?? "N/A",
                            Supplier = row["SupplierName"]?.ToString() ?? "N/A"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking low stock: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return items;
        }

        /// <summary>
        /// Gets products that are completely out of stock.
        /// </summary>
        public static List<LowStockItem> GetOutOfStockItems()
        {
            var items = GetLowStockItems(1); // Threshold of 1 means out of stock
            return items.FindAll(item => item.IsOutOfStock);
        }

        /// <summary>
        /// Gets critical low stock items (below 50% of threshold).
        /// </summary>
        public static List<LowStockItem> GetCriticalItems()
        {
            var items = GetLowStockItems();
            return items.FindAll(item => item.IsCritical);
        }

        /// <summary>
        /// Gets the count of low stock items.
        /// </summary>
        public static int GetLowStockCount(int threshold = -1)
        {
            return GetLowStockItems(threshold).Count;
        }

        // ─── Alert Display ────────────────────────────────────────────────────

        /// <summary>
        /// Shows a low stock alert popup if there are items below threshold.
        /// </summary>
        public static void ShowAlertIfNeeded(int threshold = -1)
        {
            if (!_alertsEnabled) return;

            var items = GetLowStockItems(threshold);
            if (items.Count == 0) return;

            ShowAlertForm(items);
        }

        /// <summary>
        /// Shows the low stock alert form with all low stock items.
        /// </summary>
        public static void ShowAlertForm(List<LowStockItem> items = null)
        {
            if (_alertForm != null && !_alertForm.IsDisposed)
            {
                _alertForm.BringToFront();
                _alertForm.Focus();
                return;
            }

            var lowStockItems = items ?? GetLowStockItems();
            if (lowStockItems.Count == 0)
            {
                MessageBox.Show("No low stock items found.", "Low Stock Alert", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _alertForm = new Form
            {
                Text = "Low Stock Alert",
                Size = new System.Drawing.Size(700, 500),
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var mainPanel = new System.Windows.Forms.TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50));

            // Header
            var headerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = clsFormTheme.DangerColor
            };
            var headerLabel = new Label
            {
                Text = $"⚠ Low Stock Alert - {lowStockItems.Count} Items Need Attention",
                Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            headerPanel.Controls.Add(headerLabel);

            // Grid
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = lowStockItems
            };

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Product",
                Name = "colProduct",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CurrentStock",
                HeaderText = "Stock",
                Name = "colStock",
                Width = 60
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StockNeeded",
                HeaderText = "Needed",
                Name = "colNeeded",
                Width = 60
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Category",
                HeaderText = "Category",
                Name = "colCategory",
                Width = 100
            });

            clsFormTheme.ApplyGridStyle(grid);

            // Buttons
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            var btnClose = new Button
            {
                Text = "Close",
                Size = new System.Drawing.Size(100, 30),
                Location = new System.Drawing.Point(580, 10)
            };
            clsFormTheme.ApplySecondaryButtonStyle(btnClose, clsFormTheme.Icons.Exit);
            btnClose.Click += (s, e) => _alertForm.Close();

            var btnViewProducts = new Button
            {
                Text = "View Products",
                Size = new System.Drawing.Size(120, 30),
                Location = new System.Drawing.Point(450, 10)
            };
            clsFormTheme.ApplyPrimaryButtonStyle(btnViewProducts, clsFormTheme.Icons.Products);
            btnViewProducts.Click += (s, e) =>
            {
                _alertForm.Close();
                var productsForm = new frmProductsManagment();
                productsForm.Show();
            };

            var btnReorder = new Button
            {
                Text = "Reorder Selected",
                Size = new System.Drawing.Size(140, 30),
                Location = new System.Drawing.Point(300, 10)
            };
            clsFormTheme.ApplySuccessButtonStyle(btnReorder, clsFormTheme.Icons.Add);
            btnReorder.Click += (s, e) =>
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a product to reorder.", "Reorder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var selectedItem = grid.SelectedRows[0].DataBoundItem as LowStockItem;
                if (selectedItem != null)
                {
                    _alertForm.Close();
                    ShowReorderDialog(selectedItem);
                }
            };

            buttonPanel.Controls.Add(btnClose);
            buttonPanel.Controls.Add(btnViewProducts);
            buttonPanel.Controls.Add(btnReorder);

            mainPanel.Controls.Add(headerPanel, 0, 0);
            mainPanel.Controls.Add(grid, 0, 1);
            mainPanel.Controls.Add(buttonPanel, 0, 2);

            _alertForm.Controls.Add(mainPanel);
            _alertForm.FormClosed += (s, e) => _alertForm = null;

            _alertForm.Show();
        }

        // ─── Automatic Monitoring ───────────────────────────────────────────────

        /// <summary>
        /// Starts automatic low stock checking at specified interval.
        /// </summary>
        public static void StartMonitoring(int intervalMinutes = 30)
        {
            if (_checkTimer != null)
            {
                _checkTimer.Stop();
                _checkTimer.Dispose();
            }

            _checkTimer = new System.Windows.Forms.Timer
            {
                Interval = intervalMinutes * 60 * 1000 // Convert to milliseconds
            };

            _checkTimer.Tick += (s, e) =>
            {
                if (_alertsEnabled)
                {
                    ShowAlertIfNeeded();
                }
            };

            _checkTimer.Start();
        }

        /// <summary>
        /// Stops automatic low stock monitoring.
        /// </summary>
        public static void StopMonitoring()
        {
            if (_checkTimer != null)
            {
                _checkTimer.Stop();
                _checkTimer.Dispose();
                _checkTimer = null;
            }
        }

        // ─── Email Notifications (Future Enhancement) ───────────────────────────

        /// <summary>
        /// Sends low stock alert via email (placeholder for future implementation).
        /// </summary>
        public static void SendEmailAlert(List<LowStockItem> items, string recipientEmail)
        {
            // TODO: Implement email notification system
            // This would require SMTP configuration and email template
            MessageBox.Show($"Email notification would be sent to {recipientEmail} for {items.Count} low stock items.", 
                "Email Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ─── Summary Report ───────────────────────────────────────────────────

        /// <summary>
        /// Gets a summary text of low stock status.
        /// </summary>
        public static string GetSummary()
        {
            var items = GetLowStockItems();
            var outOfStock = GetOutOfStockItems();
            var critical = GetCriticalItems();

            return $@"Low Stock Summary
═══════════════════════════════════════════════════════════
Total Low Stock Items: {items.Count}
Out of Stock: {outOfStock.Count}
Critical (below 50% threshold): {critical.Count}
Default Threshold: {_defaultThreshold}
Alerts Enabled: {_alertsEnabled}";
        }

        // ─── Reorder Dialog ─────────────────────────────────────────────────────

        /// <summary>
        /// Shows a reorder dialog for a specific low stock item.
        /// </summary>
        private static void ShowReorderDialog(LowStockItem item)
        {
            using (Form reorderForm = new Form
            {
                Text = "Reorder Product",
                Size = new System.Drawing.Size(450, 350),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false
            })
            {
                clsFormTheme.ApplyFormStyle(reorderForm);
                clsFormTheme.CreateHeaderPanel(reorderForm, "Reorder Product", clsFormTheme.Icons.Add);

                var mainPanel = new System.Windows.Forms.TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 6,
                    Padding = new System.Windows.Forms.Padding(20)
                };
                mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120));
                mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100));

                // Product info
                var lblProduct = new Label { Text = "Product:", Anchor = AnchorStyles.Left };
                var txtProduct = new TextBox { Text = item.ProductName, ReadOnly = true, Dock = DockStyle.Fill };
                var lblSupplier = new Label { Text = "Supplier:", Anchor = AnchorStyles.Left };
                var txtSupplier = new TextBox { Text = item.Supplier, ReadOnly = true, Dock = DockStyle.Fill };
                var lblCurrentStock = new Label { Text = "Current Stock:", Anchor = AnchorStyles.Left };
                var txtCurrentStock = new TextBox { Text = item.CurrentStock.ToString(), ReadOnly = true, Dock = DockStyle.Fill };
                var lblReorderQty = new Label { Text = "Reorder Qty:", Anchor = AnchorStyles.Left };
                var txtReorderQty = new TextBox { Text = item.StockNeeded.ToString(), Dock = DockStyle.Fill };
                var lblNotes = new Label { Text = "Notes:", Anchor = AnchorStyles.Left };
                var txtNotes = new TextBox { Multiline = true, Height = 60, Dock = DockStyle.Fill };

                clsFormTheme.ApplyLabelStyle(lblProduct);
                clsFormTheme.ApplyLabelStyle(lblSupplier);
                clsFormTheme.ApplyLabelStyle(lblCurrentStock);
                clsFormTheme.ApplyLabelStyle(lblReorderQty);
                clsFormTheme.ApplyLabelStyle(lblNotes);
                clsFormTheme.ApplyTextBoxStyle(txtProduct);
                clsFormTheme.ApplyTextBoxStyle(txtSupplier);
                clsFormTheme.ApplyTextBoxStyle(txtCurrentStock);
                clsFormTheme.ApplyTextBoxStyle(txtReorderQty);
                clsFormTheme.ApplyTextBoxStyle(txtNotes);

                mainPanel.Controls.Add(lblProduct, 0, 0);
                mainPanel.Controls.Add(txtProduct, 1, 0);
                mainPanel.Controls.Add(lblSupplier, 0, 1);
                mainPanel.Controls.Add(txtSupplier, 1, 1);
                mainPanel.Controls.Add(lblCurrentStock, 0, 2);
                mainPanel.Controls.Add(txtCurrentStock, 1, 2);
                mainPanel.Controls.Add(lblReorderQty, 0, 3);
                mainPanel.Controls.Add(txtReorderQty, 1, 3);
                mainPanel.Controls.Add(lblNotes, 0, 4);
                mainPanel.Controls.Add(txtNotes, 1, 4);

                // Buttons
                var btnPanel = new Panel { Dock = DockStyle.Fill, Height = 50 };
                var btnCreate = new Button { Text = "Create Reorder", Width = 120, Height = 35 };
                var btnCancel = new Button { Text = "Cancel", Width = 100, Height = 35 };

                clsFormTheme.ApplyPrimaryButtonStyle(btnCreate, clsFormTheme.Icons.Add);
                clsFormTheme.ApplySecondaryButtonStyle(btnCancel, clsFormTheme.Icons.Cancel);

                btnCreate.Click += (s, e) =>
                {
                    int reorderQty;
                    if (!int.TryParse(txtReorderQty.Text, out reorderQty) || reorderQty <= 0)
                    {
                        MessageBox.Show("Please enter a valid reorder quantity.", "Reorder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Create reorder record (simplified - just log it for now)
                    clsAuditLog.LogAction("Reorder Created", 
                        $"Product: {item.ProductName}, Qty: {reorderQty}, Supplier: {item.Supplier}, Notes: {txtNotes.Text}", 
                        "Inventory");

                    MessageBox.Show($"Reorder request created for {reorderQty} units of {item.ProductName}.\n\n" +
                        $"Supplier: {item.Supplier}\n" +
                        $"Notes: {txtNotes.Text}", 
                        "Reorder Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    reorderForm.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, e) => reorderForm.DialogResult = DialogResult.Cancel;

                btnPanel.Controls.Add(btnCreate);
                btnPanel.Controls.Add(btnCancel);
                btnCreate.Location = new System.Drawing.Point(180, 10);
                btnCancel.Location = new System.Drawing.Point(310, 10);

                mainPanel.Controls.Add(btnPanel, 0, 5);
                mainPanel.SetColumnSpan(btnPanel, 2);

                reorderForm.Controls.Add(mainPanel);
                reorderForm.ShowDialog();
            }
        }
    }
}
