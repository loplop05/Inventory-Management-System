using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsReportData
    {
        public static DataTable GetStockValuationReport()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT P.ProductID,
                                        P.ProductName,
                                        C.CategoryName,
                                        S.SupplierName,
                                        P.Price,
                                        P.Quantity,
                                        (P.Price * P.Quantity) AS StockValue
                                 FROM Products P
                                 INNER JOIN Categories C ON P.CategoryID = C.CategoryID
                                 INNER JOIN Suppliers S ON P.SupplierID = S.SupplierID
                                 ORDER BY StockValue DESC, P.ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch { }
                }
            }

            return dt;
        }
    }
}
