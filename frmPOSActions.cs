using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using InventoryBusinessLayer;
using InventoryDataAccessLayer;

namespace InventoryManagementSystem
{
    public partial class frmPOSActions : Form
    {
        // Receipt items passed from frmPOS - use the same ReceiptItem class from frmPOS
        public dynamic ReceiptItems { get; set; }
        public DataGridView ReceiptGrid { get; set; }
        public Action RefreshTotals { get; set; }
        public Action ClearCustomerInfo { get; set; }
        public int? SelectedCustomerID { get; set; }
        public DataTable ProductsTable { get; set; }
        public Action<decimal, string> ApplyManualDiscount { get; set; }
        public Action<string, decimal> ApplyCoupon { get; set; }
        public Action ClearDiscounts { get; set; }

        public frmPOSActions()
        {
            InitializeComponent();
        }

        private void frmPOSActions_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            
            // Apply themed button styles
            clsFormTheme.ApplyPrimaryButtonStyle(_btnAddDiscount);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnApplyCoupon);
            clsFormTheme.ApplyDangerButtonStyle(_btnVoidItem);
            clsFormTheme.ApplyDangerButtonStyle(_btnVoidOrder);
            clsFormTheme.ApplySecondaryButtonStyle(_btnHoldOrder);
            clsFormTheme.ApplySecondaryButtonStyle(_btnRetrieveHeld);
            clsFormTheme.ApplySecondaryButtonStyle(_btnClose);

            // Enable/disable buttons based on receipt state
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            int count = 0;
            if (ReceiptItems != null)
            {
                // Use reflection to get Count property from dynamic object
                var countProp = ReceiptItems.GetType().GetProperty("Count");
                if (countProp != null)
                    count = (int)countProp.GetValue(ReceiptItems);
            }

