namespace EHD.Admin {
    partial class EditTherapyTypeForm {
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
            this.label18 = new System.Windows.Forms.Label();
            this.txtTherapyTypeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnableMinutesPerTreatment = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numMinutesPerTreatment = new System.Windows.Forms.NumericUpDown();
            this.numMaxTreatmentsPerDay = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEnableMaxTreatmentsPerDay = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkShowDurationOnInvoice = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDefaultPrice = new System.Windows.Forms.TextBox();
            this.chkAllowTimeOverlap = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutesPerTreatment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTreatmentsPerDay)).BeginInit();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(451, 12);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(15, 16);
            this.label18.TabIndex = 36;
            this.label18.Text = "*";
            // 
            // txtTherapyTypeName
            // 
            this.txtTherapyTypeName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTherapyTypeName.Location = new System.Drawing.Point(249, 6);
            this.txtTherapyTypeName.Name = "txtTherapyTypeName";
            this.txtTherapyTypeName.Size = new System.Drawing.Size(196, 22);
            this.txtTherapyTypeName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Therapy Type Name:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(387, 269);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 269);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enable Minutes per Treatment:";
            // 
            // chkEnableMinutesPerTreatment
            // 
            this.chkEnableMinutesPerTreatment.AutoSize = true;
            this.chkEnableMinutesPerTreatment.Location = new System.Drawing.Point(249, 70);
            this.chkEnableMinutesPerTreatment.Name = "chkEnableMinutesPerTreatment";
            this.chkEnableMinutesPerTreatment.Size = new System.Drawing.Size(68, 17);
            this.chkEnableMinutesPerTreatment.TabIndex = 3;
            this.chkEnableMinutesPerTreatment.Text = "Enabled";
            this.chkEnableMinutesPerTreatment.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Minutes per Treatment:";
            // 
            // numMinutesPerTreatment
            // 
            this.numMinutesPerTreatment.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMinutesPerTreatment.Location = new System.Drawing.Point(249, 93);
            this.numMinutesPerTreatment.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numMinutesPerTreatment.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numMinutesPerTreatment.Name = "numMinutesPerTreatment";
            this.numMinutesPerTreatment.Size = new System.Drawing.Size(56, 22);
            this.numMinutesPerTreatment.TabIndex = 4;
            this.numMinutesPerTreatment.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // numMaxTreatmentsPerDay
            // 
            this.numMaxTreatmentsPerDay.Location = new System.Drawing.Point(249, 152);
            this.numMaxTreatmentsPerDay.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMaxTreatmentsPerDay.Name = "numMaxTreatmentsPerDay";
            this.numMaxTreatmentsPerDay.Size = new System.Drawing.Size(56, 22);
            this.numMaxTreatmentsPerDay.TabIndex = 6;
            this.numMaxTreatmentsPerDay.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(113, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Max treatments per day:";
            // 
            // chkEnableMaxTreatmentsPerDay
            // 
            this.chkEnableMaxTreatmentsPerDay.AutoSize = true;
            this.chkEnableMaxTreatmentsPerDay.Location = new System.Drawing.Point(249, 129);
            this.chkEnableMaxTreatmentsPerDay.Name = "chkEnableMaxTreatmentsPerDay";
            this.chkEnableMaxTreatmentsPerDay.Size = new System.Drawing.Size(68, 17);
            this.chkEnableMaxTreatmentsPerDay.TabIndex = 5;
            this.chkEnableMaxTreatmentsPerDay.Text = "Enabled";
            this.chkEnableMaxTreatmentsPerDay.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Enable Max treatments per day:";
            // 
            // chkShowDurationOnInvoice
            // 
            this.chkShowDurationOnInvoice.AutoSize = true;
            this.chkShowDurationOnInvoice.Location = new System.Drawing.Point(249, 228);
            this.chkShowDurationOnInvoice.Name = "chkShowDurationOnInvoice";
            this.chkShowDurationOnInvoice.Size = new System.Drawing.Size(68, 17);
            this.chkShowDurationOnInvoice.TabIndex = 7;
            this.chkShowDurationOnInvoice.Text = "Enabled";
            this.chkShowDurationOnInvoice.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Show Service Duration on Invoice:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(168, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "Default Price:";
            // 
            // txtDefaultPrice
            // 
            this.txtDefaultPrice.Location = new System.Drawing.Point(249, 35);
            this.txtDefaultPrice.MaxLength = 8;
            this.txtDefaultPrice.Name = "txtDefaultPrice";
            this.txtDefaultPrice.Size = new System.Drawing.Size(100, 22);
            this.txtDefaultPrice.TabIndex = 2;
            // 
            // chkAllowTimeOverlap
            // 
            this.chkAllowTimeOverlap.AutoSize = true;
            this.chkAllowTimeOverlap.Location = new System.Drawing.Point(249, 192);
            this.chkAllowTimeOverlap.Name = "chkAllowTimeOverlap";
            this.chkAllowTimeOverlap.Size = new System.Drawing.Size(68, 17);
            this.chkAllowTimeOverlap.TabIndex = 46;
            this.chkAllowTimeOverlap.Text = "Allowed";
            this.chkAllowTimeOverlap.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(207, 13);
            this.label8.TabIndex = 47;
            this.label8.Text = "Allow time overlap on other treaments:";
            // 
            // EditTherapyTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 309);
            this.Controls.Add(this.chkAllowTimeOverlap);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDefaultPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkShowDurationOnInvoice);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numMaxTreatmentsPerDay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkEnableMaxTreatmentsPerDay);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numMinutesPerTreatment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkEnableMinutesPerTreatment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtTherapyTypeName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditTherapyTypeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Therapy Type";
            this.Load += new System.EventHandler(this.EditTherapyTypeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMinutesPerTreatment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTreatmentsPerDay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtTherapyTypeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnableMinutesPerTreatment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numMinutesPerTreatment;
        private System.Windows.Forms.NumericUpDown numMaxTreatmentsPerDay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkEnableMaxTreatmentsPerDay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkShowDurationOnInvoice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDefaultPrice;
        private System.Windows.Forms.CheckBox chkAllowTimeOverlap;
        private System.Windows.Forms.Label label8;
    }
}