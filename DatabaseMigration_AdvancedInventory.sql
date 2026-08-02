-- Migration for Advanced Inventory Features
-- Run this script to add expiration tracking, batch management, and stock transfers

-- Add expiration tracking to Products table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'ExpirationDate')
BEGIN
    ALTER TABLE Products ADD ExpirationDate DATE NULL;
    PRINT 'ExpirationDate column added to Products table.';
END
ELSE
BEGIN
    PRINT 'ExpirationDate column already exists in Products table.';
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'BatchNumber')
BEGIN
    ALTER TABLE Products ADD BatchNumber NVARCHAR(50) NULL;
    PRINT 'BatchNumber column added to Products table.';
END
ELSE
BEGIN
    PRINT 'BatchNumber column already exists in Products table.';
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'ManufactureDate')
BEGIN
    ALTER TABLE Products ADD ManufactureDate DATE NULL;
    PRINT 'ManufactureDate column added to Products table.';
END
ELSE
BEGIN
    PRINT 'ManufactureDate column already exists in Products table.';
END

-- Create StockTransfers table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StockTransfers')
BEGIN
    CREATE TABLE StockTransfers (
        TransferID INT IDENTITY(1,1) PRIMARY KEY,
        FromBranchID INT NOT NULL,
        ToBranchID INT NOT NULL,
        TransferDate DATETIME DEFAULT GETDATE(),
        TransferStatus NVARCHAR(50) DEFAULT 'Pending', -- Pending, Approved, InTransit, Completed, Cancelled
        Notes NVARCHAR(500),
        CreatedBy INT NOT NULL,
        ApprovedBy INT NULL,
        ApprovalDate DATETIME NULL,
        CompletedBy INT NULL,
        CompletionDate DATETIME NULL,
        FOREIGN KEY (FromBranchID) REFERENCES Branches(BranchID),
        FOREIGN KEY (ToBranchID) REFERENCES Branches(BranchID),
        FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
        FOREIGN KEY (ApprovedBy) REFERENCES Users(UserID),
        FOREIGN KEY (CompletedBy) REFERENCES Users(UserID)
    );
    
    PRINT 'StockTransfers table created successfully.';
END
ELSE
BEGIN
    PRINT 'StockTransfers table already exists.';
END

-- Create StockTransferItems table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StockTransferItems')
BEGIN
    CREATE TABLE StockTransferItems (
        TransferItemID INT IDENTITY(1,1) PRIMARY KEY,
        TransferID INT NOT NULL,
        ProductID INT NOT NULL,
        ProductName NVARCHAR(200) NOT NULL,
        Quantity INT NOT NULL,
        UnitPrice DECIMAL(18,2) NOT NULL,
        FOREIGN KEY (TransferID) REFERENCES StockTransfers(TransferID),
        FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
    );
    
    PRINT 'StockTransferItems table created successfully.';
END
ELSE
BEGIN
    PRINT 'StockTransferItems table already exists.';
END

PRINT 'Advanced inventory migration completed successfully.';
