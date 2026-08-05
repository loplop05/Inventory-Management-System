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

            // Wire sidebar navigation
            _sidebar.NavigationRequested += OnSidebarNavigation;
            _sidebar.SetActive("Dashboard");

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

            // Apply card styling
            ApplyCardStyling();
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
                clsFormTheme.ShowError(this, ex.Message, "Error");
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

                    lblTodaySalesValue.Text = totalSales.ToString("C");
                    lblTotalOrdersValue.Text = orderCount.ToString();
                }
                else
                {
                    lblTodaySalesValue.Text = "$0.00";
                    lblTotalOrdersValue.Text = "0";
                }
            }
            catch
            {
                lblTodaySalesValue.Text = "$0.00";
                lblTotalOrdersValue.Text = "0";
            }
        }

        private void LoadLowStockAlerts()
        {
            try
            {
                DataTable lowStock = clsPOS.GetLowStockProducts(5); // Products with quantity < 5
                
                if (lowStock != null && lowStock.Rows.Count > 0)
                {
                    lblLowStockValue.Text = lowStock.Rows.Count.ToString();
                    lblLowStockValue.ForeColor = clsFormTheme.WarningColor;
                }
                else
                {
                    lblLowStockValue.Text = "0";
                    lblLowStockValue.ForeColor = clsFormTheme.SuccessColor;
                }
            }
            catch
            {
                lblLowStockValue.Text = "0";
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
                    clsFormTheme.ApplyDarkHeaderGridStyle(gridRecentOrders);
                    gridRecentOrders.AutoGenerateColumns = false;
                    
                    // Configure columns
                    if (gridRecentOrders.Columns.Count == 0)
                    {
                        gridRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "OrderDate",
                            HeaderText = "Time",
                            Name = "colTime"
                        });
                        
                        gridRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "OrderID",
                            HeaderText = "Details",
                            Name = "colDetails"
                        });
                        
                        gridRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            DataPropertyName = "TotalAmount",
                            HeaderText = "Amount",
                            Name = "colAmount"
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
                    clsFormTheme.ApplyDarkHeaderGridStyle(gridTopProducts);
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
            // Not used in new design - summary cards show Today's Sales, Total Orders, Low Stock Alerts
        }

        private void OnSidebarNavigation(string screenKey)
        {
            switch (screenKey)
            {
                case "Dashboard":
                    // Already on Dashboard
                    break;
                case "POS":
                    var posForm = new frmPOS();
                    posForm.Show();
                    this.Close();
                    break;
                case "Inventory":
                    var productsForm = new frmProductsManagment();
                    productsForm.Show();
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

        private void ApplyCardStyling()
        {
            // Apply card styling to summary cards manually (panels with accent bars)
            _cardTodaySales.BackColor = clsFormTheme.CardColor;
            _cardTodaySales.Paint += (s, e) => DrawCardAccent(e.Graphics, _cardTodaySales, clsFormTheme.PrimaryColor);
            
            _cardTotalOrders.BackColor = clsFormTheme.CardColor;
            _cardTotalOrders.Paint += (s, e) => DrawCardAccent(e.Graphics, _cardTotalOrders, clsFormTheme.SecondaryColor);
            
            _cardLowStock.BackColor = clsFormTheme.CardColor;
            _cardLowStock.Paint += (s, e) => DrawCardAccent(e.Graphics, _cardLowStock, clsFormTheme.WarningColor);
        }

        private void DrawCardAccent(Graphics g, Panel panel, Color accentColor)
        {
            // Card border
            using (Pen border = new Pen(clsFormTheme.CardBorderColor, 1))
                g.DrawRectangle(border, 0, 0, panel.Width - 1, panel.Height - 1);
            
            // Top accent bar
            using (SolidBrush accent = new SolidBrush(accentColor))
                g.FillRectangle(accent, new Rectangle(0, 0, panel.Width, 3));
        }
    }
}
