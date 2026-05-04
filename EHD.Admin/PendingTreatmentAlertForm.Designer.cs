namespace EHD.Admin {
    partial class PendingTreatmentAlertForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingTreatmentAlertForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtCreatedFrom = new System.Windows.Forms.DateTimePicker();
            this.dtCreatedTo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtServiceDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtServiceDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNumOfPending = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colPatient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoice = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colInvoiceItem = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colInitTreatment = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colFollowUp = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ucInsurer = new EHD.Admin.UCCheckBoxDropDown();
            this.ucTreatment = new EHD.Admin.UCCheckBoxDropDown();
            this.ucTherapist = new EHD.Admin.UCCheckBoxDropDown();
            this.ucPatient = new EHD.Admin.UCAutoCompleteTextBox();
            this.pager = new EHD.Admin.UCPager();
            this.btnAutoCreateFollowups = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // dtCreatedFrom
            // 
            this.dtCreatedFrom.CustomFormat = "MMM dd, yyyy";
            this.dtCreatedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCreatedFrom.Location = new System.Drawing.Point(416, 35);
            this.dtCreatedFrom.Name = "dtCreatedFrom";
            this.dtCreatedFrom.ShowCheckBox = true;
            this.dtCreatedFrom.Size = new System.Drawing.Size(129, 20);
            this.dtCreatedFrom.TabIndex = 6;
            // 
            // dtCreatedTo
            // 
            this.dtCreatedTo.Checked = false;
            this.dtCreatedTo.CustomFormat = "MMM dd, yyyy";
            this.dtCreatedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtCreatedTo.Location = new System.Drawing.Point(577, 35);
            this.dtCreatedTo.Name = "dtCreatedTo";
            this.dtCreatedTo.ShowCheckBox = true;
            this.dtCreatedTo.Size = new System.Drawing.Size(129, 20);
            this.dtCreatedTo.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(271, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 13);
            this.label10.TabIndex = 44;
            this.label10.Text = "Invoice Creation Date From:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(551, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "To";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(626, 85);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(78, 26);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(367, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Patient:";
            // 
            // dtServiceDateTo
            // 
            this.dtServiceDateTo.Checked = false;
            this.dtServiceDateTo.CustomFormat = "MMM dd, yyyy";
            this.dtServiceDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtServiceDateTo.Location = new System.Drawing.Point(577, 10);
            this.dtServiceDateTo.Name = "dtServiceDateTo";
            this.dtServiceDateTo.ShowCheckBox = true;
            this.dtServiceDateTo.Size = new System.Drawing.Size(129, 20);
            this.dtServiceDateTo.TabIndex = 5;
            // 
            // dtServiceDateFrom
            // 
            this.dtServiceDateFrom.Checked = false;
            this.dtServiceDateFrom.CustomFormat = "MMM dd, yyyy";
            this.dtServiceDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtServiceDateFrom.Location = new System.Drawing.Point(416, 10);
            this.dtServiceDateFrom.Name = "dtServiceDateFrom";
            this.dtServiceDateFrom.ShowCheckBox = true;
            this.dtServiceDateFrom.Size = new System.Drawing.Size(129, 20);
            this.dtServiceDateFrom.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Treatment:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(274, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Invoice Service Date From:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Therapist:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(25, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Insurer:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(551, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "To";
            // 
            // lblNumOfPending
            // 
            this.lblNumOfPending.AutoSize = true;
            this.lblNumOfPending.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumOfPending.ForeColor = System.Drawing.Color.Blue;
            this.lblNumOfPending.Location = new System.Drawing.Point(144, 95);
            this.lblNumOfPending.Name = "lblNumOfPending";
            this.lblNumOfPending.Size = new System.Drawing.Size(17, 19);
            this.lblNumOfPending.TabIndex = 12;
            this.lblNumOfPending.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Pending invoice found:";
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AllowUserToDeleteRows = false;
            this.grdList.AllowUserToResizeRows = false;
            this.grdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdList.BackgroundColor = System.Drawing.Color.White;
            this.grdList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPatient,
            this.colInvoice,
            this.colInvoiceItem,
            this.colInitTreatment,
            this.colFollowUp});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdList.Location = new System.Drawing.Point(1, 114);
            this.grdList.MultiSelect = false;
            this.grdList.Name = "grdList";
            this.grdList.ReadOnly = true;
            this.grdList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdList.RowHeadersVisible = false;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdList.ShowEditingIcon = false;
            this.grdList.Size = new System.Drawing.Size(714, 244);
            this.grdList.TabIndex = 10;
            this.grdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdList_CellContentClick);
            // 
            // colPatient
            // 
            this.colPatient.FillWeight = 25F;
            this.colPatient.HeaderText = "Patient";
            this.colPatient.Name = "colPatient";
            this.colPatient.ReadOnly = true;
            // 
            // colInvoice
            // 
            this.colInvoice.FillWeight = 25F;
            this.colInvoice.HeaderText = "Invoice";
            this.colInvoice.Name = "colInvoice";
            this.colInvoice.ReadOnly = true;
            // 
            // colInvoiceItem
            // 
            this.colInvoiceItem.FillWeight = 25F;
            this.colInvoiceItem.HeaderText = "Invoice Item";
            this.colInvoiceItem.Name = "colInvoiceItem";
            this.colInvoiceItem.ReadOnly = true;
            // 
            // colInitTreatment
            // 
            this.colInitTreatment.FillWeight = 25F;
            this.colInitTreatment.HeaderText = "Initial Treatment";
            this.colInitTreatment.Name = "colInitTreatment";
            this.colInitTreatment.ReadOnly = true;
            // 
            // colFollowUp
            // 
            this.colFollowUp.FillWeight = 25F;
            this.colFollowUp.HeaderText = "Follow Up";
            this.colFollowUp.Name = "colFollowUp";
            this.colFollowUp.ReadOnly = true;
            // 
            // ucInsurer
            // 
            this.ucInsurer.Location = new System.Drawing.Point(73, 11);
            this.ucInsurer.Name = "ucInsurer";
            this.ucInsurer.Size = new System.Drawing.Size(192, 19);
            this.ucInsurer.TabIndex = 1;
            this.ucInsurer.UpdateDataList += new System.EventHandler(this.ucInsurer_UpdateDataList);
            // 
            // ucTreatment
            // 
            this.ucTreatment.Location = new System.Drawing.Point(73, 36);
            this.ucTreatment.Name = "ucTreatment";
            this.ucTreatment.Size = new System.Drawing.Size(192, 19);
            this.ucTreatment.TabIndex = 2;
            this.ucTreatment.UpdateDataList += new System.EventHandler(this.ucTreatment_UpdateDataList);
            // 
            // ucTherapist
            // 
            this.ucTherapist.Location = new System.Drawing.Point(73, 61);
            this.ucTherapist.Name = "ucTherapist";
            this.ucTherapist.Size = new System.Drawing.Size(192, 19);
            this.ucTherapist.TabIndex = 3;
            this.ucTherapist.UpdateDataList += new System.EventHandler(this.ucTherapist_UpdateDataList);
            // 
            // ucPatient
            // 
            this.ucPatient.ActionDelayMilliseconds = ((uint)(1000u));
            this.ucPatient.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucPatient.InputText = "";
            this.ucPatient.Location = new System.Drawing.Point(416, 60);
            this.ucPatient.MinInputLengthToStartSearch = ((uint)(2u));
            this.ucPatient.Name = "ucPatient";
            this.ucPatient.SelectedItem = null;
            this.ucPatient.Size = new System.Drawing.Size(290, 20);
            this.ucPatient.TabIndex = 8;
            this.ucPatient.Tooltip = "Enter File#, First Name or Last Name to search";
            this.ucPatient.MinInputLengthReached += new System.EventHandler(this.ucPatient_MinInputLengthReached);
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pager.Location = new System.Drawing.Point(313, 361);
            this.pager.Margin = new System.Windows.Forms.Padding(0);
            this.pager.Name = "pager";
            this.pager.Page = 1;
            this.pager.PageSize = 20;
            this.pager.Size = new System.Drawing.Size(402, 27);
            this.pager.TabIndex = 11;
            this.pager.TotalPage = 0;
            // 
            // btnAutoCreateFollowups
            // 
            this.btnAutoCreateFollowups.Location = new System.Drawing.Point(450, 87);
            this.btnAutoCreateFollowups.Name = "btnAutoCreateFollowups";
            this.btnAutoCreateFollowups.Size = new System.Drawing.Size(170, 23);
            this.btnAutoCreateFollowups.TabIndex = 46;
            this.btnAutoCreateFollowups.Text = "Auto Create Followups";
            this.btnAutoCreateFollowups.UseVisualStyleBackColor = true;
            this.btnAutoCreateFollowups.Click += new System.EventHandler(this.btnAutoCreateFollowups_Click);
            // 
            // PendingTreatmentAlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 388);
            this.Controls.Add(this.btnAutoCreateFollowups);
            this.Controls.Add(this.pager);
            this.Controls.Add(this.ucPatient);
            this.Controls.Add(this.ucTherapist);
            this.Controls.Add(this.ucTreatment);
            this.Controls.Add(this.ucInsurer);
            this.Controls.Add(this.dtCreatedFrom);
            this.Controls.Add(this.dtCreatedTo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtServiceDateTo);
            this.Controls.Add(this.dtServiceDateFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblNumOfPending);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grdList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(730, 420);
            this.Name = "PendingTreatmentAlertForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Treatment Alert";
            this.Load += new System.EventHandler(this.PendingTreatmentAlertForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtCreatedFrom;
        private System.Windows.Forms.DateTimePicker dtCreatedTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtServiceDateTo;
        private System.Windows.Forms.DateTimePicker dtServiceDateFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNumOfPending;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatient;
        private System.Windows.Forms.DataGridViewLinkColumn colInvoice;
        private System.Windows.Forms.DataGridViewLinkColumn colInvoiceItem;
        private System.Windows.Forms.DataGridViewLinkColumn colInitTreatment;
        private System.Windows.Forms.DataGridViewLinkColumn colFollowUp;
        private UCCheckBoxDropDown ucInsurer;
        private UCCheckBoxDropDown ucTreatment;
        private UCCheckBoxDropDown ucTherapist;
        private UCAutoCompleteTextBox ucPatient;
        private UCPager pager;
        private System.Windows.Forms.Button btnAutoCreateFollowups;
    }
}