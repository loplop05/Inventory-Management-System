-- ============================================
-- Database Migration: Shift & Cash Drawer Management
-- ============================================

USE InventoryDB;
GO

-- =========================
-- Create Shifts Table
-- =========================

IF OBJECT_ID('Shifts', 'U') IS NULL
BEGIN
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
        Notes NVARCHAR(300) NULL,

        CONSTRAINT FK_Shifts_Users FOREIGN KEY (UserID) REFERENCES Users(UserID)
    );
    PRINT 'Shifts table created successfully.';
END
ELSE
BEGIN
    PRINT 'Shifts table already exists.';
END
GO

-- =========================
-- Add ShiftID Column to Orders Table
-- =========================

IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Orders' AND COLUMN_NAME = 'ShiftID'
)
BEGIN
    ALTER TABLE Orders ADD ShiftID INT NULL;
    PRINT 'ShiftID column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'ShiftID column already exists in Orders table.';
END
GO

-- =========================
-- Add Foreign Key Constraint for ShiftID
-- =========================

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Orders_Shifts')
BEGIN
    ALTER TABLE Orders
    ADD CONSTRAINT FK_Orders_Shifts FOREIGN KEY (ShiftID) REFERENCES Shifts(ShiftID);
    PRINT 'Foreign key FK_Orders_Shifts added successfully.';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_Orders_Shifts already exists.';
END
GO

-- =========================
-- Create Index on Shifts for Performance
-- =========================

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes WHERE name = 'IX_Shifts_UserID_Status' AND object_id = OBJECT_ID('Shifts')
)
BEGIN
    CREATE INDEX IX_Shifts_UserID_Status ON Shifts(UserID, Status);
    PRINT 'Index IX_Shifts_UserID_Status created successfully.';
END
ELSE
BEGIN
    PRINT 'Index IX_Shifts_UserID_Status already exists.';
END
GO

PRINT '===========================================';
PRINT 'Shift Management Database Migration Completed Successfully!';
PRINT '===========================================';
