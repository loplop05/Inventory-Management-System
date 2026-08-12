USE InventoryDB;
GO

IF OBJECT_ID('CustomerSegments', 'U') IS NULL
BEGIN
    CREATE TABLE CustomerSegments
    (
        SegmentID INT IDENTITY(1,1) PRIMARY KEY,
        CustomerID INT NOT NULL,
        SegmentLabel NVARCHAR(50) NOT NULL, -- 'Champions', 'Loyal Customers', 'At Risk', etc.
        RecencyScore INT NOT NULL, -- Days since last purchase (lower is better)
        FrequencyScore INT NOT NULL, -- Number of orders
        MonetaryScore DECIMAL(18,2) NOT NULL, -- Total spent
        ClusterID INT NOT NULL, -- K-Means cluster assignment
        GeneratedDate DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_CustomerSegments_Customers
        FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
    );

    CREATE INDEX IX_CustomerSegments_SegmentLabel ON CustomerSegments(SegmentLabel);
    CREATE INDEX IX_CustomerSegments_ClusterID ON CustomerSegments(ClusterID);

    PRINT 'CustomerSegments table created successfully.';
END
ELSE
BEGIN
    PRINT 'CustomerSegments table already exists.';
END
GO
