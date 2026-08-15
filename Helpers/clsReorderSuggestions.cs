using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Low-stock reorder suggestion system for inventory management.
    /// Generates purchase order suggestions based on configurable reorder thresholds.
    /// </summary>
    public static class clsReorderSuggestions
    {
        public class ReorderItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string CategoryName { get; set; }
            public string SupplierName { get; set; }
            public int CurrentStock { get; set; }
            public int ReorderThreshold { get; set; }
            public int SuggestedOrderQty { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal SuggestedOrderCost => SuggestedOrderQty * UnitPrice;
        }

        private const int DefaultReorderThreshold = 10;
        private const int DefaultSuggestedOrderQty = 20;

        /// <summary>
        /// Gets all products that need reordering based on stock levels.
        /// </summary>
        public static List<ReorderItem> GetReorderSuggestions(int? customThreshold = null)
        {
            List<ReorderItem> suggestions = new List<ReorderItem>();
            string errorMessage;
            DataTable products;
            if (!clsProduct.GetAllProducts(out products, out errorMessage))
            {
                return suggestions;
            }

            if (products == null) return suggestions;

            int threshold = customThreshold ?? DefaultReorderThreshold;

            foreach (DataRow row in products.Rows)
            {
                int currentStock = row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 0;
                string productName = row["ProductName"].ToString();
                string categoryName = row["CategoryName"] != DBNull.Value ? row["CategoryName"].ToString() : "";
                string supplierName = row["SupplierName"] != DBNull.Value ? row["SupplierName"].ToString() : "";
                decimal unitPrice = row["Price"] != DBNull.Value ? Convert.ToDecimal(row["Price"]) : 0;

                if (currentStock <= threshold)
                {
                    suggestions.Add(new ReorderItem
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = productName,
                        CategoryName = categoryName,
                        SupplierName = supplierName,
                        CurrentStock = currentStock,
                        ReorderThreshold = threshold,
                        SuggestedOrderQty = DefaultSuggestedOrderQty,
                        UnitPrice = unitPrice
                    });
                }
            }

            return suggestions.OrderBy(s => s.CategoryName).ThenBy(s => s.ProductName).ToList();
        }

        /// <summary>
        /// Groups reorder suggestions by supplier for purchase order generation.
        /// </summary>
        public static Dictionary<string, List<ReorderItem>> GetReorderSuggestionsBySupplier(int? customThreshold = null)
        {
            var suggestions = GetReorderSuggestions(customThreshold);
            return suggestions.GroupBy(s => s.SupplierName)
                             .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Calculates total cost for all reorder suggestions.
        /// </summary>
        public static decimal CalculateTotalReorderCost(List<ReorderItem> suggestions)
        {
            return suggestions.Sum(s => s.SuggestedOrderCost);
        }
    }
}
