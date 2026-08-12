import pandas as pd
from datetime import datetime, timedelta
from db import get_connection

try:
    from prophet import Prophet
    PROPHET_AVAILABLE = True
except ImportError:
    PROPHET_AVAILABLE = False

from sklearn.linear_model import LinearRegression
import numpy as np

MIN_DAYS_HISTORY = 14
FORECAST_DAYS = 14

def run():
    """Run sales forecasting model and write results to ProductForecasts table."""
    conn = get_connection()
    cursor = conn.cursor()
    
    # Read daily sales per product
    query = """
    SELECT 
        oi.ProductID,
        CAST(o.OrderDate AS DATE) AS SaleDate,
        SUM(oi.Quantity) AS DailyQty
    FROM OrderItems oi
    INNER JOIN Orders o ON oi.OrderID = o.OrderID
    GROUP BY oi.ProductID, CAST(o.OrderDate AS DATE)
    ORDER BY oi.ProductID, SaleDate
    """
    
    df = pd.read_sql(query, conn)
    
    if df.empty:
        print("No order data found for forecasting.")
        return 0
    
    total_rows_written = 0
    
    # Process each product
    for product_id in df['ProductID'].unique():
        product_df = df[df['ProductID'] == product_id].copy()
        product_df = product_df.sort_values('SaleDate')
        
        # Skip products with insufficient history
        if len(product_df) < MIN_DAYS_HISTORY:
            continue
        
        # Prepare data for Prophet/Linear
        product_df['ds'] = pd.to_datetime(product_df['SaleDate'])
        product_df['y'] = product_df['DailyQty']
        
        # Delete existing forecasts for this product
        cursor.execute("DELETE FROM ProductForecasts WHERE ProductID = ?", product_id)
        
        # Fit model and generate forecast
        try:
            if PROPHET_AVAILABLE:
                forecast_rows = _forecast_with_prophet(product_df, product_id)
            else:
                forecast_rows = _forecast_with_linear(product_df, product_id)
            
            # Insert forecasts
            for row in forecast_rows:
                cursor.execute("""
                    INSERT INTO ProductForecasts (ProductID, ForecastDate, PredictedQty, LowerBound, UpperBound, ModelType)
                    VALUES (?, ?, ?, ?, ?, ?)
                """, row)
                total_rows_written += 1
                
        except Exception as e:
            print(f"Error forecasting product {product_id}: {e}")
            continue
    
    conn.commit()
    conn.close()
    return total_rows_written

def _forecast_with_prophet(df, product_id):
    """Forecast using Prophet."""
    model = Prophet(daily_seasonality=False, weekly_seasonality=True)
    model.fit(df)
    
    future = model.make_future_dataframe(periods=FORECAST_DAYS)
    forecast = model.predict(future)
    
    # Get only future forecasts
    last_date = df['ds'].max()
    forecast_future = forecast[forecast['ds'] > last_date]
    
    rows = []
    for _, row in forecast_future.iterrows():
        rows.append((
            product_id,
            row['ds'].date(),
            max(0, row['yhat']),  # Ensure non-negative
            max(0, row['yhat_lower']),
            max(0, row['yhat_upper']),
            'prophet'
        ))
    
    return rows

def _forecast_with_linear(df, product_id):
    """Forecast using linear regression as fallback."""
    df['days_since_start'] = (df['ds'] - df['ds'].min()).dt.days
    
    model = LinearRegression()
    model.fit(df[['days_since_start']], df['y'])
    
    # Generate future dates
    last_date = df['ds'].max()
    future_dates = [last_date + timedelta(days=i) for i in range(1, FORECAST_DAYS + 1)]
    
    rows = []
    for future_date in future_dates:
        days_since_start = (future_date - df['ds'].min()).days
        predicted = model.predict([[days_since_start]])[0]
        
        # Simple bounds: +/- 20%
        predicted = max(0, predicted)
        lower = max(0, predicted * 0.8)
        upper = predicted * 1.2
        
        rows.append((
            product_id,
            future_date.date(),
            predicted,
            lower,
            upper,
            'linear_fallback'
        ))
    
    return rows
