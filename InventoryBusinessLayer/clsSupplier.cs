using System.Data;
using InventoryDataAccessLayer;


namespace InventoryBusinessLayer
{

    public class clsSupplier
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }
        public enMode Mode = enMode.AddNew;

        public int SupplierID;
        public string SupplierName;
        public string Phone;
        public string Email;

        public clsSupplier()
        {

            SupplierID = -1;

            SupplierName = "";

            Phone = "";

            Email = "";

        }

        public clsSupplier(
            int SupplierID,
            string SupplierName,
            string Phone,
            string Email)
        {
            this.SupplierID = SupplierID;
            this.SupplierName = SupplierName;
            this.Phone = Phone;
            this.Email = Email;
            Mode = enMode.Update;
        }

        private bool _AddNewSupplier()
        {
            SupplierID =
                clsSupplierData.AddNewSupplier(
                    SupplierName,
                    Phone,
                    Email);

            return SupplierID != -1;

        }









        private bool _UpdateSupplier()
        {


            return clsSupplierData.UpdateSupplier(
                SupplierID,
                SupplierName,
                Phone,
                Email);


        }









        public static bool DeleteSupplier(int SupplierID)
        {


            return clsSupplierData.DeleteSupplier(SupplierID);


        }









        public static clsSupplier FindSupplier(int SupplierID)
        {


            string SupplierName = "";

            string Phone = "";

            string Email = "";





            if (clsSupplierData.GetSupplierByID(
                SupplierID,
                ref SupplierName,
                ref Phone,
                ref Email))
            {


                return new clsSupplier(
                    SupplierID,
                    SupplierName,
                    Phone,
                    Email);


            }



            return null;


        }

        public enum enValidateSupplier
        {
            Success,
            NameIsEmpty,
            InvalidEmail,
            InvalidPhone,
        }


        private enValidateSupplier Validate()
        {
            
            if(string.IsNullOrEmpty(SupplierName))
            {
                return enValidateSupplier.NameIsEmpty;
            }

            if(string.IsNullOrEmpty(Email) || !(Email.Contains("@")))
            {
                return enValidateSupplier.InvalidEmail;
            }

            if ((string.IsNullOrEmpty(Phone)) || !(Phone.StartsWith("+962")))
            {
                return enValidateSupplier.InvalidPhone;
            }



                return enValidateSupplier.Success;
        }




        public static DataTable GetAllSuppliers()
        {

            return clsSupplierData.GetAllSuppliers();

        }

        public bool Save()
        {
            if (Validate() != enValidateSupplier.Success)
            {
                return false;
            }

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewSupplier())
                    {
                        Mode = enMode.Update; 
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateSupplier();

                default:
                    return false;
            }
        }







    }

}