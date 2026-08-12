import os

DB_SERVER = os.environ.get("ML_DB_SERVER", "localhost")
DB_NAME = os.environ.get("ML_DB_NAME", "InventoryDB")
DB_USER = os.environ.get("ML_DB_USER", "")       # empty = use trusted connection
DB_PASSWORD = os.environ.get("ML_DB_PASSWORD", "")
FLASK_PORT = int(os.environ.get("ML_SERVICE_PORT", "5055"))
