USE InventoryDB;
GO

-- =========================
-- Permissions Table
-- =========================

CREATE TABLE Permissions
(
    PermissionID INT IDENTITY(1,1) PRIMARY KEY,
    PermissionName NVARCHAR(50) UNIQUE NOT NULL,
    Description NVARCHAR(200)
);
GO

-- =========================
-- Role-Permission Mapping Table
-- =========================

CREATE TABLE RolePermissions
(
    RoleID INT NOT NULL,
    PermissionID INT NOT NULL,
    PRIMARY KEY (RoleID, PermissionID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
    FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID)
);
GO

-- =========================
-- Seed Permissions
-- =========================

INSERT INTO Permissions (PermissionName, Description) VALUES
('ViewDashboard', 'View dashboard and analytics'),
('ManageProducts', 'Add/edit/delete products'),
('ManageCategories', 'Add/edit/delete categories'),
('ManageSuppliers', 'Add/edit/delete suppliers'),
('ManageCustomers', 'View/manage customer data'),
('ManageUsers', 'Add/edit/delete users'),
('ViewReports', 'View all reports'),
('ProcessSales', 'Process POS transactions'),
('ManageCoupons', 'Create/manage coupons'),
('ViewAuditLogs', 'View audit logs'),
('AdjustLoyalty', 'Manually adjust loyalty points'),
('DeleteOrders', 'Delete/cancel orders'),
('ManageInventory', 'Manage stock levels and reorders');
GO

-- =========================
-- Seed Role Permissions
-- =========================

-- Admin: All permissions
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 1, PermissionID FROM Permissions WHERE PermissionName IN (
    'ViewDashboard', 'ManageProducts', 'ManageCategories', 'ManageSuppliers',
    'ManageCustomers', 'ManageUsers', 'ViewReports', 'ProcessSales',
    'ManageCoupons', 'ViewAuditLogs', 'AdjustLoyalty', 'DeleteOrders', 'ManageInventory'
);

-- Manager: All except ManageUsers, ViewAuditLogs
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 2, PermissionID FROM Permissions WHERE PermissionName IN (
    'ViewDashboard', 'ManageProducts', 'ManageCategories', 'ManageSuppliers',
    'ManageCustomers', 'ViewReports', 'ProcessSales',
    'ManageCoupons', 'AdjustLoyalty', 'DeleteOrders', 'ManageInventory'
);

-- Cashier: ProcessSales only
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 3, PermissionID FROM Permissions WHERE PermissionName = 'ProcessSales';

-- Inventory Manager: Inventory-related permissions
INSERT INTO RolePermissions (RoleID, PermissionID)
SELECT 4, PermissionID FROM Permissions WHERE PermissionName IN (
    'ManageProducts', 'ManageCategories', 'ManageSuppliers', 'ViewReports', 'ManageInventory'
);
GO

-- =========================
-- Add Notes Column to Customers Table
-- =========================

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Customers') AND name = 'Notes')
BEGIN
    ALTER TABLE Customers ADD Notes NVARCHAR(500) NULL;
END
GO

-- =========================
-- Add Birthday Column to Customers Table (for future loyalty enhancements)
-- =========================

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Customers') AND name = 'Birthday')
BEGIN
    ALTER TABLE Customers ADD Birthday DATE NULL;
END
GO

-- =========================
-- Add ReferredBy Column to Customers Table (for future referral system)
-- =========================

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Customers') AND name = 'ReferredBy')
BEGIN
    ALTER TABLE Customers ADD ReferredBy INT NULL;
END
GO

-- =========================
-- Loyalty Points History Table (for point expiration tracking)
-- =========================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LoyaltyPointsHistory')
BEGIN
    CREATE TABLE LoyaltyPointsHistory
    (
        HistoryID INT IDENTITY(1,1) PRIMARY KEY,
        CustomerID INT NOT NULL,
        PointsChange INT NOT NULL,
        PointsBefore INT NOT NULL,
        PointsAfter INT NOT NULL,
        ChangeReason NVARCHAR(200) NOT NULL,
        ChangeDate DATETIME NOT NULL DEFAULT GETDATE(),
        ExpirationDate DATETIME NULL,
        IsExpired BIT NOT NULL DEFAULT 0,
        FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
    );
END
GO

-- =========================
-- Create Index for Point Expiration Queries
-- =========================

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_LoyaltyPointsHistory_Expiration' AND object_id = OBJECT_ID('LoyaltyPointsHistory'))
BEGIN
    CREATE INDEX IX_LoyaltyPointsHistory_Expiration ON LoyaltyPointsHistory(CustomerID, ExpirationDate, IsExpired);
END
GO
