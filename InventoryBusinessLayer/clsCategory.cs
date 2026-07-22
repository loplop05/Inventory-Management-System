using System.Data;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public class clsCategory
    {

        public enum enMode
        {
            AddNew = 0,
            Update = 1
        };

        public enMode Mode = enMode.AddNew;


        public enum enValidateCategory
        {
            Success = 0,
            InvalidName = 1,
            NameAlreadyExists = 2,
            NotFound = 3
        }


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

            // Empty name
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                return enValidateCategory.InvalidName;
            }


            // Check numbers and special characters
            foreach (char c in CategoryName)
            {
                if (char.IsDigit(c) ||
                  (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
                {
                    return enValidateCategory.InvalidName;
                }
            }



            // Add new validation
            if (Mode == enMode.AddNew)
            {
                if (clsCategoryData.DoesCategoryExist(CategoryName))
                {
                    return enValidateCategory.NameAlreadyExists;
                }
            }



            // Update validation
            if (Mode == enMode.Update)
            {
                string ExistingName = "";


                if (!clsCategoryData.GetCategoryByID(CategoryID, ref ExistingName))
                {
                    return enValidateCategory.NotFound;
                }


                // User changed the name
                if (ExistingName != CategoryName)
                {
                    if (clsCategoryData.DoesCategoryExist(CategoryName))
                    {
                        return enValidateCategory.NameAlreadyExists;
                    }
                }
            }



            return enValidateCategory.Success;
        }





        private bool _AddNewCategory()
        {
            this.CategoryID = clsCategoryData.AddNewCategory(this.CategoryName);

            return this.CategoryID != -1;
        }



        private bool _UpdateCategory()
        {
            return clsCategoryData.UpdateCategory(
                this.CategoryID,
                this.CategoryName);
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


            return null;
        }





        public static DataTable GetAllCategories()
        {
            return clsCategoryData.GetAllCategories();
        }

    }
}