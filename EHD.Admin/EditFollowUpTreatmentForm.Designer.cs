namespace EHD.Admin {
    partial class EditFollowUpTreatmentForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFollowUpTreatmentForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInitialTreatment = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInitialTherapist = new System.Windows.Forms.TextBox();
            this.drpTherapist = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numTreatmentDuration = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dtTreatmentTime = new System.Windows.Forms.DateTimePicker();
            this.dtTreatmentDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.grpSOAP = new System.Windows.Forms.GroupBox();
            this.pnlSOAP = new System.Windows.Forms.Panel();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveAsTemplate = new System.Windows.Forms.Button();
            this.btnCopyFromTemplate = new System.Windows.Forms.Button();
            this.lstIcon = new System.Windows.Forms.ImageList(this.components);
            this.btnOpenToTherapist = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.picComplete = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numTreatmentDuration)).BeginInit();
            this.grpSOAP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComplete)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(614, 709);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(12, 709);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(107, 9);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(171, 22);
            this.txtPatientName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Patient:";
            // 
            // txtInitialTreatment
            // 
            this.txtInitialTreatment.Location = new System.Drawing.Point(107, 35);
            this.txtInitialTreatment.Name = "txtInitialTreatment";
            this.txtInitialTreatment.ReadOnly = true;
            this.txtInitialTreatment.Size = new System.Drawing.Size(171, 22);
            this.txtInitialTreatment.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Initial Treatment:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Therapist:";
            // 
            // txtInitialTherapist
            // 
            this.txtInitialTherapist.Location = new System.Drawing.Point(107, 63);
            this.txtInitialTherapist.Name = "txtInitialTherapist";
            this.txtInitialTherapist.ReadOnly = true;
            this.txtInitialTherapist.Size = new System.Drawing.Size(171, 22);
            this.txtInitialTherapist.TabIndex = 7;
            // 
            // drpTherapist
            // 
            this.drpTherapist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpTherapist.FormattingEnabled = true;
            this.drpTherapist.Location = new System.Drawing.Point(402, 63);
            this.drpTherapist.Name = "drpTherapist";
            this.drpTherapist.Size = new System.Drawing.Size(171, 21);
            this.drpTherapist.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(284, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Follow-Up Therapist:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(525, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "minutes";
            // 
            // numTreatmentDuration
            // 
            this.numTreatmentDuration.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTreatmentDuration.Location = new System.Drawing.Point(469, 106);
            this.numTreatmentDuration.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numTreatmentDuration.Name = "numTreatmentDuration";
            this.numTreatmentDuration.Size = new System.Drawing.Size(47, 22);
            this.numTreatmentDuration.TabIndex = 18;
            this.numTreatmentDuration.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(351, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Treatment Duration:";
            // 
            // dtTreatmentTime
            // 
            this.dtTreatmentTime.CustomFormat = "h:mm tt";
            this.dtTreatmentTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTreatmentTime.Location = new System.Drawing.Point(242, 106);
            this.dtTreatmentTime.Name = "dtTreatmentTime";
            this.dtTreatmentTime.ShowUpDown = true;
            this.dtTreatmentTime.Size = new System.Drawing.Size(71, 22);
            this.dtTreatmentTime.TabIndex = 16;
            this.dtTreatmentTime.Value = new System.DateTime(2012, 6, 28, 10, 0, 0, 0);
            // 
            // dtTreatmentDate
            // 
            this.dtTreatmentDate.CustomFormat = "ddd, MMM dd, yyyy";
            this.dtTreatmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTreatmentDate.Location = new System.Drawing.Point(107, 106);
            this.dtTreatmentDate.Name = "dtTreatmentDate";
            this.dtTreatmentDate.Size = new System.Drawing.Size(134, 22);
            this.dtTreatmentDate.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Treatment Date:";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(107, 132);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNote.Size = new System.Drawing.Size(466, 66);
            this.txtNote.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Treatment Note:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(579, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(16, 16);
            this.label20.TabIndex = 41;
            this.label20.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(319, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(579, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 16);
            this.label10.TabIndex = 43;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(579, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 44;
            this.label11.Text = "*";
            // 
            // grpSOAP
            // 
            this.grpSOAP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSOAP.Controls.Add(this.pnlSOAP);
            this.grpSOAP.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSOAP.Location = new System.Drawing.Point(12, 204);
            this.grpSOAP.Name = "grpSOAP";
            this.grpSOAP.Size = new System.Drawing.Size(677, 502);
            this.grpSOAP.TabIndex = 45;
            this.grpSOAP.TabStop = false;
            this.grpSOAP.Text = "SOAP";
            // 
            // pnlSOAP
            // 
            this.pnlSOAP.AutoScroll = true;
            this.pnlSOAP.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlSOAP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSOAP.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSOAP.Location = new System.Drawing.Point(3, 18);
            this.pnlSOAP.Name = "pnlSOAP";
            this.pnlSOAP.Size = new System.Drawing.Size(671, 481);
            this.pnlSOAP.TabIndex = 0;
            // 
            // btnSaveAsTemplate
            // 
            this.btnSaveAsTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAsTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveAsTemplate.BackgroundImage")));
            this.btnSaveAsTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSaveAsTemplate.Location = new System.Drawing.Point(581, 709);
            this.btnSaveAsTemplate.Name = "btnSaveAsTemplate";
            this.btnSaveAsTemplate.Size = new System.Drawing.Size(27, 23);
            this.btnSaveAsTemplate.TabIndex = 54;
            this.tooltip.SetToolTip(this.btnSaveAsTemplate, "Set as Template");
            this.btnSaveAsTemplate.UseVisualStyleBackColor = true;
            this.btnSaveAsTemplate.Click += new System.EventHandler(this.btnSaveAsTemplate_Click);
            // 
            // btnCopyFromTemplate
            // 
            this.btnCopyFromTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFromTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCopyFromTemplate.BackgroundImage")));
            this.btnCopyFromTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCopyFromTemplate.Location = new System.Drawing.Point(548, 709);
            this.btnCopyFromTemplate.Name = "btnCopyFromTemplate";
            this.btnCopyFromTemplate.Size = new System.Drawing.Size(27, 23);
            this.btnCopyFromTemplate.TabIndex = 55;
            this.tooltip.SetToolTip(this.btnCopyFromTemplate, "Copy from Template");
            this.btnCopyFromTemplate.UseVisualStyleBackColor = true;
            this.btnCopyFromTemplate.Click += new System.EventHandler(this.btnCopyFromTemplate_Click);
            // 
            // lstIcon
            // 
            this.lstIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("lstIcon.ImageStream")));
            this.lstIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.lstIcon.Images.SetKeyName(0, "Open");
            this.lstIcon.Images.SetKeyName(1, "Closed");
            // 
            // btnOpenToTherapist
            // 
            this.btnOpenToTherapist.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenToTherapist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOpenToTherapist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenToTherapist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenToTherapist.ImageKey = "Closed";
            this.btnOpenToTherapist.ImageList = this.lstIcon;
            this.btnOpenToTherapist.Location = new System.Drawing.Point(405, 23);
            this.btnOpenToTherapist.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenToTherapist.Name = "btnOpenToTherapist";
            this.btnOpenToTherapist.Size = new System.Drawing.Size(168, 37);
            this.btnOpenToTherapist.TabIndex = 50;
            this.btnOpenToTherapist.Text = "Closed";
            this.btnOpenToTherapist.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOpenToTherapist.UseVisualStyleBackColor = false;
            this.btnOpenToTherapist.Click += new System.EventHandler(this.btnOpenToTherapist_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnComplete.Location = new System.Drawing.Point(93, 709);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(165, 23);
            this.btnComplete.TabIndex = 52;
            this.btnComplete.Text = "Save && Complete";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // picComplete
            // 
            this.picComplete.Image = ((System.Drawing.Image)(resources.GetObject("picComplete.Image")));
            this.picComplete.Location = new System.Drawing.Point(584, -1);
            this.picComplete.Name = "picComplete";
            this.picComplete.Size = new System.Drawing.Size(121, 32);
            this.picComplete.TabIndex = 53;
            this.picComplete.TabStop = false;
            // 
            // EditFollowUpTreatmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(705, 739);
            this.Controls.Add(this.btnCopyFromTemplate);
            this.Controls.Add(this.btnSaveAsTemplate);
            this.Controls.Add(this.picComplete);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.btnOpenToTherapist);
            this.Controls.Add(this.grpSOAP);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numTreatmentDuration);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtTreatmentTime);
            this.Controls.Add(this.dtTreatmentDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.drpTherapist);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInitialTherapist);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInitialTreatment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditFollowUpTreatmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Follow-Up Treatment";
            this.Load += new System.EventHandler(this.EditFollowUpTreatmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTreatmentDuration)).EndInit();
            this.grpSOAP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picComplete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInitialTreatment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInitialTherapist;
        private System.Windows.Forms.ComboBox drpTherapist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numTreatmentDuration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtTreatmentTime;
        private System.Windows.Forms.DateTimePicker dtTreatmentDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox grpSOAP;
        private System.Windows.Forms.Panel pnlSOAP;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.ImageList lstIcon;
        private System.Windows.Forms.Button btnOpenToTherapist;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.PictureBox picComplete;
        private System.Windows.Forms.Button btnSaveAsTemplate;
        private System.Windows.Forms.Button btnCopyFromTemplate;
    }
}