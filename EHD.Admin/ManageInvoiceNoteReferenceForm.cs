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
    public partial class ManageInvoiceNoteReferenceForm : Form {

        public ManageInvoiceNoteReferenceForm() {
            InitializeComponent();
        }

        private void ManageInvoiceNoteReferenceForm_Load(object sender, EventArgs e) {
            populateInvoiceNoteReferenceList();
        }

        private void populateInvoiceNoteReferenceList() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    var notes = session.QueryOver<InvoiceNoteReference>()
                        .OrderBy(x => x.Text).Asc
                        .List();
                    grdInvoiceNote.DataSource = notes;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateInvoiceNoteReferenceList error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditInvoiceNoteForm()).ShowDialog(this)) {
                populateInvoiceNoteReferenceList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if(grdInvoiceNote.SelectedRows.Count > 0) {

                InvoiceNoteReference selectedInvoiceNoteReference = grdInvoiceNote.SelectedRows[0].DataBoundItem as InvoiceNoteReference;

                if(DialogResult.OK == (new EditInvoiceNoteForm(selectedInvoiceNoteReference)).ShowDialog(this)) {
                    populateInvoiceNoteReferenceList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(grdInvoiceNote.SelectedRows.Count > 0) {

                InvoiceNoteReference selectedInvoiceNoteReference = grdInvoiceNote.SelectedRows[0].DataBoundItem as InvoiceNoteReference;

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete InvoiceNoteReference [" + selectedInvoiceNoteReference.DisplayName + "]?",
                    "Delete InvoiceNoteReference",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            InvoiceNoteReference note = session.Get<InvoiceNoteReference>(selectedInvoiceNoteReference.Id);
                            session.Delete(note);

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete InvoiceNoteReference. " + ex.Message);
                            Program.log.Error("Failed to delete InvoiceNoteReference.", ex);
                        }
                    }

                    //Refresh InvoiceNoteReference List
                    populateInvoiceNoteReferenceList();
                }
            }
        }

        private void grdInvoiceNote_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                InvoiceNoteReference selectedInvoiceNoteReference = grdInvoiceNote.Rows[e.RowIndex].DataBoundItem as InvoiceNoteReference;

                if(DialogResult.OK == (new EditInvoiceNoteForm(selectedInvoiceNoteReference)).ShowDialog(this)) {
                    populateInvoiceNoteReferenceList();
                }
            }
        }

        private void grdInvoiceNote_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if(grdInvoiceNote.Columns != null) {
                if(grdInvoiceNote.Columns.Contains("IsTestData")) grdInvoiceNote.Columns["IsTestData"].Visible = false;
                if(grdInvoiceNote.Columns.Contains("CreatedBy")) grdInvoiceNote.Columns["CreatedBy"].Visible = false;
                if(grdInvoiceNote.Columns.Contains("UpdatedBy")) grdInvoiceNote.Columns["UpdatedBy"].Visible = false;
                if(grdInvoiceNote.Columns.Contains("CreatedTime")) grdInvoiceNote.Columns["CreatedTime"].Visible = false;
                if(grdInvoiceNote.Columns.Contains("UpdatedTime")) grdInvoiceNote.Columns["UpdatedTime"].Visible = false;
                if(grdInvoiceNote.Columns.Contains("Version")) grdInvoiceNote.Columns["Version"].Visible = false;
            }
        }
    }
}
