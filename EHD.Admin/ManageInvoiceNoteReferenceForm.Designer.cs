namespace EHD.Admin {
    partial class ManageInvoiceNoteReferenceForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageInvoiceNoteReferenceForm));
            this.btnAddNew = new System.Windows.Forms.Button();
            this.grdInvoiceNote = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEditInvoiceNote = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoiceNote)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNew.BackColor = System.Drawing.Color.LightGreen;
            this.btnAddNew.Location = new System.Drawing.Point(12, 207);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(165, 23);
            this.btnAddNew.TabIndex = 13;
            this.btnAddNew.Text = "Add New Invoice Note";
            this.btnAddNew.UseVisualStyleBackColor = false;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // grdInvoiceNote
            // 
            this.grdInvoiceNote.AllowUserToAddRows = false;
            this.grdInvoiceNote.AllowUserToDeleteRows = false;
            this.grdInvoiceNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdInvoiceNote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdInvoiceNote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInvoiceNote.Location = new System.Drawing.Point(0, 0);
            this.grdInvoiceNote.MultiSelect = false;
            this.grdInvoiceNote.Name = "grdInvoiceNote";
            this.grdInvoiceNote.ReadOnly = true;
            this.grdInvoiceNote.RowHeadersVisible = false;
            this.grdInvoiceNote.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdInvoiceNote.Size = new System.Drawing.Size(784, 196);
            this.grdInvoiceNote.TabIndex = 0;
            this.grdInvoiceNote.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdInvoiceNote_CellMouseDoubleClick);
            this.grdInvoiceNote.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdInvoiceNote_DataBindingComplete);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(697, 207);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(354, 207);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(173, 23);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete Invoice Note";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEditInvoiceNote
            // 
            this.btnEditInvoiceNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditInvoiceNote.BackColor = System.Drawing.Color.DarkOrange;
            this.btnEditInvoiceNote.Location = new System.Drawing.Point(183, 207);
            this.btnEditInvoiceNote.Name = "btnEditInvoiceNote";
            this.btnEditInvoiceNote.Size = new System.Drawing.Size(165, 23);
            this.btnEditInvoiceNote.TabIndex = 16;
            this.btnEditInvoiceNote.Text = "Edit Invoice Note";
            this.btnEditInvoiceNote.UseVisualStyleBackColor = false;
            this.btnEditInvoiceNote.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // ManageInvoiceNoteReferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 237);
            this.Controls.Add(this.grdInvoiceNote);
            this.Controls.Add(this.btnEditInvoiceNote);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageInvoiceNoteReferenceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Invoice Notes Reference";
            this.Load += new System.EventHandler(this.ManageInvoiceNoteReferenceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoiceNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridView grdInvoiceNote;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEditInvoiceNote;
    }
}