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
    internal class clsCategory
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int CategoryID;
        public string CategoryName;

        public clsCategory()
        {
            CategoryID = -1;
            CategoryName = "";
        }



        public clsCategory(int CategoryID, string CategoryName)
        {
            this.CategoryID = CategoryID;
            this.CategoryName = CategoryName;
            Mode = enMode.Update;

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
            return clsCategoryData.DeleteCategory(CategoryID);
        }


        public static clsCategory FindCategory(int CategoryID)
        {
            string CategoryName = "";

            if(clsCategoryData.GetCategoryByID( CategoryID,ref CategoryName))
            {

                return new clsCategory(CategoryID, CategoryName);

            }else
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


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCategory())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCategory();




            }

            return false;

        }






    }
}