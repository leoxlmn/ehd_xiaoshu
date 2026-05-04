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
using EHD.Model.DTO;
using NHibernate;
using EHD.Repository;
using Utility;
using NHibernate.Transform;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using EHD.Constant;

namespace EHD.Admin {

    public partial class EditDocumentsForm : Form {
        private Patient _patient;
        private bool _sortAscending = false;
        private string _sortName = "DocumentCreationTime";

        public EditDocumentsForm(Patient patient) {
            InitializeComponent();

            _patient = patient;

            if (_patient == null || _patient.Id == default(int)) {
                throw new ApplicationException("Failed to open the Documents screen as the patient does NOT exist.");
            }
        }
        public EditDocumentsForm(int patientId) {
            InitializeComponent();

            if (patientId == default(int))
                throw new ApplicationException("patientId cannot be 0.");

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    _patient = session.Get<Patient>(patientId);
                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Failed to retrieve Patient info.", ex);
                }
            }

            if (_patient == null || _patient.Id == default(int)) {
                throw new ApplicationException("Failed to open the Documents screen as the patient does NOT exist.");
            }
        }

        private void loadDocuments() {

            IList<DocumentDTO> lstDocument = null;

            DocumentDTO dtoDoc = null;
            Document aDoc= null;
            DocumentType aDocType = null;
            Patient aPatient = null;
            int totalCount = 0;

            //Create key for session access
            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);

            //Retrieve documents from database
            try {
                using(ITransaction tr = session.BeginTransaction()) {

                    var qry = session.QueryOver<Document>(() => aDoc)
                        .JoinQueryOver<DocumentType>(() => aDoc.DocumentType, () => aDocType)
                        .JoinQueryOver<Patient>(() => aDoc.Patient, () => aPatient)
                        .Where(Restrictions.Eq(Projections.Property(() => aDoc.Patient.Id), _patient.Id));

                    //Get total count
                    totalCount = (qry.Clone())
                        .Select(Projections.CountDistinct<Document>(x => x.Id))
                        .FutureValue<int>()
                        .Value;

                    //Add Order by
                    switch (_sortName) {
                        case "LoadedTime":
                            if (_sortAscending) qry.OrderBy(Projections.Property(() => aDoc.CreatedTime)).Asc();
                            else qry.OrderBy(Projections.Property(() => aDoc.CreatedTime)).Desc();
                            break;
                        case "DocumentCreationTime":
                            if (_sortAscending) qry.OrderBy(Projections.Property(() => aDoc.DocumentCreationTime)).Asc();
                            else qry.OrderBy(Projections.Property(() => aDoc.DocumentCreationTime)).Desc();
                            break;
                        case "DocumentName":
                            if (_sortAscending) qry.OrderBy(Projections.Property(() => aDoc.DocumentName)).Asc();
                            else qry.OrderBy(Projections.Property(() => aDoc.DocumentName)).Desc();
                            break;
                        case "DocumentType":
                            if (_sortAscending) qry.OrderBy(Projections.Property(() => aDocType.DocumentTypeName)).Asc();
                            else qry.OrderBy(Projections.Property(() => aDocType.DocumentTypeName)).Desc();
                            break;
                    }

                    //Get documents
                    lstDocument = qry
                        .SelectList(list => list
                            .Select(Projections.Property(() => aDoc.Id)).WithAlias(() => dtoDoc.Id)
                            .Select(Projections.Property(() => aDoc.Version)).WithAlias(() => dtoDoc.Version)
                            .Select(Projections.Property(() => aDoc.CreatedTime)).WithAlias(() => dtoDoc.LoadedTime)
                            .Select(Projections.Property(() => aDoc.Patient.Id)).WithAlias(() => dtoDoc.PatientId)
                            .Select(Projections.Property(() => aDoc.DocumentName)).WithAlias(() => dtoDoc.DocumentName)
                            .Select(Projections.Property(() => aDocType.DocumentTypeName)).WithAlias(() => dtoDoc.DocumentType)
                            .Select(Projections.Property(() => aDoc.Note)).WithAlias(() => dtoDoc.DocumentNote)
                            .Select(Projections.Property(() => aDoc.DocumentCreationTime)).WithAlias(() => dtoDoc.DocumentCreationTime)
                        )
                        .TransformUsing(Transformers.AliasToBean<DocumentDTO>())
                        .Skip((pager.Page - 1) * pager.PageSize)
                        .Take(pager.PageSize)
                        .List<DocumentDTO>();

                    tr.Commit();
                }

            } catch (Exception ex) {
                MessageBox.Show(this, ex.Message);
                Program.log.Error("Failed to retrieve Documents from database.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            //Update total number
            lblDocumentCount.Text = totalCount.ToString();

            //Update pager
            if (totalCount == 0) {
                pager.TotalPage = 1;
            } else {
                pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pager.PageSize)) + 1;
            }

            //Update datagrid with document list
            grdDocuments.DataSource = lstDocument;
        }

        private void EditDocumentsForm_Load(object sender, EventArgs e) {

            //Display patient's name
            lblPatientName.Text = _patient.DisplayName;

            //Initiate pager
            pager.Page = 1;
            pager.GoToPageEvent += new GoToPageEventHandler(navigate);

            //Display documents
            loadDocuments();
        }

        private void navigate(int page) {
            loadDocuments();
        }

        private void btnAddDocument_Click(object sender, EventArgs e) {
            EditDocumentForm docFrom = new EditDocumentForm();
            docFrom.PatientId = _patient.Id;
            if (DialogResult.OK == docFrom.ShowDialog()) {
                pager.Page = 1;
                loadDocuments();
            }
        }

        private void btnDeleteDocument_Click(object sender, EventArgs e) {
            if (grdDocuments.SelectedRows.Count <= 0) return;

            DocumentDTO docToDelete = grdDocuments.SelectedRows[0].DataBoundItem as DocumentDTO;

            if (DialogResult.Yes != MessageBox.Show(this,
                "Do you want to delete this Document [" + docToDelete.DocumentName + "]?",
                "Delete Document",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)) return;

            //Create key for session access
            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);

            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Get the Document to be deleted
                    Document theDoc = session.Get<Document>(docToDelete.Id);

                    theDoc.UpdatedBy = Program.LogonUser.DisplayName;
                    theDoc.UpdatedTime = DateTime.Now;
                    session.Delete(theDoc);
                    session.Flush();

                    tr.Commit();

                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to delete this Document. " + ex.Message);
                    Program.log.Error("Failed to delete this Document.", ex);
                }
            }

            SessionFactory.TryCloseSession(key);

            loadDocuments();
        }

        private void btnEditDocument_Click(object sender, EventArgs e) {
            if (grdDocuments.SelectedRows.Count > 0) {
                DocumentDTO doc = grdDocuments.SelectedRows[0].DataBoundItem as DocumentDTO;
                if (DialogResult.OK == (new EditDocumentForm(doc.Id)).ShowDialog(this)) {
                    loadDocuments();
                }
            }
        }

        private void grdDocuments_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Show/hide columns
            if (grdDocuments.Columns != null) {
                if (grdDocuments.Columns.Contains("Id")) grdDocuments.Columns["Id"].Visible = false;
                if (grdDocuments.Columns.Contains("Version")) grdDocuments.Columns["Version"].Visible = false;
                if (grdDocuments.Columns.Contains("PatientId")) grdDocuments.Columns["PatientId"].Visible = false;
                if (grdDocuments.Columns.Contains("PatientName")) grdDocuments.Columns["PatientName"].Visible = false;
            }

            //Set sort mode for each column
            foreach (DataGridViewColumn col in grdDocuments.Columns) {
                switch (col.DataPropertyName) {
                    case "LoadedTime":
                    case "DocumentType":
                    case "DocumentCreationTime":
                        col.SortMode = DataGridViewColumnSortMode.Programmatic;
                        break;
                    case "DocumentName":
                        col.SortMode = DataGridViewColumnSortMode.Programmatic;
                        for(int row = 0; row < grdDocuments.RowCount; row++) {
                            DataGridViewCell cell = grdDocuments[col.Index, row];
                            DataGridViewLinkCell lnkCell = new DataGridViewLinkCell();
                            lnkCell.Value = cell.Value;
                            grdDocuments[col.Index, row] = lnkCell;
                        }
                        break;
                    default:
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                        break;
                }

                if (col.DataPropertyName == _sortName) {
                    col.HeaderCell.SortGlyphDirection = _sortAscending ? SortOrder.Ascending : SortOrder.Descending;
                }
            }
        }

        private void grdDocuments_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridViewColumn col = grdDocuments.Columns[e.ColumnIndex];
            if (col.SortMode != DataGridViewColumnSortMode.Programmatic) return;

            if (col.DataPropertyName == _sortName) {
                _sortAscending = !_sortAscending;
            } else {
                _sortAscending = true;
                pager.Page = 1;
            }

            _sortName = col.DataPropertyName;

            loadDocuments();
        }

        private void grdDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e) {

            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            DataGridViewCell cell = grdDocuments[e.ColumnIndex, e.RowIndex];
            if(cell is DataGridViewLinkCell && cell.Value != null) {

                int docId = (grdDocuments.Rows[e.RowIndex].DataBoundItem as DocumentDTO).Id;
                string docName = cell.Value.ToString();
                byte[] docData = null;

                //Retrieve file data
                Guid key = Guid.NewGuid();
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {
                    try {
                        //Get the Document content
                        docData = session.QueryOver<Document>()
                            .Select(Projections.Property<Document>(x => x.DocumentBLOB))
                            .Where(x => x.Id == docId)
                            .FutureValue<byte[]>()
                            .Value;

                        tr.Commit();

                    } catch (Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, "Failed to delete this Document. " + ex.Message);
                        Program.log.Error("Failed to delete this Document.", ex);
                    }
                }

                SessionFactory.TryCloseSession(key);

                //Display file
                FileHelper.DisplayFile(docName, docData, Constants.TEMP_FOLDER_NAME);
            }
        }
    }
}