            bool hasItems = count > 0;
            _btnAddDiscount.Enabled = hasItems;
            _btnApplyCoupon.Enabled = hasItems;
            _btnVoidItem.Enabled = hasItems && ReceiptGrid != null && ReceiptGrid.CurrentRow != null;
            _btnVoidOrder.Enabled = hasItems;
            _btnHoldOrder.Enabled = hasItems;
        }

        private int GetReceiptItemsCount()
        {
            if (ReceiptItems == null)
                return 0;

            var countProp = ReceiptItems.GetType().GetProperty("Count");
            return countProp != null ? (int)countProp.GetValue(ReceiptItems) : 0;
        }

        private void _btnAddDiscount_Click(object sender, EventArgs e)
        {
            if (ReceiptItems == null || GetReceiptItemsCount() == 0)
            {
                clsFormTheme.ShowInfo(this, "Receipt is empty.", "Discount");
                return;
            }

            using (frmManualDiscount discountForm = new frmManualDiscount())
            {
                if (discountForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Apply discount to the receipt
                    decimal discountValue = discountForm.DiscountValue;
                    frmManualDiscount.DiscountType discountType = discountForm.SelectedType;

                    string typeText = discountType == frmManualDiscount.DiscountType.Percentage ? "percentage" : "fixed amount";

                    // Clear any existing coupon when applying manual discount
                    ClearDiscounts?.Invoke();

                    // Apply the discount
                    ApplyManualDiscount?.Invoke(discountValue, typeText);

                    clsFormTheme.ShowSuccess(this, $"Discount applied: {typeText} - {discountValue}", "Success");
                }
            }
        }

        private void _btnApplyCoupon_Click(object sender, EventArgs e)
        {
           if (ReceiptItems == null || GetReceiptItemsCount() == 0)
            {
                clsFormTheme.ShowInfo(this, "Receipt is empty.", "Coupon");
                return;
            }

            using (frmInputBox inputForm = new frmInputBox("Enter coupon code:", "Apply Coupon"))
            {
                if (inputForm.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(inputForm.InputValue))
                    return;

                string couponCode = inputForm.InputValue;

                // Validate coupon using clsDiscountSystem
                var coupon = clsDiscountSystem.GetCoupon(couponCode);
                if (coupon == null)
                {
                    clsFormTheme.ShowError(this, "Invalid coupon code.", "Coupon");
                    return;
                }

                if (!coupon.IsValid())
                {
                    clsFormTheme.ShowWarning(this, "This coupon is expired or has reached its usage limit.", "Coupon");
                    return;
                }

                decimal subtotal = 0;
                foreach (var item in ReceiptItems)
                    subtotal += (decimal)item.GetType().GetProperty("Subtotal").GetValue(item);

                if (subtotal < coupon.MinimumPurchase)
                {
                    clsFormTheme.ShowWarning(this, $"Minimum purchase of {coupon.MinimumPurchase:C2} required for this coupon.", "Coupon");
                    return;
                }

                // Apply coupon discount
                decimal discount = 0;
                if (coupon.Type == clsDiscountSystem.DiscountType.Percentage)
                {
                    discount = subtotal * (coupon.Value / 100m);
                }
                else if (coupon.Type == clsDiscountSystem.DiscountType.FixedAmount)
                {
                    discount = coupon.Value;
                }

                // Clear any existing manual discount when applying coupon
                ClearDiscounts?.Invoke();

                // Apply the coupon
                ApplyCoupon?.Invoke(couponCode, discount);

                clsFormTheme.ShowSuccess(this, $"Coupon applied! Discount: {discount:C2}", "Success");
            }
        }

        private void _btnRedeemPoints_Click(object sender, EventArgs e)
        {
            if (ReceiptItems == null || GetReceiptItemsCount() == 0)
            {
                clsFormTheme.ShowInfo(this, "Receipt is empty.", "Loyalty");
                return;
            }

            if (SelectedCustomerID == null || !SelectedCustomerID.HasValue)
            {
                clsFormTheme.ShowWarning(this, "Please select a customer first to redeem loyalty points.", "Loyalty");
                return;
            }

            // Get customer loyalty info
            string errorMessage;
            var loyaltyInfo = clsLoyalty.GetCustomerLoyaltyInfo(SelectedCustomerID.Value, out errorMessage);
            if (loyaltyInfo == null)
            {
                clsFormTheme.ShowError(this, "Could not retrieve loyalty information: " + errorMessage, "Loyalty");
                return;
            }

            DataRow row = loyaltyInfo.Rows[0];
            int availablePoints = row["LoyaltyPoints"] != DBNull.Value ? Convert.ToInt32(row["LoyaltyPoints"]) : 0;
            decimal discountAvailable = row["DiscountAvailable"] != DBNull.Value ? Convert.ToDecimal(row["DiscountAvailable"]) : 0;

            if (availablePoints < 100)
            {
                clsFormTheme.ShowInfo(this, "Customer needs at least 100 points to redeem. Current points: " + availablePoints, "Loyalty");
                return;
            }

            // Ask how many points to redeem
            using (frmInputBox inputForm = new frmInputBox($"Enter points to redeem (100-{availablePoints}, 100 pts = $1):", "Redeem Points"))
            {
                if (inputForm.ShowDialog(this) != DialogResult.OK || string.IsNullOrWhiteSpace(inputForm.InputValue))
                    return;

                if (!int.TryParse(inputForm.InputValue, out int pointsToRedeem))
                {
                    clsFormTheme.ShowWarning(this, "Please enter a valid number of points.", "Loyalty");
                    return;
                }

                if (pointsToRedeem < 100)
                {
                    clsFormTheme.ShowWarning(this, "Minimum redemption is 100 points.", "Loyalty");
                    return;
                }

                if (pointsToRedeem > availablePoints)
                {
                    clsFormTheme.ShowWarning(this, $"Insufficient points. Available: {availablePoints}", "Loyalty");
                    return;
                }

                // Redeem points
                decimal discountAmount;
                bool success = clsLoyalty.RedeemPoints(SelectedCustomerID.Value, pointsToRedeem, out discountAmount, out errorMessage);
                if (!success)
                {
                    clsFormTheme.ShowError(this, "Failed to redeem points: " + errorMessage, "Loyalty");
                    return;
                }

                // Clear any existing discounts when redeeming points
                ClearDiscounts?.Invoke();

                // Apply the loyalty discount
                ApplyCoupon?.Invoke("Loyalty: " + pointsToRedeem + " pts", discountAmount);

                clsFormTheme.ShowSuccess(this, $"Points redeemed! Discount: {discountAmount:C2}", "Success");
            }
        }

        private void _btnVoidItem_Click(object sender, EventArgs e)
        {
            if (ReceiptGrid == null || ReceiptGrid.CurrentRow == null)
            {
                clsFormTheme.ShowWarning(this, "Please select an item to void.", "Void Item");
                return;
            }

            dynamic item = ReceiptGrid.CurrentRow.DataBoundItem;
            if (item == null)
                return;

            var productNameProp = item.GetType().GetProperty("ProductName");
            string productName = productNameProp != null ? productNameProp.GetValue(item).ToString() : "item";

            var result = clsFormTheme.ShowConfirm(this, $"Void item: {productName}?", "Void Item");

            if (result == DialogResult.Yes)
            {
                var removeMethod = ReceiptItems.GetType().GetMethod("Remove");
                if (removeMethod != null)
                    removeMethod.Invoke(ReceiptItems, new object[] { item });

                RefreshTotals?.Invoke();
                UpdateButtonStates();
            }
        }

        private void _btnVoidOrder_Click(object sender, EventArgs e)
        {
            if (ReceiptItems == null || GetReceiptItemsCount() == 0)
            {
                clsFormTheme.ShowInfo(this, "Receipt is empty.", "Void Order");
                return;
            }

            var result = clsFormTheme.ShowConfirm(this, "Void entire order? This will clear all items.", "Void Order");

            if (result == DialogResult.Yes)
            {
                var clearMethod = ReceiptItems.GetType().GetMethod("Clear");
                if (clearMethod != null)
                    clearMethod.Invoke(ReceiptItems, null);

                RefreshTotals?.Invoke();
                ClearCustomerInfo?.Invoke();
                UpdateButtonStates();
            }
        }

        private void _btnHoldOrder_Click(object sender, EventArgs e)
        {
            if (ReceiptItems == null || GetReceiptItemsCount() == 0)
            {
                clsFormTheme.ShowInfo(this, "Receipt is empty.", "Hold Order");
                return;
            }

            // Ensure HeldOrders tables exist in database
            string migrationError;
            if (!clsDatabaseMigration.EnsureHeldOrdersTablesExist(out migrationError))
            {
                clsFormTheme.ShowError(this, "Failed to initialize held orders tables: " + migrationError, "Database Error");
                return;
            }

            using (frmInputBox inputForm = new frmInputBox("Enter notes for this held order:", "Hold Order"))
            {
                if (inputForm.ShowDialog(this) != DialogResult.OK)
                    return;

                string notes = inputForm.InputValue;

                // Build order items
                DataTable orderItems = new DataTable();
                orderItems.Columns.Add("ProductID", typeof(int));
                orderItems.Columns.Add("ProductName", typeof(string));
                orderItems.Columns.Add("Quantity", typeof(int));
                orderItems.Columns.Add("UnitPrice", typeof(decimal));

                foreach (var item in ReceiptItems)
                {
                    var productIDProp = item.GetType().GetProperty("ProductID");
                    var productNameProp = item.GetType().GetProperty("ProductName");
                    var quantityProp = item.GetType().GetProperty("Quantity");
                    var unitPriceProp = item.GetType().GetProperty("UnitPrice");

                    orderItems.Rows.Add(
                        productIDProp != null ? productIDProp.GetValue(item) : 0,
                        productNameProp != null ? productNameProp.GetValue(item) : "",
                        quantityProp != null ? quantityProp.GetValue(item) : 0,
                        unitPriceProp != null ? unitPriceProp.GetValue(item) : 0m
                    );
                }

                // Calculate total
                decimal total = 0;
                foreach (var item in ReceiptItems)
                    total += (decimal)item.GetType().GetProperty("Subtotal").GetValue(item);

                string errorMessage;
                int heldOrderID = clsHeldOrder.SaveHeldOrder(SelectedCustomerID, orderItems, total, notes, out errorMessage);

                if (heldOrderID > 0)
                {
                    clsFormTheme.ShowSuccess(this, "Order held successfully.", "Success");

                    var clearMethod = ReceiptItems.GetType().GetMethod("Clear");
                    if (clearMethod != null)
                        clearMethod.Invoke(ReceiptItems, null);

                    RefreshTotals?.Invoke();
                    ClearCustomerInfo?.Invoke();
                    UpdateButtonStates();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to hold order: " + errorMessage, "Error");
                }
            }
        }

        private void _btnRetrieveHeld_Click(object sender, EventArgs e)
        {
            // Ensure HeldOrders tables exist in database
            string migrationError;
            if (!clsDatabaseMigration.EnsureHeldOrdersTablesExist(out migrationError))
            {
                clsFormTheme.ShowError(this, "Failed to initialize held orders tables: " + migrationError, "Database Error");
                return;
            }

            using (frmHeldOrders heldForm = new frmHeldOrders())
            {
                if (heldForm.ShowDialog(this) == DialogResult.OK && heldForm.SelectedHeldOrder != null)
                {
                    var heldOrder = heldForm.SelectedHeldOrder;

                    try
                    {
                        // Clear current receipt
                        var clearMethod = ReceiptItems.GetType().GetMethod("Clear");
                        if (clearMethod != null)
                            clearMethod.Invoke(ReceiptItems, null);

                        // Get the ReceiptItem type from the BindingList
                        var receiptItemType = ReceiptItems.GetType().GetGenericArguments()[0];

                        // Add held order items to receipt
                        foreach (var item in heldOrder.Items)
                        {
                            // Look up current stock from products table
                            int currentStock = 0;
                            if (ProductsTable != null)
                            {
                                var matches = ProductsTable.Select("ProductID = " + item.ProductID);
                                if (matches.Length > 0)
                                    currentStock = Convert.ToInt32(matches[0]["Quantity"]);
                            }

                            // Warn if stock is insufficient
                            if (currentStock < item.Quantity)
                            {
                                clsFormTheme.ShowWarning(this, $"Product '{item.ProductName}' has insufficient stock. Available: {currentStock}, Held: {item.Quantity}.",
                                    "Stock Warning");
                                item.Quantity = Math.Min(item.Quantity, currentStock);
                            }

                            // Create a new ReceiptItem instance using the actual type
                            var receiptItem = Activator.CreateInstance(receiptItemType);

                            var productIDProp = receiptItemType.GetProperty("ProductID");
                            var productNameProp = receiptItemType.GetProperty("ProductName");
                            var quantityProp = receiptItemType.GetProperty("Quantity");
                            var unitPriceProp = receiptItemType.GetProperty("UnitPrice");
                            var availableStockProp = receiptItemType.GetProperty("AvailableStock");

                            if (productIDProp != null) productIDProp.SetValue(receiptItem, item.ProductID);
                            if (productNameProp != null) productNameProp.SetValue(receiptItem, item.ProductName);
                            if (quantityProp != null) quantityProp.SetValue(receiptItem, item.Quantity);
                            if (unitPriceProp != null) unitPriceProp.SetValue(receiptItem, item.UnitPrice);
                            if (availableStockProp != null) availableStockProp.SetValue(receiptItem, currentStock);

                            var addMethod = ReceiptItems.GetType().GetMethod("Add");
                            if (addMethod != null)
                                addMethod.Invoke(ReceiptItems, new object[] { receiptItem });
                        }

                        // Set customer info if available
                        if (heldOrder.CustomerID.HasValue)
                        {
                            SelectedCustomerID = heldOrder.CustomerID;
                            // TODO: Update customer display in frmPOS
                        }

                        RefreshTotals?.Invoke();
                        UpdateButtonStates();

                        // Clear discounts when retrieving held order
                        ClearDiscounts?.Invoke();

                        // Delete the held order after retrieval
                        string errorMessage;
                        clsHeldOrder.DeleteHeldOrder(heldOrder.HeldOrderID, out errorMessage);

                        clsFormTheme.ShowSuccess(this, "Held order retrieved successfully.", "Success");
                    }
                    catch (Exception ex)
                    {
                        clsFormTheme.ShowError(this, "Error retrieving held order: " + ex.Message, "Error");
                    }
                }
            }
        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
