using System;
using System.Collections.Generic;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsRegister
    {
        public static bool AddRegister(clsRegisterData.RegisterInfo register, out int registerID, out string errorMessage)
        {
            // Validate register code uniqueness
            if (clsRegisterData.RegisterExists(register.RegisterCode, out string existsError))
            {
                registerID = -1;
                errorMessage = "Register code already exists.";
                return false;
            }

            // Validate branch exists
            var branch = clsBranchData.GetBranchByID(register.BranchID, out string branchError);
            if (branch == null)
            {
                registerID = -1;
                errorMessage = "Branch not found.";
                return false;
            }

            return clsRegisterData.AddRegister(register, out registerID, out errorMessage);
        }

        public static bool UpdateRegister(clsRegisterData.RegisterInfo register, out string errorMessage)
        {
            // Check if register exists
            var existing = clsRegisterData.GetRegisterByID(register.RegisterID, out string getError);
            if (existing == null)
            {
                errorMessage = "Register not found.";
                return false;
            }

            // Validate register code uniqueness if changed
            if (existing.RegisterCode != register.RegisterCode)
            {
                if (clsRegisterData.RegisterExists(register.RegisterCode, out string existsError))
                {
                    errorMessage = "Register code already exists.";
                    return false;
                }
            }

            // Validate branch exists
            var branch = clsBranchData.GetBranchByID(register.BranchID, out string branchError);
            if (branch == null)
            {
                errorMessage = "Branch not found.";
                return false;
            }

            return clsRegisterData.UpdateRegister(register, out errorMessage);
        }

        public static bool DeleteRegister(int registerID, out string errorMessage)
        {
            return clsRegisterData.DeleteRegister(registerID, out errorMessage);
        }

        public static clsRegisterData.RegisterInfo GetRegisterByID(int registerID, out string errorMessage)
        {
            return clsRegisterData.GetRegisterByID(registerID, out errorMessage);
        }

        public static List<clsRegisterData.RegisterInfo> GetAllRegisters(out string errorMessage)
        {
            return clsRegisterData.GetAllRegisters(out errorMessage);
        }

        public static List<clsRegisterData.RegisterInfo> GetRegistersByBranch(int branchID, out string errorMessage)
        {
            return clsRegisterData.GetRegistersByBranch(branchID, out errorMessage);
        }

        public static List<clsRegisterData.RegisterInfo> GetActiveRegisters(out string errorMessage)
        {
            return clsRegisterData.GetActiveRegisters(out errorMessage);
        }
    }
}
