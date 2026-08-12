using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace Inventory1PresentationLayer
{
    public partial class frmCustomersDashboard : Form
    {
        private Dictionary<string, int> _loyaltyTierData = new Dictionary<string, int>();

        public frmCustomersDashboard()
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
            _loyaltyPanel.BackColor = clsFormTheme.CardColor;
            _customerAnalyticsPanel.BackColor = clsFormTheme.CardColor;
            _segmentationPanel.BackColor = clsFormTheme.CardColor;
        }

        private void LoadDashboardData()
        {
            try
            {
                LoadLoyaltyAnalytics();
                LoadCustomerAnalytics();
                LoadSegmentationData();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, ex.Message, "Error");
            }
        }

        private void LoadLoyaltyAnalytics()
        {
            try
            {
                DataTable loyaltyData = clsAnalytics.GetTopLoyaltyMembers(10, out string errorMessage);
                
                if (loyaltyData != null && loyaltyData.Rows.Count > 0)
                {
                    gridTopLoyaltyMembers.DataSource = loyaltyData;
                    gridTopLoyaltyMembers.AutoGenerateColumns = true;
                    
                    // Build loyalty tier data for pie chart
                    _loyaltyTierData.Clear();
                    foreach (DataRow row in loyaltyData.Rows)
                    {
                        string tier = row["LoyaltyTier"].ToString();
                        if (_loyaltyTierData.ContainsKey(tier))
                            _loyaltyTierData[tier]++;
                        else
                            _loyaltyTierData[tier] = 1;
                    }
                    pnlLoyaltyChart.Invalidate();
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmCustomersDashboard.LoadLoyaltyAnalytics", ex);
            }
        }

        private void LoadCustomerAnalytics()
        {
            try
            {
                DataTable customerData = clsAnalytics.GetCustomerAnalytics(out string errorMessage);
                
                if (customerData != null && customerData.Rows.Count > 0)
                {
                    gridCustomerAnalytics.DataSource = customerData;
                    gridCustomerAnalytics.AutoGenerateColumns = true;
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmCustomersDashboard.LoadCustomerAnalytics", ex);
            }
        }

        private void LoadSegmentationData()
        {
            try
            {
                string errorMessage;
                DataTable segmentationData = clsSegment.GetAllSegments(out errorMessage);

                if (segmentationData != null)
                {
                    gridSegmentation.DataSource = segmentationData;
                    gridSegmentation.AutoGenerateColumns = true;
                    lblSegmentationTitle.Text = $"Customer Segments - {segmentationData.Rows.Count} Customers";
                }
                else if (!string.IsNullOrEmpty(errorMessage))
                {
                    lblSegmentationTitle.Text = "Error loading segmentation data";
                    clsFormTheme.ShowWarning(this, errorMessage, "Segmentation");
                }
                else
                {
                    gridSegmentation.DataSource = null;
                    lblSegmentationTitle.Text = "No segmentation data available";
                }
            }
            catch (Exception ex)
            {
                lblSegmentationTitle.Text = "Error loading segmentation data";
                clsFormTheme.ShowWarning(this, ex.Message, "Segmentation");
            }
        }

        private async void btnRunSegmentation_Click(object sender, EventArgs e)
        {
            btnRunSegmentation.Enabled = false;
            lblSegmentationTitle.Text = "Running customer segmentation... Please wait.";

            try
            {
                string errorMessage = "";
                bool success = await System.Threading.Tasks.Task.Run(() =>
                    clsMLServiceClient.TriggerSegmentTraining(out errorMessage));

                if (success)
                {
                    clsFormTheme.ShowToastSuccess(this, "Customer segmentation completed successfully!", "ML Service");
                    LoadSegmentationData();
                }
                else
                {
                    lblSegmentationTitle.Text = "Customer segmentation failed";
                    clsFormTheme.ShowWarning(this, errorMessage, "ML Service");
                }
            }
            catch (Exception ex)
            {
                lblSegmentationTitle.Text = "Customer segmentation failed";
                clsFormTheme.ShowWarning(this, ex.Message, "ML Service");
            }
            finally
            {
                btnRunSegmentation.Enabled = true;
            }
        }

        private void pnlLoyaltyChart_Paint(object sender, PaintEventArgs e)
        {
            if (_loyaltyTierData == null || _loyaltyTierData.Count == 0)
                return;
            
            DrawPieChart(e.Graphics, pnlLoyaltyChart.ClientSize, _loyaltyTierData);
        }

        private void DrawPieChart(Graphics g, Size size, Dictionary<string, int> data)
        {
            if (data == null || data.Count == 0)
                return;

            g.Clear(Color.White);
            
            int total = data.Values.Sum();
            float startAngle = 0;
            int centerX = size.Width / 2;
            int centerY = size.Height / 2;
            int radius = Math.Min(centerX, centerY) - 20;
            
            Color[] colors = new Color[]
            {
                Color.FromArgb(37, 99, 235),   // Blue
                Color.FromArgb(34, 197, 94),   // Green
                Color.FromArgb(234, 179, 8),   // Yellow
                Color.FromArgb(168, 85, 247),  // Purple
                Color.FromArgb(236, 72, 153),  // Pink
                Color.FromArgb(20, 184, 166)   // Teal
            };
            
            int colorIndex = 0;
            foreach (var kvp in data)
            {
                float sweepAngle = (float)kvp.Value / total * 360;
                
                using (Brush brush = new SolidBrush(colors[colorIndex % colors.Length]))
                {
                    g.FillPie(brush, centerX - radius, centerY - radius, radius * 2, radius * 2, startAngle, sweepAngle);
                }
                
                startAngle += sweepAngle;
                colorIndex++;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
