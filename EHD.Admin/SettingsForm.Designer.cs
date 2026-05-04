namespace EHD.Admin {
    partial class SettingsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageDiagrams = new System.Windows.Forms.Button();
            this.btnManageTherapyTypes = new System.Windows.Forms.Button();
            this.btnManageInsurer = new System.Windows.Forms.Button();
            this.btnManageOrganization = new System.Windows.Forms.Button();
            this.btnManageTherapistRegistration = new System.Windows.Forms.Button();
            this.btnManageInvoiceNote = new System.Windows.Forms.Button();
            this.btnSystemSetting = new System.Windows.Forms.Button();
            this.btnManageUserTitle = new System.Windows.Forms.Button();
            this.btnTemplates = new System.Windows.Forms.Button();
            this.btnDocumentTypes = new System.Windows.Forms.Button();
            this.btnTax = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(402, 297);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 33);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageUsers.Location = new System.Drawing.Point(41, 24);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(227, 39);
            this.btnManageUsers.TabIndex = 1;
            this.btnManageUsers.Text = "Users (Admins && Therapists)";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageDiagrams
            // 
            this.btnManageDiagrams.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageDiagrams.Location = new System.Drawing.Point(41, 114);
            this.btnManageDiagrams.Name = "btnManageDiagrams";
            this.btnManageDiagrams.Size = new System.Drawing.Size(227, 39);
            this.btnManageDiagrams.TabIndex = 5;
            this.btnManageDiagrams.Text = "Diagrams";
            this.btnManageDiagrams.UseVisualStyleBackColor = true;
            this.btnManageDiagrams.Click += new System.EventHandler(this.btnManageDiagrams_Click);
            // 
            // btnManageTherapyTypes
            // 
            this.btnManageTherapyTypes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageTherapyTypes.Location = new System.Drawing.Point(41, 159);
            this.btnManageTherapyTypes.Name = "btnManageTherapyTypes";
            this.btnManageTherapyTypes.Size = new System.Drawing.Size(227, 39);
            this.btnManageTherapyTypes.TabIndex = 7;
            this.btnManageTherapyTypes.Text = "Therapy Types";
            this.btnManageTherapyTypes.UseVisualStyleBackColor = true;
            this.btnManageTherapyTypes.Click += new System.EventHandler(this.btnManageTherapyTypes_Click);
            // 
            // btnManageInsurer
            // 
            this.btnManageInsurer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageInsurer.Location = new System.Drawing.Point(274, 24);
            this.btnManageInsurer.Name = "btnManageInsurer";
            this.btnManageInsurer.Size = new System.Drawing.Size(227, 39);
            this.btnManageInsurer.TabIndex = 2;
            this.btnManageInsurer.Text = "Insurers";
            this.btnManageInsurer.UseVisualStyleBackColor = true;
            this.btnManageInsurer.Click += new System.EventHandler(this.btnManageInsurer_Click);
            // 
            // btnManageOrganization
            // 
            this.btnManageOrganization.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageOrganization.Location = new System.Drawing.Point(274, 69);
            this.btnManageOrganization.Name = "btnManageOrganization";
            this.btnManageOrganization.Size = new System.Drawing.Size(227, 39);
            this.btnManageOrganization.TabIndex = 4;
            this.btnManageOrganization.Text = "Organizations";
            this.btnManageOrganization.UseVisualStyleBackColor = true;
            this.btnManageOrganization.Click += new System.EventHandler(this.btnManageOrganization_Click);
            // 
            // btnManageTherapistRegistration
            // 
            this.btnManageTherapistRegistration.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageTherapistRegistration.Location = new System.Drawing.Point(274, 114);
            this.btnManageTherapistRegistration.Name = "btnManageTherapistRegistration";
            this.btnManageTherapistRegistration.Size = new System.Drawing.Size(227, 39);
            this.btnManageTherapistRegistration.TabIndex = 6;
            this.btnManageTherapistRegistration.Text = "Therapist Registrations";
            this.btnManageTherapistRegistration.UseVisualStyleBackColor = true;
            this.btnManageTherapistRegistration.Click += new System.EventHandler(this.btnManageTherapistRegistration_Click);
            // 
            // btnManageInvoiceNote
            // 
            this.btnManageInvoiceNote.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageInvoiceNote.Location = new System.Drawing.Point(274, 159);
            this.btnManageInvoiceNote.Name = "btnManageInvoiceNote";
            this.btnManageInvoiceNote.Size = new System.Drawing.Size(227, 39);
            this.btnManageInvoiceNote.TabIndex = 8;
            this.btnManageInvoiceNote.Text = "Invote Notes";
            this.btnManageInvoiceNote.UseVisualStyleBackColor = true;
            this.btnManageInvoiceNote.Click += new System.EventHandler(this.btnManageInvoiceNote_Click);
            // 
            // btnSystemSetting
            // 
            this.btnSystemSetting.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSystemSetting.Location = new System.Drawing.Point(274, 249);
            this.btnSystemSetting.Name = "btnSystemSetting";
            this.btnSystemSetting.Size = new System.Drawing.Size(227, 39);
            this.btnSystemSetting.TabIndex = 11;
            this.btnSystemSetting.Text = "System Settings";
            this.btnSystemSetting.UseVisualStyleBackColor = true;
            this.btnSystemSetting.Click += new System.EventHandler(this.btnSystemSetting_Click);
            // 
            // btnManageUserTitle
            // 
            this.btnManageUserTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageUserTitle.Location = new System.Drawing.Point(41, 69);
            this.btnManageUserTitle.Name = "btnManageUserTitle";
            this.btnManageUserTitle.Size = new System.Drawing.Size(227, 39);
            this.btnManageUserTitle.TabIndex = 3;
            this.btnManageUserTitle.Text = "User Titles";
            this.btnManageUserTitle.UseVisualStyleBackColor = true;
            this.btnManageUserTitle.Click += new System.EventHandler(this.btnManageUserTitle_Click);
            // 
            // btnTemplates
            // 
            this.btnTemplates.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTemplates.Location = new System.Drawing.Point(41, 204);
            this.btnTemplates.Name = "btnTemplates";
            this.btnTemplates.Size = new System.Drawing.Size(227, 39);
            this.btnTemplates.TabIndex = 9;
            this.btnTemplates.Text = "Treatment Note Templates";
            this.btnTemplates.UseVisualStyleBackColor = true;
            this.btnTemplates.Click += new System.EventHandler(this.btnTemplates_Click);
            // 
            // btnDocumentTypes
            // 
            this.btnDocumentTypes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDocumentTypes.Location = new System.Drawing.Point(274, 204);
            this.btnDocumentTypes.Name = "btnDocumentTypes";
            this.btnDocumentTypes.Size = new System.Drawing.Size(227, 39);
            this.btnDocumentTypes.TabIndex = 10;
            this.btnDocumentTypes.Text = "Document Types";
            this.btnDocumentTypes.UseVisualStyleBackColor = true;
            this.btnDocumentTypes.Click += new System.EventHandler(this.btnDocumentTypes_Click);
            // 
            // btnTax
            // 
            this.btnTax.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTax.Location = new System.Drawing.Point(41, 249);
            this.btnTax.Name = "btnTax";
            this.btnTax.Size = new System.Drawing.Size(227, 39);
            this.btnTax.TabIndex = 13;
            this.btnTax.Text = "Taxes";
            this.btnTax.UseVisualStyleBackColor = true;
            this.btnTax.Click += new System.EventHandler(this.btnTax_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(542, 342);
            this.Controls.Add(this.btnTax);
            this.Controls.Add(this.btnDocumentTypes);
            this.Controls.Add(this.btnTemplates);
            this.Controls.Add(this.btnManageUserTitle);
            this.Controls.Add(this.btnSystemSetting);
            this.Controls.Add(this.btnManageInvoiceNote);
            this.Controls.Add(this.btnManageTherapistRegistration);
            this.Controls.Add(this.btnManageOrganization);
            this.Controls.Add(this.btnManageInsurer);
            this.Controls.Add(this.btnManageTherapyTypes);
            this.Controls.Add(this.btnManageDiagrams);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button btnManageDiagrams;
        private System.Windows.Forms.Button btnManageTherapyTypes;
        private System.Windows.Forms.Button btnManageInsurer;
        private System.Windows.Forms.Button btnManageOrganization;
        private System.Windows.Forms.Button btnManageTherapistRegistration;
        private System.Windows.Forms.Button btnManageInvoiceNote;
        private System.Windows.Forms.Button btnSystemSetting;
        private System.Windows.Forms.Button btnManageUserTitle;
        private System.Windows.Forms.Button btnTemplates;
        private System.Windows.Forms.Button btnDocumentTypes;
        private System.Windows.Forms.Button btnTax;
    }
}