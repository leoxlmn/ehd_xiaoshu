namespace EHD.Admin {
    partial class EditAccountBalanceForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAccountBalanceForm));
            this.btnAddDeposit = new System.Windows.Forms.Button();
            this.grdTransaction = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.ttButton = new System.Windows.Forms.ToolTip(this.components);
            this.btnDeleteDeposit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.dlgPrint = new System.Windows.Forms.PrintDialog();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.dlgPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransaction)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddDeposit
            // 
            this.btnAddDeposit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDeposit.BackgroundImage")));
            this.btnAddDeposit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDeposit.Location = new System.Drawing.Point(2, 40);
            this.btnAddDeposit.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddDeposit.Name = "btnAddDeposit";
            this.btnAddDeposit.Size = new System.Drawing.Size(26, 26);
            this.btnAddDeposit.TabIndex = 1;
            this.btnAddDeposit.TabStop = false;
            this.ttButton.SetToolTip(this.btnAddDeposit, "Add a deposit or spend");
            this.btnAddDeposit.UseVisualStyleBackColor = true;
            this.btnAddDeposit.Click += new System.EventHandler(this.btnAddDeposit_Click);
            // 
            // grdTransaction
            // 
            this.grdTransaction.AllowUserToAddRows = false;
            this.grdTransaction.AllowUserToDeleteRows = false;
            this.grdTransaction.AllowUserToResizeRows = false;
            this.grdTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransaction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdTransaction.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdTransaction.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdTransaction.CausesValidation = false;
            this.grdTransaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTransaction.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdTransaction.Location = new System.Drawing.Point(29, 40);
            this.grdTransaction.MultiSelect = false;
            this.grdTransaction.Name = "grdTransaction";
            this.grdTransaction.ReadOnly = true;
            this.grdTransaction.RowHeadersWidth = 25;
            this.grdTransaction.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdTransaction.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTransaction.ShowEditingIcon = false;
            this.grdTransaction.Size = new System.Drawing.Size(730, 333);
            this.grdTransaction.TabIndex = 5;
            this.grdTransaction.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdTransaction_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Patient Name:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(672, 383);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(361, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Balance:";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientName.Location = new System.Drawing.Point(113, 19);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(93, 16);
            this.lblPatientName.TabIndex = 7;
            this.lblPatientName.Text = "Patient Name";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Blue;
            this.lblBalance.Location = new System.Drawing.Point(417, 19);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(59, 16);
            this.lblBalance.TabIndex = 8;
            this.lblBalance.Text = "Balance";
            // 
            // btnDeleteDeposit
            // 
            this.btnDeleteDeposit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDeposit.BackgroundImage")));
            this.btnDeleteDeposit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteDeposit.Location = new System.Drawing.Point(2, 66);
            this.btnDeleteDeposit.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteDeposit.Name = "btnDeleteDeposit";
            this.btnDeleteDeposit.Size = new System.Drawing.Size(26, 26);
            this.btnDeleteDeposit.TabIndex = 70;
            this.btnDeleteDeposit.TabStop = false;
            this.ttButton.SetToolTip(this.btnDeleteDeposit, "Delete a deposit or spend");
            this.btnDeleteDeposit.UseVisualStyleBackColor = true;
            this.btnDeleteDeposit.Click += new System.EventHandler(this.btnDeleteDeposit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.Location = new System.Drawing.Point(2, 162);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(26, 26);
            this.btnPrint.TabIndex = 71;
            this.btnPrint.TabStop = false;
            this.ttButton.SetToolTip(this.btnPrint, "Print");
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintPreview.BackgroundImage")));
            this.btnPrintPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPrintPreview.Location = new System.Drawing.Point(2, 136);
            this.btnPrintPreview.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(26, 26);
            this.btnPrintPreview.TabIndex = 72;
            this.btnPrintPreview.TabStop = false;
            this.ttButton.SetToolTip(this.btnPrintPreview, "Print Preview");
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // dlgPrint
            // 
            this.dlgPrint.Document = this.printDoc;
            // 
            // printDoc
            // 
            this.printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDoc_BeginPrint);
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // dlgPrintPreview
            // 
            this.dlgPrintPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.dlgPrintPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.dlgPrintPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.dlgPrintPreview.Document = this.printDoc;
            this.dlgPrintPreview.Enabled = true;
            this.dlgPrintPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("dlgPrintPreview.Icon")));
            this.dlgPrintPreview.Name = "dlgPrintPreview";
            this.dlgPrintPreview.Visible = false;
            // 
            // EditAccountBalanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(759, 418);
            this.Controls.Add(this.btnPrintPreview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDeleteDeposit);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddDeposit);
            this.Controls.Add(this.grdTransaction);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EditAccountBalanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account Balance";
            this.Load += new System.EventHandler(this.EditAccountBalanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransaction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddDeposit;
        private System.Windows.Forms.DataGridView grdTransaction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.ToolTip ttButton;
        private System.Windows.Forms.Button btnDeleteDeposit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PrintDialog dlgPrint;
        private System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.PrintPreviewDialog dlgPrintPreview;
        private System.Windows.Forms.Button btnPrintPreview;
    }
}