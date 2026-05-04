namespace GoogleCalendarHelperTestWindow {
    partial class Form1 {
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
            this.txtCalendarId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnListEvents = new System.Windows.Forms.Button();
            this.dtpGetFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpGetTo = new System.Windows.Forms.DateTimePicker();
            this.grdEventList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNewTitle = new System.Windows.Forms.TextBox();
            this.dtpNewTo = new System.Windows.Forms.DateTimePicker();
            this.dtpNewFrom = new System.Windows.Forms.DateTimePicker();
            this.txtNewDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNewLocation = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAddEvent = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdEventList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCalendarId
            // 
            this.txtCalendarId.Location = new System.Drawing.Point(279, 12);
            this.txtCalendarId.Name = "txtCalendarId";
            this.txtCalendarId.Size = new System.Drawing.Size(338, 20);
            this.txtCalendarId.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter your calendar id (usually it\'s your gmail account):";
            // 
            // btnListEvents
            // 
            this.btnListEvents.Location = new System.Drawing.Point(279, 64);
            this.btnListEvents.Name = "btnListEvents";
            this.btnListEvents.Size = new System.Drawing.Size(338, 23);
            this.btnListEvents.TabIndex = 2;
            this.btnListEvents.Text = "List Events";
            this.btnListEvents.UseVisualStyleBackColor = true;
            this.btnListEvents.Click += new System.EventHandler(this.btnListEvents_Click);
            // 
            // dtpGetFrom
            // 
            this.dtpGetFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpGetFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGetFrom.Location = new System.Drawing.Point(279, 38);
            this.dtpGetFrom.Name = "dtpGetFrom";
            this.dtpGetFrom.ShowCheckBox = true;
            this.dtpGetFrom.Size = new System.Drawing.Size(112, 20);
            this.dtpGetFrom.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select date range:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "From:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(407, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "To:";
            // 
            // dtpGetTo
            // 
            this.dtpGetTo.CustomFormat = "yyyy-MM-dd";
            this.dtpGetTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGetTo.Location = new System.Drawing.Point(436, 38);
            this.dtpGetTo.Name = "dtpGetTo";
            this.dtpGetTo.ShowCheckBox = true;
            this.dtpGetTo.Size = new System.Drawing.Size(114, 20);
            this.dtpGetTo.TabIndex = 7;
            // 
            // grdEventList
            // 
            this.grdEventList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdEventList.Location = new System.Drawing.Point(15, 93);
            this.grdEventList.Name = "grdEventList";
            this.grdEventList.Size = new System.Drawing.Size(602, 164);
            this.grdEventList.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddEvent);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtNewLocation);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNewDescription);
            this.groupBox1.Controls.Add(this.dtpNewTo);
            this.groupBox1.Controls.Add(this.dtpNewFrom);
            this.groupBox1.Controls.Add(this.txtNewTitle);
            this.groupBox1.Location = new System.Drawing.Point(15, 263);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 201);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // txtNewTitle
            // 
            this.txtNewTitle.Location = new System.Drawing.Point(140, 19);
            this.txtNewTitle.Name = "txtNewTitle";
            this.txtNewTitle.Size = new System.Drawing.Size(158, 20);
            this.txtNewTitle.TabIndex = 1;
            // 
            // dtpNewTo
            // 
            this.dtpNewTo.CustomFormat = "yyyy-MM-dd HH:mm tt";
            this.dtpNewTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNewTo.Location = new System.Drawing.Point(333, 97);
            this.dtpNewTo.Name = "dtpNewTo";
            this.dtpNewTo.Size = new System.Drawing.Size(145, 20);
            this.dtpNewTo.TabIndex = 9;
            // 
            // dtpNewFrom
            // 
            this.dtpNewFrom.CustomFormat = "yyyy-MM-dd HH:mm tt";
            this.dtpNewFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNewFrom.Location = new System.Drawing.Point(140, 97);
            this.dtpNewFrom.Name = "dtpNewFrom";
            this.dtpNewFrom.Size = new System.Drawing.Size(158, 20);
            this.dtpNewFrom.TabIndex = 8;
            // 
            // txtNewDescription
            // 
            this.txtNewDescription.Location = new System.Drawing.Point(140, 45);
            this.txtNewDescription.Name = "txtNewDescription";
            this.txtNewDescription.Size = new System.Drawing.Size(158, 20);
            this.txtNewDescription.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Event Title:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Select date range:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(101, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "From:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(304, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "To:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(42, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Event description:";
            // 
            // txtNewLocation
            // 
            this.txtNewLocation.Location = new System.Drawing.Point(140, 71);
            this.txtNewLocation.Name = "txtNewLocation";
            this.txtNewLocation.Size = new System.Drawing.Size(158, 20);
            this.txtNewLocation.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(83, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Location:";
            // 
            // btnAddEvent
            // 
            this.btnAddEvent.Location = new System.Drawing.Point(140, 123);
            this.btnAddEvent.Name = "btnAddEvent";
            this.btnAddEvent.Size = new System.Drawing.Size(338, 23);
            this.btnAddEvent.TabIndex = 18;
            this.btnAddEvent.Text = "Add Event";
            this.btnAddEvent.UseVisualStyleBackColor = true;
            this.btnAddEvent.Click += new System.EventHandler(this.btnAddEvent_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 476);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grdEventList);
            this.Controls.Add(this.dtpGetTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpGetFrom);
            this.Controls.Add(this.btnListEvents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCalendarId);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.grdEventList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCalendarId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnListEvents;
        private System.Windows.Forms.DateTimePicker dtpGetFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpGetTo;
        private System.Windows.Forms.DataGridView grdEventList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddEvent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtNewLocation;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNewDescription;
        private System.Windows.Forms.DateTimePicker dtpNewTo;
        private System.Windows.Forms.DateTimePicker dtpNewFrom;
        private System.Windows.Forms.TextBox txtNewTitle;
    }
}

