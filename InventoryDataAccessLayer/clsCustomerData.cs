using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsCustomerData
    {
        public static bool AddCustomer(string phoneNumber, string customerName, out int customerID, out string errorMessage)
        {
            customerID = -1;
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Ensure loyalty columns exist
                    EnsureLoyaltyColumns(connection);

                    string query = @"
                        INSERT INTO Customers (PhoneNumber, CustomerName, CreatedDate, LoyaltyPoints, TotalSpent, Tier)
                        VALUES (@PhoneNumber, @CustomerName, GETDATE(), 0, 0, 'Bronze');
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@CustomerName", customerName);

                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            customerID = Convert.ToInt32(result);
                            return true;
                        }
                        else
                        {
                            errorMessage = "Failed to create customer record.";
                            return false;
                        }
                    }
                }
                catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation
                {
                    errorMessage = "A customer with this phone number already exists.";
                    return false;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        private static void EnsureLoyaltyColumns(SqlConnection connection)
        {
            try
            {
                string sql = @"
                    IF COL_LENGTH('Customers', 'LoyaltyPoints') IS NULL
                        ALTER TABLE Customers ADD LoyaltyPoints INT DEFAULT 0;
                    
                    IF COL_LENGTH('Customers', 'TotalSpent') IS NULL
                        ALTER TABLE Customers ADD TotalSpent DECIMAL(10,2) DEFAULT 0;
                    
                    IF COL_LENGTH('Customers', 'Tier') IS NULL
                        ALTER TABLE Customers ADD Tier NVARCHAR(20) DEFAULT 'Bronze';";
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                // Ignore errors if columns already exist
            }
        }

        public static bool CustomerExistsByPhone(string phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Customers WHERE PhoneNumber = @PhoneNumber";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        connection.Open();

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.CustomerExistsByPhone", ex);
                    return false;
                }
            }
        }

        public static DataTable GetCustomerByPhone(string phoneNumber)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    string query = @"
                        SELECT CustomerID, PhoneNumber, CustomerName, CreatedDate, LastPurchaseDate, LoyaltyPoints, TotalSpent, Tier
                        FROM Customers
                        WHERE PhoneNumber = @PhoneNumber";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.GetCustomerByPhone", ex);
                }
            }

            return dt;
        }

        public static DataTable GetCustomerByID(int customerID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    string query = @"
                        SELECT CustomerID, PhoneNumber, CustomerName, CreatedDate, LastPurchaseDate, LoyaltyPoints, TotalSpent, Tier
                        FROM Customers
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.GetCustomerByID", ex);
                }
            }

            return dt;
        }

        public static DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    string query = @"
                        SELECT CustomerID, PhoneNumber, CustomerName, CreatedDate, LastPurchaseDate, LoyaltyPoints, TotalSpent, Tier
                        FROM Customers
                        ORDER BY CustomerName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.GetAllCustomers", ex);
                }
            }

            return dt;
        }

        public static bool UpdateCustomer(int customerID, string phoneNumber, string customerName, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        UPDATE Customers
                        SET PhoneNumber = @PhoneNumber,
                            CustomerName = @CustomerName
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@CustomerName", customerName);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex) when (ex.Number == 2627)
                {
                    errorMessage = "A customer with this phone number already exists.";
                    return false;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public static bool DeleteCustomer(int customerID, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // First check if customer has orders
                    string checkQuery = "SELECT COUNT(*) FROM Orders WHERE CustomerID = @CustomerID";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@CustomerID", customerID);
                        int orderCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (orderCount > 0)
                        {
                            errorMessage = "Cannot delete customer with existing orders.";
                            return false;
                        }
                    }

                    string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public static bool UpdateLastPurchaseDate(int customerID, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        UPDATE Customers
                        SET LastPurchaseDate = GETDATE()
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public static int GetLoyaltyPoints(int customerID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    using (SqlCommand command = new SqlCommand(
                        "SELECT LoyaltyPoints FROM Customers WHERE CustomerID = @CustomerID", connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        object result = command.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                            return 0;

                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.GetLoyaltyPoints", ex);
                    return 0;
                }
            }
        }

        public static bool AddLoyaltyPoints(int customerID, int points, out string errorMessage)
        {
            errorMessage = "";

            if (points <= 0)
                return true;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);
                    return AddLoyaltyPoints(connection, null, customerID, points, 0m, out errorMessage);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.AddLoyaltyPoints", ex);
                    return false;
                }
            }
        }

        public static bool AddLoyaltyPoints(SqlConnection connection, SqlTransaction transaction, int customerID, int points, decimal purchaseAmount, out string errorMessage)
        {
            errorMessage = "";

            if (points <= 0 && purchaseAmount <= 0)
                return true;

            EnsureLoyaltyColumns(connection);

            string query = @"
                UPDATE Customers
                SET LoyaltyPoints = LoyaltyPoints + @PointsEarned,
                    TotalSpent = TotalSpent + @PurchaseAmount,
                    Tier = CASE 
                        WHEN TotalSpent + @PurchaseAmount >= 5000 THEN 'Platinum'
                        WHEN TotalSpent + @PurchaseAmount >= 2000 THEN 'Gold'
                        WHEN TotalSpent + @PurchaseAmount >= 500 THEN 'Silver'
                        ELSE 'Bronze'
                    END,
                    LastPurchaseDate = GETDATE()
                WHERE CustomerID = @CustomerID";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@CustomerID", customerID);
                command.Parameters.AddWithValue("@PointsEarned", points);
                command.Parameters.AddWithValue("@PurchaseAmount", purchaseAmount);

                if (command.ExecuteNonQuery() != 1)
                {
                    errorMessage = "Unable to update loyalty points for customer #" + customerID + ".";
                    return false;
                }
            }

            return true;
        }

        public static bool UpdateCustomerLoyalty(int customerID, decimal purchaseAmount, out string errorMessage)
        {
            errorMessage = "";

            int pointsEarned = (int)Math.Floor(purchaseAmount);

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);
                    return AddLoyaltyPoints(connection, null, customerID, pointsEarned, purchaseAmount, out errorMessage);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.UpdateCustomerLoyalty", ex);
                    return false;
                }
            }
        }

        public static bool UpdateCustomerLoyalty(int customerID, int loyaltyPoints, decimal totalSpent, string tier, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    string query = @"
                        UPDATE Customers
                        SET LoyaltyPoints = @LoyaltyPoints,
                            TotalSpent = @TotalSpent,
                            Tier = @Tier
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@LoyaltyPoints", loyaltyPoints);
                        command.Parameters.AddWithValue("@TotalSpent", totalSpent);
                        command.Parameters.AddWithValue("@Tier", tier);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.UpdateCustomerLoyalty", ex);
                    return false;
                }
            }
        }

        public static bool UpdateCustomerPoints(int customerID, int loyaltyPoints, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    string query = @"
                        UPDATE Customers
                        SET LoyaltyPoints = @LoyaltyPoints
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@LoyaltyPoints", loyaltyPoints);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.UpdateCustomerPoints", ex);
                    return false;
                }
            }
        }

        public static bool UpdateCustomerPoints(int customerID, int loyaltyPoints, string source, int? orderID, out string errorMessage)
        {
            return UpdateCustomerPoints(customerID, loyaltyPoints, null, source, orderID, out errorMessage);
        }

        public static bool UpdateCustomerPoints(int customerID, int loyaltyPoints, string reason, string source, int? orderID, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    // Get current points
                    int currentPoints = GetLoyaltyPoints(customerID);
                    int changeAmount = loyaltyPoints - currentPoints;

                    // Update customer points
                    string updateQuery = @"
                        UPDATE Customers
                        SET LoyaltyPoints = @LoyaltyPoints
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@CustomerID", customerID);
                        updateCommand.Parameters.AddWithValue("@LoyaltyPoints", loyaltyPoints);
                        updateCommand.ExecuteNonQuery();
                    }

                    // Insert into history
                    if (changeAmount != 0)
                    {
                        string historyQuery = @"
                            INSERT INTO LoyaltyPointsHistory (CustomerID, ChangeAmount, Reason, Source, OrderID, CreatedByUserID)
                            VALUES (@CustomerID, @ChangeAmount, @Reason, @Source, @OrderID, @CreatedByUserID)";

                        using (SqlCommand historyCommand = new SqlCommand(historyQuery, connection))
                        {
                            historyCommand.Parameters.AddWithValue("@CustomerID", customerID);
                            historyCommand.Parameters.AddWithValue("@ChangeAmount", changeAmount);
                            historyCommand.Parameters.AddWithValue("@Reason", (object)reason ?? DBNull.Value);
                            historyCommand.Parameters.AddWithValue("@Source", source);
                            historyCommand.Parameters.AddWithValue("@OrderID", (object)orderID ?? DBNull.Value);
                            historyCommand.Parameters.AddWithValue("@CreatedByUserID", (object)clsUserManagement.CurrentUser?.UserID ?? DBNull.Value);
                            historyCommand.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.UpdateCustomerPoints (with history)", ex);
                    return false;
                }
            }
        }

        public static string GetLoyaltyTier(int customerID)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    string query = "SELECT LoyaltyTier FROM Customers WHERE CustomerID = @CustomerID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        object result = command.ExecuteScalar();
                        return result?.ToString() ?? "Bronze";
                    }
                }
                catch
                {
                    return "Bronze";
                }
            }
        }

        public static bool UpdateLoyaltyTier(int customerID, string tier)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    string query = "UPDATE Customers SET LoyaltyTier = @Tier WHERE CustomerID = @CustomerID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@Tier", tier);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool RedeemLoyaltyPoints(int customerID, int pointsToRedeem, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if customer has enough points
                    string checkQuery = "SELECT LoyaltyPoints FROM Customers WHERE CustomerID = @CustomerID";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@CustomerID", customerID);
                        object result = checkCommand.ExecuteScalar();
                        if (result == null || result == DBNull.Value || Convert.ToInt32(result) < pointsToRedeem)
                        {
                            errorMessage = "Insufficient loyalty points.";
                            return false;
                        }
                    }

                    string query = @"
                        UPDATE Customers
                        SET LoyaltyPoints = LoyaltyPoints - @PointsToRedeem
                        WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@PointsToRedeem", pointsToRedeem);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.RedeemLoyaltyPoints", ex);
                    return false;
                }
            }
        }

        public static DataTable GetCustomerOrders(int customerID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    string query = @"
                        SELECT OrderID, OrderDate, Subtotal, TaxAmount, TotalAmount, 
                               PaymentMethod, PaymentDetails
                        FROM Orders
                        WHERE CustomerID = @CustomerID
                        ORDER BY OrderDate DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.GetCustomerOrders", ex);
                }
            }

            return dt;
        }

        public static DataTable GetOrderDetails(int orderID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    string query = @"
                        SELECT O.OrderID, O.OrderDate, O.Subtotal, O.TaxAmount, O.TotalAmount,
                               O.CustomerID, C.PhoneNumber, C.CustomerName,
                               O.PaymentMethod, O.PaymentDetails
                        FROM Orders O
                        LEFT JOIN Customers C ON O.CustomerID = C.CustomerID
                        WHERE O.OrderID = @OrderID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.GetOrderDetails", ex);
                }
            }

            return dt;
        }

        public static DataTable GetOrderItems(int orderID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    string query = @"
                        SELECT OrderItemID, ProductID, ProductName, Quantity, UnitPrice, Subtotal
                        FROM OrderItems
                        WHERE OrderID = @OrderID
                        ORDER BY OrderItemID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderID);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsErrorLog.LogException("clsCustomerData.GetOrderItems", ex);
                }
            }

            return dt;
        }

        public static DataTable GetCustomerCountByTier(out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    string query = @"
                        SELECT Tier, COUNT(*) AS CustomerCount
                        FROM Customers
                        GROUP BY Tier
                        ORDER BY CASE 
                            WHEN Tier = 'Platinum' THEN 1
                            WHEN Tier = 'Gold' THEN 2
                            WHEN Tier = 'Silver' THEN 3
                            WHEN Tier = 'Bronze' THEN 4
                            ELSE 5
                        END";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.GetCustomerCountByTier", ex);
                }
            }

            return dt;
        }

        public static bool UpdateCustomerReferrer(int customerID, int referrerID, out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureReferralColumn(connection);

                    string query = "UPDATE Customers SET ReferredBy = @ReferrerID WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", customerID);
                        command.Parameters.AddWithValue("@ReferrerID", referrerID);
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.UpdateCustomerReferrer", ex);
                    return false;
                }
            }
        }

        private static void EnsureReferralColumn(SqlConnection connection)
        {
            // Check if ReferredBy column exists
            string checkQuery = @"
                SELECT COUNT(*) 
                FROM sys.columns 
                WHERE object_id = OBJECT_ID('Customers') 
                AND name = 'ReferredBy'";

            using (SqlCommand command = new SqlCommand(checkQuery, connection))
            {
                int columnExists = Convert.ToInt32(command.ExecuteScalar());

                if (columnExists == 0)
                {
                    // Add ReferredBy column
                    string alterQuery = "ALTER TABLE Customers ADD ReferredBy INT NULL";
                    using (SqlCommand alterCmd = new SqlCommand(alterQuery, connection))
                    {
                        alterCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static DataTable GetTopLoyaltyMembers(int topN, out string errorMessage)
        {
            errorMessage = "";
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    EnsureLoyaltyColumns(connection);

                    string query = @"
                        SELECT TOP (@TopN) CustomerID, CustomerName, PhoneNumber, LoyaltyPoints, TotalSpent, Tier
                        FROM Customers
                        WHERE LoyaltyPoints > 0
                        ORDER BY TotalSpent DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TopN", topN);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    clsErrorLog.LogException("clsCustomerData.GetTopLoyaltyMembers", ex);
                }
            }

            return dt;
        }
    }
}
