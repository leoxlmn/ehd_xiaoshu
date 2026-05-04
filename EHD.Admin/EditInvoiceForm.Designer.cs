namespace EHD.Admin {
    partial class EditInvoiceForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditInvoiceForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtStatementDate = new System.Windows.Forms.DateTimePicker();
            this.drpTherapist = new System.Windows.Forms.ComboBox();
            this.lblBillTo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.drpRegistration = new System.Windows.Forms.ComboBox();
            this.lnkManageRegistration = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHSTNumber = new System.Windows.Forms.TextBox();
            this.txtInvoiceTitle = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtInvoiceNote = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grdInvoiceItem = new System.Windows.Forms.DataGridView();
            this.btnAddInvoiceItem = new System.Windows.Forms.Button();
            this.btnOpenToWord = new System.Windows.Forms.Button();
            this.printWorker = new System.ComponentModel.BackgroundWorker();
            this.btnSelectNote = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.drpTreatmentType = new System.Windows.Forms.ComboBox();
            this.btnDeleteInvoiceItem = new System.Windows.Forms.Button();
            this.btnEditInvoiceItem = new System.Windows.Forms.Button();
            this.grpAudit = new System.Windows.Forms.GroupBox();
            this.lblUpdatedTime = new System.Windows.Forms.Label();
            this.lblUpdatedBy = new System.Windows.Forms.Label();
            this.lblCreatedTime = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtRegistration = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTaxName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSetTax = new System.Windows.Forms.Button();
            this.numTaxPercent = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblTaxName = new System.Windows.Forms.Label();
            this.lblTaxTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoiceItem)).BeginInit();
            this.grpAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTaxPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(620, 497);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bill To:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(32, 497);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtStatementDate
            // 
            this.dtStatementDate.CustomFormat = "MMM dd, yyyy";
            this.dtStatementDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtStatementDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStatementDate.Location = new System.Drawing.Point(423, 2);
            this.dtStatementDate.Name = "dtStatementDate";
            this.dtStatementDate.Size = new System.Drawing.Size(143, 22);
            this.dtStatementDate.TabIndex = 3;
            this.dtStatementDate.ValueChanged += new System.EventHandler(this.dtStatementDate_ValueChanged);
            // 
            // drpTherapist
            // 
            this.drpTherapist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpTherapist.FormattingEnabled = true;
            this.drpTherapist.Location = new System.Drawing.Point(423, 113);
            this.drpTherapist.Name = "drpTherapist";
            this.drpTherapist.Size = new System.Drawing.Size(143, 21);
            this.drpTherapist.TabIndex = 42;
            this.drpTherapist.SelectedIndexChanged += new System.EventHandler(this.drpTherapist_SelectedIndexChanged);
            // 
            // lblBillTo
            // 
            this.lblBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBillTo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillTo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblBillTo.Location = new System.Drawing.Point(32, 138);
            this.lblBillTo.Name = "lblBillTo";
            this.lblBillTo.Size = new System.Drawing.Size(242, 69);
            this.lblBillTo.TabIndex = 44;
            this.lblBillTo.Text = "Patient";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(359, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Therapist:";
            // 
            // drpRegistration
            // 
            this.drpRegistration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpRegistration.FormattingEnabled = true;
            this.drpRegistration.Location = new System.Drawing.Point(423, 140);
            this.drpRegistration.Name = "drpRegistration";
            this.drpRegistration.Size = new System.Drawing.Size(272, 21);
            this.drpRegistration.TabIndex = 47;
            this.drpRegistration.SelectedIndexChanged += new System.EventHandler(this.drpRegistration_SelectedIndexChanged);
            // 
            // lnkManageRegistration
            // 
            this.lnkManageRegistration.AutoSize = true;
            this.lnkManageRegistration.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkManageRegistration.Location = new System.Drawing.Point(293, 143);
            this.lnkManageRegistration.Name = "lnkManageRegistration";
            this.lnkManageRegistration.Size = new System.Drawing.Size(124, 13);
            this.lnkManageRegistration.TabIndex = 49;
            this.lnkManageRegistration.TabStop = true;
            this.lnkManageRegistration.Text = "Therapist Registration:";
            this.lnkManageRegistration.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkManageRegistration_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(293, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "Selected Registrations:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(360, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Invoice #:";
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(423, 30);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(143, 22);
            this.txtInvoiceNumber.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(327, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Statement Date:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(371, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "H.S.T #:";
            // 
            // txtHSTNumber
            // 
            this.txtHSTNumber.Location = new System.Drawing.Point(423, 58);
            this.txtHSTNumber.MaxLength = 15;
            this.txtHSTNumber.Name = "txtHSTNumber";
            this.txtHSTNumber.Size = new System.Drawing.Size(143, 22);
            this.txtHSTNumber.TabIndex = 56;
            // 
            // txtInvoiceTitle
            // 
            this.txtInvoiceTitle.Location = new System.Drawing.Point(31, 25);
            this.txtInvoiceTitle.Multiline = true;
            this.txtInvoiceTitle.Name = "txtInvoiceTitle";
            this.txtInvoiceTitle.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInvoiceTitle.Size = new System.Drawing.Size(243, 90);
            this.txtInvoiceTitle.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(29, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "Invoice Title:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(29, 211);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 60;
            this.label15.Text = "Invoice Note:";
            // 
            // txtInvoiceNote
            // 
            this.txtInvoiceNote.Location = new System.Drawing.Point(32, 227);
            this.txtInvoiceNote.Multiline = true;
            this.txtInvoiceNote.Name = "txtInvoiceNote";
            this.txtInvoiceNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInvoiceNote.Size = new System.Drawing.Size(242, 86);
            this.txtInvoiceNote.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 316);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Invoice Details:";
            // 
            // grdInvoiceItem
            // 
            this.grdInvoiceItem.AllowUserToAddRows = false;
            this.grdInvoiceItem.AllowUserToDeleteRows = false;
            this.grdInvoiceItem.AllowUserToResizeRows = false;
            this.grdInvoiceItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdInvoiceItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdInvoiceItem.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdInvoiceItem.BackgroundColor = System.Drawing.Color.White;
            this.grdInvoiceItem.CausesValidation = false;
            this.grdInvoiceItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInvoiceItem.Location = new System.Drawing.Point(32, 332);
            this.grdInvoiceItem.MultiSelect = false;
            this.grdInvoiceItem.Name = "grdInvoiceItem";
            this.grdInvoiceItem.ReadOnly = true;
            this.grdInvoiceItem.RowHeadersWidth = 25;
            this.grdInvoiceItem.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdInvoiceItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdInvoiceItem.ShowEditingIcon = false;
            this.grdInvoiceItem.Size = new System.Drawing.Size(663, 106);
            this.grdInvoiceItem.TabIndex = 63;
            this.grdInvoiceItem.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdInvoiceItem_CellMouseDoubleClick);
            this.grdInvoiceItem.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdInvoiceItem_DataBindingComplete);
            // 
            // btnAddInvoiceItem
            // 
            this.btnAddInvoiceItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddInvoiceItem.BackgroundImage")));
            this.btnAddInvoiceItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddInvoiceItem.Location = new System.Drawing.Point(3, 332);
            this.btnAddInvoiceItem.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddInvoiceItem.Name = "btnAddInvoiceItem";
            this.btnAddInvoiceItem.Size = new System.Drawing.Size(26, 26);
            this.btnAddInvoiceItem.TabIndex = 64;
            this.btnAddInvoiceItem.TabStop = false;
            this.btnAddInvoiceItem.UseVisualStyleBackColor = true;
            this.btnAddInvoiceItem.Click += new System.EventHandler(this.btnAddInvoiceItem_Click);
            // 
            // btnOpenToWord
            // 
            this.btnOpenToWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenToWord.Location = new System.Drawing.Point(113, 497);
            this.btnOpenToWord.Name = "btnOpenToWord";
            this.btnOpenToWord.Size = new System.Drawing.Size(152, 23);
            this.btnOpenToWord.TabIndex = 65;
            this.btnOpenToWord.Text = "Open to Word";
            this.btnOpenToWord.UseVisualStyleBackColor = true;
            this.btnOpenToWord.Click += new System.EventHandler(this.btnOpenToWord_Click);
            // 
            // printWorker
            // 
            this.printWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.printWorker_DoWork);
            this.printWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.printWorker_RunWorkerCompleted);
            // 
            // btnSelectNote
            // 
            this.btnSelectNote.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSelectNote.BackgroundImage")));
            this.btnSelectNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectNote.Location = new System.Drawing.Point(280, 227);
            this.btnSelectNote.Name = "btnSelectNote";
            this.btnSelectNote.Size = new System.Drawing.Size(28, 26);
            this.btnSelectNote.TabIndex = 66;
            this.btnSelectNote.UseVisualStyleBackColor = true;
            this.btnSelectNote.Click += new System.EventHandler(this.btnSelectNote_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(326, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Treatment Type:";
            // 
            // drpTreatmentType
            // 
            this.drpTreatmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpTreatmentType.FormattingEnabled = true;
            this.drpTreatmentType.Location = new System.Drawing.Point(423, 86);
            this.drpTreatmentType.Name = "drpTreatmentType";
            this.drpTreatmentType.Size = new System.Drawing.Size(143, 21);
            this.drpTreatmentType.TabIndex = 68;
            this.drpTreatmentType.SelectedIndexChanged += new System.EventHandler(this.drpTreatmentType_SelectedIndexChanged);
            // 
            // btnDeleteInvoiceItem
            // 
            this.btnDeleteInvoiceItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteInvoiceItem.BackgroundImage")));
            this.btnDeleteInvoiceItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteInvoiceItem.Location = new System.Drawing.Point(3, 358);
            this.btnDeleteInvoiceItem.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteInvoiceItem.Name = "btnDeleteInvoiceItem";
            this.btnDeleteInvoiceItem.Size = new System.Drawing.Size(26, 26);
            this.btnDeleteInvoiceItem.TabIndex = 69;
            this.btnDeleteInvoiceItem.TabStop = false;
            this.btnDeleteInvoiceItem.UseVisualStyleBackColor = true;
            this.btnDeleteInvoiceItem.Click += new System.EventHandler(this.btnDeleteInvoiceItem_Click);
            // 
            // btnEditInvoiceItem
            // 
            this.btnEditInvoiceItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEditInvoiceItem.BackgroundImage")));
            this.btnEditInvoiceItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditInvoiceItem.Location = new System.Drawing.Point(3, 384);
            this.btnEditInvoiceItem.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditInvoiceItem.Name = "btnEditInvoiceItem";
            this.btnEditInvoiceItem.Size = new System.Drawing.Size(26, 26);
            this.btnEditInvoiceItem.TabIndex = 70;
            this.btnEditInvoiceItem.TabStop = false;
            this.btnEditInvoiceItem.UseVisualStyleBackColor = true;
            this.btnEditInvoiceItem.Click += new System.EventHandler(this.btnEditInvoiceItem_Click);
            // 
            // grpAudit
            // 
            this.grpAudit.Controls.Add(this.lblUpdatedTime);
            this.grpAudit.Controls.Add(this.lblUpdatedBy);
            this.grpAudit.Controls.Add(this.lblCreatedTime);
            this.grpAudit.Controls.Add(this.lblCreatedBy);
            this.grpAudit.Controls.Add(this.label13);
            this.grpAudit.Controls.Add(this.label12);
            this.grpAudit.Controls.Add(this.label11);
            this.grpAudit.Controls.Add(this.label10);
            this.grpAudit.Location = new System.Drawing.Point(423, 238);
            this.grpAudit.Name = "grpAudit";
            this.grpAudit.Size = new System.Drawing.Size(269, 75);
            this.grpAudit.TabIndex = 71;
            this.grpAudit.TabStop = false;
            // 
            // lblUpdatedTime
            // 
            this.lblUpdatedTime.AutoSize = true;
            this.lblUpdatedTime.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblUpdatedTime.Location = new System.Drawing.Point(94, 57);
            this.lblUpdatedTime.Name = "lblUpdatedTime";
            this.lblUpdatedTime.Size = new System.Drawing.Size(76, 13);
            this.lblUpdatedTime.TabIndex = 7;
            this.lblUpdatedTime.Text = "Updated Time:";
            // 
            // lblUpdatedBy
            // 
            this.lblUpdatedBy.AutoSize = true;
            this.lblUpdatedBy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdatedBy.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblUpdatedBy.Location = new System.Drawing.Point(94, 44);
            this.lblUpdatedBy.Name = "lblUpdatedBy";
            this.lblUpdatedBy.Size = new System.Drawing.Size(64, 13);
            this.lblUpdatedBy.TabIndex = 6;
            this.lblUpdatedBy.Text = "Updated By:";
            // 
            // lblCreatedTime
            // 
            this.lblCreatedTime.AutoSize = true;
            this.lblCreatedTime.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedTime.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCreatedTime.Location = new System.Drawing.Point(94, 31);
            this.lblCreatedTime.Name = "lblCreatedTime";
            this.lblCreatedTime.Size = new System.Drawing.Size(72, 13);
            this.lblCreatedTime.TabIndex = 5;
            this.lblCreatedTime.Text = "Created Time:";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblCreatedBy.Location = new System.Drawing.Point(94, 18);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(60, 13);
            this.lblCreatedBy.TabIndex = 4;
            this.lblCreatedBy.Text = "Created By:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Created Time:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Updated By:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Created By:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Created By:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(573, 474);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 13);
            this.label14.TabIndex = 72;
            this.label14.Text = "Total:";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(614, 474);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(65, 15);
            this.lblTotal.TabIndex = 73;
            this.lblTotal.Text = "0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtRegistration
            // 
            this.txtRegistration.Location = new System.Drawing.Point(423, 167);
            this.txtRegistration.Multiline = true;
            this.txtRegistration.Name = "txtRegistration";
            this.txtRegistration.Size = new System.Drawing.Size(269, 54);
            this.txtRegistration.TabIndex = 74;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(436, 457);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 13);
            this.label16.TabIndex = 76;
            this.label16.Text = "%";
            // 
            // txtTaxName
            // 
            this.txtTaxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTaxName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxName.Location = new System.Drawing.Point(308, 454);
            this.txtTaxName.MaxLength = 5;
            this.txtTaxName.Name = "txtTaxName";
            this.txtTaxName.Size = new System.Drawing.Size(50, 22);
            this.txtTaxName.TabIndex = 77;
            this.txtTaxName.Text = "Tax";
            this.txtTaxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTaxName.TextChanged += new System.EventHandler(this.txtTaxName_TextChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(364, 457);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(10, 13);
            this.label17.TabIndex = 78;
            this.label17.Text = ":";
            // 
            // btnSetTax
            // 
            this.btnSetTax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetTax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSetTax.Location = new System.Drawing.Point(459, 453);
            this.btnSetTax.Name = "btnSetTax";
            this.btnSetTax.Size = new System.Drawing.Size(87, 23);
            this.btnSetTax.TabIndex = 79;
            this.btnSetTax.Text = "Set Tax";
            this.btnSetTax.UseVisualStyleBackColor = true;
            this.btnSetTax.Click += new System.EventHandler(this.btnSetTax_Click);
            // 
            // numTaxPercent
            // 
            this.numTaxPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numTaxPercent.Location = new System.Drawing.Point(380, 454);
            this.numTaxPercent.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numTaxPercent.Name = "numTaxPercent";
            this.numTaxPercent.Size = new System.Drawing.Size(50, 22);
            this.numTaxPercent.TabIndex = 80;
            this.numTaxPercent.ValueChanged += new System.EventHandler(this.numTaxPercent_ValueChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(550, 441);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 13);
            this.label18.TabIndex = 81;
            this.label18.Text = "Sub Total:";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(614, 441);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(65, 15);
            this.lblSubTotal.TabIndex = 82;
            this.lblSubTotal.Text = "0";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTaxName
            // 
            this.lblTaxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaxName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxName.Location = new System.Drawing.Point(552, 457);
            this.lblTaxName.Name = "lblTaxName";
            this.lblTaxName.Size = new System.Drawing.Size(56, 13);
            this.lblTaxName.TabIndex = 83;
            this.lblTaxName.Text = "Tax:";
            this.lblTaxName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTaxTotal
            // 
            this.lblTaxTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTaxTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxTotal.Location = new System.Drawing.Point(614, 457);
            this.lblTaxTotal.Name = "lblTaxTotal";
            this.lblTaxTotal.Size = new System.Drawing.Size(65, 15);
            this.lblTaxTotal.TabIndex = 84;
            this.lblTaxTotal.Text = "0";
            this.lblTaxTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // EditInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(704, 532);
            this.Controls.Add(this.lblTaxTotal);
            this.Controls.Add(this.lblTaxName);
            this.Controls.Add(this.lblSubTotal);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.numTaxPercent);
            this.Controls.Add(this.btnSetTax);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtTaxName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtRegistration);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.grpAudit);
            this.Controls.Add(this.btnEditInvoiceItem);
            this.Controls.Add(this.btnDeleteInvoiceItem);
            this.Controls.Add(this.drpTreatmentType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSelectNote);
            this.Controls.Add(this.btnOpenToWord);
            this.Controls.Add(this.btnAddInvoiceItem);
            this.Controls.Add(this.grdInvoiceItem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtInvoiceNote);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtInvoiceTitle);
            this.Controls.Add(this.txtHSTNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtInvoiceNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lnkManageRegistration);
            this.Controls.Add(this.drpRegistration);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBillTo);
            this.Controls.Add(this.drpTherapist);
            this.Controls.Add(this.dtStatementDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EditInvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Invoice";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditInvoiceForm_FormClosed);
            this.Load += new System.EventHandler(this.EditInvoiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoiceItem)).EndInit();
            this.grpAudit.ResumeLayout(false);
            this.grpAudit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTaxPercent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtStatementDate;
        private System.Windows.Forms.ComboBox drpTherapist;
        private System.Windows.Forms.Label lblBillTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox drpRegistration;
        private System.Windows.Forms.LinkLabel lnkManageRegistration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHSTNumber;
        private System.Windows.Forms.TextBox txtInvoiceTitle;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtInvoiceNote;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView grdInvoiceItem;
        private System.Windows.Forms.Button btnAddInvoiceItem;
        private System.Windows.Forms.Button btnOpenToWord;
        private System.ComponentModel.BackgroundWorker printWorker;
        private System.Windows.Forms.Button btnSelectNote;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox drpTreatmentType;
        private System.Windows.Forms.Button btnDeleteInvoiceItem;
        private System.Windows.Forms.Button btnEditInvoiceItem;
        private System.Windows.Forms.GroupBox grpAudit;
        private System.Windows.Forms.Label lblUpdatedTime;
        private System.Windows.Forms.Label lblUpdatedBy;
        private System.Windows.Forms.Label lblCreatedTime;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtRegistration;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTaxName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnSetTax;
        private System.Windows.Forms.NumericUpDown numTaxPercent;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblTaxName;
        private System.Windows.Forms.Label lblTaxTotal;
    }
}