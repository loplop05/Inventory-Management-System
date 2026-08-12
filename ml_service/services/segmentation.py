import pandas as pd
from datetime import datetime, timedelta
from db import get_connection

try:
    from sklearn.cluster import KMeans
    from sklearn.preprocessing import StandardScaler
    SKLEARN_AVAILABLE = True
except ImportError:
    SKLEARN_AVAILABLE = False

N_CLUSTERS = 4

def run():
    """Run K-Means customer segmentation and write results to CustomerSegments table."""
    if not SKLEARN_AVAILABLE:
        print("scikit-learn not available, skipping segmentation.")
        return 0
    
    conn = get_connection()
    cursor = conn.cursor()
    
    # Calculate RFM metrics per customer
    query = """
    SELECT 
        c.CustomerID,
        c.CustomerName,
        MAX(o.OrderDate) AS LastPurchaseDate,
        COUNT(DISTINCT o.OrderID) AS Frequency,
        SUM(o.TotalAmount) AS Monetary
    FROM Customers c
    LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
    GROUP BY c.CustomerID, c.CustomerName
    """
    
    df = pd.read_sql(query, conn)
    
    if df.empty:
        print("No customer data found for segmentation.")
        return 0
    
    # Filter out customers with no orders (they can't be segmented)
    df = df[df['LastPurchaseDate'].notna()]
    
    if df.empty:
        print("No customers with purchase history for segmentation.")
        return 0
    
    # Calculate Recency (days since last purchase)
    today = datetime.now()
    df['Recency'] = (today - pd.to_datetime(df['LastPurchaseDate'])).dt.days
    
    # Prepare features for clustering
    features = df[['Recency', 'Frequency', 'Monetary']].copy()
    
    # Handle missing values
    features = features.fillna(0)
    
    # Scale features
    scaler = StandardScaler()
    features_scaled = scaler.fit_transform(features)
    
    # Run K-Means
    try:
        kmeans = KMeans(n_clusters=N_CLUSTERS, random_state=42, n_init=10)
        df['ClusterID'] = kmeans.fit_predict(features_scaled)
        
        # Assign segment labels based on cluster characteristics
        df['SegmentLabel'] = df.apply(assign_segment_label, axis=1)
        
        # Clear existing segments
        cursor.execute("TRUNCATE TABLE CustomerSegments")
        
        # Insert segments
        rows_written = 0
        for _, row in df.iterrows():
            cursor.execute("""
                INSERT INTO CustomerSegments 
                (CustomerID, SegmentLabel, RecencyScore, FrequencyScore, MonetaryScore, ClusterID)
                VALUES (?, ?, ?, ?, ?, ?)
            """, (
                int(row['CustomerID']),
                str(row['SegmentLabel']),
                int(row['Recency']),
                int(row['Frequency']),
                float(row['Monetary']),
                int(row['ClusterID'])
            ))
            rows_written += 1
        
        conn.commit()
        conn.close()
        return rows_written
        
    except Exception as e:
        print(f"Error in customer segmentation: {e}")
        conn.close()
        return 0

def assign_segment_label(row):
    """Assign segment label based on RFM scores."""
    recency = row['Recency']
    frequency = row['Frequency']
    monetary = row['Monetary']
    
    # Simple RFM-based labeling
    if frequency >= 10 and monetary >= 1000 and recency <= 30:
        return "Champions"
    elif frequency >= 5 and monetary >= 500 and recency <= 90:
        return "Loyal Customers"
    elif frequency >= 2 and recency <= 180:
        return "Potential Loyalists"
    elif recency > 180:
        return "At Risk"
    elif frequency == 1:
        return "New Customers"
    else:
        return "Others"
