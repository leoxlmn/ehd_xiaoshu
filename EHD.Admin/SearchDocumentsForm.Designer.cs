namespace EHD.Admin {
    partial class SearchDocumentsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDocumentsForm));
            this.ttButton = new System.Windows.Forms.ToolTip(this.components);
            this.btnEditInvoiceItem = new System.Windows.Forms.Button();
            this.btnDeleteDocument = new System.Windows.Forms.Button();
            this.btnAddDocument = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.dtCreationTo = new System.Windows.Forms.DateTimePicker();
            this.dtCreationFrom = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.drpDocumentType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocumentNote = new System.Windows.Forms.TextBox();
            this.txtDocumentName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtLoadedTo = new System.Windows.Forms.DateTimePicker();
            this.dtLoadedFrom = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.grdDocuments = new System.Windows.Forms.DataGridView();
            this.lblDocumentCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pager = new EHD.Admin.UCPager();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEditInvoiceItem
            // 
            this.btnEditInvoiceItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditInvoiceItem.BackgroundImage")));
            this.btnEditInvoiceItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditInvoiceItem.Location = new System.Drawing.Point(0, 184);
            this.btnEditInvoiceItem.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditInvoiceItem.Name = "btnEditInvoiceItem";
            this.btnEditInvoiceItem.Size = new System.Drawing.Size(26, 26);
            this.btnEditInvoiceItem.TabIndex = 20;
            this.btnEditInvoiceItem.TabStop = false;
            this.ttButton.SetToolTip(this.btnEditInvoiceItem, "Edit");
            this.btnEditInvoiceItem.UseVisualStyleBackColor = true;
            this.btnEditInvoiceItem.Click += new System.EventHandler(this.btnEditDocument_Click);
            // 
            // btnDeleteDocument
            // 
            this.btnDeleteDocument.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteDocument.BackgroundImage")));
            this.btnDeleteDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteDocument.Location = new System.Drawing.Point(0, 158);
            this.btnDeleteDocument.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteDocument.Name = "btnDeleteDocument";
            this.btnDeleteDocument.Size = new System.Drawing.Size(26, 26);
            this.btnDeleteDocument.TabIndex = 19;
            this.btnDeleteDocument.TabStop = false;
            this.ttButton.SetToolTip(this.btnDeleteDocument, "Delete a document");
            this.btnDeleteDocument.UseVisualStyleBackColor = true;
            this.btnDeleteDocument.Click += new System.EventHandler(this.btnDeleteDocument_Click);
            // 
            // btnAddDocument
            // 
            this.btnAddDocument.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddDocument.BackgroundImage")));
            this.btnAddDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddDocument.Location = new System.Drawing.Point(0, 132);
            this.btnAddDocument.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddDocument.Name = "btnAddDocument";
            this.btnAddDocument.Size = new System.Drawing.Size(26, 26);
            this.btnAddDocument.TabIndex = 18;
            this.btnAddDocument.TabStop = false;
            this.ttButton.SetToolTip(this.btnAddDocument, "Add a document");
            this.btnAddDocument.UseVisualStyleBackColor = true;
            this.btnAddDocument.Click += new System.EventHandler(this.btnAddDocument_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.dtCreationTo);
            this.panel1.Controls.Add(this.dtCreationFrom);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.drpDocumentType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtDocumentNote);
            this.panel1.Controls.Add(this.txtDocumentName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSearchPatient);
            this.panel1.Controls.Add(this.txtPatient);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dtLoadedTo);
            this.panel1.Controls.Add(this.dtLoadedFrom);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 104);
            this.panel1.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(351, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Creation Time From:";
            // 
            // dtCreationTo
            // 
            this.dtCreationTo.Checked = false;
            this.dtCreationTo.CustomFormat = "MMM dd, yyyy";
            this.dtCreationTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCreationTo.Location = new System.Drawing.Point(654, 68);
            this.dtCreationTo.Name = "dtCreationTo";
            this.dtCreationTo.ShowCheckBox = true;
            this.dtCreationTo.Size = new System.Drawing.Size(127, 22);
            this.dtCreationTo.TabIndex = 17;
            this.dtCreationTo.ValueChanged += new System.EventHandler(this.dtCreationTo_ValueChanged);
            // 
            // dtCreationFrom
            // 
            this.dtCreationFrom.Checked = false;
            this.dtCreationFrom.CustomFormat = "MMM dd, yyyy";
            this.dtCreationFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCreationFrom.Location = new System.Drawing.Point(477, 68);
            this.dtCreationFrom.Name = "dtCreationFrom";
            this.dtCreationFrom.ShowCheckBox = true;
            this.dtCreationFrom.Size = new System.Drawing.Size(131, 22);
            this.dtCreationFrom.TabIndex = 15;
            this.dtCreationFrom.ValueChanged += new System.EventHandler(this.dtCreationFrom_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(626, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "To";
            // 
            // drpDocumentType
            // 
            this.drpDocumentType.FormattingEnabled = true;
            this.drpDocumentType.Location = new System.Drawing.Point(477, 12);
            this.drpDocumentType.Name = "drpDocumentType";
            this.drpDocumentType.Size = new System.Drawing.Size(304, 21);
            this.drpDocumentType.TabIndex = 5;
            this.drpDocumentType.SelectedIndexChanged += new System.EventHandler(this.drpDocumentType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(371, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Document Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Document Note:";
            // 
            // txtDocumentNote
            // 
            this.txtDocumentNote.Location = new System.Drawing.Point(113, 68);
            this.txtDocumentNote.Name = "txtDocumentNote";
            this.txtDocumentNote.Size = new System.Drawing.Size(192, 22);
            this.txtDocumentNote.TabIndex = 13;
            this.txtDocumentNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumentNote_KeyPress);
            // 
            // txtDocumentName
            // 
            this.txtDocumentName.Location = new System.Drawing.Point(113, 40);
            this.txtDocumentName.Name = "txtDocumentName";
            this.txtDocumentName.Size = new System.Drawing.Size(192, 22);
            this.txtDocumentName.TabIndex = 7;
            this.txtDocumentName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumentName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Document Name:";
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.BackgroundImage")));
            this.btnSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPatient.Location = new System.Drawing.Point(311, 8);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(28, 26);
            this.btnSearchPatient.TabIndex = 3;
            this.btnSearchPatient.UseVisualStyleBackColor = true;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // txtPatient
            // 
            this.txtPatient.Location = new System.Drawing.Point(113, 12);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.Size = new System.Drawing.Size(192, 22);
            this.txtPatient.TabIndex = 2;
            this.txtPatient.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatient_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(56, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Patient:";
            // 
            // dtLoadedTo
            // 
            this.dtLoadedTo.Checked = false;
            this.dtLoadedTo.CustomFormat = "MMM dd, yyyy";
            this.dtLoadedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLoadedTo.Location = new System.Drawing.Point(654, 40);
            this.dtLoadedTo.Name = "dtLoadedTo";
            this.dtLoadedTo.ShowCheckBox = true;
            this.dtLoadedTo.Size = new System.Drawing.Size(127, 22);
            this.dtLoadedTo.TabIndex = 11;
            this.dtLoadedTo.ValueChanged += new System.EventHandler(this.dtLoadedTo_ValueChanged);
            // 
            // dtLoadedFrom
            // 
            this.dtLoadedFrom.Checked = false;
            this.dtLoadedFrom.CustomFormat = "MMM dd, yyyy";
            this.dtLoadedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtLoadedFrom.Location = new System.Drawing.Point(477, 40);
            this.dtLoadedFrom.Name = "dtLoadedFrom";
            this.dtLoadedFrom.ShowCheckBox = true;
            this.dtLoadedFrom.Size = new System.Drawing.Size(131, 22);
            this.dtLoadedFrom.TabIndex = 9;
            this.dtLoadedFrom.ValueChanged += new System.EventHandler(this.dtLoadedFrom_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(387, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Loaded From:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(626, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "To";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(712, 447);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
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
            this.grdDocuments.Location = new System.Drawing.Point(27, 130);
            this.grdDocuments.MultiSelect = false;
            this.grdDocuments.Name = "grdDocuments";
            this.grdDocuments.ReadOnly = true;
            this.grdDocuments.RowHeadersVisible = false;
            this.grdDocuments.RowHeadersWidth = 25;
            this.grdDocuments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDocuments.ShowEditingIcon = false;
            this.grdDocuments.Size = new System.Drawing.Size(766, 278);
            this.grdDocuments.TabIndex = 21;
            this.grdDocuments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDocuments_CellContentClick);
            this.grdDocuments.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdDocuments_ColumnHeaderMouseClick);
            this.grdDocuments.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdDocuments_DataBindingComplete);
            // 
            // lblDocumentCount
            // 
            this.lblDocumentCount.AutoSize = true;
            this.lblDocumentCount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumentCount.ForeColor = System.Drawing.Color.Blue;
            this.lblDocumentCount.Location = new System.Drawing.Point(701, 112);
            this.lblDocumentCount.Name = "lblDocumentCount";
            this.lblDocumentCount.Size = new System.Drawing.Size(15, 16);
            this.lblDocumentCount.TabIndex = 79;
            this.lblDocumentCount.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(566, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 13);
            this.label9.TabIndex = 78;
            this.label9.Text = "Number of Documents:";
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pager.Location = new System.Drawing.Point(385, 411);
            this.pager.Margin = new System.Windows.Forms.Padding(0);
            this.pager.Name = "pager";
            this.pager.Page = 1;
            this.pager.PageSize = 20;
            this.pager.Size = new System.Drawing.Size(402, 33);
            this.pager.TabIndex = 22;
            this.pager.TotalPage = 0;
            // 
            // SearchDocumentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(793, 475);
            this.Controls.Add(this.lblDocumentCount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnEditInvoiceItem);
            this.Controls.Add(this.pager);
            this.Controls.Add(this.btnDeleteDocument);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddDocument);
            this.Controls.Add(this.grdDocuments);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SearchDocumentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Documents";
            this.Load += new System.EventHandler(this.SearchDocumentsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtCreationTo;
        private System.Windows.Forms.DateTimePicker dtCreationFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox drpDocumentType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDocumentNote;
        private System.Windows.Forms.TextBox txtDocumentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchPatient;
        private System.Windows.Forms.TextBox txtPatient;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtLoadedTo;
        private System.Windows.Forms.DateTimePicker dtLoadedFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEditInvoiceItem;
        private UCPager pager;
        private System.Windows.Forms.Button btnDeleteDocument;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddDocument;
        private System.Windows.Forms.DataGridView grdDocuments;
        private System.Windows.Forms.Label lblDocumentCount;
        private System.Windows.Forms.Label label9;
    }
}