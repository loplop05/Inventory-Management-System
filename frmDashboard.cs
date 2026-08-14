using System;
using System.Data;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

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
            BackColor = System.Drawing.Color.White;
            _cardTodaySales.BackColor = System.Drawing.Color.White;
            _cardTotalOrders.BackColor = System.Drawing.Color.White;
            _cardLowStock.BackColor = System.Drawing.Color.White;
            _cardLowStock.Cursor = Cursors.Hand;
        }

        private void LoadDashboardData()
        {
            try
            {
                LoadTodaySales();
                LoadRecentOrders();
                LoadTopProducts();
                LoadLowStockAlerts();
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadDashboardData", ex);
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
                    decimal totalSales = row["TotalSales"] != DBNull.Value ? Convert.ToDecimal(row["TotalSales"]) : 0;
                    int orderCount = row["OrderCount"] != DBNull.Value ? Convert.ToInt32(row["OrderCount"]) : 0;

                    lblTodaySalesValue.Text = totalSales.ToString("C");
                    lblTotalOrdersValue.Text = orderCount.ToString();
                }
                else
                {
                    lblTodaySalesValue.Text = "$0.00";
                    lblTotalOrdersValue.Text = "0";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadTodaySales", ex);
                lblTodaySalesValue.Text = "$0.00";
                lblTotalOrdersValue.Text = "0";
            }
        }

        private void LoadRecentOrders()
        {
            try
            {
                // Skip for now - method doesn't exist in clsReport
                gridRecentOrders.DataSource = null;
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
                // Skip for now - method doesn't exist in clsReport
                gridTopProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadTopProducts", ex);
                gridTopProducts.DataSource = null;
            }
        }

        private void LoadLowStockAlerts()
        {
            try
            {
                // Skip for now - clsAnalytics may not have this method
                lblLowStockValue.Text = "0";
                lblLowStockValue.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard.LoadLowStockAlerts", ex);
                lblLowStockValue.Text = "0";
                lblLowStockValue.ForeColor = System.Drawing.Color.FromArgb(34, 197, 94);
            }
        }

        private void _cardLowStock_Click(object sender, EventArgs e)
        {
            // Open products form
            try
            {
                var productsForm = new frmProductsManagment();
                productsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmDashboard._cardLowStock_Click", ex);
                clsNotify.Error("Could not open products form.");
            }
        }
    }
}
