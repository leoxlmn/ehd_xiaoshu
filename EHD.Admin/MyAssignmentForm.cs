using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using NHibernate;
using EHD.Repository;
using System.Diagnostics;
using EHD.Constant;
using Utility;
using NHibernate.Criterion;
using EHD.Model.DTO;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace EHD.Admin {
    public partial class MyAssignmentForm : Form {

        private User _therapist = null;
        private bool _timerWorking = false;

        private int _lastEditedInitialTreatmentId = 0;
        private int _lastEditedFollowUpTreatmentId = 0;

        public MyAssignmentForm(User Therapist) {
            _therapist = Therapist;
            InitializeComponent();
        }

        private void MyAssignmentForm_Load(object sender, EventArgs e) {
            tsLabelAssignmentFor.Text = _therapist.DisplayName;
            tsLabelLogonUser.Text = Program.LogonUser.DisplayName;

            drpListType.SelectedItem = "Today's List";

            //Initiate pagers
            pagerInitial.Page = 1;
            pagerInitial.GoToPageEvent += new GoToPageEventHandler(initialNaviate);
            pagerFollowup.Page = 1;
            pagerFollowup.GoToPageEvent += new GoToPageEventHandler(followupNaviate);

            showInitialTreatmentList();
            showFollowUpTreatmentList();
        }

        private void initialNaviate(int page) {
            showInitialTreatmentList();
        }

        private void followupNaviate(int page) {
            showFollowUpTreatmentList();
        }

        private void showInitialTreatmentList() {

            #region Rewrote below
            /*
            //Clear list first
            grdInitialTreatment.DataSource = new List<InitialTreatment>();

            if(Program.LogonUser.UserType == UserType.None) return;

            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    //Prepare query
                    var qry = session.CreateCriteria<InitialTreatment>("it")
                        .CreateCriteria("it.Patient", "p", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("it.TreatmentType", "type", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("it.Therapist", "u", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("u.Id", _therapist.Id))
                        .Add(Restrictions.Eq("it.OpenToTherapist", true));

                    //Add filter by patient name
                    string searchText = txtSearchName.Text.Trim();
                    if (searchText.Length > 0) {
                        qry.Add(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike("p.FirstName", searchText))
                            .Add(Restrictions.InsensitiveLike("p.LastName", searchText))
                            .Add(Restrictions.InsensitiveLike("p.FileNumber", searchText))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                searchText,
                                NHibernateUtil.String))
                            );

                    }

                    //Completed search query with order and paging
                    var qrySearch = (qry.Clone() as ICriteria)
                        .AddOrder(new Order("it.TreatmentTime", false))
                        .SetFirstResult((pagerInitial.Page - 1) * pagerInitial.PageSize)
                        .SetMaxResults(pagerInitial.PageSize)
                        .Future<InitialTreatment>();

                    //Total count
                    int totalCount = qry.SetProjection(Projections.Count(Projections.Id()))
                        .FutureValue<int>()
                        .Value;
                    
                    //Get search result
                    var lstInitTreatment = qrySearch.ToList<InitialTreatment>();

                    //Set pager
                    if (totalCount == 0) {
                        pagerInitial.TotalPage = 1;
                    } else {
                        pagerInitial.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pagerInitial.PageSize)) + 1;
                    }

                    string listType = drpListType.SelectedItem.ToString().Trim().ToLower();
                    if (listType.Equals("today's list")) {
                        lstInitTreatment = lstInitTreatment.Where(x =>
                            x.TreatmentTime.Date == DateTime.Now.Date)
                            .ToList();
                    } else if (listType.Equals("up coming list")) {
                        lstInitTreatment = lstInitTreatment.Where(x =>
                            x.TreatmentTime.Date >= DateTime.Now)
                            .ToList();
                    }

                    //data source
                    grdInitialTreatment.DataSource = lstInitTreatment;

                    //highlight row
                    setSelectedInitialTreatmentRow();

                    tr.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
                Program.log.Error("showInitialTreatmentList error", ex);
            }
            */
            #endregion Rewrote below

            //Create key for NHibernate session access
            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);

            //Clear list first
            grdInitialTreatment.DataSource = null;

            if (Program.LogonUser.UserType == UserType.None) return;

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    InitialTreatmentSearchResultDTO dto = null;
                    Patient aPatient = null;
                    InitialTreatment aIT = null;
                    TherapyType aType = null;

                    //Prepare query
                    var qry = session.QueryOver<Patient>(() => aPatient)
                        .JoinQueryOver<InitialTreatment>(() => aPatient.InitialTreatments, () => aIT, JoinType.InnerJoin)
                        .JoinQueryOver<TherapyType>(() => aIT.TreatmentType, () => aType, JoinType.InnerJoin)
                        .Where(() => aIT.Therapist.Id == _therapist.Id)
                        .Where(() => aIT.OpenToTherapist == true);

                    //Add filter by patient name
                    string searchText = txtSearchName.Text.Trim();
                    if (searchText.Length > 0) {
                        qry.Where(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FirstName), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.LastName), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FileNumber), searchText))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                searchText,
                                NHibernateUtil.String))
                            );
                    }

                    //Add time filter
                    string listType = drpListType.SelectedItem.ToString().Trim().ToLower();
                    if (listType.Equals("today's list")) {
                        qry
                            .Where(Restrictions.Ge(Projections.Property(() => aIT.TreatmentTime), DateTime.Now.Date))
                            .Where(Restrictions.Lt(Projections.Property(() => aIT.TreatmentTime), DateTime.Now.Date.AddDays(1)));

                    } else if (listType.Equals("up coming list")) {
                        qry
                            .Where(Restrictions.Ge(Projections.Property(() => aIT.TreatmentTime), DateTime.Now));
                    }

                    //Completed search query with order and paging
                    var qrySearch = (qry.Clone())
                        .SelectList(list => list
                            .Select(() => aPatient.Id).WithAlias(() => dto.PatientId)
                            .Select(() => aPatient.Version).WithAlias(() => dto.PatientVersion)
                            .Select(() => aPatient.FileNumber).WithAlias(() => dto.FileNumber)
                            .Select(() => aPatient.Sex).WithAlias(() => dto.Sex)
                            .Select(() => aPatient.FirstName).WithAlias(() => dto.FirstName)
                            .Select(() => aPatient.MiddleName).WithAlias(() => dto.MiddleName)
                            .Select(() => aPatient.LastName).WithAlias(() => dto.LastName)
                            .Select(() => aIT.Id).WithAlias(() => dto.TreatmentId)
                            .Select(() => aIT.Version).WithAlias(() => dto.TreatmentVersion)
                            .Select(() => aType.TherapyTypeName).WithAlias(() => dto.TreatmentType)
                            .Select(() => aIT.TreatmentTime).WithAlias(() => dto.TreatmentTime)
                            .Select(() => aIT.TreatmentDurationMinutes).WithAlias(() => dto.Duration)
                            .Select(() => aIT.IsComplete).WithAlias(() => dto.IsCompleted)
                        )
                        .OrderBy(() => aIT.TreatmentTime).Desc
                        .ThenBy(() => aIT.Id).Desc
                        .TransformUsing(Transformers.AliasToBean<InitialTreatmentSearchResultDTO>())
                        .Skip((pagerInitial.Page - 1) * pagerInitial.PageSize)
                        .Take(pagerInitial.PageSize)
                        .Future<InitialTreatmentSearchResultDTO>();

                    //Total count
                    int totalCount = qry.Select(Projections.RowCount())
                        .SingleOrDefault<int>();

                    //Get search result
                    var lstInitTreatment = qrySearch.ToList<InitialTreatmentSearchResultDTO>();

                    //Set pager
                    if (totalCount == 0) {
                        pagerInitial.TotalPage = 1;
                    } else {
                        pagerInitial.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pagerInitial.PageSize)) + 1;
                    }


                    //data source
                    grdInitialTreatment.DataSource = lstInitTreatment;

                    //highlight row
                    setSelectedInitialTreatmentRow();

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                Program.log.Error("showInitialTreatmentList error", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

        }

        private void setSelectedInitialTreatmentRow() {
            if(_lastEditedInitialTreatmentId != 0 && grdInitialTreatment.Rows.Count > 0) {

                foreach(DataGridViewRow dr in grdInitialTreatment.Rows) {
                    InitialTreatmentSearchResultDTO it = dr.DataBoundItem as InitialTreatmentSearchResultDTO;
                    if(it.TreatmentId == _lastEditedInitialTreatmentId) {
                        //Highlight selected row
                        dr.Selected = true;
                        //Scroll grid to display the selected row if it's not displayed
                        if(!dr.Displayed)
                            grdInitialTreatment.FirstDisplayedScrollingRowIndex = dr.Index;

                        Debug.WriteLine("Set selected row to " + dr.Index);

                        break;
                    }
                }
            }
        }

        private void showFollowUpTreatmentList() {
            #region Rewrote below
            /*
            //Clear list first
            grdFollowUpTreatment.DataSource = new List<FollowUpTreatment>();

            if(Program.LogonUser.UserType == UserType.None) return;

            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()){

                    //Prepare query
                    var qry = session.CreateCriteria<FollowUpTreatment>("ft")
                        .CreateCriteria("ft.InitialTreatment", "it", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("it.Patient", "p", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("p.Insurer", "i", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("it.TreatmentType", "type", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("ft.Therapist", "u", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("u.Id", _therapist.Id))
                        .Add(Restrictions.Eq("ft.OpenToTherapist", true));

                    //Add filter by patient name
                    string searchText = txtSearchName.Text.Trim();
                    if (searchText.Length > 0) {
                        qry.Add(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike("p.FirstName", searchText))
                            .Add(Restrictions.InsensitiveLike("p.LastName", searchText))
                            .Add(Restrictions.InsensitiveLike("p.FileNumber", searchText))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                searchText,
                                NHibernateUtil.String))
                            );

                    }

                    //Completed search query with order and paging
                    var qrySearch = (qry.Clone() as ICriteria)
                        .AddOrder(new Order("ft.TreatmentTime", false))
                        .SetFirstResult((pagerFollowup.Page - 1) * pagerFollowup.PageSize)
                        .SetMaxResults(pagerFollowup.PageSize)
                        .Future<FollowUpTreatment>();

                    //Total count
                    int totalCount = qry.SetProjection(Projections.Count(Projections.Id()))
                        .FutureValue<int>()
                        .Value;

                    //Get search result
                    var lstFollowUpTreatment = qrySearch.ToList<FollowUpTreatment>();

                    //Set pager
                    if (totalCount == 0) {
                        pagerFollowup.TotalPage = 1;
                    } else {
                        pagerFollowup.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pagerFollowup.PageSize)) + 1;
                    }

                    string listType = drpListType.SelectedItem.ToString().Trim().ToLower();
                    if (listType.Equals("today's list")) {
                        lstFollowUpTreatment = lstFollowUpTreatment.Where(x =>
                            x.TreatmentTime.Date == DateTime.Now.Date)
                            .ToList();
                    } else if (listType.Equals("up coming list")) {
                        lstFollowUpTreatment = lstFollowUpTreatment.Where(x =>
                            x.TreatmentTime.Date >= DateTime.Now)
                            .ToList();
                    }

                    //data source
                    grdFollowUpTreatment.DataSource = lstFollowUpTreatment;

                    //highlight row
                    setSelectedFollowUpTreatmentRow();

                    tr.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
                Program.log.Error("showFollowUpTreatmentList error.", ex);
            }
            */
            #endregion Rewrote below

            //Create key for NHibernate session access
            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);

            if (Program.LogonUser.UserType == UserType.None) return;

            //Clear list first
            grdFollowUpTreatment.DataSource = null;

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    FollowupTreatmentSearchResultDTO dto = null;
                    Patient aPatient = null;
                    InitialTreatment aIT = null;
                    FollowUpTreatment aFT = null;
                    TherapyType aType = null;

                    //Prepare query
                    var qry = session.QueryOver<Patient>(() => aPatient)
                        .JoinQueryOver<InitialTreatment>(() => aPatient.InitialTreatments, () => aIT, JoinType.InnerJoin)
                        .JoinQueryOver<TherapyType>(() => aIT.TreatmentType, () => aType, JoinType.InnerJoin)
                        .JoinQueryOver<FollowUpTreatment>(() => aIT.FollowUpTreatments, () => aFT, JoinType.InnerJoin)
                        .Where(() => aFT.Therapist.Id == _therapist.Id)
                        .Where(() => aFT.OpenToTherapist == true);

                    //Add filter by patient name
                    string searchText = txtSearchName.Text.Trim();
                    if (searchText.Length > 0) {
                        qry.Where(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FirstName), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.LastName), searchText))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FileNumber), searchText))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                searchText,
                                NHibernateUtil.String))
                            );
                    }

                    //Add time filter
                    string listType = drpListType.SelectedItem.ToString().Trim().ToLower();
                    if (listType.Equals("today's list")) {
                        qry
                            .Where(Restrictions.Ge(Projections.Property(() => aFT.TreatmentTime), DateTime.Now.Date))
                            .Where(Restrictions.Lt(Projections.Property(() => aFT.TreatmentTime), DateTime.Now.Date.AddDays(1)));

                    } else if (listType.Equals("up coming list")) {
                        qry
                            .Where(Restrictions.Ge(Projections.Property(() => aFT.TreatmentTime), DateTime.Now));
                    }

                    //Completed search query with order and paging
                    var qrySearch = (qry.Clone())
                        .SelectList(list => list
                            .Select(() => aPatient.Id).WithAlias(() => dto.PatientId)
                            .Select(() => aPatient.Version).WithAlias(() => dto.PatientVersion)
                            .Select(() => aPatient.FileNumber).WithAlias(() => dto.FileNumber)
                            .Select(() => aPatient.Sex).WithAlias(() => dto.Sex)
                            .Select(() => aPatient.FirstName).WithAlias(() => dto.FirstName)
                            .Select(() => aPatient.MiddleName).WithAlias(() => dto.MiddleName)
                            .Select(() => aPatient.LastName).WithAlias(() => dto.LastName)

                            .Select(() => aIT.Id).WithAlias(() => dto.InitialTreatmentId)

                            .Select(() => aFT.Id).WithAlias(() => dto.TreatmentId)
                            .Select(() => aFT.Version).WithAlias(() => dto.TreatmentVersion)
                            .Select(() => aType.TherapyTypeName).WithAlias(() => dto.TreatmentType)
                            .Select(() => aFT.TreatmentTime).WithAlias(() => dto.TreatmentTime)
                            .Select(() => aFT.TreatmentDurationMinutes).WithAlias(() => dto.Duration)
                            .Select(() => aFT.IsComplete).WithAlias(() => dto.IsCompleted)
                        )
                        .TransformUsing(Transformers.AliasToBean<FollowupTreatmentSearchResultDTO>())
                        .OrderBy(() => aFT.TreatmentTime).Desc
                        .ThenBy(() => aFT.Id).Desc
                        .Skip((pagerFollowup.Page - 1) * pagerFollowup.PageSize)
                        .Take(pagerFollowup.PageSize)
                        .Future<FollowupTreatmentSearchResultDTO>();

                    //Total count
                    int totalCount = qry.Select(Projections.RowCount())
                        .SingleOrDefault<int>();

                    //Get search result
                    var lstTreatment = qrySearch.ToList<FollowupTreatmentSearchResultDTO>();

                    //Set pager
                    if (totalCount == 0) {
                        pagerFollowup.TotalPage = 1;
                    } else {
                        pagerFollowup.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pagerFollowup.PageSize)) + 1;
                    }

                    //data source
                    grdFollowUpTreatment.DataSource = lstTreatment;

                    //highlight row
                    setSelectedFollowUpTreatmentRow();

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                Program.log.Error("showFollowUpTreatmentList error.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }

        private void setSelectedFollowUpTreatmentRow() {
            if(_lastEditedFollowUpTreatmentId != 0 && grdFollowUpTreatment.Rows.Count > 0) {

                foreach(DataGridViewRow dr in grdFollowUpTreatment.Rows) {
                    FollowupTreatmentSearchResultDTO ft = dr.DataBoundItem as FollowupTreatmentSearchResultDTO;
                    if(ft.TreatmentId == _lastEditedFollowUpTreatmentId) {
                        //Highlight selected row
                        dr.Selected = true;
                        //Scroll grid to display the selected row if it's not displayed
                        if(!dr.Displayed)
                            grdFollowUpTreatment.FirstDisplayedScrollingRowIndex = dr.Index;

                        Debug.WriteLine("Set selected row to " + dr.Index);

                        break;
                    }
                }
            } 
        }

        private void grdInitialTreatment_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            InitialTreatmentSearchResultDTO it = grdInitialTreatment.Rows[rowIndex].DataBoundItem as InitialTreatmentSearchResultDTO;
            _lastEditedInitialTreatmentId = it.TreatmentId;
            editInitialTreatment(it.TreatmentId);
        }

        private void grdInitialTreatment_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            foreach(DataGridViewRow dr in grdInitialTreatment.Rows) {
                InitialTreatmentSearchResultDTO it = dr.DataBoundItem as InitialTreatmentSearchResultDTO;

                //Bind Patient Button Image
                dr.Cells[Constants.IMAGE_BUTTON_PATIENT_BUTTON].Value = (it.Sex == Sex.Male ? lstIcons.Images["Male"] :
                    it.Sex == Sex.Female ? lstIcons.Images["Female"] :
                    lstIcons.Images["Person"]);

                //Bind Edit Initial Treatment Button
                dr.Cells[Constants.IMAGE_BUTTON_EDIT_INITIAL_TREATMENT].Value = lstIcons.Images["Edit"];

                //Bind Initial Treatment Complete icon
                dr.Cells[Constants.IMAGE_ICON_INIT_TREATMENT_COMPLETE].Value =
                    (it.IsCompleted ? lstIcons.Images["Complete"] : lstIcons.Images["Transparent"]);
            }

            //Show/hide columns
            showHideTrackingColumns(grdInitialTreatment, false);
        }

        private void grdFollowUpTreatment_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            FollowupTreatmentSearchResultDTO ft = grdFollowUpTreatment.Rows[rowIndex].DataBoundItem as FollowupTreatmentSearchResultDTO;
            _lastEditedFollowUpTreatmentId = ft.TreatmentId;
            editFollowUpTreatment(_lastEditedFollowUpTreatmentId);
        }

        private void grdFollowUpTreatment_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            foreach(DataGridViewRow dr in grdFollowUpTreatment.Rows) {
                FollowupTreatmentSearchResultDTO ft = dr.DataBoundItem as FollowupTreatmentSearchResultDTO;

                //Bind Patient Button Image
                dr.Cells[Constants.IMAGE_BUTTON_FOLLOWUP_PATIENT_BUTTON].Value = ft.Sex == Sex.Male ? lstIcons.Images["Male"] :
                    ft.Sex == Sex.Female ? lstIcons.Images["Female"] :
                    lstIcons.Images["Person"];

                //Bind Initial Treatment Button Image
                dr.Cells[Constants.IMAGE_BUTTON_INITIAL_TREATMENT_BUTTON].Value = lstIcons.Images["InitialTreatment"];

                //Bind Edit FollowUp Treatment Button Image
                dr.Cells[Constants.IMAGE_BUTTON_EDIT_FOLLOWUP_TREATMENT_BUTTON].Value = lstIcons.Images["Edit"];

                //Bind Follow Up Treatment Complete icon
                dr.Cells[Constants.IMAGE_ICON_FOLLOWUP_TREATMENT_COMPLETE].Value =
                    ft.IsCompleted ? lstIcons.Images["Complete"] : lstIcons.Images["Transparent"];
            }

            //Show/hide columns
            showHideTrackingColumns(grdFollowUpTreatment, false);
        }

        private void grdInitialTreatment_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            InitialTreatmentSearchResultDTO it = grdInitialTreatment.Rows[rowIndex].DataBoundItem as InitialTreatmentSearchResultDTO;
            _lastEditedInitialTreatmentId = it.TreatmentId;

            string colName = grdInitialTreatment.Columns[colIndex].Name;
            switch(colName) {
                case Constants.IMAGE_BUTTON_PATIENT_BUTTON:
                    viewPatient(it.PatientId);
                    break;
                case Constants.IMAGE_BUTTON_EDIT_INITIAL_TREATMENT:
                    editInitialTreatment(it.TreatmentId);
                    break;
            }
        }

        private void editInitialTreatment(int itId) {
            EditInitialTreatmentForm InitialTreatmentForm = new EditInitialTreatmentForm(0, itId);
            if(DialogResult.OK == InitialTreatmentForm.ShowDialog(this)) {
                _lastEditedInitialTreatmentId = InitialTreatmentForm.EditingInitialTreatment.Id;
                refreshList();
            }
        }

        private void grdFollowUpTreatment_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            FollowupTreatmentSearchResultDTO ft = grdFollowUpTreatment.Rows[rowIndex].DataBoundItem as FollowupTreatmentSearchResultDTO;
            _lastEditedFollowUpTreatmentId = ft.TreatmentId;

            string colName = grdFollowUpTreatment.Columns[colIndex].Name;
            switch(colName) {
                case Constants.IMAGE_BUTTON_FOLLOWUP_PATIENT_BUTTON:
                    viewPatient(ft.PatientId);
                    break;
                case Constants.IMAGE_BUTTON_INITIAL_TREATMENT_BUTTON:
                    viewInitialTreatment(ft.InitialTreatmentId);
                    break;
                case Constants.IMAGE_BUTTON_EDIT_FOLLOWUP_TREATMENT_BUTTON:
                    editFollowUpTreatment(ft.TreatmentId);
                    break;
            }
        }

        private void editFollowUpTreatment(int ftId) {
            EditFollowUpTreatmentForm FollowUpTreatmentForm = new EditFollowUpTreatmentForm(0, ftId);
            if(DialogResult.OK == FollowUpTreatmentForm.ShowDialog(this)) {
                _lastEditedFollowUpTreatmentId = FollowUpTreatmentForm.EditingFollowUpTreatment.Id;
                refreshList();
            }
        }

        private void viewPatient(int patientId) {
            EditPatientForm patientForm = new EditPatientForm(patientId, true);
            patientForm.ShowDialog(this);
        }

        private void viewInitialTreatment(int itId) {
            EditInitialTreatmentForm InitialTreatmentForm = new EditInitialTreatmentForm(0, itId, true);
            InitialTreatmentForm.ShowDialog(this);
        }

        private void refreshList() {

            showInitialTreatmentList();
            showFollowUpTreatmentList();
        }

        //Reload list every 10 seconds
        private void timerReload_Tick(object sender, EventArgs e) {

            if (!chkAutoRefresh.Checked) return;

            if(_timerWorking) return;

            _timerWorking = true;

            refreshList();

            _timerWorking = false;
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Application.Restart();
        }

        private void btnBiggerFont_Click(object sender, EventArgs e) {
            changeAllGridFont(1);
        }

        private void btnSmallerFont_Click(object sender, EventArgs e) {
            changeAllGridFont(-1);
        }

        private void changeAllGridFont(int delta) {
            changeGridFont(grdInitialTreatment, delta);
            changeGridFont(grdFollowUpTreatment, delta);
        }

        private void changeGridFont(DataGridView grd, int delta) {
            Font curFont = grd.Font;
            grd.Font = new Font(curFont.FontFamily, curFont.Size + delta);
        }

        private void showHideTrackingColumns(DataGridView grd, bool visible) {
            if (grd.Columns.Contains("PatientId")) grd.Columns["PatientId"].Visible = visible;
            if (grd.Columns.Contains("PatientVersion")) grd.Columns["PatientVersion"].Visible = visible;
            if (grd.Columns.Contains("Sex")) grd.Columns["Sex"].Visible = visible;
            if (grd.Columns.Contains("TreatmentId")) grd.Columns["TreatmentId"].Visible = visible;
            if (grd.Columns.Contains("TreatmentVersion")) grd.Columns["TreatmentVersion"].Visible = visible;
            if (grd.Columns.Contains("InitialTreatmentId")) grd.Columns["InitialTreatmentId"].Visible = visible;
        }

        private void grdInitialTreatment_DataError(object sender, DataGridViewDataErrorEventArgs e) {

        }

        private void grdFollowUpTreatment_DataError(object sender, DataGridViewDataErrorEventArgs e) {

        }

        private void drpListType_SelectedIndexChanged(object sender, EventArgs e) {
            refreshList();
        }

        private void btnSearchName_Click(object sender, EventArgs e) {
            refreshList();
        }

        private void txtSearchName_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                refreshList();
            }
        }
    }
}
