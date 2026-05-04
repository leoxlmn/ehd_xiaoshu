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
using EHD.Service;
using EHD.Constant;
using NHibernate;
using EHD.FormUtitlity;
using Utility;
using WordFileProcessor;
using NHibernate.Criterion;
using HolidayCalculator;
using NHibernate.Transform;
using NHibernate.SqlCommand;
using System.IO;
using System.Diagnostics;

namespace EHD.Admin {
    public partial class EditDocumentForm : Form {

        //Open session for the form
        private Guid _sessionKey = Guid.NewGuid();
        private ISession session;

        #region Private members
        Document EditingDocument = null;
        int _editingDocumentId = 0;
        int _patientId = 0;
        bool _isSearchingPatient = false;
        string _fileName = null;
        byte[] _fileData = null;
        #endregion Private members

        #region Public properties
        public int PatientId {
            get {
                return _patientId;
            }
            set {
                _patientId = value;
            }
        }
        #endregion Public Properties

        public EditDocumentForm(int documentId = 0) {

            _editingDocumentId = documentId;

            InitializeComponent();

        }

        private void EditDocumentForm_Load(object sender, EventArgs e) {

            //Open session
            if (session == null) session = SessionFactory.GetOpenSession(_sessionKey);

            //Populate dropdownlists
            populateDropdowns();

            //Load Document info
            loadDocument();
        }

        private void loadDocument() {
            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    if (_editingDocumentId == 0) {
                        EditingDocument = new Document();
                        EditingDocument.CreatedBy = Program.LogonUser.DisplayName;
                        EditingDocument.CreatedTime = DateTime.Now;

                        if(PatientId != default(int)) {
                            EditingDocument.Patient = session.Get<Patient>(PatientId);
                        }
                    } else {
                        EditingDocument = session.Get<Document>(_editingDocumentId);
                    }

                    populateDocumentDetail(EditingDocument);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Document. " + ex.Message);
                Program.log.Error("Failed to load Document.", ex);
            } finally {
            }
        }

        private void populateDocumentDetail(Document doc) {
            if (doc.Id != default(int)) {
                lblDocId.Text = doc.Id.ToString();
                lblLoadedTime.Text = doc.DocumentCreationTime.ToString( doc.DocumentCreationTime.TimeOfDay.TotalMinutes >= 1 ? "MMM dd, yyyy hh:mm tt" : "MMMdd, yyyy");

                if(doc.Patient != null) {
                    ucPatient.SelectedItem = new SimpleListItem {
                        Id = doc.Patient.Id,
                        Text = string.Format("{0} - {1} {2}", doc.Patient.FileNumber, doc.Patient.FirstName, doc.Patient.LastName)
                    };
                }

                drpDocType.SelectedValue = doc.DocumentType.Id;

                dtCreationDate.Value = doc.CreatedTime.Date;
                dtCreationTime.Value = doc.CreatedTime;

                _fileName = doc.DocumentName;
                _fileData = doc.DocumentBLOB;

                lnkFile.Visible = (_fileData != null && _fileData.Length > 0);
                lnkFile.Text = _fileName;

            } else {

                lblDocId.Text = string.Empty;
                lblLoadedTime.Text = string.Empty;

                if (doc.Patient != null) {
                    ucPatient.SelectedItem = new SimpleListItem {
                        Id = doc.Patient.Id,
                        Text = string.Format("{0} - {1} {2}", doc.Patient.FileNumber, doc.Patient.FirstName, doc.Patient.LastName)
                    };
                }

                dtCreationDate.Value = DateTime.Now.Date;
                dtCreationTime.Value = new DateTime(2000, 1, 1, 0, 0, 0);
                lnkFile.Visible = false;
            }
        }

        private void populateDropdowns() {
            try {
                IList<SimpleListItem> lstDocumentType = null;

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

                drpDocType.DataSource = lstDocumentType;
                drpDocType.ValueMember = "Id";
                drpDocType.DisplayMember = "Text";

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Document Type List. " + ex.Message);
                Program.log.Error("Failed to load Document Type List.", ex);
            }
        }

