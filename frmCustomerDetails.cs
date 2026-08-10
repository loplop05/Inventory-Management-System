using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmCustomerDetails : Form
    {
        private int _customerID;

        public frmCustomerDetails(int customerID)
        {
            InitializeComponent();
            _customerID = customerID;
        }

        private void frmCustomerDetails_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyGridStyle(gridCustomerOrders);
            
            _btnClose.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(_btnClose);
            
            _btnAdjustPoints.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAdjustPoints, clsFormTheme.Icons.Update);
            
            clsLanguageManager.ApplyLanguage(this);
            ApplyLocalization();
            
            LoadCustomerData();
            LoadCustomerOrders();
            
            _btnClose.Click += (s, ev) => Close();
            _btnAdjustPoints.Click += _btnAdjustPoints_Click;
        }

        private void ApplyLocalization()
        {
            _lblLoyaltyTitle.Text = clsLanguageManager.GetString("Loyalty Information");
            _lblOrdersTitle.Text = clsLanguageManager.GetString("Order History");
            _btnClose.Text = clsLanguageManager.GetString("Close");
            _btnAdjustPoints.Text = clsLanguageManager.GetString("Adjust Points");
            Text = clsLanguageManager.GetString("Customer Details");
        }

        private void LoadCustomerData()
        {
            try
            {
                string errorMessage;
                DataTable loyaltyInfo = clsLoyalty.GetCustomerLoyaltyInfo(_customerID, out errorMessage);
                
                if (loyaltyInfo != null && loyaltyInfo.Rows.Count > 0)
                {
                    DataRow row = loyaltyInfo.Rows[0];
                    
                    _lblCustomerName.Text = row["CustomerName"].ToString();
                    _lblPhoneNumber.Text = "Phone: " + row["PhoneNumber"].ToString();
                    
                    string tier = row["Tier"] != DBNull.Value ? row["Tier"].ToString() : "Bronze";
                    int points = row["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(row["LoyaltyPoints"]) : 0;
                    decimal totalSpent = row["TotalSpent"] != DBNull.Value ? Convert.ToDecimal(row["TotalSpent"]) : 0;
                    DateTime? lastPurchase = row["LastPurchaseDate"] != DBNull.Value ? Convert.ToDateTime(row["LastPurchaseDate"]) : (DateTime?)null;
                    
                    _lblTier.Text = "Tier: " + tier;
                    _lblPoints.Text = "Points: " + points.ToString();
                    _lblTotalSpent.Text = "Total Spent: $" + totalSpent.ToString("F2");
                    _lblLastPurchase.Text = "Last Purchase: " + (lastPurchase.HasValue ? lastPurchase.Value.ToString("yyyy-MM-dd") : "Never");
                    
                    // Loyalty progress info
                    string nextTier = row["NextTier"] != DBNull.Value ? row["NextTier"].ToString() : "Max";
                    decimal amountToNextTier = row["AmountToNextTier"] != DBNull.Value ? Convert.ToDecimal(row["AmountToNextTier"]) : 0;
                    decimal discountAvailable = row["DiscountAvailable"] != DBNull.Value ? Convert.ToDecimal(row["DiscountAvailable"]) : 0;
                    
                    _lblNextTier.Text = "Next Tier: " + nextTier;
                    _lblAmountToNextTier.Text = amountToNextTier > 0 ? "Amount to " + nextTier + ": $" + amountToNextTier.ToString("F2") : "You are at the highest tier!";
                    _lblDiscountAvailable.Text = "Available Discount: $" + discountAvailable.ToString("F2");

                    // Add progress toward next tier
                    UpdateTierProgress(tier, points);
                    
                    // Add repeat-buyer info
                    UpdateRepeatBuyerInfo(loyaltyInfo);
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to load customer data: " + errorMessage, "Error");
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading customer data: " + ex.Message, "Error");
            }
        }

        private void UpdateTierProgress(string currentTier, int points)
        {
            int currentThreshold = 0;
            int nextThreshold = 0;
            
            switch (currentTier)
            {
                case "Bronze":
                    currentThreshold = 0;
                    nextThreshold = 500;
                    break;
                case "Silver":
                    currentThreshold = 500;
                    nextThreshold = 2000;
                    break;
                case "Gold":
                    currentThreshold = 2000;
                    nextThreshold = 2000; // Max tier
                    break;
            }
            
            if (currentTier == "Gold")
            {
                _lblTierProgress.Text = "Progress: Max Tier Reached";
                _lblTierProgress.ForeColor = clsFormTheme.SuccessColor;
            }
            else
            {
                int progress = points - currentThreshold;
                int needed = nextThreshold - currentThreshold;
                double percentage = (double)progress / needed * 100;
                _lblTierProgress.Text = $"Progress: {progress}/{needed} points ({percentage:F0}%)";
                _lblTierProgress.ForeColor = Color.FromArgb(44, 62, 80);
            }
        }

        private void UpdateRepeatBuyerInfo(DataTable loyaltyInfo)
        {
            try
            {
                // Get order count from customer orders
                DataTable orders = clsCustomer.GetCustomerOrders(_customerID);
                int orderCount = orders != null ? orders.Rows.Count : 0;
                
                // Calculate days since last purchase
                DateTime? lastPurchase = null;
                if (loyaltyInfo.Rows.Count > 0 && loyaltyInfo.Rows[0]["LastPurchaseDate"] != DBNull.Value)
                {
                    lastPurchase = Convert.ToDateTime(loyaltyInfo.Rows[0]["LastPurchaseDate"]);
                }
                
                int daysSinceLastPurchase = 0;
                if (lastPurchase.HasValue)
                {
                    daysSinceLastPurchase = (DateTime.Now - lastPurchase.Value).Days;
                }
                
                // Display repeat-buyer info
                _lblOrderCount.Text = "Total Orders: " + orderCount.ToString();
                _lblDaysSinceLastPurchase.Text = "Days Since Last Purchase: " + (daysSinceLastPurchase > 0 ? daysSinceLastPurchase.ToString() : "Never");
                
                // Flag repeat buyers (3+ orders)
                if (orderCount >= 3)
                {
                    _lblRepeatBuyerBadge.Text = "★ Repeat Buyer";
                    _lblRepeatBuyerBadge.ForeColor = clsFormTheme.SuccessColor;
                    _lblRepeatBuyerBadge.Visible = true;
                }
                else
                {
                    _lblRepeatBuyerBadge.Visible = false;
                }
            }
            catch
            {
                _lblOrderCount.Text = "Total Orders: 0";
                _lblDaysSinceLastPurchase.Text = "Days Since Last Purchase: N/A";
                _lblRepeatBuyerBadge.Visible = false;
            }
        }

        private void LoadCustomerOrders()
        {
            try
            {
                DataTable orders = clsCustomer.GetCustomerOrders(_customerID);
                
                if (orders != null && orders.Rows.Count > 0)
                {
                    gridCustomerOrders.DataSource = orders;
                    
                    // Hide internal columns
                    if (gridCustomerOrders.Columns.Contains("CustomerID"))
                        gridCustomerOrders.Columns["CustomerID"].Visible = false;
                    
                    // Rename columns
                    if (gridCustomerOrders.Columns.Contains("OrderID"))
                        gridCustomerOrders.Columns["OrderID"].HeaderText = "Order ID";
                    if (gridCustomerOrders.Columns.Contains("OrderDate"))
                        gridCustomerOrders.Columns["OrderDate"].HeaderText = "Date";
                    if (gridCustomerOrders.Columns.Contains("TotalAmount"))
                        gridCustomerOrders.Columns["TotalAmount"].HeaderText = "Total";
                    if (gridCustomerOrders.Columns.Contains("PaymentMethod"))
                        gridCustomerOrders.Columns["PaymentMethod"].HeaderText = "Payment";
                    if (gridCustomerOrders.Columns.Contains("Status"))
                        gridCustomerOrders.Columns["Status"].HeaderText = "Status";
                }
                else
                {
                    gridCustomerOrders.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading orders: " + ex.Message, "Error");
            }
        }

        private void _btnAdjustPoints_Click(object sender, EventArgs ev)
        {
            using (var form = new frmAdjustPoints(_customerID))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    LoadCustomerData();
                }
            }
        }
    }
}
