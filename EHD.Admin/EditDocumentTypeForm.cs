using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Constant;
using NHibernate;

namespace EHD.Admin {
    public partial class EditDocumentTypeForm : Form {

        public DocumentType EditingDocumentType { get; set; }

        public EditDocumentTypeForm() {
            InitializeComponent();
        }

        public EditDocumentTypeForm(DocumentType type) : this() {
            EditingDocumentType = type;
        }
        private void EditDocumentTypeForm_Load(object sender, EventArgs e) {
            loadDocumentType();
        }

        private void loadDocumentType() {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    if (EditingDocumentType == null || EditingDocumentType.Id == default(int)) {
                        EditingDocumentType = new DocumentType();
                        EditingDocumentType.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingDocumentType.CreatedBy = Program.LogonUser.DisplayName;
                        EditingDocumentType.CreatedTime = DateTime.Now;
                    } else {
                        EditingDocumentType = session.Merge<DocumentType>(EditingDocumentType);
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Document Type. " + ex.Message);
                    Program.log.Error("Failed to load Document Type.", ex);
                }
            }

            populateDocumentTypeDetail(EditingDocumentType);
        }

        private void populateDocumentTypeDetail(DocumentType type) {
            if (type != null) {
                txtName.Text = type.DocumentTypeName;
                txtNote.Text = type.Description;
            }
        }

        private void updateDocumentTypeInput(DocumentType type) {
            type.DocumentTypeName = txtName.Text.Trim();
            type.Description = txtNote.Text.Trim();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    if (EditingDocumentType == null || EditingDocumentType.Id == default(int)) {
                        EditingDocumentType = new DocumentType();
                        EditingDocumentType.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingDocumentType.CreatedBy = Program.LogonUser.DisplayName;
                        EditingDocumentType.CreatedTime = DateTime.Now;
                    } else {
                        EditingDocumentType = session.QueryOver<DocumentType>()
                            .Where(x => x.Id == EditingDocumentType.Id)
                            .SingleOrDefault();

                        EditingDocumentType.UpdatedTime = DateTime.Now;
                        EditingDocumentType.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateDocumentTypeInput(EditingDocumentType);

                    if (!EditingDocumentType.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingDocumentType);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save Document Type. " + ex.Message);
                    Program.log.Error("Failed to save Document Type.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
