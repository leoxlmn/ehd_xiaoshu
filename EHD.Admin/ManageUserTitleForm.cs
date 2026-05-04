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
    public partial class ManageUserTitleForm : Form {

        public ManageUserTitleForm() {
            InitializeComponent();
        }

        private void ManageUserTitleForm_Load(object sender, EventArgs e) {
            populateUserTitleList();
        }

        private void populateUserTitleList() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    var titles = session.QueryOver<UserTitle>()
                        .OrderBy(x => x.Name).Asc
                        .List();
                    grdUserTitle.DataSource = titles;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateUserTitleList error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditUserTitleForm()).ShowDialog(this)) {
                populateUserTitleList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if(grdUserTitle.SelectedRows.Count > 0) {

                UserTitle selectedUserTitle = grdUserTitle.SelectedRows[0].DataBoundItem as UserTitle;

                if(DialogResult.OK == (new EditUserTitleForm(selectedUserTitle)).ShowDialog(this)) {
                    populateUserTitleList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(grdUserTitle.SelectedRows.Count > 0) {

                UserTitle selectedUserTitle = grdUserTitle.SelectedRows[0].DataBoundItem as UserTitle;

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete UserTitle [" + selectedUserTitle.DisplayName + "]?",
                    "Delete UserTitle",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            UserTitle title = session.Get<UserTitle>(selectedUserTitle.Id);
                            session.Delete(title);

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete UserTitle. " + ex.Message);
                            Program.log.Error("Failed to delete UserTitle.", ex);
                        }
                    }

                    //Refresh UserTitle List
                    populateUserTitleList();
                }
            }
        }

        private void grdUserTitle_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                UserTitle selectedUserTitle = grdUserTitle.Rows[e.RowIndex].DataBoundItem as UserTitle;

                if(DialogResult.OK == (new EditUserTitleForm(selectedUserTitle)).ShowDialog(this)) {
                    populateUserTitleList();
                }
            }
        }

        private void grdUserTitle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if(grdUserTitle.Columns != null) {
                if(grdUserTitle.Columns.Contains("IsTestData")) grdUserTitle.Columns["IsTestData"].Visible = false;
                if(grdUserTitle.Columns.Contains("CreatedBy")) grdUserTitle.Columns["CreatedBy"].Visible = false;
                if(grdUserTitle.Columns.Contains("UpdatedBy")) grdUserTitle.Columns["UpdatedBy"].Visible = false;
                if(grdUserTitle.Columns.Contains("CreatedTime")) grdUserTitle.Columns["CreatedTime"].Visible = false;
                if(grdUserTitle.Columns.Contains("UpdatedTime")) grdUserTitle.Columns["UpdatedTime"].Visible = false;
                if(grdUserTitle.Columns.Contains("Version")) grdUserTitle.Columns["Version"].Visible = false;
            }
        }
    }
}
