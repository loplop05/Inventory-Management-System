-- =============================================
-- Held Orders Table Migration
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeldOrders')
BEGIN
    CREATE TABLE HeldOrders
    (
        HeldOrderID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT NULL,
        CustomerID INT NULL,
        CustomerName NVARCHAR(100) NULL,
        CustomerPhone NVARCHAR(20) NULL,
        PaymentMethod NVARCHAR(50) NULL,
        PaymentDetails NVARCHAR(100) NULL,
        CouponCode NVARCHAR(50) NULL,
        ManualDiscountType NVARCHAR(20) NULL, -- 'Percentage' or 'Fixed'
        ManualDiscountValue DECIMAL(10,2) NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        Notes NVARCHAR(500) NULL,
        
        CONSTRAINT FK_HeldOrders_Users FOREIGN KEY(UserID) REFERENCES Users(UserID),
        CONSTRAINT FK_HeldOrders_Customers FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)
    )
    
    PRINT 'HeldOrders table created successfully.'
END
ELSE
BEGIN
    PRINT 'HeldOrders table already exists.'
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'HeldOrderItems')
BEGIN
    CREATE TABLE HeldOrderItems
    (
        HeldOrderItemID INT IDENTITY(1,1) PRIMARY KEY,
        HeldOrderID INT NOT NULL,
        ProductID INT NOT NULL,
        ProductName NVARCHAR(200) NOT NULL,
        Quantity INT NOT NULL,
        UnitPrice DECIMAL(10,2) NOT NULL,
        Subtotal DECIMAL(10,2) NOT NULL,
        
        CONSTRAINT FK_HeldOrderItems_HeldOrders FOREIGN KEY(HeldOrderID) REFERENCES HeldOrders(HeldOrderID) ON DELETE CASCADE
    )
    
    PRINT 'HeldOrderItems table created successfully.'
END
ELSE
BEGIN
    PRINT 'HeldOrderItems table already exists.'
END
GO

-- Create index for faster queries
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_HeldOrders_CreatedDate' AND object_id = OBJECT_ID('HeldOrders'))
BEGIN
    CREATE INDEX IX_HeldOrders_CreatedDate ON HeldOrders(CreatedDate DESC)
    PRINT 'Index IX_HeldOrders_CreatedDate created successfully.'
END
GO
