using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using InventoryBusinessLayer;

namespace InventoryManagementSystem
{
    public partial class frmManagePermissions : Form
    {
        private int _userID;
        private string _displayName;
        private int _roleID;
        private DataTable _allPermissions;
        private List<string> _currentPermissions;

        public frmManagePermissions(int userID, string displayName, int roleID)
        {
            InitializeComponent();
            _userID = userID;
            _displayName = displayName;
            _roleID = roleID;
        }

        private void frmManagePermissions_Load(object sender, EventArgs e)
        {
            clsFormTheme.ApplyFormStyle(this);
            clsFormTheme.ApplyGridStyle(gridPermissions);
            
            _btnSave.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplyPrimaryButtonStyle(_btnSave, clsFormTheme.Icons.Save);
            
            _btnCancel.Font = new Font(clsFormTheme.MainFontName, 10F, FontStyle.Bold);
            clsFormTheme.ApplySecondaryButtonStyle(_btnCancel, clsFormTheme.Icons.Cancel);
            
            clsLanguageManager.ApplyLanguage(this);
            ApplyLocalization();
            
            LoadPermissions();
            
            _btnSave.Click += _btnSave_Click;
            _btnCancel.Click += (s, ev) => Close();
        }

        private void ApplyLocalization()
        {
            _lblPermissionsTitle.Text = clsLanguageManager.GetString("Permissions");
            _btnSave.Text = clsLanguageManager.GetString("Save");
            _btnCancel.Text = clsLanguageManager.GetString("Cancel");
            Text = clsLanguageManager.GetString("Manage Permissions");
        }

        private void LoadPermissions()
        {
            try
            {
                _lblUserInfo.Text = $"User: {_displayName}";
                _lblRoleInfo.Text = $"Role ID: {_roleID} (Permissions are managed by role)";
                
                // Load all available permissions
                string errorMessage;
                _allPermissions = clsPermissions.GetAllPermissionsDataTable(out errorMessage);
                
                if (_allPermissions == null)
                {
                    clsFormTheme.ShowError(this, "Failed to load permissions: " + errorMessage, "Error");
                    return;
                }
                
                // Load current permissions for this role
                DataTable currentPerms = clsPermissions.GetRolePermissions(_roleID, out errorMessage);
                
                if (currentPerms != null && currentPerms.Rows.Count > 0)
                {
                    _currentPermissions = new List<string>();
                    foreach (DataRow row in currentPerms.Rows)
                    {
                        _currentPermissions.Add(row["PermissionName"].ToString());
                    }
                }
                else
                {
                    _currentPermissions = new List<string>();
                }
                
                // Setup grid with checkboxes
                gridPermissions.DataSource = _allPermissions;
                gridPermissions.Columns["PermissionID"].Visible = false;
                gridPermissions.Columns["Description"].Visible = false;
                gridPermissions.Columns["PermissionName"].HeaderText = "Permission";
                gridPermissions.Columns["PermissionName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                
                // Add checkbox column
                DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
                checkColumn.Name = "Granted";
                checkColumn.HeaderText = "Granted";
                checkColumn.Width = 60;
                checkColumn.ReadOnly = false;
                gridPermissions.Columns.Insert(0, checkColumn);
                
                // Check current permissions
                foreach (DataGridViewRow row in gridPermissions.Rows)
                {
                    string permName = row.Cells["PermissionName"].Value.ToString();
                    row.Cells["Granted"].Value = _currentPermissions.Contains(permName);
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error loading permissions: " + ex.Message, "Error");
            }
        }

        private void _btnSave_Click(object sender, EventArgs ev)
        {
            try
            {
                List<string> selectedPermissions = new List<string>();
                
                foreach (DataGridViewRow row in gridPermissions.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Granted"].Value))
                    {
                        string permName = row.Cells["PermissionName"].Value.ToString();
                        selectedPermissions.Add(permName);
                    }
                }
                
                string errorMessage;
                if (clsPermissions.SetRolePermissions(_roleID, selectedPermissions, out errorMessage))
                {
                    clsFormTheme.ShowSuccess(this, "Permissions updated successfully.", "Success");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    clsFormTheme.ShowError(this, "Failed to update permissions: " + errorMessage, "Error");
                }
            }
            catch (Exception ex)
            {
                clsFormTheme.ShowError(this, "Error saving permissions: " + ex.Message, "Error");
            }
        }
    }
}
