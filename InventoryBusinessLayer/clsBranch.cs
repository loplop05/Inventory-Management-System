using System;
using System.Collections.Generic;
using InventoryDataAccessLayer;

namespace InventoryBusinessLayer
{
    public static class clsBranch
    {
        public static bool AddBranch(clsBranchData.BranchInfo branch, out int branchID, out string errorMessage)
        {
            // Validate branch code uniqueness
            if (clsBranchData.BranchExists(branch.BranchCode, out string existsError))
            {
                branchID = -1;
                errorMessage = "Branch code already exists.";
                return false;
            }

            return clsBranchData.AddBranch(branch, out branchID, out errorMessage);
        }

        public static bool UpdateBranch(clsBranchData.BranchInfo branch, out string errorMessage)
        {
            // Check if branch exists
            var existing = clsBranchData.GetBranchByID(branch.BranchID, out string getError);
            if (existing == null)
            {
                errorMessage = "Branch not found.";
                return false;
            }

            // Validate branch code uniqueness if changed
            if (existing.BranchCode != branch.BranchCode)
            {
                if (clsBranchData.BranchExists(branch.BranchCode, out string existsError))
                {
                    errorMessage = "Branch code already exists.";
                    return false;
                }
            }

            return clsBranchData.UpdateBranch(branch, out errorMessage);
        }

        public static bool DeleteBranch(int branchID, out string errorMessage)
        {
            return clsBranchData.DeleteBranch(branchID, out errorMessage);
        }

        public static clsBranchData.BranchInfo GetBranchByID(int branchID, out string errorMessage)
        {
            return clsBranchData.GetBranchByID(branchID, out errorMessage);
        }

        public static List<clsBranchData.BranchInfo> GetAllBranches(out string errorMessage)
        {
            return clsBranchData.GetAllBranches(out errorMessage);
        }

        public static List<clsBranchData.BranchInfo> GetActiveBranches(out string errorMessage)
        {
            return clsBranchData.GetActiveBranches(out errorMessage);
        }
    }
}
