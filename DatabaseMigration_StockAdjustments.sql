-- Migration: Stock Adjustments with Reason Codes
-- Description: Adds table for tracking manual stock adjustments with audit trail
-- Date: 2025

-- Check if table exists
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'StockAdjustments')
BEGIN
    CREATE TABLE StockAdjustments (
        AdjustmentID INT IDENTITY(1,1) PRIMARY KEY,
        ProductID INT NOT NULL,
        QuantityChange INT NOT NULL,
        PreviousStock INT NOT NULL,
        NewStock INT NOT NULL,
        Reason INT NOT NULL,
        Notes NVARCHAR(500) NULL,
        AdjustedBy NVARCHAR(100) NULL,
        AdjustmentDate DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
    );

    PRINT 'StockAdjustments table created successfully.';
END
ELSE
BEGIN
    PRINT 'StockAdjustments table already exists.';
END

-- Add index for faster lookups
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_StockAdjustments_ProductID' AND object_id = OBJECT_ID('StockAdjustments'))
BEGIN
    CREATE INDEX IX_StockAdjustments_ProductID ON StockAdjustments(ProductID);
    PRINT 'Index IX_StockAdjustments_ProductID created.';
END

-- Add index for date range queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_StockAdjustments_AdjustmentDate' AND object_id = OBJECT_ID('StockAdjustments'))
BEGIN
    CREATE INDEX IX_StockAdjustments_AdjustmentDate ON StockAdjustments(AdjustmentDate);
    PRINT 'Index IX_StockAdjustments_AdjustmentDate created.';
END

PRINT 'Stock Adjustments migration completed.';
