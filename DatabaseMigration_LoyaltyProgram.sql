-- ============================================
-- Database Migration: Customer Loyalty Program
-- ============================================

USE InventoryDB;
GO

-- =========================
-- Add Loyalty Points Column to Customers
-- =========================

IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Customers' AND COLUMN_NAME = 'LoyaltyPoints'
)
BEGIN
    ALTER TABLE Customers ADD LoyaltyPoints INT NOT NULL DEFAULT 0;
    PRINT 'LoyaltyPoints column added to Customers table.';
END
ELSE
BEGIN
    PRINT 'LoyaltyPoints column already exists in Customers table.';
END
GO

-- =========================
-- Add Loyalty Tier Column to Customers
-- =========================

IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Customers' AND COLUMN_NAME = 'LoyaltyTier'
)
BEGIN
    ALTER TABLE Customers ADD LoyaltyTier NVARCHAR(20) NOT NULL DEFAULT 'Bronze';
    PRINT 'LoyaltyTier column added to Customers table.';
END
ELSE
BEGIN
    PRINT 'LoyaltyTier column already exists in Customers table.';
END
GO

-- =========================
-- Create Loyalty Points History Table
-- =========================

IF OBJECT_ID('LoyaltyPointsHistory', 'U') IS NULL
BEGIN
    CREATE TABLE LoyaltyPointsHistory
    (
        HistoryID INT IDENTITY(1,1) PRIMARY KEY,
        CustomerID INT NOT NULL,
        ChangeAmount INT NOT NULL,
        Reason NVARCHAR(200) NULL,
        Source NVARCHAR(30) NOT NULL,
        OrderID INT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedByUserID INT NULL,

        CONSTRAINT FK_LoyaltyHistory_Customers FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
        CONSTRAINT FK_LoyaltyHistory_Orders FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
    );
    PRINT 'LoyaltyPointsHistory table created successfully.';
END
ELSE
BEGIN
    PRINT 'LoyaltyPointsHistory table already exists.';
END
GO

-- =========================
-- Create Index on CustomerID for Loyalty History
-- =========================

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes WHERE name = 'IX_LoyaltyHistory_CustomerID' AND object_id = OBJECT_ID('LoyaltyPointsHistory')
)
BEGIN
    CREATE INDEX IX_LoyaltyHistory_CustomerID ON LoyaltyPointsHistory(CustomerID);
    PRINT 'Index IX_LoyaltyHistory_CustomerID created successfully.';
END
ELSE
BEGIN
    PRINT 'Index IX_LoyaltyHistory_CustomerID already exists.';
END
GO

PRINT '===========================================';
PRINT 'Loyalty Program Database Migration Completed Successfully!';
PRINT '===========================================';
