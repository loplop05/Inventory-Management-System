using System;
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

        public static DataTable GetDailySales(DateTime date)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT COUNT(DISTINCT O.OrderID) AS OrderCount,
                                        SUM(O.Subtotal) AS TotalSales
                                 FROM Orders O
                                 WHERE CAST(O.OrderDate AS DATE) = CAST(@Date AS DATE)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
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

        public static DataTable GetTopProducts(DateTime date, int topN)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT TOP(@TopN) P.ProductName,
                                        SUM(OI.Quantity) AS TotalQuantity,
                                        SUM(OI.Subtotal) AS TotalRevenue
                                 FROM OrderItems OI
                                 INNER JOIN Products P ON OI.ProductID = P.ProductID
                                 INNER JOIN Orders O ON OI.OrderID = O.OrderID
                                 WHERE CAST(O.OrderDate AS DATE) = CAST(@Date AS DATE)
                                 GROUP BY P.ProductName
                                 ORDER BY TotalRevenue DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TopN", topN);
                    command.Parameters.AddWithValue("@Date", date);
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
