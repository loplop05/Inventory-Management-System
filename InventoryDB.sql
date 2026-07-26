CREATE DATABASE InventoryDB;
GO

USE InventoryDB;
GO


-- =========================
-- Categories Table
-- =========================

CREATE TABLE Categories
(
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(50) UNIQUE NOT NULL
);


-- =========================
-- Suppliers Table
-- =========================

CREATE TABLE Suppliers
(
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100)
);


-- =========================
-- Products Table
-- =========================

CREATE TABLE Products
(
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,

    CategoryID INT NOT NULL,
    SupplierID INT NOT NULL,

    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL,

    Barcode NVARCHAR(50) UNIQUE NOT NULL,

    ImagePath NVARCHAR(300) NULL,

    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),


    CONSTRAINT FK_Products_Category
    FOREIGN KEY(CategoryID)
    REFERENCES Categories(CategoryID),


    CONSTRAINT FK_Products_Supplier
    FOREIGN KEY(SupplierID)
    REFERENCES Suppliers(SupplierID)
);
GO


-- =========================
-- POS Orders Table
-- =========================

CREATE TABLE Orders
(
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    Subtotal DECIMAL(10,2) NOT NULL,
    TaxAmount DECIMAL(10,2) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL
);
GO


-- =========================
-- POS Order Items Table
-- =========================

CREATE TABLE OrderItems
(
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    ProductName NVARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_OrderItems_Orders
    FOREIGN KEY(OrderID)
    REFERENCES Orders(OrderID),

    CONSTRAINT FK_OrderItems_Products
    FOREIGN KEY(ProductID)
    REFERENCES Products(ProductID)
);
GO



-- =========================
-- Insert Categories
-- =========================

INSERT INTO Categories(CategoryName)
VALUES
('Laptops'),
('Smartphones'),
('Tablets'),
('Accessories'),
('Monitors'),
('Keyboards'),
('Mice'),
('Storage Devices'),
('Networking'),
('Audio');



-- =========================
-- Insert Suppliers
-- =========================

INSERT INTO Suppliers
(SupplierName,Phone,Email)
VALUES
('Dell Technologies','0791000001','sales@dell.com'),
('HP Inc.','0791000002','sales@hp.com'),
('Logitech','0791000003','support@logitech.com'),
('Samsung Electronics','0791000004','contact@samsung.com'),
('Apple Inc.','0791000005','sales@apple.com');



-- =========================
-- Insert Products
-- =========================

INSERT INTO Products
(ProductName,CategoryID,SupplierID,Price,Quantity,Barcode,ImagePath)
VALUES

('Dell Inspiron 15',1,1,699.99,15,'1000001',NULL),

('HP Victus 15',1,2,899.99,10,'1000002',NULL),

('MacBook Air M4',1,5,1199.99,8,'1000003',NULL),


('Samsung Galaxy S25',2,4,999.99,20,'1000004',NULL),

('iPhone 17',2,5,1299.99,18,'1000005',NULL),

('Samsung Galaxy A56',2,4,499.99,25,'1000006',NULL),


('iPad Air',3,5,699.99,10,'1000007',NULL),

('Samsung Galaxy Tab S10',3,4,649.99,8,'1000008',NULL),


('USB-C Hub',4,3,39.99,50,'1000009',NULL),

('Laptop Backpack',4,3,49.99,35,'1000010',NULL),

('65W USB-C Charger',4,5,34.99,40,'1000011',NULL),


('Dell 24 Inch Monitor',5,1,179.99,15,'1000012',NULL),

('HP 27 Inch Monitor',5,2,299.99,9,'1000013',NULL),

('Samsung Odyssey G5',5,4,349.99,7,'1000014',NULL),


('Logitech MX Keys',6,3,99.99,25,'1000015',NULL),

('Logitech K380',6,3,49.99,30,'1000016',NULL),


('Logitech MX Master 3S',7,3,109.99,22,'1000018',NULL),

('Logitech M185',7,3,19.99,50,'1000020',NULL),


('Samsung 990 Pro 1TB SSD',8,4,129.99,16,'1000021',NULL),

('Samsung T7 Portable SSD',8,4,109.99,20,'1000022',NULL),


('TP-Link Archer AX55 Router',9,3,119.99,12,'1000024',NULL),

('TP-Link WiFi Range Extender',9,3,69.99,18,'1000025',NULL),


('Samsung Galaxy Buds3 Pro',10,4,229.99,15,'1000026',NULL),

('Apple AirPods Pro 3',10,5,279.99,20,'1000027',NULL),

('Logitech G Pro X Headset',10,3,129.99,18,'1000028',NULL);



-- Test

SELECT * FROM Categories;

SELECT * FROM Suppliers;

SELECT * FROM Products;
