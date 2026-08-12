-- ============================================
-- Sample Data: Sales/Orders for ML Services
-- ============================================
-- This script adds sample orders with varied patterns to support:
-- - Sales Forecasting (time series data by date)
-- - Customer Segmentation (RFM analysis)
-- - Product Associations (basket analysis)

USE InventoryDB;
GO

-- Check if we already have sample data
IF EXISTS (SELECT 1 FROM Orders WHERE OrderID <= 500)
BEGIN
    PRINT 'Sample sales data already exists. Skipping insertion.';
    RETURN;
END
GO

PRINT 'Inserting sample sales/orders data...';
GO

-- Variables for randomization
DECLARE @StartDate DATETIME = DATEADD(DAY, -90, GETDATE());
DECLARE @EndDate DATETIME = GETDATE();
DECLARE @CurrentDate DATETIME;
DECLARE @OrderCount INT = 0;

-- ============================================
-- Helper: Insert Order with Items
-- ============================================
-- This will be done through direct INSERT statements for sample data
-- ============================================

PRINT 'Generating orders for the last 90 days...';
GO

-- ============================================
-- Phase 1: High-value frequent customers (Champions)
-- These customers buy frequently with high basket values
-- ============================================

-- Customer 1: John Smith (Champion) - Multiple high-value purchases
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -1, GETDATE()), 1599.00, 159.90, 1758.90, 1, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 5, 'MacBook Pro 14-inch M3 8GB/512GB', 1, 1599.00, 1599.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -5, GETDATE()), 519.00, 51.90, 570.90, 1, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 57, 'iPhone 15 256GB Pink', 1, 519.00, 519.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -10, GETDATE()), 279.00, 27.90, 306.90, 1, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 62, 'iPhone SE 64GB Midnight', 1, 279.00, 279.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -15, GETDATE()), 899.00, 89.90, 988.90, 1, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 35, 'MacBook Air M3 13-inch 8GB/256GB', 1, 899.00, 899.00);

-- Customer 2: Sarah Johnson (Champion)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -2, GETDATE()), 1199.00, 119.90, 1318.90, 2, 'MasterCard', '**** 7832');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 37, 'MacBook Air M3 15-inch 8GB/512GB', 1, 1199.00, 1199.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -8, GETDATE()), 449.00, 44.90, 493.90, 2, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 55, 'iPhone 15 128GB Black', 1, 449.00, 449.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -18, GETDATE()), 249.00, 24.90, 273.90, 2, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 161, 'AirPods Pro 2nd Gen USB-C', 1, 249.00, 249.00);

-- Customer 3: Michael Brown (Champion)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -3, GETDATE()), 1999.00, 199.90, 2198.90, 3, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 41, 'MacBook Pro 14-inch M3 Pro 18GB/512GB', 1, 1999.00, 1999.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -12, GETDATE()), 649.00, 64.90, 713.90, 3, 'MasterCard', '**** 7832');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 53, 'iPhone 15 Pro Max 256GB Titanium', 1, 649.00, 649.00);

-- ============================================
-- Phase 2: Loyal customers (regular but lower spend)
-- ============================================

-- Customer 6: Lisa Anderson (Loyal)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -7, GETDATE()), 99.00, 9.90, 108.90, 6, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 90, 'Magic Mouse White', 1, 99.00, 99.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -21, GETDATE()), 129.00, 12.90, 141.90, 6, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 92, 'Magic Trackpad White', 1, 129.00, 129.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -35, GETDATE()), 39.00, 3.90, 42.90, 6, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 96, 'MagSafe Charger', 1, 39.00, 39.00);

-- Customer 7: Robert Taylor (Loyal)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -10, GETDATE()), 109.00, 10.90, 119.90, 7, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 103, 'Logitech MX Keys S', 1, 109.00, 109.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -25, GETDATE()), 79.00, 7.90, 86.90, 7, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 117, 'Logitech MX Anywhere 3', 1, 79.00, 79.00);

-- ============================================
-- Phase 3: Potential loyalists (recent customers)
-- ============================================

-- Customer 11: William Lee (Potential)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -1, GETDATE()), 599.00, 59.90, 658.90, 11, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 73, 'iPad Pro 11-inch M4 256GB WiFi', 1, 599.00, 599.00);

-- Customer 12: Sofia Clark (Potential)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -2, GETDATE()), 449.00, 44.90, 493.90, 12, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 56, 'iPhone 15 128GB Blue', 1, 449.00, 449.00);

-- Customer 13: Daniel Wright (Potential)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -5, GETDATE()), 649.00, 64.90, 713.90, 13, 'MasterCard', '**** 7832');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 54, 'iPhone 15 Pro 256GB Titanium', 1, 649.00, 649.00);

-- ============================================
-- Phase 4: New customers (recent sign-ups)
-- ============================================

-- Customer 16: Emma Young (New)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -15, GETDATE()), 279.00, 27.90, 306.90, 16, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 62, 'iPhone SE 64GB Midnight', 1, 279.00, 279.00);

