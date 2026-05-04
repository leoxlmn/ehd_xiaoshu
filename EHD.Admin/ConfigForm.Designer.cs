namespace EHD.Admin {
    partial class ConfigForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtMaxSigWidth = new System.Windows.Forms.TextBox();
            this.txtMaxSigHeight = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEnableGoogleCalendar = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkEnablePractitionerCalendar = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMaxInvoiceItemPerInvoice = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMaxInvoicesPerPatient = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMaxPointsPerDiagram = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMaxFollowUpsPerTreatment = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaxTreatmentsPerPatient = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMaxTreatmentTypes = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMaxTherapists = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMaxInsurers = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numWorkingHourEnd = new System.Windows.Forms.NumericUpDown();
            this.numWorkingHourStart = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtMaxDocumentType = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkingHourEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkingHourStart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(10, 361);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(600, 361);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.txtUser);
            this.groupBox3.Controls.Add(this.txtDatabase);
            this.groupBox3.Controls.Add(this.txtPort);
            this.groupBox3.Controls.Add(this.txtServer);
            this.groupBox3.Location = new System.Drawing.Point(10, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(329, 100);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database Connection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "User:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Database:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Server:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(240, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(76, 22);
            this.txtPassword.TabIndex = 16;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(68, 71);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(76, 22);
            this.txtUser.TabIndex = 15;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(68, 45);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(131, 22);
            this.txtDatabase.TabIndex = 14;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(240, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(76, 22);
            this.txtPort.TabIndex = 13;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(68, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(131, 22);
            this.txtServer.TabIndex = 12;
            // 
            // txtMaxSigWidth
            // 
            this.txtMaxSigWidth.Location = new System.Drawing.Point(68, 29);
            this.txtMaxSigWidth.Name = "txtMaxSigWidth";
            this.txtMaxSigWidth.Size = new System.Drawing.Size(76, 22);
            this.txtMaxSigWidth.TabIndex = 17;
            // 
            // txtMaxSigHeight
            // 
            this.txtMaxSigHeight.Location = new System.Drawing.Point(240, 29);
            this.txtMaxSigHeight.Name = "txtMaxSigHeight";
            this.txtMaxSigHeight.Size = new System.Drawing.Size(76, 22);
            this.txtMaxSigHeight.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtMaxSigWidth);
            this.groupBox4.Controls.Add(this.txtMaxSigHeight);
            this.groupBox4.Location = new System.Drawing.Point(10, 118);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(329, 68);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Signature Image Size";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(170, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Max Height:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Max Width:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEnableGoogleCalendar);
            this.groupBox1.Location = new System.Drawing.Point(10, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 47);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Google Calendar";
            // 
            // chkEnableGoogleCalendar
            // 
            this.chkEnableGoogleCalendar.AutoSize = true;
            this.chkEnableGoogleCalendar.Enabled = false;
            this.chkEnableGoogleCalendar.Location = new System.Drawing.Point(68, 19);
            this.chkEnableGoogleCalendar.Name = "chkEnableGoogleCalendar";
            this.chkEnableGoogleCalendar.Size = new System.Drawing.Size(151, 17);
            this.chkEnableGoogleCalendar.TabIndex = 0;
            this.chkEnableGoogleCalendar.Text = "Enable Google Calendar";
            this.toolTip1.SetToolTip(this.chkEnableGoogleCalendar, resources.GetString("chkEnableGoogleCalendar.ToolTip"));
            this.chkEnableGoogleCalendar.UseVisualStyleBackColor = true;
            // 
            // chkEnablePractitionerCalendar
            // 
            this.chkEnablePractitionerCalendar.AutoSize = true;
            this.chkEnablePractitionerCalendar.Location = new System.Drawing.Point(68, 21);
            this.chkEnablePractitionerCalendar.Name = "chkEnablePractitionerCalendar";
            this.chkEnablePractitionerCalendar.Size = new System.Drawing.Size(172, 17);
            this.chkEnablePractitionerCalendar.TabIndex = 1;
            this.chkEnablePractitionerCalendar.Text = "Enable Practitioner Calendar";
            this.toolTip1.SetToolTip(this.chkEnablePractitionerCalendar, "Enable Practitioner Calendar");
            this.chkEnablePractitionerCalendar.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDebugMode);
            this.groupBox2.Location = new System.Drawing.Point(346, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(329, 42);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Debug Mode";
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.AutoSize = true;
            this.chkDebugMode.Location = new System.Drawing.Point(68, 19);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(94, 17);
            this.chkDebugMode.TabIndex = 0;
            this.chkDebugMode.Text = "Debug Mode";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtMaxDocumentType);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.txtMaxInvoiceItemPerInvoice);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.txtMaxInvoicesPerPatient);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.txtMaxPointsPerDiagram);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txtMaxFollowUpsPerTreatment);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtMaxTreatmentsPerPatient);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtMaxTreatmentTypes);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtMaxTherapists);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtMaxInsurers);
            this.groupBox5.Location = new System.Drawing.Point(345, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(329, 295);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Application Limitations";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 214);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(187, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Maximum Invoice Items per Invoice:";
            // 
            // txtMaxInvoiceItemPerInvoice
            // 
            this.txtMaxInvoiceItemPerInvoice.Location = new System.Drawing.Point(247, 211);
            this.txtMaxInvoiceItemPerInvoice.Name = "txtMaxInvoiceItemPerInvoice";
            this.txtMaxInvoiceItemPerInvoice.Size = new System.Drawing.Size(76, 22);
            this.txtMaxInvoiceItemPerInvoice.TabIndex = 35;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(162, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "Maximum Invoices per Patient:";
            // 
            // txtMaxInvoicesPerPatient
            // 
            this.txtMaxInvoicesPerPatient.Location = new System.Drawing.Point(247, 183);
            this.txtMaxInvoicesPerPatient.Name = "txtMaxInvoicesPerPatient";
            this.txtMaxInvoicesPerPatient.Size = new System.Drawing.Size(76, 22);
            this.txtMaxInvoicesPerPatient.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 158);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(160, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Maximum Points per Diagram:";
            // 
            // txtMaxPointsPerDiagram
            // 
            this.txtMaxPointsPerDiagram.Location = new System.Drawing.Point(247, 155);
            this.txtMaxPointsPerDiagram.Name = "txtMaxPointsPerDiagram";
            this.txtMaxPointsPerDiagram.Size = new System.Drawing.Size(76, 22);
            this.txtMaxPointsPerDiagram.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 130);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(194, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Maximum Follow Ups per Treatment:";
            // 
            // txtMaxFollowUpsPerTreatment
            // 
            this.txtMaxFollowUpsPerTreatment.Location = new System.Drawing.Point(247, 127);
            this.txtMaxFollowUpsPerTreatment.Name = "txtMaxFollowUpsPerTreatment";
            this.txtMaxFollowUpsPerTreatment.Size = new System.Drawing.Size(76, 22);
            this.txtMaxFollowUpsPerTreatment.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(209, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Maximum Initial Treatments per Patient:";
            // 
            // txtMaxTreatmentsPerPatient
            // 
            this.txtMaxTreatmentsPerPatient.Location = new System.Drawing.Point(247, 99);
            this.txtMaxTreatmentsPerPatient.Name = "txtMaxTreatmentsPerPatient";
            this.txtMaxTreatmentsPerPatient.Size = new System.Drawing.Size(76, 22);
            this.txtMaxTreatmentsPerPatient.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Maximum Treatment Types:";
            // 
            // txtMaxTreatmentTypes
            // 
            this.txtMaxTreatmentTypes.Location = new System.Drawing.Point(247, 71);
            this.txtMaxTreatmentTypes.Name = "txtMaxTreatmentTypes";
            this.txtMaxTreatmentTypes.Size = new System.Drawing.Size(76, 22);
            this.txtMaxTreatmentTypes.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Maximum Therapists:";
            // 
            // txtMaxTherapists
            // 
            this.txtMaxTherapists.Location = new System.Drawing.Point(247, 45);
            this.txtMaxTherapists.Name = "txtMaxTherapists";
            this.txtMaxTherapists.Size = new System.Drawing.Size(76, 22);
            this.txtMaxTherapists.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Maximum Insurers:";
            // 
            // txtMaxInsurers
            // 
            this.txtMaxInsurers.Location = new System.Drawing.Point(247, 19);
            this.txtMaxInsurers.Name = "txtMaxInsurers";
            this.txtMaxInsurers.Size = new System.Drawing.Size(76, 22);
            this.txtMaxInsurers.TabIndex = 21;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numWorkingHourEnd);
            this.groupBox6.Controls.Add(this.numWorkingHourStart);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.chkEnablePractitionerCalendar);
            this.groupBox6.Location = new System.Drawing.Point(10, 245);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(329, 110);
            this.groupBox6.TabIndex = 23;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Practitioner Calendar";
            // 
            // numWorkingHourEnd
            // 
            this.numWorkingHourEnd.Location = new System.Drawing.Point(240, 71);
            this.numWorkingHourEnd.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numWorkingHourEnd.Name = "numWorkingHourEnd";
            this.numWorkingHourEnd.Size = new System.Drawing.Size(50, 22);
            this.numWorkingHourEnd.TabIndex = 28;
            // 
            // numWorkingHourStart
            // 
            this.numWorkingHourStart.Location = new System.Drawing.Point(240, 43);
            this.numWorkingHourStart.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numWorkingHourStart.Name = "numWorkingHourStart";
            this.numWorkingHourStart.Size = new System.Drawing.Size(50, 22);
            this.numWorkingHourStart.TabIndex = 27;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(200, 73);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 13);
            this.label18.TabIndex = 26;
            this.label18.Text = "To:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(186, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(36, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "From:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(67, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "Working Hour (0-24)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 242);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(146, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "Maximum Document Types:";
            // 
            // txtMaxDocumentType
            // 
            this.txtMaxDocumentType.Location = new System.Drawing.Point(247, 239);
            this.txtMaxDocumentType.Name = "txtMaxDocumentType";
            this.txtMaxDocumentType.Size = new System.Drawing.Size(76, 22);
            this.txtMaxDocumentType.TabIndex = 38;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(687, 396);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConfigForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkingHourEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWorkingHourStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtMaxSigWidth;
        private System.Windows.Forms.TextBox txtMaxSigHeight;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEnableGoogleCalendar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMaxPointsPerDiagram;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMaxFollowUpsPerTreatment;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMaxTreatmentsPerPatient;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMaxTreatmentTypes;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMaxTherapists;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMaxInsurers;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtMaxInvoicesPerPatient;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMaxInvoiceItemPerInvoice;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkEnablePractitionerCalendar;
        private System.Windows.Forms.NumericUpDown numWorkingHourEnd;
        private System.Windows.Forms.NumericUpDown numWorkingHourStart;
        private System.Windows.Forms.TextBox txtMaxDocumentType;
        private System.Windows.Forms.Label label19;
    }
}