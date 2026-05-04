namespace EHD.Admin {
    partial class EditDocumentsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDocumentsForm));
            this.ttButton = new System.Windows.Forms.ToolTip(this.components);
            this.btnDeleteDocument = new System.Windows.Forms.Button();
            this.btnAddDocument = new System.Windows.Forms.Button();
            this.lblDocumentCount = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grdDocuments = new System.Windows.Forms.DataGridView();
            this.btnEditInvoiceItem = new System.Windows.Forms.Button();
            this.pager = new EHD.Admin.UCPager();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDeleteDocument
            // 
            this.btnDeleteDocument.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDocument.BackgroundImage")));
            this.btnDeleteDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteDocument.Location = new System.Drawing.Point(4, 64);
            this.btnDeleteDocument.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteDocument.Name = "btnDeleteDocument";
            this.btnDeleteDocument.Size = new System.Drawing.Size(26, 26);
            this.btnDeleteDocument.TabIndex = 78;
            this.btnDeleteDocument.TabStop = false;
            this.ttButton.SetToolTip(this.btnDeleteDocument, "Delete a document");
            this.btnDeleteDocument.UseVisualStyleBackColor = true;
            this.btnDeleteDocument.Click += new System.EventHandler(this.btnDeleteDocument_Click);
            // 
            // btnAddDocument
            // 
            this.btnAddDocument.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDocument.BackgroundImage")));
            this.btnAddDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDocument.Location = new System.Drawing.Point(4, 38);
            this.btnAddDocument.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddDocument.Name = "btnAddDocument";
            this.btnAddDocument.Size = new System.Drawing.Size(26, 26);
            this.btnAddDocument.TabIndex = 71;
            this.btnAddDocument.TabStop = false;
            this.ttButton.SetToolTip(this.btnAddDocument, "Add a document");
            this.btnAddDocument.UseVisualStyleBackColor = true;
            this.btnAddDocument.Click += new System.EventHandler(this.btnAddDocument_Click);
            // 
            // lblDocumentCount
            // 
            this.lblDocumentCount.AutoSize = true;
            this.lblDocumentCount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentCount.ForeColor = System.Drawing.Color.Blue;
            this.lblDocumentCount.Location = new System.Drawing.Point(498, 17);
            this.lblDocumentCount.Name = "lblDocumentCount";
            this.lblDocumentCount.Size = new System.Drawing.Size(15, 16);
            this.lblDocumentCount.TabIndex = 77;
            this.lblDocumentCount.Text = "0";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientName.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientName.Location = new System.Drawing.Point(115, 17);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(94, 16);
            this.lblPatientName.TabIndex = 76;
            this.lblPatientName.Text = "Patient Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(363, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Number of Documents:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(686, 380);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 75;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Patient Name:";
            // 
            // grdDocuments
            // 
            this.grdDocuments.AllowUserToAddRows = false;
            this.grdDocuments.AllowUserToDeleteRows = false;
            this.grdDocuments.AllowUserToResizeRows = false;
            this.grdDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDocuments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdDocuments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdDocuments.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdDocuments.CausesValidation = false;
            this.grdDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDocuments.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.grdDocuments.Location = new System.Drawing.Point(31, 38);
            this.grdDocuments.MultiSelect = false;
            this.grdDocuments.Name = "grdDocuments";
            this.grdDocuments.ReadOnly = true;
            this.grdDocuments.RowHeadersVisible = false;
            this.grdDocuments.RowHeadersWidth = 25;
            this.grdDocuments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDocuments.ShowEditingIcon = false;
            this.grdDocuments.Size = new System.Drawing.Size(730, 303);
            this.grdDocuments.TabIndex = 74;
            this.grdDocuments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDocuments_CellContentClick);
            this.grdDocuments.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdDocuments_ColumnHeaderMouseClick);
            this.grdDocuments.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdDocuments_DataBindingComplete);
            // 
            // btnEditInvoiceItem
            // 
            this.btnEditInvoiceItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditInvoiceItem.BackgroundImage")));
            this.btnEditInvoiceItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditInvoiceItem.Location = new System.Drawing.Point(4, 90);
            this.btnEditInvoiceItem.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditInvoiceItem.Name = "btnEditInvoiceItem";
            this.btnEditInvoiceItem.Size = new System.Drawing.Size(26, 26);
            this.btnEditInvoiceItem.TabIndex = 80;
            this.btnEditInvoiceItem.TabStop = false;
            this.ttButton.SetToolTip(this.btnEditInvoiceItem, "Edit");
            this.btnEditInvoiceItem.UseVisualStyleBackColor = true;
            this.btnEditInvoiceItem.Click += new System.EventHandler(this.btnEditDocument_Click);
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pager.Location = new System.Drawing.Point(359, 344);
            this.pager.Margin = new System.Windows.Forms.Padding(0);
            this.pager.Name = "pager";
            this.pager.Page = 1;
            this.pager.PageSize = 20;
            this.pager.Size = new System.Drawing.Size(402, 33);
            this.pager.TabIndex = 79;
            this.pager.TotalPage = 0;
            // 
            // EditDocumentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(764, 421);
            this.Controls.Add(this.btnEditInvoiceItem);
            this.Controls.Add(this.pager);
            this.Controls.Add(this.btnDeleteDocument);
            this.Controls.Add(this.lblDocumentCount);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddDocument);
            this.Controls.Add(this.grdDocuments);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EditDocumentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Documents";
            this.Load += new System.EventHandler(this.EditDocumentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttButton;
        private System.Windows.Forms.Button btnDeleteDocument;
        private System.Windows.Forms.Label lblDocumentCount;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddDocument;
        private System.Windows.Forms.DataGridView grdDocuments;
        private UCPager pager;
        private System.Windows.Forms.Button btnEditInvoiceItem;
    }
}