# ML Service

Python Flask service for machine learning features in the Inventory Management System.

## Features
- Sales Forecasting (Prophet/Linear Regression)
- Product Associations (FP-Growth/Apriori)
- Customer Segmentation (K-Means RFM)

## Setup

1. Install dependencies:
```bash
pip install -r requirements.txt
```

2. Configure database connection (optional - uses localhost/InventoryDB with trusted connection by default):
```bash
set ML_DB_SERVER=localhost
set ML_DB_NAME=InventoryDB
set ML_DB_USER=
set ML_DB_PASSWORD=
set ML_SERVICE_PORT=5055
```

3. Run the service:
```bash
python app.py
```

The service will start on http://localhost:5055

## API Endpoints

- `POST /train/forecast` - Train sales forecasting model
- `POST /train/associations` - Train product association rules
- `POST /train/segments` - Train customer segmentation
- `GET /health` - Health check endpoint

All training endpoints return:
```json
{"status": "ok", "rows_written": N}
```
