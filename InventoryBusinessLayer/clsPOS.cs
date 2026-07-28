using System;
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

        public static DataTable GetProductsForPOS()
        {
            return clsPOSData.GetProductsForPOS();
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, out int orderID, out string errorMessage)
        {
            return clsPOSData.CompleteOrder(orderItems, taxRate, out orderID, out errorMessage);
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, int? customerID, string paymentMethod, string paymentDetails, out int orderID, out string errorMessage)
        {
            return clsPOSData.CompleteOrder(orderItems, taxRate, customerID, paymentMethod, paymentDetails, out orderID, out errorMessage);
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
            return clsProduct.GetAllProducts(); // TODO: Implement proper low stock query in data layer
        }

        public static DataTable GetRecentOrders(int count)
        {
            return clsPOSData.GetTodayOrders(); // TODO: Implement proper recent orders query in data layer
        }
    }
}
