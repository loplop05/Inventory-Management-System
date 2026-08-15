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
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetStockValuationReport: {ex.Message}");
                    }
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
                                        ISNULL(SUM(O.TotalAmount), 0) AS TotalSales
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
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetDailySales: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetSalesByDateRange(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT CAST(O.OrderDate AS DATE) AS Date,
                                        COUNT(DISTINCT O.OrderID) AS OrderCount,
                                        ISNULL(SUM(O.TotalAmount), 0) AS TotalSales,
                                        ISNULL(AVG(O.TotalAmount), 0) AS AverageOrderValue
                                 FROM Orders O
                                 WHERE O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY CAST(O.OrderDate AS DATE)
                                 ORDER BY Date ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetSalesByDateRange: {ex.Message}");
                    }
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
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetTopProducts: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetCategoryPerformance(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT C.CategoryName AS Category,
                                        COUNT(DISTINCT OI.OrderID) AS OrderCount,
                                        ISNULL(SUM(OI.Subtotal), 0) AS TotalSales
                                 FROM OrderItems OI
                                 INNER JOIN Products P ON OI.ProductID = P.ProductID
                                 INNER JOIN Categories C ON P.CategoryID = C.CategoryID
                                 INNER JOIN Orders O ON OI.OrderID = O.OrderID
                                 WHERE O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY C.CategoryName
                                 ORDER BY TotalSales DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetCategoryPerformance: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetSupplierPerformance(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT S.SupplierName AS Supplier,
                                        COUNT(DISTINCT OI.OrderID) AS OrderCount,
                                        ISNULL(SUM(OI.Subtotal), 0) AS TotalSales,
                                        COUNT(DISTINCT P.ProductID) AS ProductCount
                                 FROM OrderItems OI
                                 INNER JOIN Products P ON OI.ProductID = P.ProductID
                                 INNER JOIN Suppliers S ON P.SupplierID = S.SupplierID
                                 INNER JOIN Orders O ON OI.OrderID = O.OrderID
                                 WHERE O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY S.SupplierName
                                 ORDER BY TotalSales DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetSupplierPerformance: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetProductPerformance(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT P.ProductName AS Product,
                                        C.CategoryName AS Category,
                                        ISNULL(SUM(OI.Quantity), 0) AS QuantitySold,
                                        ISNULL(SUM(OI.Subtotal), 0) AS Revenue
                                 FROM OrderItems OI
                                 INNER JOIN Products P ON OI.ProductID = P.ProductID
                                 INNER JOIN Categories C ON P.CategoryID = C.CategoryID
                                 INNER JOIN Orders O ON OI.OrderID = O.OrderID
                                 WHERE O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY P.ProductName, C.CategoryName
                                 ORDER BY Revenue DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetProductPerformance: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetProfitMargin(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT C.CategoryName AS Category,
                                        ISNULL(SUM(OI.Subtotal), 0) AS Revenue,
                                        ISNULL(SUM(OI.Quantity * (P.Price * 0.7)), 0) AS Cost,
                                        ISNULL(SUM(OI.Subtotal - (OI.Quantity * (P.Price * 0.7))), 0) AS Profit
                                 FROM OrderItems OI
                                 INNER JOIN Products P ON OI.ProductID = P.ProductID
                                 INNER JOIN Categories C ON P.CategoryID = C.CategoryID
                                 INNER JOIN Orders O ON OI.OrderID = O.OrderID
                                 WHERE O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY C.CategoryName
                                 ORDER BY Revenue DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetProfitMargin: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetCustomerAnalysis(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT ISNULL(C.CustomerName, 'Walk-in Customer') AS Customer,
                                        ISNULL(C.PhoneNumber, 'N/A') AS Phone,
                                        COUNT(DISTINCT O.OrderID) AS OrderCount,
                                        ISNULL(SUM(O.TotalAmount), 0) AS TotalSpent,
                                        MAX(O.OrderDate) AS LastOrderDate
                                 FROM Orders O
                                 LEFT JOIN Customers C ON O.CustomerID = C.CustomerID
                                 WHERE O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY ISNULL(C.CustomerName, 'Walk-in Customer'), ISNULL(C.PhoneNumber, 'N/A')
                                 ORDER BY TotalSpent DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetCustomerAnalysis: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetStockMovement(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT P.ProductName AS Product,
                                        P.Quantity AS CurrentStock,
                                        ISNULL(SUM(OI.Quantity), 0) AS StockOut
                                 FROM Products P
                                 LEFT JOIN OrderItems OI ON P.ProductID = OI.ProductID
                                 LEFT JOIN Orders O ON OI.OrderID = O.OrderID AND O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY P.ProductName, P.Quantity
                                 ORDER BY StockOut DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetStockMovement: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetSalesByPaymentMethod(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT ISNULL(O.PaymentMethod, 'Unknown') AS PaymentMethod,
                                        COUNT(DISTINCT O.OrderID) AS OrderCount,
                                        ISNULL(SUM(O.TotalAmount), 0) AS TotalSales
                                 FROM Orders O
                                 WHERE O.OrderDate >= @Start AND O.OrderDate <= @End
                                 GROUP BY O.PaymentMethod
                                 ORDER BY TotalSales DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetSalesByPaymentMethod: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetRefundReport(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT R.RefundID,
                                        R.OrderID,
                                        R.RefundDate,
                                        R.RefundAmount,
                                        R.RefundReason,
                                        R.RefundType,
                                        R.RefundMethod,
                                        U.UserName AS ProcessedBy
                                 FROM Refunds R
                                 LEFT JOIN Users U ON R.ProcessedBy = U.UserID
                                 WHERE R.RefundDate >= @Start AND R.RefundDate <= @End AND R.IsVoided = 0
                                 ORDER BY R.RefundDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetRefundReport: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetPurchaseReport(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT PO.PurchaseOrderID,
                                        S.SupplierName,
                                        PO.TotalCost,
                                        PO.Status,
                                        PO.CreatedDate,
                                        PO.ExpectedDate,
                                        U.UserName AS CreatedBy
                                 FROM PurchaseOrders PO
                                 LEFT JOIN Suppliers S ON PO.SupplierID = S.SupplierID
                                 LEFT JOIN Users U ON PO.CreatedBy = U.UserID
                                 WHERE PO.CreatedDate >= @Start AND PO.CreatedDate <= @End
                                 ORDER BY PO.CreatedDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetPurchaseReport: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetCashierShiftReport(DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT S.ShiftID,
                                        U.UserName AS Cashier,
                                        S.OpeningTime,
                                        S.ClosingTime,
                                        S.StartingCash,
                                        S.ClosingCash,
                                        S.CashSales,
                                        S.CardSales,
                                        S.Refunds,
                                        S.ExpectedCash,
                                        S.ActualCash,
                                        S.CashDifference,
                                        S.Status
                                 FROM Shifts S
                                 LEFT JOIN Users U ON S.UserID = U.UserID
                                 WHERE S.OpeningTime >= @Start AND S.OpeningTime <= @End
                                 ORDER BY S.OpeningTime DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Start", start);
                    command.Parameters.AddWithValue("@End", end);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetCashierShiftReport: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetWeeklySales(int year, int week)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT DATEPART(WEEKDAY, O.OrderDate) AS DayOfWeek,
                                        COUNT(DISTINCT O.OrderID) AS OrderCount,
                                        ISNULL(SUM(O.TotalAmount), 0) AS TotalSales
                                 FROM Orders O
                                 WHERE YEAR(O.OrderDate) = @Year AND DATEPART(WEEK, O.OrderDate) = @Week
                                 GROUP BY DATEPART(WEEKDAY, O.OrderDate)
                                 ORDER BY DayOfWeek";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Week", week);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetWeeklySales: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetMonthlySales(int year, int month)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT CAST(O.OrderDate AS DATE) AS Date,
                                        COUNT(DISTINCT O.OrderID) AS OrderCount,
                                        ISNULL(SUM(O.TotalAmount), 0) AS TotalSales
                                 FROM Orders O
                                 WHERE YEAR(O.OrderDate) = @Year AND MONTH(O.OrderDate) = @Month
                                 GROUP BY CAST(O.OrderDate AS DATE)
                                 ORDER BY Date ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Month", month);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetMonthlySales: {ex.Message}");
                    }
                }
            }

            return dt;
        }

        public static DataTable GetLowStockReport()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"SELECT P.ProductID,
                                        P.ProductName,
                                        P.Quantity,
                                        P.MinStock,
                                        (P.MinStock - P.Quantity) AS Needed,
                                        C.CategoryName,
                                        P.Price
                                 FROM Products P
                                 LEFT JOIN Categories C ON P.CategoryID = C.CategoryID AND C.IsDeleted = 0
                                 WHERE P.IsDeleted = 0 AND P.Quantity <= P.MinStock
                                 ORDER BY (P.MinStock - P.Quantity) DESC";

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
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in GetLowStockReport: {ex.Message}");
                    }
                }
            }

            return dt;
        }
    }
}
