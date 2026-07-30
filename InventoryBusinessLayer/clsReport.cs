using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsReport
    {
        public static DataTable GetStockValuationReport()
        {
            return clsReportData.GetStockValuationReport();
        }

        public static DataTable GetDailySales(DateTime date)
        {
            return clsReportData.GetDailySales(date);
        }

        public static DataTable GetSalesByDateRange(DateTime start, DateTime end)
        {
            return clsReportData.GetSalesByDateRange(start, end);
        }

        public static DataTable GetTopProducts(DateTime date, int topN = 5)
        {
            return clsReportData.GetTopProducts(date, topN);
        }

        public static DataTable GetCategoryPerformance(DateTime start, DateTime end)
        {
            return clsReportData.GetCategoryPerformance(start, end);
        }

        public static DataTable GetSupplierPerformance(DateTime start, DateTime end)
        {
            return clsReportData.GetSupplierPerformance(start, end);
        }

        public static DataTable GetProductPerformance(DateTime start, DateTime end)
        {
            return clsReportData.GetProductPerformance(start, end);
        }

        public static DataTable GetProfitMargin(DateTime start, DateTime end)
        {
            return clsReportData.GetProfitMargin(start, end);
        }

        public static DataTable GetCustomerAnalysis(DateTime start, DateTime end)
        {
            return clsReportData.GetCustomerAnalysis(start, end);
        }

        public static DataTable GetStockMovement(DateTime start, DateTime end)
        {
            return clsReportData.GetStockMovement(start, end);
        }
    }
}
