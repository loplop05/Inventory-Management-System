using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public class clsAssociation
    {
        public static DataTable GetSuggestionsForProduct(int productId, int topN, out string errorMessage)
        {
            if (productId <= 0)
            {
                errorMessage = "Invalid Product ID.";
                return null;
            }

            if (topN <= 0)
            {
                topN = 5; // Default to 5 suggestions
            }

            return clsAssociationData.GetSuggestionsForProduct(productId, topN, out errorMessage);
        }
    }
}
