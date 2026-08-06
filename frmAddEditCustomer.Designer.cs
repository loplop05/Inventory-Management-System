namespace InventoryManagementSystem
{
    partial class frmAddEditCustomer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._mainPanel = new System.Windows.Forms.Panel();
            this._lblPhoneNumber = new System.Windows.Forms.Label();
            this._txtPhoneNumber = new System.Windows.Forms.TextBox();
            this._lblCustomerName = new System.Windows.Forms.Label();
            this._txtCustomerName = new System.Windows.Forms.TextBox();
            this._lblNotes = new System.Windows.Forms.Label();
            this._txtNotes = new System.Windows.Forms.TextBox();
            this._btnSave = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._lblPhoneNumber);
            this._mainPanel.Controls.Add(this._txtPhoneNumber);
            this._mainPanel.Controls.Add(this._lblCustomerName);
            this._mainPanel.Controls.Add(this._txtCustomerName);
            this._mainPanel.Controls.Add(this._lblNotes);
            this._mainPanel.Controls.Add(this._txtNotes);
            this._mainPanel.Controls.Add(this._btnSave);
            this._mainPanel.Controls.Add(this._btnCancel);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Padding = new System.Windows.Forms.Padding(20);
            this._mainPanel.Size = new System.Drawing.Size(400, 350);
            this._mainPanel.TabIndex = 0;
            // 
            // _lblPhoneNumber
            // 
            this._lblPhoneNumber.AutoSize = true;
            this._lblPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblPhoneNumber.Location = new System.Drawing.Point(20, 20);
            this._lblPhoneNumber.Name = "_lblPhoneNumber";
            this._lblPhoneNumber.Size = new System.Drawing.Size(85, 19);
            this._lblPhoneNumber.TabIndex = 0;
            this._lblPhoneNumber.Text = "Phone Number";
            // 
            // _txtPhoneNumber
            // 
            this._txtPhoneNumber.Location = new System.Drawing.Point(20, 45);
            this._txtPhoneNumber.Name = "_txtPhoneNumber";
            // this._txtPhoneNumber.PlaceholderText = "+962XXXXXXXXX or 07XXXXXXXXX";
            this._txtPhoneNumber.Size = new System.Drawing.Size(340, 23);
            this._txtPhoneNumber.TabIndex = 1;
            // 
            // _lblCustomerName
            // 
            this._lblCustomerName.AutoSize = true;
            this._lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblCustomerName.Location = new System.Drawing.Point(20, 80);
            this._lblCustomerName.Name = "_lblCustomerName";
            this._lblCustomerName.Size = new System.Drawing.Size(103, 19);
            this._lblCustomerName.TabIndex = 2;
            this._lblCustomerName.Text = "Customer Name";
            // 
            // _txtCustomerName
            // 
            this._txtCustomerName.Location = new System.Drawing.Point(20, 105);
            this._txtCustomerName.Name = "_txtCustomerName";
            // this._txtCustomerName.PlaceholderText = "Enter customer name";
            this._txtCustomerName.Size = new System.Drawing.Size(340, 23);
            this._txtCustomerName.TabIndex = 3;
            // 
            // _lblNotes
            // 
            this._lblNotes.AutoSize = true;
            this._lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblNotes.Location = new System.Drawing.Point(20, 140);
            this._lblNotes.Name = "_lblNotes";
            this._lblNotes.Size = new System.Drawing.Size(45, 19);
            this._lblNotes.TabIndex = 4;
            this._lblNotes.Text = "Notes";
            // 
            // _txtNotes
            // 
            this._txtNotes.Location = new System.Drawing.Point(20, 165);
            this._txtNotes.Multiline = true;
            this._txtNotes.Name = "_txtNotes";
            // this._txtNotes.PlaceholderText = "Optional notes about this customer";
            this._txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtNotes.Size = new System.Drawing.Size(340, 80);
            this._txtNotes.TabIndex = 5;
            // 
            // _btnSave
            // 
            this._btnSave.Location = new System.Drawing.Point(20, 270);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(100, 35);
            this._btnSave.TabIndex = 6;
            this._btnSave.Text = "Save";
            this._btnSave.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(260, 270);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 35);
            this._btnCancel.TabIndex = 7;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmAddEditCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditCustomer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Customer";
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.Label _lblPhoneNumber;
        private System.Windows.Forms.TextBox _txtPhoneNumber;
        private System.Windows.Forms.Label _lblCustomerName;
        private System.Windows.Forms.TextBox _txtCustomerName;
        private System.Windows.Forms.Label _lblNotes;
        private System.Windows.Forms.TextBox _txtNotes;
        private System.Windows.Forms.Button _btnSave;
        private System.Windows.Forms.Button _btnCancel;
    }
}
