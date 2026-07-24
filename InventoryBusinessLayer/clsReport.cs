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
    }
}
