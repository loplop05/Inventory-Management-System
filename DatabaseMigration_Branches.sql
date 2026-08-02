-- Migration for Multi-Branch/Register Support
-- Run this script to add branch and register functionality

-- Create Branches table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Branches')
BEGIN
    CREATE TABLE Branches (
        BranchID INT IDENTITY(1,1) PRIMARY KEY,
        BranchName NVARCHAR(100) NOT NULL,
        BranchCode NVARCHAR(20) NOT NULL UNIQUE,
        Address NVARCHAR(255),
        Phone NVARCHAR(20),
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT,
        UpdatedDate DATETIME,
        UpdatedBy INT
    );
    
    PRINT 'Branches table created successfully.';
END
ELSE
BEGIN
    PRINT 'Branches table already exists.';
END

-- Create Registers table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Registers')
BEGIN
    CREATE TABLE Registers (
        RegisterID INT IDENTITY(1,1) PRIMARY KEY,
        BranchID INT NOT NULL,
        RegisterName NVARCHAR(100) NOT NULL,
        RegisterCode NVARCHAR(20) NOT NULL,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT,
        UpdatedDate DATETIME,
        UpdatedBy INT,
        FOREIGN KEY (BranchID) REFERENCES Branches(BranchID)
    );
    
    PRINT 'Registers table created successfully.';
END
ELSE
BEGIN
    PRINT 'Registers table already exists.';
END

-- Add BranchID and RegisterID to Orders table if they don't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'BranchID')
BEGIN
    ALTER TABLE Orders ADD BranchID INT NULL;
    PRINT 'BranchID column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'BranchID column already exists in Orders table.';
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'RegisterID')
BEGIN
    ALTER TABLE Orders ADD RegisterID INT NULL;
    PRINT 'RegisterID column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'RegisterID column already exists in Orders table.';
END

-- Add foreign key constraints
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Orders_Branches')
BEGIN
    ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Branches 
        FOREIGN KEY (BranchID) REFERENCES Branches(BranchID);
    PRINT 'Foreign key FK_Orders_Branches added.';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Orders_Registers')
BEGIN
    ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Registers 
        FOREIGN KEY (RegisterID) REFERENCES Registers(RegisterID);
    PRINT 'Foreign key FK_Orders_Registers added.';
END

-- Add BranchID to Products table for branch-specific inventory tracking
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Products') AND name = 'BranchID')
BEGIN
    ALTER TABLE Products ADD BranchID INT NULL;
    PRINT 'BranchID column added to Products table.';
END
ELSE
BEGIN
    PRINT 'BranchID column already exists in Products table.';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Products_Branches')
BEGIN
    ALTER TABLE Products ADD CONSTRAINT FK_Products_Branches 
        FOREIGN KEY (BranchID) REFERENCES Branches(BranchID);
    PRINT 'Foreign key FK_Products_Branches added.';
END

-- Insert default branch if none exists
IF NOT EXISTS (SELECT * FROM Branches)
BEGIN
    INSERT INTO Branches (BranchName, BranchCode, Address, Phone, IsActive)
    VALUES ('Main Branch', 'MAIN', 'Main Location', '000-000-0000', 1);
    
    DECLARE @MainBranchID INT = SCOPE_IDENTITY();
    
    -- Insert default register for main branch
    INSERT INTO Registers (BranchID, RegisterName, RegisterCode, IsActive)
    VALUES (@MainBranchID, 'Register 1', 'REG-001', 1);
    
    PRINT 'Default branch and register created.';
END
ELSE
BEGIN
    PRINT 'Branches already have data.';
END

PRINT 'Branch/Register migration completed successfully.';
