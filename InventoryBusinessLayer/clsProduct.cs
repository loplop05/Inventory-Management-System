using System;
using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public class clsProduct
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }


        public enMode Mode = enMode.AddNew;


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
            ProductID = -1;
            ProductName = "";
            CategoryID = -1;
            SupplierID = -1;
            Price = 0;
            Quantity = 0;
            Barcode = "";
            ImagePath = "";
            CreatedDate = DateTime.Now;
        }



        public clsProduct(
            int ProductID,
            string ProductName,
            int CategoryID,
            int SupplierID,
            decimal Price,
            int Quantity,
            string Barcode,
            string ImagePath,
            DateTime CreatedDate)
        {
            this.ProductID = ProductID;
            this.ProductName = ProductName;
            this.CategoryID = CategoryID;
            this.SupplierID = SupplierID;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Barcode = Barcode;
            this.ImagePath = ImagePath;
            this.CreatedDate = CreatedDate;

            Mode = enMode.Update;
        }



        private bool _AddNewProduct()
        {
            ProductID = clsProductData.AddNewProduct(
                ProductName,
                CategoryID,
                SupplierID,
                Price,
                Quantity,
                Barcode,
                ImagePath,
                CreatedDate);

            return ProductID != -1;
        }



        private bool _UpdateProduct()
        {
            return clsProductData.UpdateProduct(
                ProductID,
                ProductName,
                CategoryID,
                SupplierID,
                Price,
                Quantity,
                Barcode,
                ImagePath);
        }



        public static bool DeleteProduct(int ProductID)
        {
            return clsProductData.DeleteProduct(ProductID);
        }



        public static DataTable GetAllProducts()
        {
            return clsProductData.GetAllProducts();
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewProduct())
                    {
                        Mode = enMode.Update;
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