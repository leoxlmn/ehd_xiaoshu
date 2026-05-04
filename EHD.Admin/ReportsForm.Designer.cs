namespace EHD.Admin {
    partial class ReportsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPatients = new System.Windows.Forms.TabPage();
            this.btnExportAllPatients = new System.Windows.Forms.Button();
            this.tabServices = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.btnExportServices = new System.Windows.Forms.Button();
            this.ucPatient = new EHD.Admin.UCAutoCompleteTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkFromDate = new System.Windows.Forms.CheckBox();
            this.chkToDate = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPatients.SuspendLayout();
            this.tabServices.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(400, 170);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPatients);
            this.tabControl1.Controls.Add(this.tabServices);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(486, 164);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPatients
            // 
            this.tabPatients.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPatients.Controls.Add(this.btnExportAllPatients);
            this.tabPatients.Location = new System.Drawing.Point(4, 22);
            this.tabPatients.Name = "tabPatients";
            this.tabPatients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatients.Size = new System.Drawing.Size(436, 138);
            this.tabPatients.TabIndex = 0;
            this.tabPatients.Text = "Patients";
            // 
            // btnExportAllPatients
            // 
            this.btnExportAllPatients.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportAllPatients.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportAllPatients.Location = new System.Drawing.Point(135, 44);
            this.btnExportAllPatients.Name = "btnExportAllPatients";
            this.btnExportAllPatients.Size = new System.Drawing.Size(139, 47);
            this.btnExportAllPatients.TabIndex = 0;
            this.btnExportAllPatients.Text = "Export All Patients";
            this.btnExportAllPatients.UseVisualStyleBackColor = false;
            this.btnExportAllPatients.Click += new System.EventHandler(this.btnExportAllPatients_Click);
            // 
            // tabServices
            // 
            this.tabServices.BackColor = System.Drawing.Color.Gainsboro;
            this.tabServices.Controls.Add(this.chkToDate);
            this.tabServices.Controls.Add(this.chkFromDate);
            this.tabServices.Controls.Add(this.label2);
            this.tabServices.Controls.Add(this.dtTo);
            this.tabServices.Controls.Add(this.dtFrom);
            this.tabServices.Controls.Add(this.label12);
            this.tabServices.Controls.Add(this.btnExportServices);
            this.tabServices.Controls.Add(this.ucPatient);
            this.tabServices.Controls.Add(this.label1);
            this.tabServices.Location = new System.Drawing.Point(4, 22);
            this.tabServices.Name = "tabServices";
            this.tabServices.Padding = new System.Windows.Forms.Padding(3);
            this.tabServices.Size = new System.Drawing.Size(478, 138);
            this.tabServices.TabIndex = 1;
            this.tabServices.Text = "Patient Services";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "To:";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "MMM dd, yyyy";
            this.dtTo.Enabled = false;
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(318, 44);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(91, 22);
            this.dtTo.TabIndex = 13;
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "MMM dd, yyyy";
            this.dtFrom.Enabled = false;
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(172, 44);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(91, 22);
            this.dtFrom.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(44, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "Service Date From:";
            // 
            // btnExportServices
            // 
            this.btnExportServices.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportServices.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportServices.Location = new System.Drawing.Point(152, 72);
            this.btnExportServices.Name = "btnExportServices";
            this.btnExportServices.Size = new System.Drawing.Size(257, 42);
            this.btnExportServices.TabIndex = 9;
            this.btnExportServices.Text = "Export Services";
            this.btnExportServices.UseVisualStyleBackColor = false;
            this.btnExportServices.Click += new System.EventHandler(this.btnExportServices_Click);
            // 
            // ucPatient
            // 
            this.ucPatient.ActionDelayMilliseconds = ((uint)(1000u));
            this.ucPatient.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucPatient.InputText = "";
            this.ucPatient.Location = new System.Drawing.Point(151, 18);
            this.ucPatient.MinInputLengthToStartSearch = ((uint)(2u));
            this.ucPatient.Name = "ucPatient";
            this.ucPatient.SelectedItem = null;
            this.ucPatient.Size = new System.Drawing.Size(258, 20);
            this.ucPatient.TabIndex = 8;
            this.ucPatient.Tooltip = "Enter File#, First Name or Last Name to search";
            this.ucPatient.MinInputLengthReached += new System.EventHandler(this.ucPatient_MinInputLengthReached);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Patient:";
            // 
            // chkFromDate
            // 
            this.chkFromDate.AutoSize = true;
            this.chkFromDate.Location = new System.Drawing.Point(151, 51);
            this.chkFromDate.Name = "chkFromDate";
            this.chkFromDate.Size = new System.Drawing.Size(15, 14);
            this.chkFromDate.TabIndex = 15;
            this.chkFromDate.UseVisualStyleBackColor = true;
            this.chkFromDate.CheckedChanged += new System.EventHandler(this.chkFromDate_CheckedChanged);
            // 
            // chkToDate
            // 
            this.chkToDate.AutoSize = true;
            this.chkToDate.Location = new System.Drawing.Point(297, 50);
            this.chkToDate.Name = "chkToDate";
            this.chkToDate.Size = new System.Drawing.Size(15, 14);
            this.chkToDate.TabIndex = 16;
            this.chkToDate.UseVisualStyleBackColor = true;
            this.chkToDate.CheckedChanged += new System.EventHandler(this.chkToDate_CheckedChanged);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(487, 205);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reports";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReportsForm_FormClosed);
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPatients.ResumeLayout(false);
            this.tabServices.ResumeLayout(false);
            this.tabServices.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPatients;
        private System.Windows.Forms.TabPage tabServices;
        private System.Windows.Forms.Button btnExportAllPatients;
        private System.Windows.Forms.Button btnExportServices;
        private UCAutoCompleteTextBox ucPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkToDate;
        private System.Windows.Forms.CheckBox chkFromDate;
    }
}