namespace EHD.Admin {
    partial class AdvancedSearchForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedSearchForm));
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.ucTherapist = new EHD.Admin.UCCheckBoxDropDown();
            this.ucTreatment = new EHD.Admin.UCCheckBoxDropDown();
            this.ucInsurer = new EHD.Admin.UCCheckBoxDropDown();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.trvList = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.rdoExpandAll = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdoCollapseAll = new System.Windows.Forms.RadioButton();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.pager = new EHD.Admin.UCPager();
            this.lblPatientNumber = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTreatmentNumber = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(294, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Insurer:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(460, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Therapist:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Treatment:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dtTo);
            this.panel1.Controls.Add(this.ucTherapist);
            this.panel1.Controls.Add(this.ucTreatment);
            this.panel1.Controls.Add(this.ucInsurer);
            this.panel1.Controls.Add(this.dtFrom);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 79);
            this.panel1.TabIndex = 24;
            // 
            // dtTo
            // 
            this.dtTo.Checked = false;
            this.dtTo.CustomFormat = "MMM dd, yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(488, 30);
            this.dtTo.Name = "dtTo";
            this.dtTo.ShowCheckBox = true;
            this.dtTo.Size = new System.Drawing.Size(120, 20);
            this.dtTo.TabIndex = 25;
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // ucTherapist
            // 
            this.ucTherapist.Location = new System.Drawing.Point(96, 55);
            this.ucTherapist.Name = "ucTherapist";
            this.ucTherapist.Size = new System.Drawing.Size(192, 19);
            this.ucTherapist.TabIndex = 11;
            this.ucTherapist.UpdateDataList += new System.EventHandler(this.ucTherapist_UpdateDataList);
            // 
            // ucTreatment
            // 
            this.ucTreatment.Location = new System.Drawing.Point(96, 30);
            this.ucTreatment.Name = "ucTreatment";
            this.ucTreatment.Size = new System.Drawing.Size(192, 19);
            this.ucTreatment.TabIndex = 11;
            this.ucTreatment.UpdateDataList += new System.EventHandler(this.ucTreatment_UpdateDataList);
            // 
            // ucInsurer
            // 
            this.ucInsurer.Location = new System.Drawing.Point(96, 5);
            this.ucInsurer.Name = "ucInsurer";
            this.ucInsurer.Size = new System.Drawing.Size(192, 19);
            this.ucInsurer.TabIndex = 11;
            this.ucInsurer.UpdateDataList += new System.EventHandler(this.ucInsurer_UpdateDataList);
            // 
            // dtFrom
            // 
            this.dtFrom.Checked = false;
            this.dtFrom.CustomFormat = "MMM dd, yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(334, 30);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ShowCheckBox = true;
            this.dtFrom.Size = new System.Drawing.Size(120, 20);
            this.dtFrom.TabIndex = 24;
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.trvList);
            this.panel2.Location = new System.Drawing.Point(0, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 293);
            this.panel2.TabIndex = 25;
            // 
            // trvList
            // 
            this.trvList.BackColor = System.Drawing.Color.White;
            this.trvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvList.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvList.HotTracking = true;
            this.trvList.ImageIndex = 0;
            this.trvList.ImageList = this.imgList;
            this.trvList.Location = new System.Drawing.Point(0, 0);
            this.trvList.Name = "trvList";
            this.trvList.SelectedImageIndex = 0;
            this.trvList.Size = new System.Drawing.Size(849, 293);
            this.trvList.TabIndex = 0;
            this.trvList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvList_NodeMouseClick);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "Male");
            this.imgList.Images.SetKeyName(1, "Female");
            this.imgList.Images.SetKeyName(2, "Patient");
            this.imgList.Images.SetKeyName(3, "Doctor");
            this.imgList.Images.SetKeyName(4, "InitialTreatment");
            this.imgList.Images.SetKeyName(5, "FollowUpTreatment");
            // 
            // rdoExpandAll
            // 
            this.rdoExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("rdoExpandAll.Image")));
            this.rdoExpandAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdoExpandAll.Location = new System.Drawing.Point(3, 5);
            this.rdoExpandAll.Name = "rdoExpandAll";
            this.rdoExpandAll.Size = new System.Drawing.Size(101, 24);
            this.rdoExpandAll.TabIndex = 52;
            this.rdoExpandAll.Text = "Expand All";
            this.rdoExpandAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rdoExpandAll.UseVisualStyleBackColor = true;
            this.rdoExpandAll.Click += new System.EventHandler(this.rdoExpandAll_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblTreatmentNumber);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.lblPatientNumber);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.rdoCollapseAll);
            this.panel3.Controls.Add(this.rdoExpandAll);
            this.panel3.Location = new System.Drawing.Point(0, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(849, 32);
            this.panel3.TabIndex = 53;
            // 
            // rdoCollapseAll
            // 
            this.rdoCollapseAll.Checked = true;
            this.rdoCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("rdoCollapseAll.Image")));
            this.rdoCollapseAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rdoCollapseAll.Location = new System.Drawing.Point(110, 5);
            this.rdoCollapseAll.Name = "rdoCollapseAll";
            this.rdoCollapseAll.Size = new System.Drawing.Size(101, 24);
            this.rdoCollapseAll.TabIndex = 53;
            this.rdoCollapseAll.TabStop = true;
            this.rdoCollapseAll.Text = "Collapse All";
            this.rdoCollapseAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rdoCollapseAll.UseVisualStyleBackColor = true;
            this.rdoCollapseAll.Click += new System.EventHandler(this.rdoCollapseAll_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuView});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(100, 26);
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // mnuView
            // 
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(99, 22);
            this.mnuView.Text = "View";
            // 
            // pager
            // 
            this.pager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pager.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pager.Location = new System.Drawing.Point(447, 412);
            this.pager.Margin = new System.Windows.Forms.Padding(0);
            this.pager.Name = "pager";
            this.pager.Page = 1;
            this.pager.PageSize = 20;
            this.pager.Size = new System.Drawing.Size(402, 33);
            this.pager.TabIndex = 54;
            this.pager.TotalPage = 0;
            // 
            // lblPatientNumber
            // 
            this.lblPatientNumber.AutoSize = true;
            this.lblPatientNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNumber.Location = new System.Drawing.Point(431, 10);
            this.lblPatientNumber.Name = "lblPatientNumber";
            this.lblPatientNumber.Size = new System.Drawing.Size(14, 13);
            this.lblPatientNumber.TabIndex = 57;
            this.lblPatientNumber.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(234, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Total Number of Patients Found:";
            // 
            // lblTreatmentNumber
            // 
            this.lblTreatmentNumber.AutoSize = true;
            this.lblTreatmentNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTreatmentNumber.Location = new System.Drawing.Point(630, 10);
            this.lblTreatmentNumber.Name = "lblTreatmentNumber";
            this.lblTreatmentNumber.Size = new System.Drawing.Size(14, 13);
            this.lblTreatmentNumber.TabIndex = 59;
            this.lblTreatmentNumber.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(485, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "Total treatments found:";
            // 
            // AdvancedSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 445);
            this.Controls.Add(this.pager);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedSearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Search";
            this.Load += new System.EventHandler(this.AdvancedSearchForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtTo;
        private UCCheckBoxDropDown ucTherapist;
        private UCCheckBoxDropDown ucTreatment;
        private UCCheckBoxDropDown ucInsurer;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.TreeView trvList;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.RadioButton rdoExpandAll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdoCollapseAll;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private UCPager pager;
        private System.Windows.Forms.Label lblTreatmentNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPatientNumber;
        private System.Windows.Forms.Label label6;
    }
}