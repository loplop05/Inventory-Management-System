using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmDashboard : Form
    {
        private List<decimal> _salesTrendData;
        private List<int> _ordersTrendData;

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

            // Wire up sparkline paint events
            pnlSalesSparkline.Paint += pnlSalesSparkline_Paint;
            pnlOrdersSparkline.Paint += pnlOrdersSparkline_Paint;
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

                // Load recent orders
                LoadRecentOrders();

                // Load top products
                LoadTopProducts();

                // Load sparkline data
                LoadSparklineData();
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
                    lblTodaySalesValue.Text = 0m.ToString("C");
                    lblTotalOrdersValue.Text = "0";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadTodaySales", ex);
                lblTodaySalesValue.Text = 0m.ToString("C");
                lblTotalOrdersValue.Text = "0";
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
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadRecentOrders", ex);
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
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadTopProducts", ex);
                gridTopProducts.DataSource = null;
            }
        }

        private void LoadInventorySummary()
        {
            // Not used in new design - summary cards show Today's Sales, Total Orders, Low Stock Alerts
        }

        private void LoadSparklineData()
        {
            try
            {
                string errorMessage;
                DateTime startDate = DateTime.Today.AddDays(-6);
                DateTime endDate = DateTime.Today;
                DataTable salesData = clsAnalytics.GetSalesByDateRange(startDate, endDate, out errorMessage);

                _salesTrendData = new List<decimal>();
                _ordersTrendData = new List<int>();

                // Build a lookup keyed by date, independent of column-name assumptions on Select()
                var byDate = new Dictionary<DateTime, (decimal sales, int orders)>();
                if (salesData != null)
                {
                    foreach (DataRow row in salesData.Rows)
                    {
                        DateTime saleDate = Convert.ToDateTime(row["SaleDate"]).Date;
                        decimal sales = row["TotalSales"] != DBNull.Value ? Convert.ToDecimal(row["TotalSales"]) : 0;
                        int orders = row["OrderCount"] != DBNull.Value ? Convert.ToInt32(row["OrderCount"]) : 0;
                        byDate[saleDate] = (sales, orders);
                    }
                }

                for (int i = 0; i < 7; i++)
                {
                    DateTime day = startDate.AddDays(i).Date;
                    if (byDate.TryGetValue(day, out var dayData))
                    {
                        _salesTrendData.Add(dayData.sales);
                        _ordersTrendData.Add(dayData.orders);
                    }
                    else
                    {
                        _salesTrendData.Add(0);
                        _ordersTrendData.Add(0);
                    }
                }

                pnlSalesSparkline.Invalidate();
                pnlOrdersSparkline.Invalidate();
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadSparklineData", ex);
                _salesTrendData = new List<decimal>(new decimal[7]);
                _ordersTrendData = new List<int>(new int[7]);
            }
        }

        private void pnlSalesSparkline_Paint(object sender, PaintEventArgs e)
        {
            if (_salesTrendData == null || _salesTrendData.Count == 0)
                return;
            
            DrawSparkline(e.Graphics, pnlSalesSparkline.ClientSize, _salesTrendData, Color.FromArgb(37, 99, 235));
        }

        private void pnlOrdersSparkline_Paint(object sender, PaintEventArgs e)
        {
            if (_ordersTrendData == null || _ordersTrendData.Count == 0)
                return;
            
            var decimalData = _ordersTrendData.Select(x => (decimal)x).ToList();
            DrawSparkline(e.Graphics, pnlOrdersSparkline.ClientSize, decimalData, Color.FromArgb(71, 85, 105));
        }

        private void DrawSparkline(Graphics g, Size size, List<decimal> data, Color color)
        {
            if (data == null || data.Count < 2)
                return;
            
            decimal max = data.Max();
            decimal min = data.Min();
            
            if (max == min)
            {
                max += 1;
                min = Math.Max(0, min - 1);
            }
            
            float padding = 2;
            float width = size.Width - padding * 2;
            float height = size.Height - padding * 2;
            
            List<PointF> points = new List<PointF>();
            
            for (int i = 0; i < data.Count; i++)
            {
                float x = padding + (i / (float)(data.Count - 1)) * width;
                float normalizedValue = (float)((data[i] - min) / (max - min));
                float y = padding + height - (normalizedValue * height);
                points.Add(new PointF(x, y));
            }
            
            using (Pen pen = new Pen(color, 2))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLines(pen, points.ToArray());
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
