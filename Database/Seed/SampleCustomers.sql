-- ============================================
-- Sample Data: Customers for ML Services
-- ============================================
-- This script adds sample customers with varied purchase patterns
-- to support Customer Segmentation (RFM analysis)

USE InventoryDB;
GO

-- Check if we already have sample data
IF EXISTS (SELECT 1 FROM Customers WHERE CustomerID <= 100)
BEGIN
    PRINT 'Sample customer data already exists. Skipping insertion.';
    RETURN;
END
GO

PRINT 'Inserting sample customers...';
GO

-- Insert sample customers with different purchase patterns
-- These customers will have varied RFM (Recency, Frequency, Monetary) scores
INSERT INTO Customers (PhoneNumber, CustomerName, CreatedDate, LastPurchaseDate)
VALUES 
    -- High-value frequent customers (Champions)
    ('+1-555-0101', 'John Smith', DATEADD(DAY, -180, GETDATE()), DATEADD(DAY, -1, GETDATE())),
    ('+1-555-0102', 'Sarah Johnson', DATEADD(DAY, -200, GETDATE()), DATEADD(DAY, -2, GETDATE())),
    ('+1-555-0103', 'Michael Brown', DATEADD(DAY, -150, GETDATE()), DATEADD(DAY, -3, GETDATE())),
    ('+1-555-0104', 'Emily Davis', DATEADD(DAY, -170, GETDATE()), DATEADD(DAY, -1, GETDATE())),
    ('+1-555-0105', 'David Wilson', DATEADD(DAY, -190, GETDATE()), DATEADD(DAY, -4, GETDATE())),
    
    -- Loyal customers (regular but lower spend)
    ('+1-555-0201', 'Lisa Anderson', DATEADD(DAY, -365, GETDATE()), DATEADD(DAY, -7, GETDATE())),
    ('+1-555-0202', 'Robert Taylor', DATEADD(DAY, -400, GETDATE()), DATEADD(DAY, -10, GETDATE())),
    ('+1-555-0203', 'Jennifer Martinez', DATEADD(DAY, -380, GETDATE()), DATEADD(DAY, -5, GETDATE())),
    ('+1-555-0204', 'James Garcia', DATEADD(DAY, -350, GETDATE()), DATEADD(DAY, -14, GETDATE())),
    ('+1-555-0205', 'Maria Rodriguez', DATEADD(DAY, -420, GETDATE()), DATEADD(DAY, -8, GETDATE())),
    
    -- Potential loyalists (recent customers with good potential)
    ('+1-555-0301', 'William Lee', DATEADD(DAY, -60, GETDATE()), DATEADD(DAY, -1, GETDATE())),
    ('+1-555-0302', 'Sofia Clark', DATEADD(DAY, -45, GETDATE()), DATEADD(DAY, -2, GETDATE())),
    ('+1-555-0303', 'Daniel Wright', DATEADD(DAY, -90, GETDATE()), DATEADD(DAY, -5, GETDATE())),
    ('+1-555-0304', 'Olivia Hall', DATEADD(DAY, -30, GETDATE()), DATEADD(DAY, -1, GETDATE())),
    ('+1-555-0305', 'Joseph Allen', DATEADD(DAY, -75, GETDATE()), DATEADD(DAY, -3, GETDATE())),
    
    -- New customers (recent sign-ups, minimal history)
    ('+1-555-0401', 'Emma Young', DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -15, GETDATE())),
    ('+1-555-0402', 'Alexander King', DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -10, GETDATE())),
    ('+1-555-0403', 'Sophia Hill', DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -20, GETDATE())),
    ('+1-555-0404', 'Matthew Scott', DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -5, GETDATE())),
    ('+1-555-0405', 'Ava Green', DATEADD(DAY, -25, GETDATE()), DATEADD(DAY, -25, GETDATE())),
    
    -- At-risk customers (previously active, haven't purchased recently)
    ('+1-555-0501', 'Ethan Adams', DATEADD(DAY, -500, GETDATE()), DATEADD(DAY, -90, GETDATE())),
    ('+1-555-0502', 'Chloe Nelson', DATEADD(DAY, -480, GETDATE()), DATEADD(DAY, -85, GETDATE())),
    ('+1-555-0503', 'Benjamin Carter', DATEADD(DAY, -520, GETDATE()), DATEADD(DAY, -100, GETDATE())),
    ('+1-555-0504', 'Mia Mitchell', DATEADD(DAY, -490, GETDATE()), DATEADD(DAY, -95, GETDATE())),
    ('+1-555-0505', 'Lucas Perez', DATEADD(DAY, -510, GETDATE()), DATEADD(DAY, -80, GETDATE())),
    
    -- Hibernating customers (very infrequent, low spend)
    ('+1-555-0601', 'Henry Roberts', DATEADD(DAY, -600, GETDATE()), DATEADD(DAY, -200, GETDATE())),
    ('+1-555-0602', 'Grace Turner', DATEADD(DAY, -650, GETDATE()), DATEADD(DAY, -250, GETDATE())),
    ('+1-555-0603', 'Jack Phillips', DATEADD(DAY, -700, GETDATE()), DATEADD(DAY, -300, GETDATE())),
    ('+1-555-0604', 'Victoria Campbell', DATEADD(DAY, -680, GETDATE()), DATEADD(DAY, -280, GETDATE())),
    ('+1-555-0605', 'Samuel Parker', DATEADD(DAY, -720, GETDATE()), DATEADD(DAY, -350, GETDATE())),
    
    -- Lost customers (very old, no recent activity)
    ('+1-555-0701', 'Andrew Evans', DATEADD(DAY, -900, GETDATE()), DATEADD(DAY, -600, GETDATE())),
    ('+1-555-0702', 'Isabella Edwards', DATEADD(DAY, -950, GETDATE()), DATEADD(DAY, -650, GETDATE())),
    ('+1-555-0703', 'Joshua Collins', DATEADD(DAY, -880, GETDATE()), DATEADD(DAY, -580, GETDATE())),
    ('+1-555-0704', 'Charlotte Stewart', DATEADD(DAY, -920, GETDATE()), DATEADD(DAY, -620, GETDATE())),
    ('+1-555-0705', 'Dylan Morris', DATEADD(DAY, -980, GETDATE()), DATEADD(DAY, -700, GETDATE())),
    
    -- Additional diverse customers for better segmentation
    ('+1-555-0801', 'Gabriel Reed', DATEADD(DAY, -120, GETDATE()), DATEADD(DAY, -30, GETDATE())),
    ('+1-555-0802', 'Hannah Cook', DATEADD(DAY, -140, GETDATE()), DATEADD(DAY, -25, GETDATE())),
    ('+1-555-0803', 'Samuel Morgan', DATEADD(DAY, -110, GETDATE()), DATEADD(DAY, -20, GETDATE())),
    ('+1-555-0804', 'Lily Bell', DATEADD(DAY, -130, GETDATE()), DATEADD(DAY, -35, GETDATE())),
    ('+1-555-0805', 'Nathan Murphy', DATEADD(DAY, -160, GETDATE()), DATEADD(DAY, -40, GETDATE())),
    ('+1-555-0806', 'Zoe Bailey', DATEADD(DAY, -145, GETDATE()), DATEADD(DAY, -28, GETDATE())),
    ('+1-555-0807', 'Ryan Rivera', DATEADD(DAY, -155, GETDATE()), DATEADD(DAY, -32, GETDATE())),
    ('+1-555-0808', 'Ella Cooper', DATEADD(DAY, -125, GETDATE()), DATEADD(DAY, -22, GETDATE())),
    ('+1-555-0809', 'Owen Richardson', DATEADD(DAY, -135, GETDATE()), DATEADD(DAY, -38, GETDATE())),
    ('+1-555-0810', 'Penelope Cox', DATEADD(DAY, -165, GETDATE()), DATEADD(DAY, -45, GETDATE()));

PRINT 'Sample customers inserted successfully.';
GO

PRINT '===========================================';
PRINT 'Sample Customer Data Insertion Complete!';
PRINT 'Total Customers: ' + CAST((SELECT COUNT(*) FROM Customers) AS NVARCHAR(10));
PRINT '===========================================';
GO
