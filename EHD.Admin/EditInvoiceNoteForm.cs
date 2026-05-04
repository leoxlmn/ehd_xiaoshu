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
    public partial class EditInvoiceNoteForm : Form {

        public InvoiceNoteReference EditingInvoiceNoteReference { get; set; }

        public EditInvoiceNoteForm() {
            InitializeComponent();
        }

        public EditInvoiceNoteForm(InvoiceNoteReference note)
            : this() {
            EditingInvoiceNoteReference = note;
        }

        private void EditInvoiceNoteForm_Load(object sender, EventArgs e) {
            loadInvoiceNoteReference();
        }

        private void loadInvoiceNoteReference() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingInvoiceNoteReference == null || EditingInvoiceNoteReference.Id == default(int)) {
                        EditingInvoiceNoteReference = new InvoiceNoteReference();
                        EditingInvoiceNoteReference.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingInvoiceNoteReference.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInvoiceNoteReference.CreatedTime = DateTime.Now;
                    } else {
                        EditingInvoiceNoteReference = session.Merge<InvoiceNoteReference>(EditingInvoiceNoteReference);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load InvoiceNoteReference. " + ex.Message);
                    Program.log.Error("Failed to load InvoiceNoteReference.", ex);
                }
            }

            populateInvoiceNoteReferenceDetail(EditingInvoiceNoteReference);
        }

        private void populateInvoiceNoteReferenceDetail(InvoiceNoteReference note) {
            if(note != null) {
                txtInvoiceNote.Text = note.Text;
            }
        }

        private void updateInvoiceNoteReferenceInput(InvoiceNoteReference note) {
            note.Text = txtInvoiceNote.Text.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingInvoiceNoteReference == null || EditingInvoiceNoteReference.Id == default(int)) {
                        EditingInvoiceNoteReference = new InvoiceNoteReference();
                        EditingInvoiceNoteReference.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingInvoiceNoteReference.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInvoiceNoteReference.CreatedTime = DateTime.Now;
                    } else {
                        EditingInvoiceNoteReference = session.QueryOver<InvoiceNoteReference>()
                            .Where(x => x.Id == EditingInvoiceNoteReference.Id)
                            .SingleOrDefault();

                        EditingInvoiceNoteReference.UpdatedTime = DateTime.Now;
                        EditingInvoiceNoteReference.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateInvoiceNoteReferenceInput(EditingInvoiceNoteReference);

                    if(!EditingInvoiceNoteReference.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingInvoiceNoteReference);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save InvoiceNoteReference. " + ex.Message);
                    Program.log.Error("Failed to save InvoiceNoteReference.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
