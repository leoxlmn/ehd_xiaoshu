using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnManageUsers_Click(object sender, EventArgs e) {
            (new ManageUserForm()).ShowDialog(this);
        }

        private void btnManageDiagrams_Click(object sender, EventArgs e) {
            (new ManageDiagramForm()).ShowDialog(this);
        }

        private void btnManageTherapyTypes_Click(object sender, EventArgs e) {
            (new ManageTherapyTypesForm()).ShowDialog(this);
        }

        private void btnManageInsurer_Click(object sender, EventArgs e) {
            (new ManageInsurerForm()).ShowDialog(this);
        }

        private void btnManageOrganization_Click(object sender, EventArgs e) {
            (new ManageOrganizationForm()).ShowDialog(this);
        }

        private void btnManageTherapistRegistration_Click(object sender, EventArgs e) {
            (new ManageTherapistRegistrationForm()).ShowDialog(this);
        }

        private void btnManageInvoiceNote_Click(object sender, EventArgs e) {
            (new ManageInvoiceNoteReferenceForm()).ShowDialog(this);
        }

        private void btnSystemSetting_Click(object sender, EventArgs e) {
            (new ManageSystemSettingForm()).ShowDialog(this);
        }

        private void btnManageUserTitle_Click(object sender, EventArgs e) {
            (new ManageUserTitleForm()).ShowDialog(this);
        }

        private void btnTemplates_Click(object sender, EventArgs e) {
            (new ManageTemplateForm()).ShowDialog(this);
        }

        private void btnDocumentTypes_Click(object sender, EventArgs e) {
            (new ManageDocumentTypeForm()).ShowDialog(this);
        }

        private void btnTax_Click(object sender, EventArgs e) {
            (new ManageTaxForm()).ShowDialog(this);
        }
    }
}
