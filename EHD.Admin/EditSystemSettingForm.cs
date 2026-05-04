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
    public partial class EditSystemSettingForm : Form {

        public SystemSetting EditingSystemSetting { get; set; }

        public EditSystemSettingForm() {
            InitializeComponent();
        }

        public EditSystemSettingForm(SystemSetting setting)
            : this() {
            EditingSystemSetting = setting;
        }

        private void EditSystemSettingForm_Load(object sender, EventArgs e) {
            loadSystemSetting();
        }

        private void loadSystemSetting() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingSystemSetting == null || EditingSystemSetting.Id == default(int)) {
                        EditingSystemSetting = new SystemSetting();
                        EditingSystemSetting.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingSystemSetting.CreatedBy = Program.LogonUser.DisplayName;
                        EditingSystemSetting.CreatedTime = DateTime.Now;
                    } else {
                        EditingSystemSetting = session.Merge<SystemSetting>(EditingSystemSetting);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load SystemSetting. " + ex.Message);
                    Program.log.Error("Failed to load SystemSetting.", ex);
                }
            }

            populateSystemSettingDetail(EditingSystemSetting);
        }

        private void populateSystemSettingDetail(SystemSetting setting) {
            if(setting != null) {
                txtName.Text = setting.Name;
                txtValue.Text = setting.Value;
            }
        }

        private void updateSystemSettingInput(SystemSetting setting) {
            setting.Name = txtName.Text.Trim();
            setting.Value = txtValue.Text.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingSystemSetting == null || EditingSystemSetting.Id == default(int)) {
                        EditingSystemSetting = new SystemSetting();
                        EditingSystemSetting.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingSystemSetting.CreatedBy = Program.LogonUser.DisplayName;
                        EditingSystemSetting.CreatedTime = DateTime.Now;
                    } else {
                        EditingSystemSetting = session.QueryOver<SystemSetting>()
                            .Where(x => x.Id == EditingSystemSetting.Id)
                            .SingleOrDefault();

                        EditingSystemSetting.UpdatedTime = DateTime.Now;
                        EditingSystemSetting.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateSystemSettingInput(EditingSystemSetting);

                    if(!EditingSystemSetting.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingSystemSetting);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save SystemSetting. " + ex.Message);
                    Program.log.Error("Failed to save SystemSetting.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
