-- =====================================================
-- Database Migration: Add Manager Role to Users Table
-- =====================================================
-- This migration adds the Manager role to the Users table
-- by updating the Role CHECK constraint to include 'Manager'
-- =====================================================

-- Step 1: Find and drop the existing Role constraint
DECLARE @ConstraintName NVARCHAR(128)
SELECT @ConstraintName = name 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('Users') 
AND definition LIKE '%Role%'

IF @ConstraintName IS NOT NULL
BEGIN
    DECLARE @DropSQL NVARCHAR(500)
    SET @DropSQL = 'ALTER TABLE Users DROP CONSTRAINT ' + QUOTENAME(@ConstraintName)
    EXEC sp_executesql @DropSQL
    PRINT 'Dropped existing constraint: ' + @ConstraintName
END
ELSE
BEGIN
    PRINT 'No existing Role constraint found on Users table'
END
GO

-- Step 2: Add the new constraint with Manager role included
ALTER TABLE Users 
ADD CONSTRAINT CK_Users_Role 
CHECK (Role IN ('Admin', 'Manager', 'Cashier'))
GO

PRINT 'Successfully added Manager role to Users table constraint'
GO

-- Step 3: Verify the constraint was added correctly
SELECT 
    name AS ConstraintName,
    definition AS ConstraintDefinition
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('Users') 
AND name = 'CK_Users_Role'
GO
