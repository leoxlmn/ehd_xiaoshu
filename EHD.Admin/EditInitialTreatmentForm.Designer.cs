namespace EHD.Admin {
    partial class EditInitialTreatmentForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditInitialTreatmentForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.drpTreatmentType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.drpTherapist = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtTreatmentDate = new System.Windows.Forms.DateTimePicker();
            this.dtTreatmentTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.numTreatmentDuration = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.grpTreatmentDetail = new System.Windows.Forms.GroupBox();
            this.pnlTreatmentDetail = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.printer = new System.Windows.Forms.PrintDialog();
            this.dlgPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.dlgPrinter = new System.Windows.Forms.PrintDialog();
            this.btnPrintToWord = new System.Windows.Forms.Button();
            this.btnOpenToWord = new System.Windows.Forms.Button();
            this.btnOpenToTherapist = new System.Windows.Forms.Button();
            this.lstIcon = new System.Windows.Forms.ImageList(this.components);
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveAsTemplate = new System.Windows.Forms.Button();
            this.btnCopyFromTemplate = new System.Windows.Forms.Button();
            this.printWorker = new System.ComponentModel.BackgroundWorker();
            this.picComplete = new System.Windows.Forms.PictureBox();
            this.btnComplete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numTreatmentDuration)).BeginInit();
            this.grpTreatmentDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComplete)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patient:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(107, 15);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(171, 22);
            this.txtPatientName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Treatment Type:";
            // 
            // drpTreatmentType
            // 
            this.drpTreatmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpTreatmentType.FormattingEnabled = true;
            this.drpTreatmentType.Location = new System.Drawing.Point(107, 46);
            this.drpTreatmentType.Name = "drpTreatmentType";
            this.drpTreatmentType.Size = new System.Drawing.Size(171, 21);
            this.drpTreatmentType.TabIndex = 3;
            this.drpTreatmentType.SelectedIndexChanged += new System.EventHandler(this.drpTreatmentType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(352, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Therapist:";
            // 
            // drpTherapist
            // 
            this.drpTherapist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpTherapist.FormattingEnabled = true;
            this.drpTherapist.Location = new System.Drawing.Point(416, 46);
            this.drpTherapist.Name = "drpTherapist";
            this.drpTherapist.Size = new System.Drawing.Size(171, 21);
            this.drpTherapist.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(16, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(618, 190);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Treatment Date:";
            // 
            // dtTreatmentDate
            // 
            this.dtTreatmentDate.CustomFormat = "ddd, MMM dd, yyyy";
            this.dtTreatmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTreatmentDate.Location = new System.Drawing.Point(107, 78);
            this.dtTreatmentDate.Name = "dtTreatmentDate";
            this.dtTreatmentDate.Size = new System.Drawing.Size(134, 22);
            this.dtTreatmentDate.TabIndex = 9;
            // 
            // dtTreatmentTime
            // 
            this.dtTreatmentTime.CustomFormat = "h:mm tt";
            this.dtTreatmentTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTreatmentTime.Location = new System.Drawing.Point(247, 78);
            this.dtTreatmentTime.Name = "dtTreatmentTime";
            this.dtTreatmentTime.ShowUpDown = true;
            this.dtTreatmentTime.Size = new System.Drawing.Size(71, 22);
            this.dtTreatmentTime.TabIndex = 10;
            this.dtTreatmentTime.Value = new System.DateTime(2012, 6, 28, 10, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(352, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Treatment Duration:";
            // 
            // numTreatmentDuration
            // 
            this.numTreatmentDuration.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTreatmentDuration.Location = new System.Drawing.Point(470, 78);
            this.numTreatmentDuration.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numTreatmentDuration.Name = "numTreatmentDuration";
            this.numTreatmentDuration.Size = new System.Drawing.Size(60, 22);
            this.numTreatmentDuration.TabIndex = 12;
            this.numTreatmentDuration.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(536, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "minutes";
            // 
            // grpTreatmentDetail
            // 
            this.grpTreatmentDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTreatmentDetail.Controls.Add(this.pnlTreatmentDetail);
            this.grpTreatmentDetail.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTreatmentDetail.Location = new System.Drawing.Point(16, 106);
            this.grpTreatmentDetail.Name = "grpTreatmentDetail";
            this.grpTreatmentDetail.Size = new System.Drawing.Size(677, 78);
            this.grpTreatmentDetail.TabIndex = 14;
            this.grpTreatmentDetail.TabStop = false;
            this.grpTreatmentDetail.Text = "Treatment Details";
            // 
            // pnlTreatmentDetail
            // 
            this.pnlTreatmentDetail.AutoScroll = true;
            this.pnlTreatmentDetail.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlTreatmentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTreatmentDetail.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTreatmentDetail.Location = new System.Drawing.Point(3, 16);
            this.pnlTreatmentDetail.Name = "pnlTreatmentDetail";
            this.pnlTreatmentDetail.Size = new System.Drawing.Size(671, 59);
            this.pnlTreatmentDetail.TabIndex = 0;
            this.pnlTreatmentDetail.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.pnlTreatmentDetail_ControlAdded);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(284, 49);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(16, 16);
            this.label20.TabIndex = 41;
            this.label20.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(593, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 42;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(324, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 43;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(593, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 16);
            this.label9.TabIndex = 44;
            this.label9.Text = "*";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Location = new System.Drawing.Point(374, 190);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(43, 23);
            this.btnPrint.TabIndex = 45;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // pd
            // 
            this.pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.pd_PrintPage);
            // 
            // printer
            // 
            this.printer.UseEXDialog = true;
            // 
            // dlgPrintPreview
            // 
            this.dlgPrintPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.dlgPrintPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.dlgPrintPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.dlgPrintPreview.Document = this.pd;
            this.dlgPrintPreview.Enabled = true;
            this.dlgPrintPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("dlgPrintPreview.Icon")));
            this.dlgPrintPreview.Name = "dlgPrintPreview";
            this.dlgPrintPreview.Visible = false;
            // 
            // dlgPrinter
            // 
            this.dlgPrinter.UseEXDialog = true;
            // 
            // btnPrintToWord
            // 
            this.btnPrintToWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintToWord.Location = new System.Drawing.Point(423, 190);
            this.btnPrintToWord.Name = "btnPrintToWord";
            this.btnPrintToWord.Size = new System.Drawing.Size(89, 23);
            this.btnPrintToWord.TabIndex = 46;
            this.btnPrintToWord.Text = "Print to Word";
            this.btnPrintToWord.UseVisualStyleBackColor = true;
            this.btnPrintToWord.Visible = false;
            this.btnPrintToWord.Click += new System.EventHandler(this.btnPrintToWord_Click);
            // 
            // btnOpenToWord
            // 
            this.btnOpenToWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenToWord.Location = new System.Drawing.Point(247, 190);
            this.btnOpenToWord.Name = "btnOpenToWord";
            this.btnOpenToWord.Size = new System.Drawing.Size(121, 23);
            this.btnOpenToWord.TabIndex = 47;
            this.btnOpenToWord.Text = "Open to Word";
            this.btnOpenToWord.UseVisualStyleBackColor = true;
            this.btnOpenToWord.Click += new System.EventHandler(this.btnOpenToWord_Click);
            // 
            // btnOpenToTherapist
            // 
            this.btnOpenToTherapist.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenToTherapist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOpenToTherapist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenToTherapist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenToTherapist.ImageKey = "Closed";
            this.btnOpenToTherapist.ImageList = this.lstIcon;
            this.btnOpenToTherapist.Location = new System.Drawing.Point(416, 6);
            this.btnOpenToTherapist.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenToTherapist.Name = "btnOpenToTherapist";
            this.btnOpenToTherapist.Size = new System.Drawing.Size(168, 37);
            this.btnOpenToTherapist.TabIndex = 49;
            this.btnOpenToTherapist.Text = "Closed";
            this.btnOpenToTherapist.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenToTherapist.UseVisualStyleBackColor = false;
            this.btnOpenToTherapist.Click += new System.EventHandler(this.btnOpenToTherapist_Click);
            // 
            // lstIcon
            // 
            this.lstIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lstIcon.ImageStream")));
            this.lstIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.lstIcon.Images.SetKeyName(0, "Open");
            this.lstIcon.Images.SetKeyName(1, "Closed");
            // 
            // btnSaveAsTemplate
            // 
            this.btnSaveAsTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveAsTemplate.BackgroundImage")));
            this.btnSaveAsTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveAsTemplate.Location = new System.Drawing.Point(587, 190);
            this.btnSaveAsTemplate.Name = "btnSaveAsTemplate";
            this.btnSaveAsTemplate.Size = new System.Drawing.Size(25, 23);
            this.btnSaveAsTemplate.TabIndex = 56;
            this.tooltip.SetToolTip(this.btnSaveAsTemplate, "Set as Template");
            this.btnSaveAsTemplate.UseVisualStyleBackColor = true;
            this.btnSaveAsTemplate.Click += new System.EventHandler(this.btnSaveAsTemplate_Click);
            // 
            // btnCopyFromTemplate
            // 
            this.btnCopyFromTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFromTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyFromTemplate.BackgroundImage")));
            this.btnCopyFromTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCopyFromTemplate.Location = new System.Drawing.Point(556, 190);
            this.btnCopyFromTemplate.Name = "btnCopyFromTemplate";
            this.btnCopyFromTemplate.Size = new System.Drawing.Size(25, 23);
            this.btnCopyFromTemplate.TabIndex = 57;
            this.tooltip.SetToolTip(this.btnCopyFromTemplate, "Copy from Template");
            this.btnCopyFromTemplate.UseVisualStyleBackColor = true;
            this.btnCopyFromTemplate.Click += new System.EventHandler(this.btnCopyFromTemplate_Click);
            // 
            // printWorker
            // 
            this.printWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.printWorker_DoWork);
            this.printWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.printWorker_RunWorkerCompleted);
            // 
            // picComplete
            // 
            this.picComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picComplete.Image = ((System.Drawing.Image)(resources.GetObject("picComplete.Image")));
            this.picComplete.Location = new System.Drawing.Point(587, -1);
            this.picComplete.Name = "picComplete";
            this.picComplete.Size = new System.Drawing.Size(121, 32);
            this.picComplete.TabIndex = 54;
            this.picComplete.TabStop = false;
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnComplete.Location = new System.Drawing.Point(97, 190);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(144, 23);
            this.btnComplete.TabIndex = 55;
            this.btnComplete.Text = "Save && Complete";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // EditInitialTreatmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(705, 225);
            this.Controls.Add(this.btnCopyFromTemplate);
            this.Controls.Add(this.btnSaveAsTemplate);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.picComplete);
            this.Controls.Add(this.btnOpenToTherapist);
            this.Controls.Add(this.btnOpenToWord);
            this.Controls.Add(this.btnPrintToWord);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.grpTreatmentDetail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numTreatmentDuration);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtTreatmentTime);
            this.Controls.Add(this.dtTreatmentDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.drpTherapist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.drpTreatmentType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditInitialTreatmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Initial Treatment";
            this.Load += new System.EventHandler(this.EditInitialTreatmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTreatmentDuration)).EndInit();
            this.grpTreatmentDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picComplete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox drpTreatmentType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox drpTherapist;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtTreatmentDate;
        private System.Windows.Forms.DateTimePicker dtTreatmentTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numTreatmentDuration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpTreatmentDetail;
        private System.Windows.Forms.Panel pnlTreatmentDetail;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPrint;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.PrintDialog printer;
        private System.Windows.Forms.PrintPreviewDialog dlgPrintPreview;
        private System.Windows.Forms.PrintDialog dlgPrinter;
        private System.Windows.Forms.Button btnPrintToWord;
        private System.Windows.Forms.Button btnOpenToWord;
        private System.Windows.Forms.Button btnOpenToTherapist;
        private System.Windows.Forms.ImageList lstIcon;
        private System.Windows.Forms.ToolTip tooltip;
        private System.ComponentModel.BackgroundWorker printWorker;
        private System.Windows.Forms.PictureBox picComplete;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnSaveAsTemplate;
        private System.Windows.Forms.Button btnCopyFromTemplate;
    }
}