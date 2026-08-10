using System;
using System.Collections.Generic;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsPOS
    {
        public static bool EnsurePosSetupAndSampleData(out string errorMessage)
        {
            return clsPOSData.EnsurePosSetupAndSampleData(out errorMessage);
        }

        public static bool GetProductsForPOS(out DataTable products, out string errorMessage)
        {
            return clsPOSData.GetProductsForPOS(out products, out errorMessage);
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, out int orderID, out string errorMessage)
        {
            return clsPOSData.CompleteOrder(orderItems, taxRate, out orderID, out errorMessage);
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, int? customerID, string paymentMethod, string paymentDetails, out int orderID, out string errorMessage)
        {
            return clsPOSData.CompleteOrder(orderItems, taxRate, customerID, paymentMethod, paymentDetails, 0, null, out orderID, out errorMessage);
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, int? customerID, string paymentMethod, string paymentDetails, decimal discountAmount, string couponCode, out int orderID, out string errorMessage)
        {
            return clsPOSData.CompleteOrder(orderItems, taxRate, customerID, paymentMethod, paymentDetails, discountAmount, couponCode, null, out orderID, out errorMessage);
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, int? customerID, string paymentMethod, string paymentDetails, decimal discountAmount, string couponCode, int? shiftID, out int orderID, out string errorMessage)
        {
            bool success = clsPOSData.CompleteOrder(orderItems, taxRate, customerID, paymentMethod, paymentDetails, discountAmount, couponCode, shiftID, out orderID, out errorMessage);
            
            // Update customer's loyalty points using the consolidated business logic layer
            // This happens after order completion to ensure atomicity of the order itself
            if (success && customerID.HasValue)
            {
                // Calculate subtotal for loyalty points
                decimal subtotal = 0;
                foreach (DataRow row in orderItems.Rows)
                {
                    subtotal += Convert.ToDecimal(row["Subtotal"]);
                }
                
                string loyaltyErrorMessage;
                if (!clsLoyalty.UpdateCustomerLoyalty(customerID.Value, subtotal, out loyaltyErrorMessage))
                {
                    // Log the error but don't fail the order - the order is already completed
                    // In production, this should be logged to a proper error log
                    errorMessage += " (Loyalty update warning: " + loyaltyErrorMessage + ")";
                }
            }
            
            return success;
        }

        public static DataTable GetTodayOrderSummary()
        {
            return clsPOSData.GetTodayOrderSummary();
        }

        public static DataTable GetTodayOrders()
        {
            return clsPOSData.GetTodayOrders();
        }

        public static DataTable GetTodayTopSellingProducts()
        {
            return clsPOSData.GetTodayTopSellingProducts();
        }

        public static DataTable GetLowStockProducts(int threshold)
        {
            string errorMessage;
            DataTable allProducts;
            if (!clsProduct.GetAllProducts(out allProducts, out errorMessage))
            {
                return new DataTable();
            }
            
            DataTable lowStock = allProducts.Clone();
            
            foreach (DataRow row in allProducts.Rows)
            {
                int quantity = 0;
                if (row["Quantity"] != DBNull.Value)
                {
                    quantity = Convert.ToInt32(row["Quantity"]);
                }
                
                if (quantity < threshold)
                {
                    lowStock.ImportRow(row);
                }
            }
            
            return lowStock;
        }

        public static DataTable GetRecentOrders(int count)
        {
            return clsPOSData.GetRecentOrders(count);
        }

        public static bool ProcessExchange(int orderID, List<clsPOSData.ExchangeItemInfo> returnedItems, List<clsPOSData.ReplacementItemInfo> replacementItems, out string errorMessage)
        {
            return clsPOSData.ProcessExchange(orderID, returnedItems, replacementItems, out errorMessage);
        }

        public static bool VoidOrder(int orderID, string reason, string voidedBy, out string errorMessage)
        {
            bool success = clsPOSData.VoidOrder(orderID, reason, voidedBy, out errorMessage);
            
            // Deduct loyalty points using the consolidated business logic layer
            // This happens after void completion to ensure atomicity of the void operation itself
            if (success)
            {
                // Query the order to get customerID and total amount for loyalty deduction
                DataTable orderInfo = clsPOSData.GetOrderById(orderID, out errorMessage);
                if (orderInfo != null && orderInfo.Rows.Count > 0)
                {
                    DataRow orderRow = orderInfo.Rows[0];
                    int? customerID = orderRow["CustomerID"] != DBNull.Value ? (int?)Convert.ToInt32(orderRow["CustomerID"]) : null;
                    decimal totalAmount = orderRow["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(orderRow["TotalAmount"]) : 0;
                    
                    if (customerID.HasValue && totalAmount > 0)
                    {
                        int pointsToDeduct = (int)Math.Floor(totalAmount);
                        if (pointsToDeduct > 0)
                        {
                            string loyaltyErrorMessage;
                            if (!clsLoyalty.DeductLoyaltyPointsOnVoid(customerID.Value, pointsToDeduct, out loyaltyErrorMessage))
                            {
                                // Log the error but don't fail the void - the order is already voided
                                errorMessage += " (Loyalty deduction warning: " + loyaltyErrorMessage + ")";
                            }
                        }
                    }
                }
            }
            
            return success;
        }
    }
}
