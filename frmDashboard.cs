using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmDashboard : Form
    {
        private List<decimal> _salesTrendData;
        private List<int> _ordersTrendData;
        private Dictionary<int, decimal> _hourlySalesData;
        private Dictionary<string, decimal> _categoryData;
        private Dictionary<string, decimal> _paymentData;

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

            // Wire up sparkline paint events
            pnlSalesSparkline.Paint += pnlSalesSparkline_Paint;
            pnlOrdersSparkline.Paint += pnlOrdersSparkline_Paint;
            pnlHourlyChart.Paint += pnlHourlyChart_Paint;
            pnlCategoryChart.Paint += pnlCategoryChart_Paint;

            // Wire up section toggle buttons
            _btnSectionOverview.Click += (s, e) => SwitchSection("Overview");
            _btnSectionSales.Click += (s, e) => SwitchSection("Sales");
            _btnSectionInventory.Click += (s, e) => SwitchSection("Inventory");

            // Apply pill toggle styling
            clsFormTheme.ApplyPillToggleStyle(_btnSectionOverview, true);
            clsFormTheme.ApplyPillToggleStyle(_btnSectionSales, false);
            clsFormTheme.ApplyPillToggleStyle(_btnSectionInventory, false);
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

                // Load sparkline data
                LoadSparklineData();

                // Load hourly sales data
                LoadHourlySales();

                // Load category performance data
                LoadCategoryPerformance();

                // Load payment method breakdown
                LoadPaymentMethods();

                // Load low stock products for inventory section
                LoadLowStockProducts();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, ex.Message, "Error");
            }
        }

        private void SwitchSection(string section)
        {
            // Hide all sections
            _pnlSectionOverview.Visible = false;
            _pnlSectionSales.Visible = false;
            _pnlSectionInventory.Visible = false;

            // Reset all button styles
            clsFormTheme.ApplyPillToggleStyle(_btnSectionOverview, false);
            clsFormTheme.ApplyPillToggleStyle(_btnSectionSales, false);
            clsFormTheme.ApplyPillToggleStyle(_btnSectionInventory, false);

            // Show selected section and style button
            switch (section)
            {
                case "Overview":
                    _pnlSectionOverview.Visible = true;
                    clsFormTheme.ApplyPillToggleStyle(_btnSectionOverview, true);
                    break;
                case "Sales":
                    _pnlSectionSales.Visible = true;
                    clsFormTheme.ApplyPillToggleStyle(_btnSectionSales, true);
                    break;
                case "Inventory":
                    _pnlSectionInventory.Visible = true;
                    clsFormTheme.ApplyPillToggleStyle(_btnSectionInventory, true);
                    break;
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

                // Load 7-day trend data for sparklines
                LoadSparklineData();

                // Load hourly sales data
                LoadHourlySales();

                // Load category performance data
                LoadCategoryPerformance();

                // Load payment method breakdown
                LoadPaymentMethods();

                // Load low stock products for inventory section
                LoadLowStockProducts();
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

        private void LoadCustomerAnalytics()
        {
            try
            {
                string errorMessage;
                DataTable customerAnalytics = clsAnalytics.GetCustomerAnalytics(out errorMessage);
                
                if (customerAnalytics != null && customerAnalytics.Rows.Count > 0)
                {
                    // Could add a customer analytics panel to dashboard if needed
                    // For now, this ensures clsAnalytics is being used
                    // The data is available for future dashboard enhancements
                }
            }
            catch
            {
                // Silently fail - customer analytics is optional for dashboard
            }
        }

        private void LoadSparklineData()
        {
            try
            {
                string errorMessage;
                // Get last 7 days of sales data
                DateTime startDate = DateTime.Today.AddDays(-6);
                DateTime endDate = DateTime.Today;
                DataTable salesData = clsAnalytics.GetSalesByDateRange(startDate, endDate, out errorMessage);
                
                if (salesData != null && salesData.Rows.Count > 0)
                {
                    _salesTrendData = new List<decimal>();
                    _ordersTrendData = new List<int>();
                    
                    // Group by day and extract values
                    for (int i = 0; i < 7; i++)
                    {
                        DateTime day = startDate.AddDays(i);
                        var dayRows = salesData.Select("OrderDate = '" + day.ToString("yyyy-MM-dd") + "'");
                        
                        if (dayRows.Length > 0)
                        {
                            decimal daySales = 0;
                            int dayOrders = 0;
                            foreach (DataRow row in dayRows)
                            {
                                if (row["TotalSales"] != DBNull.Value)
                                    daySales += Convert.ToDecimal(row["TotalSales"]);
                                if (row["OrderCount"] != DBNull.Value)
                                    dayOrders += Convert.ToInt32(row["OrderCount"]);
                            }
                            _salesTrendData.Add(daySales);
                            _ordersTrendData.Add(dayOrders);
                        }
                        else
                        {
                            _salesTrendData.Add(0);
                            _ordersTrendData.Add(0);
                        }
                    }
                }
                else
                {
                    _salesTrendData = new List<decimal>(new decimal[7]);
                    _ordersTrendData = new List<int>(new int[7]);
                }
                
                // Refresh sparklines
                pnlSalesSparkline.Invalidate();
                pnlOrdersSparkline.Invalidate();
            }
            catch
            {
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

        private void LoadHourlySales()
        {
            try
            {
                string errorMessage;
                DataTable hourlyData = clsAnalytics.GetHourlySales(DateTime.Today, out errorMessage);
                
                if (hourlyData != null && hourlyData.Rows.Count > 0)
                {
                    _hourlySalesData = new Dictionary<int, decimal>();
                    
                    foreach (DataRow row in hourlyData.Rows)
                    {
                        int hour = Convert.ToInt32(row["Hour"]);
                        decimal sales = row["TotalSales"] != DBNull.Value ? Convert.ToDecimal(row["TotalSales"]) : 0;
                        _hourlySalesData[hour] = sales;
                    }
                }
                else
                {
                    _hourlySalesData = new Dictionary<int, decimal>();
                }
                
                pnlHourlyChart.Invalidate();
            }
            catch
            {
                _hourlySalesData = new Dictionary<int, decimal>();
            }
        }

        private void pnlHourlyChart_Paint(object sender, PaintEventArgs e)
        {
            if (_hourlySalesData == null || _hourlySalesData.Count == 0)
                return;
            
            DrawHourlyBarChart(e.Graphics, pnlHourlyChart.ClientSize, _hourlySalesData);
        }

        private void DrawHourlyBarChart(Graphics g, Size size, Dictionary<int, decimal> data)
        {
            if (data == null || data.Count == 0)
                return;
            
            decimal max = data.Values.Max();
            if (max == 0) max = 1;
            
            float padding = 20;
            float chartWidth = size.Width - padding * 2;
            float chartHeight = size.Height - padding * 2;
            
            int barCount = 24; // 0-23 hours
            float barWidth = chartWidth / barCount - 2;
            
            using (Font font = new Font("Segoe UI", 7))
            using (Brush barBrush = new SolidBrush(Color.FromArgb(37, 99, 235)))
            using (Brush textBrush = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    decimal sales = data.ContainsKey(hour) ? data[hour] : 0;
                    float barHeight = (float)(sales / max) * chartHeight;
                    
                    float x = padding + hour * (barWidth + 2);
                    float y = size.Height - padding - barHeight;
                    
                    g.FillRectangle(barBrush, x, y, barWidth, barHeight);
                    
                    // Draw hour label every 3 hours
                    if (hour % 3 == 0)
                    {
                        string label = hour.ToString();
                        SizeF labelSize = g.MeasureString(label, font);
                        g.DrawString(label, font, textBrush, x + (barWidth - labelSize.Width) / 2, size.Height - padding + 2);
                    }
                }
            }
        }

        private void LoadCategoryPerformance()
        {
            try
            {
                string errorMessage;
                DateTime startDate = DateTime.Today.AddDays(-7);
                DateTime endDate = DateTime.Today;
                DataTable categoryData = clsAnalytics.GetSalesByCategory(startDate, endDate, out errorMessage);
                
                if (categoryData != null && categoryData.Rows.Count > 0)
                {
                    _categoryData = new Dictionary<string, decimal>();
                    
                    foreach (DataRow row in categoryData.Rows)
                    {
                        string category = row["Category"].ToString();
                        decimal sales = row["TotalSales"] != DBNull.Value ? Convert.ToDecimal(row["TotalSales"]) : 0;
                        _categoryData[category] = sales;
                    }
                }
                else
                {
                    _categoryData = new Dictionary<string, decimal>();
                }
                
                pnlCategoryChart.Invalidate();
            }
            catch
            {
                _categoryData = new Dictionary<string, decimal>();
            }
        }

        private void pnlCategoryChart_Paint(object sender, PaintEventArgs e)
        {
            if (_categoryData == null || _categoryData.Count == 0)
                return;
            
            DrawHorizontalBarChart(e.Graphics, pnlCategoryChart.ClientSize, _categoryData);
        }

        private void DrawHorizontalBarChart(Graphics g, Size size, Dictionary<string, decimal> data)
        {
            if (data == null || data.Count == 0)
                return;
            
            decimal max = data.Values.Max();
            if (max == 0) max = 1;
            
            float padding = 10;
            float barHeight = 25;
            float barSpacing = 5;
            float labelWidth = 100;
            float valueWidth = 60;
            float chartWidth = size.Width - padding * 2 - labelWidth - valueWidth;
            
            using (Font font = new Font("Segoe UI", 9))
            using (Brush barBrush = new SolidBrush(Color.FromArgb(37, 99, 235)))
            using (Brush textBrush = new SolidBrush(Color.FromArgb(44, 62, 80)))
            {
                float y = padding;
                foreach (var kvp in data.OrderByDescending(x => x.Value).Take(6))
                {
                    float barWidthPixels = (float)(kvp.Value / max) * chartWidth;
                    
                    // Draw label
                    string label = kvp.Key.Length > 12 ? kvp.Key.Substring(0, 12) + "..." : kvp.Key;
                    g.DrawString(label, font, textBrush, padding, y);
                    
                    // Draw bar
                    g.FillRectangle(barBrush, padding + labelWidth, y, barWidthPixels, barHeight);
                    
                    // Draw value
                    string value = "$" + kvp.Value.ToString("F0");
                    SizeF valueSize = g.MeasureString(value, font);
                    g.DrawString(value, font, textBrush, padding + labelWidth + barWidthPixels + 5, y);
                    
                    y += barHeight + barSpacing;
                }
            }
        }

        private void LoadPaymentMethods()
        {
            try
            {
                string errorMessage;
                DateTime startDate = DateTime.Today.AddDays(-7);
                DateTime endDate = DateTime.Today;
                DataTable paymentData = clsAnalytics.GetPaymentMethodDistribution(startDate, endDate, out errorMessage);
                
                if (paymentData != null && paymentData.Rows.Count > 0)
                {
                    _paymentData = new Dictionary<string, decimal>();
                    
                    foreach (DataRow row in paymentData.Rows)
                    {
                        string method = row["PaymentMethod"].ToString();
                        decimal amount = row["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(row["TotalAmount"]) : 0;
                        _paymentData[method] = amount;
                    }
                    
                    // Update UI labels
                    decimal cashAmount = _paymentData.ContainsKey("Cash") ? _paymentData["Cash"] : 0;
                    decimal visaAmount = _paymentData.ContainsKey("Visa") ? _paymentData["Visa"] : 0;
                    
                    lblPaymentCash.Text = "Cash: $" + cashAmount.ToString("F0");
                    lblPaymentVisa.Text = "Visa: $" + visaAmount.ToString("F0");
                }
                else
                {
                    _paymentData = new Dictionary<string, decimal>();
                    lblPaymentCash.Text = "Cash: $0";
                    lblPaymentVisa.Text = "Visa: $0";
                }
            }
            catch
            {
                _paymentData = new Dictionary<string, decimal>();
                lblPaymentCash.Text = "Cash: $0";
                lblPaymentVisa.Text = "Visa: $0";
            }
        }

        private void LoadLowStockProducts()
        {
            try
            {
                string errorMessage;
                DataTable lowStockData = clsAnalytics.GetLowStockProducts(5, out errorMessage);
                
                if (lowStockData != null && lowStockData.Rows.Count > 0)
                {
                    gridLowStockProducts.DataSource = lowStockData;
                    gridLowStockProducts.Columns["ProductID"].Visible = false;
                    gridLowStockProducts.Columns["CategoryID"].Visible = false;
                    gridLowStockProducts.Columns["SupplierID"].Visible = false;
                    gridLowStockProducts.Columns["ProductName"].HeaderText = "Product";
                    gridLowStockProducts.Columns["CategoryName"].HeaderText = "Category";
                    gridLowStockProducts.Columns["StockQuantity"].HeaderText = "Quantity";
                    gridLowStockProducts.Columns["Price"].HeaderText = "Price";
                    gridLowStockProducts.Columns["SupplierName"].HeaderText = "Supplier";
                }
                else
                {
                    gridLowStockProducts.DataSource = null;
                }
            }
            catch
            {
                gridLowStockProducts.DataSource = null;
            }
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
