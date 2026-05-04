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
    public partial class EditUserTitleForm : Form {

        public UserTitle EditingUserTitle { get; set; }

        public EditUserTitleForm() {
            InitializeComponent();
        }

        public EditUserTitleForm(UserTitle title)
            : this() {
            EditingUserTitle = title;
        }

        private void EditUserTitleForm_Load(object sender, EventArgs e) {
            loadUserTitle();
        }

        private void loadUserTitle() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingUserTitle == null || EditingUserTitle.Id == default(int)) {
                        EditingUserTitle = new UserTitle();
                        EditingUserTitle.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingUserTitle.CreatedBy = Program.LogonUser.DisplayName;
                        EditingUserTitle.CreatedTime = DateTime.Now;
                    } else {
                        EditingUserTitle = session.Merge<UserTitle>(EditingUserTitle);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load UserTitle. " + ex.Message);
                    Program.log.Error("Failed to load UserTitle.", ex);
                }
            }

            populateUserTitleDetail(EditingUserTitle);
        }

        private void populateUserTitleDetail(UserTitle title) {
            if(title != null) {
                txtUserTitleName.Text = title.Name;
            }
        }

        private void updateUserTitleInput(UserTitle title) {
            title.Name = txtUserTitleName.Text.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingUserTitle == null || EditingUserTitle.Id == default(int)) {
                        EditingUserTitle = new UserTitle();
                        EditingUserTitle.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingUserTitle.CreatedBy = Program.LogonUser.DisplayName;
                        EditingUserTitle.CreatedTime = DateTime.Now;
                    } else {
                        EditingUserTitle = session.QueryOver<UserTitle>()
                            .Where(x => x.Id == EditingUserTitle.Id)
                            .SingleOrDefault();

                        EditingUserTitle.UpdatedTime = DateTime.Now;
                        EditingUserTitle.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateUserTitleInput(EditingUserTitle);

                    if(!EditingUserTitle.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingUserTitle);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save UserTitle. " + ex.Message);
                    Program.log.Error("Failed to save UserTitle.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
