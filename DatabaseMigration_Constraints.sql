-- Database Migration: Add CHECK and UNIQUE Constraints
-- Adds critical data integrity constraints to prevent invalid data

USE InventoryDB;
GO

-- Add UNIQUE constraint on ProductName (if not exists)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UQ_Products_ProductName' AND object_id = OBJECT_ID('Products'))
BEGIN
    ALTER TABLE Products
    ADD CONSTRAINT UQ_Products_ProductName UNIQUE (ProductName);
END
GO

-- Add CHECK constraint for Price >= 0
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Products_Price_NonNegative' AND parent_object_id = OBJECT_ID('Products'))
BEGIN
    ALTER TABLE Products
    ADD CONSTRAINT CK_Products_Price_NonNegative CHECK (Price >= 0);
END
GO

-- Add CHECK constraint for Quantity >= 0
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Products_Quantity_NonNegative' AND parent_object_id = OBJECT_ID('Products'))
BEGIN
    ALTER TABLE Products
    ADD CONSTRAINT CK_Products_Quantity_NonNegative CHECK (Quantity >= 0);
END
GO

-- Add CHECK constraint for Barcode not empty
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Products_Barcode_NotEmpty' AND parent_object_id = OBJECT_ID('Products'))
BEGIN
    ALTER TABLE Products
    ADD CONSTRAINT CK_Products_Barcode_NotEmpty CHECK (Barcode IS NOT NULL AND Barcode <> '');
END
GO

-- Add CHECK constraint for ProductName not empty
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Products_ProductName_NotEmpty' AND parent_object_id = OBJECT_ID('Products'))
BEGIN
    ALTER TABLE Products
    ADD CONSTRAINT CK_Products_ProductName_NotEmpty CHECK (ProductName IS NOT NULL AND ProductName <> '');
END
GO

-- Add CHECK constraint for CategoryName not empty
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Categories_CategoryName_NotEmpty' AND parent_object_id = OBJECT_ID('Categories'))
BEGIN
    ALTER TABLE Categories
    ADD CONSTRAINT CK_Categories_CategoryName_NotEmpty CHECK (CategoryName IS NOT NULL AND CategoryName <> '');
END
GO

-- Add CHECK constraint for SupplierName not empty
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Suppliers_SupplierName_NotEmpty' AND parent_object_id = OBJECT_ID('Suppliers'))
BEGIN
    ALTER TABLE Suppliers
    ADD CONSTRAINT CK_Suppliers_SupplierName_NotEmpty CHECK (SupplierName IS NOT NULL AND SupplierName <> '');
END
GO

-- Add CHECK constraint for Orders TotalAmount >= 0
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Orders_TotalAmount_NonNegative' AND parent_object_id = OBJECT_ID('Orders'))
BEGIN
    ALTER TABLE Orders
    ADD CONSTRAINT CK_Orders_TotalAmount_NonNegative CHECK (TotalAmount >= 0);
END
GO

-- Add CHECK constraint for Orders Subtotal >= 0
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Orders_Subtotal_NonNegative' AND parent_object_id = OBJECT_ID('Orders'))
BEGIN
    ALTER TABLE Orders
    ADD CONSTRAINT CK_Orders_Subtotal_NonNegative CHECK (Subtotal >= 0);
END
GO

-- Add CHECK constraint for OrderItems Quantity > 0
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_OrderItems_Quantity_Positive' AND parent_object_id = OBJECT_ID('OrderItems'))
BEGIN
    ALTER TABLE OrderItems
    ADD CONSTRAINT CK_OrderItems_Quantity_Positive CHECK (Quantity > 0);
END
GO

-- Add CHECK constraint for OrderItems UnitPrice >= 0
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_OrderItems_UnitPrice_NonNegative' AND parent_object_id = OBJECT_ID('OrderItems'))
BEGIN
    ALTER TABLE OrderItems
    ADD CONSTRAINT CK_OrderItems_UnitPrice_NonNegative CHECK (UnitPrice >= 0);
END
GO

PRINT 'Database constraints migration completed successfully.';
