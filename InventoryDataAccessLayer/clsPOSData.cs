using System;
using System.Data;
using System.Data.SqlClient;

namespace InventoryDataAccessLayer
{
    public static class clsPOSData
    {
        public static bool EnsurePosSetupAndSampleData(out string errorMessage)
        {
            errorMessage = "";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        EnsureOrderTables(connection, transaction);
                        SeedSampleData(connection, transaction);
                        transaction.Commit();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public static DataTable GetProductsForPOS()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                    SELECT P.ProductID,
                           P.ProductName,
                           P.CategoryID,
                           C.CategoryName,
                           P.SupplierID,
                           S.SupplierName,
                           P.Price,
                           P.Quantity,
                           P.Barcode,
                           P.ImagePath
                    FROM Products P
                    INNER JOIN Categories C ON P.CategoryID = C.CategoryID
                    INNER JOIN Suppliers S ON P.SupplierID = S.SupplierID
                    ORDER BY C.CategoryName, P.ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return dt;
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, out int orderID, out string errorMessage)
        {
            return CompleteOrder(orderItems, taxRate, null, null, null, 0, null, out orderID, out errorMessage);
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, int? customerID, string paymentMethod, string paymentDetails, out int orderID, out string errorMessage)
        {
            return CompleteOrder(orderItems, taxRate, customerID, paymentMethod, paymentDetails, 0, null, out orderID, out errorMessage);
        }

        public static bool CompleteOrder(DataTable orderItems, decimal taxRate, int? customerID, string paymentMethod, string paymentDetails, decimal discountAmount, string couponCode, out int orderID, out string errorMessage)
        {
            orderID = -1;
            errorMessage = "";

            if (orderItems == null || orderItems.Rows.Count == 0)
            {
                errorMessage = "Receipt is empty.";
                return false;
            }

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        decimal subtotal = 0;

                        foreach (DataRow row in orderItems.Rows)
                        {
                            int productID = Convert.ToInt32(row["ProductID"]);
                            int quantity = Convert.ToInt32(row["Quantity"]);
                            decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);

                            if (quantity <= 0)
                            {
                                errorMessage = "Every receipt item must have a quantity greater than zero.";
                                transaction.Rollback();
                                return false;
                            }

                            int stock = GetCurrentStock(connection, transaction, productID);
                            if (stock < quantity)
                            {
                                errorMessage = "Insufficient stock for " + row["ProductName"] + ". Available: " + stock;
                                transaction.Rollback();
                                return false;
                            }

                            subtotal += quantity * unitPrice;
                        }

                        decimal discount = Math.Min(Math.Max(discountAmount, 0), subtotal);
                        decimal taxAmount = Math.Round((subtotal - discount) * taxRate, 2);
                        decimal totalAmount = subtotal - discount + taxAmount;

                        string orderQuery = @"
                            INSERT INTO Orders (OrderDate, Subtotal, DiscountAmount, CouponCode, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
                            VALUES (GETDATE(), @Subtotal, @DiscountAmount, @CouponCode, @TaxAmount, @TotalAmount, @CustomerID, @PaymentMethod, @PaymentDetails);
                            SELECT SCOPE_IDENTITY();";

                        using (SqlCommand orderCommand = new SqlCommand(orderQuery, connection, transaction))
                        {
                            orderCommand.Parameters.AddWithValue("@Subtotal", subtotal);
                            orderCommand.Parameters.AddWithValue("@DiscountAmount", discount);
                            orderCommand.Parameters.AddWithValue("@CouponCode", string.IsNullOrWhiteSpace(couponCode) ? (object)DBNull.Value : couponCode);
                            orderCommand.Parameters.AddWithValue("@TaxAmount", taxAmount);
                            orderCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);
                            orderCommand.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                            orderCommand.Parameters.AddWithValue("@PaymentMethod", paymentMethod ?? (object)DBNull.Value);
                            orderCommand.Parameters.AddWithValue("@PaymentDetails", paymentDetails ?? (object)DBNull.Value);

                            orderID = Convert.ToInt32(orderCommand.ExecuteScalar());
                        }

