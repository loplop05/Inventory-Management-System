using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace Inventory1PresentationLayer
{
    public partial class frmInventoryDashboard : Form
    {
        public frmInventoryDashboard()
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
            _lowStockPanel.BackColor = clsFormTheme.CardColor;
            _associationsPanel.BackColor = clsFormTheme.CardColor;
        }

        private void LoadDashboardData()
        {
            try
            {
                LoadLowStockProducts();
                LoadAssociationsData();
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, ex.Message, "Error");
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
                    gridLowStockProducts.AutoGenerateColumns = true;
                    lblLowStockTitle.Text = $"Low Stock Products ({lowStockData.Rows.Count})";
                }
                else
                {
                    gridLowStockProducts.DataSource = null;
                    lblLowStockTitle.Text = "Low Stock Products (0)";
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.LogException("frmInventoryDashboard.LoadLowStockProducts", ex);
                lblLowStockTitle.Text = "Error loading low stock data";
            }
        }

        private void LoadAssociationsData()
        {
            try
            {
                string errorMessage;
                DataTable associationsData = clsAssociation.GetAllAssociations(50, out errorMessage);

                if (associationsData != null)
                {
                    gridAssociations.DataSource = associationsData;
                    gridAssociations.AutoGenerateColumns = true;
                    lblAssociationsTitle.Text = $"Product Associations (Top 50 by Lift) - {associationsData.Rows.Count} Rules";
                }
                else if (!string.IsNullOrEmpty(errorMessage))
                {
                    lblAssociationsTitle.Text = "Error loading association data";
                    clsFormTheme.ShowWarning(this, errorMessage, "Associations");
                }
                else
                {
                    gridAssociations.DataSource = null;
                    lblAssociationsTitle.Text = "No association data available";
                }
            }
            catch (Exception ex)
            {
                lblAssociationsTitle.Text = "Error loading association data";
                clsFormTheme.ShowWarning(this, ex.Message, "Associations");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
