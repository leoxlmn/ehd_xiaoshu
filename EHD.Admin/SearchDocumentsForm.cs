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
    public partial class SearchDocumentsForm : Form {

        #region Private members
        private bool _sortAscending = false;
        private string _sortName = "DocumentCreationTime";
        #endregion Private members

        public SearchDocumentsForm() {
            InitializeComponent();
        }

        private void reloadDocuments() {
            IList<DocumentDTO> lstDocument = null;

            DocumentDTO dtoDoc = null;
            Document aDoc = null;
            DocumentType aDocType = null;
            Patient aPatient = null;
            int totalCount = 0;

            //Create key for session access
            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);

            //Retrieve documents from database
            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    var qry = session.QueryOver<Document>(() => aDoc)
                        .JoinQueryOver<DocumentType>(() => aDoc.DocumentType, () => aDocType)
                        .JoinQueryOver<Patient>(() => aDoc.Patient, () => aPatient);

                    //Add patient filter
                    if (txtPatient.Text != null && txtPatient.Text.Trim().Length > 0) {
                        string patientNameSearch = "%" + txtPatient.Text.Trim() + "%";
                        qry.Where(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FirstName), patientNameSearch))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.LastName), patientNameSearch))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.MiddleName), patientNameSearch))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                patientNameSearch,
                                NHibernateUtil.String))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_MiddleName, ' ', PTN_LastName) LIKE ?",
                                patientNameSearch,
                                NHibernateUtil.String)));
                    }

                    //Add document type filter
                    if (drpDocumentType.SelectedItem != null && (drpDocumentType.SelectedItem as SimpleListItem).Id > 0) {
                        qry.Where(Restrictions.Eq(Projections.Property(() => aDocType.Id), (drpDocumentType.SelectedItem as SimpleListItem).Id));
                    }

                    //Add document name filter
                    if (txtDocumentName.Text != null && txtDocumentName.Text.Trim().Length > 0) {
                        string docNameSearch = "%" + txtDocumentName.Text.Trim() + "%";
                        qry.Where(Restrictions.InsensitiveLike(Projections.Property(() => aDoc.DocumentName), docNameSearch));
                    }

                    //Add document note filter
                    if (txtDocumentNote.Text != null && txtDocumentNote.Text.Trim().Length > 0) {
                        string docNoteSearch = "%" + txtDocumentNote.Text.Trim() + "%";
                        qry.Where(Restrictions.InsensitiveLike(Projections.Property(() => aDoc.Note), docNoteSearch));
                    }

                    //Add loaded time filter
                    if (dtLoadedFrom.Checked) {
                        qry.Where(Restrictions.Ge(Projections.Property(() => aDoc.CreatedTime), dtLoadedFrom.Value.Date));
                    }
                    if (dtLoadedTo.Checked) {
                        qry.Where(Restrictions.Lt(Projections.Property(() => aDoc.CreatedTime), dtLoadedTo.Value.Date.AddDays(1)));
                    }

                    //Add creation time filter
                    if (dtCreationFrom.Checked) {
                        qry.Where(Restrictions.Ge(Projections.Property(() => aDoc.DocumentCreationTime), dtCreationFrom.Value.Date));
                    }
                    if (dtCreationTo.Checked) {
                        qry.Where(Restrictions.Lt(Projections.Property(() => aDoc.DocumentCreationTime), dtCreationTo.Value.Date.AddDays(1)));
                    }

                    //Get total count
                    totalCount = (qry.Clone())
                        .Select(Projections.CountDistinct<Document>(x => x.Id))
                        .FutureValue<int>()
                        .Value;

                    //Add Order by
                    switch (_sortName) {
                        case "PatientName":
                            if (_sortAscending) {
                                qry.OrderBy(Projections.Property(() => aPatient.FirstName)).Asc()
                                    .ThenBy(Projections.Property(() => aPatient.LastName)).Asc();
                            } else {
                                qry.OrderBy(Projections.Property(() => aPatient.FirstName)).Desc()
                                    .ThenBy(Projections.Property(() => aPatient.LastName)).Desc();
                            }
                            break;
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
                            .Select(() => Projections.Concat(aPatient.FirstName, " ", aPatient.LastName)).WithAlias(() => dtoDoc.PatientName)
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

        private void populateDropdowns() {
            try {
                IList<SimpleListItem> lstDocumentType = null;

                Guid key = Guid.NewGuid();
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    SimpleListItem aliasListItem = null;

                    var qryDocType = session.QueryOver<DocumentType>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.DocumentTypeName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.DocumentTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .Future<SimpleListItem>();


                    lstDocumentType = qryDocType.ToList();
                    lstDocumentType.Insert(0, new SimpleListItem());

                    tr.Commit();
                }

                SessionFactory.TryCloseSession(key);

                drpDocumentType.DataSource = lstDocumentType;
                drpDocumentType.ValueMember = "Id";
                drpDocumentType.DisplayMember = "Text";

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Document Type List. " + ex.Message);
                Program.log.Error("Failed to load Document Type List.", ex);
            }
        }

        private void SearchDocumentsForm_Load(object sender, EventArgs e) {
            //Initiate pager
            pager.Page = 1;
            pager.GoToPageEvent += new GoToPageEventHandler(navigate);

            //Dropdowns
            populateDropdowns();

            //Display documents
            reloadDocuments();
        }

        private void navigate(int page) {
            reloadDocuments();
        }

        private void btnAddDocument_Click(object sender, EventArgs e) {
            if (DialogResult.OK == (new EditDocumentForm()).ShowDialog()) {
                pager.Page = 1;
                reloadDocuments();
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

            reloadDocuments();
        }

        private void btnEditDocument_Click(object sender, EventArgs e) {
            if (grdDocuments.SelectedRows.Count > 0) {
                DocumentDTO doc = grdDocuments.SelectedRows[0].DataBoundItem as DocumentDTO;
                if (DialogResult.OK == (new EditDocumentForm(doc.Id)).ShowDialog(this)) {
                    reloadDocuments();
                }
            }
        }

        private void grdDocuments_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Show/hide columns
            if (grdDocuments.Columns != null) {
                if (grdDocuments.Columns.Contains("Id")) grdDocuments.Columns["Id"].Visible = false;
                if (grdDocuments.Columns.Contains("Version")) grdDocuments.Columns["Version"].Visible = false;
                if (grdDocuments.Columns.Contains("PatientId")) grdDocuments.Columns["PatientId"].Visible = false;
            }

            //Set sort mode for each column
            foreach (DataGridViewColumn col in grdDocuments.Columns) {
                switch (col.DataPropertyName) {
                    case "LoadedTime":
                    case "DocumentType":
                    case "DocumentCreationTime":
                    case "PatientName":
                        col.SortMode = DataGridViewColumnSortMode.Programmatic;
                        break;
                    case "DocumentName":
                        col.SortMode = DataGridViewColumnSortMode.Programmatic;
                        for (int row = 0; row < grdDocuments.RowCount; row++) {
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

            reloadDocuments();
        }

        private void grdDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e) {

            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewCell cell = grdDocuments[e.ColumnIndex, e.RowIndex];
            if (cell is DataGridViewLinkCell && cell.Value != null) {

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

        private void btnSearchPatient_Click(object sender, EventArgs e) {
            //reset pager
            pager.Page = 1;

            //reload list
            reloadDocuments();
        }

        private void txtPatient_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchPatient_Click(null, null);
            }
        }

        private void txtDocumentName_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchPatient_Click(null, null);
            }
        }

        private void txtDocumentNote_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchPatient_Click(null, null);
            }
        }

        private void drpDocumentType_SelectedIndexChanged(object sender, EventArgs e) {
            btnSearchPatient_Click(null, null);
        }

        private void dtLoadedFrom_ValueChanged(object sender, EventArgs e) {
            btnSearchPatient_Click(null, null);
        }

        private void dtLoadedTo_ValueChanged(object sender, EventArgs e) {
            btnSearchPatient_Click(null, null);
        }

        private void dtCreationFrom_ValueChanged(object sender, EventArgs e) {
            btnSearchPatient_Click(null, null);
        }

        private void dtCreationTo_ValueChanged(object sender, EventArgs e) {
            btnSearchPatient_Click(null, null);
        }
    }
}
