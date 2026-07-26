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
    }
}
