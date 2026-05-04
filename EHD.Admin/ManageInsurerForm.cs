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
    public partial class ManageInsurerForm : Form {

        public ManageInsurerForm() {
            InitializeComponent();
        }

        private void ManageInsurerForm_Load(object sender, EventArgs e) {
            populateInsurerList();
        }

        private void populateInsurerList() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    var insurers = session.QueryOver<Insurer>()
                        .OrderBy(x => x.InsurerName).Asc
                        .List();
                    grdInsurer.DataSource = insurers;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateInsurerList error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditInsurerForm()).ShowDialog(this)) {
                populateInsurerList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if(grdInsurer.SelectedRows.Count > 0) {

                Insurer selectedInsurer = grdInsurer.SelectedRows[0].DataBoundItem as Insurer;

                if(DialogResult.OK == (new EditInsurerForm(selectedInsurer)).ShowDialog(this)) {
                    populateInsurerList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(grdInsurer.SelectedRows.Count > 0) {

                Insurer selectedInsurer = grdInsurer.SelectedRows[0].DataBoundItem as Insurer;
                
                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete Insurer [" + selectedInsurer.DisplayName + "]?",
                    "Delete Insurer",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            Insurer insurer = session.Get<Insurer>(selectedInsurer.Id);
                            session.Delete(insurer);

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete Insurer. " + ex.Message);
                            Program.log.Error("Failed to delete Insurer.", ex);
                        }
                    }

                    //Refresh Insurer List
                    populateInsurerList();
                }
            }
        }

        private void grdInsurer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                Insurer selectedInsurer = grdInsurer.Rows[e.RowIndex].DataBoundItem as Insurer;

                if(DialogResult.OK == (new EditInsurerForm(selectedInsurer)).ShowDialog(this)) {
                    populateInsurerList();
                }
            }
        }

        private void grdInsurer_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if(grdInsurer.Columns != null) {
                if(grdInsurer.Columns.Contains("IsTestData")) grdInsurer.Columns["IsTestData"].Visible = false;
                if(grdInsurer.Columns.Contains("CreatedBy")) grdInsurer.Columns["CreatedBy"].Visible = false;
                if(grdInsurer.Columns.Contains("UpdatedBy")) grdInsurer.Columns["UpdatedBy"].Visible = false;
                if(grdInsurer.Columns.Contains("CreatedTime")) grdInsurer.Columns["CreatedTime"].Visible = false;
                if(grdInsurer.Columns.Contains("UpdatedTime")) grdInsurer.Columns["UpdatedTime"].Visible = false;
                if(grdInsurer.Columns.Contains("Version")) grdInsurer.Columns["Version"].Visible = false;
            }
        }
    }
}
