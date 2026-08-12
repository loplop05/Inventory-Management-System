using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace Inventory1PresentationLayer
{
    public partial class frmSalesDashboard : Form
    {
        private Dictionary<DateTime, decimal> _hourlySalesData = new Dictionary<DateTime, decimal>();
        private Dictionary<string, int> _categoryData = new Dictionary<string, int>();
        private Dictionary<string, decimal> _paymentData = new Dictionary<string, decimal>();

        public frmSalesDashboard()
        {
            InitializeComponent();
            ApplyTheme();
            LoadDashboardData();
        }

        private void ApplyTheme()
        {
            BackColor = clsFormTheme.BackgroundColor;
            ForeColor = clsFormTheme.TextColor;
            Font = clsFormTheme.DefaultFont;

            // Apply card styling
            ApplyCardStyling();
        }

        private void ApplyCardStyling()
        {
            _hourlySalesPanel.BackColor = clsFormTheme.CardColor;
            _categoryPanel.BackColor = clsFormTheme.CardColor;
            _paymentPanel.BackColor = clsFormTheme.CardColor;
            _forecastPanel.BackColor = clsFormTheme.CardColor;
        }

        private void LoadDashboardData()
        {
            try
            {
                LoadHourlySales();
                LoadCategoryPerformance();
                LoadPaymentMethods();
                LoadForecastData();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, ex.Message, "Error");
            }
        }

        private void LoadHourlySales()
        {
            try
            {
                DataTable hourlyData = clsAnalytics.GetHourlySales(DateTime.Today, out string errorMessage);
                
                if (hourlyData != null && hourlyData.Rows.Count > 0)
                {
                    _hourlySalesData.Clear();
                    foreach (DataRow row in hourlyData.Rows)
                    {
                        DateTime hour = Convert.ToDateTime(row["Hour"]);
                        decimal amount = Convert.ToDecimal(row["TotalAmount"]);
                        _hourlySalesData[hour] = amount;
                    }
                    pnlHourlyChart.Invalidate();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmSalesDashboard.LoadHourlySales", ex);
            }
        }

        private void LoadCategoryPerformance()
        {
            try
            {
                DataTable categoryData = clsAnalytics.GetCategoryPerformance(DateTime.Today.AddDays(-30), DateTime.Today, out string errorMessage);
                
                if (categoryData != null && categoryData.Rows.Count > 0)
                {
                    _categoryData.Clear();
                    foreach (DataRow row in categoryData.Rows)
                    {
                        string category = row["CategoryName"].ToString();
                        int count = Convert.ToInt32(row["Quantity"]);
                        _categoryData[category] = count;
                    }
                    pnlCategoryChart.Invalidate();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmSalesDashboard.LoadCategoryPerformance", ex);
            }
        }

        private void LoadPaymentMethods()
        {
            try
            {
                DataTable paymentData = clsAnalytics.GetPaymentMethodBreakdown(DateTime.Today.AddDays(-30), DateTime.Today, out string errorMessage);
                
                if (paymentData != null && paymentData.Rows.Count > 0)
                {
                    _paymentData.Clear();
                    decimal cash = 0, visa = 0, other = 0;
                    
                    foreach (DataRow row in paymentData.Rows)
                    {
                        string method = row["PaymentMethod"].ToString();
                        decimal amount = Convert.ToDecimal(row["TotalAmount"]);
                        
                        if (method.ToLower() == "cash")
                            cash += amount;
                        else if (method.ToLower() == "visa")
                            visa += amount;
                        else
                            other += amount;
                    }
                    
                    _paymentData["Cash"] = cash;
                    _paymentData["Visa"] = visa;
                    _paymentData["Other"] = other;
                    
                    lblPaymentCash.Text = "Cash: " + clsFormTheme.FormatCurrency(cash);
                    lblPaymentVisa.Text = "Visa: " + clsFormTheme.FormatCurrency(visa);
                    lblPaymentOther.Text = "Other: " + clsFormTheme.FormatCurrency(other);
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmSalesDashboard.LoadPaymentMethods", ex);
            }
        }

        private void LoadForecastData()
        {
            try
            {
                string errorMessage;
                DataTable forecastData = clsForecast.GetNext7DayForecastSummary(out errorMessage);

                if (forecastData != null)
                {
                    gridForecast.DataSource = forecastData;
                    gridForecast.AutoGenerateColumns = true;
                    lblForecastTitle.Text = $"Sales Forecast (Next 7 Days) - {forecastData.Rows.Count} Products";
                }
                else if (!string.IsNullOrEmpty(errorMessage))
                {
                    lblForecastTitle.Text = "Error loading forecast data";
                    clsFormTheme.ShowWarning(this, errorMessage, "Forecast");
                }
                else
                {
                    gridForecast.DataSource = null;
                    lblForecastTitle.Text = "No forecast data available";
                }
            }
            catch (Exception ex)
            {
                lblForecastTitle.Text = "Error loading forecast data";
                clsFormTheme.ShowWarning(this, ex.Message, "Forecast");
            }
        }

        private async void btnRunForecast_Click(object sender, EventArgs e)
        {
            btnRunForecast.Enabled = false;
            lblForecastTitle.Text = "Training forecast model... Please wait.";

            try
            {
                string errorMessage = "";
                bool success = await System.Threading.Tasks.Task.Run(() =>
                    clsMLServiceClient.TriggerForecastTraining(out errorMessage));

                if (success)
                {
                    clsFormTheme.ShowToastSuccess(this, "Forecast training completed successfully!", "ML Service");
                    LoadForecastData();
                }
                else
                {
                    lblForecastTitle.Text = "Forecast training failed";
                    clsFormTheme.ShowWarning(this, errorMessage, "ML Service");
                }
            }
            catch (Exception ex)
            {
                lblForecastTitle.Text = "Forecast training failed";
                clsFormTheme.ShowWarning(this, ex.Message, "ML Service");
            }
            finally
            {
                btnRunForecast.Enabled = true;
            }
        }

        private void pnlHourlyChart_Paint(object sender, PaintEventArgs e)
        {
            if (_hourlySalesData.Count == 0)
                return;
            
            DrawHorizontalBarChart(e.Graphics, pnlHourlyChart.ClientSize, _hourlySalesData);
        }

        private void pnlCategoryChart_Paint(object sender, PaintEventArgs e)
        {
            if (_categoryData.Count == 0)
                return;
            
            DrawHorizontalBarChart(e.Graphics, pnlCategoryChart.ClientSize, 
                _categoryData.ToDictionary(k => k.Key, k => (decimal)k.Value));
        }

        private void DrawHorizontalBarChart(Graphics g, Size size, Dictionary<string, decimal> data)
        {
            if (data == null || data.Count == 0)
                return;

            g.Clear(Color.White);
            
            int padding = 40;
            int barHeight = (size.Height - padding * 2) / data.Count;
            int maxBarWidth = size.Width - padding * 3;
            
            decimal maxValue = data.Values.Max();
            
            int y = padding;
            foreach (var kvp in data)
            {
                int barWidth = (int)((kvp.Value / maxValue) * maxBarWidth);
                
                // Draw bar
                using (Brush brush = new SolidBrush(clsFormTheme.PrimaryColor))
                {
                    g.FillRectangle(brush, padding * 2, y, barWidth, barHeight - 5);
                }
                
                // Draw label
                using (Brush textBrush = new SolidBrush(clsFormTheme.TextColor))
                using (Font font = new Font("Segoe UI", 9))
                {
                    g.DrawString(kvp.Key, font, textBrush, 5, y);
                }
                
                y += barHeight;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