-- Customer 17: Alexander King (New)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -10, GETDATE()), 349.00, 34.90, 383.90, 17, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 77, 'iPad 10th Gen 64GB WiFi Blue', 1, 349.00, 349.00);

-- Customer 18: Sophia Hill (New)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -20, GETDATE()), 179.00, 17.90, 196.90, 18, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 163, 'AirPods 3rd Gen', 1, 179.00, 179.00);

-- ============================================
-- Phase 5: At-risk customers (previously active)
-- ============================================

-- Customer 21: Ethan Adams (At-risk)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -90, GETDATE()), 1599.00, 159.90, 1758.90, 21, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 5, 'MacBook Pro 14-inch M3 8GB/512GB', 1, 1599.00, 1599.00);

INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -75, GETDATE()), 249.00, 24.90, 273.90, 21, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 161, 'AirPods Pro 2nd Gen USB-C', 1, 249.00, 249.00);

-- Customer 22: Chloe Nelson (At-risk)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -85, GETDATE()), 1199.00, 119.90, 1318.90, 22, 'MasterCard', '**** 7832');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 37, 'MacBook Air M3 15-inch 8GB/512GB', 1, 1199.00, 1199.00);

-- ============================================
-- Phase 6: Hibernating customers (very infrequent)
-- ============================================

-- Customer 26: Henry Roberts (Hibernating)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -200, GETDATE()), 399.00, 39.90, 438.90, 26, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 58, 'iPhone 14 128GB Midnight', 1, 399.00, 399.00);

-- Customer 27: Grace Turner (Hibernating)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -250, GETDATE()), 349.00, 34.90, 383.90, 27, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 77, 'iPad 10th Gen 64GB WiFi Blue', 1, 349.00, 349.00);

-- ============================================
-- Phase 7: Lost customers (very old, no recent activity)
-- ============================================

-- Customer 31: Andrew Evans (Lost)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -600, GETDATE()), 899.00, 89.90, 988.90, 31, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 35, 'MacBook Air M3 13-inch 8GB/256GB', 1, 899.00, 899.00);

-- ============================================
-- Phase 8: Product Association Patterns
-- Create baskets with complementary products
-- ============================================

-- MacBook + Accessories bundle (Customer 4)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -1, GETDATE()), 1827.00, 182.70, 2009.70, 4, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 35, 'MacBook Air M3 13-inch 8GB/256GB', 1, 899.00, 899.00),
((SELECT MAX(OrderID) FROM Orders), 87, 'Apple Pencil Pro', 1, 129.00, 129.00),
((SELECT MAX(OrderID) FROM Orders), 88, 'Magic Keyboard iPad Pro 11-inch', 1, 299.00, 299.00),
((SELECT MAX(OrderID) FROM Orders), 90, 'Magic Mouse White', 1, 99.00, 99.00),
((SELECT MAX(OrderID) FROM Orders), 96, 'MagSafe Charger', 1, 39.00, 39.00),
((SELECT MAX(OrderID) FROM Orders), 207, 'MacBook Air 13-inch Sleeve', 1, 39.00, 39.00),
((SELECT MAX(OrderID) FROM Orders), 213, 'iPhone 15 Pro Max Case', 1, 39.00, 39.00),
((SELECT MAX(OrderID) FROM Orders), 215, 'AirPods Pro Case Silicone', 1, 29.00, 29.00);

-- iPhone + AirPods bundle (Customer 5)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -4, GETDATE()), 898.00, 89.80, 987.80, 5, 'MasterCard', '**** 7832');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 53, 'iPhone 15 Pro Max 256GB Titanium', 1, 649.00, 649.00),
((SELECT MAX(OrderID) FROM Orders), 161, 'AirPods Pro 2nd Gen USB-C', 1, 249.00, 249.00);

-- iPad + Accessories bundle (Customer 8)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -5, GETDATE()), 1177.00, 117.70, 1294.70, 8, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 73, 'iPad Pro 11-inch M4 256GB WiFi', 1, 599.00, 599.00),
((SELECT MAX(OrderID) FROM Orders), 87, 'Apple Pencil Pro', 1, 129.00, 129.00),
((SELECT MAX(OrderID) FROM Orders), 88, 'Magic Keyboard iPad Pro 11-inch', 1, 299.00, 299.00),
((SELECT MAX(OrderID) FROM Orders), 211, 'iPad Air 11-inch Case', 1, 49.00, 49.00),
((SELECT MAX(OrderID) FROM Orders), 215, 'AirPods Pro Case Silicone', 1, 29.00, 29.00);

-- Gaming bundle (Customer 9)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -14, GETDATE()), 578.00, 57.80, 635.80, 9, 'Cash', NULL);

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 178, 'Xbox Series X 1TB', 1, 449.00, 449.00),
((SELECT MAX(OrderID) FROM Orders), 181, 'Xbox Wireless Controller White', 1, 59.00, 59.00),
((SELECT MAX(OrderID) FROM Orders), 185, 'Xbox Stereo Headset', 1, 59.00, 59.00),
((SELECT MAX(OrderID) FROM Orders), 186, 'Xbox Game Pass Ultimate 1 Month', 1, 14.99, 14.99);

