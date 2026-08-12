-- ============================================================
-- Seed Products Fill Categories
-- ============================================================
-- Purpose: Add realistic products to ensure every category has 8-12 products
-- Domain: Apple Authorized Reseller in Jordan
-- Prices: JOD (Jordanian Dinar)
-- 
-- Summary of changes:
-- Category 1 (Laptops): 0 → 10 products
-- Category 2 (Smartphones): 0 → 10 products
-- Category 3 (Tablets): 0 → 10 products
-- Category 4 (Accessories): 3 → 10 products (+7)
-- Category 6 (Keyboards): 0 → 8 products
-- Category 7 (Mice): 0 → 8 products
-- Category 8 (Storage Devices): 0 → 8 products
-- Category 9 (Networking): 0 → 8 products
-- Category 10 (Audio): 0 → 10 products
-- Category 11 (Xbox): 0 → 8 products
-- Category 12 (Gaming): 0 → 8 products
-- Category 14 (Bags): 0 → 8 products
-- Category 18 (Monitors): 0 → 8 products
-- Category 20 (Cars): 0 → 8 products
-- Category 31 (Micros): 0 → 8 products
-- 
-- Note: Category 30 (??????) skipped due to corrupted name
-- ============================================================

USE InventoryDB;
GO

-- ============================================================
-- Category 1: Laptops (Apple MacBook lineup)
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('MacBook Air M3 13-inch 8GB/256GB', 1, 5, 899.00, 15, 'APMBAIRM313256', NULL, GETDATE()),
('MacBook Air M3 13-inch 8GB/512GB', 1, 5, 1099.00, 12, 'APMBAIRM313512', NULL, GETDATE()),
('MacBook Air M3 15-inch 8GB/256GB', 1, 5, 1199.00, 8, 'APMBAIRM315256', NULL, GETDATE()),
('MacBook Air M3 15-inch 8GB/512GB', 1, 5, 1399.00, 5, 'APMBAIRM315512', NULL, GETDATE()),
('MacBook Pro 14-inch M3 8GB/512GB', 1, 5, 1599.00, 10, 'APMBP14M38512', NULL, GETDATE()),
('MacBook Pro 14-inch M3 Pro 18GB/512GB', 1, 5, 1999.00, 7, 'APMBP14M3PRO18512', NULL, GETDATE()),
('MacBook Pro 14-inch M3 Max 36GB/1TB', 1, 5, 3199.00, 3, 'APMBP14M3MAX361T', NULL, GETDATE()),
('MacBook Pro 16-inch M3 Pro 18GB/512GB', 1, 5, 2499.00, 6, 'APMBP16M3PRO18512', NULL, GETDATE()),
('MacBook Pro 16-inch M3 Max 36GB/1TB', 1, 5, 3499.00, 4, 'APMBP16M3MAX361T', NULL, GETDATE()),
('MacBook Air M2 13-inch 8GB/256GB', 1, 5, 799.00, 0, 'APMBAIRM213256', NULL, GETDATE());
GO

-- ============================================================
-- Category 2: Smartphones (iPhone lineup)
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('iPhone 15 Pro Max 256GB Titanium', 2, 5, 649.00, 20, 'AP15PM256TIT', NULL, GETDATE()),
('iPhone 15 Pro Max 512GB Titanium', 2, 5, 769.00, 15, 'AP15PM512TIT', NULL, GETDATE()),
('iPhone 15 Pro 256GB Titanium', 2, 5, 599.00, 18, 'AP15P256TIT', NULL, GETDATE()),
('iPhone 15 Pro 512GB Titanium', 2, 5, 719.00, 12, 'AP15P512TIT', NULL, GETDATE()),
('iPhone 15 128GB Black', 2, 5, 449.00, 25, 'AP15128BLK', NULL, GETDATE()),
('iPhone 15 128GB Blue', 2, 5, 449.00, 22, 'AP15128BLU', NULL, GETDATE()),
('iPhone 15 256GB Pink', 2, 5, 519.00, 15, 'AP15256PNK', NULL, GETDATE()),
('iPhone 14 128GB Midnight', 2, 5, 399.00, 30, 'AP14128MID', NULL, GETDATE()),
('iPhone 14 256GB Starlight', 2, 5, 469.00, 20, 'AP14256STR', NULL, GETDATE()),
('iPhone SE 64GB Midnight', 2, 5, 279.00, 35, 'APSE64MID', NULL, GETDATE());
GO

