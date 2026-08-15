-- Database Migration: Add MinStock Column
-- Adds minimum stock threshold for low-stock alerts

USE InventoryDB;
GO

-- Add MinStock column to Products table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'MinStock')
BEGIN
    ALTER TABLE Products
    ADD MinStock INT NOT NULL DEFAULT 10;
END
GO

-- Add index on MinStock for performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Products_MinStock' AND object_id = OBJECT_ID('Products'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_Products_MinStock ON Products(MinStock);
END
GO

-- Update existing products with default MinStock if needed
UPDATE Products
SET MinStock = 10
WHERE MinStock IS NULL OR MinStock < 0;
GO

PRINT 'MinStock column migration completed successfully.';
