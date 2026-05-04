namespace EHD.Admin {
    partial class InvoiceSearchForm {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceSearchForm));
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoServiceAndStatementDates = new System.Windows.Forms.RadioButton();
            this.rdoServiceDateOnly = new System.Windows.Forms.RadioButton();
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.txtPatient = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.ucTherapist = new EHD.Admin.UCCheckBoxDropDown();
            this.ucTreatment = new EHD.Admin.UCCheckBoxDropDown();
            this.ucInsurer = new EHD.Admin.UCCheckBoxDropDown();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pager = new EHD.Admin.UCPager();
            this.trvList = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.rdoExpandAll = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTotalAfterTax = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalTreatment = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPrintInvoiceList = new System.Windows.Forms.Button();
            this.lblPatientNumber = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rdoCollapseAll = new System.Windows.Forms.RadioButton();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAddNewInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAddInvoiceItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditInvoiceItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteInvoiceItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(294, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Insurer:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(460, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Therapist:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Treatment:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rdoServiceAndStatementDates);
            this.panel1.Controls.Add(this.rdoServiceDateOnly);
            this.panel1.Controls.Add(this.btnSearchPatient);
            this.panel1.Controls.Add(this.txtPatient);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dtTo);
            this.panel1.Controls.Add(this.ucTherapist);
            this.panel1.Controls.Add(this.ucTreatment);
            this.panel1.Controls.Add(this.ucInsurer);
            this.panel1.Controls.Add(this.dtFrom);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(817, 104);
            this.panel1.TabIndex = 24;
            // 
            // rdoServiceAndStatementDates
            // 
            this.rdoServiceAndStatementDates.AutoSize = true;
            this.rdoServiceAndStatementDates.Location = new System.Drawing.Point(614, 44);
            this.rdoServiceAndStatementDates.Name = "rdoServiceAndStatementDates";
            this.rdoServiceAndStatementDates.Size = new System.Drawing.Size(189, 17);
            this.rdoServiceAndStatementDates.TabIndex = 9;
            this.rdoServiceAndStatementDates.Text = "Both Statement and Service Dates";
            this.rdoServiceAndStatementDates.UseVisualStyleBackColor = true;
            this.rdoServiceAndStatementDates.CheckedChanged += new System.EventHandler(this.rdoServiceAndStatementDates_CheckedChanged);
            // 
            // rdoServiceDateOnly
            // 
            this.rdoServiceDateOnly.AutoSize = true;
            this.rdoServiceDateOnly.Checked = true;
            this.rdoServiceDateOnly.Location = new System.Drawing.Point(614, 21);
            this.rdoServiceDateOnly.Name = "rdoServiceDateOnly";
            this.rdoServiceDateOnly.Size = new System.Drawing.Size(111, 17);
            this.rdoServiceDateOnly.TabIndex = 8;
            this.rdoServiceDateOnly.TabStop = true;
            this.rdoServiceDateOnly.Text = "Service Date Only";
            this.rdoServiceDateOnly.UseVisualStyleBackColor = true;
            this.rdoServiceDateOnly.CheckedChanged += new System.EventHandler(this.rdoServiceDateOnly_CheckedChanged);
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPatient.BackgroundImage")));
            this.btnSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPatient.Location = new System.Drawing.Point(294, 76);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(28, 26);
            this.btnSearchPatient.TabIndex = 5;
            this.btnSearchPatient.UseVisualStyleBackColor = true;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // txtPatient
            // 
            this.txtPatient.Location = new System.Drawing.Point(96, 80);
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.Size = new System.Drawing.Size(192, 20);
            this.txtPatient.TabIndex = 4;
            this.txtPatient.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatient_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(39, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Patient:";
            // 
            // dtTo
            // 
            this.dtTo.Checked = false;
            this.dtTo.CustomFormat = "MMM dd, yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(488, 30);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowCheckBox = true;
            this.dtTo.Size = new System.Drawing.Size(120, 20);
            this.dtTo.TabIndex = 7;
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // ucTherapist
            // 
            this.ucTherapist.Location = new System.Drawing.Point(96, 55);
            this.ucTherapist.Name = "ucTherapist";
            this.ucTherapist.Size = new System.Drawing.Size(192, 19);
            this.ucTherapist.TabIndex = 3;
            this.ucTherapist.UpdateDataList += new System.EventHandler(this.ucTherapist_UpdateDataList);
            // 
            // ucTreatment
            // 
            this.ucTreatment.Location = new System.Drawing.Point(96, 30);
            this.ucTreatment.Name = "ucTreatment";
            this.ucTreatment.Size = new System.Drawing.Size(192, 19);
            this.ucTreatment.TabIndex = 2;
            this.ucTreatment.UpdateDataList += new System.EventHandler(this.ucTreatment_UpdateDataList);
            // 
            // ucInsurer
            // 
            this.ucInsurer.Location = new System.Drawing.Point(96, 5);
            this.ucInsurer.Name = "ucInsurer";
            this.ucInsurer.Size = new System.Drawing.Size(192, 19);
            this.ucInsurer.TabIndex = 1;
            this.ucInsurer.UpdateDataList += new System.EventHandler(this.ucInsurer_UpdateDataList);
            // 
            // dtFrom
            // 
            this.dtFrom.Checked = false;
            this.dtFrom.CustomFormat = "MMM dd, yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(334, 30);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowCheckBox = true;
            this.dtFrom.Size = new System.Drawing.Size(120, 20);
            this.dtFrom.TabIndex = 6;
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pager);
            this.panel2.Controls.Add(this.trvList);
            this.panel2.Location = new System.Drawing.Point(0, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(817, 283);
            this.panel2.TabIndex = 25;
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pager.Location = new System.Drawing.Point(406, 250);
            this.pager.Margin = new System.Windows.Forms.Padding(0);
            this.pager.Name = "pager";
            this.pager.Page = 1;
            this.pager.PageSize = 20;
            this.pager.Size = new System.Drawing.Size(402, 33);
            this.pager.TabIndex = 13;
            this.pager.TotalPage = 0;
            // 
            // trvList
            // 
            this.trvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvList.BackColor = System.Drawing.Color.White;
            this.trvList.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvList.HotTracking = true;
            this.trvList.ImageIndex = 0;
            this.trvList.ImageList = this.imgList;
            this.trvList.Location = new System.Drawing.Point(0, 3);
            this.trvList.Name = "trvList";
            this.trvList.SelectedImageIndex = 0;
            this.trvList.Size = new System.Drawing.Size(817, 244);
            this.trvList.TabIndex = 0;
            this.trvList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvList_NodeMouseClick);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "Male");
            this.imgList.Images.SetKeyName(1, "Female");
            this.imgList.Images.SetKeyName(2, "Patient");
            this.imgList.Images.SetKeyName(3, "Doctor");
            this.imgList.Images.SetKeyName(4, "InitialTreatment");
            this.imgList.Images.SetKeyName(5, "FollowUpTreatment");
            this.imgList.Images.SetKeyName(6, "Invoice");
            this.imgList.Images.SetKeyName(7, "InvoiceItem");
            // 
            // rdoExpandAll
            // 
            this.rdoExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("rdoExpandAll.Image")));
            this.rdoExpandAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdoExpandAll.Location = new System.Drawing.Point(3, 5);
            this.rdoExpandAll.Name = "rdoExpandAll";
            this.rdoExpandAll.Size = new System.Drawing.Size(101, 24);
            this.rdoExpandAll.TabIndex = 10;
            this.rdoExpandAll.Text = "Expand All";
            this.rdoExpandAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rdoExpandAll.UseVisualStyleBackColor = true;
            this.rdoExpandAll.Click += new System.EventHandler(this.rdoExpandAll_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblTotalAfterTax);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.lblTotalAmount);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.lblTotalTreatment);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.btnPrintInvoiceList);
            this.panel3.Controls.Add(this.lblPatientNumber);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.rdoCollapseAll);
            this.panel3.Controls.Add(this.rdoExpandAll);
            this.panel3.Location = new System.Drawing.Point(0, 106);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(817, 57);
            this.panel3.TabIndex = 53;
            // 
            // lblTotalAfterTax
            // 
            this.lblTotalAfterTax.AutoSize = true;
            this.lblTotalAfterTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalAfterTax.Location = new System.Drawing.Point(539, 35);
            this.lblTotalAfterTax.Name = "lblTotalAfterTax";
            this.lblTotalAfterTax.Size = new System.Drawing.Size(14, 13);
            this.lblTotalAfterTax.TabIndex = 62;
            this.lblTotalAfterTax.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(391, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 13);
            this.label11.TabIndex = 61;
            this.label11.Text = "Total Amount After Tax:";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalAmount.Location = new System.Drawing.Point(539, 11);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(14, 13);
            this.lblTotalAmount.TabIndex = 60;
            this.lblTotalAmount.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(447, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "Total Amount:";
            // 
            // lblTotalTreatment
            // 
            this.lblTotalTreatment.AutoSize = true;
            this.lblTotalTreatment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalTreatment.Location = new System.Drawing.Point(261, 35);
            this.lblTotalTreatment.Name = "lblTotalTreatment";
            this.lblTotalTreatment.Size = new System.Drawing.Size(14, 13);
            this.lblTotalTreatment.TabIndex = 58;
            this.lblTotalTreatment.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(147, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Total Treatments:";
            // 
            // btnPrintInvoiceList
            // 
            this.btnPrintInvoiceList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintInvoiceList.BackgroundImage")));
            this.btnPrintInvoiceList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrintInvoiceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintInvoiceList.Location = new System.Drawing.Point(668, 18);
            this.btnPrintInvoiceList.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrintInvoiceList.Name = "btnPrintInvoiceList";
            this.btnPrintInvoiceList.Size = new System.Drawing.Size(140, 30);
            this.btnPrintInvoiceList.TabIndex = 12;
            this.btnPrintInvoiceList.TabStop = false;
            this.btnPrintInvoiceList.Text = "Print  Invoice List";
            this.btnPrintInvoiceList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintInvoiceList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintInvoiceList.UseVisualStyleBackColor = true;
            this.btnPrintInvoiceList.Click += new System.EventHandler(this.btnPrintInvoiceList_Click);
            // 
            // lblPatientNumber
            // 
            this.lblPatientNumber.AutoSize = true;
            this.lblPatientNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNumber.Location = new System.Drawing.Point(261, 11);
            this.lblPatientNumber.Name = "lblPatientNumber";
            this.lblPatientNumber.Size = new System.Drawing.Size(14, 13);
            this.lblPatientNumber.TabIndex = 55;
            this.lblPatientNumber.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(164, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Total Patients:";
            // 
            // rdoCollapseAll
            // 
            this.rdoCollapseAll.Checked = true;
            this.rdoCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("rdoCollapseAll.Image")));
            this.rdoCollapseAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdoCollapseAll.Location = new System.Drawing.Point(3, 29);
            this.rdoCollapseAll.Name = "rdoCollapseAll";
            this.rdoCollapseAll.Size = new System.Drawing.Size(101, 24);
            this.rdoCollapseAll.TabIndex = 11;
            this.rdoCollapseAll.TabStop = true;
            this.rdoCollapseAll.Text = "Collapse All";
            this.rdoCollapseAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rdoCollapseAll.UseVisualStyleBackColor = true;
            this.rdoCollapseAll.Click += new System.EventHandler(this.rdoCollapseAll_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuView,
            this.toolStripSeparator1,
            this.mnuAddNewInvoice,
            this.mnuEditInvoice,
            this.mnuDeleteInvoice,
            this.toolStripSeparator2,
            this.mnuAddInvoiceItem,
            this.mnuEditInvoiceItem,
            this.mnuDeleteInvoiceItem});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(192, 170);
            // 
            // mnuView
            // 
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(191, 22);
            this.mnuView.Text = "View";
            this.mnuView.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuAddNewInvoice
            // 
            this.mnuAddNewInvoice.Name = "mnuAddNewInvoice";
            this.mnuAddNewInvoice.Size = new System.Drawing.Size(191, 22);
            this.mnuAddNewInvoice.Text = "Add New Invoice";
            this.mnuAddNewInvoice.Click += new System.EventHandler(this.mnuAddNewInvoice_Click);
            // 
            // mnuEditInvoice
            // 
            this.mnuEditInvoice.Name = "mnuEditInvoice";
            this.mnuEditInvoice.Size = new System.Drawing.Size(191, 22);
            this.mnuEditInvoice.Text = "Edit Invoice";
            this.mnuEditInvoice.Click += new System.EventHandler(this.mnuEditInvoice_Click);
            // 
            // mnuDeleteInvoice
            // 
            this.mnuDeleteInvoice.Name = "mnuDeleteInvoice";
            this.mnuDeleteInvoice.Size = new System.Drawing.Size(191, 22);
            this.mnuDeleteInvoice.Text = "Delete Invoice";
            this.mnuDeleteInvoice.Click += new System.EventHandler(this.mnuDeleteInvoice_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuAddInvoiceItem
            // 
            this.mnuAddInvoiceItem.Name = "mnuAddInvoiceItem";
            this.mnuAddInvoiceItem.Size = new System.Drawing.Size(191, 22);
            this.mnuAddInvoiceItem.Text = "Add New Invoice Item";
            this.mnuAddInvoiceItem.Click += new System.EventHandler(this.mnuAddInvoiceItem_Click);
            // 
            // mnuEditInvoiceItem
            // 
            this.mnuEditInvoiceItem.Name = "mnuEditInvoiceItem";
            this.mnuEditInvoiceItem.Size = new System.Drawing.Size(191, 22);
            this.mnuEditInvoiceItem.Text = "Edit Invoice Item";
            this.mnuEditInvoiceItem.Click += new System.EventHandler(this.mnuEditInvoiceItem_Click);
            // 
            // mnuDeleteInvoiceItem
            // 
            this.mnuDeleteInvoiceItem.Name = "mnuDeleteInvoiceItem";
            this.mnuDeleteInvoiceItem.Size = new System.Drawing.Size(191, 22);
            this.mnuDeleteInvoiceItem.Text = "Delete Invoice Item";
            this.mnuDeleteInvoiceItem.Click += new System.EventHandler(this.mnuDeleteInvoiceItem_Click);
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // InvoiceSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 445);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InvoiceSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Invoice Search";
            this.Load += new System.EventHandler(this.InvoiceSearchForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtTo;
        private UCCheckBoxDropDown ucTherapist;
        private UCCheckBoxDropDown ucTreatment;
        private UCCheckBoxDropDown ucInsurer;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.TreeView trvList;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.RadioButton rdoExpandAll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdoCollapseAll;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuAddNewInvoice;
        private System.Windows.Forms.ToolStripMenuItem mnuEditInvoice;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteInvoice;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuAddInvoiceItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEditInvoiceItem;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteInvoiceItem;
        private System.Windows.Forms.Label lblPatientNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPatient;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSearchPatient;
        private System.Windows.Forms.Button btnPrintInvoiceList;
        private System.Drawing.Printing.PrintDocument printDoc;
        private UCPager pager;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalTreatment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotalAfterTax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton rdoServiceAndStatementDates;
        private System.Windows.Forms.RadioButton rdoServiceDateOnly;
    }
}