        private void ucPatient_MinInputLengthReached(object sender, EventArgs e) {
            if (_isSearchingPatient) return;

            _isSearchingPatient = true;

            //Search patients
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    //Search top 20 matches
                    SimpleListItem alias = null;
                    var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Insurer).Eager
                        .Where(Restrictions.Disjunction()
                            .Add(Restrictions.Eq(Projections.Property<Patient>(x => x.FileNumber), ucPatient.InputText))
                            .Add(Restrictions.On<Patient>(x => x.FirstName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.LastName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.ContactInfo.Address.AddressLine1).IsInsensitiveLike(ucPatient.InputText + "%"))
                            )
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => alias.Id)
                            .Select(x => Projections.Concat(x.FileNumber, " - ", x.FirstName, " ", x.LastName, " [", x.ContactInfo.Address.AddressLine1, " ", x.ContactInfo.Address.AddressLine2, "]")).WithAlias(() => alias.Text)
                            )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.FirstName).Asc
                        .Take(20)
                        .List<SimpleListItem>();

                    ucPatient.UpdateList(patients);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to search patient. " + ex.Message);
                    Program.log.Error("Failed to search patient.", ex);
                } finally {
                    _isSearchingPatient = false;
                }
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e) {
            if (DialogResult.OK == dlgOpenFile.ShowDialog(this)) {

                FileInfo fi = new FileInfo(dlgOpenFile.FileName);

                if (fi.Length <= 0) {
                    MessageBox.Show(this, "The file is empty! Please select a valid file to add.");
                    return;
                }

                if (dlgOpenFile.SafeFileName.Length > 100) {
                    MessageBox.Show(this, "The file name is too long. Please rename it to be no longer than 100 characters.");
                    return;
                }

                _fileName = dlgOpenFile.SafeFileName;
                _fileData = File.ReadAllBytes(dlgOpenFile.FileName);

                lnkFile.Visible = true;
                lnkFile.Text = _fileName;
            }
        }

        private void lnkFile_Click(object sender, EventArgs e) {
            if (_fileData == null) {
                return;
            }

            try {
                FileHelper.DisplayFile(_fileName, _fileData, Constants.TEMP_FOLDER_NAME);
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to view Document. " + ex.Message);
                Program.log.Error("Failed to view Document.", ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {

            //Check if Patient has been selected
            if (ucPatient.SelectedItem == null || ucPatient.SelectedItem.Id == default(int)) {
                MessageBox.Show(this, "Please select a Patient.");
                ucPatient.Focus();
                ucPatient.Select();
                return;
            }

            //Check if Document Type has been selected
            if (drpDocType.SelectedItem == null || (drpDocType.SelectedItem as SimpleListItem).Id == default(int)) {
                MessageBox.Show(this, "Please select a Document Type.");
                drpDocType.Focus();
                drpDocType.Select();
                return;
            }

            //Check if file has been selected
            if(_fileData == null) {
                MessageBox.Show(this, "Please select a file to upload.");
                btnBrowseFile.Focus();
                btnBrowseFile.Select();
                return;
            }

            if (EditingDocument != null) {
                try {

                    using (ITransaction tr = session.BeginTransaction()) {

                        if (EditingDocument == null || EditingDocument.Id == default(int)) {
                            EditingDocument = new Document();
                            EditingDocument.IsTestData = Program.LogonUser.IsTestData;
                            EditingDocument.CreatedBy = Program.LogonUser.DisplayName;
                            EditingDocument.CreatedTime = DateTime.Now;
                        } else {
                            EditingDocument = session.Get<Document>(EditingDocument.Id);
                            EditingDocument.UpdatedBy = Program.LogonUser.DisplayName;
                            EditingDocument.UpdatedTime = DateTime.Now;
                        }

                        EditingDocument.Patient = session.Get<Patient>(ucPatient.SelectedItem.Id);
                        EditingDocument.DocumentType = session.Get<DocumentType>((drpDocType.SelectedItem as SimpleListItem).Id);
                        EditingDocument.DocumentCreationTime = dtCreationDate.Value.Date
                            .AddHours(dtCreationTime.Value.Hour)
                            .AddMinutes(dtCreationTime.Value.Minute);
                        EditingDocument.Note = txtNote.Text;
                        EditingDocument.DocumentName = _fileName;
                        EditingDocument.DocumentBLOB = _fileData;

                        if (!EditingDocument.HasMandatoryValues()) {
                            MessageBox.Show(this,
                                Constants.MSG_MANDATORY_FIELD_MISSING,
                                "Missing Values",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            tr.Rollback();
                            return;
                        }

                        session.Save(EditingDocument);

                        tr.Commit();
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to save Document. " + ex.Message);
                    Program.log.Error("Failed to save Document.", ex);
                } finally {
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void EditDocumentForm_FormClosed(object sender, FormClosedEventArgs e) {
            //Try to close session
            SessionFactory.TryCloseSession(_sessionKey);
        }
    }
}
