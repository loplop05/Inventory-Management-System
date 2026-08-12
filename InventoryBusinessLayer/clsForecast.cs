using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public class clsForecast
    {
        public static DataTable GetForecastsForProduct(int productId, out string errorMessage)
        {
            if (productId <= 0)
            {
                errorMessage = "Invalid Product ID.";
                return null;
            }

            return clsForecastData.GetForecastsForProduct(productId, out errorMessage);
        }

        public static DataTable GetForecastSummary(out string errorMessage)
        {
            return clsForecastData.GetForecastSummary(out errorMessage);
        }

        public static DataTable GetNext7DayForecastSummary(out string errorMessage)
        {
            return clsForecastData.GetNext7DayForecastSummary(out errorMessage);
        }
    }
}
