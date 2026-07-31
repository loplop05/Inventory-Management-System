# Multi-Branch/Register Awareness - Design Note

## Overview
This design note outlines considerations for extending the Inventory Management System to support multiple branches and registers.

## Current State
The system currently operates as a single-branch, single-register POS system:
- Orders are stored without branch/register identification
- Inventory is shared across all operations
- Reports aggregate all sales data
- No concept of cash drawer management per register

## Proposed Architecture

### Database Schema Changes

#### 1. Branches Table
```sql
CREATE TABLE Branches (
    BranchID INT IDENTITY(1,1) PRIMARY KEY,
    BranchName NVARCHAR(100) NOT NULL,
    BranchCode NVARCHAR(20) UNIQUE NOT NULL,
    Address NVARCHAR(200),
    Phone NVARCHAR(20),
    ManagerID INT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
```

#### 2. Registers Table
```sql
CREATE TABLE Registers (
    RegisterID INT IDENTITY(1,1) PRIMARY KEY,
    BranchID INT NOT NULL,
    RegisterName NVARCHAR(50) NOT NULL,
    RegisterCode NVARCHAR(20) UNIQUE NOT NULL,
    AssignedUserID INT NULL,
    CurrentCashDrawerBalance DECIMAL(10,2) NOT NULL DEFAULT 0,
    LastOpenedDate DATETIME NULL,
    LastClosedDate DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Registers_Branches FOREIGN KEY (BranchID) REFERENCES Branches(BranchID)
);
```

#### 3. RegisterSessions Table
```sql
CREATE TABLE RegisterSessions (
    SessionID INT IDENTITY(1,1) PRIMARY KEY,
    RegisterID INT NOT NULL,
    UserID INT NOT NULL,
    OpeningBalance DECIMAL(10,2) NOT NULL DEFAULT 0,
    ClosingBalance DECIMAL(10,2) NULL,
    ExpectedClosingBalance DECIMAL(10,2) NULL,
    Variance DECIMAL(10,2) NULL,
    OpenedAt DATETIME NOT NULL DEFAULT GETDATE(),
    ClosedAt DATETIME NULL,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT FK_RegisterSessions_Registers FOREIGN KEY (RegisterID) REFERENCES Registers(RegisterID)
);
```

#### 4. Orders Table Modifications
```sql
ALTER TABLE Orders ADD
    BranchID INT NULL,
    RegisterID INT NULL,
    SessionID INT NULL,
    CONSTRAINT FK_Orders_Branches FOREIGN KEY (BranchID) REFERENCES Branches(BranchID),
    CONSTRAINT FK_Orders_Registers FOREIGN KEY (RegisterID) REFERENCES Registers(RegisterID),
    CONSTRAINT FK_Orders_Sessions FOREIGN KEY (SessionID) REFERENCES RegisterSessions(SessionID);
```

### Business Logic Changes

#### 1. Branch Selection
- Add branch selection at login
- Store current branch in session/context
- Filter products/inventory by branch (if branch-specific inventory is needed)

#### 2. Register Management
- Add register selection at POS login
- Implement register open/close procedures
- Track cash drawer balance per session
- Handle register transfers between users

#### 3. Order Processing
- Associate each order with branch, register, and session
- Validate register is open before allowing sales
- Track cash payments against register balance

#### 4. Reporting
- Add branch-level reports
- Add register-level reports
- Add session-level close-out reports
- Consolidate multi-branch reports for headquarters view

### UI Changes

#### 1. Login Flow
```
Current: Username/Password → Main Menu
Proposed: Username/Password → Branch Selection → Register Selection → Main Menu
```

#### 2. POS Interface
- Display current branch/register in header
- Add "Open/Close Register" functionality
- Show current cash drawer balance
- Add session summary before closing

#### 3. Reports
- Add branch/register filters to all reports
- Add "Register Close-out" report
- Add "Multi-Branch Consolidation" report

### Inventory Considerations

#### Option A: Shared Inventory
- All branches share the same inventory pool
- Simpler implementation
- May not reflect physical reality

#### Option B: Branch-Specific Inventory
- Each branch has its own inventory
- Requires inventory transfer functionality
- More complex but more accurate

**Recommendation:** Start with Option A (Shared Inventory) for simplicity, with Option B as a future enhancement.

### Security Considerations

1. **User Permissions**
   - Branch-level access control
   - Register assignment restrictions
   - Cross-branch reporting permissions

2. **Audit Trail**
   - Track which user opened/closed each register
   - Log all register transfers
   - Track inventory movements between branches

### Implementation Phases

#### Phase 1: Foundation (Low Effort)
- Add Branches and Registers tables
- Implement branch/register selection in login
- Add branch/register fields to Orders table
- Update reporting to filter by branch/register

#### Phase 2: Register Management (Medium Effort)
- Implement register open/close procedures
- Add RegisterSessions tracking
- Implement cash drawer balance management
- Add session close-out reports

#### Phase 3: Advanced Features (High Effort)
- Branch-specific inventory (if needed)
- Inventory transfer between branches
- Multi-branch consolidation reports
- Advanced security and permissions

### Migration Strategy

1. Create a default branch (e.g., "Main Branch")
2. Create a default register (e.g., "Register 1")
3. Backfill existing orders with default branch/register
4. Gradually roll out to users with training

### Risks and Mitigations

| Risk | Mitigation |
|------|------------|
| Data inconsistency during migration | Perform migration during off-hours, backup database |
| User resistance to new login flow | Provide clear training and documentation |
| Register balance discrepancies | Implement variance tracking and reconciliation procedures |
| Performance impact of additional joins | Add appropriate indexes on new foreign keys |

### Estimated Effort

- **Phase 1:** 2-3 days
- **Phase 2:** 3-4 days
- **Phase 3:** 5-7 days

### Conclusion

Multi-branch/register awareness is a significant enhancement that would make the system suitable for larger retail operations. The phased approach allows for incremental implementation and testing. Starting with shared inventory reduces initial complexity while still providing the core multi-branch functionality.
