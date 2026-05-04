using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using EHD.Repository;
using EHD.Model.Entity;

namespace EHD.Admin {
    public partial class ManageDocumentTypeForm : Form {
        public ManageDocumentTypeForm() {
            InitializeComponent();
        }
        private void ManageDocumentTypeForm_Load(object sender, EventArgs e) {
            populateDocumentTypeList();
        }

        private void populateDocumentTypeList() {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    var types = session.QueryOver<DocumentType>()
                        .OrderBy(x => x.DocumentTypeName).Asc
                        .Take(Program.config.MaxDocumentType)
                        .List();
                    grdDocumentType.DataSource = types;

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populate Document Type list error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if (DialogResult.OK == (new EditDocumentTypeForm()).ShowDialog(this)) {
                populateDocumentTypeList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if (grdDocumentType.SelectedRows.Count > 0) {

                DocumentType selectedDocumentType = grdDocumentType.SelectedRows[0].DataBoundItem as DocumentType;

                if (DialogResult.OK == (new EditDocumentTypeForm(selectedDocumentType)).ShowDialog(this)) {
                    populateDocumentTypeList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (grdDocumentType.SelectedRows.Count > 0) {

                DocumentType selectedDocumentType = grdDocumentType.SelectedRows[0].DataBoundItem as DocumentType;

                if (DialogResult.Yes == MessageBox.Show(this, "Do you want to delete Document Type [" + selectedDocumentType.DisplayName + "]?",
                    "Delete Document Type",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using (ITransaction tr = session.BeginTransaction()) {
                        try {
                            DocumentType type = session.Get<DocumentType>(selectedDocumentType.Id);
                            session.Delete(type);

                            tr.Commit();
                        } catch (Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete Document Type. " + ex.Message);
                            Program.log.Error("Failed to delete Document Type.", ex);
                        }
                    }

                    //Refresh DocumentType List
                    populateDocumentTypeList();
                }
            }
        }

        private void grdDocumentType_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                DocumentType selectedDocumentType = grdDocumentType.Rows[e.RowIndex].DataBoundItem as DocumentType;

                if (DialogResult.OK == (new EditDocumentTypeForm(selectedDocumentType)).ShowDialog(this)) {
                    populateDocumentTypeList();
                }
            }
        }

        private void grdDocumentType_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if (grdDocumentType.Columns != null) {
                if (grdDocumentType.Columns.Contains("IsTestData")) grdDocumentType.Columns["IsTestData"].Visible = false;
                if (grdDocumentType.Columns.Contains("CreatedBy")) grdDocumentType.Columns["CreatedBy"].Visible = false;
                if (grdDocumentType.Columns.Contains("UpdatedBy")) grdDocumentType.Columns["UpdatedBy"].Visible = false;
                if (grdDocumentType.Columns.Contains("CreatedTime")) grdDocumentType.Columns["CreatedTime"].Visible = false;
                if (grdDocumentType.Columns.Contains("UpdatedTime")) grdDocumentType.Columns["UpdatedTime"].Visible = false;
                if (grdDocumentType.Columns.Contains("Version")) grdDocumentType.Columns["Version"].Visible = false;
            }
        }
    }
}
