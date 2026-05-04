namespace EHD.Admin {
    partial class ManageTemplateForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageTemplateForm));
            this.grdTemplate = new System.Windows.Forms.DataGridView();
            this.btnSetDefault = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoInitial = new System.Windows.Forms.RadioButton();
            this.rdoFollowup = new System.Windows.Forms.RadioButton();
            this.drpTreatmentType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTemplate
            // 
            this.grdTemplate.AllowUserToAddRows = false;
            this.grdTemplate.AllowUserToDeleteRows = false;
            this.grdTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTemplate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTemplate.Location = new System.Drawing.Point(2, 61);
            this.grdTemplate.MultiSelect = false;
            this.grdTemplate.Name = "grdTemplate";
            this.grdTemplate.ReadOnly = true;
            this.grdTemplate.RowHeadersVisible = false;
            this.grdTemplate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTemplate.Size = new System.Drawing.Size(596, 150);
            this.grdTemplate.TabIndex = 4;
            this.grdTemplate.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdTemplate_DataBindingComplete);
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetDefault.BackColor = System.Drawing.Color.DarkOrange;
            this.btnSetDefault.Location = new System.Drawing.Point(12, 217);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(107, 23);
            this.btnSetDefault.TabIndex = 5;
            this.btnSetDefault.Text = "Set Default";
            this.btnSetDefault.UseVisualStyleBackColor = false;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(125, 217);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete Template";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(512, 217);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Inital or Followup:";
            // 
            // rdoInitial
            // 
            this.rdoInitial.AutoSize = true;
            this.rdoInitial.Checked = true;
            this.rdoInitial.Location = new System.Drawing.Point(163, 9);
            this.rdoInitial.Name = "rdoInitial";
            this.rdoInitial.Size = new System.Drawing.Size(108, 17);
            this.rdoInitial.TabIndex = 1;
            this.rdoInitial.TabStop = true;
            this.rdoInitial.Text = "Initial Treatment";
            this.rdoInitial.UseVisualStyleBackColor = true;
            this.rdoInitial.CheckedChanged += new System.EventHandler(this.rdoInitial_CheckedChanged);
            // 
            // rdoFollowup
            // 
            this.rdoFollowup.AutoSize = true;
            this.rdoFollowup.Location = new System.Drawing.Point(277, 9);
            this.rdoFollowup.Name = "rdoFollowup";
            this.rdoFollowup.Size = new System.Drawing.Size(128, 17);
            this.rdoFollowup.TabIndex = 2;
            this.rdoFollowup.Text = "Followup Treatment";
            this.rdoFollowup.UseVisualStyleBackColor = true;
            this.rdoFollowup.CheckedChanged += new System.EventHandler(this.rdoFollowup_CheckedChanged);
            // 
            // drpTreatmentType
            // 
            this.drpTreatmentType.FormattingEnabled = true;
            this.drpTreatmentType.Location = new System.Drawing.Point(163, 32);
            this.drpTreatmentType.Name = "drpTreatmentType";
            this.drpTreatmentType.Size = new System.Drawing.Size(186, 21);
            this.drpTreatmentType.TabIndex = 3;
            this.drpTreatmentType.SelectedIndexChanged += new System.EventHandler(this.drpTreatmentType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Treatment Type:";
            // 
            // ManageTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 247);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.drpTreatmentType);
            this.Controls.Add(this.rdoFollowup);
            this.Controls.Add(this.rdoInitial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSetDefault);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.grdTemplate);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageTemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Templates";
            this.Load += new System.EventHandler(this.ManageTemplateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdTemplate;
        private System.Windows.Forms.Button btnSetDefault;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoInitial;
        private System.Windows.Forms.RadioButton rdoFollowup;
        private System.Windows.Forms.ComboBox drpTreatmentType;
        private System.Windows.Forms.Label label2;
    }
}