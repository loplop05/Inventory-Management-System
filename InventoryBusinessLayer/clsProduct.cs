using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public class clsProduct
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        // Helps the UI identify exactly why a Save operation failed
        public enum enValidateProduct
        {
            Success = 0,
            InvalidName = 1,
            NameAlreadyExists = 2,
            InvalidCategory = 3,
            InvalidSupplier = 4,
            InvalidPrice = 5,
            InvalidQuantity = 6,
            InvalidBarcode = 7,
            BarcodeAlreadyExists = 8,
            NotFound = 9
        }

        public int ProductID;
        public string ProductName;
        public int CategoryID;
        public int SupplierID;
        public decimal Price;
        public int Quantity;
        public string Barcode;
        public string ImagePath;
        public DateTime CreatedDate;

     
        public clsProduct()
        {
            this.ProductID = -1;
            this.ProductName = "";
            this.CategoryID = -1;
            this.SupplierID = -1;
            this.Price = 0;
            this.Quantity = 0;
            this.Barcode = "";
            this.ImagePath = "";
            this.CreatedDate = DateTime.Now;

            Mode = enMode.AddNew;
        }

        
        public clsProduct(int ProductID, string ProductName, int CategoryID, int SupplierID,
                          decimal Price, int Quantity, string Barcode, string ImagePath, DateTime CreatedDate)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName?.Trim() ?? "";
            this.CategoryID = CategoryID;
            this.SupplierID = SupplierID;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Barcode = Barcode?.Trim() ?? "";
            this.ImagePath = ImagePath?.Trim() ?? "";
            this.CreatedDate = CreatedDate;

            this.Mode = enMode.Update;
        }

        // Comprehensive Validation Method
        public enValidateProduct Validate()
        {
            // 1. Text validations
            if (string.IsNullOrWhiteSpace(ProductName)) return enValidateProduct.InvalidName;
            if (string.IsNullOrWhiteSpace(Barcode)) return enValidateProduct.InvalidBarcode;

            this.ProductName = this.ProductName.Trim();
            this.Barcode = this.Barcode.Trim();

        
            if (Price < 0) return enValidateProduct.InvalidPrice;
            if (Quantity < 0) return enValidateProduct.InvalidQuantity;

            // 2. Foreign key validations
            if (!clsCategoryData.DoesCategoryExist(CategoryID)) return enValidateProduct.InvalidCategory;
            if (!clsSupplierData.DoesSupplierExist(SupplierID)) return enValidateProduct.InvalidSupplier;

            // 3. Database rules check based on the current mode
            switch (Mode)
            {
                case enMode.AddNew:
                    if (clsProductData.DoesProductExistByName(ProductName))
                        return enValidateProduct.NameAlreadyExists;

                    if (clsProductData.DoesProductExistByBarcode(Barcode))
                        return enValidateProduct.BarcodeAlreadyExists;
                    break;

                case enMode.Update:
                    if (!clsProductData.DoesProductExist(ProductID))
                        return enValidateProduct.NotFound;

                    if (clsProductData.DoesProductExistByNameExcept(ProductName, ProductID))
                        return enValidateProduct.NameAlreadyExists;

                    if (clsProductData.DoesProductExistByBarcodeExcept(Barcode, ProductID))
                        return enValidateProduct.BarcodeAlreadyExists;
                    break;
            }

            return enValidateProduct.Success;
        }

        private bool _AddNewProduct()
        {
            this.ProductID = clsProductData.AddNewProduct(
                this.ProductName,
                this.CategoryID,
                this.SupplierID,
                this.Price,
                this.Quantity,
                this.Barcode,
                this.ImagePath,
                this.CreatedDate);

            return (this.ProductID != -1);
        }

        private bool _UpdateProduct()
        {
            return clsProductData.UpdateProduct(
                this.ProductID,
                this.ProductName,
                this.CategoryID,
                this.SupplierID,
                this.Price,
                this.Quantity,
                this.Barcode,
                this.ImagePath);
        }

        public static bool DeleteProduct(int ProductID)
        {
            if (!clsProductData.DoesProductExist(ProductID))
            {
                return false;
            }
            return clsProductData.DeleteProduct(ProductID);
        }

        public static bool RestockProduct(int productID, int quantity, out string errorMessage)
        {
            errorMessage = "";
            try
            {
                if (!clsProductData.DoesProductExist(productID))
                {
                    errorMessage = "Product not found.";
                    return false;
                }

                if (quantity <= 0)
                {
                    errorMessage = "Quantity must be greater than zero.";
                    return false;
                }

                return clsProductData.RestockProduct(productID, quantity, out errorMessage);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool GetAllProducts(out DataTable products, out string errorMessage)
        {
            return clsProductData.GetAllProducts(out products, out errorMessage);
        }

        public static clsProduct FindProduct(int ProductID)
        {
            string ProductName = "";
            int CategoryID = -1;
            int SupplierID = -1;
            decimal Price = 0;
            int Quantity = 0;
            string Barcode = "";
            string ImagePath = "";
            DateTime CreatedDate = DateTime.Now;

            if (clsProductData.GetProductByID(ProductID, ref ProductName, ref CategoryID, ref SupplierID, ref Price, ref Quantity, ref Barcode, ref ImagePath, ref CreatedDate))
            {
                return new clsProduct(ProductID, ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate);
            }
            return null;
        }

        public static clsProduct FindProductByBarcode(string Barcode)
        {
            int ProductID = -1;
            string ProductName = "";
            int CategoryID = -1;
            int SupplierID = -1;
            decimal Price = 0;
            int Quantity = 0;
            string ImagePath = "";
            DateTime CreatedDate = DateTime.Now;

            if (clsProductData.GetProductByBarcode(Barcode, ref ProductID, ref ProductName, ref CategoryID, ref SupplierID, ref Price, ref Quantity, ref ImagePath, ref CreatedDate))
            {
                return new clsProduct(ProductID, ProductName, CategoryID, SupplierID, Price, Quantity, Barcode, ImagePath, CreatedDate);
            }
            return null;
        }

        public bool Save()
        {
            // Stop executing immediately if validation rules fail
            if (Validate() != enValidateProduct.Success)
            {
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewProduct())
                    {
                        Mode = enMode.Update; // Update mode after a successful insert
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateProduct();
            }

            return false;
        }
    }
}