-- ============================================================
-- Category 3: Tablets (iPad lineup)
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('iPad Pro 12.9-inch M4 256GB WiFi', 3, 5, 1199.00, 12, 'APP129M4256W', NULL, GETDATE()),
('iPad Pro 12.9-inch M4 512GB WiFi', 3, 5, 1399.00, 8, 'APP129M4512W', NULL, GETDATE()),
('iPad Pro 11-inch M4 256GB WiFi', 3, 5, 999.00, 15, 'APP11M4256W', NULL, GETDATE()),
('iPad Pro 11-inch M4 512GB WiFi', 3, 5, 1199.00, 10, 'APP11M4512W', NULL, GETDATE()),
('iPad Air 13-inch M2 256GB WiFi', 3, 5, 799.00, 18, 'APA13M2256W', NULL, GETDATE()),
('iPad Air 11-inch M2 256GB WiFi', 3, 5, 649.00, 22, 'APA11M2256W', NULL, GETDATE()),
('iPad 10th Gen 64GB WiFi Blue', 3, 5, 349.00, 25, 'AP10G64WBLU', NULL, GETDATE()),
('iPad 10th Gen 64GB WiFi Silver', 3, 5, 349.00, 28, 'AP10G64WSLV', NULL, GETDATE()),
('iPad mini 6th Gen 64GB WiFi', 3, 5, 449.00, 15, 'APM6G64W', NULL, GETDATE()),
('iPad mini 6th Gen 256GB WiFi', 3, 5, 599.00, 10, 'APM6G256W', NULL, GETDATE());
GO

-- ============================================================
-- Category 4: Accessories (Apple accessories)
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Apple Pencil Pro', 4, 5, 129.00, 20, 'APPENPRO', NULL, GETDATE()),
('Magic Keyboard iPad Pro 11-inch', 4, 5, 299.00, 12, 'MKBP11', NULL, GETDATE()),
('Magic Keyboard iPad Pro 13-inch', 4, 5, 349.00, 10, 'MKBP13', NULL, GETDATE()),
('Magic Mouse White', 4, 5, 99.00, 15, 'MMWHT', NULL, GETDATE()),
('Magic Trackpad White', 4, 5, 129.00, 12, 'MTPWHT', NULL, GETDATE()),
('MagSafe Charger', 4, 5, 39.00, 30, 'MSC', NULL, GETDATE()),
('MagSafe Battery Pack', 4, 5, 99.00, 18, 'MSBP', NULL, GETDATE());
GO

-- ============================================================
-- Category 6: Keyboards
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Magic Keyboard with Touch ID', 6, 3, 149.00, 15, 'MKWTD', NULL, GETDATE()),
('Magic Keyboard Numeric', 6, 3, 199.00, 12, 'MKNUM', NULL, GETDATE()),
('Logitech MX Keys S', 6, 3, 109.00, 20, 'LMXKS', NULL, GETDATE()),
('Logitech MX Mechanical', 6, 3, 149.00, 15, 'LMM', NULL, GETDATE()),
('Logitech K380 Multi-Device', 6, 3, 39.00, 25, 'LK380', NULL, GETDATE()),
('Logitech K780 Multi-Device', 6, 3, 59.00, 18, 'LK780', NULL, GETDATE()),
('Razer BlackWidow V3', 6, 3, 129.00, 10, 'RBWV3', NULL, GETDATE()),
('Razer Huntsman Mini', 6, 3, 99.00, 12, 'RHMINI', NULL, GETDATE());
GO

-- ============================================================
-- Category 7: Mice
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Logitech MX Master 3S', 7, 3, 99.00, 18, 'LMM3S', NULL, GETDATE()),
('Logitech MX Anywhere 3', 7, 3, 79.00, 22, 'LMA3', NULL, GETDATE()),
('Logitech G Pro Wireless', 7, 3, 89.00, 15, 'LGPW', NULL, GETDATE()),
('Logitech G502 HERO', 7, 3, 49.00, 25, 'LG502', NULL, GETDATE()),
('Razer DeathAdder V3', 7, 3, 69.00, 20, 'RDAV3', NULL, GETDATE()),
('Razer Viper Ultimate', 7, 3, 99.00, 12, 'RVU', NULL, GETDATE()),
('Apple Magic Mouse 2', 7, 5, 79.00, 15, 'AMM2', NULL, GETDATE()),
('Apple Magic Trackpad 2', 7, 5, 129.00, 10, 'AMT2', NULL, GETDATE());
GO

-- ============================================================
-- Category 8: Storage Devices
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Samsung T7 Shield 1TB', 8, 4, 89.00, 20, 'SST7S1T', NULL, GETDATE()),
('Samsung T7 Shield 2TB', 8, 4, 149.00, 15, 'SST7S2T', NULL, GETDATE()),
('Samsung T9 Portable 2TB', 8, 4, 199.00, 12, 'SST92T', NULL, GETDATE()),
('SanDisk Extreme Pro 1TB', 8, 4, 79.00, 25, 'SDEP1T', NULL, GETDATE()),
('SanDisk Extreme Pro 2TB', 8, 4, 129.00, 18, 'SDEP2T', NULL, GETDATE()),
('WD My Passport 1TB', 8, 4, 69.00, 30, 'WDMYP1T', NULL, GETDATE()),
('WD My Passport 2TB', 8, 4, 99.00, 22, 'WDMYP2T', NULL, GETDATE()),
('Crucial X8 1TB', 8, 4, 59.00, 28, 'CX81T', NULL, GETDATE());
GO

