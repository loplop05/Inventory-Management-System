using System;
using System.Collections.Generic;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsRefund
    {
        public static bool ProcessFullRefund(int orderID, string refundReason, string refundMethod, int processedBy, out int refundID, out string errorMessage)
        {
            refundID = -1;
            errorMessage = "";

            try
            {
                // Get order details
                var order = clsCustomer.GetOrderDetails(orderID);
                if (order == null || order.Rows.Count == 0)
                {
                    errorMessage = "Order not found.";
                    return false;
                }

                // Check if order is already refunded
                if (order.Rows[0]["RefundID"] != DBNull.Value)
                {
                    errorMessage = "Order has already been refunded.";
                    return false;
                }

                // Check if order is voided
                bool isVoided = order.Rows[0]["IsVoided"] != DBNull.Value && Convert.ToBoolean(order.Rows[0]["IsVoided"]);
                if (isVoided)
                {
                    errorMessage = "Cannot refund a voided order.";
                    return false;
                }

                decimal totalAmount = Convert.ToDecimal(order.Rows[0]["TotalAmount"]);

                // Create refund record
                var refund = new clsRefundData.RefundInfo
                {
                    OrderID = orderID,
                    RefundAmount = totalAmount,
                    RefundReason = refundReason,
                    RefundType = "Full",
                    RefundMethod = refundMethod,
                    ProcessedBy = processedBy
                };

                if (!clsRefundData.AddRefund(refund, out refundID, out errorMessage))
                {
                    return false;
                }

                // Get order items and create refund items
                var orderItems = clsCustomer.GetOrderItems(orderID);
                if (orderItems != null && orderItems.Rows.Count > 0)
                {
                    foreach (DataRow item in orderItems.Rows)
                    {
                        var refundItem = new clsRefundData.RefundItemInfo
                        {
                            RefundID = refundID,
                            ProductID = Convert.ToInt32(item["ProductID"]),
                            ProductName = item["ProductName"].ToString(),
                            Quantity = Convert.ToInt32(item["Quantity"]),
                            UnitPrice = Convert.ToDecimal(item["UnitPrice"]),
                            RefundAmount = Convert.ToDecimal(item["Subtotal"])
                        };

                        if (!clsRefundData.AddRefundItem(refundItem, out string itemError))
                        {
                            errorMessage = $"Failed to add refund item: {itemError}";
                            return false;
                        }
                    }
                }

                // Update order with refund ID
                if (!clsRefundData.UpdateOrderRefundID(orderID, refundID, out errorMessage))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool ProcessPartialRefund(int orderID, decimal refundAmount, List<clsRefundData.RefundItemInfo> refundItems, string refundReason, string refundMethod, int processedBy, out int refundID, out string errorMessage)
        {
            refundID = -1;
            errorMessage = "";

            try
            {
                // Get order details
                var order = clsCustomer.GetOrderDetails(orderID);
                if (order == null || order.Rows.Count == 0)
                {
                    errorMessage = "Order not found.";
                    return false;
                }

                decimal totalAmount = Convert.ToDecimal(order.Rows[0]["TotalAmount"]);
                if (refundAmount > totalAmount)
                {
                    errorMessage = "Refund amount cannot exceed order total.";
                    return false;
                }

                // Create refund record
                var refund = new clsRefundData.RefundInfo
                {
                    OrderID = orderID,
                    RefundAmount = refundAmount,
                    RefundReason = refundReason,
                    RefundType = "Partial",
                    RefundMethod = refundMethod,
                    ProcessedBy = processedBy
                };

                if (!clsRefundData.AddRefund(refund, out refundID, out errorMessage))
                {
                    return false;
                }

                // Add refund items
                foreach (var refundItem in refundItems)
                {
                    refundItem.RefundID = refundID;
                    if (!clsRefundData.AddRefundItem(refundItem, out string itemError))
                    {
                        errorMessage = $"Failed to add refund item: {itemError}";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool VoidRefund(int refundID, int voidedBy, string voidReason, out string errorMessage)
        {
            return clsRefundData.VoidRefund(refundID, voidedBy, voidReason, out errorMessage);
        }

        public static clsRefundData.RefundInfo GetRefundByID(int refundID, out string errorMessage)
        {
            return clsRefundData.GetRefundByID(refundID, out errorMessage);
        }

        public static List<clsRefundData.RefundItemInfo> GetRefundItems(int refundID, out string errorMessage)
        {
            return clsRefundData.GetRefundItems(refundID, out errorMessage);
        }

        public static List<clsRefundData.RefundInfo> GetRefundsByOrder(int orderID, out string errorMessage)
        {
            return clsRefundData.GetRefundsByOrder(orderID, out errorMessage);
        }

        public static List<clsRefundData.RefundInfo> GetAllRefunds(out string errorMessage)
        {
            return clsRefundData.GetAllRefunds(out errorMessage);
        }
    }
}
