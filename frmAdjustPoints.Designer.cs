namespace InventoryManagementSystem
{
    partial class frmAdjustPoints
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
            this._lblCurrentPoints = new System.Windows.Forms.Label();
            this._lblAdjustment = new System.Windows.Forms.Label();
            this._txtAdjustment = new System.Windows.Forms.TextBox();
            this._lblReason = new System.Windows.Forms.Label();
            this._txtReason = new System.Windows.Forms.TextBox();
            this._btnAdd = new System.Windows.Forms.Button();
            this._btnDeduct = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.Controls.Add(this._lblCurrentPoints);
            this._mainPanel.Controls.Add(this._lblAdjustment);
            this._mainPanel.Controls.Add(this._txtAdjustment);
            this._mainPanel.Controls.Add(this._lblReason);
            this._mainPanel.Controls.Add(this._txtReason);
            this._mainPanel.Controls.Add(this._btnAdd);
            this._mainPanel.Controls.Add(this._btnDeduct);
            this._mainPanel.Controls.Add(this._btnCancel);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Padding = new System.Windows.Forms.Padding(20);
            this._mainPanel.Size = new System.Drawing.Size(400, 300);
            this._mainPanel.TabIndex = 0;
            // 
            // _lblCurrentPoints
            // 
            this._lblCurrentPoints.AutoSize = true;
            this._lblCurrentPoints.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._lblCurrentPoints.Location = new System.Drawing.Point(20, 20);
            this._lblCurrentPoints.Name = "_lblCurrentPoints";
            this._lblCurrentPoints.Size = new System.Drawing.Size(0, 28);
            this._lblCurrentPoints.TabIndex = 0;
            // 
            // _lblAdjustment
            // 
            this._lblAdjustment.AutoSize = true;
            this._lblAdjustment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblAdjustment.Location = new System.Drawing.Point(20, 60);
            this._lblAdjustment.Name = "_lblAdjustment";
            this._lblAdjustment.Size = new System.Drawing.Size(68, 19);
            this._lblAdjustment.TabIndex = 1;
            this._lblAdjustment.Text = "Points to Add";
            // 
            // _txtAdjustment
            // 
            this._txtAdjustment.Location = new System.Drawing.Point(20, 85);
            this._txtAdjustment.Name = "_txtAdjustment";
            // this._txtAdjustment.PlaceholderText = "Enter number of points";
            this._txtAdjustment.Size = new System.Drawing.Size(340, 23);
            this._txtAdjustment.TabIndex = 2;
            // 
            // _lblReason
            // 
            this._lblReason.AutoSize = true;
            this._lblReason.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblReason.Location = new System.Drawing.Point(20, 120);
            this._lblReason.Name = "_lblReason";
            this._lblReason.Size = new System.Drawing.Size(48, 19);
            this._lblReason.TabIndex = 3;
            this._lblReason.Text = "Reason";
            // 
            // _txtReason
            // 
            this._txtReason.Location = new System.Drawing.Point(20, 145);
            this._txtReason.Multiline = true;
            this._txtReason.Name = "_txtReason";
            // this._txtReason.PlaceholderText = "Reason for adjustment (optional)";
            this._txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtReason.Size = new System.Drawing.Size(340, 60);
            this._txtReason.TabIndex = 4;
            // 
            // _btnAdd
            // 
            this._btnAdd.Location = new System.Drawing.Point(20, 230);
            this._btnAdd.Name = "_btnAdd";
            this._btnAdd.Size = new System.Drawing.Size(100, 35);
            this._btnAdd.TabIndex = 5;
            this._btnAdd.Text = "Add Points";
            this._btnAdd.UseVisualStyleBackColor = true;
            // 
            // _btnDeduct
            // 
            this._btnDeduct.Location = new System.Drawing.Point(130, 230);
            this._btnDeduct.Name = "_btnDeduct";
            this._btnDeduct.Size = new System.Drawing.Size(100, 35);
            this._btnDeduct.TabIndex = 6;
            this._btnDeduct.Text = "Deduct Points";
            this._btnDeduct.UseVisualStyleBackColor = true;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(260, 230);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 35);
            this._btnCancel.TabIndex = 7;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmAdjustPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this._mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdjustPoints";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adjust Loyalty Points";
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel _mainPanel;
        private System.Windows.Forms.Label _lblCurrentPoints;
        private System.Windows.Forms.Label _lblAdjustment;
        private System.Windows.Forms.TextBox _txtAdjustment;
        private System.Windows.Forms.Label _lblReason;
        private System.Windows.Forms.TextBox _txtReason;
        private System.Windows.Forms.Button _btnAdd;
        private System.Windows.Forms.Button _btnDeduct;
        private System.Windows.Forms.Button _btnCancel;
    }
}
