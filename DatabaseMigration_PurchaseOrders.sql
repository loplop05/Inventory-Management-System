-- Migration: Purchase Order Workflow
-- Description: Adds tables for managing purchase orders and stock replenishment
-- Date: 2025

-- Check if PurchaseOrders table exists
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PurchaseOrders')
BEGIN
    CREATE TABLE PurchaseOrders (
        PurchaseOrderID INT IDENTITY(1,1) PRIMARY KEY,
        SupplierID INT NOT NULL,
        TotalCost DECIMAL(10,2) NOT NULL,
        Status INT NOT NULL DEFAULT 0,
        Notes NVARCHAR(500) NULL,
        CreatedBy NVARCHAR(100) NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ExpectedDate DATETIME NULL,
        ReceivedDate DATETIME NULL,
        ReceivedBy NVARCHAR(100) NULL,
        ModifiedDate DATETIME NULL,
        FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
    );

    PRINT 'PurchaseOrders table created successfully.';
END
ELSE
BEGIN
    PRINT 'PurchaseOrders table already exists.';
END

-- Check if PurchaseOrderItems table exists
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PurchaseOrderItems')
BEGIN
    CREATE TABLE PurchaseOrderItems (
        PurchaseOrderItemID INT IDENTITY(1,1) PRIMARY KEY,
        PurchaseOrderID INT NOT NULL,
        ProductID INT NOT NULL,
        Quantity INT NOT NULL,
        UnitCost DECIMAL(10,2) NOT NULL,
        TotalCost DECIMAL(10,2) NOT NULL,
        FOREIGN KEY (PurchaseOrderID) REFERENCES PurchaseOrders(PurchaseOrderID),
        FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
    );

    PRINT 'PurchaseOrderItems table created successfully.';
END
ELSE
BEGIN
    PRINT 'PurchaseOrderItems table already exists.';
END

-- Add indexes for performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseOrders_SupplierID' AND object_id = OBJECT_ID('PurchaseOrders'))
BEGIN
    CREATE INDEX IX_PurchaseOrders_SupplierID ON PurchaseOrders(SupplierID);
    PRINT 'Index IX_PurchaseOrders_SupplierID created.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseOrders_Status' AND object_id = OBJECT_ID('PurchaseOrders'))
BEGIN
    CREATE INDEX IX_PurchaseOrders_Status ON PurchaseOrders(Status);
    PRINT 'Index IX_PurchaseOrders_Status created.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseOrders_CreatedDate' AND object_id = OBJECT_ID('PurchaseOrders'))
BEGIN
    CREATE INDEX IX_PurchaseOrders_CreatedDate ON PurchaseOrders(CreatedDate);
    PRINT 'Index IX_PurchaseOrders_CreatedDate created.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseOrderItems_PurchaseOrderID' AND object_id = OBJECT_ID('PurchaseOrderItems'))
BEGIN
    CREATE INDEX IX_PurchaseOrderItems_PurchaseOrderID ON PurchaseOrderItems(PurchaseOrderID);
    PRINT 'Index IX_PurchaseOrderItems_PurchaseOrderID created.';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_PurchaseOrderItems_ProductID' AND object_id = OBJECT_ID('PurchaseOrderItems'))
BEGIN
    CREATE INDEX IX_PurchaseOrderItems_ProductID ON PurchaseOrderItems(ProductID);
    PRINT 'Index IX_PurchaseOrderItems_ProductID created.';
END

PRINT 'Purchase Orders migration completed.';
