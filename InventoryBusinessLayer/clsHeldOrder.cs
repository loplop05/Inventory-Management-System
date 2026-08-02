using System;
using System.Collections.Generic;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsHeldOrder
    {
        public static int SaveHeldOrder(clsHeldOrderData.HeldOrderInfo heldOrder, out string errorMessage)
        {
            if (heldOrder == null)
            {
                errorMessage = "Held order information is required.";
                return -1;
            }

            if (heldOrder.Items == null || heldOrder.Items.Count == 0)
            {
                errorMessage = "Held order must contain at least one item.";
                return -1;
            }

            return clsHeldOrderData.SaveHeldOrder(heldOrder, out errorMessage);
        }

        public static clsHeldOrderData.HeldOrderInfo GetHeldOrder(int heldOrderID, out string errorMessage)
        {
            return clsHeldOrderData.GetHeldOrder(heldOrderID, out errorMessage);
        }

        public static DataTable GetAllHeldOrders(out string errorMessage)
        {
            return clsHeldOrderData.GetAllHeldOrders(out errorMessage);
        }

        public static bool DeleteHeldOrder(int heldOrderID, out string errorMessage)
        {
            return clsHeldOrderData.DeleteHeldOrder(heldOrderID, out errorMessage);
        }
    }
}
