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
            this._rbCash = new System.Windows.Forms.RadioButton();
            this._rbCard = new System.Windows.Forms.RadioButton();
            this._rbSplit = new System.Windows.Forms.RadioButton();
            this._lblCardLastFour = new System.Windows.Forms.Label();
            this._txtCardLastFour = new System.Windows.Forms.TextBox();
            this._btnCash = new System.Windows.Forms.Button();
            this._btnCard = new System.Windows.Forms.Button();
            this._btnSplit = new System.Windows.Forms.Button();
            this._btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _lblTotalAmount
            // 
            this._lblTotalAmount.AutoSize = true;
            this._lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._lblTotalAmount.Location = new System.Drawing.Point(20, 20);
            this._lblTotalAmount.Name = "_lblTotalAmount";
            this._lblTotalAmount.Size = new System.Drawing.Size(140, 24);
            this._lblTotalAmount.TabIndex = 0;
            this._lblTotalAmount.Text = "Total Amount: $0.00";
            // 
            // _rbCash
            // 
            this._rbCash.AutoSize = true;
            this._rbCash.Checked = true;
            this._rbCash.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._rbCash.Location = new System.Drawing.Point(20, 60);
            this._rbCash.Name = "_rbCash";
            this._rbCash.Size = new System.Drawing.Size(55, 19);
            this._rbCash.TabIndex = 1;
            this._rbCash.TabStop = true;
            this._rbCash.Text = "Cash";
            this._rbCash.UseVisualStyleBackColor = true;
            // 
            // _rbCard
            // 
            this._rbCard.AutoSize = true;
            this._rbCard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._rbCard.Location = new System.Drawing.Point(20, 90);
            this._rbCard.Name = "_rbCard";
            this._rbCard.Size = new System.Drawing.Size(48, 19);
            this._rbCard.TabIndex = 2;
            this._rbCard.Text = "Card";
            this._rbCard.UseVisualStyleBackColor = true;
            // 
            // _rbSplit
            // 
            this._rbSplit.AutoSize = true;
            this._rbSplit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._rbSplit.Location = new System.Drawing.Point(20, 120);
            this._rbSplit.Name = "_rbSplit";
            this._rbSplit.Size = new System.Drawing.Size(51, 19);
            this._rbSplit.TabIndex = 3;
            this._rbSplit.Text = "Split";
            this._rbSplit.UseVisualStyleBackColor = true;
            // 
            // _lblCardLastFour
            // 
            this._lblCardLastFour.AutoSize = true;
            this._lblCardLastFour.Location = new System.Drawing.Point(20, 150);
            this._lblCardLastFour.Name = "_lblCardLastFour";
            this._lblCardLastFour.Size = new System.Drawing.Size(82, 13);
            this._lblCardLastFour.TabIndex = 4;
            this._lblCardLastFour.Text = "Last 4 Digits:";
            // 
            // _txtCardLastFour
            // 
            this._txtCardLastFour.Location = new System.Drawing.Point(110, 147);
            this._txtCardLastFour.Name = "_txtCardLastFour";
            this._txtCardLastFour.Size = new System.Drawing.Size(80, 20);
            this._txtCardLastFour.TabIndex = 5;
            // 
            // _btnCash
            // 
            this._btnCash.Location = new System.Drawing.Point(20, 190);
            this._btnCash.Name = "_btnCash";
            this._btnCash.Size = new System.Drawing.Size(100, 35);
            this._btnCash.TabIndex = 6;
            this._btnCash.Text = "Cash";
            this._btnCash.UseVisualStyleBackColor = true;
            this._btnCash.Click += new System.EventHandler(this._btnCash_Click);
            // 
            // _btnCard
            // 
            this._btnCard.Location = new System.Drawing.Point(130, 190);
            this._btnCard.Name = "_btnCard";
            this._btnCard.Size = new System.Drawing.Size(100, 35);
            this._btnCard.TabIndex = 7;
            this._btnCard.Text = "Card";
            this._btnCard.UseVisualStyleBackColor = true;
            this._btnCard.Click += new System.EventHandler(this._btnCard_Click);
            // 
            // _btnSplit
            // 
            this._btnSplit.Location = new System.Drawing.Point(240, 190);
            this._btnSplit.Name = "_btnSplit";
            this._btnSplit.Size = new System.Drawing.Size(100, 35);
            this._btnSplit.TabIndex = 8;
            this._btnSplit.Text = "Split";
            this._btnSplit.UseVisualStyleBackColor = true;
            this._btnSplit.Click += new System.EventHandler(this._btnSplit_Click);
            // 
            // _btnCancel
            // 
            this._btnCancel.Location = new System.Drawing.Point(350, 190);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(100, 35);
            this._btnCancel.TabIndex = 9;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // frmPaymentSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 240);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnSplit);
            this.Controls.Add(this._btnCard);
            this.Controls.Add(this._btnCash);
            this.Controls.Add(this._txtCardLastFour);
            this.Controls.Add(this._lblCardLastFour);
            this.Controls.Add(this._rbSplit);
            this.Controls.Add(this._rbCard);
            this.Controls.Add(this._rbCash);
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
        private System.Windows.Forms.RadioButton _rbCash;
        private System.Windows.Forms.RadioButton _rbCard;
        private System.Windows.Forms.RadioButton _rbSplit;
        private System.Windows.Forms.Label _lblCardLastFour;
        private System.Windows.Forms.TextBox _txtCardLastFour;
        private System.Windows.Forms.Button _btnCash;
        private System.Windows.Forms.Button _btnCard;
        private System.Windows.Forms.Button _btnSplit;
        private System.Windows.Forms.Button _btnCancel;
    }
}
