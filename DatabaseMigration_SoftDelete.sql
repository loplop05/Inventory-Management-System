-- Database Migration: Soft Delete Pattern
-- Adds IsDeleted column to Products, Categories, and Suppliers tables
-- This prevents data integrity issues when deleting records referenced by Orders

USE InventoryDB;
GO

-- Add IsDeleted column to Products table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'IsDeleted')
BEGIN
    ALTER TABLE Products
    ADD IsDeleted BIT NOT NULL DEFAULT 0;
END
GO

-- Add IsDeleted column to Categories table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Categories') AND name = 'IsDeleted')
BEGIN
    ALTER TABLE Categories
    ADD IsDeleted BIT NOT NULL DEFAULT 0;
END
GO

-- Add IsDeleted column to Suppliers table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Suppliers') AND name = 'IsDeleted')
BEGIN
    ALTER TABLE Suppliers
    ADD IsDeleted BIT NOT NULL DEFAULT 0;
END
GO

-- Add index on IsDeleted for performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Products_IsDeleted' AND object_id = OBJECT_ID('Products'))
BEGIN
    CREATE INDEX IX_Products_IsDeleted ON Products(IsDeleted);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Categories_IsDeleted' AND object_id = OBJECT_ID('Categories'))
BEGIN
    CREATE INDEX IX_Categories_IsDeleted ON Categories(IsDeleted);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Suppliers_IsDeleted' AND object_id = OBJECT_ID('Suppliers'))
BEGIN
    CREATE INDEX IX_Suppliers_IsDeleted ON Suppliers(IsDeleted);
END
GO

PRINT 'Soft delete migration completed successfully.';
