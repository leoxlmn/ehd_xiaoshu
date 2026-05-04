using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using EHD.Login;
using Utility;
using NHibernate;
using NHibernate.Criterion;
using EHD.Repository;
using EHD.Model.Entity;
using EHD.Constant;
using EHD.Service;
using FilePathExtender;
using WordFileProcessor;
using System.IO;
using System.Drawing.Drawing2D;
using System.Reflection;
using NHibernate.Transform;
using NHibernate.SqlCommand;
using EHD.Model.DTO;
using System.Deployment.Application;

namespace EHD.Admin
{
    public partial class MainForm : Form
    {
        #region Delegates
        delegate void ShowMessageDelegate(string msg);
        #endregion

        private void ShowMessage(string msg) {
            ShowMessageDelegate d = new ShowMessageDelegate(_showMsg);
            this.Invoke(d, new object[] { msg });
        }

        private void _showMsg(string msg) {
            MessageBox.Show(this, msg);
        }

        private string _patientSearchText = string.Empty;
        private int _lastEditedPatientId = 0;
        private int _lastEditedInitialTreatmentId = 0;
        private int _lastEditedFollowUpTreatmentId = 0;
        private PleaseWaitForm _waitForm = null;
        private bool _printProcessCompleted = false;

        private string _treatmentNoteTitle = string.Empty;

        private bool _formLoading = true;

        private bool _patientSortAscending = false;
        private string _patientSortColumnName = "CreatedTime";//Sort by FileNumber by default

        private BookingForm _frmBooking = null;

        //private Guid _key = Guid.NewGuid();

        public MainForm()
        {
            InitializeComponent();
        }

        private void tsbExit_Click(object sender, EventArgs e) {
            //Application.Exit();
            Application.Restart();
        }

        private void tsbSettings_Click(object sender, EventArgs e) {
            (new SettingsForm()).ShowDialog(this);
        }

        private void MainForm_Load(object sender, EventArgs e) {
            //Get session
            //ISession session = SessionFactory.GetOpenSession(_key);

            string version = string.Empty;
            if (ApplicationDeployment.IsNetworkDeployed) version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            this.Text = $"Easy Healthcare Desktop (Version: [{version}] Server: [{Program.config.Server}] DB: [{Program.config.Database}])";

            lblLogonUser.Text = Program.LogonUser.DisplayName;

            //Hide Settings button if the user is a Receptionist
            if (Program.LogonUser.UserType == UserType.Receptionist) {
                tsbSettings.Visible = false;
                toolStripSeparator2.Visible = false;
            }

            //Show/hide Practitioner Calendar button
            tsbPractitionerCalendar.Visible = Program.config.EnablePractitionerCalendar;

            //load checked list box user controls
            loadCheckedListBoxes();

            //Initiate pager
            pager.Page = 1;
            pager.GoToPageEvent += new GoToPageEventHandler(patientNavigate);

            //Show Patient list
            showPatientList();

            //Indicate the loading has completed.
            _formLoading = false;
        }

        private void patientNavigate(int page) {
            showPatientList();
        }

        private void tsbAddPatient_Click(object sender, EventArgs e) {
            EditPatientForm patientForm = new EditPatientForm();
            if(DialogResult.OK == patientForm.ShowDialog(this)) {

                _lastEditedPatientId = patientForm.EditingPatient.Id;

                //Refresh patient list
                txtSearchName.Text = string.Empty;
                txtSearchPhone.Text = string.Empty;
                _patientSearchText = string.Empty;

                showPatientList(_lastEditedPatientId);
            }
        }

        private void btnPicAddPatient_Click(object sender, EventArgs e) {
            tsbAddPatient_Click(sender, e);
        }

        private void tsbPatients_Click(object sender, EventArgs e) {
            _patientSearchText = string.Empty;
            pager.Page = 1;
            showPatientList();
        }

        #region Patient List

        private void grdPatientList_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            PatientDTO patient = grdPatientList.Rows[rowIndex].DataBoundItem as PatientDTO;

            string colName = grdPatientList.Columns[colIndex].Name;
            switch(colName) {
                case Constants.IMAGE_BUTTON_EDIT_PATIENT:
                    editPatient(patient.Id);
                    break;
                case Constants.IMAGE_BUTTON_DELETE_PATIENT:
                    deletePatient(patient.Id);
                    break;
                case Constants.IMAGE_BUTTON_VIEW_PATIENT:
                    viewPatient(patient.Id);
                    break;
                case Constants.IMAGE_BUTTON_ADD_INIT_TREATMENT:
                    addInitialTreatment(patient.Id);
                    break;
                case Constants.IMAGE_BUTTON_ACCOUNT_BALANCE:
                    viewAccountBalance(patient.Id);
                    break;
                case Constants.IMAGE_BUTTON_DOCUMENT:
                    viewDocuments(patient.Id);
                    break;
            }
        }

