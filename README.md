# Inventory Management System

A desktop Inventory Management and Point of Sale (POS) system built with C# WinForms (.NET Framework), using a 3-tier architecture and Microsoft SQL Server. It also includes an optional Python ML microservice for sales forecasting, product associations, and customer segmentation.

## Features

- Product, category, and supplier management
- Point of sale checkout with barcode and name search
- Stock tracking, low-stock alerts, and stock adjustments
- Customer management and loyalty/points program
- Shift and cash drawer management
- Refunds, exchanges, held orders, and split payments
- Discounts and coupons
- Receipts with search and reprint
- Sales, KPI, and stock valuation reports
- User accounts with roles and permissions, plus audit logging
- Multi-branch support
- ML-powered dashboard: sales forecasting, product associations, and customer segmentation

## Architecture

The project follows a 3-tier architecture:

- **Presentation Layer** — WinForms UI (Forms, POS, dashboard, reports)
- **Business Layer** (`InventoryBusinessLayer`) — Business rules, validation, calculations
- **Data Access Layer** (`InventoryDataAccessLayer`) — SQL Server queries via Dapper

A separate Flask-based ML service (`ml_service`) handles forecasting, association rules, and segmentation, and writes results back to dedicated SQL tables.

## Tech Stack

| Component      | Technology                          |
|----------------|--------------------------------------|
| UI Framework   | WinForms (.NET Framework)            |
| Language       | C#                                   |
| Database       | Microsoft SQL Server                 |
| Data Access    | Dapper                               |
| ML Service     | Python, Flask                        |
| Architecture   | 3-Tier (Presentation / Business / Data Access) |

## Project Structure

```
Inventory-Management-System/
├── Forms/                     # WinForms screens (POS, inventory, reports, etc.)
├── Helpers/                   # Theming, printing, notifications, and utilities
├── InventoryBusinessLayer/    # Business logic and validation
├── InventoryDataAccessLayer/  # Database access
├── Database/Seed/             # Sample data scripts
├── DatabaseMigration_*.sql    # Database migration scripts
├── InventoryDB.sql            # Base database schema
├── ml_service/                # Python Flask ML microservice
└── README.md
```

## Getting Started

### Prerequisites

- Windows OS
- Visual Studio 2022 (recommended)
- .NET Framework SDK (matching the project's target framework)
- SQL Server (Express or full edition)
- Python 3.9+ (only needed for the ML service)

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/loplop05/Inventory-Management-System.git
   ```
2. Open `InventoryManagementSystem.slnx` in Visual Studio
3. Create your local database connection settings:
   - Copy `App.config.local.example` to `App.config.local`
   - Set your SQL Server credentials in the connection string
4. Run `InventoryDB.sql` to create the database schema
5. Run the `DatabaseMigration_*.sql` scripts to apply the remaining schema updates
6. (Optional) Run the seed scripts in `Database/Seed` for sample data
7. Build and run the project (F5 in Visual Studio)

### ML Service (optional)

The ML service powers the dashboard's forecasting, associations, and segmentation features.

```bash
cd ml_service
pip install -r requirements.txt
python app.py
```

By default it connects to `localhost/InventoryDB` and runs on `http://localhost:5055`. See `ml_service/README.md` for configuration and API details.

## Author

Ammar — AI & Data Science student, Al-Zaytoonah University of Jordan
GitHub: [@loplop05](https://github.com/loplop05)

## License

This project is currently unlicensed. Add a license file if you plan to distribute or open-source this project.