-- ============================================================
-- Category 9: Networking
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('ASUS AX6000 WiFi 6 Router', 9, 1, 199.00, 12, 'AAX6', NULL, GETDATE()),
('TP-Link Archer AX50', 9, 1, 89.00, 20, 'TAX50', NULL, GETDATE()),
('Netgear Nighthawk AX4', 9, 1, 149.00, 15, 'NNAX4', NULL, GETDATE()),
('Linksys MX5400', 9, 1, 179.00, 10, 'LMX54', NULL, GETDATE()),
('TP-Link Deco X50 Mesh', 9, 1, 129.00, 18, 'TDX50', NULL, GETDATE()),
('ASUS ZenWiFi AX6600', 9, 1, 299.00, 8, 'AZAX66', NULL, GETDATE()),
('Netgear Orbi RBK50', 9, 1, 249.00, 10, 'NORBK50', NULL, GETDATE()),
('Google Nest WiFi', 9, 1, 199.00, 12, 'GNW', NULL, GETDATE());
GO

-- ============================================================
-- Category 10: Audio (AirPods and speakers)
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('AirPods Pro 2nd Gen USB-C', 10, 5, 249.00, 25, 'APP2USBC', NULL, GETDATE()),
('AirPods Pro 2nd Gen MagSafe', 10, 5, 279.00, 20, 'APP2MS', NULL, GETDATE()),
('AirPods 3rd Gen', 10, 5, 179.00, 30, 'AP3', NULL, GETDATE()),
('AirPods Max Silver', 10, 5, 449.00, 12, 'APMSLV', NULL, GETDATE()),
('AirPods Max Space Gray', 10, 5, 449.00, 10, 'APMSG', NULL, GETDATE()),
('HomePod Mini White', 10, 5, 99.00, 18, 'HPMW', NULL, GETDATE()),
('HomePod Mini Orange', 10, 5, 99.00, 15, 'HPMO', NULL, GETDATE()),
('Beats Studio Pro', 10, 5, 349.00, 15, 'BSP', NULL, GETDATE()),
('Beats Studio Buds+', 10, 5, 179.00, 22, 'BSB+', NULL, GETDATE()),
('Beats Solo 4', 10, 5, 199.00, 18, 'BS4', NULL, GETDATE());
GO

-- ============================================================
-- Category 11: Xbox
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Xbox Series X 1TB', 11, 10, 449.00, 15, 'XSX1T', NULL, GETDATE()),
('Xbox Series S 512GB', 11, 10, 279.00, 25, 'XSS512', NULL, GETDATE()),
('Xbox Elite Series 2 Controller', 11, 10, 179.00, 20, 'XESC2', NULL, GETDATE()),
('Xbox Wireless Controller White', 11, 10, 59.00, 35, 'XWCW', NULL, GETDATE()),
('Xbox Wireless Controller Black', 11, 10, 59.00, 30, 'XWCB', NULL, GETDATE()),
('Xbox Game Pass Ultimate 1 Month', 11, 10, 14.99, 50, 'XGPU1M', NULL, GETDATE()),
('Xbox Game Pass Ultimate 3 Months', 11, 10, 39.99, 40, 'XGPU3M', NULL, GETDATE()),
('Xbox Stereo Headset', 11, 10, 59.00, 22, 'XSH', NULL, GETDATE());
GO

-- ============================================================
-- Category 12: Gaming
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('PlayStation 5 Standard', 12, 10, 449.00, 12, 'PS5STD', NULL, GETDATE()),
('PlayStation 5 Digital', 12, 10, 399.00, 15, 'PS5DIG', NULL, GETDATE()),
('PS5 DualSense Controller', 12, 10, 69.00, 30, 'PDSC', NULL, GETDATE()),
('PS5 DualSense Edge', 12, 10, 199.00, 10, 'PDSE', NULL, GETDATE()),
('Nintendo Switch OLED', 12, 10, 329.00, 18, 'NSOLED', NULL, GETDATE()),
('Nintendo Switch Lite', 12, 10, 199.00, 22, 'NSLITE', NULL, GETDATE()),
('Steam Deck LCD', 12, 10, 399.00, 10, 'SDLCD', NULL, GETDATE()),
('Steam Deck OLED', 12, 10, 549.00, 8, 'SDOLED', NULL, GETDATE());
GO

