using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsAnalytics
    {
        public static DataTable GetSalesByDateRange(DateTime startDate, DateTime endDate, out string errorMessage)
        {
            if (startDate > endDate)
            {
                errorMessage = "Start date cannot be after end date.";
                return null;
            }

            return clsAnalyticsData.GetSalesByDateRange(startDate, endDate, out errorMessage);
        }

        public static DataTable GetTopSellingProducts(int topN, DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            if (topN <= 0)
            {
                errorMessage = "Top N must be greater than 0.";
                return null;
            }

            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                errorMessage = "Start date cannot be after end date.";
                return null;
            }

            return clsAnalyticsData.GetTopSellingProducts(topN, startDate, endDate, out errorMessage);
        }

        public static DataTable GetSalesByCategory(DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                errorMessage = "Start date cannot be after end date.";
                return null;
            }

            return clsAnalyticsData.GetSalesByCategory(startDate, endDate, out errorMessage);
        }

        public static DataTable GetCustomerAnalytics(out string errorMessage)
        {
            return clsAnalyticsData.GetCustomerAnalytics(out errorMessage);
        }

        public static DataTable GetHourlySales(DateTime date, out string errorMessage)
        {
            if (date > DateTime.Now)
            {
                errorMessage = "Date cannot be in the future.";
                return null;
            }

            return clsAnalyticsData.GetHourlySales(date, out errorMessage);
        }

        public static DataTable GetPaymentMethodDistribution(DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                errorMessage = "Start date cannot be after end date.";
                return null;
            }

            return clsAnalyticsData.GetPaymentMethodDistribution(startDate, endDate, out errorMessage);
        }

        public static DataTable GetLowStockProducts(int threshold, out string errorMessage)
        {
            if (threshold < 0)
            {
                errorMessage = "Threshold cannot be negative.";
                return null;
            }

            return clsAnalyticsData.GetLowStockProducts(threshold, out errorMessage);
        }

        public static DataTable GetProfitMargin(DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            {
                errorMessage = "Start date cannot be after end date.";
                return null;
            }

            return clsAnalyticsData.GetProfitMargin(startDate, endDate, out errorMessage);
        }

        public static decimal CalculateTotalRevenue(DataTable salesData)
        {
            decimal total = 0;
            foreach (DataRow row in salesData.Rows)
            {
                if (row["TotalSales"] != DBNull.Value)
                {
                    total += Convert.ToDecimal(row["TotalSales"]);
                }
            }
            return total;
        }

        public static int CalculateTotalOrders(DataTable salesData)
        {
            int total = 0;
            foreach (DataRow row in salesData.Rows)
            {
                if (row["OrderCount"] != DBNull.Value)
                {
                    total += Convert.ToInt32(row["OrderCount"]);
                }
            }
            return total;
        }

        public static decimal CalculateAverageOrderValue(DataTable salesData)
        {
            decimal totalRevenue = CalculateTotalRevenue(salesData);
            int totalOrders = CalculateTotalOrders(salesData);

            return totalOrders > 0 ? totalRevenue / totalOrders : 0;
        }
    }
}
