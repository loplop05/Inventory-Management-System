-- ============================================
-- Database Migration: Add Coupon Discount Support
-- ============================================

USE InventoryDB;
GO

-- =========================
-- Add discount columns to Orders Table
-- =========================

IF NOT EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Orders'
    AND COLUMN_NAME = 'DiscountAmount'
)
BEGIN
    ALTER TABLE Orders ADD DiscountAmount DECIMAL(10,2) NOT NULL DEFAULT 0;
    PRINT 'DiscountAmount column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'DiscountAmount column already exists.';
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Orders'
    AND COLUMN_NAME = 'CouponCode'
)
BEGIN
    ALTER TABLE Orders ADD CouponCode NVARCHAR(50) NULL;
    PRINT 'CouponCode column added to Orders table.';
END
ELSE
BEGIN
    PRINT 'CouponCode column already exists.';
END
GO

PRINT 'Coupon discount migration completed successfully.';
GO
