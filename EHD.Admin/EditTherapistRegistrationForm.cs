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
using EHD.FormUtitlity;

namespace EHD.Admin {
    public partial class EditTherapistRegistrationForm : Form {

        private User _therapist = null;
        private TherapistOrganizationRegistration _registration = null;

        public EditTherapistRegistrationForm(User Therapist) {
            InitializeComponent();

            _therapist = Therapist;
        }

        public EditTherapistRegistrationForm(TherapistOrganizationRegistration Registration) {
            InitializeComponent();

            _registration = Registration;
        }


        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void EditTherapistRegistrationForm_Load(object sender, EventArgs e) {
            
            populateDropDownLists();

            populateForm();
        }

        private void populateForm() {
            if(_registration != null) {//Editing existing registration
                txtTherapist.Text = _registration.Therapist.DisplayName;
                txtRegistrationNumber.Text = _registration.RegistrationNumber;
                setOrganization(_registration.Organization);
            } else if(_therapist != null) {//Add new registration for therapist
                txtTherapist.Text = _therapist.DisplayName;
            }
        }

        private void populateDropDownLists() {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    IList<Organization> lstOrg = session.QueryOver<Organization>()
                        .OrderBy(x => x.OrganizationName).Asc
                        .List<Organization>();

                    //Add an empty Organization to the dropdown
                    lstOrg.Insert(0, new Organization());
                    drpOrganization.DataSource = lstOrg;
                    drpOrganization.ValueMember = "Id";
                    drpOrganization.DisplayMember = "DisplayName";

                    tr.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load Organization list. " + ex.Message);
                Program.log.Error("Failed to load Organization list.", ex);
            }
        }

        private void setOrganization(Organization org) {
            if(org == null) return;

            drpOrganization.SelectedValue = org.Id;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    TherapistOrganizationRegistration reg = null;

                    if(_registration == null && _therapist != null) {
                        //Always create a new group for new registrations
                        TherapistOrganizationRegistrationGroup newGroup = new TherapistOrganizationRegistrationGroup();
                        newGroup.Therapist = _therapist;
                        newGroup.IsTestData = Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer;
                        newGroup.CreatedTime = DateTime.Now;
                        newGroup.CreatedBy = Program.LogonUser.DisplayName;

                        session.Save(newGroup);

                        reg = new TherapistOrganizationRegistration();
                        reg.RegistrationGroup = newGroup;
                        reg.Therapist = _therapist;
                        reg.IsTestData = Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer;
                        reg.CreatedBy = Program.LogonUser.DisplayName;
                        reg.CreatedTime = DateTime.Now;
                    } else if(_registration != null) {
                        reg = session.Get<TherapistOrganizationRegistration>(_registration.Id);
                        reg.UpdatedTime = DateTime.Now;
                        reg.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    //update with dropdown selection
                    if(drpOrganization.SelectedValue != null)
                        reg.Organization =
                            session.Get<Organization>(int.Parse(drpOrganization.SelectedValue.ToString()));

                    //Update user input
                    reg.RegistrationNumber = txtRegistrationNumber.Text.Trim();

                    if(reg.HasMandatoryValues()) {
                        session.Save(reg);

                        tr.Commit();
                        MessageBox.Show(this, "Information Updated.");
                        DialogResult = DialogResult.OK;
                        this.Close();
                    } else {
                        MessageBox.Show(this,
                            Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to save Therapist Registration. " + ex.Message);
                Program.log.Error("Failed to save Therapist Registration.", ex);
            }
        }

        private void lnkManageOrganization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            (new ManageOrganizationForm()).ShowDialog(this);
            populateDropDownLists();
        }

    }
}
