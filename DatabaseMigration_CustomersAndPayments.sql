-- ============================================
-- Database Migration: Add Customers and Payment Support
-- ============================================

USE InventoryDB;
GO

-- =========================
-- Create Customers Table
-- =========================

IF OBJECT_ID('Customers', 'U') IS NULL
BEGIN
    CREATE TABLE Customers
    (
        CustomerID INT IDENTITY(1,1) PRIMARY KEY,
        PhoneNumber NVARCHAR(20) UNIQUE NOT NULL,
        CustomerName NVARCHAR(100) NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        LastPurchaseDate DATETIME NULL
    );
    
    PRINT 'Customers table created successfully.';
END
ELSE
BEGIN
    PRINT 'Customers table already exists.';
END
GO

-- =========================
-- Add Customer and Payment columns to Orders Table
-- =========================

IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Orders' 
    AND COLUMN_NAME = 'CustomerID'
)
BEGIN
    ALTER TABLE Orders
    ADD CustomerID INT NULL;
    
    PRINT 'CustomerID列 added to Orders table.';
END
ELSE
BEGIN
    PRINT 'CustomerID column already exists in Orders table.';
END
GO

IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Orders' 
    AND COLUMN_NAME = 'PaymentMethod'
)
BEGIN
    ALTER TABLE Orders
    ADD PaymentMethod NVARCHAR(50) NULL; -- 'Cash', 'Visa', 'MasterCard', etc.
    
    PRINT 'PaymentMethod column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'PaymentMethod column already exists in Orders table.';
END
GO

IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'Orders' 
    AND COLUMN_NAME = 'PaymentDetails'
)
BEGIN
    ALTER TABLE Orders
    ADD PaymentDetails NVARCHAR(100) NULL; -- Last 4 digits of card, etc.
    
    PRINT 'PaymentDetails column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'PaymentDetails column already exists in Orders table.';
END
GO

-- =========================
-- Add Foreign Key Constraint for CustomerID
-- =========================

IF NOT EXISTS (
    SELECT 1 
    FROM sys.foreign_keys 
    WHERE name = 'FK_Orders_Customers'
)
BEGIN
    ALTER TABLE Orders
    ADD CONSTRAINT FK_Orders_Customers
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID);
    
    PRINT 'Foreign key FK_Orders_Customers added successfully.';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_Orders_Customers already exists.';
END
GO

-- =========================
-- Create Index on CustomerID for better query performance
-- =========================

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IX_Orders_CustomerID' 
    AND object_id = OBJECT_ID('Orders')
)
BEGIN
    CREATE INDEX IX_Orders_CustomerID ON Orders(CustomerID);
    PRINT 'Index IX_Orders_CustomerID created successfully.';
END
ELSE
BEGIN
    PRINT 'Index IX_Orders_CustomerID already exists.';
END
GO

-- =========================
-- Create Index on PhoneNumber for faster customer lookups
-- =========================

IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'IX_Customers_PhoneNumber' 
    AND object_id = OBJECT_ID('Customers')
)
BEGIN
    CREATE INDEX IX_Customers_PhoneNumber ON Customers(PhoneNumber);
    PRINT 'Index IX_Customers_PhoneNumber created successfully.';
END
ELSE
BEGIN
    PRINT 'Index IX_Customers_PhoneNumber already exists.';
END
GO

PRINT '===========================================';
PRINT 'Database Migration Completed Successfully!';
PRINT '===========================================';
