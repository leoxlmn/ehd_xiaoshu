namespace EHD.Admin
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbAddPatient = new System.Windows.Forms.ToolStripButton();
            this.tsbPatients = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdvancedSearch = new System.Windows.Forms.ToolStripButton();
            this.tsbInvoice = new System.Windows.Forms.ToolStripButton();
            this.tsbAlert = new System.Windows.Forms.ToolStripButton();
            this.tsbPractitionerCalendar = new System.Windows.Forms.ToolStripButton();
            this.tsbBooking = new System.Windows.Forms.ToolStripButton();
            this.tsbDocuments = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBiggerFont = new System.Windows.Forms.ToolStripButton();
            this.tsbSmallerFont = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.stMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLogonUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.lblSearchResult = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalPatient = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnListFamily = new System.Windows.Forms.Button();
            this.pager = new EHD.Admin.UCPager();
            this.btnSearchPostalCode = new System.Windows.Forms.Button();
            this.txtSearchPostalCode = new System.Windows.Forms.MaskedTextBox();
            this.btnSearchPhone = new System.Windows.Forms.Button();
            this.btnSearchName = new System.Windows.Forms.Button();
            this.txtSearchPhone = new System.Windows.Forms.MaskedTextBox();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.btnDeletePatient = new System.Windows.Forms.Button();
            this.ucInsurer = new EHD.Admin.UCCheckBoxDropDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPicAddPatient = new System.Windows.Forms.Button();
            this.picPatientListLabel = new System.Windows.Forms.PictureBox();
            this.grdPatientList = new System.Windows.Forms.DataGridView();
            this.PatientEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.ViewPatientDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this.PrintPatient = new System.Windows.Forms.DataGridViewImageColumn();
            this.AddInitTreatment = new System.Windows.Forms.DataGridViewImageColumn();
            this.AccountBalance = new System.Windows.Forms.DataGridViewImageColumn();
            this.Document = new System.Windows.Forms.DataGridViewImageColumn();
            this.splBottom = new System.Windows.Forms.SplitContainer();
            this.btnDeleteInitialTreatment = new System.Windows.Forms.Button();
            this.ucTherapist = new EHD.Admin.UCCheckBoxDropDown();
            this.ucTreatment = new EHD.Admin.UCCheckBoxDropDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPicAddInitTreatment = new System.Windows.Forms.Button();
            this.picInitTreatmentListLabel = new System.Windows.Forms.PictureBox();
            this.grdInitialTreatmentList = new System.Windows.Forms.DataGridView();
            this.EditInitialTreatment = new System.Windows.Forms.DataGridViewImageColumn();
            this.ViewInitialTreatmentDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this.PrintInitialTreatment = new System.Windows.Forms.DataGridViewImageColumn();
            this.AddFollowUpTreatment = new System.Windows.Forms.DataGridViewImageColumn();
            this.InitialTreatmentComplete = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnDeleteFollowUp = new System.Windows.Forms.Button();
            this.btnPrintAllSOAPNotes = new System.Windows.Forms.Button();
            this.btnPicAddFollowUpTreatment = new System.Windows.Forms.Button();
            this.picFollowUpTreatmentListLabel = new System.Windows.Forms.PictureBox();
            this.grdFollowUpTreatmentList = new System.Windows.Forms.DataGridView();
            this.EditFollowUpTreatment = new System.Windows.Forms.DataGridViewImageColumn();
            this.ViewFollowUpTreatmentDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this.FollowUpTreatmentComplete = new System.Windows.Forms.DataGridViewImageColumn();
            this.lstIcons = new System.Windows.Forms.ImageList(this.components);
            this.ttButton = new System.Windows.Forms.ToolTip(this.components);
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.tsbReports = new System.Windows.Forms.ToolStripButton();
            this.tsMain.SuspendLayout();
            this.stMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPatientListLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splBottom)).BeginInit();
            this.splBottom.Panel1.SuspendLayout();
            this.splBottom.Panel2.SuspendLayout();
            this.splBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInitTreatmentListLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInitialTreatmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFollowUpTreatmentListLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFollowUpTreatmentList)).BeginInit();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tsMain.AutoSize = false;
            this.tsMain.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMain.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddPatient,
            this.tsbPatients,
            this.toolStripSeparator5,
            this.tsbAdvancedSearch,
            this.tsbInvoice,
            this.tsbAlert,
            this.tsbPractitionerCalendar,
            this.tsbBooking,
            this.tsbDocuments,
            this.tsbReports,
            this.toolStripSeparator2,
            this.tsbSettings,
            this.toolStripSeparator4,
            this.tsbHistory,
            this.toolStripSeparator1,
            this.tsbBiggerFont,
            this.tsbSmallerFont,
            this.tsbExit,
            this.toolStripSeparator3,
            this.tsbHelp,
            this.toolStripSeparator6});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0);
            this.tsMain.Size = new System.Drawing.Size(850, 31);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 2;
            // 
            // tsbAddPatient
            // 
            this.tsbAddPatient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddPatient.Image")));
            this.tsbAddPatient.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAddPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddPatient.Name = "tsbAddPatient";
            this.tsbAddPatient.Size = new System.Drawing.Size(28, 28);
            this.tsbAddPatient.Text = "Add";
            this.tsbAddPatient.ToolTipText = "Add a New Patient";
            this.tsbAddPatient.Visible = false;
            this.tsbAddPatient.Click += new System.EventHandler(this.tsbAddPatient_Click);
            // 
            // tsbPatients
            // 
            this.tsbPatients.Image = ((System.Drawing.Image)(resources.GetObject("tsbPatients.Image")));
            this.tsbPatients.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPatients.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPatients.Name = "tsbPatients";
            this.tsbPatients.Size = new System.Drawing.Size(82, 28);
            this.tsbPatients.Text = "Patients";
            this.tsbPatients.Click += new System.EventHandler(this.tsbPatients_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbAdvancedSearch
            // 
            this.tsbAdvancedSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdvancedSearch.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdvancedSearch.Image")));
            this.tsbAdvancedSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAdvancedSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdvancedSearch.Name = "tsbAdvancedSearch";
            this.tsbAdvancedSearch.Size = new System.Drawing.Size(28, 28);
            this.tsbAdvancedSearch.Text = "Advanced Search";
            this.tsbAdvancedSearch.Click += new System.EventHandler(this.tsbAdvancedSearch_Click);
            // 
            // tsbInvoice
            // 
            this.tsbInvoice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInvoice.Image = ((System.Drawing.Image)(resources.GetObject("tsbInvoice.Image")));
            this.tsbInvoice.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbInvoice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInvoice.Name = "tsbInvoice";
            this.tsbInvoice.Size = new System.Drawing.Size(28, 28);
            this.tsbInvoice.Text = "Invoice";
            this.tsbInvoice.Click += new System.EventHandler(this.tsbInvoice_Click);
            // 
            // tsbAlert
            // 
            this.tsbAlert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAlert.Image = ((System.Drawing.Image)(resources.GetObject("tsbAlert.Image")));
            this.tsbAlert.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAlert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAlert.Name = "tsbAlert";
            this.tsbAlert.Size = new System.Drawing.Size(28, 28);
            this.tsbAlert.Text = "Pending Treatment Alert";
            this.tsbAlert.ToolTipText = "Pending Treatment Alert";
            this.tsbAlert.Click += new System.EventHandler(this.tsbAlert_Click);
            // 
            // tsbPractitionerCalendar
            // 
            this.tsbPractitionerCalendar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPractitionerCalendar.Image = ((System.Drawing.Image)(resources.GetObject("tsbPractitionerCalendar.Image")));
            this.tsbPractitionerCalendar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPractitionerCalendar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPractitionerCalendar.Name = "tsbPractitionerCalendar";
            this.tsbPractitionerCalendar.Size = new System.Drawing.Size(28, 28);
            this.tsbPractitionerCalendar.Text = "Practitioner Calendar";
            this.tsbPractitionerCalendar.Click += new System.EventHandler(this.tsbPractitionerCalendar_Click);
            // 
            // tsbBooking
            // 
            this.tsbBooking.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBooking.Image = ((System.Drawing.Image)(resources.GetObject("tsbBooking.Image")));
            this.tsbBooking.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbBooking.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBooking.Name = "tsbBooking";
            this.tsbBooking.Size = new System.Drawing.Size(28, 28);
            this.tsbBooking.Text = "Booking";
            this.tsbBooking.Click += new System.EventHandler(this.tsbBooking_Click);
            // 
            // tsbDocuments
            // 
            this.tsbDocuments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDocuments.Image = ((System.Drawing.Image)(resources.GetObject("tsbDocuments.Image")));
            this.tsbDocuments.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDocuments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDocuments.Name = "tsbDocuments";
            this.tsbDocuments.Size = new System.Drawing.Size(28, 28);
            this.tsbDocuments.Text = "Documents";
            this.tsbDocuments.Click += new System.EventHandler(this.tsbDocuments_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbSettings
            // 
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(82, 28);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbHistory
            // 
            this.tsbHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsbHistory.Image")));
            this.tsbHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(28, 28);
            this.tsbHistory.Text = "History";
            this.tsbHistory.ToolTipText = "Data Modification History";
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbBiggerFont
            // 
            this.tsbBiggerFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBiggerFont.Image = ((System.Drawing.Image)(resources.GetObject("tsbBiggerFont.Image")));
            this.tsbBiggerFont.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbBiggerFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBiggerFont.Name = "tsbBiggerFont";
            this.tsbBiggerFont.Size = new System.Drawing.Size(28, 28);
            this.tsbBiggerFont.ToolTipText = "Increase Font Size";
            this.tsbBiggerFont.Click += new System.EventHandler(this.tsbBiggerFont_Click);
            // 
            // tsbSmallerFont
            // 
            this.tsbSmallerFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSmallerFont.Image = ((System.Drawing.Image)(resources.GetObject("tsbSmallerFont.Image")));
            this.tsbSmallerFont.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSmallerFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSmallerFont.Name = "tsbSmallerFont";
            this.tsbSmallerFont.Size = new System.Drawing.Size(28, 28);
            this.tsbSmallerFont.Text = "toolStripButton1";
            this.tsbSmallerFont.ToolTipText = "Decrease Font Size";
            this.tsbSmallerFont.Click += new System.EventHandler(this.tsbSmallerFont_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Margin = new System.Windows.Forms.Padding(0, 1, 3, 1);
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tsbExit.Size = new System.Drawing.Size(93, 29);
            this.tsbExit.Text = "Sign Out";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbHelp
            // 
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(28, 28);
            this.tsbHelp.Text = "Help";
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // stMain
            // 
            this.stMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblLogonUser});
            this.stMain.Location = new System.Drawing.Point(0, 615);
            this.stMain.Name = "stMain";
            this.stMain.Size = new System.Drawing.Size(849, 22);
            this.stMain.TabIndex = 2;
            this.stMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel1.Text = "Logon User:";
            // 
            // lblLogonUser
            // 
            this.lblLogonUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogonUser.Name = "lblLogonUser";
            this.lblLogonUser.Size = new System.Drawing.Size(0, 17);
            // 
            // splMain
            // 
            this.splMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splMain.Location = new System.Drawing.Point(0, 34);
            this.splMain.Name = "splMain";
            this.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.lblSearchResult);
            this.splMain.Panel1.Controls.Add(this.label7);
            this.splMain.Panel1.Controls.Add(this.lblTotalPatient);
            this.splMain.Panel1.Controls.Add(this.label6);
            this.splMain.Panel1.Controls.Add(this.btnListFamily);
            this.splMain.Panel1.Controls.Add(this.pager);
            this.splMain.Panel1.Controls.Add(this.btnSearchPostalCode);
            this.splMain.Panel1.Controls.Add(this.txtSearchPostalCode);
            this.splMain.Panel1.Controls.Add(this.btnSearchPhone);
            this.splMain.Panel1.Controls.Add(this.btnSearchName);
            this.splMain.Panel1.Controls.Add(this.txtSearchPhone);
            this.splMain.Panel1.Controls.Add(this.txtSearchName);
            this.splMain.Panel1.Controls.Add(this.btnDeletePatient);
            this.splMain.Panel1.Controls.Add(this.ucInsurer);
            this.splMain.Panel1.Controls.Add(this.label1);
            this.splMain.Panel1.Controls.Add(this.btnPicAddPatient);
            this.splMain.Panel1.Controls.Add(this.picPatientListLabel);
            this.splMain.Panel1.Controls.Add(this.grdPatientList);
            this.splMain.Panel1MinSize = 160;
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.splBottom);
            this.splMain.Panel2MinSize = 160;
            this.splMain.Size = new System.Drawing.Size(849, 578);
            this.splMain.SplitterDistance = 234;
            this.splMain.TabIndex = 4;
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSearchResult.AutoSize = true;
            this.lblSearchResult.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchResult.Location = new System.Drawing.Point(263, 206);
            this.lblSearchResult.Name = "lblSearchResult";
            this.lblSearchResult.Size = new System.Drawing.Size(13, 13);
            this.lblSearchResult.TabIndex = 19;
            this.lblSearchResult.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(178, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Search Result:";
            // 
            // lblTotalPatient
            // 
            this.lblTotalPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalPatient.AutoSize = true;
            this.lblTotalPatient.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPatient.Location = new System.Drawing.Point(105, 206);
            this.lblTotalPatient.Name = "lblTotalPatient";
            this.lblTotalPatient.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPatient.TabIndex = 17;
            this.lblTotalPatient.Text = "0";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Total Patient:";
            // 
            // btnListFamily
            // 
            this.btnListFamily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListFamily.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnListFamily.BackgroundImage")));
            this.btnListFamily.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnListFamily.Location = new System.Drawing.Point(459, 0);
            this.btnListFamily.Margin = new System.Windows.Forms.Padding(0);
            this.btnListFamily.Name = "btnListFamily";
            this.btnListFamily.Size = new System.Drawing.Size(26, 26);
            this.btnListFamily.TabIndex = 15;
            this.btnListFamily.TabStop = false;
            this.ttButton.SetToolTip(this.btnListFamily, "Family Members");
            this.btnListFamily.UseVisualStyleBackColor = true;
            this.btnListFamily.Click += new System.EventHandler(this.btnListFamily_Click);
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pager.Location = new System.Drawing.Point(436, 206);
            this.pager.Margin = new System.Windows.Forms.Padding(0);
            this.pager.Name = "pager";
            this.pager.Page = 1;
            this.pager.PageSize = 20;
            this.pager.Size = new System.Drawing.Size(403, 26);
            this.pager.TabIndex = 13;
            this.pager.TotalPage = 0;
            // 
            // btnSearchPostalCode
            // 
            this.btnSearchPostalCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchPostalCode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPostalCode.BackgroundImage")));
            this.btnSearchPostalCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPostalCode.Location = new System.Drawing.Point(701, 0);
            this.btnSearchPostalCode.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearchPostalCode.Name = "btnSearchPostalCode";
            this.btnSearchPostalCode.Size = new System.Drawing.Size(26, 26);
            this.btnSearchPostalCode.TabIndex = 10;
            this.btnSearchPostalCode.TabStop = false;
            this.ttButton.SetToolTip(this.btnSearchPostalCode, "Search patients by postal code");
            this.btnSearchPostalCode.UseVisualStyleBackColor = true;
            this.btnSearchPostalCode.Click += new System.EventHandler(this.btnSearchPostalCode_Click);
            // 
            // txtSearchPostalCode
            // 
            this.txtSearchPostalCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchPostalCode.Location = new System.Drawing.Point(635, 3);
            this.txtSearchPostalCode.Mask = "L9L 9L9";
            this.txtSearchPostalCode.Name = "txtSearchPostalCode";
            this.txtSearchPostalCode.Size = new System.Drawing.Size(63, 22);
            this.txtSearchPostalCode.TabIndex = 9;
            this.ttButton.SetToolTip(this.txtSearchPostalCode, "Enter patient\'s postal code to search");
            this.txtSearchPostalCode.Enter += new System.EventHandler(this.txtSearchPostalCode_Enter);
            this.txtSearchPostalCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchPostalCode_KeyPress);
            // 
            // btnSearchPhone
            // 
            this.btnSearchPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchPhone.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchPhone.BackgroundImage")));
            this.btnSearchPhone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchPhone.Location = new System.Drawing.Point(823, 0);
            this.btnSearchPhone.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearchPhone.Name = "btnSearchPhone";
            this.btnSearchPhone.Size = new System.Drawing.Size(26, 26);
            this.btnSearchPhone.TabIndex = 12;
            this.btnSearchPhone.TabStop = false;
            this.ttButton.SetToolTip(this.btnSearchPhone, "Search patients by phone");
            this.btnSearchPhone.UseVisualStyleBackColor = true;
            this.btnSearchPhone.Click += new System.EventHandler(this.btnSearchPhone_Click);
            // 
            // btnSearchName
            // 
            this.btnSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchName.BackgroundImage")));
            this.btnSearchName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearchName.Location = new System.Drawing.Point(606, 0);
            this.btnSearchName.Margin = new System.Windows.Forms.Padding(0);
            this.btnSearchName.Name = "btnSearchName";
            this.btnSearchName.Size = new System.Drawing.Size(26, 26);
            this.btnSearchName.TabIndex = 8;
            this.btnSearchName.TabStop = false;
            this.ttButton.SetToolTip(this.btnSearchName, "Search patients by name");
            this.btnSearchName.UseVisualStyleBackColor = true;
            this.btnSearchName.Click += new System.EventHandler(this.btnSearchName_Click);
            // 
            // txtSearchPhone
            // 
            this.txtSearchPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchPhone.Location = new System.Drawing.Point(730, 3);
            this.txtSearchPhone.Mask = "(999) 000-0000";
            this.txtSearchPhone.Name = "txtSearchPhone";
            this.txtSearchPhone.Size = new System.Drawing.Size(90, 22);
            this.txtSearchPhone.TabIndex = 11;
            this.ttButton.SetToolTip(this.txtSearchPhone, "Enter patient\'s phone number to search");
            this.txtSearchPhone.Enter += new System.EventHandler(this.txtSearchPhone_Enter);
            this.txtSearchPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchPhone_KeyPress);
            // 
            // txtSearchName
            // 
            this.txtSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchName.Location = new System.Drawing.Point(503, 3);
            this.txtSearchName.MaxLength = 50;
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(100, 22);
            this.txtSearchName.TabIndex = 7;
            this.ttButton.SetToolTip(this.txtSearchName, "Enter patient\'s name to search");
            this.txtSearchName.Enter += new System.EventHandler(this.txtSearchName_Enter);
            this.txtSearchName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchName_KeyPress);
            // 
            // btnDeletePatient
            // 
            this.btnDeletePatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeletePatient.BackgroundImage")));
            this.btnDeletePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeletePatient.Location = new System.Drawing.Point(1, 52);
            this.btnDeletePatient.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeletePatient.Name = "btnDeletePatient";
            this.btnDeletePatient.Size = new System.Drawing.Size(26, 26);
            this.btnDeletePatient.TabIndex = 7;
            this.btnDeletePatient.TabStop = false;
            this.ttButton.SetToolTip(this.btnDeletePatient, "Delete Patient");
            this.btnDeletePatient.UseVisualStyleBackColor = true;
            this.btnDeletePatient.Click += new System.EventHandler(this.btnDeletePatient_Click);
            // 
            // ucInsurer
            // 
            this.ucInsurer.Location = new System.Drawing.Point(71, 5);
            this.ucInsurer.Name = "ucInsurer";
            this.ucInsurer.Size = new System.Drawing.Size(227, 17);
            this.ucInsurer.TabIndex = 6;
            this.ucInsurer.UpdateDataList += new System.EventHandler(this.ucInsurer_UpdateDataList);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Insurer:";
            // 
            // btnPicAddPatient
            // 
            this.btnPicAddPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPicAddPatient.BackgroundImage")));
            this.btnPicAddPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPicAddPatient.Location = new System.Drawing.Point(1, 26);
            this.btnPicAddPatient.Margin = new System.Windows.Forms.Padding(0);
            this.btnPicAddPatient.Name = "btnPicAddPatient";
            this.btnPicAddPatient.Size = new System.Drawing.Size(26, 26);
            this.btnPicAddPatient.TabIndex = 4;
            this.btnPicAddPatient.TabStop = false;
            this.ttButton.SetToolTip(this.btnPicAddPatient, "Add a New Patient");
            this.btnPicAddPatient.UseVisualStyleBackColor = true;
            this.btnPicAddPatient.Click += new System.EventHandler(this.btnPicAddPatient_Click);
            // 
            // picPatientListLabel
            // 
            this.picPatientListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picPatientListLabel.Image = ((System.Drawing.Image)(resources.GetObject("picPatientListLabel.Image")));
            this.picPatientListLabel.InitialImage = null;
            this.picPatientListLabel.Location = new System.Drawing.Point(0, 81);
            this.picPatientListLabel.Name = "picPatientListLabel";
            this.picPatientListLabel.Size = new System.Drawing.Size(27, 122);
            this.picPatientListLabel.TabIndex = 3;
            this.picPatientListLabel.TabStop = false;
            // 
            // grdPatientList
            // 
            this.grdPatientList.AllowUserToAddRows = false;
            this.grdPatientList.AllowUserToDeleteRows = false;
            this.grdPatientList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdPatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPatientList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatientEdit,
            this.ViewPatientDetail,
            this.PrintPatient,
            this.AddInitTreatment,
            this.AccountBalance,
            this.Document});
            this.grdPatientList.Location = new System.Drawing.Point(27, 26);
            this.grdPatientList.MultiSelect = false;
            this.grdPatientList.Name = "grdPatientList";
            this.grdPatientList.ReadOnly = true;
            this.grdPatientList.RowHeadersVisible = false;
            this.grdPatientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPatientList.Size = new System.Drawing.Size(820, 177);
            this.grdPatientList.TabIndex = 2;
            this.grdPatientList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPatientList_CellClick);
            this.grdPatientList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdPatientList_CellMouseDoubleClick);
            this.grdPatientList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdPatientList_ColumnHeaderMouseClick);
            this.grdPatientList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdPatientList_DataBindingComplete);
            this.grdPatientList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPatientList_RowEnter);
            // 
            // PatientEdit
            // 
            this.PatientEdit.Frozen = true;
            this.PatientEdit.HeaderText = "";
            this.PatientEdit.MinimumWidth = 25;
            this.PatientEdit.Name = "PatientEdit";
            this.PatientEdit.ReadOnly = true;
            this.PatientEdit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PatientEdit.Width = 25;
            // 
            // ViewPatientDetail
            // 
            this.ViewPatientDetail.Frozen = true;
            this.ViewPatientDetail.HeaderText = "";
            this.ViewPatientDetail.MinimumWidth = 25;
            this.ViewPatientDetail.Name = "ViewPatientDetail";
            this.ViewPatientDetail.ReadOnly = true;
            this.ViewPatientDetail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ViewPatientDetail.Width = 25;
            // 
            // PrintPatient
            // 
            this.PrintPatient.Frozen = true;
            this.PrintPatient.HeaderText = "";
            this.PrintPatient.MinimumWidth = 25;
            this.PrintPatient.Name = "PrintPatient";
            this.PrintPatient.ReadOnly = true;
            this.PrintPatient.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrintPatient.Width = 25;
            // 
            // AddInitTreatment
            // 
            this.AddInitTreatment.Frozen = true;
            this.AddInitTreatment.HeaderText = "";
            this.AddInitTreatment.MinimumWidth = 25;
            this.AddInitTreatment.Name = "AddInitTreatment";
            this.AddInitTreatment.ReadOnly = true;
            this.AddInitTreatment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AddInitTreatment.Width = 25;
            // 
            // AccountBalance
            // 
            this.AccountBalance.Frozen = true;
            this.AccountBalance.HeaderText = "";
            this.AccountBalance.MinimumWidth = 25;
            this.AccountBalance.Name = "AccountBalance";
            this.AccountBalance.ReadOnly = true;
            this.AccountBalance.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AccountBalance.Width = 25;
            // 
            // Document
            // 
            this.Document.Frozen = true;
            this.Document.HeaderText = "";
            this.Document.MinimumWidth = 25;
            this.Document.Name = "Document";
            this.Document.ReadOnly = true;
            this.Document.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Document.Width = 25;
            // 
            // splBottom
            // 
            this.splBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splBottom.Location = new System.Drawing.Point(0, 0);
            this.splBottom.Name = "splBottom";
            this.splBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splBottom.Panel1
            // 
            this.splBottom.Panel1.Controls.Add(this.btnDeleteInitialTreatment);
            this.splBottom.Panel1.Controls.Add(this.ucTherapist);
            this.splBottom.Panel1.Controls.Add(this.ucTreatment);
            this.splBottom.Panel1.Controls.Add(this.label5);
            this.splBottom.Panel1.Controls.Add(this.label4);
            this.splBottom.Panel1.Controls.Add(this.label3);
            this.splBottom.Panel1.Controls.Add(this.dtTo);
            this.splBottom.Panel1.Controls.Add(this.dtFrom);
            this.splBottom.Panel1.Controls.Add(this.label2);
            this.splBottom.Panel1.Controls.Add(this.btnPicAddInitTreatment);
            this.splBottom.Panel1.Controls.Add(this.picInitTreatmentListLabel);
            this.splBottom.Panel1.Controls.Add(this.grdInitialTreatmentList);
            this.splBottom.Panel1MinSize = 160;
            // 
            // splBottom.Panel2
            // 
            this.splBottom.Panel2.Controls.Add(this.btnDeleteFollowUp);
            this.splBottom.Panel2.Controls.Add(this.btnPrintAllSOAPNotes);
            this.splBottom.Panel2.Controls.Add(this.btnPicAddFollowUpTreatment);
            this.splBottom.Panel2.Controls.Add(this.picFollowUpTreatmentListLabel);
            this.splBottom.Panel2.Controls.Add(this.grdFollowUpTreatmentList);
            this.splBottom.Panel2MinSize = 160;
            this.splBottom.Size = new System.Drawing.Size(847, 338);
            this.splBottom.SplitterDistance = 174;
            this.splBottom.TabIndex = 1;
            // 
            // btnDeleteInitialTreatment
            // 
            this.btnDeleteInitialTreatment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteInitialTreatment.BackgroundImage")));
            this.btnDeleteInitialTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteInitialTreatment.Location = new System.Drawing.Point(1, 51);
            this.btnDeleteInitialTreatment.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteInitialTreatment.Name = "btnDeleteInitialTreatment";
            this.btnDeleteInitialTreatment.Size = new System.Drawing.Size(26, 26);
            this.btnDeleteInitialTreatment.TabIndex = 15;
            this.btnDeleteInitialTreatment.TabStop = false;
            this.ttButton.SetToolTip(this.btnDeleteInitialTreatment, "Delete Initial Treatment");
            this.btnDeleteInitialTreatment.UseVisualStyleBackColor = true;
            this.btnDeleteInitialTreatment.Click += new System.EventHandler(this.btnDeleteInitialTreatment_Click);
            // 
            // ucTherapist
            // 
            this.ucTherapist.Location = new System.Drawing.Point(617, 4);
            this.ucTherapist.Name = "ucTherapist";
            this.ucTherapist.Size = new System.Drawing.Size(110, 17);
            this.ucTherapist.TabIndex = 16;
            this.ucTherapist.UpdateDataList += new System.EventHandler(this.ucTherapist_LoadListData);
            // 
            // ucTreatment
            // 
            this.ucTreatment.Location = new System.Drawing.Point(94, 4);
            this.ucTreatment.Name = "ucTreatment";
            this.ucTreatment.Size = new System.Drawing.Size(139, 17);
            this.ucTreatment.TabIndex = 13;
            this.ucTreatment.UpdateDataList += new System.EventHandler(this.ucTreatment_LoadListData);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(239, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(401, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(547, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Therapist:";
            // 
            // dtTo
            // 
            this.dtTo.Checked = false;
            this.dtTo.CustomFormat = "MMM dd, yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(427, 4);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowCheckBox = true;
            this.dtTo.Size = new System.Drawing.Size(120, 22);
            this.dtTo.TabIndex = 15;
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // dtFrom
            // 
            this.dtFrom.Checked = false;
            this.dtFrom.CustomFormat = "MMM dd, yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(275, 4);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowCheckBox = true;
            this.dtFrom.Size = new System.Drawing.Size(120, 22);
            this.dtFrom.TabIndex = 14;
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Treatment:";
            // 
            // btnPicAddInitTreatment
            // 
            this.btnPicAddInitTreatment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPicAddInitTreatment.BackgroundImage")));
            this.btnPicAddInitTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPicAddInitTreatment.Location = new System.Drawing.Point(1, 25);
            this.btnPicAddInitTreatment.Margin = new System.Windows.Forms.Padding(0);
            this.btnPicAddInitTreatment.Name = "btnPicAddInitTreatment";
            this.btnPicAddInitTreatment.Size = new System.Drawing.Size(26, 26);
            this.btnPicAddInitTreatment.TabIndex = 5;
            this.btnPicAddInitTreatment.TabStop = false;
            this.ttButton.SetToolTip(this.btnPicAddInitTreatment, "Add a New Initial Treatment");
            this.btnPicAddInitTreatment.UseVisualStyleBackColor = true;
            this.btnPicAddInitTreatment.Click += new System.EventHandler(this.btnPicAddInitTreatment_Click);
            // 
            // picInitTreatmentListLabel
            // 
            this.picInitTreatmentListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picInitTreatmentListLabel.Image = ((System.Drawing.Image)(resources.GetObject("picInitTreatmentListLabel.Image")));
            this.picInitTreatmentListLabel.Location = new System.Drawing.Point(0, 79);
            this.picInitTreatmentListLabel.Name = "picInitTreatmentListLabel";
            this.picInitTreatmentListLabel.Size = new System.Drawing.Size(27, 93);
            this.picInitTreatmentListLabel.TabIndex = 4;
            this.picInitTreatmentListLabel.TabStop = false;
            // 
            // grdInitialTreatmentList
            // 
            this.grdInitialTreatmentList.AllowUserToAddRows = false;
            this.grdInitialTreatmentList.AllowUserToDeleteRows = false;
            this.grdInitialTreatmentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdInitialTreatmentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInitialTreatmentList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EditInitialTreatment,
            this.ViewInitialTreatmentDetail,
            this.PrintInitialTreatment,
            this.AddFollowUpTreatment,
            this.InitialTreatmentComplete});
            this.grdInitialTreatmentList.Location = new System.Drawing.Point(27, 26);
            this.grdInitialTreatmentList.MultiSelect = false;
            this.grdInitialTreatmentList.Name = "grdInitialTreatmentList";
            this.grdInitialTreatmentList.ReadOnly = true;
            this.grdInitialTreatmentList.RowHeadersVisible = false;
            this.grdInitialTreatmentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdInitialTreatmentList.Size = new System.Drawing.Size(820, 146);
            this.grdInitialTreatmentList.TabIndex = 2;
            this.grdInitialTreatmentList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdInitialTreatmentList_CellClick);
            this.grdInitialTreatmentList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdInitialTreatmentList_CellMouseDoubleClick);
            this.grdInitialTreatmentList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdInitialTreatmentList_DataBindingComplete);
            this.grdInitialTreatmentList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdInitialTreatmentList_RowEnter);
            // 
            // EditInitialTreatment
            // 
            this.EditInitialTreatment.Frozen = true;
            this.EditInitialTreatment.HeaderText = "";
            this.EditInitialTreatment.MinimumWidth = 25;
            this.EditInitialTreatment.Name = "EditInitialTreatment";
            this.EditInitialTreatment.ReadOnly = true;
            this.EditInitialTreatment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EditInitialTreatment.Width = 25;
            // 
            // ViewInitialTreatmentDetail
            // 
            this.ViewInitialTreatmentDetail.Frozen = true;
            this.ViewInitialTreatmentDetail.HeaderText = "";
            this.ViewInitialTreatmentDetail.MinimumWidth = 25;
            this.ViewInitialTreatmentDetail.Name = "ViewInitialTreatmentDetail";
            this.ViewInitialTreatmentDetail.ReadOnly = true;
            this.ViewInitialTreatmentDetail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ViewInitialTreatmentDetail.Width = 25;
            // 
            // PrintInitialTreatment
            // 
            this.PrintInitialTreatment.Frozen = true;
            this.PrintInitialTreatment.HeaderText = "";
            this.PrintInitialTreatment.MinimumWidth = 25;
            this.PrintInitialTreatment.Name = "PrintInitialTreatment";
            this.PrintInitialTreatment.ReadOnly = true;
            this.PrintInitialTreatment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PrintInitialTreatment.Width = 25;
            // 
            // AddFollowUpTreatment
            // 
            this.AddFollowUpTreatment.Frozen = true;
            this.AddFollowUpTreatment.HeaderText = "";
            this.AddFollowUpTreatment.MinimumWidth = 25;
            this.AddFollowUpTreatment.Name = "AddFollowUpTreatment";
            this.AddFollowUpTreatment.ReadOnly = true;
            this.AddFollowUpTreatment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AddFollowUpTreatment.Width = 25;
            // 
            // InitialTreatmentComplete
            // 
            this.InitialTreatmentComplete.Frozen = true;
            this.InitialTreatmentComplete.HeaderText = "";
            this.InitialTreatmentComplete.MinimumWidth = 25;
            this.InitialTreatmentComplete.Name = "InitialTreatmentComplete";
            this.InitialTreatmentComplete.ReadOnly = true;
            this.InitialTreatmentComplete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InitialTreatmentComplete.Width = 25;
            // 
            // btnDeleteFollowUp
            // 
            this.btnDeleteFollowUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteFollowUp.BackgroundImage")));
            this.btnDeleteFollowUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeleteFollowUp.Location = new System.Drawing.Point(1, 26);
            this.btnDeleteFollowUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteFollowUp.Name = "btnDeleteFollowUp";
            this.btnDeleteFollowUp.Size = new System.Drawing.Size(26, 26);
            this.btnDeleteFollowUp.TabIndex = 16;
            this.btnDeleteFollowUp.TabStop = false;
            this.ttButton.SetToolTip(this.btnDeleteFollowUp, "Delete Follow-up Treatment");
            this.btnDeleteFollowUp.UseVisualStyleBackColor = true;
            this.btnDeleteFollowUp.Click += new System.EventHandler(this.btnDeleteFollowUp_Click);
            // 
            // btnPrintAllSOAPNotes
            // 
            this.btnPrintAllSOAPNotes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrintAllSOAPNotes.BackgroundImage")));
            this.btnPrintAllSOAPNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintAllSOAPNotes.Location = new System.Drawing.Point(1, 52);
            this.btnPrintAllSOAPNotes.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrintAllSOAPNotes.Name = "btnPrintAllSOAPNotes";
            this.btnPrintAllSOAPNotes.Size = new System.Drawing.Size(26, 26);
            this.btnPrintAllSOAPNotes.TabIndex = 6;
            this.btnPrintAllSOAPNotes.TabStop = false;
            this.ttButton.SetToolTip(this.btnPrintAllSOAPNotes, "Print All Follow Up Treatment Notes");
            this.btnPrintAllSOAPNotes.UseVisualStyleBackColor = true;
            this.btnPrintAllSOAPNotes.Click += new System.EventHandler(this.btnPrintAllSOAPNotes_Click);
            // 
            // btnPicAddFollowUpTreatment
            // 
            this.btnPicAddFollowUpTreatment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPicAddFollowUpTreatment.BackgroundImage")));
            this.btnPicAddFollowUpTreatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPicAddFollowUpTreatment.Location = new System.Drawing.Point(1, 0);
            this.btnPicAddFollowUpTreatment.Margin = new System.Windows.Forms.Padding(0);
            this.btnPicAddFollowUpTreatment.Name = "btnPicAddFollowUpTreatment";
            this.btnPicAddFollowUpTreatment.Size = new System.Drawing.Size(26, 26);
            this.btnPicAddFollowUpTreatment.TabIndex = 5;
            this.btnPicAddFollowUpTreatment.TabStop = false;
            this.ttButton.SetToolTip(this.btnPicAddFollowUpTreatment, "Add a New Follow-Up Treatment");
            this.btnPicAddFollowUpTreatment.UseVisualStyleBackColor = true;
            this.btnPicAddFollowUpTreatment.Click += new System.EventHandler(this.btnPicAddFollowUpTreatment_Click);
            // 
            // picFollowUpTreatmentListLabel
            // 
            this.picFollowUpTreatmentListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picFollowUpTreatmentListLabel.Image = ((System.Drawing.Image)(resources.GetObject("picFollowUpTreatmentListLabel.Image")));
            this.picFollowUpTreatmentListLabel.Location = new System.Drawing.Point(0, 82);
            this.picFollowUpTreatmentListLabel.Name = "picFollowUpTreatmentListLabel";
            this.picFollowUpTreatmentListLabel.Size = new System.Drawing.Size(27, 75);
            this.picFollowUpTreatmentListLabel.TabIndex = 4;
            this.picFollowUpTreatmentListLabel.TabStop = false;
            // 
            // grdFollowUpTreatmentList
            // 
            this.grdFollowUpTreatmentList.AllowUserToAddRows = false;
            this.grdFollowUpTreatmentList.AllowUserToDeleteRows = false;
            this.grdFollowUpTreatmentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdFollowUpTreatmentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFollowUpTreatmentList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EditFollowUpTreatment,
            this.ViewFollowUpTreatmentDetail,
            this.FollowUpTreatmentComplete});
            this.grdFollowUpTreatmentList.Location = new System.Drawing.Point(27, 3);
            this.grdFollowUpTreatmentList.MultiSelect = false;
            this.grdFollowUpTreatmentList.Name = "grdFollowUpTreatmentList";
            this.grdFollowUpTreatmentList.ReadOnly = true;
            this.grdFollowUpTreatmentList.RowHeadersVisible = false;
            this.grdFollowUpTreatmentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdFollowUpTreatmentList.Size = new System.Drawing.Size(820, 157);
            this.grdFollowUpTreatmentList.TabIndex = 2;
            this.grdFollowUpTreatmentList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFollowUpTreatmentList_CellClick);
            this.grdFollowUpTreatmentList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdFollowUpTreatmentList_CellMouseDoubleClick);
            this.grdFollowUpTreatmentList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdFollowUpTreatmentList_DataBindingComplete);
            this.grdFollowUpTreatmentList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFollowUpTreatmentList_RowEnter);
            // 
            // EditFollowUpTreatment
            // 
            this.EditFollowUpTreatment.Frozen = true;
            this.EditFollowUpTreatment.HeaderText = "";
            this.EditFollowUpTreatment.MinimumWidth = 25;
            this.EditFollowUpTreatment.Name = "EditFollowUpTreatment";
            this.EditFollowUpTreatment.ReadOnly = true;
            this.EditFollowUpTreatment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.EditFollowUpTreatment.Width = 25;
            // 
            // ViewFollowUpTreatmentDetail
            // 
            this.ViewFollowUpTreatmentDetail.Frozen = true;
            this.ViewFollowUpTreatmentDetail.HeaderText = "";
            this.ViewFollowUpTreatmentDetail.MinimumWidth = 25;
            this.ViewFollowUpTreatmentDetail.Name = "ViewFollowUpTreatmentDetail";
            this.ViewFollowUpTreatmentDetail.ReadOnly = true;
            this.ViewFollowUpTreatmentDetail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ViewFollowUpTreatmentDetail.Width = 25;
            // 
            // FollowUpTreatmentComplete
            // 
            this.FollowUpTreatmentComplete.Frozen = true;
            this.FollowUpTreatmentComplete.HeaderText = "";
            this.FollowUpTreatmentComplete.MinimumWidth = 25;
            this.FollowUpTreatmentComplete.Name = "FollowUpTreatmentComplete";
            this.FollowUpTreatmentComplete.ReadOnly = true;
            this.FollowUpTreatmentComplete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FollowUpTreatmentComplete.Width = 25;
            // 
            // lstIcons
            // 
            this.lstIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lstIcons.ImageStream")));
            this.lstIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.lstIcons.Images.SetKeyName(0, "Delete");
            this.lstIcons.Images.SetKeyName(1, "Female");
            this.lstIcons.Images.SetKeyName(2, "Male");
            this.lstIcons.Images.SetKeyName(3, "Edit");
            this.lstIcons.Images.SetKeyName(4, "InitialTreatment");
            this.lstIcons.Images.SetKeyName(5, "FollowUpTreatment");
            this.lstIcons.Images.SetKeyName(6, "ViewDetail");
            this.lstIcons.Images.SetKeyName(7, "Remove");
            this.lstIcons.Images.SetKeyName(8, "Transparent");
            this.lstIcons.Images.SetKeyName(9, "Print");
            this.lstIcons.Images.SetKeyName(10, "BiggerFont");
            this.lstIcons.Images.SetKeyName(11, "SmallerFont");
            this.lstIcons.Images.SetKeyName(12, "Complete");
            this.lstIcons.Images.SetKeyName(13, "Dollar");
            this.lstIcons.Images.SetKeyName(14, "Folder");
            // 
            // worker
            // 
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // tsbReports
            // 
            this.tsbReports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbReports.Image = ((System.Drawing.Image)(resources.GetObject("tsbReports.Image")));
            this.tsbReports.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReports.Name = "tsbReports";
            this.tsbReports.Size = new System.Drawing.Size(28, 28);
            this.tsbReports.Text = "Reports";
            this.tsbReports.Click += new System.EventHandler(this.tsbReports_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(849, 637);
            this.Controls.Add(this.splMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.stMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(670, 590);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Easy Healthcare Desktop";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.stMain.ResumeLayout(false);
            this.stMain.PerformLayout();
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel1.PerformLayout();
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPatientListLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatientList)).EndInit();
            this.splBottom.Panel1.ResumeLayout(false);
            this.splBottom.Panel1.PerformLayout();
            this.splBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splBottom)).EndInit();
            this.splBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInitTreatmentListLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInitialTreatmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFollowUpTreatmentListLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFollowUpTreatmentList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbPatients;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbAddPatient;
        private System.Windows.Forms.StatusStrip stMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblLogonUser;
        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.SplitContainer splBottom;
        private System.Windows.Forms.ImageList lstIcons;
        private System.Windows.Forms.DataGridView grdInitialTreatmentList;
        private System.Windows.Forms.DataGridView grdFollowUpTreatmentList;
        //private System.Windows.Forms.DataGridViewImageColumn DeleteInitialTreatment;
        private System.Windows.Forms.DataGridViewImageColumn EditInitialTreatment;
        private System.Windows.Forms.DataGridViewImageColumn ViewInitialTreatmentDetail;
        private System.Windows.Forms.DataGridViewImageColumn PrintInitialTreatment;
        private System.Windows.Forms.DataGridViewImageColumn AddFollowUpTreatment;
        //private System.Windows.Forms.DataGridViewImageColumn DeleteFollowUpTreatment;
        private System.Windows.Forms.DataGridViewImageColumn EditFollowUpTreatment;
        private System.Windows.Forms.DataGridViewImageColumn ViewFollowUpTreatmentDetail;
        private System.Windows.Forms.DataGridViewImageColumn InitialTreatmentComplete;
        private System.Windows.Forms.DataGridViewImageColumn FollowUpTreatmentComplete;
        private System.Windows.Forms.DataGridView grdPatientList;
        private System.Windows.Forms.PictureBox picPatientListLabel;
        private System.Windows.Forms.PictureBox picInitTreatmentListLabel;
        private System.Windows.Forms.PictureBox picFollowUpTreatmentListLabel;
        private System.Windows.Forms.Button btnPicAddPatient;
        private System.Windows.Forms.Button btnPicAddInitTreatment;
        private System.Windows.Forms.Button btnPicAddFollowUpTreatment;
        private System.Windows.Forms.ToolTip ttButton;
        //private System.Windows.Forms.DataGridViewImageColumn PatientDelete;
        private System.Windows.Forms.Button btnPrintAllSOAPNotes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbBiggerFont;
        private System.Windows.Forms.ToolStripButton tsbSmallerFont;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label2;
        private UCCheckBoxDropDown ucInsurer;
        private UCCheckBoxDropDown ucTherapist;
        private UCCheckBoxDropDown ucTreatment;
        private System.Windows.Forms.ToolStripButton tsbAdvancedSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbInvoice;
        private System.Windows.Forms.ToolStripButton tsbAlert;
        private System.Windows.Forms.Button btnDeletePatient;
        private System.Windows.Forms.Button btnDeleteInitialTreatment;
        private System.Windows.Forms.Button btnDeleteFollowUp;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Button btnSearchPhone;
        private System.Windows.Forms.Button btnSearchName;
        private System.Windows.Forms.MaskedTextBox txtSearchPhone;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.Button btnSearchPostalCode;
        private System.Windows.Forms.MaskedTextBox txtSearchPostalCode;
        private UCPager pager;
        private System.Windows.Forms.ToolStripButton tsbPractitionerCalendar;
        private System.Windows.Forms.ToolStripButton tsbBooking;
        private System.Windows.Forms.Button btnListFamily;
        private System.Windows.Forms.ToolStripButton tsbDocuments;
        private System.Windows.Forms.DataGridViewImageColumn PatientEdit;
        private System.Windows.Forms.DataGridViewImageColumn ViewPatientDetail;
        private System.Windows.Forms.DataGridViewImageColumn PrintPatient;
        private System.Windows.Forms.DataGridViewImageColumn AddInitTreatment;
        private System.Windows.Forms.DataGridViewImageColumn AccountBalance;
        private System.Windows.Forms.DataGridViewImageColumn Document;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSearchResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalPatient;
        private System.Windows.Forms.ToolStripButton tsbReports;
    }
}

