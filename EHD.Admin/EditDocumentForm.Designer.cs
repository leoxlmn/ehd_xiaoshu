namespace EHD.Admin {
    partial class EditDocumentForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDocumentForm));
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDocId = new System.Windows.Forms.Label();
            this.drpDocType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtCreationDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.dtCreationTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblLoadedTime = new System.Windows.Forms.Label();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lnkFile = new System.Windows.Forms.LinkLabel();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.ucPatient = new EHD.Admin.UCAutoCompleteTextBox();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(324, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 16);
            this.label18.TabIndex = 122;
            this.label18.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Patient:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Document Id:";
            // 
            // lblDocId
            // 
            this.lblDocId.AutoSize = true;
            this.lblDocId.Location = new System.Drawing.Point(118, 9);
            this.lblDocId.Name = "lblDocId";
            this.lblDocId.Size = new System.Drawing.Size(13, 13);
            this.lblDocId.TabIndex = 2;
            this.lblDocId.Text = "0";
            // 
            // drpDocType
            // 
            this.drpDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drpDocType.FormattingEnabled = true;
            this.drpDocType.Location = new System.Drawing.Point(121, 83);
            this.drpDocType.Name = "drpDocType";
            this.drpDocType.Size = new System.Drawing.Size(285, 21);
            this.drpDocType.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(409, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 127;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Document Type:";
            // 
            // dtCreationDate
            // 
            this.dtCreationDate.CustomFormat = "ddd, MMM dd, yyyy";
            this.dtCreationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCreationDate.Location = new System.Drawing.Point(121, 110);
            this.dtCreationDate.Name = "dtCreationDate";
            this.dtCreationDate.Size = new System.Drawing.Size(171, 22);
            this.dtCreationDate.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Creation Time:";
            // 
            // dtCreationTime
            // 
            this.dtCreationTime.CustomFormat = "hh:mm tt";
            this.dtCreationTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCreationTime.Location = new System.Drawing.Point(298, 110);
            this.dtCreationTime.Name = "dtCreationTime";
            this.dtCreationTime.ShowUpDown = true;
            this.dtCreationTime.Size = new System.Drawing.Size(108, 22);
            this.dtCreationTime.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(409, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 16);
            this.label5.TabIndex = 144;
            this.label5.Text = "*";
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(121, 138);
            this.txtNote.MaxLength = 255;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(285, 72);
            this.txtNote.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Note:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Loaded Time:";
            // 
            // lblLoadedTime
            // 
            this.lblLoadedTime.AutoSize = true;
            this.lblLoadedTime.Location = new System.Drawing.Point(118, 31);
            this.lblLoadedTime.Name = "lblLoadedTime";
            this.lblLoadedTime.Size = new System.Drawing.Size(13, 13);
            this.lblLoadedTime.TabIndex = 4;
            this.lblLoadedTime.Text = "0";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(121, 235);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(285, 23);
            this.btnBrowseFile.TabIndex = 16;
            this.btnBrowseFile.Text = "Upload a new file";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(87, 219);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "File:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(412, 277);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Orange;
            this.btnSave.Location = new System.Drawing.Point(121, 277);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lnkFile
            // 
            this.lnkFile.AutoSize = true;
            this.lnkFile.Location = new System.Drawing.Point(121, 219);
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Size = new System.Drawing.Size(49, 13);
            this.lnkFile.TabIndex = 17;
            this.lnkFile.TabStop = true;
            this.lnkFile.Text = "view file";
            this.lnkFile.Click += new System.EventHandler(this.lnkFile_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "PDF files|*.pdf|Image files (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|All files (*.*)" +
    "|*.*";
            // 
            // ucPatient
            // 
            this.ucPatient.ActionDelayMilliseconds = ((uint)(1000u));
            this.ucPatient.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucPatient.InputText = "";
            this.ucPatient.Location = new System.Drawing.Point(121, 57);
            this.ucPatient.MinInputLengthToStartSearch = ((uint)(2u));
            this.ucPatient.Name = "ucPatient";
            this.ucPatient.SelectedItem = null;
            this.ucPatient.Size = new System.Drawing.Size(197, 20);
            this.ucPatient.TabIndex = 6;
            this.ucPatient.Tooltip = "Enter File#, First Name or Last Name to search";
            this.ucPatient.MinInputLengthReached += new System.EventHandler(this.ucPatient_MinInputLengthReached);
            // 
            // EditDocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 318);
            this.Controls.Add(this.lnkFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.lblLoadedTime);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtCreationDate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dtCreationTime);
            this.Controls.Add(this.drpDocType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDocId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucPatient);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "EditDocumentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Document";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditDocumentForm_FormClosed);
            this.Load += new System.EventHandler(this.EditDocumentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCAutoCompleteTextBox ucPatient;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDocId;
        private System.Windows.Forms.ComboBox drpDocType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtCreationDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtCreationTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblLoadedTime;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.LinkLabel lnkFile;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    }
}