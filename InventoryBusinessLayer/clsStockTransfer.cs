using System;
using System.Collections.Generic;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsStockTransfer
    {
        public static bool CreateStockTransfer(clsStockTransferData.StockTransferInfo transfer, List<clsStockTransferData.StockTransferItemInfo> items, out int transferID, out string errorMessage)
        {
            transferID = -1;
            errorMessage = "";

            try
            {
                // Validate branches are different
                if (transfer.FromBranchID == transfer.ToBranchID)
                {
                    errorMessage = "Source and destination branches must be different.";
                    return false;
                }

                // Validate branches exist
                var fromBranch = clsBranchData.GetBranchByID(transfer.FromBranchID, out string fromError);
                var toBranch = clsBranchData.GetBranchByID(transfer.ToBranchID, out string toError);

                if (fromBranch == null)
                {
                    errorMessage = "Source branch not found.";
                    return false;
                }
                if (toBranch == null)
                {
                    errorMessage = "Destination branch not found.";
                    return false;
                }

                // Create stock transfer
                if (!clsStockTransferData.AddStockTransfer(transfer, out transferID, out errorMessage))
                {
                    return false;
                }

                // Add transfer items
                foreach (var item in items)
                {
                    item.TransferID = transferID;
                    if (!clsStockTransferData.AddStockTransferItem(item, out string itemError))
                    {
                        errorMessage = $"Failed to add transfer item: {itemError}";
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

        public static bool ApproveTransfer(int transferID, int approvedBy, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Check if transfer exists and is pending
                var transfer = clsStockTransferData.GetStockTransferByID(transferID, out string getError);
                if (transfer == null)
                {
                    errorMessage = "Transfer not found.";
                    return false;
                }

                if (transfer.TransferStatus != "Pending")
                {
                    errorMessage = "Transfer can only be approved when status is Pending.";
                    return false;
                }

                return clsStockTransferData.UpdateTransferStatus(transferID, "Approved", approvedBy, null, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool CompleteTransfer(int transferID, int completedBy, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Check if transfer exists and is approved
                var transfer = clsStockTransferData.GetStockTransferByID(transferID, out string getError);
                if (transfer == null)
                {
                    errorMessage = "Transfer not found.";
                    return false;
                }

                if (transfer.TransferStatus != "Approved" && transfer.TransferStatus != "InTransit")
                {
                    errorMessage = "Transfer can only be completed when status is Approved or InTransit.";
                    return false;
                }

                // Get transfer items and update stock levels
                var items = clsStockTransferData.GetStockTransferItems(transferID, out string itemsError);
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        // Decrease stock at source branch
                        // Increase stock at destination branch
                        // Note: This would require updating the Products table stock levels
                        // For now, we'll just update the transfer status
                    }
                }

                return clsStockTransferData.UpdateTransferStatus(transferID, "Completed", null, completedBy, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool CancelTransfer(int transferID, out string errorMessage)
        {
            errorMessage = "";

            try
            {
                // Check if transfer exists
                var transfer = clsStockTransferData.GetStockTransferByID(transferID, out string getError);
                if (transfer == null)
                {
                    errorMessage = "Transfer not found.";
                    return false;
                }

                if (transfer.TransferStatus == "Completed")
                {
                    errorMessage = "Cannot cancel a completed transfer.";
                    return false;
                }

                return clsStockTransferData.UpdateTransferStatus(transferID, "Cancelled", null, null, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static clsStockTransferData.StockTransferInfo GetStockTransferByID(int transferID, out string errorMessage)
        {
            return clsStockTransferData.GetStockTransferByID(transferID, out errorMessage);
        }

        public static List<clsStockTransferData.StockTransferItemInfo> GetStockTransferItems(int transferID, out string errorMessage)
        {
            return clsStockTransferData.GetStockTransferItems(transferID, out errorMessage);
        }

        public static List<clsStockTransferData.StockTransferInfo> GetAllStockTransfers(out string errorMessage)
        {
            return clsStockTransferData.GetAllStockTransfers(out errorMessage);
        }

        public static List<clsStockTransferData.StockTransferInfo> GetStockTransfersByBranch(int branchID, out string errorMessage)
        {
            return clsStockTransferData.GetStockTransfersByBranch(branchID, out errorMessage);
        }
    }
}
