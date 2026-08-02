using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsAnalyticsData
    {
        public static DataTable GetSalesByDateRange(DateTime startDate, DateTime endDate, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            CAST(OrderDate AS DATE) as SaleDate,
                            COUNT(*) as OrderCount,
                            SUM(TotalAmount) as TotalSales,
                            SUM(Subtotal) as Subtotal,
                            SUM(TaxAmount) as TaxAmount
                        FROM Orders
                        WHERE OrderDate >= @StartDate AND OrderDate <= @EndDate
                            AND IsVoided = 0
                        GROUP BY CAST(OrderDate AS DATE)
                        ORDER BY SaleDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate.AddDays(1).AddTicks(-1));

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        public static DataTable GetTopSellingProducts(int topN, DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT TOP (@TopN)
                            p.ProductID,
                            p.ProductName,
                            p.CategoryID,
                            c.CategoryName,
                            SUM(oi.Quantity) as TotalQuantity,
                            SUM(oi.Subtotal) as TotalRevenue,
                            COUNT(DISTINCT oi.OrderID) as OrderCount
                        FROM OrderItems oi
                        INNER JOIN Products p ON oi.ProductID = p.ProductID
                        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                        INNER JOIN Orders o ON oi.OrderID = o.OrderID
                        WHERE o.IsVoided = 0";

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND o.OrderDate >= @StartDate AND o.OrderDate <= @EndDate";
                    }

                    query += @"
                        GROUP BY p.ProductID, p.ProductName, p.CategoryID, c.CategoryName
                        ORDER BY TotalRevenue DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TopN", topN);
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@StartDate", startDate.Value);
                            command.Parameters.AddWithValue("@EndDate", endDate.Value.AddDays(1).AddTicks(-1));
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        public static DataTable GetSalesByCategory(DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            c.CategoryID,
                            c.CategoryName,
                            COUNT(DISTINCT o.OrderID) as OrderCount,
                            SUM(oi.Quantity) as TotalQuantity,
                            SUM(oi.Subtotal) as TotalRevenue
                        FROM OrderItems oi
                        INNER JOIN Products p ON oi.ProductID = p.ProductID
                        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                        INNER JOIN Orders o ON oi.OrderID = o.OrderID
                        WHERE o.IsVoided = 0";

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND o.OrderDate >= @StartDate AND o.OrderDate <= @EndDate";
                    }

                    query += @"
                        GROUP BY c.CategoryID, c.CategoryName
                        ORDER BY TotalRevenue DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@StartDate", startDate.Value);
                            command.Parameters.AddWithValue("@EndDate", endDate.Value.AddDays(1).AddTicks(-1));
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        public static DataTable GetCustomerAnalytics(out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            CustomerID,
                            CustomerName,
                            PhoneNumber,
                            LoyaltyPoints,
                            TotalSpent,
                            Tier,
                            (SELECT COUNT(*) FROM Orders WHERE CustomerID = c.CustomerID AND IsVoided = 0) as OrderCount,
                            (SELECT MAX(OrderDate) FROM Orders WHERE CustomerID = c.CustomerID AND IsVoided = 0) as LastPurchaseDate
                        FROM Customers c
                        WHERE TotalSpent > 0
                        ORDER BY TotalSpent DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        public static DataTable GetHourlySales(DateTime date, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            DATEPART(HOUR, OrderDate) as Hour,
                            COUNT(*) as OrderCount,
                            SUM(TotalAmount) as TotalSales
                        FROM Orders
                        WHERE CAST(OrderDate AS DATE) = @Date AND IsVoided = 0
                        GROUP BY DATEPART(HOUR, OrderDate)
                        ORDER BY Hour";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Date", date);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        public static DataTable GetPaymentMethodDistribution(DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            PaymentMethod,
                            COUNT(*) as OrderCount,
                            SUM(TotalAmount) as TotalAmount,
                            CAST(COUNT(*) * 100.0 / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) as Percentage
                        FROM Orders
                        WHERE IsVoided = 0";

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND OrderDate >= @StartDate AND OrderDate <= @EndDate";
                    }

                    query += @"
                        GROUP BY PaymentMethod
                        ORDER BY TotalAmount DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@StartDate", startDate.Value);
                            command.Parameters.AddWithValue("@EndDate", endDate.Value.AddDays(1).AddTicks(-1));
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        public static DataTable GetLowStockProducts(int threshold, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            p.ProductID,
                            p.ProductName,
                            p.CategoryID,
                            c.CategoryName,
                            p.StockQuantity,
                            p.Price,
                            p.SupplierID,
                            s.SupplierName
                        FROM Products p
                        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                        LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID
                        WHERE p.StockQuantity <= @Threshold
                        ORDER BY p.StockQuantity ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Threshold", threshold);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }

        public static DataTable GetProfitMargin(DateTime? startDate, DateTime? endDate, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            p.ProductID,
                            p.ProductName,
                            p.CategoryID,
                            c.CategoryName,
                            p.Price as SalePrice,
                            p.CostPrice,
                            (p.Price - p.CostPrice) as ProfitPerUnit,
                            SUM(oi.Quantity) as TotalQuantity,
                            SUM(oi.Subtotal) as TotalRevenue,
                            SUM(oi.Quantity * p.CostPrice) as TotalCost,
                            SUM(oi.Subtotal) - SUM(oi.Quantity * p.CostPrice) as TotalProfit,
                            CAST((SUM(oi.Subtotal) - SUM(oi.Quantity * p.CostPrice)) * 100.0 / NULLIF(SUM(oi.Subtotal), 0) AS DECIMAL(5,2)) as ProfitMargin
                        FROM OrderItems oi
                        INNER JOIN Products p ON oi.ProductID = p.ProductID
                        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
                        INNER JOIN Orders o ON oi.OrderID = o.OrderID
                        WHERE o.IsVoided = 0";

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        query += " AND o.OrderDate >= @StartDate AND o.OrderDate <= @EndDate";
                    }

                    query += @"
                        GROUP BY p.ProductID, p.ProductName, p.CategoryID, c.CategoryName, p.Price, p.CostPrice
                        HAVING SUM(oi.Subtotal) > 0
                        ORDER BY TotalProfit DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@StartDate", startDate.Value);
                            command.Parameters.AddWithValue("@EndDate", endDate.Value.AddDays(1).AddTicks(-1));
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return dt;
        }
    }
}
