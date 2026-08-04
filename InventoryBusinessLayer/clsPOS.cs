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
            return clsPOSData.CompleteOrder(orderItems, taxRate, customerID, paymentMethod, paymentDetails, discountAmount, couponCode, out orderID, out errorMessage);
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
            return clsPOSData.VoidOrder(orderID, reason, voidedBy, out errorMessage);
        }
    }
}
