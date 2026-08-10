using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsDatabaseMigration
    {
        public static bool EnsureShiftsTablesExist(out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if Shifts table exists
                    bool shiftsExists = TableExists(connection, "Shifts");
                    bool shiftIDExists = ColumnExists(connection, "Orders", "ShiftID");

                    if (shiftsExists && shiftIDExists)
                        return true;

                    // Create Shifts table
                    if (!shiftsExists)
                    {
                        string createShifts = @"
                            CREATE TABLE Shifts
                            (
                                ShiftID INT IDENTITY(1,1) PRIMARY KEY,
                                UserID INT NOT NULL,
                                OpenedAt DATETIME NOT NULL DEFAULT GETDATE(),
                                ClosedAt DATETIME NULL,
                                StartingCash DECIMAL(10,2) NOT NULL,
                                ExpectedCash DECIMAL(10,2) NULL,
                                CountedCash DECIMAL(10,2) NULL,
                                CashDifference DECIMAL(10,2) NULL,
                                Status NVARCHAR(20) NOT NULL DEFAULT 'Open',
                                Notes NVARCHAR(300) NULL
                            )";

                        using (SqlCommand command = new SqlCommand(createShifts, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Add foreign key to Users if table exists
                        if (TableExists(connection, "Users"))
                        {
                            try
                            {
                                string addFK = @"
                                    ALTER TABLE Shifts
                                    ADD CONSTRAINT FK_Shifts_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)";
                                using (SqlCommand command = new SqlCommand(addFK, connection))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch { /* Ignore if FK fails */ }
                        }

                        // Create index
                        if (!IndexExists(connection, "IX_Shifts_UserID_Status", "Shifts"))
                        {
                            string createIndex = "CREATE INDEX IX_Shifts_UserID_Status ON Shifts(UserID, Status)";
                            using (SqlCommand command = new SqlCommand(createIndex, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    // Add ShiftID column to Orders table
                    if (!shiftIDExists)
                    {
                        string addColumn = "ALTER TABLE Orders ADD ShiftID INT NULL";
                        using (SqlCommand command = new SqlCommand(addColumn, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Add foreign key if Shifts table exists
                        if (TableExists(connection, "Shifts"))
                        {
                            try
                            {
                                string addFK = @"
                                    ALTER TABLE Orders
                                    ADD CONSTRAINT FK_Orders_Shifts FOREIGN KEY (ShiftID) REFERENCES Shifts(ShiftID)";
                                using (SqlCommand command = new SqlCommand(addFK, connection))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                            catch { /* Ignore if FK fails */ }
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = "Shifts database migration error: " + ex.Message;
                    return false;
                }
            }
        }

        public static bool EnsureHeldOrdersTablesExist(out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if HeldOrders table exists
                    bool heldOrdersExists = TableExists(connection, "HeldOrders");
                    bool heldOrderItemsExists = TableExists(connection, "HeldOrderItems");

                    if (heldOrdersExists && heldOrderItemsExists)
                        return true; // Tables already exist

                    // Create HeldOrders table
                    if (!heldOrdersExists)
                    {
                        string createHeldOrders = @"
                            CREATE TABLE HeldOrders
                            (
                                HeldOrderID INT IDENTITY(1,1) PRIMARY KEY,
                                UserID INT NULL,
                                CustomerID INT NULL,
                                CustomerName NVARCHAR(100) NULL,
                                CustomerPhone NVARCHAR(20) NULL,
                                PaymentMethod NVARCHAR(50) NULL,
                                PaymentDetails NVARCHAR(100) NULL,
                                CouponCode NVARCHAR(50) NULL,
                                ManualDiscountType NVARCHAR(20) NULL,
                                ManualDiscountValue DECIMAL(10,2) NULL,
                                CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
                                Notes NVARCHAR(500) NULL
                            )";

                        using (SqlCommand command = new SqlCommand(createHeldOrders, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Try to add foreign key constraints if referenced tables exist
                        try
                        {
                            if (TableExists(connection, "Users"))
                            {
                                string addFKUsers = @"
                                    ALTER TABLE HeldOrders
                                    ADD CONSTRAINT FK_HeldOrders_Users FOREIGN KEY(UserID) REFERENCES Users(UserID)";
                                using (SqlCommand command = new SqlCommand(addFKUsers, connection))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        catch { /* Ignore if FK fails */ }

                        try
                        {
                            if (TableExists(connection, "Customers"))
                            {
                                string addFKCustomers = @"
                                    ALTER TABLE HeldOrders
                                    ADD CONSTRAINT FK_HeldOrders_Customers FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)";
                                using (SqlCommand command = new SqlCommand(addFKCustomers, connection))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        catch { /* Ignore if FK fails */ }
                    }

                    // Create HeldOrderItems table
                    if (!heldOrderItemsExists)
                    {
                        string createHeldOrderItems = @"
                            CREATE TABLE HeldOrderItems
                            (
                                HeldOrderItemID INT IDENTITY(1,1) PRIMARY KEY,
                                HeldOrderID INT NOT NULL,
                                ProductID INT NOT NULL,
                                ProductName NVARCHAR(200) NOT NULL,
                                Quantity INT NOT NULL,
                                UnitPrice DECIMAL(10,2) NOT NULL,
                                Subtotal DECIMAL(10,2) NOT NULL
                            )";

                        using (SqlCommand command = new SqlCommand(createHeldOrderItems, connection))
                        {
                            command.ExecuteNonQuery();
                        }

                        // Add foreign key constraint to HeldOrders
                        try
                        {
                            string addFK = @"
                                ALTER TABLE HeldOrderItems
                                ADD CONSTRAINT FK_HeldOrderItems_HeldOrders FOREIGN KEY(HeldOrderID) REFERENCES HeldOrders(HeldOrderID) ON DELETE CASCADE";
                            using (SqlCommand command = new SqlCommand(addFK, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        catch { /* Ignore if FK fails */ }
                    }

                    // Create index if it doesn't exist
                    if (!IndexExists(connection, "IX_HeldOrders_CreatedDate", "HeldOrders"))
                    {
                        string createIndex = "CREATE INDEX IX_HeldOrders_CreatedDate ON HeldOrders(CreatedDate DESC)";
                        using (SqlCommand command = new SqlCommand(createIndex, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = "Database migration error: " + ex.Message;
                    return false;
                }
            }
        }

        private static bool TableExists(SqlConnection connection, string tableName)
        {
            string query = @"
                SELECT COUNT(*)
                FROM sys.tables
                WHERE name = @TableName";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TableName", tableName);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private static bool ColumnExists(SqlConnection connection, string tableName, string columnName)
        {
            string query = @"
                SELECT COUNT(*)
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TableName", tableName);
                command.Parameters.AddWithValue("@ColumnName", columnName);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private static bool IndexExists(SqlConnection connection, string indexName, string tableName)
        {
            string query = @"
                SELECT COUNT(*)
                FROM sys.indexes
                WHERE name = @IndexName AND object_id = OBJECT_ID(@TableName)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IndexName", indexName);
                command.Parameters.AddWithValue("@TableName", tableName);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
