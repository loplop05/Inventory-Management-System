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

        public static DataTable GetTopProducts(DateTime date, int topN = 5)
        {
            return clsReportData.GetTopProducts(date, topN);
        }
    }
}
