using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using EHD.Repository;
using EHD.Model.Entity;
using EHD.Constant;
using EHD.Service;
using Utility;

namespace EHD.Admin {
    public partial class SetGroupForm : Form {

        private IList<TherapistOrganizationRegistration> _registrations = 
            new List<TherapistOrganizationRegistration>();
        private User _therapist;
        private bool _isClosed = false;

        public event EventHandler SetGroupCompeleted = null;

        public SetGroupForm(User Therapist, IList<TherapistOrganizationRegistration> Registrations) {

            InitializeComponent();

            _therapist = Therapist;

            if(Registrations != null && Registrations.Count > 0) {
                User regTherapist = Registrations[0].Therapist;
                foreach(TherapistOrganizationRegistration reg in Registrations) {
                    if(reg.Therapist != regTherapist)
                        throw new ArgumentException("Registrations must belong to the same Therapist.");

                    _registrations.Add(reg);
                }
            }

            loadList();
        }

        public SetGroupForm() {
            InitializeComponent();

            loadList();
        }

        private void loadList() {
            lstRegistration.DataSource = _registrations;
            lstRegistration.ValueMember = "Id";
            lstRegistration.DisplayMember = "DisplayName";
        }

        public void AddRegistrationToCombine(TherapistOrganizationRegistration reg) {
            if(reg == null ||
                _registrations.Contains<TherapistOrganizationRegistration>(reg)) return;

            if(_registrations.Count > 0 &&
                _registrations[0].Therapist.Id != reg.Therapist.Id) {
                
                _registrations.Clear();
            }

            _therapist = reg.Therapist;
            _registrations.Add(reg);

            resetTherapist();
            reloadList();
        }

        public bool IsClosed {
            get {
                return _isClosed;
            }
        }

        private void resetTherapist() {
            lblTherapist.Text = _therapist.DisplayName;
        }

        private void reloadList() {
            ((CurrencyManager)lstRegistration.BindingContext[lstRegistration.DataSource]).Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if(_registrations.Count <= 0) return;

            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    //Create a new registration group
                    TherapistOrganizationRegistrationGroup newGroup = new TherapistOrganizationRegistrationGroup();
                    newGroup.Therapist = _registrations[0].Therapist;
                    newGroup.IsTestData = Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer;
                    newGroup.CreatedBy = Program.LogonUser.DisplayName;
                    newGroup.CreatedTime = DateTime.Now;

                    session.Save(newGroup);

                    foreach(TherapistOrganizationRegistration reg in _registrations) {
                        TherapistOrganizationRegistration regToUpdate = 
                            session.Get<TherapistOrganizationRegistration>(reg.Id);
                        regToUpdate.RegistrationGroup = newGroup;
                        regToUpdate.UpdatedTime = DateTime.Now;
                        regToUpdate.UpdatedBy = Program.LogonUser.DisplayName;

                        session.Save(regToUpdate);
                    }
                    tr.Commit();

                    if(SetGroupCompeleted != null) {
                        this.Invoke(SetGroupCompeleted, null, null);
                    }

                    MessageBox.Show(this, "Combined to group " + newGroup.Id.ToString());
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to combine. " + ex.Message);
                Program.log.Error("Failed to combine.", ex);
            }


            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            foreach(TherapistOrganizationRegistration reg in lstRegistration.SelectedItems) {
                if(_registrations.Contains(reg)) {
                    _registrations.Remove(reg);
                }
            }

            reloadList();
        }

        private void SetGroupForm_FormClosed(object sender, FormClosedEventArgs e) {
            _isClosed = true;
        }
    }
}
