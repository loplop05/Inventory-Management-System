namespace InventoryManagementSystem
{
    partial class frmPaymentSelection
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
            this._lblTotalAmount = new System.Windows.Forms.Label();
            this._lblPaymentMethods = new System.Windows.Forms.Label();
            this._btnCash = new System.Windows.Forms.Button();
            this._btnCard = new System.Windows.Forms.Button();
            this._btnSplit = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lblTotalAmount
            // 
            this._lblTotalAmount.AutoSize = true;
            this._lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this._lblTotalAmount.Location = new System.Drawing.Point(30, 30);
            this._lblTotalAmount.Name = "_lblTotalAmount";
            this._lblTotalAmount.Size = new System.Drawing.Size(180, 32);
            this._lblTotalAmount.TabIndex = 0;
            this._lblTotalAmount.Text = "Total: $0.00";
            // 
            // _lblPaymentMethods
            // 
            this._lblPaymentMethods.AutoSize = true;
            this._lblPaymentMethods.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this._lblPaymentMethods.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this._lblPaymentMethods.Location = new System.Drawing.Point(30, 80);
            this._lblPaymentMethods.Name = "_lblPaymentMethods";
            this._lblPaymentMethods.Size = new System.Drawing.Size(130, 21);
            this._lblPaymentMethods.TabIndex = 1;
            this._lblPaymentMethods.Text = "Select Payment:";
            // 
            // _btnCash
            // 
            this._btnCash.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnCash.Location = new System.Drawing.Point(30, 120);
            this._btnCash.Name = "_btnCash";
            this._btnCash.Size = new System.Drawing.Size(140, 50);
            this._btnCash.TabIndex = 2;
            this._btnCash.Text = "💵 Cash";
            this._btnCash.UseVisualStyleBackColor = true;
            this._btnCash.Click += new System.EventHandler(this._btnCash_Click);
            // 
            // _btnCard
            // 
            this._btnCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnCard.Location = new System.Drawing.Point(190, 120);
            this._btnCard.Name = "_btnCard";
            this._btnCard.Size = new System.Drawing.Size(140, 50);
            this._btnCard.TabIndex = 3;
            this._btnCard.Text = "💳 Card";
            this._btnCard.UseVisualStyleBackColor = true;
            this._btnCard.Click += new System.EventHandler(this._btnCard_Click);
            // 
            // _btnSplit
            // 
            this._btnSplit.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._btnSplit.Location = new System.Drawing.Point(350, 120);
            this._btnSplit.Name = "_btnSplit";
            this._btnSplit.Size = new System.Drawing.Size(140, 50);
            this._btnSplit.TabIndex = 4;
            this._btnSplit.Text = "💰 Split";
            this._btnSplit.UseVisualStyleBackColor = true;
            this._btnSplit.Click += new System.EventHandler(this._btnSplit_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this._btnCancel.Location = new System.Drawing.Point(390, 200);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 35);
            this._btnCancel.TabIndex = 5;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // frmPaymentSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 260);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnSplit);
            this.Controls.Add(this._btnCard);
            this.Controls.Add(this._btnCash);
            this.Controls.Add(this._lblPaymentMethods);
            this.Controls.Add(this._lblTotalAmount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaymentSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Payment Method";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label _lblTotalAmount;
        private System.Windows.Forms.Label _lblPaymentMethods;
        private System.Windows.Forms.Button _btnCash;
        private System.Windows.Forms.Button _btnCard;
        private System.Windows.Forms.Button _btnSplit;
        private System.Windows.Forms.Button _btnCancel;

    }
}