-- ============================================================
-- Category 14: Bags
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('MacBook Pro 16-inch Sleeve', 14, 6, 49.00, 20, 'MBP16SLV', NULL, GETDATE()),
('MacBook Air 13-inch Sleeve', 14, 6, 39.00, 25, 'MBA13SLV', NULL, GETDATE()),
('MacBook Pro 14-inch Backpack', 14, 6, 79.00, 15, 'MBP14BP', NULL, GETDATE()),
('iPad Pro 12.9-inch Case', 14, 6, 59.00, 22, 'APP129CS', NULL, GETDATE()),
('iPad Air 11-inch Case', 14, 6, 49.00, 28, 'APA11CS', NULL, GETDATE()),
('iPhone 15 Pro Max Case', 14, 6, 39.00, 35, 'I15PMCS', NULL, GETDATE()),
('iPhone 15 Case Clear', 14, 6, 19.00, 40, 'I15CSCLR', NULL, GETDATE()),
('AirPods Pro Case Silicone', 14, 6, 29.00, 30, 'APPCSIL', NULL, GETDATE());
GO

-- ============================================================
-- Category 18: Monitors
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Apple Studio Display 27-inch', 18, 5, 1599.00, 8, 'ASD27', NULL, GETDATE()),
('Apple Pro Display XDR', 18, 5, 4999.00, 3, 'APXDR', NULL, GETDATE()),
('LG UltraFine 5K 27-inch', 18, 4, 999.00, 10, 'LUF5K27', NULL, GETDATE()),
('LG UltraFine 4K 24-inch', 18, 4, 699.00, 15, 'LUF4K24', NULL, GETDATE()),
('Dell UltraSharp U2723QE', 18, 1, 549.00, 12, 'DUSU27', NULL, GETDATE()),
('Dell UltraSharp U3223QE', 18, 1, 699.00, 10, 'DUSU32', NULL, GETDATE()),
('Samsung Odyssey G7 32-inch', 18, 4, 599.00, 15, 'SOG732', NULL, GETDATE()),
('ASUS ProArt PA279CRV', 18, 1, 449.00, 18, 'APPA27', NULL, GETDATE());
GO

-- ============================================================
-- Category 20: Cars
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Tesla Model Y CarPlay Adapter', 20, 10, 199.00, 12, 'TMYCPA', NULL, GETDATE()),
('Tesla Model 3 CarPlay Adapter', 20, 10, 199.00, 15, 'TM3CPA', NULL, GETDATE()),
('Tesla Wireless Phone Charger', 20, 10, 89.00, 20, 'TWPC', NULL, GETDATE()),
('Tesla Center Console Organizer', 20, 10, 49.00, 25, 'TCCO', NULL, GETDATE()),
('Tesla Model Y Floor Mats', 20, 10, 79.00, 18, 'TYFM', NULL, GETDATE()),
('Tesla Model 3 Floor Mats', 20, 10, 79.00, 20, 'T3FM', NULL, GETDATE()),
('Tesla Roof Rack Model Y', 20, 10, 299.00, 8, 'TRYRR', NULL, GETDATE()),
('Tesla Bike Rack Model Y', 20, 10, 349.00, 6, 'TYBR', NULL, GETDATE());
GO

-- ============================================================
-- Category 31: Micros (Microphones)
-- ============================================================
INSERT INTO Products (ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate)
VALUES 
('Shure SM7B Microphone', 31, 3, 399.00, 8, 'SSM7B', NULL, GETDATE()),
('Rode NT-USB Microphone', 31, 3, 169.00, 15, 'RNTU', NULL, GETDATE()),
('Blue Yeti USB Microphone', 31, 3, 129.00, 20, 'BYU', NULL, GETDATE()),
('Audio-Technica AT2020', 31, 3, 149.00, 18, 'AAT2020', NULL, GETDATE()),
('HyperX QuadCast', 31, 3, 139.00, 22, 'HQ', NULL, GETDATE()),
('Elgato Wave 3', 31, 3, 159.00, 15, 'EW3', NULL, GETDATE()),
('Rode PodMic', 31, 3, 99.00, 25, 'RPM', NULL, GETDATE()),
('Samson Q2U USB', 31, 3, 69.00, 30, 'SQ2U', NULL, GETDATE());
GO

-- ============================================================
-- Verification Query
-- ============================================================
SELECT 
    c.CategoryID,
    c.CategoryName,
    COUNT(p.ProductID) AS ProductCount
FROM Categories c
LEFT JOIN Products p ON c.CategoryID = p.CategoryID
GROUP BY c.CategoryID, c.CategoryName
ORDER BY c.CategoryID;
GO
