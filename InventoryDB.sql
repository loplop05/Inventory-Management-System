


create table Categories(

CategoryID int primary key,

CategoryName nvarchar(50) unique not null , 


)



create table Suppliers(

SupplierID int primary key, 
SupplierName nvarchar(100) ,
Phone nvarchar(20) , 
Email nvarchar(100)

)


CREATE TABLE Products (
    ProductID INT IDENTITY PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    CategoryID INT NOT NULL,
    SupplierID INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL,
    Barcode NVARCHAR(50) NOT NULL UNIQUE,
    ImagePath NVARCHAR(300) NULL,
    CreatedDate DATETIME NOT NULL,
    CONSTRAINT FK_Products_Category FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    CONSTRAINT FK_Products_Supplier FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);


-- Categories
INSERT INTO Categories (CategoryID, CategoryName)
VALUES
(1, 'Laptops'),
(2, 'Smartphones'),
(3, 'Tablets'),
(4, 'Accessories'),
(5, 'Monitors'),
(6, 'Keyboards'),
(7, 'Mice'),
(8, 'Storage Devices'),
(9, 'Networking'),
(10, 'Audio');


-- Suppliers
INSERT INTO Suppliers (SupplierID, SupplierName, Phone, Email)
VALUES
(1, 'Dell Technologies', '0791000001', 'sales@dell.com'),
(2, 'HP Inc.', '0791000002', 'sales@hp.com'),
(3, 'Logitech', '0791000003', 'support@logitech.com'),
(4, 'Samsung Electronics', '0791000004', 'contact@samsung.com'),
(5, 'Apple Inc.', '0791000005', 'sales@apple.com');



INSERT INTO Products
(ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES
-- Dell
('Dell Inspiron 15',1,1,699.99,15,'1000001',NULL,GETDATE()),

-- HP
('HP Victus 15',1,2,899.99,10,'1000002',NULL,GETDATE()),

-- Apple
('MacBook Air M4',1,5,1199.99,8,'1000003',NULL,GETDATE()),

-- Samsung
('Samsung Galaxy S25',2,4,999.99,20,'1000004',NULL,GETDATE()),

-- Apple
('iPhone 17',2,5,1299.99,18,'1000005',NULL,GETDATE()),

-- Samsung
('Samsung Galaxy A56',2,4,499.99,25,'1000006',NULL,GETDATE()),

-- Apple
('iPad Air',3,5,699.99,10,'1000007',NULL,GETDATE()),

-- Samsung
('Samsung Galaxy Tab S10',3,4,649.99,8,'1000008',NULL,GETDATE()),

-- Accessories
('USB-C Hub',4,3,39.99,50,'1000009',NULL,GETDATE()),
('Laptop Backpack',4,3,49.99,35,'1000010',NULL,GETDATE()),
('65W USB-C Charger',4,5,34.99,40,'1000011',NULL,GETDATE()),

-- Monitors
('Dell 24 Inch Monitor',5,1,179.99,15,'1000012',NULL,GETDATE()),
('HP 27 Inch Monitor',5,2,299.99,9,'1000013',NULL,GETDATE()),
('Samsung Odyssey G5',5,4,349.99,7,'1000014',NULL,GETDATE()),

-- Keyboards
('Logitech MX Keys',6,3,99.99,25,'1000015',NULL,GETDATE()),
('Logitech K380',6,3,49.99,30,'1000016',NULL,GETDATE()),
('Logitech G915',6,3,199.99,12,'1000017',NULL,GETDATE()),

-- Mice
('Logitech MX Master 3S',7,3,109.99,22,'1000018',NULL,GETDATE()),
('Logitech G Pro X Superlight',7,3,149.99,18,'1000019',NULL,GETDATE()),
('Logitech M185',7,3,19.99,50,'1000020',NULL,GETDATE()),

-- Storage
('Samsung 990 Pro 1TB SSD',8,4,129.99,16,'1000021',NULL,GETDATE()),
('Samsung T7 Portable SSD',8,4,109.99,20,'1000022',NULL,GETDATE()),
('SanDisk 64GB USB Flash Drive',8,3,14.99,60,'1000023',NULL,GETDATE()),

-- Networking
('TP-Link Archer AX55 Router',9,3,119.99,12,'1000024',NULL,GETDATE()),
('TP-Link WiFi Range Extender',9,3,69.99,18,'1000025',NULL,GETDATE()),

-- Audio
('Samsung Galaxy Buds3 Pro',10,4,229.99,15,'1000026',NULL,GETDATE()),
('Apple AirPods Pro 3',10,5,279.99,20,'1000027',NULL,GETDATE()),
('Logitech G Pro X Headset',10,3,129.99,18,'1000028',NULL,GETDATE()),
('Apple HomePod Mini',10,5,99.99,10,'1000029',NULL,GETDATE()),
('Samsung Soundbar Q600C',10,4,349.99,8,'1000030',NULL,GETDATE());



SELECT * FROM Categories;


-- 1. Add a temporary identity column
ALTER TABLE Categories ADD CategoryID INT IDENTITY(1,1);

-- 2. Drop the original column
ALTER TABLE Categories DROP COLUMN CategoryID;

-- 3. Rename the temporary column to match your original column name
EXEC sp_rename 'Categories.CategoryID', 'CategoryID', 'CategoryID';


