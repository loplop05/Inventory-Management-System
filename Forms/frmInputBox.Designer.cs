namespace InventoryManagementSystem
{
    partial class frmInputBox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this._lblPrompt = new System.Windows.Forms.Label();
            this._txtInput = new System.Windows.Forms.TextBox();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this._rootLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _rootLayout
            // 
            this._rootLayout.ColumnCount = 2;
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this._rootLayout.Controls.Add(this._lblPrompt, 0, 0);
            this._rootLayout.Controls.Add(this._txtInput, 0, 1);
            this._rootLayout.Controls.Add(this._btnOK, 1, 2);
            this._rootLayout.Controls.Add(this._btnCancel, 0, 2);
            this._rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootLayout.Location = new System.Drawing.Point(0, 0);
            this._rootLayout.Name = "_rootLayout";
            this._rootLayout.RowCount = 3;
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._rootLayout.Size = new System.Drawing.Size(400, 150);
            this._rootLayout.TabIndex = 0;
            // 
            // _lblPrompt
            // 
            this._lblPrompt.AutoSize = true;
            this._lblPrompt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._lblPrompt.Location = new System.Drawing.Point(3, 0);
            this._lblPrompt.Name = "_lblPrompt";
            this._lblPrompt.Size = new System.Drawing.Size(0, 24);
            this._lblPrompt.TabIndex = 0;
            // 
            // _txtInput
            // 
            this._txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtInput.Location = new System.Drawing.Point(3, 40);
            this._txtInput.Name = "_txtInput";
            this._txtInput.Size = new System.Drawing.Size(274, 30);
            this._txtInput.TabIndex = 1;
            // 
            // _btnOK
            // 
            this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOK.Location = new System.Drawing.Point(280, 83);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(117, 34);
            this._btnOK.TabIndex = 2;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.Location = new System.Drawing.Point(160, 83);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(117, 34);
            this._btnCancel.TabIndex = 3;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // frmInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 150);
            this.Controls.Add(this._rootLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this._rootLayout.ResumeLayout(false);
            this._rootLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel _rootLayout;
        private System.Windows.Forms.Label _lblPrompt;
        private System.Windows.Forms.TextBox _txtInput;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnCancel;
    }
}
