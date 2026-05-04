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
    public partial class ManageOrganizationForm : Form {

        public ManageOrganizationForm() {
            InitializeComponent();
        }

        private void ManageOrganizationForm_Load(object sender, EventArgs e) {
            populateOrganizationList();
        }

        private void populateOrganizationList() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    var orgs = session.QueryOver<Organization>()
                        .OrderBy(x => x.OrganizationName).Asc
                        .List();
                    grdOrganization.DataSource = orgs;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateOrganizationList error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditOrganizationForm()).ShowDialog(this)) {
                populateOrganizationList();
            }
        }

        private void btnEditOrganization_Click(object sender, EventArgs e) {
            if(grdOrganization.SelectedRows.Count > 0) {

                Organization selectedOrganization = grdOrganization.SelectedRows[0].DataBoundItem as Organization;

                if(DialogResult.OK == (new EditOrganizationForm(selectedOrganization)).ShowDialog(this)) {
                    populateOrganizationList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(grdOrganization.SelectedRows.Count > 0) {

                Organization selectedOrganization = grdOrganization.SelectedRows[0].DataBoundItem as Organization;

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete Organization [" + selectedOrganization.DisplayName + "]?",
                    "Delete Insurer",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            Organization org = session.Get<Organization>(selectedOrganization.Id);
                            session.Delete(org);

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete Organization. " + ex.Message);
                            Program.log.Error("Failed to delete Organization.", ex);
                        }
                    }

                    //Refresh Organization List
                    populateOrganizationList();
                }
            }
        }

        private void grdOrganization_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                Organization selectedOrganization = grdOrganization.Rows[e.RowIndex].DataBoundItem as Organization;

                if(DialogResult.OK == (new EditOrganizationForm(selectedOrganization)).ShowDialog(this)) {
                    populateOrganizationList();
                }
            }
        }

        private void grdOrganization_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if(grdOrganization.Columns != null) {
                if(grdOrganization.Columns.Contains("IsTestData")) grdOrganization.Columns["IsTestData"].Visible = false;
                if(grdOrganization.Columns.Contains("CreatedBy")) grdOrganization.Columns["CreatedBy"].Visible = false;
                if(grdOrganization.Columns.Contains("UpdatedBy")) grdOrganization.Columns["UpdatedBy"].Visible = false;
                if(grdOrganization.Columns.Contains("CreatedTime")) grdOrganization.Columns["CreatedTime"].Visible = false;
                if(grdOrganization.Columns.Contains("UpdatedTime")) grdOrganization.Columns["UpdatedTime"].Visible = false;
                if(grdOrganization.Columns.Contains("Version")) grdOrganization.Columns["Version"].Visible = false;
            }
        }
    }
}