                        foreach (DataRow row in orderItems.Rows)
                        {
                            int productID = Convert.ToInt32(row["ProductID"]);
                            string productName = row["ProductName"].ToString();
                            int quantity = Convert.ToInt32(row["Quantity"]);
                            decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                            decimal itemSubtotal = quantity * unitPrice;

                            using (SqlCommand itemCommand = new SqlCommand(@"
                                INSERT INTO OrderItems
                                (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
                                VALUES
                                (@OrderID, @ProductID, @ProductName, @Quantity, @UnitPrice, @Subtotal);", connection, transaction))
                            {
                                itemCommand.Parameters.AddWithValue("@OrderID", orderID);
                                itemCommand.Parameters.AddWithValue("@ProductID", productID);
                                itemCommand.Parameters.AddWithValue("@ProductName", productName);
                                itemCommand.Parameters.AddWithValue("@Quantity", quantity);
                                itemCommand.Parameters.AddWithValue("@UnitPrice", unitPrice);
                                itemCommand.Parameters.AddWithValue("@Subtotal", itemSubtotal);
                                itemCommand.ExecuteNonQuery();
                            }

                            using (SqlCommand stockCommand = new SqlCommand(@"
                                UPDATE Products
                                SET Quantity = Quantity - @Quantity
                                WHERE ProductID = @ProductID
                                  AND Quantity >= @Quantity;", connection, transaction))
                            {
                                stockCommand.Parameters.AddWithValue("@ProductID", productID);
                                stockCommand.Parameters.AddWithValue("@Quantity", quantity);

                                if (stockCommand.ExecuteNonQuery() != 1)
                                {
                                    errorMessage = "Unable to update stock for " + productName + ".";
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                        }

                        // Update customer's last purchase date and loyalty points if customerID is provided
                        if (customerID.HasValue)
                        {
                            int pointsEarned = (int)Math.Floor(subtotal);
                            if (!clsCustomerData.AddLoyaltyPoints(connection, transaction, customerID.Value, pointsEarned, subtotal, out errorMessage))
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    orderID = -1;
                    return false;
                }
            }
        }

        public static bool VoidOrder(int orderID, string reason, string voidedBy, out string errorMessage)
        {
            errorMessage = "";
            
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        // Check if order exists and is not already voided
                        string checkQuery = @"
                            SELECT OrderID, CustomerID, TotalAmount, CouponCode
                            FROM Orders
                            WHERE OrderID = @OrderID AND IsVoided = 0";
                        
                        int? customerID = null;
                        decimal totalAmount = 0;
                        string couponCode = null;
                        
                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@OrderID", orderID);
                            using (SqlDataReader reader = checkCommand.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    reader.Close();
                                    errorMessage = "Order not found or already voided.";
                                    transaction.Rollback();
                                    return false;
                                }
                                customerID = reader["CustomerID"] != DBNull.Value ? (int?)reader["CustomerID"] : null;
                                totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                                couponCode = reader["CouponCode"] != DBNull.Value ? reader["CouponCode"].ToString() : null;
                                reader.Close();
                            }
                        }

                        // Get order items to reverse stock
                        string itemsQuery = @"
                            SELECT ProductID, Quantity
                            FROM OrderItems
                            WHERE OrderID = @OrderID";
                        
                        DataTable orderItems = new DataTable();
                        using (SqlCommand itemsCommand = new SqlCommand(itemsQuery, connection, transaction))
                        {
                            itemsCommand.Parameters.AddWithValue("@OrderID", orderID);
                            using (SqlDataReader reader = itemsCommand.ExecuteReader())
                            {
                                orderItems.Load(reader);
                            }
                        }

                        // Reverse stock for each item
                        foreach (DataRow row in orderItems.Rows)
                        {
                            int productID = Convert.ToInt32(row["ProductID"]);
                            int quantity = Convert.ToInt32(row["Quantity"]);

                            using (SqlCommand stockCommand = new SqlCommand(@"
                                UPDATE Products
                                SET Quantity = Quantity + @Quantity
                                WHERE ProductID = @ProductID;", connection, transaction))
                            {
                                stockCommand.Parameters.AddWithValue("@ProductID", productID);
                                stockCommand.Parameters.AddWithValue("@Quantity", quantity);
                                stockCommand.ExecuteNonQuery();
                            }
                        }

                        // Deduct loyalty points if they were awarded (1 point per JOD spent)
                        if (customerID.HasValue && totalAmount > 0)
                        {
                            int pointsToDeduct = (int)Math.Floor(totalAmount);
                            if (pointsToDeduct > 0)
                            {
                                using (SqlCommand loyaltyCommand = new SqlCommand(@"
                                    UPDATE Customers
                                    SET LoyaltyPoints = LoyaltyPoints - @PointsToDeduct
                                    WHERE CustomerID = @CustomerID
                                      AND LoyaltyPoints >= @PointsToDeduct;", connection, transaction))
                                {
                                    loyaltyCommand.Parameters.AddWithValue("@CustomerID", customerID.Value);
                                    loyaltyCommand.Parameters.AddWithValue("@PointsToDeduct", pointsToDeduct);
                                    loyaltyCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        // Mark order as voided
                        string voidQuery = @"
                            UPDATE Orders
                            SET IsVoided = 1,
                                VoidDate = GETDATE(),
                                VoidReason = @VoidReason,
                                VoidedBy = @VoidedBy
                            WHERE OrderID = @OrderID";

                        using (SqlCommand voidCommand = new SqlCommand(voidQuery, connection, transaction))
                        {
                            voidCommand.Parameters.AddWithValue("@OrderID", orderID);
                            voidCommand.Parameters.AddWithValue("@VoidReason", reason ?? "");
                            voidCommand.Parameters.AddWithValue("@VoidedBy", voidedBy ?? Environment.UserName);
                            voidCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
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

        public static DataTable GetTodayOrderSummary()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                    SELECT COUNT(*) AS OrderCount,
                           ISNULL(SUM(Subtotal), 0) AS Subtotal,
                           ISNULL(SUM(TaxAmount), 0) AS TaxAmount,
                           ISNULL(SUM(TotalAmount), 0) AS TotalRevenue
                    FROM Orders
                    WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return dt;
        }

        public static DataTable GetTodayOrders()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                    SELECT OrderID,
                           OrderDate,
                           Subtotal,
                           TaxAmount,
                           TotalAmount,
                           PaymentMethod
                    FROM Orders
                    WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)
                    ORDER BY OrderDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return dt;
        }

        public static DataTable GetTodayTopSellingProducts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                string query = @"
                    SELECT TOP 10 OI.ProductName,
                           SUM(OI.Quantity) AS UnitsSold,
                           SUM(OI.Subtotal) AS Revenue
                    FROM OrderItems OI
                    INNER JOIN Orders O ON O.OrderID = OI.OrderID
                    WHERE CAST(O.OrderDate AS DATE) = CAST(GETDATE() AS DATE)
                    GROUP BY OI.ProductName
                    ORDER BY UnitsSold DESC, Revenue DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return dt;
        }

        private static void EnsureOrderTables(SqlConnection connection, SqlTransaction transaction)
        {
            string query = @"
                IF OBJECT_ID('Orders', 'U') IS NULL
                BEGIN
                    CREATE TABLE Orders
                    (
                        OrderID INT IDENTITY(1,1) PRIMARY KEY,
                        OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
                        Subtotal DECIMAL(10,2) NOT NULL,
                        DiscountAmount DECIMAL(10,2) NOT NULL DEFAULT 0,
                        CouponCode NVARCHAR(50) NULL,
                        TaxAmount DECIMAL(10,2) NOT NULL,
                        TotalAmount DECIMAL(10,2) NOT NULL,
                        CustomerID INT NULL,
                        PaymentMethod NVARCHAR(50) NULL,
                        PaymentDetails NVARCHAR(200) NULL,
                        IsVoided BIT NOT NULL DEFAULT 0,
                        VoidDate DATETIME NULL,
                        VoidReason NVARCHAR(500) NULL,
                        VoidedBy NVARCHAR(100) NULL
                    );
                END;

                IF COL_LENGTH('Orders', 'DiscountAmount') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD DiscountAmount DECIMAL(10,2) NOT NULL DEFAULT 0;
                END;

                IF COL_LENGTH('Orders', 'CouponCode') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD CouponCode NVARCHAR(50) NULL;
                END;

                IF COL_LENGTH('Orders', 'CustomerID') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD CustomerID INT NULL;
                END;

                IF COL_LENGTH('Orders', 'PaymentMethod') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD PaymentMethod NVARCHAR(50) NULL;
                END;

                IF COL_LENGTH('Orders', 'PaymentDetails') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD PaymentDetails NVARCHAR(200) NULL;
                END;

                IF COL_LENGTH('Orders', 'IsVoided') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD IsVoided BIT NOT NULL DEFAULT 0;
                END;

                IF COL_LENGTH('Orders', 'VoidDate') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD VoidDate DATETIME NULL;
                END;

                IF COL_LENGTH('Orders', 'VoidReason') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD VoidReason NVARCHAR(500) NULL;
                END;

                IF COL_LENGTH('Orders', 'VoidedBy') IS NULL
                BEGIN
                    ALTER TABLE Orders ADD VoidedBy NVARCHAR(100) NULL;
                END;

                IF COL_LENGTH('Customers', 'LoyaltyPoints') IS NULL
                BEGIN
                    ALTER TABLE Customers ADD LoyaltyPoints INT NOT NULL DEFAULT 0;
                END;

                IF OBJECT_ID('OrderItems', 'U') IS NULL
                BEGIN
                    CREATE TABLE OrderItems
                    (
                        OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
                        OrderID INT NOT NULL,
                        ProductID INT NOT NULL,
                        ProductName NVARCHAR(100) NOT NULL,
                        Quantity INT NOT NULL,
                        UnitPrice DECIMAL(10,2) NOT NULL,
                        Subtotal DECIMAL(10,2) NOT NULL,
                        CONSTRAINT FK_OrderItems_Orders
                            FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
                        CONSTRAINT FK_OrderItems_Products
                            FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
                    );
                END;";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void SeedSampleData(SqlConnection connection, SqlTransaction transaction)
        {
            string[] suppliers = new[]
            {
                "Metro POS Supplies",
                "Prime Wholesale",
                "City Distribution",
                "Value Traders",
                "Global Goods"
            };

            foreach (string supplier in suppliers)
            {
                EnsureSupplier(connection, transaction, supplier);
            }

            SeedCategoryProducts(connection, transaction, "Electronics", "ELEC", suppliers, new[]
            {
                "Bluetooth Speaker", "Wireless Earbuds", "USB-C Charger", "Smart Watch", "Power Bank",
                "HDMI Cable", "Laptop Stand", "Phone Tripod", "Gaming Mouse", "Mechanical Keyboard",
                "Webcam", "Portable SSD", "WiFi Router", "Tablet Case", "Screen Protector",
                "Smart Plug", "Desk Microphone", "Monitor Light Bar", "USB Hub", "Memory Card"
            }, new decimal[]
            {
                34.99m, 49.99m, 19.99m, 89.99m, 29.99m, 8.99m, 24.99m, 14.99m, 39.99m, 69.99m,
                44.99m, 99.99m, 79.99m, 18.99m, 6.99m, 15.99m, 54.99m, 32.99m, 22.99m, 12.99m
            });

            SeedCategoryProducts(connection, transaction, "Groceries", "GROC", suppliers, new[]
            {
                "Basmati Rice", "Olive Oil", "Pasta Pack", "Tomato Sauce", "Canned Tuna",
                "Breakfast Cereal", "Ground Coffee", "Green Tea", "Chocolate Bar", "Mixed Nuts",
                "Honey Jar", "Flour Bag", "Brown Sugar", "Lentils", "Chickpeas",
                "Peanut Butter", "Jam Jar", "Corn Flakes", "Bottled Water", "Orange Juice"
            }, new decimal[]
            {
                7.99m, 12.49m, 2.25m, 1.75m, 3.50m, 4.95m, 8.90m, 3.80m, 1.20m, 6.60m,
                5.75m, 2.40m, 2.10m, 2.85m, 2.65m, 4.35m, 3.25m, 4.10m, 0.45m, 1.95m
            });

            SeedCategoryProducts(connection, transaction, "Clothing", "CLOT", suppliers, new[]
            {
                "Cotton T-Shirt", "Polo Shirt", "Denim Jeans", "Hoodie", "Light Jacket",
                "Formal Shirt", "Chino Pants", "Sports Shorts", "Running Socks", "Baseball Cap",
                "Leather Belt", "Winter Scarf", "Knit Beanie", "Gym Leggings", "Casual Dress",
                "Track Pants", "Tank Top", "Cardigan", "Raincoat", "Sneakers"
            }, new decimal[]
            {
                9.99m, 18.99m, 34.99m, 29.99m, 49.99m, 24.99m, 31.99m, 16.99m, 4.99m, 8.99m,
                12.99m, 11.50m, 7.50m, 22.99m, 39.99m, 21.99m, 6.99m, 27.99m, 44.99m, 59.99m
            });

            SeedCategoryProducts(connection, transaction, "Home Goods", "HOME", suppliers, new[]
            {
                "Ceramic Mug", "Dinner Plate Set", "Kitchen Towel", "Storage Basket", "Laundry Hamper",
                "LED Desk Lamp", "Wall Clock", "Throw Pillow", "Bed Sheet Set", "Bath Mat",
                "Glass Vase", "Cutting Board", "Food Container", "Scented Candle", "Door Mat",
                "Ironing Board Cover", "Cleaning Brush", "Soap Dispenser", "Picture Frame", "Plant Pot"
            }, new decimal[]
            {
                3.99m, 22.99m, 5.50m, 8.75m, 16.25m, 21.99m, 13.99m, 10.99m, 35.99m, 9.99m,
                14.50m, 11.99m, 6.99m, 7.25m, 12.49m, 6.75m, 3.25m, 8.99m, 5.99m, 4.99m
            });

            SeedCategoryProducts(connection, transaction, "Health & Beauty", "HLTH", suppliers, new[]
            {
                "Shampoo", "Conditioner", "Body Wash", "Face Cleanser", "Moisturizer",
                "Sunscreen", "Toothpaste", "Toothbrush", "Mouthwash", "Hand Cream",
                "Lip Balm", "Deodorant", "Hair Gel", "Cotton Pads", "Makeup Remover",
                "Vitamin C", "Bandage Pack", "Hand Sanitizer", "Nail Clippers", "Body Lotion"
            }, new decimal[]
            {
                4.99m, 5.49m, 3.99m, 6.75m, 8.95m, 9.99m, 2.25m, 1.99m, 4.50m, 3.25m,
                1.75m, 3.80m, 4.20m, 2.10m, 5.95m, 7.99m, 2.50m, 1.60m, 2.99m, 6.50m
            });
        }

        private static void SeedCategoryProducts(SqlConnection connection, SqlTransaction transaction, string categoryName, string barcodePrefix, string[] suppliers, string[] productNames, decimal[] prices)
        {
            int categoryID = EnsureCategory(connection, transaction, categoryName);
            int existingCount = CountProductsInCategory(connection, transaction, categoryID);

            for (int index = existingCount; index < 20; index++)
            {
                string barcode = "POS-" + barcodePrefix + "-" + (index + 1).ToString("00");

                if (ProductBarcodeExists(connection, transaction, barcode))
                    continue;

                string supplierName = suppliers[index % suppliers.Length];
                int supplierID = GetSupplierID(connection, transaction, supplierName);
                string productName = productNames[index];
                decimal price = prices[index];
                int quantity = 25 + (index * 3);
                string imageUrl = "https://placehold.co/96x96/png?text=" + Uri.EscapeDataString(barcodePrefix + " " + (index + 1).ToString("00"));

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Products
                    (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
                    VALUES
                    (@ProductName, @CategoryID, @SupplierID, @Price, @Quantity, @Barcode, @ImagePath, GETDATE());", connection, transaction))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@CategoryID", categoryID);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Barcode", barcode);
                    command.Parameters.AddWithValue("@ImagePath", imageUrl);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static int EnsureCategory(SqlConnection connection, SqlTransaction transaction, string categoryName)
        {
            using (SqlCommand command = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = @CategoryName)
                BEGIN
                    INSERT INTO Categories (CategoryName) VALUES (@CategoryName);
                END

                SELECT CategoryID FROM Categories WHERE CategoryName = @CategoryName;", connection, transaction))
            {
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static int GetSupplierID(SqlConnection connection, SqlTransaction transaction, string supplierName)
        {
            using (SqlCommand command = new SqlCommand(@"
                SELECT TOP 1 SupplierID
                FROM Suppliers
                WHERE SupplierName = @SupplierName
                ORDER BY SupplierID;", connection, transaction))
            {
                command.Parameters.AddWithValue("@SupplierName", supplierName);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static int EnsureSupplier(SqlConnection connection, SqlTransaction transaction, string supplierName)
        {
            object existingID;

            using (SqlCommand findCommand = new SqlCommand(@"
                SELECT TOP 1 SupplierID
                FROM Suppliers
                WHERE SupplierName = @SupplierName
                ORDER BY SupplierID;", connection, transaction))
            {
                findCommand.Parameters.AddWithValue("@SupplierName", supplierName);
                existingID = findCommand.ExecuteScalar();
            }

            if (existingID != null)
                return Convert.ToInt32(existingID);

            bool supplierIDIsIdentity = IsIdentityColumn(connection, transaction, "Suppliers", "SupplierID");
            string email = supplierName.Replace(" ", "").ToLower() + "@example.com";

            if (supplierIDIsIdentity)
            {
                using (SqlCommand insertCommand = new SqlCommand(@"
                    INSERT INTO Suppliers (SupplierName, Phone, Email)
                    VALUES (@SupplierName, @Phone, @Email);
                    SELECT SCOPE_IDENTITY();", connection, transaction))
                {
                    insertCommand.Parameters.AddWithValue("@SupplierName", supplierName);
                    insertCommand.Parameters.AddWithValue("@Phone", "0792000000");
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    return Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }

            int supplierID = GetNextIntID(connection, transaction, "Suppliers", "SupplierID");

            using (SqlCommand insertCommand = new SqlCommand(@"
                INSERT INTO Suppliers (SupplierID, SupplierName, Phone, Email)
                VALUES (@SupplierID, @SupplierName, @Phone, @Email);", connection, transaction))
            {
                insertCommand.Parameters.AddWithValue("@SupplierID", supplierID);
                insertCommand.Parameters.AddWithValue("@SupplierName", supplierName);
                insertCommand.Parameters.AddWithValue("@Phone", "0792000000");
                insertCommand.Parameters.AddWithValue("@Email", email);
                insertCommand.ExecuteNonQuery();
            }

            return supplierID;
        }

        private static bool IsIdentityColumn(SqlConnection connection, SqlTransaction transaction, string tableName, string columnName)
        {
            using (SqlCommand command = new SqlCommand(@"
                SELECT COLUMNPROPERTY(OBJECT_ID(@TableName), @ColumnName, 'IsIdentity');", connection, transaction))
            {
                command.Parameters.AddWithValue("@TableName", tableName);
                command.Parameters.AddWithValue("@ColumnName", columnName);
                object result = command.ExecuteScalar();
                return result != null && result != DBNull.Value && Convert.ToInt32(result) == 1;
            }
        }

        private static int GetNextIntID(SqlConnection connection, SqlTransaction transaction, string tableName, string columnName)
        {
            string safeTableName = WrapSqlIdentifier(tableName);
            string safeColumnName = WrapSqlIdentifier(columnName);

            using (SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(" + safeColumnName + "), 0) + 1 FROM " + safeTableName + ";", connection, transaction))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static string WrapSqlIdentifier(string name)
        {
            return "[" + name.Replace("]", "]]") + "]";
        }

        private static int CountProductsInCategory(SqlConnection connection, SqlTransaction transaction, int categoryID)
        {
            using (SqlCommand command = new SqlCommand(@"
                SELECT COUNT(*)
                FROM Products
                WHERE CategoryID = @CategoryID;", connection, transaction))
            {
                command.Parameters.AddWithValue("@CategoryID", categoryID);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private static bool ProductBarcodeExists(SqlConnection connection, SqlTransaction transaction, string barcode)
        {
            using (SqlCommand command = new SqlCommand(@"
                SELECT TOP 1 1
                FROM Products
                WHERE Barcode = @Barcode;", connection, transaction))
            {
                command.Parameters.AddWithValue("@Barcode", barcode);
                return command.ExecuteScalar() != null;
            }
        }

        private static int GetCurrentStock(SqlConnection connection, SqlTransaction transaction, int productID)
        {
            using (SqlCommand command = new SqlCommand(@"
                SELECT Quantity
                FROM Products WITH (UPDLOCK, ROWLOCK)
                WHERE ProductID = @ProductID;", connection, transaction))
            {
                command.Parameters.AddWithValue("@ProductID", productID);
                object result = command.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }

        public class ExchangeItemInfo
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int ReturnedQuantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Reason { get; set; }
        }

        public class ReplacementItemInfo
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }

        public static bool ProcessExchange(int orderID, System.Collections.Generic.List<ExchangeItemInfo> returnedItems, System.Collections.Generic.List<ReplacementItemInfo> replacementItems, out string errorMessage)
        {
            errorMessage = "";

            if ((returnedItems == null || returnedItems.Count == 0) && (replacementItems == null || replacementItems.Count == 0))
            {
                errorMessage = "No items selected for exchange.";
                return false;
            }

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        // 1. Restock returned items
                        if (returnedItems != null)
                        {
                            foreach (var item in returnedItems)
                            {
                                string queryRestock = @"UPDATE Products SET Quantity = Quantity + @Qty WHERE ProductID = @ProductID;";
                                using (SqlCommand cmd = new SqlCommand(queryRestock, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Qty", item.ReturnedQuantity);
                                    cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // 2. Deduct replacement items stock
                        if (replacementItems != null)
                        {
                            foreach (var rep in replacementItems)
                            {
                                int stock = GetCurrentStock(connection, transaction, rep.ProductID);
                                if (stock < rep.Quantity)
                                {
                                    errorMessage = $"Insufficient stock for replacement product {rep.ProductName}. Available: {stock}";
                                    transaction.Rollback();
                                    return false;
                                }

                                string queryDeduct = @"UPDATE Products SET Quantity = Quantity - @Qty WHERE ProductID = @ProductID;";
                                using (SqlCommand cmd = new SqlCommand(queryDeduct, connection, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Qty", rep.Quantity);
                                    cmd.Parameters.AddWithValue("@ProductID", rep.ProductID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // 3. Log exchange note on Order
                        string exchangeNote = $"[Exchanged on {DateTime.Now:yyyy-MM-dd HH:mm}]";
                        string queryUpdateOrder = @"UPDATE Orders SET PaymentDetails = ISNULL(PaymentDetails + '; ', '') + @Note WHERE OrderID = @OrderID;";
                        using (SqlCommand cmdOrder = new SqlCommand(queryUpdateOrder, connection, transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@Note", exchangeNote);
                            cmdOrder.Parameters.AddWithValue("@OrderID", orderID);
                            cmdOrder.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = "Database error during exchange: " + ex.Message;
                    return false;
                }
            }
        }
    }
}
