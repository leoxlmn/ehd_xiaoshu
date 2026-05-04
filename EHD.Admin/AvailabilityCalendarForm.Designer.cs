namespace EHD.Admin {
    partial class AvailabilityCalendarForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvailabilityCalendarForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlAddDayOff = new System.Windows.Forms.Panel();
            this.chkFullDay = new System.Windows.Forms.CheckBox();
            this.btnAddDayOff = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.drpUser = new System.Windows.Forms.ComboBox();
            this.lstUsers = new System.Windows.Forms.CheckedListBox();
            this.tabUserSchedule = new System.Windows.Forms.TabControl();
            this.tabWeekly = new System.Windows.Forms.TabPage();
            this.tblWeekly = new EHD.Admin.TableLayoutPanelEx();
            this.lblSaturday = new System.Windows.Forms.Label();
            this.lblFriday = new System.Windows.Forms.Label();
            this.lblThursday = new System.Windows.Forms.Label();
            this.lblWednesday = new System.Windows.Forms.Label();
            this.lblTuesday = new System.Windows.Forms.Label();
            this.lblMonday = new System.Windows.Forms.Label();
            this.lblSunday = new System.Windows.Forms.Label();
            this.tabTimeOff = new System.Windows.Forms.TabPage();
            this.tblTimeOffs = new EHD.Admin.TableLayoutPanelEx();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTimeOffMonthPicker = new System.Windows.Forms.Panel();
            this.btnThisMonth = new System.Windows.Forms.Button();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.btnPreMonth = new System.Windows.Forms.Button();
            this.drpYear = new System.Windows.Forms.ComboBox();
            this.drpMonth = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlAddDayOff.SuspendLayout();
            this.tabUserSchedule.SuspendLayout();
            this.tabWeekly.SuspendLayout();
            this.tblWeekly.SuspendLayout();
            this.tabTimeOff.SuspendLayout();
            this.tblTimeOffs.SuspendLayout();
            this.pnlTimeOffMonthPicker.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.pnlAddDayOff);
            this.splitContainer1.Panel1.Controls.Add(this.lstUsers);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabUserSchedule);
            this.splitContainer1.Size = new System.Drawing.Size(782, 552);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlAddDayOff
            // 
            this.pnlAddDayOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAddDayOff.Controls.Add(this.chkFullDay);
            this.pnlAddDayOff.Controls.Add(this.btnAddDayOff);
            this.pnlAddDayOff.Controls.Add(this.label4);
            this.pnlAddDayOff.Controls.Add(this.txtReason);
            this.pnlAddDayOff.Controls.Add(this.dtTo);
            this.pnlAddDayOff.Controls.Add(this.label3);
            this.pnlAddDayOff.Controls.Add(this.dtFrom);
            this.pnlAddDayOff.Controls.Add(this.label2);
            this.pnlAddDayOff.Controls.Add(this.label1);
            this.pnlAddDayOff.Controls.Add(this.drpUser);
            this.pnlAddDayOff.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddDayOff.Location = new System.Drawing.Point(12, 233);
            this.pnlAddDayOff.Name = "pnlAddDayOff";
            this.pnlAddDayOff.Size = new System.Drawing.Size(174, 307);
            this.pnlAddDayOff.TabIndex = 1;
            // 
            // chkFullDay
            // 
            this.chkFullDay.AutoSize = true;
            this.chkFullDay.Location = new System.Drawing.Point(120, 110);
            this.chkFullDay.Name = "chkFullDay";
            this.chkFullDay.Size = new System.Drawing.Size(68, 17);
            this.chkFullDay.TabIndex = 9;
            this.chkFullDay.Text = "Full Day";
            this.chkFullDay.UseVisualStyleBackColor = true;
            this.chkFullDay.CheckedChanged += new System.EventHandler(this.chkFullDay_CheckedChanged);
            // 
            // btnAddDayOff
            // 
            this.btnAddDayOff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDayOff.Location = new System.Drawing.Point(7, 280);
            this.btnAddDayOff.Name = "btnAddDayOff";
            this.btnAddDayOff.Size = new System.Drawing.Size(164, 23);
            this.btnAddDayOff.TabIndex = 8;
            this.btnAddDayOff.Text = "Add Time Off";
            this.btnAddDayOff.UseVisualStyleBackColor = true;
            this.btnAddDayOff.Click += new System.EventHandler(this.btnAddDayOff_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Reason:";
            // 
            // txtReason
            // 
            this.txtReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReason.Location = new System.Drawing.Point(3, 172);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(168, 102);
            this.txtReason.TabIndex = 6;
            // 
            // dtTo
            // 
            this.dtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtTo.CustomFormat = "ddd, MMM dd, yyyy hh:mm tt";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(3, 130);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(168, 22);
            this.dtTo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "To:";
            // 
            // dtFrom
            // 
            this.dtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtFrom.CustomFormat = "ddd, MMM dd, yyyy hh:mm tt";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(3, 82);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(168, 22);
            this.dtFrom.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "From:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Practitioner:";
            // 
            // drpUser
            // 
            this.drpUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drpUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpUser.FormattingEnabled = true;
            this.drpUser.Location = new System.Drawing.Point(3, 31);
            this.drpUser.Name = "drpUser";
            this.drpUser.Size = new System.Drawing.Size(168, 21);
            this.drpUser.Sorted = true;
            this.drpUser.TabIndex = 0;
            // 
            // lstUsers
            // 
            this.lstUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUsers.CheckOnClick = true;
            this.lstUsers.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.IntegralHeight = false;
            this.lstUsers.Location = new System.Drawing.Point(12, 22);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(174, 174);
            this.lstUsers.Sorted = true;
            this.lstUsers.TabIndex = 0;
            this.lstUsers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstUsers_ItemCheck);
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // tabUserSchedule
            // 
            this.tabUserSchedule.Controls.Add(this.tabWeekly);
            this.tabUserSchedule.Controls.Add(this.tabTimeOff);
            this.tabUserSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabUserSchedule.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabUserSchedule.Location = new System.Drawing.Point(0, 0);
            this.tabUserSchedule.Name = "tabUserSchedule";
            this.tabUserSchedule.SelectedIndex = 0;
            this.tabUserSchedule.Size = new System.Drawing.Size(579, 552);
            this.tabUserSchedule.TabIndex = 0;
            this.tabUserSchedule.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabUserSchedule_Selected);
            // 
            // tabWeekly
            // 
            this.tabWeekly.Controls.Add(this.tblWeekly);
            this.tabWeekly.Location = new System.Drawing.Point(4, 22);
            this.tabWeekly.Name = "tabWeekly";
            this.tabWeekly.Padding = new System.Windows.Forms.Padding(3);
            this.tabWeekly.Size = new System.Drawing.Size(571, 526);
            this.tabWeekly.TabIndex = 0;
            this.tabWeekly.Text = "Weekly Availability";
            this.tabWeekly.UseVisualStyleBackColor = true;
            // 
            // tblWeekly
            // 
            this.tblWeekly.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblWeekly.ColumnCount = 7;
            this.tblWeekly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblWeekly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblWeekly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblWeekly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblWeekly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblWeekly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblWeekly.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblWeekly.Controls.Add(this.lblSaturday, 6, 0);
            this.tblWeekly.Controls.Add(this.lblFriday, 5, 0);
            this.tblWeekly.Controls.Add(this.lblThursday, 4, 0);
            this.tblWeekly.Controls.Add(this.lblWednesday, 3, 0);
            this.tblWeekly.Controls.Add(this.lblTuesday, 2, 0);
            this.tblWeekly.Controls.Add(this.lblMonday, 1, 0);
            this.tblWeekly.Controls.Add(this.lblSunday, 0, 0);
            this.tblWeekly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblWeekly.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblWeekly.Location = new System.Drawing.Point(3, 3);
            this.tblWeekly.Name = "tblWeekly";
            this.tblWeekly.RowCount = 3;
            this.tblWeekly.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblWeekly.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblWeekly.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblWeekly.Size = new System.Drawing.Size(565, 520);
            this.tblWeekly.TabIndex = 0;
            // 
            // lblSaturday
            // 
            this.lblSaturday.AutoSize = true;
            this.lblSaturday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSaturday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaturday.Location = new System.Drawing.Point(484, 1);
            this.lblSaturday.Name = "lblSaturday";
            this.lblSaturday.Size = new System.Drawing.Size(77, 30);
            this.lblSaturday.TabIndex = 6;
            this.lblSaturday.Text = "Saturday";
            this.lblSaturday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFriday
            // 
            this.lblFriday.AutoSize = true;
            this.lblFriday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFriday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFriday.Location = new System.Drawing.Point(404, 1);
            this.lblFriday.Name = "lblFriday";
            this.lblFriday.Size = new System.Drawing.Size(73, 30);
            this.lblFriday.TabIndex = 5;
            this.lblFriday.Text = "Friday";
            this.lblFriday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblThursday
            // 
            this.lblThursday.AutoSize = true;
            this.lblThursday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblThursday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThursday.Location = new System.Drawing.Point(324, 1);
            this.lblThursday.Name = "lblThursday";
            this.lblThursday.Size = new System.Drawing.Size(73, 30);
            this.lblThursday.TabIndex = 4;
            this.lblThursday.Text = "Thursday";
            this.lblThursday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWednesday
            // 
            this.lblWednesday.AutoSize = true;
            this.lblWednesday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWednesday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWednesday.Location = new System.Drawing.Point(244, 1);
            this.lblWednesday.Name = "lblWednesday";
            this.lblWednesday.Size = new System.Drawing.Size(73, 30);
            this.lblWednesday.TabIndex = 3;
            this.lblWednesday.Text = "Wednesday";
            this.lblWednesday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTuesday
            // 
            this.lblTuesday.AutoSize = true;
            this.lblTuesday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTuesday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuesday.Location = new System.Drawing.Point(164, 1);
            this.lblTuesday.Name = "lblTuesday";
            this.lblTuesday.Size = new System.Drawing.Size(73, 30);
            this.lblTuesday.TabIndex = 2;
            this.lblTuesday.Text = "Tuesday";
            this.lblTuesday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMonday
            // 
            this.lblMonday.AutoSize = true;
            this.lblMonday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonday.Location = new System.Drawing.Point(84, 1);
            this.lblMonday.Name = "lblMonday";
            this.lblMonday.Size = new System.Drawing.Size(73, 30);
            this.lblMonday.TabIndex = 1;
            this.lblMonday.Text = "Monday";
            this.lblMonday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSunday
            // 
            this.lblSunday.AutoSize = true;
            this.lblSunday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSunday.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSunday.Location = new System.Drawing.Point(4, 1);
            this.lblSunday.Name = "lblSunday";
            this.lblSunday.Size = new System.Drawing.Size(73, 30);
            this.lblSunday.TabIndex = 0;
            this.lblSunday.Text = "Sunday";
            this.lblSunday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabTimeOff
            // 
            this.tabTimeOff.Controls.Add(this.tblTimeOffs);
            this.tabTimeOff.Location = new System.Drawing.Point(4, 22);
            this.tabTimeOff.Name = "tabTimeOff";
            this.tabTimeOff.Padding = new System.Windows.Forms.Padding(3);
            this.tabTimeOff.Size = new System.Drawing.Size(571, 526);
            this.tabTimeOff.TabIndex = 1;
            this.tabTimeOff.Text = "Time Offs";
            this.tabTimeOff.UseVisualStyleBackColor = true;
            // 
            // tblTimeOffs
            // 
            this.tblTimeOffs.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTimeOffs.ColumnCount = 7;
            this.tblTimeOffs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblTimeOffs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblTimeOffs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblTimeOffs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblTimeOffs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblTimeOffs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblTimeOffs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28572F));
            this.tblTimeOffs.Controls.Add(this.label11, 6, 1);
            this.tblTimeOffs.Controls.Add(this.label10, 5, 1);
            this.tblTimeOffs.Controls.Add(this.label9, 4, 1);
            this.tblTimeOffs.Controls.Add(this.label8, 3, 1);
            this.tblTimeOffs.Controls.Add(this.label7, 2, 1);
            this.tblTimeOffs.Controls.Add(this.label6, 1, 1);
            this.tblTimeOffs.Controls.Add(this.label5, 0, 1);
            this.tblTimeOffs.Controls.Add(this.pnlTimeOffMonthPicker, 0, 0);
            this.tblTimeOffs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTimeOffs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblTimeOffs.Location = new System.Drawing.Point(3, 3);
            this.tblTimeOffs.Name = "tblTimeOffs";
            this.tblTimeOffs.RowCount = 8;
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTimeOffs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblTimeOffs.Size = new System.Drawing.Size(565, 520);
            this.tblTimeOffs.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(484, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 30);
            this.label11.TabIndex = 7;
            this.label11.Text = "Saturday";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(404, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 30);
            this.label10.TabIndex = 6;
            this.label10.Text = "Friday";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(324, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 30);
            this.label9.TabIndex = 5;
            this.label9.Text = "Thursday";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(244, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 30);
            this.label8.TabIndex = 4;
            this.label8.Text = "Wednesday";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(164, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 30);
            this.label7.TabIndex = 3;
            this.label7.Text = "Tuesday";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(84, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 30);
            this.label6.TabIndex = 2;
            this.label6.Text = "Mondy";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Sunday";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTimeOffMonthPicker
            // 
            this.pnlTimeOffMonthPicker.BackColor = System.Drawing.Color.White;
            this.tblTimeOffs.SetColumnSpan(this.pnlTimeOffMonthPicker, 7);
            this.pnlTimeOffMonthPicker.Controls.Add(this.btnThisMonth);
            this.pnlTimeOffMonthPicker.Controls.Add(this.btnNextMonth);
            this.pnlTimeOffMonthPicker.Controls.Add(this.btnPreMonth);
            this.pnlTimeOffMonthPicker.Controls.Add(this.drpYear);
            this.pnlTimeOffMonthPicker.Controls.Add(this.drpMonth);
            this.pnlTimeOffMonthPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimeOffMonthPicker.Location = new System.Drawing.Point(1, 1);
            this.pnlTimeOffMonthPicker.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTimeOffMonthPicker.Name = "pnlTimeOffMonthPicker";
            this.pnlTimeOffMonthPicker.Size = new System.Drawing.Size(563, 50);
            this.pnlTimeOffMonthPicker.TabIndex = 0;
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisMonth.Location = new System.Drawing.Point(356, 11);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(75, 23);
            this.btnThisMonth.TabIndex = 4;
            this.btnThisMonth.Text = "This Month";
            this.btnThisMonth.UseVisualStyleBackColor = true;
            this.btnThisMonth.Click += new System.EventHandler(this.btnThisMonth_Click);
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextMonth.Location = new System.Drawing.Point(311, 11);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(39, 23);
            this.btnNextMonth.TabIndex = 3;
            this.btnNextMonth.Text = ">>";
            this.toolTip1.SetToolTip(this.btnNextMonth, "Next Month");
            this.btnNextMonth.UseVisualStyleBackColor = true;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);
            // 
            // btnPreMonth
            // 
            this.btnPreMonth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreMonth.Location = new System.Drawing.Point(76, 11);
            this.btnPreMonth.Name = "btnPreMonth";
            this.btnPreMonth.Size = new System.Drawing.Size(39, 23);
            this.btnPreMonth.TabIndex = 2;
            this.btnPreMonth.Text = "<<";
            this.toolTip1.SetToolTip(this.btnPreMonth, "Previous Month");
            this.btnPreMonth.UseVisualStyleBackColor = true;
            this.btnPreMonth.Click += new System.EventHandler(this.btnPreMonth_Click);
            // 
            // drpYear
            // 
            this.drpYear.DisplayMember = "Text";
            this.drpYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpYear.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpYear.FormattingEnabled = true;
            this.drpYear.Location = new System.Drawing.Point(216, 12);
            this.drpYear.Name = "drpYear";
            this.drpYear.Size = new System.Drawing.Size(89, 21);
            this.drpYear.TabIndex = 1;
            this.drpYear.ValueMember = "Id";
            this.drpYear.SelectedIndexChanged += new System.EventHandler(this.drpYear_SelectedIndexChanged);
            // 
            // drpMonth
            // 
            this.drpMonth.DisplayMember = "Text";
            this.drpMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpMonth.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drpMonth.FormattingEnabled = true;
            this.drpMonth.Location = new System.Drawing.Point(121, 12);
            this.drpMonth.Name = "drpMonth";
            this.drpMonth.Size = new System.Drawing.Size(89, 21);
            this.drpMonth.TabIndex = 0;
            this.drpMonth.ValueMember = "Id";
            // 
            // AvailabilityCalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 552);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "AvailabilityCalendarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Practitioner Calendar";
            this.Load += new System.EventHandler(this.AvailabilityCalendarForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlAddDayOff.ResumeLayout(false);
            this.pnlAddDayOff.PerformLayout();
            this.tabUserSchedule.ResumeLayout(false);
            this.tabWeekly.ResumeLayout(false);
            this.tblWeekly.ResumeLayout(false);
            this.tblWeekly.PerformLayout();
            this.tabTimeOff.ResumeLayout(false);
            this.tblTimeOffs.ResumeLayout(false);
            this.tblTimeOffs.PerformLayout();
            this.pnlTimeOffMonthPicker.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabUserSchedule;
        private System.Windows.Forms.TabPage tabWeekly;
        private System.Windows.Forms.TabPage tabTimeOff;
        private System.Windows.Forms.Panel pnlAddDayOff;
        private System.Windows.Forms.Button btnAddDayOff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox drpUser;
        private System.Windows.Forms.CheckedListBox lstUsers;
        private TableLayoutPanelEx tblWeekly;
        private System.Windows.Forms.Label lblSaturday;
        private System.Windows.Forms.Label lblFriday;
        private System.Windows.Forms.Label lblThursday;
        private System.Windows.Forms.Label lblWednesday;
        private System.Windows.Forms.Label lblTuesday;
        private System.Windows.Forms.Label lblMonday;
        private System.Windows.Forms.Label lblSunday;
        private TableLayoutPanelEx tblTimeOffs;
        private System.Windows.Forms.Panel pnlTimeOffMonthPicker;
        private System.Windows.Forms.ComboBox drpYear;
        private System.Windows.Forms.ComboBox drpMonth;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.Button btnPreMonth;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkFullDay;
        private System.Windows.Forms.Button btnThisMonth;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}