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
    public partial class ManageSystemSettingForm : Form {

        public ManageSystemSettingForm() {
            InitializeComponent();
        }

        private void ManageSystemSettingForm_Load(object sender, EventArgs e) {
            populateSystemSettingList();
        }

        private void populateSystemSettingList() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    var settings = session.QueryOver<SystemSetting>()
                        .OrderBy(x => x.Name).Asc
                        .List();
                    grdSystemSetting.DataSource = settings;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateSystemSettingList error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditSystemSettingForm()).ShowDialog(this)) {
                populateSystemSettingList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if(grdSystemSetting.SelectedRows.Count > 0) {

                SystemSetting selectedSystemSetting = grdSystemSetting.SelectedRows[0].DataBoundItem as SystemSetting;

                if(DialogResult.OK == (new EditSystemSettingForm(selectedSystemSetting)).ShowDialog(this)) {
                    populateSystemSettingList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(grdSystemSetting.SelectedRows.Count > 0) {

                SystemSetting selectedSystemSetting = grdSystemSetting.SelectedRows[0].DataBoundItem as SystemSetting;

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete SystemSetting [" + selectedSystemSetting.DisplayName + "]?",
                    "Delete SystemSetting",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            SystemSetting setting = session.Get<SystemSetting>(selectedSystemSetting.Id);
                            session.Delete(setting);

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete SystemSetting. " + ex.Message);
                            Program.log.Error("Failed to delete SystemSetting.", ex);
                        }
                    }

                    //Refresh SystemSetting List
                    populateSystemSettingList();
                }
            }
        }

        private void grdSystemSetting_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                SystemSetting selectedSystemSetting = grdSystemSetting.Rows[e.RowIndex].DataBoundItem as SystemSetting;

                if(DialogResult.OK == (new EditSystemSettingForm(selectedSystemSetting)).ShowDialog(this)) {
                    populateSystemSettingList();
                }
            }
        }

        private void grdSystemSetting_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if(grdSystemSetting.Columns != null) {
                if(grdSystemSetting.Columns.Contains("IsTestData")) grdSystemSetting.Columns["IsTestData"].Visible = false;
                if(grdSystemSetting.Columns.Contains("CreatedBy")) grdSystemSetting.Columns["CreatedBy"].Visible = false;
                if(grdSystemSetting.Columns.Contains("UpdatedBy")) grdSystemSetting.Columns["UpdatedBy"].Visible = false;
                if(grdSystemSetting.Columns.Contains("CreatedTime")) grdSystemSetting.Columns["CreatedTime"].Visible = false;
                if(grdSystemSetting.Columns.Contains("UpdatedTime")) grdSystemSetting.Columns["UpdatedTime"].Visible = false;
                if(grdSystemSetting.Columns.Contains("Version")) grdSystemSetting.Columns["Version"].Visible = false;
            }
        }
    }
}
