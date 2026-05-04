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
    public partial class ManageTherapyTypesForm : Form {

        public ManageTherapyTypesForm() {
            InitializeComponent();
        }

        private void ManageTherapyTypesForm_Load(object sender, EventArgs e) {
            populateTherapyTypeList();
        }

        private void populateTherapyTypeList() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    var therapys = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .List();
                    grdTherapyType.DataSource = therapys;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateTherapyTypeList error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditTherapyTypeForm()).ShowDialog(this)) {
                populateTherapyTypeList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if(grdTherapyType.SelectedRows.Count > 0) {

                TherapyType selectedTherapyType = grdTherapyType.SelectedRows[0].DataBoundItem as TherapyType;

                if(DialogResult.OK == (new EditTherapyTypeForm(selectedTherapyType)).ShowDialog(this)) {
                    populateTherapyTypeList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(grdTherapyType.SelectedRows.Count > 0) {

                TherapyType selectedTherapyType = grdTherapyType.SelectedRows[0].DataBoundItem as TherapyType;

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete TherapyType [" + selectedTherapyType.DisplayName + "]?",
                    "Delete TherapyType",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            TherapyType therapy = session.Get<TherapyType>(selectedTherapyType.Id);
                            session.Delete(therapy);

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete TherapyType. " + ex.Message);
                            Program.log.Error("Failed to delete TherapyType.", ex);
                        }
                    }

                    //Refresh TherapyType List
                    populateTherapyTypeList();
                }
            }
        }

        private void grdTherapyType_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                TherapyType selectedTherapyType = grdTherapyType.Rows[e.RowIndex].DataBoundItem as TherapyType;

                if(DialogResult.OK == (new EditTherapyTypeForm(selectedTherapyType)).ShowDialog(this)) {
                    populateTherapyTypeList();
                }
            }
        }

        private void grdTherapyType_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if(grdTherapyType.Columns != null) {
                if(grdTherapyType.Columns.Contains("IsTestData")) grdTherapyType.Columns["IsTestData"].Visible = false;
                if(grdTherapyType.Columns.Contains("CreatedBy")) grdTherapyType.Columns["CreatedBy"].Visible = false;
                if(grdTherapyType.Columns.Contains("UpdatedBy")) grdTherapyType.Columns["UpdatedBy"].Visible = false;
                if(grdTherapyType.Columns.Contains("CreatedTime")) grdTherapyType.Columns["CreatedTime"].Visible = false;
                if(grdTherapyType.Columns.Contains("UpdatedTime")) grdTherapyType.Columns["UpdatedTime"].Visible = false;
                if(grdTherapyType.Columns.Contains("Version")) grdTherapyType.Columns["Version"].Visible = false;
            }
        }
    }
}
