import pandas as pd
from db import get_connection

try:
    from mlxtend.frequent_patterns import fpgrowth
    from mlxtend.frequent_patterns import association_rules
    MLXTEND_AVAILABLE = True
except ImportError:
    MLXTEND_AVAILABLE = False

MIN_SUPPORT = 0.01
MIN_LIFT = 1.0
MAX_RULES = 200

def run():
    """Run market basket analysis and write results to ProductAssociations table."""
    if not MLXTEND_AVAILABLE:
        print("mlxtend not available, skipping association mining.")
        return 0
    
    conn = get_connection()
    cursor = conn.cursor()
    
    # Build basket DataFrame: one row per OrderID, one column per ProductID
    query = """
    SELECT 
        OrderID,
        ProductID
    FROM OrderItems
    """
    
    df = pd.read_sql(query, conn)
    
    if df.empty:
        print("No order items found for association mining.")
        return 0
    
    # Create basket matrix
    basket = df.groupby(['OrderID', 'ProductID']).size().unstack(fill_value=0)
    basket = basket.applymap(lambda x: 1 if x > 0 else 0)
    
    if basket.empty or basket.shape[1] < 2:
        print("Not enough products for association mining.")
        return 0
    
    # Run FP-Growth
    try:
        frequent_itemsets = fpgrowth(basket, min_support=MIN_SUPPORT, use_colnames=True)
        
        if frequent_itemsets.empty:
            print("No frequent itemsets found with current support threshold.")
            return 0
        
        # Generate association rules
        rules = association_rules(frequent_itemsets, metric="lift", min_threshold=MIN_LIFT)
        
        if rules.empty:
            print("No association rules found with current lift threshold.")
            return 0
        
        # Filter by lift > 1 and cap at top rules
        rules = rules[rules['lift'] > MIN_LIFT]
        rules = rules.sort_values('lift', ascending=False).head(MAX_RULES)
        
        # Clear existing associations
        cursor.execute("TRUNCATE TABLE ProductAssociations")
        
        # Insert rules (explode multi-item antecedents into individual rows)
        rows_written = 0
        for _, rule in rules.iterrows():
            antecedents = rule['antecedents']
            consequents = rule['consequents']
            
            # Handle frozensets
            if isinstance(antecedents, frozenset):
                antecedents = list(antecedents)
            else:
                antecedents = [antecedents]
            
            if isinstance(consequents, frozenset):
                consequents = list(consequents)
            else:
                consequents = [consequents]
            
            # Create individual antecedent → consequent pairs
            for antecedent in antecedents:
                for consequent in consequents:
                    if antecedent != consequent:  # Don't associate product with itself
                        cursor.execute("""
                            INSERT INTO ProductAssociations 
                            (AntecedentProductID, ConsequentProductID, Support, Confidence, Lift)
                            VALUES (?, ?, ?, ?, ?)
                        """, (
                            int(antecedent),
                            int(consequent),
                            float(rule['support']),
                            float(rule['confidence']),
                            float(rule['lift'])
                        ))
                        rows_written += 1
        
        conn.commit()
        conn.close()
        return rows_written
        
    except Exception as e:
        print(f"Error in association mining: {e}")
        conn.close()
        return 0
