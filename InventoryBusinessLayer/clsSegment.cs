using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public class clsSegment
    {
        public static DataTable GetSegmentSummary(out string errorMessage)
        {
            return clsSegmentData.GetSegmentSummary(out errorMessage);
        }

        public static DataTable GetSegmentsForCustomer(int customerId, out string errorMessage)
        {
            if (customerId <= 0)
            {
                errorMessage = "Invalid Customer ID.";
                return null;
            }

            return clsSegmentData.GetSegmentsForCustomer(customerId, out errorMessage);
        }

        public static DataTable GetAllSegments(out string errorMessage)
        {
            return clsSegmentData.GetAllSegments(out errorMessage);
        }
    }
}
