using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using EHD.Constant;
using NHibernate;
using NHibernate.Criterion;

namespace EHD.Admin {
    public partial class EditOrganizationForm : Form {

        public Organization EditingOrganization { get; set; }

        public EditOrganizationForm() {
            InitializeComponent();
        }

        public EditOrganizationForm(Organization organization)
            : this() {
            EditingOrganization = organization;
        }

        private void EditOrganizationForm_Load(object sender, EventArgs e) {
            loadOrganization();
        }

        private void loadOrganization() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingOrganization == null || EditingOrganization.Id == default(int)) {
                        EditingOrganization = new Organization();
                        EditingOrganization.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingOrganization.CreatedBy = Program.LogonUser.DisplayName;
                        EditingOrganization.CreatedTime = DateTime.Now;
                    } else {
                        EditingOrganization = session.Merge<Organization>(EditingOrganization);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Organization. " + ex.Message);
                    Program.log.Error("Failed to load Organization.", ex);
                }
            }

            populateOrganizationDetail(EditingOrganization);
        }

        private void populateOrganizationDetail(Organization organization) {
            if(organization != null) {
                txtOrganizationName.Text = organization.OrganizationName;
            }
        }

        private void updateOrganizationInput(Organization organization) {
            organization.OrganizationName = txtOrganizationName.Text.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingOrganization == null || EditingOrganization.Id == default(int)) {
                        EditingOrganization = new Organization();
                        EditingOrganization.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingOrganization.CreatedBy = Program.LogonUser.DisplayName;
                        EditingOrganization.CreatedTime = DateTime.Now;
                    } else {
                        EditingOrganization = session.QueryOver<Organization>()
                            .Where(x => x.Id == EditingOrganization.Id)
                            .SingleOrDefault();

                        EditingOrganization.UpdatedTime = DateTime.Now;
                        EditingOrganization.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateOrganizationInput(EditingOrganization);

                    if(!EditingOrganization.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingOrganization);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save Organization. " + ex.Message);
                    Program.log.Error("Failed to save Organization.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

    }
}
