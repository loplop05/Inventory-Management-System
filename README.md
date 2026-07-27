# Inventory Management System

A desktop **Inventory Management & Point of Sale (POS) System** built with **C# WinForms (.NET)**, using a **3-tier architecture** and **Microsoft SQL Server** for data storage. Designed to help small-to-medium retail businesses manage stock, process sales, print receipts, and generate reports — all from a single desktop application.

---

## ✨ Features

### 📦 Inventory Management
- Add, edit, and delete products
- Track stock levels and low-stock alerts
- Organize products by category
- Barcode support for fast product lookup

### 🛒 Point of Sale (POS)
- Fast checkout workflow for cashiers
- Search products by **name or barcode**
- Live cart management (add/remove items, adjust quantities)
- Multiple payment methods supported (Cash, Card, etc.)

### 🧾 Receipts
- Auto-generated receipts on checkout
- Receipt includes:
  - Order number
  - Date and time
  - Customer name & phone number
  - Payment method
  - Itemized list with quantity, unit price, and totals
- Search and reprint past receipts

### 📊 Reports & KPIs
- Sales reports with date filtering
- Key Performance Indicators (KPIs): total sales, order count, revenue trends
- Payment method breakdown in reports
- Visual, responsive report layout

---

## 🏗️ Architecture

This project follows a **3-tier architecture** to separate concerns and keep the codebase maintainable:

```
┌─────────────────────────┐
│   Presentation Layer     │  → WinForms UI (POS, Inventory, Reports, Receipts)
├─────────────────────────┤
│   Business Logic Layer   │  → Services, validation, calculations
├─────────────────────────┤
│   Data Access Layer      │  → SQL Server queries, repositories
└─────────────────────────┘
```

- **Presentation Layer** – WinForms screens for POS, inventory, receipts, and reports
- **Business Logic Layer** – Handles order processing, pricing, KPI calculations, and validation
- **Data Access Layer** – Manages all communication with the SQL Server database

---

## 🛠️ Tech Stack

| Component        | Technology              |
|-------------------|--------------------------|
| UI Framework       | WinForms (.NET)          |
| Language            | C#                       |
| Database            | Microsoft SQL Server     |
| Architecture         | 3-Tier (Presentation / Business / Data) |

---

## 🚀 Getting Started

### Prerequisites
- Windows OS
- [.NET SDK](https://dotnet.microsoft.com/download) (matching the project's target framework)
- SQL Server (Express or full edition)
- Visual Studio 2022 (recommended)

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/loplop05/Inventory-Management-System.git
   ```
2. Open the solution file (`.sln`) in Visual Studio
3. Update the database connection string in the config file to point to your local SQL Server instance
4. Run the SQL scripts (if provided) to create the database schema
5. Build and run the project (`F5` in Visual Studio)

---

## 📁 Project Structure

```
Inventory-Management-System/
├── PresentationLayer/     # WinForms - POS, Inventory, Reports, Receipts
├── BusinessLogicLayer/    # Services and business rules
├── DataAccessLayer/       # Database access and repositories
└── README.md
```

---

## 📌 Roadmap

- [ ] Improve receipt printing for multi-page orders
- [ ] Add barcode scanner hardware integration
- [ ] Export reports to PDF/Excel
- [ ] Add user roles and authentication (Admin/Cashier)
- [ ] Add sales analytics dashboard

---

## 👤 Author

**Ammar** — AI & Data Science student, Al-Zaytoonah University of Jordan
GitHub: [@loplop05](https://github.com/loplop05)

---

## 📄 License

This project is currently unlicensed. Add a license file if you plan to distribute or open-source this project.
