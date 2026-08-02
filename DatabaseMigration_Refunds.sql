-- Migration for Full Refund Workflow
-- Run this script to add refund functionality

-- Create Refunds table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Refunds')
BEGIN
    CREATE TABLE Refunds (
        RefundID INT IDENTITY(1,1) PRIMARY KEY,
        OrderID INT NOT NULL,
        RefundDate DATETIME DEFAULT GETDATE(),
        RefundAmount DECIMAL(18,2) NOT NULL,
        RefundReason NVARCHAR(500),
        RefundType NVARCHAR(50) DEFAULT 'Full', -- Full, Partial
        RefundMethod NVARCHAR(50), -- Cash, Card, StoreCredit
        ProcessedBy INT NOT NULL,
        IsVoided BIT DEFAULT 0,
        VoidDate DATETIME,
        VoidedBy INT,
        VoidReason NVARCHAR(500),
        CreatedDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
        FOREIGN KEY (ProcessedBy) REFERENCES Users(UserID),
        FOREIGN KEY (VoidedBy) REFERENCES Users(UserID)
    );
    
    PRINT 'Refunds table created successfully.';
END
ELSE
BEGIN
    PRINT 'Refunds table already exists.';
END

-- Create RefundItems table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RefundItems')
BEGIN
    CREATE TABLE RefundItems (
        RefundItemID INT IDENTITY(1,1) PRIMARY KEY,
        RefundID INT NOT NULL,
        ProductID INT NOT NULL,
        ProductName NVARCHAR(200) NOT NULL,
        Quantity INT NOT NULL,
        UnitPrice DECIMAL(18,2) NOT NULL,
        RefundAmount DECIMAL(18,2) NOT NULL,
        FOREIGN KEY (RefundID) REFERENCES Refunds(RefundID),
        FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
    );
    
    PRINT 'RefundItems table created successfully.';
END
ELSE
BEGIN
    PRINT 'RefundItems table already exists.';
END

-- Add RefundID to Orders table to mark refunded orders
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'RefundID')
BEGIN
    ALTER TABLE Orders ADD RefundID INT NULL;
    PRINT 'RefundID column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'RefundID column already exists in Orders table.';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Orders_Refunds')
BEGIN
    ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Refunds 
        FOREIGN KEY (RefundID) REFERENCES Refunds(RefundID);
    PRINT 'Foreign key FK_Orders_Refunds added.';
END

PRINT 'Refund migration completed successfully.';
