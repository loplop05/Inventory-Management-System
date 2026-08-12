import pyodbc
from config import DB_SERVER, DB_NAME, DB_USER, DB_PASSWORD

def get_connection():
    """Get a pyodbc connection to the SQL Server database."""
    if DB_USER:
        conn_string = f"DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={DB_SERVER};DATABASE={DB_NAME};UID={DB_USER};PWD={DB_PASSWORD}"
    else:
        conn_string = f"DRIVER={{ODBC Driver 17 for SQL Server}};SERVER={DB_SERVER};DATABASE={DB_NAME};Trusted_Connection=yes"
    
    return pyodbc.connect(conn_string)
