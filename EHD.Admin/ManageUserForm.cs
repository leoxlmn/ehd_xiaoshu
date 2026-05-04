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
    public partial class ManageUserForm : Form {

        public ManageUserForm() {
            InitializeComponent();
        }

        private void ManageUserForm_Load(object sender, EventArgs e) {
            populateUserList();
        }

        private void populateUserList() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    var users = session.QueryOver<User>()
                        .OrderBy(x => x.FirstName).Asc
                        .OrderBy(x => x.LastName).Asc
                        .List();
                    grdUser.DataSource = users;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateUserList error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditUserForm()).ShowDialog(this)) {
                populateUserList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if(grdUser.SelectedRows.Count > 0) {

                User selectedUser = grdUser.SelectedRows[0].DataBoundItem as User;

                if(DialogResult.OK == (new EditUserForm(selectedUser)).ShowDialog(this)) {
                    populateUserList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(grdUser.SelectedRows.Count > 0) {

                User selectedUser = grdUser.SelectedRows[0].DataBoundItem as User;

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete User [" + selectedUser.DisplayName + "]?",
                    "Delete User",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            User user = session.Get<User>(selectedUser.Id);
                            session.Delete(user);

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete User. " + ex.Message);
                            Program.log.Error("Failed to delete User.", ex);
                        }
                    }

                    //Refresh User List
                    populateUserList();
                }
            }
        }

        private void grdUser_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                User selectedUser = grdUser.Rows[e.RowIndex].DataBoundItem as User;

                if(DialogResult.OK == (new EditUserForm(selectedUser)).ShowDialog(this)) {
                    populateUserList();
                }
            }
        }

        private void grdUser_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if(grdUser.Columns != null) {
                if(grdUser.Columns.Contains("IsTestData")) grdUser.Columns["IsTestData"].Visible = false;
                if(grdUser.Columns.Contains("CreatedBy")) grdUser.Columns["CreatedBy"].Visible = false;
                if(grdUser.Columns.Contains("UpdatedBy")) grdUser.Columns["UpdatedBy"].Visible = false;
                if(grdUser.Columns.Contains("CreatedTime")) grdUser.Columns["CreatedTime"].Visible = false;
                if(grdUser.Columns.Contains("UpdatedTime")) grdUser.Columns["UpdatedTime"].Visible = false;
                if(grdUser.Columns.Contains("Version")) grdUser.Columns["Version"].Visible = false;
            }
        }
    }
}
