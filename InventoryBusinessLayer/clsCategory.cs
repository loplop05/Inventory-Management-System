using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    // Changed to 'public' so your UI project can access and instantiate this class
    public class clsCategory
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        // Tracks exact validation outcomes for debugging or specialized UI error tracking
        public enum enValidateCategory { Success = 0, InvalidName = 1, NameAlreadyExists = 2, NotFound = 3 }

        public int CategoryID;
        public string CategoryName;

        public clsCategory()
        {
            CategoryID = -1;
            CategoryName = "";
            Mode = enMode.AddNew;
        }

        public clsCategory(int CategoryID, string CategoryName)
        {
            this.CategoryID = CategoryID;
            this.CategoryName = CategoryName?.Trim() ?? ""; 
            Mode = enMode.Update;
        }

       
        public enValidateCategory Validate()
        {
            
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                return enValidateCategory.InvalidName;
            }

            CategoryName = CategoryName.Trim();

            switch (Mode)
            {
                case enMode.AddNew:
                    if (clsCategoryData.DoesCategoryExist(CategoryName))
                    {
                        return enValidateCategory.NameAlreadyExists;
                    }
                    break;

                case enMode.Update:
                    if (!clsCategoryData.DoesCategoryExist(CategoryID))
                    {
                        return enValidateCategory.NotFound;
                    }
                    break;
            }

            return enValidateCategory.Success;
        }

        private bool _AddNewCategory()
        {
            this.CategoryID = clsCategoryData.AddNewCategory(this.CategoryID, this.CategoryName);
            return (this.CategoryID != -1);
        }

        private bool _UpdateCategory()
        {
            return clsCategoryData.UpdateCategory(this.CategoryID, this.CategoryName);
        }

        public static bool DeleteCategory(int CategoryID)
        {
            if (!clsCategoryData.DoesCategoryExist(CategoryID))
            {
                return false;
            }
            return clsCategoryData.DeleteCategory(CategoryID);
        }

        public static clsCategory FindCategory(int CategoryID)
        {
            string CategoryName = "";

            if (clsCategoryData.GetCategoryByID(CategoryID, ref CategoryName))
            {
                return new clsCategory(CategoryID, CategoryName);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllCategories()
        {
            return clsCategoryData.GetAllCategories();
        }









        public bool Save()
        {
            if (Validate() != enValidateCategory.Success)
            {
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCategory())
                    {
                        Mode = enMode.Update; 
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateCategory();
            }

            return false;
        }











    }
}
