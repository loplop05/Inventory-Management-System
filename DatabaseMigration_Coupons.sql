-- Migration for Enhanced Coupon System
-- Run this script to add coupon functionality

-- Create Coupons table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Coupons')
BEGIN
    CREATE TABLE Coupons (
        CouponID INT IDENTITY(1,1) PRIMARY KEY,
        CouponCode NVARCHAR(50) NOT NULL UNIQUE,
        CouponType NVARCHAR(50) NOT NULL, -- Percentage, FixedAmount, BOGO
        DiscountValue DECIMAL(18,2) NOT NULL,
        MinPurchaseAmount DECIMAL(18,2) DEFAULT 0,
        MaxDiscountAmount DECIMAL(18,2) NULL,
        ValidFrom DATETIME NOT NULL,
        ValidUntil DATETIME NOT NULL,
        UsageLimit INT NULL, -- NULL for unlimited
        UsedCount INT DEFAULT 0,
        IsActive BIT DEFAULT 1,
        ApplicableCategories NVARCHAR(MAX) NULL, -- Comma-separated category IDs
        ApplicableProducts NVARCHAR(MAX) NULL, -- Comma-separated product IDs
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NULL,
        FOREIGN KEY (CreatedBy) REFERENCES Users(UserID)
    );
    
    PRINT 'Coupons table created successfully.';
END
ELSE
BEGIN
    PRINT 'Coupons table already exists.';
END

-- Add CouponID to Orders table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'CouponID')
BEGIN
    ALTER TABLE Orders ADD CouponID INT NULL;
    PRINT 'CouponID column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'CouponID column already exists in Orders table.';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Orders_Coupons')
BEGIN
    ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Coupons 
        FOREIGN KEY (CouponID) REFERENCES Coupons(CouponID);
    PRINT 'Foreign key FK_Orders_Coupons added.';
END

-- Add CouponDiscount to Orders table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Orders') AND name = 'CouponDiscount')
BEGIN
    ALTER TABLE Orders ADD CouponDiscount DECIMAL(18,2) DEFAULT 0;
    PRINT 'CouponDiscount column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'CouponDiscount column already exists in Orders table.';
END

PRINT 'Coupon migration completed successfully.';
