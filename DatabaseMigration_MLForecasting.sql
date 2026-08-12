USE InventoryDB;
GO

IF OBJECT_ID('ProductForecasts', 'U') IS NULL
BEGIN
    CREATE TABLE ProductForecasts
    (
        ForecastID INT IDENTITY(1,1) PRIMARY KEY,
        ProductID INT NOT NULL,
        ForecastDate DATE NOT NULL,
        PredictedQty DECIMAL(10,2) NOT NULL,
        LowerBound DECIMAL(10,2) NULL,
        UpperBound DECIMAL(10,2) NULL,
        ModelType NVARCHAR(30) NOT NULL DEFAULT 'prophet', -- 'prophet' or 'linear_fallback'
        GeneratedDate DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_ProductForecasts_Products
        FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
    );

    CREATE INDEX IX_ProductForecasts_ProductID_Date ON ProductForecasts(ProductID, ForecastDate);

    PRINT 'ProductForecasts table created successfully.';
END
ELSE
BEGIN
    PRINT 'ProductForecasts table already exists.';
END
GO
