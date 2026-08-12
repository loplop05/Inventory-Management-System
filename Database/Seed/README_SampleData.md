# Sample Data for ML Services

This directory contains SQL scripts to populate the database with sample data for testing and demonstrating the ML services (Sales Forecasting, Customer Segmentation, and Product Associations).

## Scripts

### 1. SampleCustomers.sql
Inserts sample customer records with varied purchase patterns to support RFM-based customer segmentation.

**Customer Segments Included:**
- **Champions (5 customers):** High-value frequent customers with recent purchases
- **Loyal (5 customers):** Regular customers with consistent but lower spending
- **Potential Loyalists (5 customers):** Recent customers showing good potential
- **New (5 customers):** Recently signed up with minimal purchase history
- **At-Risk (5 customers):** Previously active but haven't purchased recently
- **Hibernating (5 customers):** Very infrequent, low spend customers
- **Lost (5 customers):** Very old customers with no recent activity
- **Additional (10 customers):** Diverse customers for better segmentation

**Total Customers:** 50

### 2. SampleSales.sql
Inserts sample orders and order items to support:
- Sales Forecasting (time series data by date)
- Customer Segmentation (RFM analysis)
- Product Associations (basket analysis)

**Data Patterns Included:**
- High-value purchases for champion customers
- Regular purchases for loyal customers
- Recent purchases for potential loyalists
- Historical purchases for at-risk/lost customers
- Product bundles for association analysis (MacBook + Accessories, iPhone + AirPods, etc.)
- Daily sales data for the last 90 days for forecasting

**Total Orders:** ~100+ orders with varied patterns
**Total Order Items:** ~200+ items

## Prerequisites

Before running these scripts, ensure:
1. The database `InventoryDB` exists
2. The following tables exist and are properly structured:
   - `Customers` (from `DatabaseMigration_CustomersAndPayments.sql`)
   - `Orders` (from `InventoryDB.sql` + migrations)
   - `OrderItems` (from `InventoryDB.sql`)
   - `Products` (from `seed_products_fill_categories.sql`)
3. Product data is populated (run `seed_products_fill_categories.sql` first)

## Execution Order

Run the scripts in this order:

1. **Database Setup** (if not already done)
   ```bash
   sqlcmd -S .\SQLEXPRESS -i InventoryDB.sql
   sqlcmd -S .\SQLEXPRESS -i DatabaseMigration_CustomersAndPayments.sql
   sqlcmd -S .\SQLEXPRESS -i Database\Seed\seed_products_fill_categories.sql
   ```

2. **Sample Data**
   ```bash
   sqlcmd -S .\SQLEXPRESS -i Database\Seed\SampleCustomers.sql
   sqlcmd -S .\SQLEXPRESS -i Database\Seed\SampleSales.sql
   ```

## Using with ML Services

After populating the sample data:

### Sales Forecasting
- Navigate to the **Sales** section in the dashboard
- Click "Run Forecast" to train the forecasting model
- The model will use the last 90 days of sales data
- View forecast results in the forecast grid

### Customer Segmentation
- Navigate to the **Customers** section in the dashboard
- Click "Run Segmentation" to train the segmentation model
- The model will analyze RFM (Recency, Frequency, Monetary) scores
- View segment distribution in the segmentation grid

### Product Associations
- Navigate to the **Inventory** section in the dashboard
- View top product associations by lift score
- Note: Association training is typically run after sufficient order history accumulates

## Verification Queries

Each script includes verification queries at the end:

**SampleCustomers.sql:**
- Total customer count

**SampleSales.sql:**
- Sales by date (last 7 days)
- Customer purchase summary
- Top selling products

## Notes

- Scripts include checks to prevent duplicate data insertion
- All dates are relative to `GETDATE()` for realistic time-series data
- Product IDs reference the products from `seed_products_fill_categories.sql`
- Customer IDs are sequential starting from 1
- Payment methods include Cash, Visa, and MasterCard

## Troubleshooting

**Error: "Sample data already exists"**
- The scripts check for existing data before insertion
- To reset, manually delete the sample data or truncate the tables

**Error: "Foreign key constraint"**
- Ensure `Products` table is populated first
- Run `seed_products_fill_categories.sql` before sample data scripts

**Error: "Invalid column name"**
- Ensure all database migrations are applied
- Run migration scripts in order before sample data
