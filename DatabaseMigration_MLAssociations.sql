USE InventoryDB;
GO

IF OBJECT_ID('ProductAssociations', 'U') IS NULL
BEGIN
    CREATE TABLE ProductAssociations
    (
        AssociationID INT IDENTITY(1,1) PRIMARY KEY,
        AntecedentProductID INT NOT NULL,     -- "if customer buys this..."
        ConsequentProductID INT NOT NULL,     -- "...suggest this"
        Support DECIMAL(6,4) NOT NULL,
        Confidence DECIMAL(6,4) NOT NULL,
        Lift DECIMAL(8,4) NOT NULL,
        GeneratedDate DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_ProductAssociations_Antecedent
        FOREIGN KEY (AntecedentProductID) REFERENCES Products(ProductID),
        CONSTRAINT FK_ProductAssociations_Consequent
        FOREIGN KEY (ConsequentProductID) REFERENCES Products(ProductID)
    );

    CREATE INDEX IX_ProductAssociations_Antecedent ON ProductAssociations(AntecedentProductID, Lift DESC);

    PRINT 'ProductAssociations table created successfully.';
END
ELSE
BEGIN
    PRINT 'ProductAssociations table already exists.';
END
GO