        private void grdPatientList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            PatientDTO patient = grdPatientList.Rows[rowIndex].DataBoundItem as PatientDTO;
            editPatient(patient.Id);
        }

        private void grdPatientList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            foreach(DataGridViewRow dr in grdPatientList.Rows) {
                PatientDTO patient = dr.DataBoundItem as PatientDTO;

                //Populate Sex Icon column
                dr.Cells[Constants.IMAGE_BUTTON_VIEW_PATIENT].Value = patient.Sex == Sex.Male ? lstIcons.Images["Male"] :
                    patient.Sex == Sex.Female ? lstIcons.Images["Female"] :
                    lstIcons.Images["Transparent"];

                //Bind Edit Button column
                dr.Cells[Constants.IMAGE_BUTTON_EDIT_PATIENT].Value = lstIcons.Images["Edit"];

                //Bind Delete Button column
                //dr.Cells[Constants.IMAGE_BUTTON_DELETE_PATIENT].Value = lstIcons.Images["Remove"];

                //Bind Add Initial Treatment Button column
                dr.Cells[Constants.IMAGE_BUTTON_ADD_INIT_TREATMENT].Value = lstIcons.Images["InitialTreatment"];

                //Bind Print Button column
                dr.Cells[Constants.IMAGE_BUTTON_PRINT_PATIENT].Value = lstIcons.Images["Print"];

                //Bind Account Balance column
                dr.Cells[Constants.IMAGE_BUTTON_ACCOUNT_BALANCE].Value = lstIcons.Images["Dollar"];

                //Bind Document column
                dr.Cells[Constants.IMAGE_BUTTON_DOCUMENT].Value = lstIcons.Images["Folder"];
            }

            //Hide some not-in-use buttons. Can be re-enabled if needed
            grdPatientList.Columns[Constants.IMAGE_BUTTON_ADD_INIT_TREATMENT].Visible = false;
            grdPatientList.Columns[Constants.IMAGE_BUTTON_PRINT_PATIENT].Visible = false;

            //Hide unimportment columns
            showHideTrackingColumns(grdPatientList, false);

            //Set sort mode for each column
            foreach (DataGridViewColumn col in grdPatientList.Columns) {
                switch (col.DataPropertyName) {
                    case "FileNumber":
                    case "FirstName":
                    case "LastName":
                    case "CreatedTime":
                        col.SortMode = DataGridViewColumnSortMode.Programmatic;
                        break;
                    default:
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                        break;
                }

                if (col.DataPropertyName == _patientSortColumnName) {
                    col.HeaderCell.SortGlyphDirection = _patientSortAscending ? SortOrder.Ascending : SortOrder.Descending;
                }
            }
        }

        private void grdPatientList_RowEnter(object sender, DataGridViewCellEventArgs e) {

            //This event might be called multiple times before the form has been loaded
            //Do NOT do anything until the form has been loaded.
            if (_formLoading) return;

            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            Debug.WriteLine("RowEnter on PatientList Grid row " + e.RowIndex.ToString());

            PatientDTO selectedPatient = grdPatientList.Rows[e.RowIndex].DataBoundItem as PatientDTO;
            if (selectedPatient != null) {
                showInitialTreatmentList(selectedPatient.Id);
            }
        }

        private void clearPatientList() {
            grdPatientList.DataSource = null;//Clear patient list
            _lastEditedPatientId = 0;
            clearInitialList();
        }

        private void clearInitialList() {
            grdInitialTreatmentList.DataSource = null;
            _lastEditedInitialTreatmentId = 0;
            clearFollowUpList();
        }

        private void clearFollowUpList() {
            grdFollowUpTreatmentList.DataSource = null;
            _lastEditedFollowUpTreatmentId = 0;
        }

        private void showPatientList(int patientId = 0) {
            //Create key for NHibernate session access
            Guid key = Guid.NewGuid();

            if(Program.LogonUser.UserType == UserType.None) return;

            if (ucInsurer.NoneChecked) {
                clearPatientList();
                pager.Page = 1;
                pager.TotalPage = 1;
                return;
            }

            //Get selected insurer ids
            IList<int> selectedInsurerIds = new List<int>();
            foreach (object item in ucInsurer.CheckedItems) {
                if (item is SimpleListItem) {
                    selectedInsurerIds.Add((item as SimpleListItem).Id);
                }
            }

            //Prepare a SortableBindingList for patient grid
            SortableBindingList<PatientDTO> patients = new SortableBindingList<PatientDTO>();

            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    #region Rewrote below
                    /*
                    //Prepare query
                    var qry = session.CreateCriteria<Patient>("p")
                        .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

                    //Add insurer restrictions
                    if (ucInsurer.AllChecked) {//No restrictions
                    } else if (ucInsurer.AllNonBlanks) {//Exclude nulls
                        qry.Add(Restrictions.IsNotNull("i.Id"));
                    } else if (ucInsurer.BlanksOnly) {//Only nulls
                        qry.Add(Restrictions.IsNull("i.Id"));
                    } else if (ucInsurer.NoneChecked) {//Select none
                        qry.Add(Restrictions.IsNull("p.Id"));//Always false
                    } else if (ucInsurer.IsBlankChecked) {//Include nulls
                        qry.Add(Restrictions.Disjunction()
                            .Add(Restrictions.IsNull("i.Id"))
                            .Add(Restrictions.In("i.Id", selectedInsurerIds.ToArray()))
                            );
                    } else {
                        qry.Add(Restrictions.In("i.Id", selectedInsurerIds.ToArray()));
                    }

                    //Add search restrictions
                    if (_patientSearchText.Length > 0) {
                        string searchText = _patientSearchText + '%';
                        qry.Add(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike("p.FirstName", searchText))
                            .Add(Restrictions.InsensitiveLike("p.LastName", searchText))
                            .Add(Restrictions.InsensitiveLike("p.FileNumber", searchText))
                            .Add(Restrictions.InsensitiveLike("p.ContactInfo.HomePhone", searchText))
                            .Add(Restrictions.InsensitiveLike("p.ContactInfo.CellPhone", searchText))
                            .Add(Restrictions.InsensitiveLike("p.ContactInfo.Address.PostalCode", searchText))
                            .Add(Restrictions.InsensitiveLike("p.ContactInfo.Address.AddressLine1", searchText))
                            .Add(Restrictions.InsensitiveLike("p.ContactInfo.Address.AddressLine2", searchText))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                searchText,
                                NHibernateUtil.String))
                            );
                    }

                    //Add file number search
                    if (fileNo.Length > 0) {
                        qry.Add(Restrictions.InsensitiveLike("p.FileNumber", fileNo));
                    }

                    //Retrieve patient list
                    var lstPatient = (qry.Clone() as ICriteria)
                        .AddOrder(new Order(string.Format("p.{0}", _patientSortColumnName), _patientSortAscending))
                        .SetFirstResult((pager.Page - 1) * pager.PageSize)
                        .SetMaxResults(pager.PageSize)
                        .Future<Patient>();

                    //Get total count
                    int totalCount = qry.SetProjection(Projections.Count(Projections.Id()))
                        .FutureValue<int>()
                        .Value;

                    //Set pager
                    if (totalCount == 0) {
                        pager.TotalPage = 1;
                    } else {
                        pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pager.PageSize)) + 1;
                    }

                    //Comment transaction
                    tr.Commit();

                    //Add patient list into SortableBindingList
                    SortableBindingList<Patient> patients = new SortableBindingList<Patient>();
                    foreach (Patient p in lstPatient) {
                        patients.Add(p);
                    }

                    grdPatientList.DataSource = patients;// lstPatient;
                    if (patients != null && patients.Count > 0) {
                        setSelectedPatientRow();
                    } else {
                        clearInitialList();
                    }
                    */
                    #endregion Reworte below

                    PatientDTO dtoPatient = null;
                    Patient aPatient = null;
                    Insurer aInsurer = null;

                    var qry = session.QueryOver<Patient>(() => aPatient)
                        .JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin);

                    //Add insurer restrictions
                    if (ucInsurer.AllChecked) {//No restrictions
                    } else if (ucInsurer.AllNonBlanks) {//Exclude nulls
                        qry.Where(Restrictions.IsNotNull(Projections.Property(() => aInsurer.Id)));
                    } else if (ucInsurer.BlanksOnly) {//Only nulls
                        qry.Where(Restrictions.IsNull(Projections.Property(() => aInsurer.Id)));
                    } else if (ucInsurer.NoneChecked) {//Select none
                        qry.Where(Restrictions.IsNull(Projections.Property(() => aPatient.Id)));//Always false
                    } else if (ucInsurer.IsBlankChecked) {//Include nulls
                        qry.Where(Restrictions.Disjunction()
                            .Add(Restrictions.IsNull(Projections.Property(() => aInsurer.Id)))
                            .Add(Restrictions.In(Projections.Property(() => aInsurer.Id), selectedInsurerIds.ToArray()))
                            );
                    } else {
                        qry.Where(Restrictions.In(Projections.Property(() => aInsurer.Id), selectedInsurerIds.ToArray()));
                    }

                    //Add search restrictions
                    if (_patientSearchText.Length > 0) {
                        string searchText = _patientSearchText + '%';
                        qry.Where(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FirstName), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.LastName), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FileNumber), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.ContactInfo.HomePhone), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.ContactInfo.CellPhone), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.ContactInfo.Address.PostalCode), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.ContactInfo.Address.AddressLine1), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.ContactInfo.Address.AddressLine2), searchText))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                searchText,
                                NHibernateUtil.String))
                            );
                    }

                    //Add file number search
                    if (patientId != 0) {
                        qry.Where(() => aPatient.Id == patientId);
                    }

                    //Retrieve patient list
                    var qryPatient = (qry.Clone())
                        .SelectList(list => list
                            .Select(() => aPatient.Id).WithAlias(() => dtoPatient.Id)
                            .Select(() => aPatient.Version).WithAlias(() => dtoPatient.Version)
                            .Select(() => aPatient.FamilyPatientId).WithAlias(() => dtoPatient.FamilyPatientId)
                            .Select(() => aPatient.Sex).WithAlias(() => dtoPatient.Sex)
                            .Select(() => aPatient.FileNumber).WithAlias(() => dtoPatient.FileNumber)
                            .Select(() => aPatient.FirstName).WithAlias(() => dtoPatient.FirstName)
                            .Select(() => aPatient.LastName).WithAlias(() => dtoPatient.LastName)
                            .Select(() => aPatient.MiddleName).WithAlias(() => dtoPatient.MiddleName)
                            .Select(() => aPatient.DateOfBirth).WithAlias(() => dtoPatient.DateOfBirth)

                            .Select(() => aPatient.ContactInfo.Address.AddressLine1).WithAlias(() => dtoPatient.Address1)
                            .Select(() => aPatient.ContactInfo.Address.AddressLine2).WithAlias(() => dtoPatient.Address2)
                            .Select(() => aPatient.ContactInfo.Address.PostalCode).WithAlias(() => dtoPatient.PostalCode)
                            .Select(() => aPatient.ContactInfo.HomePhone).WithAlias(() => dtoPatient.HomePhone)
                            .Select(() => aPatient.ContactInfo.CellPhone).WithAlias(() => dtoPatient.CellPhone)
                            .Select(() => aPatient.ContactInfo.HomeFax).WithAlias(() => dtoPatient.HomeFax)
                            .Select(() => aPatient.ContactInfo.Email).WithAlias(() => dtoPatient.Email)

                            .Select(() => aPatient.Note).WithAlias(() => dtoPatient.Note)
                            .Select(() => aInsurer.InsurerName).WithAlias(() => dtoPatient.Insurer)
                        )
                        .TransformUsing(Transformers.AliasToBean<PatientDTO>());

                    //Order by
                    switch (_patientSortColumnName) {
                        case "FileNumber":
                            if (_patientSortAscending) qryPatient.OrderBy(() => aPatient.FileNumber).Asc();
                            else qryPatient.OrderBy(() => aPatient.FileNumber).Desc();
                            break;
                        case "FirstName":
                            if (_patientSortAscending) qryPatient.OrderBy(() => aPatient.FirstName).Asc();
                            else qryPatient.OrderBy(() => aPatient.FirstName).Desc();
                            break;
                        case "LastName":
                            if (_patientSortAscending) qryPatient.OrderBy(() => aPatient.LastName).Asc();
                            else qryPatient.OrderBy(() => aPatient.LastName).Desc();
                            break;
                        case "Insurer":
                            if (_patientSortAscending) qryPatient.OrderBy(() => aInsurer.InsurerName).Asc();
                            else qryPatient.OrderBy(() => aInsurer.InsurerName).Desc();
                            break;
                        default:
                            if (_patientSortAscending) qryPatient.OrderBy(() => aPatient.FileNumber).Asc();
                            else qryPatient.OrderBy(() => aPatient.FileNumber).Desc();
                            break;
                    }

                    var lstPatient = qryPatient
                        .Skip((pager.Page - 1) * pager.PageSize)
                        .Take(pager.PageSize)
                        .Future<PatientDTO>();

                    //Get total count in search result
                    int totalCount = qry.RowCount();

                    //Get grand total
                    int grandTotalCount = session.QueryOver<Patient>()
                        .RowCount();

                    //Set pager
                    if (totalCount == 0) {
                        pager.TotalPage = 1;
                    } else {
                        pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pager.PageSize)) + 1;
                    }

                    //Add patient list into SortableBindingList
                    foreach (PatientDTO p in lstPatient) {
                        patients.Add(p);
                    }

                    //Comment transaction
                    tr.Commit();

                    //Update total labels
                    lblTotalPatient.Text = grandTotalCount.ToString();
                    lblSearchResult.Text = totalCount.ToString();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, ex.Message);
                Program.log.Error("Show Patient List error.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            grdPatientList.DataSource = patients;
            if (patients != null && patients.Count > 0) {
                setSelectedPatientRow();
            } else {
                clearInitialList();
            }

        }

        private void setSelectedPatientRow() {
            if(_lastEditedPatientId != 0 && grdPatientList.Rows.Count > 0) {
                
                foreach(DataGridViewRow dr in grdPatientList.Rows) {
                    PatientDTO p = dr.DataBoundItem as PatientDTO;
                    if(p.Id == _lastEditedPatientId) {
                        //Highlight selected row
                        dr.Selected = true;
                        //Fireup RowEnter event for the selected row
                        grdPatientList_RowEnter(grdPatientList, new DataGridViewCellEventArgs(0, dr.Index));
                        //Scroll grid to display the selected row if it's not displayed
                        if(!dr.Displayed) 
                            grdPatientList.FirstDisplayedScrollingRowIndex = dr.Index;

                        Debug.WriteLine("Set selected row to " + dr.Index);

                        break;
                    }
                }
            }
        }

        private void viewPatient(int patientId) {
            EditPatientForm patientForm = new EditPatientForm(patientId, true);
            patientForm.ShowDialog(this);
        }

        private void editPatient(int patientId) {
            EditPatientForm patientForm = new EditPatientForm(patientId);
            if(DialogResult.OK == patientForm.ShowDialog(this)) {

                _lastEditedPatientId = patientForm.EditingPatient.Id;

                showPatientList(_lastEditedPatientId);
            }
        }

        private void deletePatient(int patientId) {
            if(DialogResult.Yes == MessageBox.Show(this, "Do you want to DELETE this patient?",
                "Delete Patient",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)) {

                //Create key for session access
                Guid key = Guid.NewGuid();

                try {
                    //Get session
                    ISession session = SessionFactory.GetOpenSession(key);

                    using (ITransaction tr = session.BeginTransaction()) {
                        Patient patientToDelete = session.Get<Patient>(patientId);
                        //Update UpdateBy field so the audit listener would know who is deleting
                        patientToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                        session.Delete(patientToDelete);

                        //Commit transaction
                        tr.Commit();

                        _lastEditedPatientId = 0;
                        showPatientList();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to delete patient. " + ex.Message);
                    Program.log.Error("Failed to delete patient.", ex);
                } finally {
                    SessionFactory.TryCloseSession(key);
                }

            }
        }
        #endregion

        #region Initial Treatment List
        private void grdInitialTreatmentList_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            InitialTreatmentDTO it = grdInitialTreatmentList.Rows[rowIndex].DataBoundItem as InitialTreatmentDTO;

            string colName = grdInitialTreatmentList.Columns[colIndex].Name;
            switch(colName) {
                case Constants.IMAGE_BUTTON_EDIT_INIT_TREATMENT:
                    editInitialTreatment(it.Id);
                    break;
                case Constants.IMAGE_BUTTON_DELETE_INIT_TREATMENT:
                    deleteInitialTreatment(it.Id);
                    break;
                case Constants.IMAGE_BUTTON_VIEW_INIT_TREATMENT:
                    viewInitialTreatment(it.Id);
                    break;
                case Constants.IMAGE_BUTTON_ADD_FOLLOWUP_TREATMENT:
                    addFollowUpTreatment(it.Id);
                    break;
                case Constants.IMAGE_BUTTON_PRINT_INIT_TREATMENT:
                    printInitialTreatment(it.Id);
                    break;
            }
        }

        private void grdInitialTreatmentList_RowEnter(object sender, DataGridViewCellEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            Debug.WriteLine("RowEnter on InitialTreatmentList Grid row " + e.RowIndex.ToString());

            InitialTreatmentDTO it = grdInitialTreatmentList.Rows[e.RowIndex].DataBoundItem as InitialTreatmentDTO;
            if (it != null) {
                showFollowUpTreatmentList(it.Id);
            }
        }

        private void grdInitialTreatmentList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            InitialTreatmentDTO it = grdInitialTreatmentList.Rows[rowIndex].DataBoundItem as InitialTreatmentDTO;
            editInitialTreatment(it.Id);
        }

        private void grdInitialTreatmentList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            foreach(DataGridViewRow dr in grdInitialTreatmentList.Rows) {
                InitialTreatmentDTO it = dr.DataBoundItem as InitialTreatmentDTO;

                //Populate View Detail column
                dr.Cells[Constants.IMAGE_BUTTON_VIEW_INIT_TREATMENT].Value = lstIcons.Images["ViewDetail"];

                //Bind Edit Button column
                dr.Cells[Constants.IMAGE_BUTTON_EDIT_INIT_TREATMENT].Value = lstIcons.Images["Edit"];

                //Bind Delete Button column
                //dr.Cells[Constants.IMAGE_BUTTON_DELETE_INIT_TREATMENT].Value = lstIcons.Images["Remove"];

                //Bind Add Initial Treatment Button column
                dr.Cells[Constants.IMAGE_BUTTON_ADD_FOLLOWUP_TREATMENT].Value = lstIcons.Images["FollowUpTreatment"];

                //Bind Print Button column
                dr.Cells[Constants.IMAGE_BUTTON_PRINT_INIT_TREATMENT].Value = lstIcons.Images["Print"];

                //Bind Complete icon column
                dr.Cells[Constants.IMAGE_ICON_INIT_TREATMENT_COMPLETE].Value = 
                    it.IsCompleted ? lstIcons.Images["Complete"] : lstIcons.Images["Transparent"];
            }

            //Hide some not-in-use buttons. Can be re-enabled if needed
            grdInitialTreatmentList.Columns[Constants.IMAGE_BUTTON_ADD_FOLLOWUP_TREATMENT].Visible = false;
            grdInitialTreatmentList.Columns[Constants.IMAGE_BUTTON_PRINT_INIT_TREATMENT].Visible = false;

            //Hide unimportment columns
            showHideTrackingColumns(grdInitialTreatmentList, false);
        }

        private void viewInitialTreatment(int itId) {
            EditInitialTreatmentForm InitialTreatmentForm = new EditInitialTreatmentForm(0, itId, true);
            InitialTreatmentForm.ShowDialog(this);
        }

        private void deleteInitialTreatment(int itId) {
            if(DialogResult.Yes == MessageBox.Show(this, "Do you want to DELETE this Initial Treatment?",
                "Delete Initial Treatment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)) {

                //Create key for session access
                Guid key = Guid.NewGuid();

                try {
                    //Get session
                    ISession session = SessionFactory.GetOpenSession(key);

                    using (ITransaction tr = session.BeginTransaction()) {

                        InitialTreatment it = session.Get<InitialTreatment>(itId);

                        //backup last editing patient
                        _lastEditedPatientId = it.Patient.Id;

                        //Delete
                        JCService service = new JCService(session, Program.LogonUser);
                        service.DeleteInitialTreatment(itId);

                        //Commit
                        tr.Commit();

                        _lastEditedInitialTreatmentId = 0;
                        showInitialTreatmentList(_lastEditedPatientId);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to delete Initial Treatment. " + ex.Message);
                    Program.log.Error("Failed to delete Initial Treatment.", ex);
                } finally {
                    SessionFactory.TryCloseSession(key);
                }
            }
        }

        private void editInitialTreatment(int itId) {
            EditInitialTreatmentForm InitialTreatmentForm = new EditInitialTreatmentForm(0, itId);
            if(DialogResult.OK == InitialTreatmentForm.ShowDialog(this)) {

                _lastEditedInitialTreatmentId = InitialTreatmentForm.EditingInitialTreatment.Id;

                //Refresh Initial Treatment list
                showInitialTreatmentList(InitialTreatmentForm.EditingInitialTreatment.Patient.Id);
            }
        }

        private void addInitialTreatment(int patientId) {
            EditInitialTreatmentForm InitialTreatmentForm = new EditInitialTreatmentForm(patientId, 0);
            if (DialogResult.OK == InitialTreatmentForm.ShowDialog(this)) {

                _lastEditedInitialTreatmentId = InitialTreatmentForm.EditingInitialTreatment.Id;

                //Refresh Initial Treatment list
                showInitialTreatmentList(patientId);
            }
        }

        private void viewAccountBalance(int patientId) {
            EditAccountBalanceForm balanceForm = new EditAccountBalanceForm(patientId);
            balanceForm.ShowDialog(this);
        }

        private void viewDocuments(int patientId) {
            EditDocumentsForm docForm = new EditDocumentsForm(patientId);
            docForm.ShowDialog(this);
        }

        private void btnPicAddInitTreatment_Click(object sender, EventArgs e) {
            if(grdPatientList.Rows.Count > 0 && grdPatientList.SelectedRows.Count > 0) {
                PatientDTO p = grdPatientList.SelectedRows[0].DataBoundItem as PatientDTO;
                addInitialTreatment(p.Id);
            }
        }

        private void showInitialTreatmentList(int patientId) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            //Clear list first
            grdInitialTreatmentList.DataSource = new List<InitialTreatmentDTO>();

            if(Program.LogonUser.UserType == UserType.None) return;

            //Get selected treatment type ids
            IList<int> selectedTreatmentTypeIds = new List<int>();
            foreach (object item in ucTreatment.CheckedItems) {
                if (item is SimpleListItem) {
                    selectedTreatmentTypeIds.Add((item as SimpleListItem).Id);
                }
            }

            //Get selected therapist ids
            IList<int> selectedTherapistIds = new List<int>();
            foreach (object item in ucTherapist.CheckedItems) {
                if (item is SimpleUserItem) {
                    selectedTherapistIds.Add((item as SimpleUserItem).Id);
                }
            }

            //Prepare a SortableBindingList for initial treatment grid
            SortableBindingList<InitialTreatmentDTO> initialTreatments = new SortableBindingList<InitialTreatmentDTO>();

            if (patientId != 0) {
                try {
                    //Get session
                    ISession session = SessionFactory.GetOpenSession(key);

                    using (ITransaction tr = session.BeginTransaction()) {

                        #region Rewrote below
                        /*
                        var qry = session.CreateCriteria<InitialTreatment>("it")
                            .CreateCriteria("it.Patient", "p")
                            .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .CreateCriteria("it.Therapist", "t")
                            .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .CreateCriteria("it.TreatmentType", "tt")
                            .Add(Restrictions.Eq("p.Id", patientId));

                        //Add treatment type restrictions
                        if (ucTreatment.AllChecked) {//No restrictions
                        } else if (ucTreatment.AllNonBlanks) {//Exclude nulls
                            qry.Add(Restrictions.IsNotNull("tt.Id"));
                        } else if (ucTreatment.BlanksOnly) {//Only nulls
                            qry.Add(Restrictions.IsNull("tt.Id"));
                        } else if (ucTreatment.NoneChecked) {//Select none
                            qry.Add(Restrictions.IsNull("it.Id"));//Always false
                        } else if (ucTreatment.IsBlankChecked) {//Include nulls
                            qry.Add(Restrictions.Disjunction()
                                .Add(Restrictions.IsNull("tt.Id"))
                                .Add(Restrictions.In("tt.Id", selectedTreatmentTypeIds.ToArray()))
                                );
                        } else {
                            qry.Add(Restrictions.In("tt.Id", selectedTreatmentTypeIds.ToArray()));
                        }
                        //Add therapist restrictions
                        if (ucTherapist.AllChecked) {//No restrictions
                        } else if (ucTherapist.AllNonBlanks) {//Exclude nulls
                            qry.Add(Restrictions.IsNotNull("t.Id"));
                        } else if (ucTherapist.BlanksOnly) {//Only nulls
                            qry.Add(Restrictions.IsNull("t.Id"));
                        } else if (ucTherapist.NoneChecked) {//Select none
                            qry.Add(Restrictions.IsNull("it.Id"));//Always false
                        } else if (ucTherapist.IsBlankChecked) {//Include nulls
                            qry.Add(Restrictions.Disjunction()
                                .Add(Restrictions.IsNull("t.Id"))
                                .Add(Restrictions.In("t.Id", selectedTherapistIds.ToArray()))
                                );
                        } else {
                            qry.Add(Restrictions.In("t.Id", selectedTherapistIds.ToArray()));
                        }
                        //Add date range restrictions
                        if (dtFrom.Checked) {
                            qry.Add(Restrictions.Ge("it.TreatmentTime", dtFrom.Value.Date));
                        }
                        if (dtTo.Checked) {
                            qry.Add(Restrictions.Lt("it.TreatmentTime", dtTo.Value.Date.AddDays(1)));
                        }

                        var lstInitTreatment = qry
                            .SetMaxResults(Program.config.MaxInitialTreatmentsPerPatient)
                            .SetResultTransformer(Transformers.DistinctRootEntity)
                            .List<InitialTreatment>();

                        tr.Commit();

                        //Add list into SortableBindingList
                        SortableBindingList<InitialTreatment> initialTreatments = new SortableBindingList<InitialTreatment>();
                        foreach (InitialTreatment init in lstInitTreatment) {
                            initialTreatments.Add(init);
                        }

                        grdInitialTreatmentList.DataSource = initialTreatments;// lstInitTreatment;
                        setSelectedInitialTreatmentRow();
                        */
                        #endregion Rewrote below

                        InitialTreatment aIT = null;
                        TherapyType aType = null;
                        User aUser = null;
                        UserTitle aTitle = null;
                        InitialTreatmentDTO dtoIT = null;

                        var qry = session.QueryOver<InitialTreatment>(() => aIT)
                            .JoinQueryOver<User>(() => aIT.Therapist, () => aUser, JoinType.InnerJoin)
                            .JoinQueryOver<TherapyType>(() => aIT.TreatmentType, () => aType, JoinType.InnerJoin)
                            .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                            .SelectList(list => list
                                .Select(() => aIT.Id).WithAlias(() => dtoIT.Id)
                                .Select(() => aIT.Version).WithAlias(() => dtoIT.Version)
                                .Select(() => aIT.Patient.Id).WithAlias(() => dtoIT.PatientId)
                                .Select(() => aType.TherapyTypeName).WithAlias(() => dtoIT.TreatmentType)
                                .Select(() => aUser.FirstName).WithAlias(() => dtoIT.TherapistFirstName)
                                .Select(() => aUser.LastName).WithAlias(() => dtoIT.TherapistLastName)
                                .Select(() => aUser.MiddleName).WithAlias(() => dtoIT.TherapistMiddleName)
                                .Select(() => aTitle.Name).WithAlias(() => dtoIT.TherapistTitle)
                                .Select(() => aIT.TreatmentTime).WithAlias(() => dtoIT.TreatmentTime)
                                .Select(() => aIT.TreatmentDurationMinutes).WithAlias(() => dtoIT.Duration)
                                .Select(() => aIT.OpenToTherapist).WithAlias(() => dtoIT.IsOpenToTherapist)
                                .Select(() => aIT.IsComplete).WithAlias(() => dtoIT.IsCompleted)
                            )
                            .TransformUsing(Transformers.AliasToBean<InitialTreatmentDTO>())
                            .Where(() => aIT.Patient.Id == patientId);

                        //Add treatment type restrictions
                        if (ucTreatment.AllChecked) {//No restrictions
                        } else if (ucTreatment.AllNonBlanks) {//Exclude nulls
                            qry.Where(Restrictions.IsNotNull(Projections.Property(() => aType.Id)));
                        } else if (ucTreatment.BlanksOnly) {//Only nulls
                            qry.Where(Restrictions.IsNull(Projections.Property(() => aType.Id)));
                        } else if (ucTreatment.NoneChecked) {//Select none
                            qry.Where(Restrictions.IsNull(Projections.Property(() => aIT.Id)));//Always false
                        } else if (ucTreatment.IsBlankChecked) {//Include nulls
                            qry.Where(Restrictions.Disjunction()
                                .Add(Restrictions.IsNull(Projections.Property(() => aType.Id)))
                                .Add(Restrictions.In(Projections.Property(() => aType.Id), selectedTreatmentTypeIds.ToArray()))
                                );
                        } else {
                            qry.Where(Restrictions.In(Projections.Property(() => aType.Id), selectedTreatmentTypeIds.ToArray()));
                        }
                        //Add therapist restrictions
                        if (ucTherapist.AllChecked) {//No restrictions
                        } else if (ucTherapist.AllNonBlanks) {//Exclude nulls
                            qry.Where(Restrictions.IsNotNull(Projections.Property(() => aUser.Id)));
                        } else if (ucTherapist.BlanksOnly) {//Only nulls
                            qry.Where(Restrictions.IsNull(Projections.Property(() => aUser.Id)));
                        } else if (ucTherapist.NoneChecked) {//Select none
                            qry.Where(Restrictions.IsNull(Projections.Property(() => aIT.Id)));//Always false
                        } else if (ucTherapist.IsBlankChecked) {//Include nulls
                            qry.Where(Restrictions.Disjunction()
                                .Add(Restrictions.IsNull(Projections.Property(() => aUser.Id)))
                                .Add(Restrictions.In(Projections.Property(() => aUser.Id), selectedTherapistIds.ToArray()))
                                );
                        } else {
                            qry.Where(Restrictions.In(Projections.Property(() => aUser.Id), selectedTherapistIds.ToArray()));
                        }
                        //Add date range restrictions
                        if (dtFrom.Checked) {
                            qry.Where(Restrictions.Ge(Projections.Property(() => aIT.TreatmentTime), dtFrom.Value.Date));
                        }
                        if (dtTo.Checked) {
                            qry.Where(Restrictions.Lt(Projections.Property(() => aIT.TreatmentTime), dtTo.Value.Date.AddDays(1)));
                        }

                        var lstInitTreatment = qry
                            .Take(Program.config.MaxInitialTreatmentsPerPatient)
                            .List<InitialTreatmentDTO>();


                        //Add list into SortableBindingList
                        foreach (InitialTreatmentDTO init in lstInitTreatment) {
                            initialTreatments.Add(init);
                        }

                        tr.Commit();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Show Initial Treatment List Error.", ex);
                } finally {
                    SessionFactory.TryCloseSession(key);
                }

            }

            grdInitialTreatmentList.DataSource = initialTreatments;
            setSelectedInitialTreatmentRow();

            //Manually clear the FollowUpTreatment list if no InitialTreatment list is empty
            if (grdInitialTreatmentList.Rows.Count <= 0) {
                grdFollowUpTreatmentList.DataSource = new List<FollowupTreatmentDTO>();
            }

        }

        private void setSelectedInitialTreatmentRow() {
            if(_lastEditedInitialTreatmentId != 0 && grdInitialTreatmentList.Rows.Count > 0) {

                foreach(DataGridViewRow dr in grdInitialTreatmentList.Rows) {
                    InitialTreatmentDTO it = dr.DataBoundItem as InitialTreatmentDTO;
                    if(it.Id == _lastEditedInitialTreatmentId) {
                        //Highlight selected row
                        dr.Selected = true;
                        //Fireup RowEnter event for the selected row
                        grdInitialTreatmentList_RowEnter(grdInitialTreatmentList, new DataGridViewCellEventArgs(0, dr.Index));
                        //Scroll grid to display the selected row if it's not displayed
                        if(!dr.Displayed)
                            grdInitialTreatmentList.FirstDisplayedScrollingRowIndex = dr.Index;

                        Debug.WriteLine("Set selected row to " + dr.Index);

                        break;
                    }
                }
            }
        }

        private void printInitialTreatment(int itId) {
            MessageBox.Show(this, "TO DO: Print");
        }
        #endregion

        #region Follow Up Treatment List
        private void grdFollowUpTreatmentList_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            FollowupTreatmentDTO ft = grdFollowUpTreatmentList.Rows[rowIndex].DataBoundItem as FollowupTreatmentDTO;

            string colName = grdFollowUpTreatmentList.Columns[colIndex].Name;
            switch(colName) {
                case Constants.IMAGE_BUTTON_EDIT_FOLLOWUP_TREATMENT:
                    editFollowUpTreatment(ft.Id);
                    break;
                case Constants.IMAGE_BUTTON_DELETE_FOLLOWUP_TREATMENT:
                    deleteFollowUpTreatment(ft.Id);
                    break;
                case Constants.IMAGE_BUTTON_VIEW_FOLLOWUP_TREATMENT:
                    viewFollowUpTreatment(ft.Id);
                    break;
            }
        }

        private void grdFollowUpTreatmentList_RowEnter(object sender, DataGridViewCellEventArgs e) {

        }

        private void grdFollowUpTreatmentList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            FollowupTreatmentDTO ft = grdFollowUpTreatmentList.Rows[rowIndex].DataBoundItem as FollowupTreatmentDTO;
            editFollowUpTreatment(ft.Id);
        }

        private void grdFollowUpTreatmentList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {

            foreach(DataGridViewRow dr in grdFollowUpTreatmentList.Rows) {
                FollowupTreatmentDTO ft = dr.DataBoundItem as FollowupTreatmentDTO;
                
                //Populate View Detail column
                dr.Cells[Constants.IMAGE_BUTTON_VIEW_FOLLOWUP_TREATMENT].Value = lstIcons.Images["ViewDetail"];

                //Bind Edit Button column
                dr.Cells[Constants.IMAGE_BUTTON_EDIT_FOLLOWUP_TREATMENT].Value = lstIcons.Images["Edit"];

                //Bind Delete Button column
                //dr.Cells[Constants.IMAGE_BUTTON_DELETE_FOLLOWUP_TREATMENT].Value = lstIcons.Images["Remove"];

                //Bind Complete icon column
                dr.Cells[Constants.IMAGE_ICON_FOLLOWUP_TREATMENT_COMPLETE].Value =
                    ft.IsCompleted ? lstIcons.Images["Complete"] : lstIcons.Images["Transparent"];
            }

            //Hide unimportment columns
            showHideTrackingColumns(grdFollowUpTreatmentList, false);
        }

        private void addFollowUpTreatment(int initialTreatmentId) {
            EditFollowUpTreatmentForm followUpTreatmentForm = new EditFollowUpTreatmentForm(initialTreatmentId, 0);
            if (DialogResult.OK == followUpTreatmentForm.ShowDialog(this)) {

                _lastEditedFollowUpTreatmentId = followUpTreatmentForm.EditingFollowUpTreatment.Id;

                //Refresh FollowUp Treatment list
                showFollowUpTreatmentList(initialTreatmentId);
            }
        }

        private void btnPicAddFollowUpTreatment_Click(object sender, EventArgs e) {
            if(grdInitialTreatmentList.Rows.Count > 0 && grdInitialTreatmentList.SelectedRows.Count > 0) {
                InitialTreatmentDTO it = grdInitialTreatmentList.SelectedRows[0].DataBoundItem as InitialTreatmentDTO;
                addFollowUpTreatment(it.Id);
            }
        }

        private void viewFollowUpTreatment(int ftId) {
            EditFollowUpTreatmentForm FollowUpTreatmentForm = new EditFollowUpTreatmentForm(0, ftId, true);
            FollowUpTreatmentForm.ShowDialog(this);
        }

        private void deleteFollowUpTreatment(int ftId) {
            if(DialogResult.Yes == MessageBox.Show(this, "Do you want to DELETE the Follow-Up Treatment?",
                "Delete Follow-Up Treatment",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)) {

                //Create key for session access
                Guid key = Guid.NewGuid();

                try {
                    //Get session
                    ISession session = SessionFactory.GetOpenSession(key);

                    using (ITransaction tr = session.BeginTransaction()) {
                        FollowUpTreatment ftToDelete = session.Get<FollowUpTreatment>(ftId);
                        //backup last editing initial treatment
                        _lastEditedInitialTreatmentId = ftToDelete.InitialTreatment.Id;

                        JCService service = new JCService(session, Program.LogonUser);
                        service.DeleteFollowUpTreatment(ftToDelete);

                        //Commit transaction
                        tr.Commit();

                        _lastEditedFollowUpTreatmentId = 0;
                        showFollowUpTreatmentList(_lastEditedInitialTreatmentId);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to delete Follow-Up Treatment. " + ex.Message);
                    Program.log.Error("Failed to delete Follow-Up Treatment.", ex);
                } finally {
                    SessionFactory.TryCloseSession(key);
                }
            }
        }

        private void editFollowUpTreatment(int ftId) {
            EditFollowUpTreatmentForm FollowUpTreatmentForm = new EditFollowUpTreatmentForm(0, ftId);
            if(DialogResult.OK == FollowUpTreatmentForm.ShowDialog(this)) {

                _lastEditedFollowUpTreatmentId = FollowUpTreatmentForm.EditingFollowUpTreatment.Id;

                //Refresh FollowUp Treatment list
                showFollowUpTreatmentList(FollowUpTreatmentForm.EditingFollowUpTreatment.InitialTreatment.Id);
            }
        }

        private void showFollowUpTreatmentList(int treatmentId) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            //Clear list first
            grdFollowUpTreatmentList.DataSource = new List<FollowUpTreatment>();

            if(Program.LogonUser.UserType == UserType.None) return;

            //Get selected therapist ids
            IList<int> selectedTherapistIds = new List<int>();
            foreach (object item in ucTherapist.CheckedItems) {
                if (item is SimpleUserItem) {
                    selectedTherapistIds.Add((item as SimpleUserItem).Id);
                }
            }

            //Add list to SortableBindingList
            SortableBindingList<FollowupTreatmentDTO> followUps = new SortableBindingList<FollowupTreatmentDTO>();

            if (treatmentId != 0) {
                try {
                    //Get session
                    ISession session = SessionFactory.GetOpenSession(key);

                    using (ITransaction tr = session.BeginTransaction()) {

                        #region Rewrote below
                        /*
                        var qry = session.CreateCriteria<FollowUpTreatment>("f")
                            .CreateCriteria("f.InitialTreatment", "it")
                            .CreateCriteria("it.TreatmentType", "tt")
                            .CreateCriteria("it.Patient", "p")
                            .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .CreateCriteria("f.Therapist", "t")
                            .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .Add(Restrictions.Eq("it.Id", treatmentId));

                        //Add therapist restrictions
                        if (ucTherapist.AllChecked) {//No restrictions
                        } else if (ucTherapist.AllNonBlanks) {//Exclude nulls
                            qry.Add(Restrictions.IsNotNull("t.Id"));
                        } else if (ucTherapist.BlanksOnly) {//Only nulls
                            qry.Add(Restrictions.IsNull("t.Id"));
                        } else if (ucTherapist.NoneChecked) {//Select none
                            qry.Add(Restrictions.IsNull("f.Id"));//Always false
                        } else if (ucTherapist.IsBlankChecked) {//Include nulls
                            qry.Add(Restrictions.Disjunction()
                                .Add(Restrictions.IsNull("t.Id"))
                                .Add(Restrictions.In("t.Id", selectedTherapistIds.ToArray()))
                                );
                        } else {
                            qry.Add(Restrictions.In("t.Id", selectedTherapistIds.ToArray()));
                        }
                        //Add date range restrictions
                        if (dtFrom.Checked) {
                            qry.Add(Restrictions.Ge("f.TreatmentTime", dtFrom.Value.Date));
                        }
                        if (dtTo.Checked) {
                            qry.Add(Restrictions.Lt("f.TreatmentTime", dtTo.Value.Date.AddDays(1)));
                        }

                        var lstFollowUpTreatment = qry
                            .SetMaxResults(Program.config.MaxFollowUpsPerTreatment)
                            .List<FollowUpTreatment>();

                        tr.Commit();

                        //Add list to SortableBindingList
                        SortableBindingList<FollowUpTreatment> followUps = new SortableBindingList<FollowUpTreatment>();
                        foreach (FollowUpTreatment followUp in lstFollowUpTreatment) {
                            followUps.Add(followUp);
                        }
                        */
                        #endregion Rewrote below

                        FollowUpTreatment aFT = null;
                        InitialTreatment aIT = null;
                        User aUser = null;
                        UserTitle aTitle = null;
                        FollowupTreatmentDTO dtoFT = null;

                        var qry = session.QueryOver<FollowUpTreatment>(() => aFT)
                            .JoinQueryOver<InitialTreatment>(() => aFT.InitialTreatment, () => aIT, JoinType.InnerJoin)
                            .JoinQueryOver<User>(() => aFT.Therapist, () => aUser, JoinType.InnerJoin)
                            .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                            .SelectList(list => list
                                .Select(() => aFT.Id).WithAlias(() => dtoFT.Id)
                                .Select(() => aFT.Version).WithAlias(() => dtoFT.Version)
                                .Select(() => aIT.Id).WithAlias(() => dtoFT.InitialTreatmentId)
                                .Select(() => aUser.FirstName).WithAlias(() => dtoFT.TherapistFirstName)
                                .Select(() => aUser.LastName).WithAlias(() => dtoFT.TherapistLastName)
                                .Select(() => aUser.MiddleName).WithAlias(() => dtoFT.TherapistMiddleName)
                                .Select(() => aTitle.Name).WithAlias(() => dtoFT.TherapistTitle)
                                .Select(() => aFT.TreatmentTime).WithAlias(() => dtoFT.TreatmentTime)
                                .Select(() => aFT.TreatmentDurationMinutes).WithAlias(() => dtoFT.Duration)
                                .Select(() => aFT.Note).WithAlias(() => dtoFT.Note)
                                .Select(() => aFT.OpenToTherapist).WithAlias(() => dtoFT.IsOpenToTherapist)
                                .Select(() => aFT.IsComplete).WithAlias(() => dtoFT.IsCompleted)
                            )
                            .TransformUsing(Transformers.AliasToBean<FollowupTreatmentDTO>())
                            .Where(() => aFT.InitialTreatment.Id == treatmentId);

                        //Add therapist restrictions
                        if (ucTherapist.AllChecked) {//No restrictions
                        } else if (ucTherapist.AllNonBlanks) {//Exclude nulls
                            qry.Where(Restrictions.IsNotNull(Projections.Property(() => aUser.Id)));
                        } else if (ucTherapist.BlanksOnly) {//Only nulls
                            qry.Where(Restrictions.IsNull(Projections.Property(() => aUser.Id)));
                        } else if (ucTherapist.NoneChecked) {//Select none
                            qry.Where(Restrictions.IsNull(Projections.Property(() => aFT.Id)));//Always false
                        } else if (ucTherapist.IsBlankChecked) {//Include nulls
                            qry.Where(Restrictions.Disjunction()
                                .Add(Restrictions.IsNull(Projections.Property(() => aUser.Id)))
                                .Add(Restrictions.In(Projections.Property(() => aUser.Id), selectedTherapistIds.ToArray()))
                                );
                        } else {
                            qry.Where(Restrictions.In(Projections.Property(() => aUser.Id), selectedTherapistIds.ToArray()));
                        }
                        //Add date range restrictions
                        if (dtFrom.Checked) {
                            qry.Where(Restrictions.Ge(Projections.Property(() => aFT.TreatmentTime), dtFrom.Value.Date));
                        }
                        if (dtTo.Checked) {
                            qry.Where(Restrictions.Lt(Projections.Property(() => aFT.TreatmentTime), dtTo.Value.Date.AddDays(1)));
                        }

                        var lstFollowUpTreatment = qry
                            .Take(Program.config.MaxFollowUpsPerTreatment)
                            .List<FollowupTreatmentDTO>();

                        //Add list to SortableBindingList
                        foreach (FollowupTreatmentDTO followUp in lstFollowUpTreatment) {
                            followUps.Add(followUp);
                        }

                        tr.Commit();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Show Follow Up Treatment List Error.", ex);
                } finally {
                    SessionFactory.TryCloseSession(key);
                }

                grdFollowUpTreatmentList.DataSource = followUps;// lstFollowUpTreatment;
                setSelectedFollowUpTreatmentRow();

            }
        }

        private void setSelectedFollowUpTreatmentRow() {
            if(_lastEditedFollowUpTreatmentId != 0 && grdFollowUpTreatmentList.Rows.Count > 0) {

                foreach(DataGridViewRow dr in grdFollowUpTreatmentList.Rows) {
                    FollowupTreatmentDTO ft = dr.DataBoundItem as FollowupTreatmentDTO;
                    if(ft.Id == _lastEditedFollowUpTreatmentId) {
                        //Highlight selected row
                        dr.Selected = true;
                        //Fireup RowEnter event for the selected row
                        grdFollowUpTreatmentList_RowEnter(grdFollowUpTreatmentList, new DataGridViewCellEventArgs(0, dr.Index));
                        //Scroll grid to display the selected row if it's not displayed
                        if(!dr.Displayed)
                            grdFollowUpTreatmentList.FirstDisplayedScrollingRowIndex = dr.Index;

                        Debug.WriteLine("Set selected row to " + dr.Index);

                        break;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Print all follow up treatment notes(Open in Word)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintAllSOAPNotes_Click(object sender, EventArgs e) {

            if (grdFollowUpTreatmentList.Rows.Count <= 0) {
                MessageBox.Show("There is no followup treatments to print.");
                return;
            }

            #region Decide to print ALL or treatments within a date time range

            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get the selected Initial treatment
            FollowupTreatmentDTO selectedFT = grdFollowUpTreatmentList.Rows[0].DataBoundItem as FollowupTreatmentDTO;
            int itId = selectedFT.InitialTreatmentId;

            //Prepare DateTimeRange array
            DateTime[] dateTimeRange = new DateTime[2];

            try {
                ISession session = SessionFactory.GetOpenSession(key);
                using (ITransaction tr = session.BeginTransaction()) {

                    //Get the followup treatment date time range for the selected initial treatment
                    var fromDateTime = session.QueryOver<FollowUpTreatment>()
                        .Where(x => x.InitialTreatment.Id == itId)
                        .Select(
                            Projections.Min<FollowUpTreatment>(f => f.TreatmentTime))
                        .FutureValue<DateTime>();
                    DateTime toDateTime = session.QueryOver<FollowUpTreatment>()
                        .Where(x => x.InitialTreatment.Id == itId)
                        .Select(
                            Projections.Max<FollowUpTreatment>(f => f.TreatmentTime))
                        .SingleOrDefault<DateTime>();
                    
                    //Display dialog window and get user selection
                    PrintRangeForm frmPrintRange = new PrintRangeForm();
                    frmPrintRange.FromDateTime = fromDateTime.Value;
                    frmPrintRange.ToDateTime = toDateTime;
                    if (DialogResult.Cancel == frmPrintRange.ShowDialog()) {
                        return;
                    }
                    if (frmPrintRange.ToPrintAll) {
                        dateTimeRange = null;
                    } else {
                        dateTimeRange[0] = frmPrintRange.FromDateTime;
                        dateTimeRange[1] = frmPrintRange.ToDateTime;
                    }

                    tr.Commit();
                }

            } catch (Exception ex) {
                MessageBox.Show(this, ex.Message);
                Program.log.Error("Failed to retrieve the followup treament date time range.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
            #endregion Decide to print ALL or treatments within a date time range
            
            //Start the printing process
            _printProcessCompleted = false;
            worker.RunWorkerAsync(dateTimeRange);

            //Show waiting form
            if(_waitForm == null) {
                _waitForm = new PleaseWaitForm();
                _waitForm.ProcessCheckHandler += new EventHandler(_waitForm_ProcessCheckHandler);
            }
            _waitForm.ShowDialog(this);
        }

        void _waitForm_ProcessCheckHandler(object sender, EventArgs e) {
            if (_printProcessCompleted) {
                if (_waitForm != null) {
                    _waitForm.Close();
                    _waitForm.Dispose();
                    _waitForm = null;
                }
            }
        }

        private void printAllSOAPNotes(DateTime[] dateTimeRange) {
            if(grdFollowUpTreatmentList.DataSource == null ||
                grdFollowUpTreatmentList.Rows.Count <= 0) {
                //Nothing to print
                return;
            }

            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get the selected Initial treatment
            FollowupTreatmentDTO selectedFT = grdFollowUpTreatmentList.Rows[0].DataBoundItem as FollowupTreatmentDTO;
            int itId = selectedFT.InitialTreatmentId;

            //Get treatment type
            string treatmentTypeName = "";

            IList<FollowUpTreatment> lstSOAP = new List<FollowUpTreatment>();

            ISession session = SessionFactory.GetOpenSession(key);
            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    InitialTreatment it = session.Get<InitialTreatment>(itId);
                    treatmentTypeName = it.TreatmentType.TherapyTypeName;

                    if (dateTimeRange == null || dateTimeRange.Length < 2) {
                        lstSOAP = session.CreateCriteria<FollowUpTreatment>("f")
                            .CreateCriteria("f.InitialTreatment", "it")
                            .CreateCriteria("it.TreatmentType", "tt")
                            .CreateCriteria("it.Patient", "p")
                            .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .CreateCriteria("f.Therapist", "t")
                            .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .Add(Restrictions.Eq("it.Id", itId))
                            .AddOrder(new Order("f.TreatmentTime", true))
                            .SetMaxResults(Program.config.MaxFollowUpsPerTreatment)
                            .List<FollowUpTreatment>();
                    } else {
                        lstSOAP = session.CreateCriteria<FollowUpTreatment>("f")
                            .CreateCriteria("f.InitialTreatment", "it")
                            .CreateCriteria("it.TreatmentType", "tt")
                            .CreateCriteria("it.Patient", "p")
                            .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .CreateCriteria("f.Therapist", "t")
                            .CreateCriteria("t.Title", "title", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                            .Add(Restrictions.Eq("it.Id", itId))
                            .Add(Restrictions.Le("f.TreatmentTime", dateTimeRange[1]))
                            .Add(Restrictions.Ge("f.TreatmentTime", dateTimeRange[0]))
                            .AddOrder(new Order("f.TreatmentTime", true))
                            .SetMaxResults(Program.config.MaxFollowUpsPerTreatment)
                            .List<FollowUpTreatment>();
                    }

                    //Get Treatment Note Title setting
                    JCService service = new JCService(session, Program.LogonUser);
                    SystemSetting treatmentNoteTitleSetting = service.GetSettingBy("Treatment Note Title");
                    if (treatmentNoteTitleSetting != null) {
                        _treatmentNoteTitle = treatmentNoteTitleSetting.Value;
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                ShowMessage("Failed to retrieve SOAP notes." + ex.Message);
                Program.log.Error("Failed to retrieve SOAP notes.", ex);
                return;
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            try {
                //Go through all Follow Up Treatments
                IList<string> lstWordFiles = new List<string>();
                foreach (FollowUpTreatment soap in lstSOAP) {

                    WordExportHelper wordExporter = null;

                    if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower())) {
                        wordExporter = new WordExportHelper(
                            soap,
                            Constants.CFG_TEMPLATE_FILE_CHIROPRACTIC_SOAP,
                            new PopulateExportRequestDelegate(buildChiropracticSOAPRequest),
                            new ProgressReporterDelegate(wordProgressReporter));
                    } else if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {
                        wordExporter = new WordExportHelper(
                            soap,
                            Constants.CFG_TEMPLATE_FILE_OSTEOPATHY_SOAP,
                            new PopulateExportRequestDelegate(buildOsteopathySOAPRequest),
                            new ProgressReporterDelegate(wordProgressReporter));
                    } else if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.Trim().ToLower())
                        || treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.Trim().ToLower())
                        ) {
                        wordExporter = new WordExportHelper(
                            soap,
                            Constants.CFG_TEMPLATE_FILE_GENERAL_SOAP,
                            new PopulateExportRequestDelegate(buildGeneralSOAPRequest),
                            new ProgressReporterDelegate(wordProgressReporter));
                    } else if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {
                        wordExporter = new WordExportHelper(
                            soap,
                            Constants.CFG_TEMPLATE_FILE_MASSAGE_SOAP,
                            new PopulateExportRequestDelegate(buildMassageSOAPRequest),
                            new ProgressReporterDelegate(wordProgressReporter));
                    } else if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {
                        wordExporter = new WordExportHelper(
                            soap,
                            Constants.CFG_TEMPLATE_FILE_ACUPUNCTURE_SOAP,
                            new PopulateExportRequestDelegate(buildAcupunctureSOAPRequest),
                            new ProgressReporterDelegate(wordProgressReporter));
                    } else {
                        throw new ApplicationException("This treatment type " + treatmentTypeName + " has not been implemented yet in btnPrintAllSOAPNotes_Click() function.");
                    }

                    wordExporter.Export();
                    if (wordExporter.HasError) {
                        MessageBox.Show(this, wordExporter.ErrorMessage);
                        return;
                    } else {
                        lstWordFiles.Add(wordExporter.WordExported);
                    }
                }

                //Combine all Word files into one and Open
                bool addPageBreak = false;
                if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {
                    addPageBreak = true;
                }
                if (lstWordFiles.Count >= 1) {
                    RequestProcessor processor = new RequestProcessor(Program.log);
                    //processor.CombineWordFiles(lstWordFiles.ToArray());
                    processor.OpenXMLCombineWordFiles(true, addPageBreak, lstWordFiles.ToArray());
                    processor.OpenWordFile(lstWordFiles[0]);
                }
            } catch (Exception ex) {
                ShowMessage("Failed to export SOAP notes to Word." + ex.Message);
                Program.log.Error("Failed to export SOAP notes to Word.", ex);
            }
        }

        private void buildAcupunctureSOAPRequest(ProcessRequest request, object obj) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            FollowUpTreatment soap = obj as FollowUpTreatment;
            AcupunctureFollowUpDetail detail = null;

            #region Diagram
            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    //Create JCService
                    JCService service = new JCService(session, Program.LogonUser);

                    //Get FollowUpTreatmentDetail
                    detail = service.GetFollowUpTreatmentDetail(soap) as AcupunctureFollowUpDetail;

                    if (detail == null) {
                        throw new ApplicationException("Cannot find Acupuncture SOAP detail.");
                    }

                    //Get Diagram and Points
                    if (detail.TongueDiagram != null) {
                        //Get Diagram Image
                        Diagram dg = detail.TongueDiagram;
                        MemoryStream stream = new MemoryStream(dg.Image);
                        Image img = Image.FromStream(stream);
                        Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                        using (Graphics g = Graphics.FromImage(imgResized)) {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                        }
                        stream.Close();
                        //Draw Points onto diagram image
                        IList<PointOnDiagram> points = service.GetAcupunctureFollowUpTonguePoints(detail, Program.config.MaxPointsPerDiagram);
                        using (Graphics g = Graphics.FromImage(imgResized)) {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            //Draw points
                            foreach (PointOnDiagram p in points) {
                                int deltaX = Program.PinPointImages[p.PainKey].Width / 2;
                                int deltaY = Program.PinPointImages[p.PainKey].Height / 2;
                                g.DrawImage(Program.PinPointImages[p.PainKey], p.X - deltaX, p.Y - deltaY);
                            }
                        }

                        //Diagram
                        List<PictureReplacement> picDiagram = new List<PictureReplacement>();
                        string diagramImagePath = Program.CreateImageFile(imgResized);
                        picDiagram.Add(new PictureReplacement(diagramImagePath));
                        request.Replacements.Add("{diagram}", picDiagram);
                    }

                    //Commit transaction
                    tr.Commit();
                }
            } catch (Exception ex) {
                ShowMessage("Failed to retrieve Chiropractic SOAP detail. " + ex.Message);
                Program.log.Error("Failed to retrieve Chiropractic SOAP detail.", ex);
            }
            #endregion Diagram

            #region Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(soap.Therapist.SignatureImage);
            if (signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                request.Replacements.Add("{signature}", picSignature);
            } else {
                request.AddWordReplacement("{signature}", "");
            }
            #endregion Signature

            #region Other Text
            //0) Template Title
            request.AddWordReplacement("{treatment note title}", _treatmentNoteTitle);

            Patient patient = soap.InitialTreatment.Patient;
            //0.1) Patient name
            //request.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            request.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            request.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            request.AddWordReplacement("{file number}", patient.FileNumber);

            //1) Date/duration & Therapist
            request.AddWordReplacement("{date}", soap.TreatmentTime.ToString(Constants.DATE_FORMAT));
            request.AddWordReplacement("{datetime}", soap.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            request.AddWordReplacement("{time_from}", soap.TreatmentTime.ToString(Constants.TIME_FORMAT));
            request.AddWordReplacement("{time_to}", soap.TreatmentTime.AddMinutes(soap.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            request.AddWordReplacement("{duration}", soap.TreatmentDurationMinutes.ToString());
            request.AddWordReplacement("{therapist}", soap.Therapist.DisplayName);

            //Header
            request.AddWordReplacement("{patient name}", soap.InitialTreatment.Patient.DisplayName);
            request.AddWordReplacement("{treatment type}", soap.InitialTreatment.TreatmentType.DisplayName);

            //2) Tongue
            request.AddWordReplacement("{tongue}", detail.Tongue);

            //3) Pulse
            request.AddWordReplacement("{pulse}", detail.Pulse);

            //4) Blood pressure
            request.AddWordReplacement("{blood pressure}", detail.BloodPressure);

            //5) Blood sugar
            request.AddWordReplacement("{blood sugar}", detail.BloodSugar);

            //6) Subjective
            request.AddWordReplacement("{subjective}", detail.Subjective);

            //7) TCM diagnosis
            request.AddWordReplacement("{tcm diagnosis}", detail.TCMDiagnosis);

            //8) Treatment plan
            request.AddWordReplacement("{treatment plan}", detail.TreatmentPlan);

            #endregion Other Text
        }

        private void buildChiropracticSOAPRequest(ProcessRequest request, object obj) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            FollowUpTreatment soap = obj as FollowUpTreatment;
            ChiropracticFollowUpDetail detail = null;

            #region Diagram
            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using(ITransaction tr = session.BeginTransaction()){

                    //Create JCService
                    JCService service = new JCService(session, Program.LogonUser);

                    //Get FollowUpTreatmentDetail
                    detail = service.GetFollowUpTreatmentDetail(soap) as ChiropracticFollowUpDetail;

                    if(detail == null) {
                        throw new ApplicationException("Cannot find Chiropractic SOAP detail.");
                    }

                    //Get Diagram and Points
                    if(detail.ChiropracticSOAPDiagram != null){
                        //Get Diagram Image
                        Diagram dg = detail.ChiropracticSOAPDiagram;
                        MemoryStream stream = new MemoryStream(dg.Image);
                        Image img = Image.FromStream(stream);
                        Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                        using(Graphics g = Graphics.FromImage(imgResized)) {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                        }
                        stream.Close();
                        //Draw Points onto diagram image
                        IList<PointOnDiagram> points = service.GetChiropracticSOAPPoints(detail, Program.config.MaxPointsPerDiagram);
                        using(Graphics g = Graphics.FromImage(imgResized)) {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            //Draw points
                            foreach(PointOnDiagram p in points){
                                int deltaX = Program.PinPointImages[p.PainKey].Width / 2;
                                int deltaY = Program.PinPointImages[p.PainKey].Height / 2;
                                g.DrawImage(Program.PinPointImages[p.PainKey], p.X - deltaX, p.Y - deltaY);
                            }
                        }

                        //Diagram
                        List<PictureReplacement> picDiagram = new List<PictureReplacement>();
                        string diagramImagePath = Program.CreateImageFile(imgResized);
                        picDiagram.Add(new PictureReplacement(diagramImagePath));
                        request.Replacements.Add("{diagram}", picDiagram);
                    }

                    //Commit transaction
                    tr.Commit();
                }
            }catch(Exception ex){
                ShowMessage("Failed to retrieve Chiropractic SOAP detail. " + ex.Message);
                Program.log.Error("Failed to retrieve Chiropractic SOAP detail.", ex);
            }
            #endregion Diagram

            #region Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(soap.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                request.Replacements.Add("{signature}", picSignature);
            } else {
                request.AddWordReplacement("{signature}", "");
            }
            #endregion Signature

            #region Checkboxes
            //Load check/uncheck image
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckMarkRed.png");
            Image imgCheck = Image.FromStream(s);
            string checkImagePath = Program.CreateImageFile(imgCheck);
            s.Close();
            s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckBoxEmpty.png");
            Image imgUncheck = Image.FromStream(s);
            string uncheckImagePath = Program.CreateImageFile(imgUncheck);
            s.Close();

            //Check picture replacement
            List<PictureReplacement> checkReplacements = new List<PictureReplacement>();
            checkReplacements.Add(new PictureReplacement(checkImagePath));

            //Uncheck replacement
            List<PictureReplacement> uncheckReplacements = new List<PictureReplacement>();
            uncheckReplacements.Add(new PictureReplacement(uncheckImagePath));

            //1) VAS improving
            request.Replacements.Add("{ck1}", 
                (detail.ConditionChange == ChiropracticSOAPConditionChanges.Improving ?
                checkReplacements : uncheckReplacements));

            //2) VAS no change
            request.Replacements.Add("{ck2}",
                (detail.ConditionChange == ChiropracticSOAPConditionChanges.NoChange ?
                checkReplacements : uncheckReplacements));

            //3) VAS worsening
            request.Replacements.Add("{ck3}",
                (detail.ConditionChange == ChiropracticSOAPConditionChanges.Worsening ?
                checkReplacements : uncheckReplacements));

            //4) VAS aggravation
            request.Replacements.Add("{ck4}",
                (detail.ConditionChange == ChiropracticSOAPConditionChanges.Aggravation ?
                checkReplacements : uncheckReplacements));

            //5) VAS new condition
            request.Replacements.Add("{ck5}",
                (detail.ConditionChange == ChiropracticSOAPConditionChanges.NewCondition ?
                checkReplacements : uncheckReplacements));

            //6) Osseous manipulation
            request.Replacements.Add("{ck6}",
                (detail.OsseousManipulation ? checkReplacements : uncheckReplacements));

            //7) massage
            request.Replacements.Add("{ck7}",
                (detail.Massage ? checkReplacements : uncheckReplacements));

            //8) stretch
            request.Replacements.Add("{ck8}",
                (detail.Stretch ? checkReplacements : uncheckReplacements));

            //9) trigger point therapy
            request.Replacements.Add("{ck9}",
                (detail.TriggerPointTherapy ? checkReplacements : uncheckReplacements));

            //10) traction
            request.Replacements.Add("{ck10}",
                (detail.Traction ? checkReplacements : uncheckReplacements));

            //11) theraputic exercise
            request.Replacements.Add("{ck11}",
                (detail.TheraputicExercise ? checkReplacements : uncheckReplacements));

            //12) heat
            request.Replacements.Add("{ck12}",
                (detail.Heat ? checkReplacements : uncheckReplacements));

            //13) cold
            request.Replacements.Add("{ck13}",
                (detail.Cold ? checkReplacements : uncheckReplacements));

            //14) ultrasound
            request.Replacements.Add("{ck14}",
                (detail.UltraSound ? checkReplacements : uncheckReplacements));

            //15) electrotherapy
            request.Replacements.Add("{ck15}",
                (detail.Electrotherapy ? checkReplacements : uncheckReplacements));
            
            //16) no dx change
            request.Replacements.Add("{ck16}",
                (detail.NoDxChange ? checkReplacements : uncheckReplacements));

            //17) new diagnosis
            request.Replacements.Add("{ck17}",
                (detail.NewDiagnosis ? checkReplacements : uncheckReplacements));

            //18) PRN
            request.Replacements.Add("{ck18}",
                (detail.PRN ? checkReplacements : uncheckReplacements));

            //19) referral
            request.Replacements.Add("{ck19}",
                (detail.Referral ? checkReplacements : uncheckReplacements));
            #endregion Checkboxes

            #region Numbers
            //1) VAS  n/10
            request.AddWordReplacement("{n1}", detail.VAS.ToString());

            //2) Ultrasound
            request.AddWordReplacement("{n2}", detail.UltraSoundValue.ToString());

            //3) PTR days
            request.AddWordReplacement("{n3}", detail.PTRDays.ToString());

            //4) PTR days
            request.AddWordReplacement("{n4}", detail.PTRWeeks.ToString());

            //5) PTR days
            request.AddWordReplacement("{n5}", detail.PTRMonths.ToString());
            #endregion Numbers

            #region Other Text

            //0) Template Title
            request.AddWordReplacement("{treatment note title}", _treatmentNoteTitle);


            Patient patient = soap.InitialTreatment.Patient;
            //0.1) Patient name
            //request.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            request.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            request.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            request.AddWordReplacement("{file number}", patient.FileNumber);

            //1) Date/duration & Therapist
            request.AddWordReplacement("{date}", soap.TreatmentTime.ToString(Constants.DATE_FORMAT));
            request.AddWordReplacement("{datetime}", soap.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            request.AddWordReplacement("{time_from}", soap.TreatmentTime.ToString(Constants.TIME_FORMAT));
            request.AddWordReplacement("{time_to}", soap.TreatmentTime.AddMinutes(soap.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            request.AddWordReplacement("{duration}", soap.TreatmentDurationMinutes.ToString());
            request.AddWordReplacement("{therapist}", soap.Therapist.DisplayName);

            //Header
            request.AddWordReplacement("{patient name}", soap.InitialTreatment.Patient.DisplayName);
            request.AddWordReplacement("{treatment type}", soap.InitialTreatment.TreatmentType.DisplayName);

            //2) new condition
            request.AddWordReplacement("{new condition}", detail.ConditionChangeNote);

            //3) O
            request.AddWordReplacement("{o}", detail.O);

            //4) treatment note
            request.AddWordReplacement("{treatment note}", soap.Note);

            //5) exercise note
            request.AddWordReplacement("{exercise note}", detail.ExerciseNote);

            //6) electrotherapy note
            request.AddWordReplacement("{electrotherapy note}", detail.ElectrotherapyNote);

            //7) new diagnosis
            request.AddWordReplacement("{new diagnosis}", detail.NewDiagnosisNote);

            //8) referral
            request.AddWordReplacement("{referral}", detail.ReferralNote);

            //9) home care
            request.AddWordReplacement("{home care}", detail.HomeCare);

            //10) comments
            request.AddWordReplacement("{comments}", detail.Comments);

            #endregion Other Text
        }

        private void buildMassageSOAPRequest(ProcessRequest req, object obj) {
            FollowUpTreatment soap = obj as FollowUpTreatment;
            MassageFollowUpDetail detail = null;

            //Create key for session access
            Guid key = Guid.NewGuid();

            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    //Create JCService
                    JCService service = new JCService(session, Program.LogonUser);

                    //Get FollowUpTreatmentDetail
                    detail = service.GetFollowUpTreatmentDetail(soap) as MassageFollowUpDetail;
                    if (detail == null) {
                        throw new ApplicationException("Cannot find Massage SOAP detail.");
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                ShowMessage("Failed to retrieve Massage SOAP detail. " + ex.Message);
                Program.log.Error("Failed to retrieve Massage SOAP detail.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            //Load checkbox images and replacements
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckMarkRed.png");
            Image imgCheck = Image.FromStream(s);
            string checkImagePath = Program.CreateImageFile(imgCheck);
            s.Close();
            s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckBoxEmpty.png");
            Image imgCheckEmpty = Image.FromStream(s);
            string checkEmptyImagePath = Program.CreateImageFile(imgCheckEmpty);
            s.Close();

            List<PictureReplacement> checkReplacements = new List<PictureReplacement>();
            checkReplacements.Add(new PictureReplacement(checkImagePath));

            List<PictureReplacement> uncheckReplacements = new List<PictureReplacement>();
            uncheckReplacements.Add(new PictureReplacement(checkEmptyImagePath));


            //0) Template Title
            req.AddWordReplacement("{treatment note title}", _treatmentNoteTitle);

            Patient patient = soap.InitialTreatment.Patient;
            //0.1) Patient name
            //req.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            req.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            req.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            req.AddWordReplacement("{file number}", patient.FileNumber);

            //1) Text fields and checkboxes
            req.AddWordReplacement("{date}", soap.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", soap.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", soap.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", soap.TreatmentTime.AddMinutes(soap.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", soap.TreatmentDurationMinutes.ToString());

            req.Replacements.Add("{chkStroking}", detail.TreatmentUsedStroking ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkRocking}", detail.TreatmentUsedRocking ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkEffleurage}", detail.TreatmentUsedEffleurage ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkPetrissage}", detail.TreatmentUsedPetrissage ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkFriction}", detail.TreatmentUsedFriction ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkVibration}", detail.TreatmentUsedVibration ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkTapotement}", detail.TreatmentUsedTapotement ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkFacial}", detail.TreatmentUsedFacial ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkMyo}", detail.TreatmentUsedMyoFacialTriggerPoint ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkHigh}", detail.TreatmentUsedHighGradeJointMobilization ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkLow}", detail.TreatmentUsedLowGradeJointMobilization ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkStretch}", detail.TreatmentUsedStretch ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkIntraOral}", detail.TreatmentUsedIntraOral ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkBreastMassage}", detail.TreatmentUsedBreastMassage ? checkReplacements : uncheckReplacements);

            req.AddWordReplacement("{other techniques}", detail.TreatmentUsedOther);
            req.AddWordReplacement("{note}", detail.TreatmentNote);

            req.Replacements.Add("{chkBack}", detail.TreatmentAreaBack ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkNeck}", detail.TreatmentAreaNeck ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkShoulders}", detail.TreatmentAreaShoulders ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkFace}", detail.TreatmentAreaFace ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkLA}", detail.TreatmentAreaLeftArm ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkRA}", detail.TreatmentAreaRightArm ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkLL}", detail.TreatmentAreaLeftLeg ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkRL}", detail.TreatmentAreaRightLeg ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkGluteus}", detail.TreatmentAreaGluteus ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkAbdominals}", detail.TreatmentAreaAbdominals ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkChest}", detail.TreatmentAreaChest ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkBreast}", detail.TreatmentAreaBreast ? checkReplacements : uncheckReplacements);

            req.AddWordReplacement("{other areas treated}", detail.TreatmentAreaOther);

            //2) Therapist
            req.AddWordReplacement("{therapist}", soap.Therapist.ToString());

            //Header
            req.AddWordReplacement("{patient name}", soap.InitialTreatment.Patient.DisplayName);
            req.AddWordReplacement("{treatment type}", soap.InitialTreatment.TreatmentType.DisplayName);

            //3) Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(soap.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }

        }

        private void buildOsteopathySOAPRequest(ProcessRequest req, object obj) {
            FollowUpTreatment soap = obj as FollowUpTreatment;
            OsteopathyFollowUpDetail detail = null;

            //Create key for session access
            Guid key = Guid.NewGuid();

            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    //Create JCService
                    JCService service = new JCService(session, Program.LogonUser);

                    //Get FollowUpTreatmentDetail
                    detail = service.GetFollowUpTreatmentDetail(soap) as OsteopathyFollowUpDetail;
                    if (detail == null) {
                        throw new ApplicationException("Cannot find Osteopathy SOAP detail.");
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                ShowMessage("Failed to retrieve Osteopathy SOAP detail. " + ex.Message);
                Program.log.Error("Failed to retrieve Osteopathy SOAP detail.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            //Load checkbox images and replacements
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckMarkRed.png");
            Image imgCheck = Image.FromStream(s);
            string checkImagePath = Program.CreateImageFile(imgCheck);
            s.Close();
            s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckBoxEmpty.png");
            Image imgCheckEmpty = Image.FromStream(s);
            string checkEmptyImagePath = Program.CreateImageFile(imgCheckEmpty);
            s.Close();

            List<PictureReplacement> checkReplacements = new List<PictureReplacement>();
            checkReplacements.Add(new PictureReplacement(checkImagePath));

            List<PictureReplacement> uncheckReplacements = new List<PictureReplacement>();
            uncheckReplacements.Add(new PictureReplacement(checkEmptyImagePath));


            //0) Template Title
            req.AddWordReplacement("{treatment note title}", _treatmentNoteTitle);

            Patient patient = soap.InitialTreatment.Patient;
            //0.1) Patient name
            //req.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            req.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            req.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            req.AddWordReplacement("{file number}", patient.FileNumber);

            //1) Text fields and checkboxes
            req.AddWordReplacement("{date}", soap.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", soap.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", soap.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", soap.TreatmentTime.AddMinutes(soap.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", soap.TreatmentDurationMinutes.ToString());

            req.Replacements.Add("{chkRocking}", detail.TreatmentUsedRocking ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkPetrissage}", detail.TreatmentUsedPetrissage ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkFriction}", detail.TreatmentUsedFriction ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkVibration}", detail.TreatmentUsedVibration ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkTapotement}", detail.TreatmentUsedTapotement ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkMyofacialRelease}", detail.TreatmentUsedMyofacialRelease ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkTenderPointRelease}", detail.TreatmentUsedTenderPointRelease ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkMuscleEnergy}", detail.TreatmentUsedMuscleEnergy ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkTotalBodyAdjustment}", detail.TreatmentUsedTotalBodyAdjustment ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkStillTechnique}", detail.TreatmentUsedStillTechnique ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCraniosacral}", detail.TreatmentUsedCraniosacral ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkVisceral}", detail.TreatmentUsedVisceral ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkSoftTissueJointMobilization}", detail.TreatmentUsedSoftTissueJointMobilization ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkStretch}", detail.TreatmentUsedStretch ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkIntraOral}", detail.TreatmentUsedIntraOral ? checkReplacements : uncheckReplacements);

            req.AddWordReplacement("{other techniques}", detail.TreatmentUsedOther);
            req.AddWordReplacement("{note}", detail.TreatmentNote);

            req.Replacements.Add("{chkBack}", detail.TreatmentAreaBack ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkNeck}", detail.TreatmentAreaNeck ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkShoulders}", detail.TreatmentAreaShoulders ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkFace}", detail.TreatmentAreaFace ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkLA}", detail.TreatmentAreaLeftArm ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkRA}", detail.TreatmentAreaRightArm ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkLL}", detail.TreatmentAreaLeftLeg ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkRL}", detail.TreatmentAreaRightLeg ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkGluteus}", detail.TreatmentAreaGluteus ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkAbdominals}", detail.TreatmentAreaAbdominals ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkChest}", detail.TreatmentAreaChest ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkBreast}", detail.TreatmentAreaBreast ? checkReplacements : uncheckReplacements);

            req.AddWordReplacement("{other areas treated}", detail.TreatmentAreaOther);

            //2) Therapist
            req.AddWordReplacement("{therapist}", soap.Therapist.ToString());

            //Header
            req.AddWordReplacement("{patient name}", soap.InitialTreatment.Patient.DisplayName);
            req.AddWordReplacement("{treatment type}", soap.InitialTreatment.TreatmentType.DisplayName);

            //3) Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(soap.Therapist.SignatureImage);
            if (signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }

        }

        private void buildGeneralSOAPRequest(ProcessRequest request, object obj) {
            FollowUpTreatment soap = obj as FollowUpTreatment;

            //0) Template Title
            request.AddWordReplacement("{treatment note title}", _treatmentNoteTitle);


            Patient patient = soap.InitialTreatment.Patient;
            //0.1) Patient name
            //request.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            request.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            request.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            request.AddWordReplacement("{file number}", patient.FileNumber);


            //1) Date/duration
            request.AddWordReplacement("{date}", soap.TreatmentTime.ToString(Constants.DATE_FORMAT));
            request.AddWordReplacement("{datetime}", soap.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            request.AddWordReplacement("{time_from}", soap.TreatmentTime.ToString(Constants.TIME_FORMAT));
            request.AddWordReplacement("{time_to}", soap.TreatmentTime.AddMinutes(soap.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            request.AddWordReplacement("{duration}", soap.TreatmentDurationMinutes.ToString());
            //2) Note
            request.AddWordReplacement("{note}", soap.Note);
            //3) Therapist
            request.AddWordReplacement("{therapist}", soap.Therapist.ToString());

            //Header
            request.AddWordReplacement("{patient name}", soap.InitialTreatment.Patient.DisplayName);
            request.AddWordReplacement("{treatment type}", soap.InitialTreatment.TreatmentType.DisplayName);

            //4) Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(soap.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                request.Replacements.Add("{signature}", picSignature);
            } else {
                request.AddWordReplacement("{signature}", "");
            }
        }

        private void tsbTherapists_Click(object sender, EventArgs e) {
            //
            MessageBox.Show(this, "TO DO: tsbTherapists_Click.");
        }

        private void showHideTrackingColumns(DataGridView grd, bool visible) {
            if(grd.Columns.Contains("Id")) grd.Columns["Id"].Visible = visible;
            if(grd.Columns.Contains("Version")) grd.Columns["Version"].Visible = visible;
            if (grd.Columns.Contains("Address1")) grd.Columns["Address1"].Visible = visible;
            if (grd.Columns.Contains("Address2")) grd.Columns["Address2"].Visible = visible;
            if (grd.Columns.Contains("PostalCode")) grd.Columns["PostalCode"].Visible = visible;
            //if (grd.Columns.Contains("HomePhone")) grd.Columns["HomePhone"].Visible = visible;
            //if (grd.Columns.Contains("CellPhone")) grd.Columns["CellPhone"].Visible = visible;
            if (grd.Columns.Contains("HomeFax")) grd.Columns["HomeFax"].Visible = visible;
            //if (grd.Columns.Contains("Email")) grd.Columns["Email"].Visible = visible;
            if (grd.Columns.Contains("FamilyPatientId")) grd.Columns["FamilyPatientId"].Visible = visible;
        }

        private void tsbBiggerFont_Click(object sender, EventArgs e) {
            changeAllGridFont(1);
        }

        private void tsbSmallerFont_Click(object sender, EventArgs e) {
            changeAllGridFont(-1);
        }

        private void changeAllGridFont(int delta) {
            changeGridFont(grdPatientList, delta);
            changeGridFont(grdInitialTreatmentList, delta);
            changeGridFont(grdFollowUpTreatmentList, delta);
        }

        private void changeGridFont(DataGridView grd, int delta) {
            Font curFont = grd.Font;
            grd.Font = new Font(curFont.FontFamily, curFont.Size + delta);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            printAllSOAPNotes((DateTime[]) e.Argument);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            _printProcessCompleted = true;
            if (_waitForm != null) {
                _waitForm.Close();
                _waitForm.Dispose();
                _waitForm = null;
            }
        }

        private void wordProgressReporter(string msg, double percent) {
            if(_waitForm != null) {
                _waitForm.SetProgress(msg, percent);
            }
        }

        private void loadCheckedListBoxes() {

            //Create key for session access
            Guid key = Guid.NewGuid();
            
            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    /*
                    //Insurer query
                    var insurers = session.CreateCriteria<Insurer>()
                        .SetMaxResults(Program.config.MaxInsurers)
                        .AddOrder(new Order("InsurerName", true))
                        .Future<Insurer>();

                    //Treatment query
                    var treatmentTypes = session.CreateCriteria<TherapyType>()
                        .SetMaxResults(Program.config.MaxTreatmentTypes)
                        .AddOrder(new Order("TherapyTypeName", true))
                        .Future<TherapyType>();

                    //Therapist query
                    var therapists = session.CreateCriteria<User>("u")
                        .CreateCriteria("u.Title", "t", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("u.UserType", UserType.Therapist))
                        .SetMaxResults(Program.config.MaxTherapists)
                        .Future<User>();

                    //Insurer list
                    foreach (Insurer insurer in insurers) {
                        ucInsurer.AddItem(insurer, true);
                    }
                    ucInsurer.CheckCompleted += new EventHandler(ucInsurer_CheckCompleted);

                    //Treatment list
                    foreach (TherapyType tt in treatmentTypes) {
                        ucTreatment.AddItem(tt, true);
                    }
                    ucTreatment.CheckCompleted += new EventHandler(ucTreatment_CheckCompleted);

                    //Therapist list
                    foreach (User usr in therapists) {
                        ucTherapist.AddItem(usr, true);
                    }
                    ucTherapist.CheckCompleted += new EventHandler(ucTherapist_CheckCompleted);
                    */

                    SimpleListItem aliasListItem = null;
                    SimpleUserItem aliasUserItem = null;

                    User aUser = null;
                    UserTitle aTitle = null;

                    //Load all reference tables in one batch query
                    var lstInsurer = session.QueryOver<Insurer>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.InsurerName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.InsurerName).Asc
                        .Take(Program.config.MaxInsurers)
                        .Future<SimpleListItem>();

                    var lstTreatmentType = session.QueryOver<TherapyType>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.TherapyTypeName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .Future<SimpleListItem>();

                    var lstTherapist = session.QueryOver<User>(() => aUser)
                        .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                        .Where(() => aUser.UserType == UserType.Therapist)
                        .SelectList(list => list
                            .Select(() => aUser.Id).WithAlias(() => aliasUserItem.Id)
                            .Select(() => aTitle.Name).WithAlias(() => aliasUserItem.Title)
                            .Select(() => aUser.FirstName).WithAlias(() => aliasUserItem.FirstName)
                            .Select(() => aUser.LastName).WithAlias(() => aliasUserItem.LastName)
                            .Select(() => aUser.MiddleName).WithAlias(() => aliasUserItem.MiddleName)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleUserItem>())
                        .OrderBy(() => aUser.FirstName).Asc
                        .OrderBy(() => aUser.LastName).Asc
                        .Take(Program.config.MaxTherapists)
                        .Future<SimpleUserItem>();

                    //Insurer
                    foreach (SimpleListItem insurer in lstInsurer) {
                        ucInsurer.AddItem(insurer, true);
                    }
                    ucInsurer.CheckCompleted += new EventHandler(ucInsurer_CheckCompleted);

                    //Treatment Types
                    foreach (SimpleListItem tt in lstTreatmentType) {
                        ucTreatment.AddItem(tt, true);
                    }
                    ucTreatment.CheckCompleted += new EventHandler(ucTreatment_CheckCompleted);

                    //Therapist
                    foreach (SimpleUserItem usr in lstTherapist) {
                        ucTherapist.AddItem(usr, true);
                    }
                    ucTherapist.CheckCompleted += new EventHandler(ucTherapist_CheckCompleted);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Filter lists. " + ex.Message);
                Program.log.Error("Failed to load Filter lists.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }

        void ucTherapist_CheckCompleted(object sender, EventArgs e) {
            reloadTreatmentList();
        }

        void ucTreatment_CheckCompleted(object sender, EventArgs e) {
            reloadTreatmentList();
        }

        void ucInsurer_CheckCompleted(object sender, EventArgs e) {
            pager.Page = 1;
            showPatientList();
        }

        private void reloadTreatmentList() {
            if(grdPatientList.SelectedRows.Count > 0) {
                PatientDTO p = grdPatientList.SelectedRows[0].DataBoundItem as PatientDTO;
                showInitialTreatmentList(p.Id);
            }
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e) {
            reloadTreatmentList();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e) {
            reloadTreatmentList();
        }

        private void tsbAdvancedSearch_Click(object sender, EventArgs e) {
            (new AdvancedSearchForm()).ShowDialog(this);
        }

        private void ucInsurer_UpdateDataList(object sender, EventArgs e) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    //Insurer
                    /*
                    IList<Insurer> lstInsurer = session.QueryOver<Insurer>()
                        .OrderBy(x => x.InsurerName).Asc
                        .Take(Program.config.MaxInsurers)
                        .List<Insurer>();
                    ucInsurer.UpdateListItems<Insurer>(lstInsurer);
                    */
                    SimpleListItem aliasListItem = null;
                    var lstInsurer = session.QueryOver<Insurer>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.InsurerName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.InsurerName).Asc
                        .Take(Program.config.MaxInsurers)
                        .List<SimpleListItem>();
                    ucInsurer.UpdateListItems<SimpleListItem>(lstInsurer);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Insurer lists. " + ex.Message);
                Program.log.Error("Failed to load Insurer lists.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }

        private void ucTreatment_LoadListData(object sender, EventArgs e) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    /*
                    //Treatment
                    IList<TherapyType> lstTreatmentType = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .List<TherapyType>();
                    ucTreatment.UpdateListItems<TherapyType>(lstTreatmentType);
                    */
                    SimpleListItem aliasListItem = null;
                    var lstTreatmentType = session.QueryOver<TherapyType>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.TherapyTypeName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .List<SimpleListItem>();
                    ucTreatment.UpdateListItems<SimpleListItem>(lstTreatmentType);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Treatment lists. " + ex.Message);
                Program.log.Error("Failed to load Treatment lists.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }

        private void ucTherapist_LoadListData(object sender, EventArgs e) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            try {
                //Get session
                ISession session = SessionFactory.GetOpenSession(key);

                using (ITransaction tr = session.BeginTransaction()) {

                    /*
                    //Therapist
                    IList<User> lstTherapist = session.QueryOver<User>()
                        .Where(x => x.UserType == UserType.Therapist)
                        .OrderBy(x => x.FirstName).Asc
                        .OrderBy(x => x.LastName).Asc
                        .Take(Program.config.MaxTherapists)
                        .List<User>();
                    ucTherapist.UpdateListItems<User>(lstTherapist);
                    */
                    SimpleUserItem aliasUserItem = null;
                    User aUser = null;
                    UserTitle aTitle = null;
                    var lstTherapist = session.QueryOver<User>(() => aUser)
                        .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                        .Where(() => aUser.UserType == UserType.Therapist)
                        .SelectList(list => list
                            .Select(() => aUser.Id).WithAlias(() => aliasUserItem.Id)
                            .Select(() => aTitle.Name).WithAlias(() => aliasUserItem.Title)
                            .Select(() => aUser.FirstName).WithAlias(() => aliasUserItem.FirstName)
                            .Select(() => aUser.LastName).WithAlias(() => aliasUserItem.LastName)
                            .Select(() => aUser.MiddleName).WithAlias(() => aliasUserItem.MiddleName)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleUserItem>())
                        .OrderBy(() => aUser.FirstName).Asc
                        .OrderBy(() => aUser.LastName).Asc
                        .Take(Program.config.MaxTherapists)
                        .List<SimpleUserItem>();
                    ucTherapist.UpdateListItems<SimpleUserItem>(lstTherapist);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Therapist lists. " + ex.Message);
                Program.log.Error("Failed to load Therapist lists.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }

        private void tsbInvoice_Click(object sender, EventArgs e) {
            //(new InvoiceSearchForm()).ShowDialog(this);
            if (grdPatientList.SelectedRows.Count > 0) {
                PatientDTO p = grdPatientList.SelectedRows[0].DataBoundItem as PatientDTO;
                (new InvoiceSearchForm(p.Id)).ShowDialog(this);
            } else {
                (new InvoiceSearchForm()).ShowDialog(this);
            }
        }

        private void tsbAlert_Click(object sender, EventArgs e) {
            (new PendingTreatmentAlertForm()).ShowDialog(this);
            showPatientList();
        }

        private void btnDeletePatient_Click(object sender, EventArgs e) {
            if(grdPatientList.SelectedRows != null && grdPatientList.SelectedRows.Count > 0) {
                PatientDTO patient = grdPatientList.SelectedRows[0].DataBoundItem as PatientDTO;
                deletePatient(patient.Id);
            }
        }

        private void btnDeleteInitialTreatment_Click(object sender, EventArgs e) {
            if(grdInitialTreatmentList.SelectedRows != null && grdInitialTreatmentList.SelectedRows.Count > 0) {
                InitialTreatmentDTO it = grdInitialTreatmentList.SelectedRows[0].DataBoundItem as InitialTreatmentDTO;
                deleteInitialTreatment(it.Id);
            }
        }

        private void btnDeleteFollowUp_Click(object sender, EventArgs e) {
            if(grdFollowUpTreatmentList.SelectedRows != null && grdFollowUpTreatmentList.SelectedRows.Count > 0) {
                FollowupTreatmentDTO ft = grdFollowUpTreatmentList.SelectedRows[0].DataBoundItem as FollowupTreatmentDTO;
                deleteFollowUpTreatment(ft.Id);
            }
        }

        private void tsbHistory_Click(object sender, EventArgs e) {
            (new HistoryForm()).ShowDialog(this);
        }

        private void tsbHelp_Click(object sender, EventArgs e) {
            (new HelpForm()).ShowDialog(this);
        }

        private void tsbLogoffCalendar_Click(object sender, EventArgs e) {
            /* Google Calendar function has been removed.
            if (Program._calendar != null) {
                try {
                    Program._calendar.Logoff();
                    MessageBox.Show("You have logged off from Google Calendar.");
                } catch (Exception ex) {
                    MessageBox.Show("Google Calendar Error:\n" + ex.Message);
                }
            }*/
        }

        private void btnSearchName_Click(object sender, EventArgs e) {
            _patientSearchText = txtSearchName.Text.Trim();
            pager.Page = 1;
            showPatientList();
        }

        private void btnSearchPhone_Click(object sender, EventArgs e) {
            _patientSearchText = txtSearchPhone.Text.Trim();
            pager.Page = 1;
            showPatientList();
        }

        private void txtSearchName_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchName_Click(null, null);
            }
        }

        private void txtSearchPhone_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchPhone_Click(null, null);
            }
        }

        private void btnSearchPostalCode_Click(object sender, EventArgs e) {
            _patientSearchText = txtSearchPostalCode.Text.Trim();
            pager.Page = 1;
            showPatientList();
        }

        private void txtSearchPostalCode_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchPostalCode_Click(null, null);
            }
        }

        private void txtSearchName_Enter(object sender, EventArgs e) {
            txtSearchName.SelectAll();
            txtSearchName.Focus();
        }

        private void txtSearchPostalCode_Enter(object sender, EventArgs e) {
            txtSearchPostalCode.SelectAll();
            txtSearchPostalCode.Focus();
        }

        private void txtSearchPhone_Enter(object sender, EventArgs e) {
            txtSearchPhone.SelectAll();
            txtSearchPhone.Focus();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            //SessionFactory.TryCloseSession(_key);
            SessionFactory.EnsureSessionClose();
        }

        private void tsbPractitionerCalendar_Click(object sender, EventArgs e) {
            (new AvailabilityCalendarForm()).ShowDialog(this);
        }

        //Custom sort
        private void grdPatientList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridViewColumn col = grdPatientList.Columns[e.ColumnIndex];
            if (col.SortMode != DataGridViewColumnSortMode.Programmatic) return;

            if (col.DataPropertyName == _patientSortColumnName) {
                _patientSortAscending = !_patientSortAscending;
            } else {
                _patientSortAscending = true;
                pager.Page = 1;
            }

            _patientSortColumnName = col.DataPropertyName;

            showPatientList();
        }

        private void tsbBooking_Click(object sender, EventArgs e) {
            if (_frmBooking == null || _frmBooking.IsDisposed) {
                _frmBooking = new BookingForm(CalendarMode.Daily, DateTime.Now);
            }

            _frmBooking.Show(this);
        }

        private void btnListFamily_Click(object sender, EventArgs e) {

            if (grdPatientList.SelectedRows != null && grdPatientList.SelectedRows.Count > 0) {

                PatientDTO patient = grdPatientList.SelectedRows[0].DataBoundItem as PatientDTO;
                int primaryFamilyId = patient.FamilyPatientId.HasValue ? patient.FamilyPatientId.Value : patient.Id;

                pager.Page = 1;
            
                //Create key for NHibernate session access
                Guid key = Guid.NewGuid();

                if (Program.LogonUser.UserType == UserType.None) return;

                try {
                    //Get session
                    ISession session = SessionFactory.GetOpenSession(key);

                    using (ITransaction tr = session.BeginTransaction()) {

                        PatientDTO dtoPatient = null;
                        Patient aPatient = null;
                        Insurer aInsurer = null;

                        //Prepare query
                        var qry = session.QueryOver<Patient>(() => aPatient)
                            .JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin);

                        qry.Where(Restrictions.Disjunction()
                            .Add(Restrictions.Eq(Projections.Property(() => aPatient.FamilyPatientId), primaryFamilyId))
                            .Add(Restrictions.Eq(Projections.Property(() => aPatient.Id), primaryFamilyId))
                            );

                        var members = (qry.Clone())
                            .SelectList(list => list
                                .Select(() => aPatient.Id).WithAlias(() => dtoPatient.Id)
                                .Select(() => aPatient.Version).WithAlias(() => dtoPatient.Version)
                                .Select(() => aPatient.FamilyPatientId).WithAlias(() => dtoPatient.FamilyPatientId)
                                .Select(() => aPatient.Sex).WithAlias(() => dtoPatient.Sex)
                                .Select(() => aPatient.FileNumber).WithAlias(() => dtoPatient.FileNumber)
                                .Select(() => aPatient.FirstName).WithAlias(() => dtoPatient.FirstName)
                                .Select(() => aPatient.LastName).WithAlias(() => dtoPatient.LastName)
                                .Select(() => aPatient.MiddleName).WithAlias(() => dtoPatient.MiddleName)
                                .Select(() => aPatient.DateOfBirth).WithAlias(() => dtoPatient.DateOfBirth)

                                .Select(() => aPatient.ContactInfo.Address.AddressLine1).WithAlias(() => dtoPatient.Address1)
                                .Select(() => aPatient.ContactInfo.Address.AddressLine2).WithAlias(() => dtoPatient.Address2)
                                .Select(() => aPatient.ContactInfo.Address.PostalCode).WithAlias(() => dtoPatient.PostalCode)
                                .Select(() => aPatient.ContactInfo.HomePhone).WithAlias(() => dtoPatient.HomePhone)
                                .Select(() => aPatient.ContactInfo.CellPhone).WithAlias(() => dtoPatient.CellPhone)
                                .Select(() => aPatient.ContactInfo.HomeFax).WithAlias(() => dtoPatient.HomeFax)
                                .Select(() => aPatient.ContactInfo.Email).WithAlias(() => dtoPatient.Email)

                                .Select(() => aPatient.Note).WithAlias(() => dtoPatient.Note)
                                .Select(() => aInsurer.InsurerName).WithAlias(() => dtoPatient.Insurer)
                            )
                            .TransformUsing(Transformers.AliasToBean<PatientDTO>())
                            .OrderBy(() => aPatient.FamilyPatientId).Asc
                            .ThenBy(() => aPatient.DateOfBirth).Asc
                            .ThenBy(() => aPatient.FirstName).Asc
                            .Future<PatientDTO>();

                        //Get total count
                        int totalCount = qry.RowCount();

                        //Set pager
                        if (totalCount == 0) {
                            pager.TotalPage = 1;
                        } else {
                            pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pager.PageSize)) + 1;
                        }

                        //Comment transaction
                        tr.Commit();

                        //Add patients into SortableBindingList
                        SortableBindingList<PatientDTO> patients = new SortableBindingList<PatientDTO>();
                        foreach (PatientDTO p in members) {
                            patients.Add(p);
                        }

                        grdPatientList.DataSource = patients;// lstPatient;
                        if (patients != null && patients.Count > 0) {
                            setSelectedPatientRow();
                        } else {
                            clearInitialList();
                        }

                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Show Patient List error.", ex);
                } finally {
                    SessionFactory.TryCloseSession(key);
                }
            }
        }

        private void tsbDocuments_Click(object sender, EventArgs e) {
            (new SearchDocumentsForm()).ShowDialog(this);
        }

        private void tsbReports_Click(object sender, EventArgs e) {
            (new ReportsForm()).ShowDialog(this);
        }
    }
}
