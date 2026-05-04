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
    public partial class ManageTherapistRegistrationForm : Form {

        private SetGroupForm _setGroupForm = null;

        public ManageTherapistRegistrationForm() {
            InitializeComponent();
        }

        private void ManageTherapistRegistrationForm_Load(object sender, EventArgs e) {
            loadRegistration();
        }

        private void loadRegistration() {
            tvReg.BeginUpdate();
            tvReg.Nodes.Clear();

            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession()){

                    IList<User> lstTherapist = session.QueryOver<User>()
                        .Where(x => x.UserType == UserType.Therapist)
                        .OrderBy(x => x.FirstName).Asc
                        .OrderBy(x => x.LastName).Asc
                        .List<User>();

                    foreach(User therapist in lstTherapist) {
                        TreeNode therapistNode = new TreeNode(therapist.DisplayName);
                        therapistNode.Tag = therapist;
                        therapistNode.NodeFont = new Font("Verdana", 10, FontStyle.Bold);
                        therapistNode.ForeColor = Color.FromArgb(21, 72, 144);

                        IList<TherapistOrganizationRegistration> lstReg =
                            session.QueryOver<TherapistOrganizationRegistration>()
                            .List<TherapistOrganizationRegistration>()
                            .Where(r => r.Therapist.Id == therapist.Id)
                            .OrderBy(r => r.Organization)
                            .ToList<TherapistOrganizationRegistration>();

                        foreach(TherapistOrganizationRegistration reg in lstReg) {
                            TreeNode regNode = new TreeNode(formatRegistration(reg));
                            regNode.Tag = reg;

                            therapistNode.Nodes.Add(regNode);
                        }

                        tvReg.Nodes.Add(therapistNode);
                    }

                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load Therapist Registration Tree. " + ex.Message);
                Program.log.Error("Failed to load Therapist Registration Tree.", ex);
            }

            tvReg.ExpandAll();
            tvReg.EndUpdate();
        }

        private string formatRegistration(TherapistOrganizationRegistration reg) {
            return string.Format("[Group {0}] - {1} - {2}",
                reg.RegistrationGroup.Id,
                reg.Organization.DisplayName,
                reg.RegistrationNumber);
        }

        private void tvReg_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if(e.Button == System.Windows.Forms.MouseButtons.Right) {
                tvReg.SelectedNode = e.Node;
                menu.Show(sender as Control, e.Location);
            } else if(e.Button == System.Windows.Forms.MouseButtons.Left &&
                _setGroupForm != null && !_setGroupForm.IsClosed) {

                TherapistOrganizationRegistration reg = e.Node.Tag as TherapistOrganizationRegistration;
                if(reg != null) {
                    _setGroupForm.AddRegistrationToCombine(reg);
                }
            }
        }

        private void mnuAddReg_Click(object sender, EventArgs e) {
            if(tvReg.SelectedNode == null) return;

            User therapist = tvReg.SelectedNode.Tag as User;

            if(therapist == null) return;

            (new EditTherapistRegistrationForm(therapist)).ShowDialog(this);

            loadRegistration();
        }

        private void mnuEditReg_Click(object sender, EventArgs e) {
            if(tvReg.SelectedNode == null) return;

            TherapistOrganizationRegistration reg = tvReg.SelectedNode.Tag as TherapistOrganizationRegistration;

            if(reg == null) return;

            (new EditTherapistRegistrationForm(reg)).ShowDialog(this);

            loadRegistration();
        }

        private void mnuDeleteReg_Click(object sender, EventArgs e) {
            if(tvReg.SelectedNode == null) return;

            TherapistOrganizationRegistration reg = tvReg.SelectedNode.Tag as TherapistOrganizationRegistration;

            if(reg == null) return;

            if(DialogResult.OK != MessageBox.Show(this,
                "Do you want to DELETE the selected registration?\n\nPress [OK] to confirm\nPress [Cancel] to cancel.",
                "Delete Therapist Registration",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question)) {
                
                return;
            }

            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    TherapistOrganizationRegistration regToDelete = session.Get<TherapistOrganizationRegistration>(reg.Id);
                    session.Delete(regToDelete);

                    tr.Commit();

                    MessageBox.Show(this, "Therapist Registration has been deleted.");
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to delete Therapist Registration." + ex.Message);
                Program.log.Error("Failed to delete Therapist Registration.", ex);
            }

            loadRegistration();
        }

        private void ManageTherapistRegistrationForm_KeyDown(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.ControlKey) {
                MessageBox.Show(this, "Click on each registration, which you want to combine, and press [Combine] button to combine.");

                if(_setGroupForm != null) {
                    _setGroupForm.Close();
                    _setGroupForm.Dispose();
                    _setGroupForm = null;
                }

                _setGroupForm = new SetGroupForm();
                _setGroupForm.SetGroupCompeleted += new EventHandler(_setGroupForm_SetGroupCompeleted);
                _setGroupForm.Show(this);
            }
        }

        void _setGroupForm_SetGroupCompeleted(object sender, EventArgs e) {
            loadRegistration();
        }
    }
}
