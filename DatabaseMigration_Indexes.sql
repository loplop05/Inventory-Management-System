-- Database Migration: Add Performance Indexes
-- Adds indexes to frequently queried columns for performance optimization

USE InventoryDB;
GO

-- Index on Products Barcode for fast barcode scanning
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Products_Barcode' AND object_id = OBJECT_ID('Products'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Products_Barcode ON Products(Barcode);
END
GO

-- Index on Products ProductName for search
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Products_ProductName' AND object_id = OBJECT_ID('Products'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Products_ProductName ON Products(ProductName);
END
GO

-- Index on Products CategoryID for filtering
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Products_CategoryID' AND object_id = OBJECT_ID('Products'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Products_CategoryID ON Products(CategoryID);
END
GO

-- Index on Orders OrderDate for date range queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_OrderDate' AND object_id = OBJECT_ID('Orders'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Orders_OrderDate ON Orders(OrderDate);
END
GO

-- Index on Orders CustomerID for customer order history
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Orders_CustomerID' AND object_id = OBJECT_ID('Orders'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Orders_CustomerID ON Orders(CustomerID);
END
GO

-- Index on OrderItems OrderID for order detail queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OrderItems_OrderID' AND object_id = OBJECT_ID('OrderItems'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_OrderItems_OrderID ON OrderItems(OrderID);
END
GO

-- Index on OrderItems ProductID for product sales history
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_OrderItems_ProductID' AND object_id = OBJECT_ID('OrderItems'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_OrderItems_ProductID ON OrderItems(ProductID);
END
GO

-- Index on Customers PhoneNumber for quick customer lookup
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Customers_PhoneNumber' AND object_id = OBJECT_ID('Customers'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Customers_PhoneNumber ON Customers(PhoneNumber);
END
GO

-- Index on Shifts UserID for user shift history
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Shifts_UserID' AND object_id = OBJECT_ID('Shifts'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Shifts_UserID ON Shifts(UserID);
END
GO

-- Index on Shifts Status for filtering open/closed shifts
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Shifts_Status' AND object_id = OBJECT_ID('Shifts'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Shifts_Status ON Shifts(Status);
END
GO

-- Index on Refunds OrderID for refund history lookup
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Refunds_OrderID' AND object_id = OBJECT_ID('Refunds'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Refunds_OrderID ON Refunds(OrderID);
END
GO

-- Index on Refunds RefundDate for date range queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Refunds_RefundDate' AND object_id = OBJECT_ID('Refunds'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Refunds_RefundDate ON Refunds(RefundDate);
END
GO

PRINT 'Database indexes migration completed successfully.';
