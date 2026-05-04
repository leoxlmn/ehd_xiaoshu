namespace EHD.Admin {
    partial class BookingForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookingForm));
            this.tabBookings = new System.Windows.Forms.TabControl();
            this.tabCalendar = new System.Windows.Forms.TabPage();
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.lstPractitioner = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splCalendar = new System.Windows.Forms.SplitContainer();
            this.pnlCalendarNavigation = new System.Windows.Forms.Panel();
            this.drpBookingMinMinutes = new System.Windows.Forms.ComboBox();
            this.btnShowDatePicker = new System.Windows.Forms.Button();
            this.lblCalendarDate = new System.Windows.Forms.Label();
            this.btnNextDate = new System.Windows.Forms.Button();
            this.rdoDaily = new System.Windows.Forms.RadioButton();
            this.btnPreDate = new System.Windows.Forms.Button();
            this.rdoWeekly = new System.Windows.Forms.RadioButton();
            this.rdoMonthly = new System.Windows.Forms.RadioButton();
            this.btnThisDate = new System.Windows.Forms.Button();
            this.tblCalendar = new EHD.Admin.TableLayoutPanelEx();
            this.tabList = new System.Windows.Forms.TabPage();
            this.splSearchBookings = new System.Windows.Forms.SplitContainer();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtDateTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.ucPatient = new EHD.Admin.UCAutoCompleteTextBox();
            this.dtDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.drpTreatmentType = new System.Windows.Forms.ComboBox();
            this.drpUser = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalFound = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pager = new EHD.Admin.UCPager();
            this.grdBookingList = new System.Windows.Forms.DataGridView();
            this.btnAddNewBooking = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ContainerPanel = new System.Windows.Forms.Panel();
            this.tabBookings.SuspendLayout();
            this.tabCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).BeginInit();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splCalendar)).BeginInit();
            this.splCalendar.Panel1.SuspendLayout();
            this.splCalendar.Panel2.SuspendLayout();
            this.splCalendar.SuspendLayout();
            this.pnlCalendarNavigation.SuspendLayout();
            this.tabList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splSearchBookings)).BeginInit();
            this.splSearchBookings.Panel1.SuspendLayout();
            this.splSearchBookings.Panel2.SuspendLayout();
            this.splSearchBookings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookingList)).BeginInit();
            this.ContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabBookings
            // 
            this.tabBookings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabBookings.Controls.Add(this.tabCalendar);
            this.tabBookings.Controls.Add(this.tabList);
            this.tabBookings.Location = new System.Drawing.Point(0, 0);
            this.tabBookings.Margin = new System.Windows.Forms.Padding(0);
            this.tabBookings.Name = "tabBookings";
            this.tabBookings.SelectedIndex = 0;
            this.tabBookings.Size = new System.Drawing.Size(816, 419);
            this.tabBookings.TabIndex = 0;
            // 
            // tabCalendar
            // 
            this.tabCalendar.Controls.Add(this.splMain);
            this.tabCalendar.Location = new System.Drawing.Point(4, 22);
            this.tabCalendar.Name = "tabCalendar";
            this.tabCalendar.Padding = new System.Windows.Forms.Padding(3);
            this.tabCalendar.Size = new System.Drawing.Size(808, 393);
            this.tabCalendar.TabIndex = 0;
            this.tabCalendar.Text = "Calendar View";
            this.tabCalendar.UseVisualStyleBackColor = true;
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splMain.IsSplitterFixed = true;
            this.splMain.Location = new System.Drawing.Point(3, 3);
            this.splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splMain.Panel1.Controls.Add(this.chkCheckAll);
            this.splMain.Panel1.Controls.Add(this.lstPractitioner);
            this.splMain.Panel1.Controls.Add(this.label1);
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.splCalendar);
            this.splMain.Panel2MinSize = 600;
            this.splMain.Size = new System.Drawing.Size(802, 387);
            this.splMain.SplitterDistance = 182;
            this.splMain.TabIndex = 1;
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Checked = true;
            this.chkCheckAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheckAll.Location = new System.Drawing.Point(3, 25);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(119, 17);
            this.chkCheckAll.TabIndex = 2;
            this.chkCheckAll.Text = "Select/Deselect All";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            this.chkCheckAll.Click += new System.EventHandler(this.chkCheckAll_Click);
            // 
            // lstPractitioner
            // 
            this.lstPractitioner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPractitioner.CheckOnClick = true;
            this.lstPractitioner.FormattingEnabled = true;
            this.lstPractitioner.IntegralHeight = false;
            this.lstPractitioner.Location = new System.Drawing.Point(0, 43);
            this.lstPractitioner.Name = "lstPractitioner";
            this.lstPractitioner.Size = new System.Drawing.Size(182, 344);
            this.lstPractitioner.TabIndex = 1;
            this.lstPractitioner.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstPractitioner_ItemCheck);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Practitioners";
            // 
            // splCalendar
            // 
            this.splCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splCalendar.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splCalendar.IsSplitterFixed = true;
            this.splCalendar.Location = new System.Drawing.Point(0, 0);
            this.splCalendar.Name = "splCalendar";
            this.splCalendar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splCalendar.Panel1
            // 
            this.splCalendar.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splCalendar.Panel1.Controls.Add(this.pnlCalendarNavigation);
            // 
            // splCalendar.Panel2
            // 
            this.splCalendar.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splCalendar.Panel2.Controls.Add(this.tblCalendar);
            this.splCalendar.Size = new System.Drawing.Size(616, 387);
            this.splCalendar.SplitterDistance = 40;
            this.splCalendar.SplitterWidth = 1;
            this.splCalendar.TabIndex = 0;
            // 
            // pnlCalendarNavigation
            // 
            this.pnlCalendarNavigation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlCalendarNavigation.Controls.Add(this.drpBookingMinMinutes);
            this.pnlCalendarNavigation.Controls.Add(this.btnShowDatePicker);
            this.pnlCalendarNavigation.Controls.Add(this.lblCalendarDate);
            this.pnlCalendarNavigation.Controls.Add(this.btnNextDate);
            this.pnlCalendarNavigation.Controls.Add(this.rdoDaily);
            this.pnlCalendarNavigation.Controls.Add(this.btnPreDate);
            this.pnlCalendarNavigation.Controls.Add(this.rdoWeekly);
            this.pnlCalendarNavigation.Controls.Add(this.rdoMonthly);
            this.pnlCalendarNavigation.Controls.Add(this.btnThisDate);
            this.pnlCalendarNavigation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCalendarNavigation.Location = new System.Drawing.Point(16, 5);
            this.pnlCalendarNavigation.Name = "pnlCalendarNavigation";
            this.pnlCalendarNavigation.Size = new System.Drawing.Size(581, 30);
            this.pnlCalendarNavigation.TabIndex = 1;
            // 
            // drpBookingMinMinutes
            // 
            this.drpBookingMinMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpBookingMinMinutes.FormattingEnabled = true;
            this.drpBookingMinMinutes.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30",
            "60"});
            this.drpBookingMinMinutes.Location = new System.Drawing.Point(202, 5);
            this.drpBookingMinMinutes.Name = "drpBookingMinMinutes";
            this.drpBookingMinMinutes.Size = new System.Drawing.Size(40, 21);
            this.drpBookingMinMinutes.TabIndex = 3;
            this.toolTip1.SetToolTip(this.drpBookingMinMinutes, "Minimum Minutes for Booking");
            this.drpBookingMinMinutes.SelectedIndexChanged += new System.EventHandler(this.drpBookingMinMinutes_SelectedIndexChanged);
            // 
            // btnShowDatePicker
            // 
            this.btnShowDatePicker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShowDatePicker.BackgroundImage")));
            this.btnShowDatePicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnShowDatePicker.Location = new System.Drawing.Point(469, 3);
            this.btnShowDatePicker.Name = "btnShowDatePicker";
            this.btnShowDatePicker.Size = new System.Drawing.Size(27, 23);
            this.btnShowDatePicker.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnShowDatePicker, "Select a Date, Week or Month");
            this.btnShowDatePicker.UseVisualStyleBackColor = true;
            this.btnShowDatePicker.Click += new System.EventHandler(this.btnShowDatePicker_Click);
            // 
            // lblCalendarDate
            // 
            this.lblCalendarDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCalendarDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalendarDate.Location = new System.Drawing.Point(282, 5);
            this.lblCalendarDate.Name = "lblCalendarDate";
            this.lblCalendarDate.Size = new System.Drawing.Size(148, 20);
            this.lblCalendarDate.TabIndex = 5;
            this.lblCalendarDate.Text = "label2";
            this.lblCalendarDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblCalendarDate.TextChanged += new System.EventHandler(this.lblCalendarDate_TextChanged);
            // 
            // btnNextDate
            // 
            this.btnNextDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNextDate.BackgroundImage")));
            this.btnNextDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNextDate.Location = new System.Drawing.Point(436, 3);
            this.btnNextDate.Name = "btnNextDate";
            this.btnNextDate.Size = new System.Drawing.Size(27, 23);
            this.btnNextDate.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnNextDate, "Next");
            this.btnNextDate.UseVisualStyleBackColor = true;
            this.btnNextDate.Click += new System.EventHandler(this.btnNextDate_Click);
            // 
            // rdoDaily
            // 
            this.rdoDaily.AutoSize = true;
            this.rdoDaily.Location = new System.Drawing.Point(145, 7);
            this.rdoDaily.Name = "rdoDaily";
            this.rdoDaily.Size = new System.Drawing.Size(51, 17);
            this.rdoDaily.TabIndex = 2;
            this.rdoDaily.TabStop = true;
            this.rdoDaily.Text = "Daily";
            this.rdoDaily.UseVisualStyleBackColor = true;
            this.rdoDaily.CheckedChanged += new System.EventHandler(this.rdoCalendarMode_CheckedChanged);
            // 
            // btnPreDate
            // 
            this.btnPreDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreDate.BackgroundImage")));
            this.btnPreDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPreDate.Location = new System.Drawing.Point(249, 3);
            this.btnPreDate.Name = "btnPreDate";
            this.btnPreDate.Size = new System.Drawing.Size(27, 23);
            this.btnPreDate.TabIndex = 4;
            this.btnPreDate.UseVisualStyleBackColor = true;
            this.btnPreDate.Click += new System.EventHandler(this.btnPreDate_Click);
            // 
            // rdoWeekly
            // 
            this.rdoWeekly.AutoSize = true;
            this.rdoWeekly.Location = new System.Drawing.Point(77, 7);
            this.rdoWeekly.Name = "rdoWeekly";
            this.rdoWeekly.Size = new System.Drawing.Size(63, 17);
            this.rdoWeekly.TabIndex = 1;
            this.rdoWeekly.TabStop = true;
            this.rdoWeekly.Text = "Weekly";
            this.rdoWeekly.UseVisualStyleBackColor = true;
            this.rdoWeekly.CheckedChanged += new System.EventHandler(this.rdoCalendarMode_CheckedChanged);
            // 
            // rdoMonthly
            // 
            this.rdoMonthly.AutoSize = true;
            this.rdoMonthly.Checked = true;
            this.rdoMonthly.Location = new System.Drawing.Point(3, 7);
            this.rdoMonthly.Name = "rdoMonthly";
            this.rdoMonthly.Size = new System.Drawing.Size(70, 17);
            this.rdoMonthly.TabIndex = 0;
            this.rdoMonthly.TabStop = true;
            this.rdoMonthly.Text = "Monthly";
            this.rdoMonthly.UseVisualStyleBackColor = true;
            this.rdoMonthly.CheckedChanged += new System.EventHandler(this.rdoCalendarMode_CheckedChanged);
            // 
            // btnThisDate
            // 
            this.btnThisDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisDate.Location = new System.Drawing.Point(502, 4);
            this.btnThisDate.Name = "btnThisDate";
            this.btnThisDate.Size = new System.Drawing.Size(75, 23);
            this.btnThisDate.TabIndex = 8;
            this.btnThisDate.Text = "This Date";
            this.btnThisDate.UseVisualStyleBackColor = true;
            this.btnThisDate.Click += new System.EventHandler(this.btnThisDate_Click);
            // 
            // tblCalendar
            // 
            this.tblCalendar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblCalendar.ColumnCount = 2;
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCalendar.Location = new System.Drawing.Point(0, 0);
            this.tblCalendar.Margin = new System.Windows.Forms.Padding(0);
            this.tblCalendar.Name = "tblCalendar";
            this.tblCalendar.RowCount = 2;
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCalendar.Size = new System.Drawing.Size(616, 346);
            this.tblCalendar.TabIndex = 0;
            this.tblCalendar.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tblCalendar_CellPaint);
            this.tblCalendar.Click += new System.EventHandler(this.tblCalendar_Click);
            this.tblCalendar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tblCalendar_MouseClick);
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.splSearchBookings);
            this.tabList.Location = new System.Drawing.Point(4, 22);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(808, 393);
            this.tabList.TabIndex = 1;
            this.tabList.Text = "Search Bookings";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // splSearchBookings
            // 
            this.splSearchBookings.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splSearchBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splSearchBookings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splSearchBookings.Location = new System.Drawing.Point(3, 3);
            this.splSearchBookings.Name = "splSearchBookings";
            this.splSearchBookings.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splSearchBookings.Panel1
            // 
            this.splSearchBookings.Panel1.Controls.Add(this.btnSearch);
            this.splSearchBookings.Panel1.Controls.Add(this.dtDateTo);
            this.splSearchBookings.Panel1.Controls.Add(this.label4);
            this.splSearchBookings.Panel1.Controls.Add(this.ucPatient);
            this.splSearchBookings.Panel1.Controls.Add(this.dtDateFrom);
            this.splSearchBookings.Panel1.Controls.Add(this.label12);
            this.splSearchBookings.Panel1.Controls.Add(this.drpTreatmentType);
            this.splSearchBookings.Panel1.Controls.Add(this.drpUser);
            this.splSearchBookings.Panel1.Controls.Add(this.label5);
            this.splSearchBookings.Panel1.Controls.Add(this.label3);
            this.splSearchBookings.Panel1.Controls.Add(this.label2);
            this.splSearchBookings.Panel1MinSize = 70;
            // 
            // splSearchBookings.Panel2
            // 
            this.splSearchBookings.Panel2.Controls.Add(this.lblTotalFound);
            this.splSearchBookings.Panel2.Controls.Add(this.label6);
            this.splSearchBookings.Panel2.Controls.Add(this.pager);
            this.splSearchBookings.Panel2.Controls.Add(this.grdBookingList);
            this.splSearchBookings.Size = new System.Drawing.Size(802, 387);
            this.splSearchBookings.SplitterDistance = 73;
            this.splSearchBookings.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.PaleGreen;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(598, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(197, 23);
            this.btnSearch.TabIndex = 151;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtDateTo
            // 
            this.dtDateTo.CustomFormat = "MMM dd, yyyy";
            this.dtDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateTo.Location = new System.Drawing.Point(328, 40);
            this.dtDateTo.Name = "dtDateTo";
            this.dtDateTo.ShowCheckBox = true;
            this.dtDateTo.Size = new System.Drawing.Size(197, 22);
            this.dtDateTo.TabIndex = 149;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 150;
            this.label4.Text = "To:";
            // 
            // ucPatient
            // 
            this.ucPatient.ActionDelayMilliseconds = ((uint)(1000u));
            this.ucPatient.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucPatient.Location = new System.Drawing.Point(57, 14);
            this.ucPatient.MinInputLengthToStartSearch = ((uint)(2u));
            this.ucPatient.Name = "ucPatient";
            this.ucPatient.SelectedItem = null;
            this.ucPatient.Size = new System.Drawing.Size(197, 20);
            this.ucPatient.TabIndex = 141;
            this.ucPatient.Tooltip = "Enter File#, First Name or Last Name to search";
            this.ucPatient.MinInputLengthReached += new System.EventHandler(this.ucPatient_MinInputLengthReached);
            // 
            // dtDateFrom
            // 
            this.dtDateFrom.CustomFormat = "MMM dd, yyyy";
            this.dtDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDateFrom.Location = new System.Drawing.Point(57, 40);
            this.dtDateFrom.Name = "dtDateFrom";
            this.dtDateFrom.ShowCheckBox = true;
            this.dtDateFrom.Size = new System.Drawing.Size(197, 22);
            this.dtDateFrom.TabIndex = 144;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 148;
            this.label12.Text = "From:";
            // 
            // drpTreatmentType
            // 
            this.drpTreatmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpTreatmentType.FormattingEnabled = true;
            this.drpTreatmentType.Location = new System.Drawing.Point(598, 13);
            this.drpTreatmentType.Name = "drpTreatmentType";
            this.drpTreatmentType.Size = new System.Drawing.Size(197, 21);
            this.drpTreatmentType.Sorted = true;
            this.drpTreatmentType.TabIndex = 143;
            // 
            // drpUser
            // 
            this.drpUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpUser.FormattingEnabled = true;
            this.drpUser.Location = new System.Drawing.Point(328, 13);
            this.drpUser.Name = "drpUser";
            this.drpUser.Size = new System.Drawing.Size(197, 21);
            this.drpUser.TabIndex = 142;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(531, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 147;
            this.label5.Text = "Treatment:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 146;
            this.label3.Text = "Practioner:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 145;
            this.label2.Text = "Patient:";
            // 
            // lblTotalFound
            // 
            this.lblTotalFound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalFound.AutoSize = true;
            this.lblTotalFound.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFound.Location = new System.Drawing.Point(81, 284);
            this.lblTotalFound.Name = "lblTotalFound";
            this.lblTotalFound.Size = new System.Drawing.Size(13, 13);
            this.lblTotalFound.TabIndex = 150;
            this.lblTotalFound.Text = "0";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 284);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 149;
            this.label6.Text = "Total found:";
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pager.Location = new System.Drawing.Point(397, 284);
            this.pager.Margin = new System.Windows.Forms.Padding(0);
            this.pager.Name = "pager";
            this.pager.Page = 1;
            this.pager.PageSize = 20;
            this.pager.Size = new System.Drawing.Size(403, 26);
            this.pager.TabIndex = 14;
            this.pager.TotalPage = 0;
            // 
            // grdBookingList
            // 
            this.grdBookingList.AllowUserToAddRows = false;
            this.grdBookingList.AllowUserToDeleteRows = false;
            this.grdBookingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdBookingList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdBookingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBookingList.Location = new System.Drawing.Point(0, 3);
            this.grdBookingList.MultiSelect = false;
            this.grdBookingList.Name = "grdBookingList";
            this.grdBookingList.ReadOnly = true;
            this.grdBookingList.RowHeadersVisible = false;
            this.grdBookingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBookingList.Size = new System.Drawing.Size(802, 278);
            this.grdBookingList.TabIndex = 3;
            // 
            // btnAddNewBooking
            // 
            this.btnAddNewBooking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNewBooking.BackColor = System.Drawing.Color.LightGreen;
            this.btnAddNewBooking.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewBooking.Location = new System.Drawing.Point(7, 422);
            this.btnAddNewBooking.Name = "btnAddNewBooking";
            this.btnAddNewBooking.Size = new System.Drawing.Size(175, 38);
            this.btnAddNewBooking.TabIndex = 9;
            this.btnAddNewBooking.Text = "Add New Booking";
            this.btnAddNewBooking.UseVisualStyleBackColor = false;
            this.btnAddNewBooking.Click += new System.EventHandler(this.btnAddNewBooking_Click);
            // 
            // ContainerPanel
            // 
            this.ContainerPanel.Controls.Add(this.tabBookings);
            this.ContainerPanel.Controls.Add(this.btnAddNewBooking);
            this.ContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContainerPanel.Location = new System.Drawing.Point(0, 0);
            this.ContainerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContainerPanel.Name = "ContainerPanel";
            this.ContainerPanel.Size = new System.Drawing.Size(816, 472);
            this.ContainerPanel.TabIndex = 2;
            // 
            // BookingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 472);
            this.Controls.Add(this.ContainerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "BookingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bookings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BookingForm_Load);
            this.Resize += new System.EventHandler(this.BookingForm_Resize);
            this.tabBookings.ResumeLayout(false);
            this.tabCalendar.ResumeLayout(false);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel1.PerformLayout();
            this.splMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splMain)).EndInit();
            this.splMain.ResumeLayout(false);
            this.splCalendar.Panel1.ResumeLayout(false);
            this.splCalendar.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splCalendar)).EndInit();
            this.splCalendar.ResumeLayout(false);
            this.pnlCalendarNavigation.ResumeLayout(false);
            this.pnlCalendarNavigation.PerformLayout();
            this.tabList.ResumeLayout(false);
            this.splSearchBookings.Panel1.ResumeLayout(false);
            this.splSearchBookings.Panel1.PerformLayout();
            this.splSearchBookings.Panel2.ResumeLayout(false);
            this.splSearchBookings.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splSearchBookings)).EndInit();
            this.splSearchBookings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBookingList)).EndInit();
            this.ContainerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabBookings;
        private System.Windows.Forms.TabPage tabCalendar;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.SplitContainer splCalendar;
        private System.Windows.Forms.RadioButton rdoDaily;
        private System.Windows.Forms.RadioButton rdoWeekly;
        private System.Windows.Forms.RadioButton rdoMonthly;
        private System.Windows.Forms.Panel pnlCalendarNavigation;
        private System.Windows.Forms.Button btnThisDate;
        private System.Windows.Forms.Button btnNextDate;
        private System.Windows.Forms.Button btnPreDate;
        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCalendarDate;
        private System.Windows.Forms.Button btnAddNewBooking;
        private System.Windows.Forms.CheckedListBox lstPractitioner;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private TableLayoutPanelEx tblCalendar;
        private System.Windows.Forms.Button btnShowDatePicker;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel ContainerPanel;
        private System.Windows.Forms.ComboBox drpBookingMinMinutes;
        private System.Windows.Forms.SplitContainer splSearchBookings;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtDateTo;
        private System.Windows.Forms.Label label4;
        private UCAutoCompleteTextBox ucPatient;
        private System.Windows.Forms.DateTimePicker dtDateFrom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox drpTreatmentType;
        private System.Windows.Forms.ComboBox drpUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView grdBookingList;
        private UCPager pager;
        private System.Windows.Forms.Label lblTotalFound;
        private System.Windows.Forms.Label label6;
    }
}