using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LoadDashboardData();
        }

        private void ApplyTheme()
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.CreateHeaderPanel(this, "Dashboard", clsFormTheme.Icons.Home);

            // Setup keyboard shortcuts
            clsKeyboardShortcuts.SetupCommonShortcuts(
                this,
                onEscape: () => Close(),
                onRefresh: () => LoadDashboardData(),
                onSearch: null,
                onAdd: null
            );

            // Setup help
            clsHelpSystem.SetupHelp(this, clsHelpSystem.Topics.MainMenu);

            clsLanguageManager.ApplyLanguage(this);
            EventHandler onLanguageChanged = (s, e) => ApplyLocalization();
            clsLanguageManager.LanguageChanged += onLanguageChanged;
            FormClosed += (s, e) => clsLanguageManager.LanguageChanged -= onLanguageChanged;
        }

        private void ApplyLocalization()
        {
            clsLanguageManager.ApplyLanguage(this);
            Text = clsLanguageManager.GetString("Dashboard");
        }

        private void LoadDashboardData()
        {
            try
            {
                // Load today's sales
                LoadTodaySales();

                // Load low stock alerts
                LoadLowStockAlerts();

                // Load recent orders
                LoadRecentOrders();

                // Load top products
                LoadTopProducts();

                // Load inventory summary
                LoadInventorySummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard data: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTodaySales()
        {
            try
            {
                DataTable salesData = clsReport.GetDailySales(DateTime.Today);
                
                if (salesData != null && salesData.Rows.Count > 0)
                {
                    var row = salesData.Rows[0];
                    decimal totalSales = Convert.ToDecimal(row["TotalSales"]);
                    int orderCount = Convert.ToInt32(row["OrderCount"]);

                    lblTodaySales.Text = totalSales.ToString("C");
                    lblOrderCount.Text = orderCount.ToString();
                }
                else
                {
                    lblTodaySales.Text = "$0.00";
                    lblOrderCount.Text = "0";
                }
            }
            catch
            {
                lblTodaySales.Text = "$0.00";
                lblOrderCount.Text = "0";
            }
        }

        private void LoadLowStockAlerts()
        {
            try
            {
                DataTable lowStock = clsPOS.GetLowStockProducts(5); // Products with quantity < 5
                
                if (lowStock != null && lowStock.Rows.Count > 0)
                {
                    lblLowStockCount.Text = lowStock.Rows.Count.ToString();
                    lblLowStockCount.ForeColor = clsFormTheme.DangerColor;
                    
                    gridLowStock.DataSource = lowStock;
                    clsFormTheme.ApplyGridStyle(gridLowStock);
                    gridLowStock.AutoGenerateColumns = false;
                    
                    // Configure columns
                    if (gridLowStock.Columns.Count == 0)
                    {
                        gridLowStock.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "ProductName",
                            HeaderText = "Product",
                            Name = "colProduct"
                        });
                        
                        gridLowStock.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "Quantity",
                            HeaderText = "Stock",
                            Name = "colQuantity"
                        });
                    }
                }
                else
                {
                    lblLowStockCount.Text = "0";
                    lblLowStockCount.ForeColor = clsFormTheme.SuccessColor;
                    gridLowStock.DataSource = null;
                }
            }
            catch
            {
                lblLowStockCount.Text = "0";
                gridLowStock.DataSource = null;
            }
        }

        private void LoadRecentOrders()
        {
            try
            {
                DataTable recentOrders = clsPOS.GetRecentOrders(5);
                
                if (recentOrders != null && recentOrders.Rows.Count > 0)
                {
                    gridRecentOrders.DataSource = recentOrders;
                    clsFormTheme.ApplyGridStyle(gridRecentOrders);
                    gridRecentOrders.AutoGenerateColumns = false;
                    
                    // Configure columns
                    if (gridRecentOrders.Columns.Count == 0)
                    {
                        gridRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "OrderID",
                            HeaderText = "Order #",
                            Name = "colOrderID"
                        });
                        
                        gridRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "OrderDate",
                            HeaderText = "Date",
                            Name = "colDate"
                        });
                        
                        gridRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "TotalAmount",
                            HeaderText = "Total",
                            Name = "colTotal"
                        });
                    }
                }
                else
                {
                    gridRecentOrders.DataSource = null;
                }
            }
            catch
            {
                gridRecentOrders.DataSource = null;
            }
        }

        private void LoadTopProducts()
        {
            try
            {
                DataTable topProducts = clsReport.GetTopProducts(DateTime.Today, 5);
                
                if (topProducts != null && topProducts.Rows.Count > 0)
                {
                    gridTopProducts.DataSource = topProducts;
                    clsFormTheme.ApplyGridStyle(gridTopProducts);
                    gridTopProducts.AutoGenerateColumns = false;
                    
                    // Configure columns
                    if (gridTopProducts.Columns.Count == 0)
                    {
                        gridTopProducts.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "ProductName",
                            HeaderText = "Product",
                            Name = "colProduct"
                        });
                        
                        gridTopProducts.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "TotalQuantity",
                            HeaderText = "Sold",
                            Name = "colSold"
                        });
                        
                        gridTopProducts.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "TotalRevenue",
                            HeaderText = "Revenue",
                            Name = "colRevenue"
                        });
                    }
                }
                else
                {
                    gridTopProducts.DataSource = null;
                }
            }
            catch
            {
                gridTopProducts.DataSource = null;
            }
        }

        private void LoadInventorySummary()
        {
            try
            {
                DataTable allProducts = clsProduct.GetAllProducts();
                
                if (allProducts != null)
                {
                    int totalProducts = allProducts.Rows.Count;
                    decimal totalValue = 0;
                    int totalStock = 0;

                    foreach (DataRow row in allProducts.Rows)
                    {
                        decimal price = Convert.ToDecimal(row["Price"]);
                        int quantity = Convert.ToInt32(row["Quantity"]);
                        totalValue += price * quantity;
                        totalStock += quantity;
                    }

                    lblTotalProducts.Text = totalProducts.ToString();
                    lblTotalStock.Text = totalStock.ToString();
                    lblInventoryValue.Text = totalValue.ToString("C");
                }
                else
                {
                    lblTotalProducts.Text = "0";
                    lblTotalStock.Text = "0";
                    lblInventoryValue.Text = "$0.00";
                }
            }
            catch
            {
                lblTotalProducts.Text = "0";
                lblTotalStock.Text = "0";
                lblInventoryValue.Text = "$0.00";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void btnViewLowStock_Click(object sender, EventArgs e)
        {
            var productsForm = new frmProductsManagment();
            productsForm.Show();
        }

        private void btnViewRecentOrders_Click(object sender, EventArgs e)
        {
            var receiptForm = new frmReceiptSearch();
            receiptForm.Show();
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
            var reportForm = new frmDailyReport();
            reportForm.Show();
        }

        private void btnNewSale_Click(object sender, EventArgs e)
        {
            var posForm = new frmPOS();
            posForm.Show();
        }
    }
}
