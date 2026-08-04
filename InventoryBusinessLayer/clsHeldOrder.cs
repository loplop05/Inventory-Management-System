using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public static int SaveHeldOrder(int? customerID, DataTable orderItems, decimal totalAmount, string notes, out string errorMessage)
        {
            errorMessage = "";

            if (orderItems == null || orderItems.Rows.Count == 0)
            {
                errorMessage = "Order must contain at least one item.";
                return -1;
            }

            // Build HeldOrderInfo from parameters
            var heldOrder = new clsHeldOrderData.HeldOrderInfo
            {
                CustomerID = customerID,
                Notes = notes,
                CreatedDate = DateTime.Now,
                Items = new List<clsHeldOrderData.HeldOrderItemInfo>()
            };

            // Convert DataTable items to HeldOrderItemInfo
            foreach (DataRow row in orderItems.Rows)
            {
                int productID = Convert.ToInt32(row["ProductID"]);
                string productName = row["ProductName"].ToString();
                int quantity = Convert.ToInt32(row["Quantity"]);
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                decimal subtotal = quantity * unitPrice;

                heldOrder.Items.Add(new clsHeldOrderData.HeldOrderItemInfo
                {
                    ProductID = productID,
                    ProductName = productName,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    Subtotal = subtotal
                });
            }

            return SaveHeldOrder(heldOrder, out errorMessage);
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