-- Audio bundle (Customer 10)
INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
VALUES (DATEADD(DAY, -8, GETDATE()), 428.00, 42.80, 470.80, 10, 'Visa', '**** 4521');

INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
VALUES 
((SELECT MAX(OrderID) FROM Orders), 161, 'AirPods Pro 2nd Gen USB-C', 1, 249.00, 249.00),
((SELECT MAX(OrderID) FROM Orders), 163, 'AirPods 3rd Gen', 1, 179.00, 179.00);

-- ============================================
-- Phase 9: Time series data for forecasting
-- Add daily sales spread across the last 90 days
-- ============================================

DECLARE @DayCounter INT = 0;
WHILE @DayCounter < 90
BEGIN
    -- Random daily orders (2-5 orders per day)
    INSERT INTO Orders (OrderDate, Subtotal, TaxAmount, TotalAmount, CustomerID, PaymentMethod, PaymentDetails)
    VALUES 
    (DATEADD(DAY, -@DayCounter, GETDATE()), 
     CASE WHEN @DayCounter % 7 = 0 THEN 899.00 WHEN @DayCounter % 7 = 1 THEN 449.00 ELSE 179.00 END,
     CASE WHEN @DayCounter % 7 = 0 THEN 89.90 WHEN @DayCounter % 7 = 1 THEN 44.90 ELSE 17.90 END,
     CASE WHEN @DayCounter % 7 = 0 THEN 988.90 WHEN @DayCounter % 7 = 1 THEN 493.90 ELSE 196.90 END,
     CASE WHEN @DayCounter % 7 = 0 THEN 1 WHEN @DayCounter % 7 = 1 THEN 2 ELSE 3 END,
     CASE WHEN @DayCounter % 3 = 0 THEN 'Cash' ELSE 'Visa' END,
     CASE WHEN @DayCounter % 3 = 0 THEN NULL ELSE '**** 4521' END);
    
    -- Add order items for the last order
    INSERT INTO OrderItems (OrderID, ProductID, ProductName, Quantity, UnitPrice, Subtotal)
    VALUES 
    ((SELECT MAX(OrderID) FROM Orders), 
     CASE WHEN @DayCounter % 7 = 0 THEN 35 WHEN @DayCounter % 7 = 1 THEN 55 ELSE 163 END,
     CASE WHEN @DayCounter % 7 = 0 THEN 'MacBook Air M3 13-inch 8GB/256GB' WHEN @DayCounter % 7 = 1 THEN 'iPhone 15 128GB Black' ELSE 'AirPods 3rd Gen' END,
     1,
     CASE WHEN @DayCounter % 7 = 0 THEN 899.00 WHEN @DayCounter % 7 = 1 THEN 449.00 ELSE 179.00 END,
     CASE WHEN @DayCounter % 7 = 0 THEN 899.00 WHEN @DayCounter % 7 = 1 THEN 449.00 ELSE 179.00 END);
    
    SET @DayCounter = @DayCounter + 1;
END
GO

-- ============================================
-- Update Customer LastPurchaseDate
-- ============================================
UPDATE c
SET LastPurchaseDate = (
    SELECT MAX(o.OrderDate)
    FROM Orders o
    WHERE o.CustomerID = c.CustomerID
)
FROM Customers c
WHERE EXISTS (
    SELECT 1 FROM Orders o WHERE o.CustomerID = c.CustomerID
);
GO

PRINT 'Sample sales/orders data inserted successfully.';
GO

PRINT '===========================================';
PRINT 'Sample Sales Data Insertion Complete!';
PRINT 'Total Orders: ' + CAST((SELECT COUNT(*) FROM Orders) AS NVARCHAR(10));
PRINT 'Total Order Items: ' + CAST((SELECT COUNT(*) FROM OrderItems) AS NVARCHAR(10));
PRINT '===========================================';
GO

-- ============================================
-- Verification Queries
-- ============================================
PRINT '=== Sales by Date (Last 7 Days) ===';
SELECT CAST(OrderDate AS DATE) AS SaleDate, 
       COUNT(*) AS OrderCount, 
       SUM(TotalAmount) AS TotalSales
FROM Orders
WHERE OrderDate >= DATEADD(DAY, -7, GETDATE())
GROUP BY CAST(OrderDate AS DATE)
ORDER BY SaleDate DESC;
GO

PRINT '=== Customer Purchase Summary ===';
SELECT c.CustomerName, 
       COUNT(o.OrderID) AS TotalOrders,
       SUM(o.TotalAmount) AS TotalSpent,
       MAX(o.OrderDate) AS LastPurchaseDate
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerName, c.CustomerID
ORDER BY TotalSpent DESC;
GO

PRINT '=== Top Selling Products ===';
SELECT TOP 10 
    p.ProductName,
    COUNT(oi.OrderItemID) AS TimesSold,
    SUM(oi.Quantity) AS TotalQuantity,
    SUM(oi.Subtotal) AS TotalRevenue
FROM Products p
INNER JOIN OrderItems oi ON p.ProductID = oi.ProductID
GROUP BY p.ProductName, p.ProductID
ORDER BY TotalRevenue DESC;
GO
