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
using WordFileProcessor;
using System.IO;
using FilePathExtender;
using System.Reflection;
using System.Drawing.Drawing2D;
using EHD.FormUtitlity;
using System.Threading;
using HolidayCalculator;
using NHibernate.Transform;
using EHD.Model.DTO;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace EHD.Admin {
    public partial class EditInitialTreatmentForm : Form {

        private bool _viewOnly = false;

        private PleaseWaitForm _waitForm = null;

        private const int _defaultFormHeight = 700;

        private bool _printProcessCompleted = false;

        private string _treatmentNoteTitle = string.Empty;
        private string _originalTreatmentType = string.Empty;

        public InitialTreatment EditingInitialTreatment { get; set; }

        private ITreatmentDetail EditingTreatmentDetail {
            get {
                if(pnlTreatmentDetail.Controls.Count <= 0) return null;

                ITreatmentDetailUserControl uc = pnlTreatmentDetail.Controls[0] as ITreatmentDetailUserControl;
                return uc.TreatmentDetail;
            }
        }

        /*
        public EditInitialTreatmentForm(InitialTreatment InitialTreatment, bool ViewOnly = false) {

            _viewOnly = ViewOnly;

            if(InitialTreatment == null || InitialTreatment.Patient == null)
                throw new ArgumentException("InitialTreatment and it's Parent cannot be null.");

            InitializeComponent();

            EditingInitialTreatment = InitialTreatment;

            //Populate dropdownlists
            populateDropDownLists();

            //Populate form
            loadInitialTreatment();

            //Some form controls are not for therapists
            if(Program.LogonUser.UserType == UserType.Therapist) {
                btnOpenToTherapist.Visible = false;
                drpTherapist.Enabled = false;
                drpTreatmentType.Enabled = false;
                dtTreatmentDate.Enabled = false;
                dtTreatmentTime.Enabled = false;
                numTreatmentDuration.Enabled = false;
            }
        }*/

        public EditInitialTreatmentForm(int PatientId, int InitialTreatmentId, bool ViewOnly = false, int TreatmentTypeId = 0, DateTime? TreatmentDate = null) {

            _viewOnly = ViewOnly;

            if (InitialTreatmentId == 0 && PatientId == 0)
                throw new ArgumentException("PatientId and InitialTreatmentId cannot both be 0.");

            InitializeComponent();

            //Populate dropdownlists
            populateDropDownLists();

            //Populate form
            loadInitialTreatment(PatientId, InitialTreatmentId, TreatmentTypeId, TreatmentDate);

            //Some form controls are not for therapists
            if (Program.LogonUser.UserType == UserType.Therapist) {
                btnOpenToTherapist.Visible = false;
                drpTherapist.Enabled = false;
                drpTreatmentType.Enabled = false;
                dtTreatmentDate.Enabled = false;
                dtTreatmentTime.Enabled = false;
                numTreatmentDuration.Enabled = false;
            }

            if (InitialTreatmentId == 0) {
                btnSaveAsTemplate.Visible = false;
            } else {
                btnSaveAsTemplate.Visible = true;
            }
        }

        private void populateDropDownLists() {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    /*
                    IList<TherapyType> lstTreatmentType = session.QueryOver<TherapyType>().List<TherapyType>();
                    //Add an empty TherapyType to the dropdown
                    lstTreatmentType.Insert(0, new TherapyType());
                    drpTreatmentType.DataSource = lstTreatmentType;
                    drpTreatmentType.ValueMember = "Id";
                    drpTreatmentType.DisplayMember = "TherapyTypeName";

                    IList<User> lstTherapist = session.QueryOver<User>().List<User>();
                    for(int i = lstTherapist.Count - 1; i >= 0; i--) {
                        User usr = lstTherapist[i];
                        if(usr.UserType != UserType.Therapist) {
                            lstTherapist.Remove(usr);
                        }
                    }
                    //Add an empty Therapist to the dropdown
                    lstTherapist.Insert(0, new User());
                    drpTherapist.DataSource = lstTherapist;
                    drpTherapist.ValueMember = "Id";
                    drpTherapist.DisplayMember = "DisplayName";
                    */

                    SimpleListItem aliasListItem = null;
                    SimpleUserItem aliasUserItem = null;

                    User aUser = null;
                    UserTitle aTitle = null;

                    var qryTreatmentType = session.QueryOver<TherapyType>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.TherapyTypeName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .Future<SimpleListItem>();

                    var qryTherapist = session.QueryOver<User>(() => aUser)
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

                    //Treatment Types
                    List<SimpleListItem> lstTreatmentType = qryTreatmentType.ToList();
                    lstTreatmentType.Insert(0, new SimpleListItem());
                    drpTreatmentType.DataSource = lstTreatmentType;
                    drpTreatmentType.ValueMember = "Id";
                    drpTreatmentType.DisplayMember = "Text";

                    //Therapist
                    List<SimpleUserItem> lstTherapist = qryTherapist.ToList();
                    lstTherapist.Insert(0, new SimpleUserItem());
                    drpTherapist.DataSource = lstTherapist;
                    drpTherapist.ValueMember = "Id";
                    drpTherapist.DisplayMember = "DisplayName";

                    tr.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load InitialTreatment. " + ex.Message);
                Program.log.Error("Failed to load InitialTreatment.", ex);
            }
        }

        /*
        private void loadInitialTreatment() {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                {

                    if(EditingInitialTreatment.Id == default(int)) {
                        EditingInitialTreatment.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInitialTreatment.CreatedTime = DateTime.Now;
                    } else {
                        EditingInitialTreatment = session.Get<InitialTreatment>(EditingInitialTreatment.Id);
                    }

                    //Get Treatment Note Title setting
                    JCService service = new JCService(session, Program.LogonUser);
                    SystemSetting treatmentNoteTitleSetting = service.GetSettingBy("Treatment Note Title");
                    if (treatmentNoteTitleSetting != null) {
                        _treatmentNoteTitle = treatmentNoteTitleSetting.Value;
                    }

                    populateInitialTreatmentDetail(EditingInitialTreatment);
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load InitialTreatment. " + ex.Message);
                Program.log.Error("Failed to load InitialTreatment.", ex);
            }
        }*/

        private void loadInitialTreatment(int patientId, int initialTreatmentId, int treatmentTypeId = 0, DateTime? treatmentDate = null) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {

                    if (initialTreatmentId == default(int)) {
                        EditingInitialTreatment = new InitialTreatment();
                        EditingInitialTreatment.Patient = session.Load<Patient>(patientId);
                        EditingInitialTreatment.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInitialTreatment.CreatedTime = DateTime.Now;
                        if (treatmentTypeId != 0) {
                            EditingInitialTreatment.TreatmentType = session.Load<TherapyType>(treatmentTypeId);
                        }
                        if (treatmentDate != null && treatmentDate.HasValue) {
                            EditingInitialTreatment.TreatmentTime = treatmentDate.Value;
                        }
                    } else {
                        EditingInitialTreatment = session.QueryOver<InitialTreatment>()
                            .Fetch(x => x.Patient).Eager
                            .Fetch(x => x.TreatmentType).Eager
                            .Fetch(x => x.Therapist).Eager
                            .Fetch(x => x.Therapist.Title).Eager
                            .Where(x => x.Id == initialTreatmentId)
                            .SingleOrDefault<InitialTreatment>();
                        _originalTreatmentType = EditingInitialTreatment.TreatmentType.TherapyTypeName;
                    }

                    //Get Treatment Note Title setting
                    JCService service = new JCService(session, Program.LogonUser);
                    SystemSetting treatmentNoteTitleSetting = service.GetSettingBy("Treatment Note Title");
                    if (treatmentNoteTitleSetting != null) {
                        _treatmentNoteTitle = treatmentNoteTitleSetting.Value;
                    }

                    populateInitialTreatmentDetail(EditingInitialTreatment);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load InitialTreatment. " + ex.Message);
                Program.log.Error("Failed to load InitialTreatment.", ex);
            }
        }

        private void populateInitialTreatmentDetail(InitialTreatment initialTreatment) {
            txtPatientName.Text = initialTreatment.Patient.DisplayName;
            setTreatmentType(initialTreatment.TreatmentType);
            setTherapist(initialTreatment.Therapist);
            dtTreatmentDate.Value = initialTreatment.TreatmentTime;
            dtTreatmentTime.Value = initialTreatment.TreatmentTime;
            numTreatmentDuration.Value = initialTreatment.TreatmentDurationMinutes;

            setOpenToTherapist(initialTreatment.OpenToTherapist);

            picComplete.Visible = initialTreatment.IsComplete;
            btnComplete.Visible = !initialTreatment.IsComplete;
        }

        private void setTreatmentType(TherapyType type){
            if(type == null) return;
            
            drpTreatmentType.SelectedValue = type.Id;
        }

        private void setTherapist(User therapist) {
            if(therapist == null) return;

            drpTherapist.SelectedValue = therapist.Id;
        }

        private void updateInitialTreatmentInput(InitialTreatment initialTreatment) {
            initialTreatment.TreatmentTime = new DateTime(dtTreatmentDate.Value.Year,
                dtTreatmentDate.Value.Month,
                dtTreatmentDate.Value.Day,
                dtTreatmentTime.Value.Hour,
                dtTreatmentTime.Value.Minute,
                0);
            initialTreatment.TreatmentDurationMinutes = (int)numTreatmentDuration.Value;
            initialTreatment.OpenToTherapist = getOpenToTherapist();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            save(false);
        }

        private void save(bool setComplete) {
            if(EditingInitialTreatment != null) {
                ISessionFactory sessionFactory = null;
                try {
                    sessionFactory = SessionFactory.GetSessionFactory();
                    using(ISession session = sessionFactory.OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {

                        if(EditingInitialTreatment.Id == default(int)) {
                            EditingInitialTreatment.IsTestData = Program.LogonUser.IsTestData;
                            EditingInitialTreatment.CreatedBy = Program.LogonUser.DisplayName;
                            EditingInitialTreatment.CreatedTime = DateTime.Now;
                        } else {
                            EditingInitialTreatment = session.Get<InitialTreatment>(EditingInitialTreatment.Id);
                            EditingInitialTreatment.UpdatedTime = DateTime.Now;
                            EditingInitialTreatment.UpdatedBy = Program.LogonUser.DisplayName;
                        }

                        //update with dropdown selection
                        if(drpTherapist.SelectedValue != null)
                            EditingInitialTreatment.Therapist =
                                session.Get<User>(int.Parse(drpTherapist.SelectedValue.ToString()));
                        if(drpTreatmentType.SelectedValue != null)
                            EditingInitialTreatment.TreatmentType =
                                session.Get<TherapyType>(int.Parse(drpTreatmentType.SelectedValue.ToString()));

                        //update complete status
                        if(setComplete) EditingInitialTreatment.IsComplete = setComplete;

                        //Update with other inputs
                        updateInitialTreatmentInput(EditingInitialTreatment);

                        if(EditingInitialTreatment.HasMandatoryValues()) {

                            JCService service = new JCService(session, Program.LogonUser);

                            //When changing treatment type, the existing followup details should be removed.
                            if (EditingInitialTreatment.Id != default(int) && _originalTreatmentType != EditingInitialTreatment.TreatmentType.TherapyTypeName) {
                                service.DeleteAllFollowUpDetails(EditingInitialTreatment.Id, _originalTreatmentType);
                            }

                            //Validate Insurer Treatment Per Day
                            /*
                            if (EditingInitialTreatment.Patient.Insurer != null) {

                                Insurer insurer = session.Merge<Insurer>(EditingInitialTreatment.Patient.Insurer);
                                TherapyType treatmentType = EditingInitialTreatment.TreatmentType;

                                // 1) ========== Check number of treatments per insurer ========================
                                //Get the number of treatments except this treatment
                                double numTreatment = service.GetNumberOfTreatmentFor(EditingInitialTreatment.Therapist.Id,
                                    insurer.Id,
                                    EditingInitialTreatment.TreatmentTime,
                                    EditingInitialTreatment.Id,
                                    0);
                                //Get the number of this treatment
                                double numThisTreatment = (treatmentType.EnableMinutesPerTreatment ?
                                    (double)EditingInitialTreatment.TreatmentDurationMinutes / treatmentType.MinutesPerTreatment
                                    : 1);
                                //Total number of treatment will be
                                double numTotalTreatment = numTreatment + numThisTreatment;

                                if (insurer.MaxTreatmentPerDay > 0 && numTotalTreatment > insurer.MaxTreatmentPerDay) {
                                    MessageBox.Show(this,
                                        string.Format("You are trying to assign {0:0.##} treatments for this therapist for insurer [{1}], which is more than the maximum number [{2}] allowed. \n\nYou cannot assign more on the same day.",
                                            numTotalTreatment,
                                            insurer.DisplayName,
                                            insurer.MaxTreatmentPerDay
                                        ),
                                        "Max Number of Treatments Reached",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);

                                    tr.Rollback();
                                    return;
                                } else if(insurer.WarnTreatmentPerDay > 0 && numTotalTreatment > insurer.WarnTreatmentPerDay) {
                                    MessageBox.Show(this,
                                        string.Format("This is a warning...\n\nYou can still go ahead to add this assignment,\nbut for the same therapist and insurer [{0}],\nyou can only assign {1:0.##} more treatments for the same day.",
                                            insurer.DisplayName,
                                            insurer.MaxTreatmentPerDay - numTotalTreatment
                                        ),
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                }

                                // 2) =========== Check number of treatments per treatment type, insurer =========================
                                if (treatmentType.EnableMaxTreatmentsPerDayPerInsurer) {
                                    //Get the number of treatments except this treatment
                                    numTreatment = service.GetNumberOfTreatmentFor(treatmentType,
                                        EditingInitialTreatment.Therapist,
                                        insurer,
                                        EditingInitialTreatment.TreatmentTime,
                                        EditingInitialTreatment.Id,
                                        0);
                                    //Get the number of this treatment
                                    numThisTreatment = (treatmentType.EnableMinutesPerTreatment ?
                                        (double)EditingInitialTreatment.TreatmentDurationMinutes / treatmentType.MinutesPerTreatment
                                        : 1);
                                    //Total number of treatment will be
                                    numTotalTreatment = numTreatment + numThisTreatment;

                                    double numAvailable = treatmentType.MaxTreatmentsPerDayPerInsurer - numTotalTreatment;
                                    if (numAvailable < 0) {
                                        MessageBox.Show(this,
                                            string.Format("You are trying to assign {0:0.##} {1} treatments for this therapist for insurer [{2}], which is more than the maximum number [{3}].",
                                                numTotalTreatment,
                                                treatmentType.DisplayName,
                                                insurer.DisplayName,
                                                treatmentType.MaxTreatmentsPerDayPerInsurer
                                            ),
                                            string.Format("Max Number of Treatments for {0} Reached", treatmentType.DisplayName),
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Stop);
                                        tr.Rollback();
                                        return;
                                    } else if (numAvailable < 3) {//Warning when close to Max
                                        string msg = string.Format("This is a warning...\n\n{0} has a maximum limit [{1}] for per therapist per insurer per day.\n\n",
                                                treatmentType.DisplayName,
                                                treatmentType.MaxTreatmentsPerDayPerInsurer);
                                        if (numAvailable < 0.000001) {
                                            msg += "The maximum limit has been reached.";
                                        } else {
                                            msg += string.Format("You can assign {0:0.##} more.", numAvailable);
                                        }

                                        MessageBox.Show(this,
                                            msg,
                                            "Warning",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            */

                            //Validate number of treatments per user per day per treatment type
                            TherapyType treatmentType = EditingInitialTreatment.TreatmentType;

                            if (treatmentType.EnableMaxTreatmentsPerDay) {

                                //Get the number of treatments except this treatment
                                double numTreatment = service.GetNumberOfTreatmentFor(treatmentType,
                                    EditingInitialTreatment.Therapist,
                                    EditingInitialTreatment.TreatmentTime,
                                    EditingInitialTreatment.Id,
                                    0);
                                //Get the number of this treatment
                                double numThisTreatment = (treatmentType.EnableMinutesPerTreatment ?
                                    (double)EditingInitialTreatment.TreatmentDurationMinutes / treatmentType.MinutesPerTreatment
                                    : 1);
                                //Total number of treatment will be
                                double numTotalTreatment = numTreatment + numThisTreatment;

                                double numAvailable = treatmentType.MaxTreatmentsPerDay - numTotalTreatment;
                                if (numAvailable < 0) {
                                    MessageBox.Show(this,
                                        string.Format("You are trying to assign {0:0.##} {1} treatments for this therapist, which is more than the maximum number [{2}].",
                                            numTotalTreatment,
                                            treatmentType.DisplayName,
                                            treatmentType.MaxTreatmentsPerDay
                                        ),
                                        string.Format("Max Number of Treatments for {0} Reached", treatmentType.DisplayName),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                                    tr.Rollback();
                                    return;
                                } else if (numAvailable < 3) {//Warning when close to Max
                                    string msg = string.Format("This is a warning...\n\n{0} has a maximum limit [{1}] for per therapist per day.\n\n",
                                            treatmentType.DisplayName,
                                            treatmentType.MaxTreatmentsPerDay);
                                    if (numAvailable < 0.000001) {
                                        msg += "The maximum limit has been reached.";
                                    } else {
                                        msg += string.Format("You can assign {0:0.##} more.", numAvailable);
                                    }

                                    MessageBox.Show(this,
                                        msg,
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                }
                            }

                            #region Rewrote using JCService.CheckPractitionerAvailability function below
                            /*
                            //Check if it's holiday
                            if (CanadaHolidays.IsHoliday(EditingInitialTreatment.TreatmentTime.Date)) {
                                if (DialogResult.Yes != MessageBox.Show(this,
                                    string.Format(Constants.WARNING_HOLIDAY, EditingInitialTreatment.TreatmentTime.Date),
                                    "Holiday Warning",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning)) {
                                    dtTreatmentTime.Focus();
                                    dtTreatmentTime.Select();

                                    tr.Rollback();
                                    return;
                                }
                            }

                            //Check if the doctor is available through the Google Calendar
                            if (!DoctorCalendarHelper.IsDoctorAvailable(this,
                                EditingInitialTreatment.Therapist.DisplayName,
                                EditingInitialTreatment.Therapist.GoogleCalendarId,
                                EditingInitialTreatment.TreatmentTime.Date)) {

                                tr.Rollback();
                                return;
                            }

                            //Add to doctor's calendar
                            if (!DoctorCalendarHelper.AddToCalendar(this,
                                EditingInitialTreatment.Therapist.GoogleCalendarId,
                                EditingInitialTreatment.TreatmentTime,
                                EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes),
                                "Initial Treatment Assignment - " + EditingInitialTreatment.Id.ToString(),
                                "Initial Treatment Assignment - " + EditingInitialTreatment.Id.ToString())) {

                                tr.Rollback();
                                return;
                            }

                            //Check if the practitioner is off
                            if (Program.config.EnablePractitionerCalendar) {

                                bool isAvailable = false;

                                IList<UserWeeklyAvailability> avails = session.QueryOver<UserWeeklyAvailability>()
                                    .Where(x => x.Practitioner.Id == EditingInitialTreatment.Therapist.Id)
                                    .Where(x => x.WeekDay == EditingInitialTreatment.TreatmentTime.DayOfWeek)
                                    .List<UserWeeklyAvailability>();

                                foreach (UserWeeklyAvailability avail in avails) {
                                    if (avail.FullDayAvailable) {
                                        isAvailable = true;
                                        break;
                                    }else{
                                        decimal treatmentStartHour = (decimal)EditingInitialTreatment.TreatmentTime.Hour + (decimal)EditingInitialTreatment.TreatmentTime.Minute / 60;
                                        decimal treatmentEndHour = (decimal)EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).Hour
                                            + (decimal)EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).Minute / 60;

                                        if (avail.StartHour <= treatmentStartHour && avail.EndHour >= treatmentEndHour) {
                                            isAvailable = true;
                                            break;
                                        }
                                    }
                                }

                                if (isAvailable) {

                                    //Continue to check time offs
                                    DateTime dtCheckStart = EditingInitialTreatment.TreatmentTime;
                                    DateTime dtCheckEnd = EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes);

                                    int timeOffCount = session.QueryOver<UserTimeOff>()
                                        .Where(x => x.Practitioner.Id == EditingInitialTreatment.Therapist.Id)
                                        .Where(x => (x.StartTime >= dtCheckStart && x.EndTime <= dtCheckEnd) ||
                                                    (x.StartTime < dtCheckStart && x.EndTime > dtCheckStart) ||
                                                    (x.StartTime < dtCheckEnd && x.EndTime > dtCheckEnd))
                                        .RowCount();

                                    if (timeOffCount > 0) {
                                        isAvailable = false;
                                    }

                                    if (!isAvailable) {
                                        if (DialogResult.OK != MessageBox.Show(this,
                                            Constants.WARNING_TIME_OFF,
                                            "Time Off Warning",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Warning)) {
                                            dtTreatmentTime.Focus();
                                            dtTreatmentTime.Select();

                                            tr.Rollback();
                                            return;
                                        }
                                    }

                                } else {
                                    if (DialogResult.OK != MessageBox.Show(this,
                                        Constants.WARNING_NOT_WORKING_TIME,
                                        "Working Hour Warning",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Warning)) {
                                        dtTreatmentTime.Focus();
                                        dtTreatmentTime.Select();

                                        tr.Rollback();
                                        return;
                                    }
                                }
                            }*/
                            #endregion Rewrote using JCService.CheckPractitionerAvailability function below

                            #region Check practitioner availability

                            List<JCService.AvailableStatus> pracStatus = service.CheckPractitionerAvailability(
                                EditingInitialTreatment.Therapist,
                                EditingInitialTreatment.TreatmentTime,
                                EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes),
                                JCService.AvailabilityCheckType.Treatment,
                                EditingInitialTreatment.Id,
                                !EditingInitialTreatment.TreatmentType.AllowTimeOverlap);

                            if (pracStatus.Contains(JCService.AvailableStatus.Holiday)) {
                                if (DialogResult.Yes != MessageBox.Show(this,
                                    string.Format(Constants.WARNING_HOLIDAY, EditingInitialTreatment.TreatmentTime.Date),
                                    "Holiday Warning",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning)) {
                                    dtTreatmentTime.Focus();
                                    dtTreatmentTime.Select();

                                    tr.Rollback();
                                    return;
                                }
                            }

                            if (Program.config.EnablePractitionerCalendar) {
                                if (pracStatus.Contains(JCService.AvailableStatus.NoneWorkingHour)) {
                                    if (DialogResult.OK != MessageBox.Show(this,
                                        Constants.WARNING_NOT_WORKING_TIME,
                                        "Working Hour Warning",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Warning)) {
                                        dtTreatmentTime.Focus();
                                        dtTreatmentTime.Select();

                                        tr.Rollback();
                                        return;
                                    }
                                }

                                if (pracStatus.Contains(JCService.AvailableStatus.TimeOff)) {
                                    if (DialogResult.OK != MessageBox.Show(this,
                                        Constants.WARNING_TIME_OFF,
                                        "Time Off Warning",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Warning)) {
                                        dtTreatmentTime.Focus();
                                        dtTreatmentTime.Select();

                                        tr.Rollback();
                                        return;
                                    }
                                }
                            }

                            if (pracStatus.Contains(JCService.AvailableStatus.NotAvailable)) {
                                if (DialogResult.OK != MessageBox.Show(this,
                                    Constants.WARNING_NOT_AVAILABLE_TREATMENT,
                                    "Not Available Warning",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Warning)) {
                                    dtTreatmentTime.Focus();
                                    dtTreatmentTime.Select();

                                    tr.Rollback();
                                    return;
                                }
                            }

                            #endregion Check practitioner availability

                            //Save InitialTreatment
                            session.SaveOrUpdate(EditingInitialTreatment);

                            //Clear orphan TreatmentDetails
                            service.DeleteOrphanTreatmentDetails(EditingInitialTreatment);

                            //Save Treatment Detail
                            if(pnlTreatmentDetail.Controls.Count > 0) {
                                ITreatmentDetailUserControl uc = pnlTreatmentDetail.Controls[0] as ITreatmentDetailUserControl;
                                uc.Save(session);
                            }

                            tr.Commit();
                            MessageBox.Show(this, "Information Updated.");
                            DialogResult = DialogResult.OK;
                            this.Close();
                        } else {
                            MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                                "Missing Values",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            return;
                        }
                    }
                } catch(Exception ex) {
                    MessageBox.Show(this, "Failed to save Initial Treatment. " + ex.Message);
                    Program.log.Error("Failed to save Initial Treatment.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        //Populate Treatment Details control based on selected treatment type
        private void drpTreatmentType_SelectedIndexChanged(object sender, EventArgs e) {

            //Clear Treatment Detail control
            pnlTreatmentDetail.Controls.Clear();
            //EditingTreatmentDetail = null;
            
            //Return if nothing selected
            if(drpTreatmentType.SelectedValue == null) return;

            //Populate Treatment Detail control
            int selectedTypeId = int.Parse(drpTreatmentType.SelectedValue.ToString());
            if(selectedTypeId != default(int)) {
                TherapyType selectedTherapyType = null;

                ISessionFactory sessionFactory = null;
                try {
                    //Get selected TherapyType
                    sessionFactory = SessionFactory.GetSessionFactory();
                    using(ISession session = sessionFactory.OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        selectedTherapyType = session.Get<TherapyType>(selectedTypeId);
                        EditingInitialTreatment.TreatmentType = selectedTherapyType;
                        tr.Commit();
                    }

                    //Create UserControl
                    UserControl uc = UCTreatmentDetailFactory.CreateTreatmentDetailUserControl(EditingInitialTreatment,
                        selectedTherapyType.TherapyTypeName, _viewOnly);

                    if(uc != null) {
                        pnlTreatmentDetail.Controls.Add(uc);
                    }

                } catch(Exception ex) {
                    MessageBox.Show(this, "Failed to retrieve Initial Treatment Details. " + ex.Message);
                    Program.log.Error("Failed to retrieve Initial Treatment Details.", ex);
                }
            }
        }

        #region Test manual printing
        //Test printing
        Bitmap memImage;
        private void btnPrint_Click(object sender, EventArgs e) {
            if(DialogResult.OK == dlgPrinter.ShowDialog()) {
                //Print the active content from panel
                /*
                Graphics gp = pnlTreatmentDetail.CreateGraphics();
                Size s = pnlTreatmentDetail.PreferredSize;
                memImage = new Bitmap(s.Width, s.Height, gp);
                Graphics memGp = Graphics.FromImage(memImage);

                IntPtr dc1 = gp.GetHdc();
                IntPtr dc2 = memGp.GetHdc();
                BitBlt(dc2, 0, 0, pnlTreatmentDetail.PreferredSize.Width, pnlTreatmentDetail.PreferredSize.Height,
                    dc1, 0, 0, 13369376);
                gp.ReleaseHdc(dc1);
                memGp.ReleaseHdc(dc2);
                */

                Graphics gp = pnlTreatmentDetail.CreateGraphics();
                Size s = pnlTreatmentDetail.PreferredSize;
                
                memImage = new Bitmap(s.Width, s.Height, gp);
                Graphics memGp = Graphics.FromImage(memImage);

                Point pDest = new Point(0, 0);
                memGp.DrawLine(new Pen(Color.Black), pDest.X, pDest.Y, memImage.Width, memImage.Height);

                Font font = new Font("Arial", 10);
                Point point = new Point(0, 0);
                memGp.DrawString("Test string", font, Brushes.Black, point);

                //pd.Print();
                pd.PrinterSettings = dlgPrinter.PrinterSettings;
                dlgPrintPreview.ShowDialog();
            }
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            e.Graphics.DrawImage(memImage, 0, 0);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        #endregion Test manual printing

        private void btnPrintToWord_Click(object sender, EventArgs e) {
            openOrPrintToWord(true);
        }

        //Create the word file and return the file name
        private string createPrintOutFile(string rootPath) {

            string strPatientName = EditingInitialTreatment.Patient.LastName.Trim();
            string strFolderPath = FileExtender.ConcatPhysicalPath(rootPath, strPatientName);

            if(!Directory.Exists(strFolderPath)) {
                Directory.CreateDirectory(strFolderPath);
            }

            string strTemp = FileExtender.ConcatPhysicalPath(strFolderPath,
                string.Format("{0}_{1}_{2}_",
                strPatientName,
                EditingTreatmentDetail.GetType().Name,
                EditingInitialTreatment.TreatmentTime.ToString("yyyyMMMdd")));

            int i = 1;
            while(File.Exists(string.Format("{0}{1}.docx",strTemp,i))) {
                i++;
                if(i > 1000) {
                    throw new ApplicationException("You have created too many print out for this patient. Please remove the old ones and try again.");
                }
            }

            string strFilePath = string.Format("{0}{1}.docx", strTemp, i);

            return strFilePath;
        }

        private void buildChiropracticRequest(ProcessRequest req, object obj) {
            UCChiropracticDetail ucDetail = obj as UCChiropracticDetail;
            ChiropracticDetail detail = ucDetail.EditingChiropracticDetail;

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

            //Patient patient = detail.InitialTreatment.Patient;
            Patient patient = EditingInitialTreatment.Patient;

            //0.1) Patient name
            //req.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            req.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            req.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            req.AddWordReplacement("{file number}", patient.FileNumber);

            #region Page 1
            req.AddWordReplacement("{chief complaint}", detail.ChiefComplaint);
            req.AddWordReplacement("{history of condition}", detail.HistoryOfCondition);
            req.AddWordReplacement("{aggravating factors}", detail.AggravatingFactors);
            req.AddWordReplacement("{associated symptoms}", detail.AssociatedSymptoms);
            req.AddWordReplacement("{relieving factors}", detail.RelievingFactors);
            req.AddWordReplacement("{previous tx}", detail.PreviousTX);
            req.AddWordReplacement("{medical}", detail.Medical);
            req.AddWordReplacement("{family history}", detail.FamilyHistory);
            req.AddWordReplacement("{lifestyle}", detail.LifeStyle);
            req.AddWordReplacement("{past illness}", detail.PastIllness);
            req.AddWordReplacement("{caregiver number}", detail.NumOfCareGiverNeeded.ToString());
            req.AddWordReplacement("{employer}", detail.Employer);
            req.AddWordReplacement("{last work date}", (detail.LastWorkDate.HasValue ? detail.LastWorkDate.Value.ToString(Constants.DATE_FORMAT) : ""));
            req.AddWordReplacement("{job title}", detail.JobTitleAndDuties);
            req.AddWordReplacement("{education}", detail.Education);
            req.AddWordReplacement("{dominant hand}", detail.DominantHand.ToString());
            #endregion Page 1

            #region Page 2
            //UPPER EXTREMITY
            req.Replacements.Add("{u011}", (detail.ShoulderYergason == LeftRight.Both || detail.ShoulderYergason == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u012}", (detail.ShoulderYergason == LeftRight.Both || detail.ShoulderYergason == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u013}", (detail.ElbowVarusStress == LeftRight.Both || detail.ElbowVarusStress == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u014}", (detail.ElbowVarusStress == LeftRight.Both || detail.ElbowVarusStress == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u015}", (detail.WristHandFinkelstein == LeftRight.Both || detail.WristHandFinkelstein == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u016}", (detail.WristHandFinkelstein == LeftRight.Both || detail.WristHandFinkelstein == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u021}", (detail.ShoulderSpeed == LeftRight.Both || detail.ShoulderSpeed == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u022}", (detail.ShoulderSpeed == LeftRight.Both || detail.ShoulderSpeed == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u023}", (detail.ElbowVargusStress == LeftRight.Both || detail.ElbowVargusStress == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u024}", (detail.ElbowVargusStress == LeftRight.Both || detail.ElbowVargusStress == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u025}", (detail.WristHandFroment == LeftRight.Both || detail.WristHandFroment == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u026}", (detail.WristHandFroment == LeftRight.Both || detail.WristHandFroment == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u031}", (detail.ShoulderDropArm == LeftRight.Both || detail.ShoulderDropArm == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u032}", (detail.ShoulderDropArm == LeftRight.Both || detail.ShoulderDropArm == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u033}", (detail.ElbowCozen == LeftRight.Both || detail.ElbowCozen == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u034}", (detail.ElbowCozen == LeftRight.Both || detail.ElbowCozen == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u035}", (detail.WristHandPinchGrip == LeftRight.Both || detail.WristHandPinchGrip == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u036}", (detail.WristHandPinchGrip == LeftRight.Both || detail.WristHandPinchGrip == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u041}", (detail.ShoulderEmptyCan == LeftRight.Both || detail.ShoulderEmptyCan == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u042}", (detail.ShoulderEmptyCan == LeftRight.Both || detail.ShoulderEmptyCan == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u043}", (detail.ElbowMill == LeftRight.Both || detail.ElbowMill == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u044}", (detail.ElbowMill == LeftRight.Both || detail.ElbowMill == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u045}", (detail.WristHandPhalen == LeftRight.Both || detail.WristHandPhalen == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u046}", (detail.WristHandPhalen == LeftRight.Both || detail.WristHandPhalen == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u051}", (detail.ShoulderOBrien == LeftRight.Both || detail.ShoulderOBrien == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u052}", (detail.ShoulderOBrien == LeftRight.Both || detail.ShoulderOBrien == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u053}", (detail.ElbowGolferElbow == LeftRight.Both || detail.ElbowGolferElbow == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u054}", (detail.ElbowGolferElbow == LeftRight.Both || detail.ElbowGolferElbow == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u055}", (detail.WristHandReversePhalen == LeftRight.Both || detail.WristHandReversePhalen == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u056}", (detail.WristHandReversePhalen == LeftRight.Both || detail.WristHandReversePhalen == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u061}", (detail.ShoulderNeerImpingement == LeftRight.Both || detail.ShoulderNeerImpingement == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u062}", (detail.ShoulderNeerImpingement == LeftRight.Both || detail.ShoulderNeerImpingement == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u071}", (detail.ShoulderKennedyHawkins == LeftRight.Both || detail.ShoulderKennedyHawkins == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u072}", (detail.ShoulderKennedyHawkins == LeftRight.Both || detail.ShoulderKennedyHawkins == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u075}", (detail.WristHandTinelAtWrist == LeftRight.Both || detail.WristHandTinelAtWrist == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u076}", (detail.WristHandTinelAtWrist == LeftRight.Both || detail.WristHandTinelAtWrist == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u081}", (detail.ShoulderHornblower == LeftRight.Both || detail.ShoulderHornblower == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u082}", (detail.ShoulderHornblower == LeftRight.Both || detail.ShoulderHornblower == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u083}", (detail.ElbowMedianNerveResistedPronationEX == LeftRight.Both || detail.ElbowMedianNerveResistedPronationEX == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u084}", (detail.ElbowMedianNerveResistedPronationEX == LeftRight.Both || detail.ElbowMedianNerveResistedPronationEX == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u085}", (detail.WristHandAllen == LeftRight.Both || detail.WristHandAllen == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u086}", (detail.WristHandAllen == LeftRight.Both || detail.WristHandAllen == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u091}", (detail.ShoulderLiftOff == LeftRight.Both || detail.ShoulderLiftOff == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u092}", (detail.ShoulderLiftOff == LeftRight.Both || detail.ShoulderLiftOff == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u093}", (detail.ElbowMedianNerveResistedFLSupination == LeftRight.Both || detail.ElbowMedianNerveResistedFLSupination == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u094}", (detail.ElbowMedianNerveResistedFLSupination == LeftRight.Both || detail.ElbowMedianNerveResistedFLSupination == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u095}", (detail.WristHandBunnelLittler == LeftRight.Both || detail.WristHandBunnelLittler == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u096}", (detail.WristHandBunnelLittler == LeftRight.Both || detail.WristHandBunnelLittler == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u101}", (detail.ShoulderAnteriorSlide == LeftRight.Both || detail.ShoulderAnteriorSlide == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u102}", (detail.ShoulderAnteriorSlide == LeftRight.Both || detail.ShoulderAnteriorSlide == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u103}", (detail.ElbowMedianNerveResistedFLofPIP == LeftRight.Both || detail.ElbowMedianNerveResistedFLofPIP == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u104}", (detail.ElbowMedianNerveResistedFLofPIP == LeftRight.Both || detail.ElbowMedianNerveResistedFLofPIP == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u111}", (detail.ShoulderLoadShift == LeftRight.Both || detail.ShoulderLoadShift == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u112}", (detail.ShoulderLoadShift == LeftRight.Both || detail.ShoulderLoadShift == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u121}", (detail.ShoulderCrank == LeftRight.Both || detail.ShoulderCrank == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u122}", (detail.ShoulderCrank == LeftRight.Both || detail.ShoulderCrank == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u125}", (detail.WristHandGripStrength == LeftRight.Both || detail.WristHandGripStrength == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u126}", (detail.WristHandGripStrength == LeftRight.Both || detail.WristHandGripStrength == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u133}", (detail.ElbowAINResistedPronationFL == LeftRight.Both || detail.ElbowAINResistedPronationFL == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u134}", (detail.ElbowAINResistedPronationFL == LeftRight.Both || detail.ElbowAINResistedPronationFL == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u135}", (detail.WristHandPinchStrength == LeftRight.Both || detail.WristHandPinchStrength == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u136}", (detail.WristHandPinchStrength == LeftRight.Both || detail.WristHandPinchStrength == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u141}", (detail.ShoulderAnteriorApprehension == LeftRight.Both || detail.ShoulderAnteriorApprehension == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u142}", (detail.ShoulderAnteriorApprehension == LeftRight.Both || detail.ShoulderAnteriorApprehension == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u151}", (detail.ShoulderJobeRelocation == LeftRight.Both || detail.ShoulderJobeRelocation == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u152}", (detail.ShoulderJobeRelocation == LeftRight.Both || detail.ShoulderJobeRelocation == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u161}", (detail.ShoulderPosteriorApprehension == LeftRight.Both || detail.ShoulderPosteriorApprehension == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u162}", (detail.ShoulderPosteriorApprehension == LeftRight.Both || detail.ShoulderPosteriorApprehension == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u163}", (detail.ElbowRadialNerveResistedSupination == LeftRight.Both || detail.ElbowRadialNerveResistedSupination == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u164}", (detail.ElbowRadialNerveResistedSupination == LeftRight.Both || detail.ElbowRadialNerveResistedSupination == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u173}", (detail.ElbowRadialNerveResistedEXofThirdFinger == LeftRight.Both || detail.ElbowRadialNerveResistedEXofThirdFinger == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u174}", (detail.ElbowRadialNerveResistedEXofThirdFinger == LeftRight.Both || detail.ElbowRadialNerveResistedEXofThirdFinger == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u181}", (detail.ShoulderSupraspinatus == LeftRight.Both || detail.ShoulderSupraspinatus == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u182}", (detail.ShoulderSupraspinatus == LeftRight.Both || detail.ShoulderSupraspinatus == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u191}", (detail.ShoulderInfraspinatus == LeftRight.Both || detail.ShoulderInfraspinatus == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u192}", (detail.ShoulderInfraspinatus == LeftRight.Both || detail.ShoulderInfraspinatus == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u201}", (detail.ShoulderTeresMinor == LeftRight.Both || detail.ShoulderTeresMinor == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u202}", (detail.ShoulderTeresMinor == LeftRight.Both || detail.ShoulderTeresMinor == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u203}", (detail.ElbowUlnarNerveTinelElbow == LeftRight.Both || detail.ElbowUlnarNerveTinelElbow == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u204}", (detail.ElbowUlnarNerveTinelElbow == LeftRight.Both || detail.ElbowUlnarNerveTinelElbow == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u211}", (detail.ShoulderTeresMajor == LeftRight.Both || detail.ShoulderTeresMajor == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u212}", (detail.ShoulderTeresMajor == LeftRight.Both || detail.ShoulderTeresMajor == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u213}", (detail.ElbowUlnarNerveFullElbowFL == LeftRight.Both || detail.ElbowUlnarNerveFullElbowFL == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u214}", (detail.ElbowUlnarNerveFullElbowFL == LeftRight.Both || detail.ElbowUlnarNerveFullElbowFL == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{u221}", (detail.ShoulderSubscapularis == LeftRight.Both || detail.ShoulderSubscapularis == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{u222}", (detail.ShoulderSubscapularis == LeftRight.Both || detail.ShoulderSubscapularis == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            
            //LOWER EXTREMITY
            req.Replacements.Add("{l011}", (detail.HipTrueLegLength == LeftRight.Both || detail.HipTrueLegLength == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l012}", (detail.HipTrueLegLength == LeftRight.Both || detail.HipTrueLegLength == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l013}", (detail.KneeThessaly == LeftRight.Both || detail.KneeThessaly == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l014}", (detail.KneeThessaly == LeftRight.Both || detail.KneeThessaly == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l015}", (detail.FootAnkleAnteriorDrawer == LeftRight.Both || detail.FootAnkleAnteriorDrawer == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l016}", (detail.FootAnkleAnteriorDrawer == LeftRight.Both || detail.FootAnkleAnteriorDrawer == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l021}", (detail.HipApparentLegLength == LeftRight.Both || detail.HipApparentLegLength == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l022}", (detail.HipApparentLegLength == LeftRight.Both || detail.HipApparentLegLength == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l023}", (detail.KneeApleyCompression == LeftRight.Both || detail.KneeApleyCompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l024}", (detail.KneeApleyCompression == LeftRight.Both || detail.KneeApleyCompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l025}", (detail.FootAnklePosteriorDrawer == LeftRight.Both || detail.FootAnklePosteriorDrawer == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l026}", (detail.FootAnklePosteriorDrawer == LeftRight.Both || detail.FootAnklePosteriorDrawer == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l031}", (detail.HipSLR == LeftRight.Both || detail.HipSLR == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l032}", (detail.HipSLR == LeftRight.Both || detail.HipSLR == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l033}", (detail.KneeApleyDistraction == LeftRight.Both || detail.KneeApleyDistraction == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l034}", (detail.KneeApleyDistraction == LeftRight.Both || detail.KneeApleyDistraction == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l035}", (detail.FootAnkleInversionStress1 == LeftRight.Both || detail.FootAnkleInversionStress1 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l036}", (detail.FootAnkleInversionStress1 == LeftRight.Both || detail.FootAnkleInversionStress1 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l041}", (detail.HipThomas == LeftRight.Both || detail.HipThomas == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l042}", (detail.HipThomas == LeftRight.Both || detail.HipThomas == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l043}", (detail.KneePatellarPosition == LeftRight.Both || detail.KneePatellarPosition == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l044}", (detail.KneePatellarPosition == LeftRight.Both || detail.KneePatellarPosition == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l045}", (detail.FootAnkleEversionStress1 == LeftRight.Both || detail.FootAnkleEversionStress1 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l046}", (detail.FootAnkleEversionStress1 == LeftRight.Both || detail.FootAnkleEversionStress1 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l051}", (detail.HipPatrickFabers == LeftRight.Both || detail.HipPatrickFabers == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l052}", (detail.HipPatrickFabers == LeftRight.Both || detail.HipPatrickFabers == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l053}", (detail.KneeWipeTest == LeftRight.Both || detail.KneeWipeTest == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l054}", (detail.KneeWipeTest == LeftRight.Both || detail.KneeWipeTest == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l055}", (detail.FootAnkleForefootAbduction == LeftRight.Both || detail.FootAnkleForefootAbduction == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l056}", (detail.FootAnkleForefootAbduction == LeftRight.Both || detail.FootAnkleForefootAbduction == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l061}", (detail.HipAPSICompression == LeftRight.Both || detail.HipAPSICompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l062}", (detail.HipAPSICompression == LeftRight.Both || detail.HipAPSICompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l063}", (detail.KneeVarusStress == LeftRight.Both || detail.KneeVarusStress == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l064}", (detail.KneeVarusStress == LeftRight.Both || detail.KneeVarusStress == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l065}", (detail.FootAnkleExternalRotation == LeftRight.Both || detail.FootAnkleExternalRotation == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l066}", (detail.FootAnkleExternalRotation == LeftRight.Both || detail.FootAnkleExternalRotation == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l071}", (detail.HipGaenslen == LeftRight.Both || detail.HipGaenslen == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l072}", (detail.HipGaenslen == LeftRight.Both || detail.HipGaenslen == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l073}", (detail.KneeValgusStress == LeftRight.Both || detail.KneeValgusStress == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l074}", (detail.KneeValgusStress == LeftRight.Both || detail.KneeValgusStress == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l075}", (detail.FootAnkleSyndesmosisSqueeze == LeftRight.Both || detail.FootAnkleSyndesmosisSqueeze == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l076}", (detail.FootAnkleSyndesmosisSqueeze == LeftRight.Both || detail.FootAnkleSyndesmosisSqueeze == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l081}", (detail.HipNobelCompression == LeftRight.Both || detail.HipNobelCompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l082}", (detail.HipNobelCompression == LeftRight.Both || detail.HipNobelCompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l083}", (detail.KneePosteriorSagSign == LeftRight.Both || detail.KneePosteriorSagSign == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l084}", (detail.KneePosteriorSagSign == LeftRight.Both || detail.KneePosteriorSagSign == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l085}", (detail.FootAnkleForefootNeuromaSqueeze == LeftRight.Both || detail.FootAnkleForefootNeuromaSqueeze == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l086}", (detail.FootAnkleForefootNeuromaSqueeze == LeftRight.Both || detail.FootAnkleForefootNeuromaSqueeze == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l091}", (detail.HipPsoasStrength == LeftRight.Both || detail.HipPsoasStrength == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l092}", (detail.HipPsoasStrength == LeftRight.Both || detail.HipPsoasStrength == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l093}", (detail.KneeAnteriorDrawer == LeftRight.Both || detail.KneeAnteriorDrawer == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l094}", (detail.KneeAnteriorDrawer == LeftRight.Both || detail.KneeAnteriorDrawer == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l095}", (detail.FootAnklePlanarFasciaTenderness == LeftRight.Both || detail.FootAnklePlanarFasciaTenderness == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l096}", (detail.FootAnklePlanarFasciaTenderness == LeftRight.Both || detail.FootAnklePlanarFasciaTenderness == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l103}", (detail.KneePosteriorDrawer == LeftRight.Both || detail.KneePosteriorDrawer == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l104}", (detail.KneePosteriorDrawer == LeftRight.Both || detail.KneePosteriorDrawer == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l105}", (detail.FootAnkleHoman == LeftRight.Both || detail.FootAnkleHoman == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l106}", (detail.FootAnkleHoman == LeftRight.Both || detail.FootAnkleHoman == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l111}", (detail.HipOber == LeftRight.Both || detail.HipOber == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l112}", (detail.HipOber == LeftRight.Both || detail.HipOber == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l113}", (detail.KneeSlocumIR == LeftRight.Both || detail.KneeSlocumIR == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l114}", (detail.KneeSlocumIR == LeftRight.Both || detail.KneeSlocumIR == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l115}", (detail.FootAnkleThompson == LeftRight.Both || detail.FootAnkleThompson == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l116}", (detail.FootAnkleThompson == LeftRight.Both || detail.FootAnkleThompson == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l123}", (detail.KneeSlocumER == LeftRight.Both || detail.KneeSlocumER == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l124}", (detail.KneeSlocumER == LeftRight.Both || detail.KneeSlocumER == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l125}", (detail.FootAnkleValgusStress == LeftRight.Both || detail.FootAnkleValgusStress == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l126}", (detail.FootAnkleValgusStress == LeftRight.Both || detail.FootAnkleValgusStress == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l131}", (detail.HipEly == LeftRight.Both || detail.HipEly == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l132}", (detail.HipEly == LeftRight.Both || detail.HipEly == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l133}", (detail.KneeLachman == LeftRight.Both || detail.KneeLachman == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l134}", (detail.KneeLachman == LeftRight.Both || detail.KneeLachman == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l135}", (detail.FootAnkleVarusStress == LeftRight.Both || detail.FootAnkleVarusStress == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l136}", (detail.FootAnkleVarusStress == LeftRight.Both || detail.FootAnkleVarusStress == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l141}", (detail.HipYeoman == LeftRight.Both || detail.HipYeoman == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l142}", (detail.HipYeoman == LeftRight.Both || detail.HipYeoman == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l143}", (detail.KneeLateralPivotShift == LeftRight.Both || detail.KneeLateralPivotShift == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l144}", (detail.KneeLateralPivotShift == LeftRight.Both || detail.KneeLateralPivotShift == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l151}", (detail.HipHibb == LeftRight.Both || detail.HipHibb == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l152}", (detail.HipHibb == LeftRight.Both || detail.HipHibb == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l153}", (detail.KneeReversePivotShift == LeftRight.Both || detail.KneeReversePivotShift == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l154}", (detail.KneeReversePivotShift == LeftRight.Both || detail.KneeReversePivotShift == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l155}", (detail.FootAnkleATFL == LeftRight.Both || detail.FootAnkleATFL == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l156}", (detail.FootAnkleATFL == LeftRight.Both || detail.FootAnkleATFL == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l161}", (detail.HipPASICompression == LeftRight.Both || detail.HipPASICompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l162}", (detail.HipPASICompression == LeftRight.Both || detail.HipPASICompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l163}", (detail.KneeNobelCompression == LeftRight.Both || detail.KneeNobelCompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l164}", (detail.KneeNobelCompression == LeftRight.Both || detail.KneeNobelCompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l165}", (detail.FootAnkleCFL == LeftRight.Both || detail.FootAnkleCFL == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l166}", (detail.FootAnkleCFL == LeftRight.Both || detail.FootAnkleCFL == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l171}", (detail.HipTrendelenburg == LeftRight.Both || detail.HipTrendelenburg == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l172}", (detail.HipTrendelenburg == LeftRight.Both || detail.HipTrendelenburg == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l173}", (detail.KneeHyperextension == LeftRight.Both || detail.KneeHyperextension == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l174}", (detail.KneeHyperextension == LeftRight.Both || detail.KneeHyperextension == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l175}", (detail.FootAnkleInversionStress2 == LeftRight.Both || detail.FootAnkleInversionStress2 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l176}", (detail.FootAnkleInversionStress2 == LeftRight.Both || detail.FootAnkleInversionStress2 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l183}", (detail.KneeBounceHome == LeftRight.Both || detail.KneeBounceHome == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l184}", (detail.KneeBounceHome == LeftRight.Both || detail.KneeBounceHome == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l185}", (detail.FootAnkleEversionStress2 == LeftRight.Both || detail.FootAnkleEversionStress2 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l186}", (detail.FootAnkleEversionStress2 == LeftRight.Both || detail.FootAnkleEversionStress2 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l193}", (detail.KneeJointLineTenderness == LeftRight.Both || detail.KneeJointLineTenderness == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l194}", (detail.KneeJointLineTenderness == LeftRight.Both || detail.KneeJointLineTenderness == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l203}", (detail.KneeMcMurray == LeftRight.Both || detail.KneeMcMurray == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l204}", (detail.KneeMcMurray == LeftRight.Both || detail.KneeMcMurray == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l213}", (detail.KneeAnderson == LeftRight.Both || detail.KneeAnderson == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l214}", (detail.KneeAnderson == LeftRight.Both || detail.KneeAnderson == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l221}", (detail.HipOrtolani == LeftRight.Both || detail.HipOrtolani == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l222}", (detail.HipOrtolani == LeftRight.Both || detail.HipOrtolani == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l223}", (detail.KneePatellarCompression == LeftRight.Both || detail.KneePatellarCompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l224}", (detail.KneePatellarCompression == LeftRight.Both || detail.KneePatellarCompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l225}", (detail.OtherTMJ == LeftRight.Both || detail.OtherTMJ == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l226}", (detail.OtherTMJ == LeftRight.Both || detail.OtherTMJ == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l231}", (detail.HipBarlow == LeftRight.Both || detail.HipBarlow == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l232}", (detail.HipBarlow == LeftRight.Both || detail.HipBarlow == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l233}", (detail.KneePatellarApprehension == LeftRight.Both || detail.KneePatellarApprehension == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l234}", (detail.KneePatellarApprehension == LeftRight.Both || detail.KneePatellarApprehension == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l235}", (detail.OtherGAIT == LeftRight.Both || detail.OtherGAIT == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l236}", (detail.OtherGAIT == LeftRight.Both || detail.OtherGAIT == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l241}", (detail.HipGaleazziSign == LeftRight.Both || detail.HipGaleazziSign == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l242}", (detail.HipGaleazziSign == LeftRight.Both || detail.HipGaleazziSign == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l243}", (detail.KneeClarkeSign == LeftRight.Both || detail.KneeClarkeSign == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l244}", (detail.KneeClarkeSign == LeftRight.Both || detail.KneeClarkeSign == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l245}", (detail.OtherBloodPressure == LeftRight.Both || detail.OtherBloodPressure == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l246}", (detail.OtherBloodPressure == LeftRight.Both || detail.OtherBloodPressure == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l251}", (detail.HipTelescoping == LeftRight.Both || detail.HipTelescoping == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l252}", (detail.HipTelescoping == LeftRight.Both || detail.HipTelescoping == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l253}", (detail.KneeHughstonPlica == LeftRight.Both || detail.KneeHughstonPlica == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l254}", (detail.KneeHughstonPlica == LeftRight.Both || detail.KneeHughstonPlica == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l255}", (detail.OtherHeartRate == LeftRight.Both || detail.OtherHeartRate == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l256}", (detail.OtherHeartRate == LeftRight.Both || detail.OtherHeartRate == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{l263}", (detail.KneeMediopatellarPlica == LeftRight.Both || detail.KneeMediopatellarPlica == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l264}", (detail.KneeMediopatellarPlica == LeftRight.Both || detail.KneeMediopatellarPlica == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l265}", (detail.OtherPulse == LeftRight.Both || detail.OtherPulse == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{l266}", (detail.OtherPulse == LeftRight.Both || detail.OtherPulse == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            #endregion Page 2

            #region Page 3
            //NEUROLOGICAL TESTS
            req.Replacements.Add("{n011}", (detail.MyotomesC2 == LeftRight.Both || detail.MyotomesC2 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n012}", (detail.MyotomesC2 == LeftRight.Both || detail.MyotomesC2 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n013}", (detail.DermatomesC4 == LeftRight.Both || detail.DermatomesC4 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n014}", (detail.DermatomesC4 == LeftRight.Both || detail.DermatomesC4 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n021}", (detail.MyotomesC3 == LeftRight.Both || detail.MyotomesC3 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n022}", (detail.MyotomesC3 == LeftRight.Both || detail.MyotomesC3 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n023}", (detail.DermatomesC5 == LeftRight.Both || detail.DermatomesC5 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n024}", (detail.DermatomesC5 == LeftRight.Both || detail.DermatomesC5 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n031}", (detail.MyotomesC4 == LeftRight.Both || detail.MyotomesC4 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n032}", (detail.MyotomesC4 == LeftRight.Both || detail.MyotomesC4 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n033}", (detail.DermatomesC6 == LeftRight.Both || detail.DermatomesC6 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n034}", (detail.DermatomesC6 == LeftRight.Both || detail.DermatomesC6 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n041}", (detail.MyotomesC5 == LeftRight.Both || detail.MyotomesC5 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n042}", (detail.MyotomesC5 == LeftRight.Both || detail.MyotomesC5 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n043}", (detail.DermatomesC7 == LeftRight.Both || detail.DermatomesC7 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n044}", (detail.DermatomesC7 == LeftRight.Both || detail.DermatomesC7 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n051}", (detail.MyotomesC6 == LeftRight.Both || detail.MyotomesC6 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n052}", (detail.MyotomesC6 == LeftRight.Both || detail.MyotomesC6 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n053}", (detail.DermatomesC8 == LeftRight.Both || detail.DermatomesC8 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n054}", (detail.DermatomesC8 == LeftRight.Both || detail.DermatomesC8 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n061}", (detail.MyotomesC7 == LeftRight.Both || detail.MyotomesC7 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n062}", (detail.MyotomesC7 == LeftRight.Both || detail.MyotomesC7 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n063}", (detail.DermatomesT1 == LeftRight.Both || detail.DermatomesT1 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n064}", (detail.DermatomesT1 == LeftRight.Both || detail.DermatomesT1 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n071}", (detail.MyotomesC8 == LeftRight.Both || detail.MyotomesC8 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n072}", (detail.MyotomesC8 == LeftRight.Both || detail.MyotomesC8 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n073}", (detail.DermatomesT2 == LeftRight.Both || detail.DermatomesT2 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n074}", (detail.DermatomesT2 == LeftRight.Both || detail.DermatomesT2 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n081}", (detail.MyotomesT1 == LeftRight.Both || detail.MyotomesT1 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n082}", (detail.MyotomesT1 == LeftRight.Both || detail.MyotomesT1 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n083}", (detail.DermatomesL3 == LeftRight.Both || detail.DermatomesL3 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n084}", (detail.DermatomesL3 == LeftRight.Both || detail.DermatomesL3 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n091}", (detail.MyotomesL2 == LeftRight.Both || detail.MyotomesL2 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n092}", (detail.MyotomesL2 == LeftRight.Both || detail.MyotomesL2 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n093}", (detail.DermatomesL4 == LeftRight.Both || detail.DermatomesL4 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n094}", (detail.DermatomesL4 == LeftRight.Both || detail.DermatomesL4 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n101}", (detail.MyotomesL3 == LeftRight.Both || detail.MyotomesL3 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n102}", (detail.MyotomesL3 == LeftRight.Both || detail.MyotomesL3 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n103}", (detail.DermatomesL5LateralFoot == LeftRight.Both || detail.DermatomesL5LateralFoot == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n104}", (detail.DermatomesL5LateralFoot == LeftRight.Both || detail.DermatomesL5LateralFoot == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n111}", (detail.MyotomesL4 == LeftRight.Both || detail.MyotomesL4 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n112}", (detail.MyotomesL4 == LeftRight.Both || detail.MyotomesL4 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n113}", (detail.DermatomesS1 == LeftRight.Both || detail.DermatomesS1 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n114}", (detail.DermatomesS1 == LeftRight.Both || detail.DermatomesS1 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n121}", (detail.MyotomesS1 == LeftRight.Both || detail.MyotomesS1 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n122}", (detail.MyotomesS1 == LeftRight.Both || detail.MyotomesS1 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n123}", (detail.DermatomesL5AnteriorLeg == LeftRight.Both || detail.DermatomesL5AnteriorLeg == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n124}", (detail.DermatomesL5AnteriorLeg == LeftRight.Both || detail.DermatomesL5AnteriorLeg == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n131}", (detail.MyotomesS2 == LeftRight.Both || detail.MyotomesS2 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n132}", (detail.MyotomesS2 == LeftRight.Both || detail.MyotomesS2 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n133}", (detail.DermatomesS2 == LeftRight.Both || detail.DermatomesS2 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n134}", (detail.DermatomesS2 == LeftRight.Both || detail.DermatomesS2 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n141}", (detail.MyotomesS4 == LeftRight.Both || detail.MyotomesS4 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n142}", (detail.MyotomesS4 == LeftRight.Both || detail.MyotomesS4 == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n151}", detail.CranialNerveExamCN1OlfactoryCoffee ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n152}", detail.CranialNerveExamCN1OlfactoryVanilla ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n153}", detail.CranialNerveExamCN1OlfactoryOrange ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{n161}", (detail.DeepTendonReflexesC5 == LeftRight.Both || detail.DeepTendonReflexesC5 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n162}", (detail.DeepTendonReflexesC5 == LeftRight.Both || detail.DeepTendonReflexesC5 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.AddWordReplacement("{n163}", detail.CranialNerveExamCN2);

            req.Replacements.Add("{n171}", (detail.DeepTendonReflexesC6 == LeftRight.Both || detail.DeepTendonReflexesC6 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n172}", (detail.DeepTendonReflexesC6 == LeftRight.Both || detail.DeepTendonReflexesC6 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.AddWordReplacement("{n173}", detail.CranialNerveExamCN346);

            req.Replacements.Add("{n181}", (detail.DeepTendonReflexesC7 == LeftRight.Both || detail.DeepTendonReflexesC7 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n182}", (detail.DeepTendonReflexesC7 == LeftRight.Both || detail.DeepTendonReflexesC7 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.AddWordReplacement("{n183}", detail.CranialNerveExamCN7);

            req.Replacements.Add("{n191}", (detail.DeepTendonReflexesL4 == LeftRight.Both || detail.DeepTendonReflexesL4 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n192}", (detail.DeepTendonReflexesL4 == LeftRight.Both || detail.DeepTendonReflexesL4 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.AddWordReplacement("{n193}", detail.CranialNerveExamCN11);

            req.Replacements.Add("{n201}", (detail.DeepTendonReflexesS1 == LeftRight.Both || detail.DeepTendonReflexesS1 == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{n202}", (detail.DeepTendonReflexesS1 == LeftRight.Both || detail.DeepTendonReflexesS1 == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.AddWordReplacement("{n203}", detail.CranialNerveExamCN12);

            req.AddWordReplacement("{system review}", detail.SystemsReview);

            //SPINE
            req.Replacements.Add("{s011}", (detail.OrthopaedicExamSpineCervicalValsalva == LeftRight.Both || detail.OrthopaedicExamSpineCervicalValsalva == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s012}", (detail.OrthopaedicExamSpineCervicalValsalva == LeftRight.Both || detail.OrthopaedicExamSpineCervicalValsalva == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s013}", (detail.OrthopaedicExamSpineThoracicValsalva == LeftRight.Both || detail.OrthopaedicExamSpineThoracicValsalva == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s014}", (detail.OrthopaedicExamSpineThoracicValsalva == LeftRight.Both || detail.OrthopaedicExamSpineThoracicValsalva == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s015}", (detail.OrthopaedicExamSpineLumbarValsalva == LeftRight.Both || detail.OrthopaedicExamSpineLumbarValsalva == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s016}", (detail.OrthopaedicExamSpineLumbarValsalva == LeftRight.Both || detail.OrthopaedicExamSpineLumbarValsalva == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s021}", (detail.OrthopaedicExamSpineCervicalKemp == LeftRight.Both || detail.OrthopaedicExamSpineCervicalKemp == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s022}", (detail.OrthopaedicExamSpineCervicalKemp == LeftRight.Both || detail.OrthopaedicExamSpineCervicalKemp == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s023}", (detail.OrthopaedicExamSpineThoracicChestExpansion == LeftRight.Both || detail.OrthopaedicExamSpineThoracicChestExpansion == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s024}", (detail.OrthopaedicExamSpineThoracicChestExpansion == LeftRight.Both || detail.OrthopaedicExamSpineThoracicChestExpansion == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s025}", (detail.OrthopaedicExamSpineLumbarKemp == LeftRight.Both || detail.OrthopaedicExamSpineLumbarKemp == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s026}", (detail.OrthopaedicExamSpineLumbarKemp == LeftRight.Both || detail.OrthopaedicExamSpineLumbarKemp == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s031}", (detail.OrthopaedicExamSpineCervicalSpurling == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSpurling == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s032}", (detail.OrthopaedicExamSpineCervicalSpurling == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSpurling == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s033}", (detail.OrthopaedicExamSpineThoracicScapularApproximation == LeftRight.Both || detail.OrthopaedicExamSpineThoracicScapularApproximation == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s034}", (detail.OrthopaedicExamSpineThoracicScapularApproximation == LeftRight.Both || detail.OrthopaedicExamSpineThoracicScapularApproximation == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s035}", (detail.OrthopaedicExamSpineLumbarSLR == LeftRight.Both || detail.OrthopaedicExamSpineLumbarSLR == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s036}", (detail.OrthopaedicExamSpineLumbarSLR == LeftRight.Both || detail.OrthopaedicExamSpineLumbarSLR == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s041}", (detail.OrthopaedicExamSpineCervicalJackson == LeftRight.Both || detail.OrthopaedicExamSpineCervicalJackson == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s042}", (detail.OrthopaedicExamSpineCervicalJackson == LeftRight.Both || detail.OrthopaedicExamSpineCervicalJackson == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s043}", (detail.OrthopaedicExamSpineThoracicT1NerveStretch == LeftRight.Both || detail.OrthopaedicExamSpineThoracicT1NerveStretch == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s044}", (detail.OrthopaedicExamSpineThoracicT1NerveStretch == LeftRight.Both || detail.OrthopaedicExamSpineThoracicT1NerveStretch == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s045}", (detail.OrthopaedicExamSpineLumbarBraggards == LeftRight.Both || detail.OrthopaedicExamSpineLumbarBraggards == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s046}", (detail.OrthopaedicExamSpineLumbarBraggards == LeftRight.Both || detail.OrthopaedicExamSpineLumbarBraggards == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s051}", (detail.OrthopaedicExamSpineCervicalDoorbell == LeftRight.Both || detail.OrthopaedicExamSpineCervicalDoorbell == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s052}", (detail.OrthopaedicExamSpineCervicalDoorbell == LeftRight.Both || detail.OrthopaedicExamSpineCervicalDoorbell == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s053}", (detail.OrthopaedicExamSpineThoracicT2NerveStretch == LeftRight.Both || detail.OrthopaedicExamSpineThoracicT2NerveStretch == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s054}", (detail.OrthopaedicExamSpineThoracicT2NerveStretch == LeftRight.Both || detail.OrthopaedicExamSpineThoracicT2NerveStretch == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s055}", (detail.OrthopaedicExamSpineLumbarBowstring == LeftRight.Both || detail.OrthopaedicExamSpineLumbarBowstring == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s056}", (detail.OrthopaedicExamSpineLumbarBowstring == LeftRight.Both || detail.OrthopaedicExamSpineLumbarBowstring == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s063}", (detail.OrthopaedicExamSpineThoracicSlumpTest == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSlumpTest == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s064}", (detail.OrthopaedicExamSpineThoracicSlumpTest == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSlumpTest == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s065}", (detail.OrthopaedicExamSpineLumbarPatrickFaber == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPatrickFaber == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s066}", (detail.OrthopaedicExamSpineLumbarPatrickFaber == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPatrickFaber == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s071}", (detail.OrthopaedicExamSpineCervicalSotoHall == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSotoHall == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s072}", (detail.OrthopaedicExamSpineCervicalSotoHall == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSotoHall == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s075}", (detail.OrthopaedicExamSpineLumbarThomas == LeftRight.Both || detail.OrthopaedicExamSpineLumbarThomas == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s076}", (detail.OrthopaedicExamSpineLumbarThomas == LeftRight.Both || detail.OrthopaedicExamSpineLumbarThomas == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s083}", (detail.OrthopaedicExamSpineThoracicSotoHall == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSotoHall == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s084}", (detail.OrthopaedicExamSpineThoracicSotoHall == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSotoHall == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s085}", (detail.OrthopaedicExamSpineLumbarAPSICompression == LeftRight.Both || detail.OrthopaedicExamSpineLumbarAPSICompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s086}", (detail.OrthopaedicExamSpineLumbarAPSICompression == LeftRight.Both || detail.OrthopaedicExamSpineLumbarAPSICompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s091}", (detail.OrthopaedicExamSpineCervicalKernig == LeftRight.Both || detail.OrthopaedicExamSpineCervicalKernig == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s092}", (detail.OrthopaedicExamSpineCervicalKernig == LeftRight.Both || detail.OrthopaedicExamSpineCervicalKernig == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s093}", (detail.OrthopaedicExamSpineThoracicSternalCompression == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSternalCompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s094}", (detail.OrthopaedicExamSpineThoracicSternalCompression == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSternalCompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s095}", (detail.OrthopaedicExamSpineLumbarPPPP == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPPPP == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s096}", (detail.OrthopaedicExamSpineLumbarPPPP == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPPPP == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s101}", (detail.OrthopaedicExamSpineCervicalHermitte == LeftRight.Both || detail.OrthopaedicExamSpineCervicalHermitte == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s102}", (detail.OrthopaedicExamSpineCervicalHermitte == LeftRight.Both || detail.OrthopaedicExamSpineCervicalHermitte == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s103}", (detail.OrthopaedicExamSpineThoracicAbdominalReflex == LeftRight.Both || detail.OrthopaedicExamSpineThoracicAbdominalReflex == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s104}", (detail.OrthopaedicExamSpineThoracicAbdominalReflex == LeftRight.Both || detail.OrthopaedicExamSpineThoracicAbdominalReflex == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s105}", (detail.OrthopaedicExamSpineLumbarPASICompression == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPASICompression == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s106}", (detail.OrthopaedicExamSpineLumbarPASICompression == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPASICompression == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s113}", (detail.OrthopaedicExamSpineThoracicBeevorSign == LeftRight.Both || detail.OrthopaedicExamSpineThoracicBeevorSign == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s114}", (detail.OrthopaedicExamSpineThoracicBeevorSign == LeftRight.Both || detail.OrthopaedicExamSpineThoracicBeevorSign == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s115}", (detail.OrthopaedicExamSpineLumbarEly == LeftRight.Both || detail.OrthopaedicExamSpineLumbarEly == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s116}", (detail.OrthopaedicExamSpineLumbarEly == LeftRight.Both || detail.OrthopaedicExamSpineLumbarEly == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s121}", (detail.OrthopaedicExamSpineCervicalEAST == LeftRight.Both || detail.OrthopaedicExamSpineCervicalEAST == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s122}", (detail.OrthopaedicExamSpineCervicalEAST == LeftRight.Both || detail.OrthopaedicExamSpineCervicalEAST == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s123}", (detail.OrthopaedicExamSpineThoracicSLR == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSLR == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s124}", (detail.OrthopaedicExamSpineThoracicSLR == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSLR == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s125}", (detail.OrthopaedicExamSpineLumbarYeoman == LeftRight.Both || detail.OrthopaedicExamSpineLumbarYeoman == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s126}", (detail.OrthopaedicExamSpineLumbarYeoman == LeftRight.Both || detail.OrthopaedicExamSpineLumbarYeoman == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s131}", (detail.OrthopaedicExamSpineCervicalAdson == LeftRight.Both || detail.OrthopaedicExamSpineCervicalAdson == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s132}", (detail.OrthopaedicExamSpineCervicalAdson == LeftRight.Both || detail.OrthopaedicExamSpineCervicalAdson == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s133}", (detail.OrthopaedicExamSpineThoracicTrueLegLength == LeftRight.Both || detail.OrthopaedicExamSpineThoracicTrueLegLength == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s134}", (detail.OrthopaedicExamSpineThoracicTrueLegLength == LeftRight.Both || detail.OrthopaedicExamSpineThoracicTrueLegLength == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s135}", (detail.OrthopaedicExamSpineLumbarHibbs == LeftRight.Both || detail.OrthopaedicExamSpineLumbarHibbs == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s136}", (detail.OrthopaedicExamSpineLumbarHibbs == LeftRight.Both || detail.OrthopaedicExamSpineLumbarHibbs == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s141}", (detail.OrthopaedicExamSpineCervicalWrightHyperabduction == LeftRight.Both || detail.OrthopaedicExamSpineCervicalWrightHyperabduction == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s142}", (detail.OrthopaedicExamSpineCervicalWrightHyperabduction == LeftRight.Both || detail.OrthopaedicExamSpineCervicalWrightHyperabduction == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s151}", (detail.OrthopaedicExamSpineCervicalEden == LeftRight.Both || detail.OrthopaedicExamSpineCervicalEden == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s152}", (detail.OrthopaedicExamSpineCervicalEden == LeftRight.Both || detail.OrthopaedicExamSpineCervicalEden == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s153}", (detail.OrthopaedicExamSpineThoracicHermitte == LeftRight.Both || detail.OrthopaedicExamSpineThoracicHermitte == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s154}", (detail.OrthopaedicExamSpineThoracicHermitte == LeftRight.Both || detail.OrthopaedicExamSpineThoracicHermitte == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s155}", (detail.OrthopaedicExamSpineLumbarErectors == LeftRight.Both || detail.OrthopaedicExamSpineLumbarErectors == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s156}", (detail.OrthopaedicExamSpineLumbarErectors == LeftRight.Both || detail.OrthopaedicExamSpineLumbarErectors == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s163}", (detail.OrthopaedicExamSpineThoracicKernig == LeftRight.Both || detail.OrthopaedicExamSpineThoracicKernig == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s164}", (detail.OrthopaedicExamSpineThoracicKernig == LeftRight.Both || detail.OrthopaedicExamSpineThoracicKernig == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s165}", (detail.OrthopaedicExamSpineLumbarGluteusMax == LeftRight.Both || detail.OrthopaedicExamSpineLumbarGluteusMax == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s166}", (detail.OrthopaedicExamSpineLumbarGluteusMax == LeftRight.Both || detail.OrthopaedicExamSpineLumbarGluteusMax == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s171}", (detail.OrthopaedicExamSpineCervicalRotaryChair == LeftRight.Both || detail.OrthopaedicExamSpineCervicalRotaryChair == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s172}", (detail.OrthopaedicExamSpineCervicalRotaryChair == LeftRight.Both || detail.OrthopaedicExamSpineCervicalRotaryChair == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s175}", (detail.OrthopaedicExamSpineLumbarGluteusMedMin == LeftRight.Both || detail.OrthopaedicExamSpineLumbarGluteusMedMin == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s176}", (detail.OrthopaedicExamSpineLumbarGluteusMedMin == LeftRight.Both || detail.OrthopaedicExamSpineLumbarGluteusMedMin == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s181}", (detail.OrthopaedicExamSpineCervicalDixHallpike == LeftRight.Both || detail.OrthopaedicExamSpineCervicalDixHallpike == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s182}", (detail.OrthopaedicExamSpineCervicalDixHallpike == LeftRight.Both || detail.OrthopaedicExamSpineCervicalDixHallpike == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s183}", (detail.OrthopaedicExamSpineThoracicRhomboids == LeftRight.Both || detail.OrthopaedicExamSpineThoracicRhomboids == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s184}", (detail.OrthopaedicExamSpineThoracicRhomboids == LeftRight.Both || detail.OrthopaedicExamSpineThoracicRhomboids == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s185}", (detail.OrthopaedicExamSpineLumbarPiriformis == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPiriformis == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s186}", (detail.OrthopaedicExamSpineLumbarPiriformis == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPiriformis == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s193}", (detail.OrthopaedicExamSpineThoracicTrapezius == LeftRight.Both || detail.OrthopaedicExamSpineThoracicTrapezius == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s194}", (detail.OrthopaedicExamSpineThoracicTrapezius == LeftRight.Both || detail.OrthopaedicExamSpineThoracicTrapezius == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s195}", (detail.OrthopaedicExamSpineLumbarPsoas == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPsoas == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s196}", (detail.OrthopaedicExamSpineLumbarPsoas == LeftRight.Both || detail.OrthopaedicExamSpineLumbarPsoas == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s201}", (detail.OrthopaedicExamSpineCervicalSubOccipitals == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSubOccipitals == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s202}", (detail.OrthopaedicExamSpineCervicalSubOccipitals == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSubOccipitals == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s203}", (detail.OrthopaedicExamSpineThoracicErectors == LeftRight.Both || detail.OrthopaedicExamSpineThoracicErectors == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s204}", (detail.OrthopaedicExamSpineThoracicErectors == LeftRight.Both || detail.OrthopaedicExamSpineThoracicErectors == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s205}", (detail.OrthopaedicExamSpineLumbarTFLITB == LeftRight.Both || detail.OrthopaedicExamSpineLumbarTFLITB == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s206}", (detail.OrthopaedicExamSpineLumbarTFLITB == LeftRight.Both || detail.OrthopaedicExamSpineLumbarTFLITB == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s211}", (detail.OrthopaedicExamSpineCervicalTrapezius == LeftRight.Both || detail.OrthopaedicExamSpineCervicalTrapezius == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s212}", (detail.OrthopaedicExamSpineCervicalTrapezius == LeftRight.Both || detail.OrthopaedicExamSpineCervicalTrapezius == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s213}", (detail.OrthopaedicExamSpineThoracicLatissimus == LeftRight.Both || detail.OrthopaedicExamSpineThoracicLatissimus == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s214}", (detail.OrthopaedicExamSpineThoracicLatissimus == LeftRight.Both || detail.OrthopaedicExamSpineThoracicLatissimus == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s215}", (detail.OrthopaedicExamSpineLumbarQuadratusLumborum == LeftRight.Both || detail.OrthopaedicExamSpineLumbarQuadratusLumborum == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s216}", (detail.OrthopaedicExamSpineLumbarQuadratusLumborum == LeftRight.Both || detail.OrthopaedicExamSpineLumbarQuadratusLumborum == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s221}", (detail.OrthopaedicExamSpineCervicalLevatorScapulae == LeftRight.Both || detail.OrthopaedicExamSpineCervicalLevatorScapulae == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s222}", (detail.OrthopaedicExamSpineCervicalLevatorScapulae == LeftRight.Both || detail.OrthopaedicExamSpineCervicalLevatorScapulae == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s223}", (detail.OrthopaedicExamSpineThoracicSerratusPostInf == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSerratusPostInf == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s224}", (detail.OrthopaedicExamSpineThoracicSerratusPostInf == LeftRight.Both || detail.OrthopaedicExamSpineThoracicSerratusPostInf == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s231}", (detail.OrthopaedicExamSpineCervicalErectors == LeftRight.Both || detail.OrthopaedicExamSpineCervicalErectors == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s232}", (detail.OrthopaedicExamSpineCervicalErectors == LeftRight.Both || detail.OrthopaedicExamSpineCervicalErectors == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s233}", (detail.OrthopaedicExamSpineThoracicPcMajorMinor == LeftRight.Both || detail.OrthopaedicExamSpineThoracicPcMajorMinor == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s234}", (detail.OrthopaedicExamSpineThoracicPcMajorMinor == LeftRight.Both || detail.OrthopaedicExamSpineThoracicPcMajorMinor == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s241}", (detail.OrthopaedicExamSpineCervicalSternocleidomastoid == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSternocleidomastoid == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s242}", (detail.OrthopaedicExamSpineCervicalSternocleidomastoid == LeftRight.Both || detail.OrthopaedicExamSpineCervicalSternocleidomastoid == LeftRight.Left) ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{s251}", (detail.OrthopaedicExamSpineCervicalScalenes == LeftRight.Both || detail.OrthopaedicExamSpineCervicalScalenes == LeftRight.Right) ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{s252}", (detail.OrthopaedicExamSpineCervicalScalenes == LeftRight.Both || detail.OrthopaedicExamSpineCervicalScalenes == LeftRight.Left) ? checkReplacements : uncheckReplacements);
            #endregion Page 3

            #region Page 4
            req.AddWordReplacement("{t011}", detail.OrthopaedicExamCervicalFlexionROM.ToString());
            req.AddWordReplacement("{t012}", detail.OrthopaedicExamCervicalFlexionERP.ToString());
            req.AddWordReplacement("{t013}", detail.OrthopaedicExamCervicalFlexionComment);
            req.AddWordReplacement("{t014}", detail.OrthopaedicExamShoulderRightFlexionROM.ToString());
            req.AddWordReplacement("{t015}", detail.OrthopaedicExamShoulderRightFlexionERP.ToString());
            req.AddWordReplacement("{t016}", detail.OrthopaedicExamShoulderRightFlexionComment);

            req.AddWordReplacement("{t021}", detail.OrthopaedicExamCervicalExtensionROM.ToString());
            req.AddWordReplacement("{t022}", detail.OrthopaedicExamCervicalExtensionERP.ToString());
            req.AddWordReplacement("{t023}", detail.OrthopaedicExamCervicalExtensionComment);
            req.AddWordReplacement("{t024}", detail.OrthopaedicExamShoulderLeftFlexionROM.ToString());
            req.AddWordReplacement("{t025}", detail.OrthopaedicExamShoulderLeftFlexionERP.ToString());
            req.AddWordReplacement("{t026}", detail.OrthopaedicExamShoulderLeftFlexionComment);

            req.AddWordReplacement("{t031}", detail.OrthopaedicExamCervicalRightRotationROM.ToString());
            req.AddWordReplacement("{t032}", detail.OrthopaedicExamCervicalRightRotationERP.ToString());
            req.AddWordReplacement("{t033}", detail.OrthopaedicExamCervicalRightRotationComment);
            req.AddWordReplacement("{t034}", detail.OrthopaedicExamShoulderRightAbductionROM.ToString());
            req.AddWordReplacement("{t035}", detail.OrthopaedicExamShoulderRightAbductionERP.ToString());
            req.AddWordReplacement("{t036}", detail.OrthopaedicExamShoulderRightAbductionComment);

            req.AddWordReplacement("{t041}", detail.OrthopaedicExamCervicalLeftRotationROM.ToString());
            req.AddWordReplacement("{t042}", detail.OrthopaedicExamCervicalLeftRotationERP.ToString());
            req.AddWordReplacement("{t043}", detail.OrthopaedicExamCervicalLeftRotationComment);
            req.AddWordReplacement("{t044}", detail.OrthopaedicExamShoulderLeftAbductionROM.ToString());
            req.AddWordReplacement("{t045}", detail.OrthopaedicExamShoulderLeftAbductionERP.ToString());
            req.AddWordReplacement("{t046}", detail.OrthopaedicExamShoulderLeftAbductionComment);

            req.AddWordReplacement("{t051}", detail.OrthopaedicExamCervicalRightLateralFlexionROM.ToString());
            req.AddWordReplacement("{t052}", detail.OrthopaedicExamCervicalRightLateralFlexionERP.ToString());
            req.AddWordReplacement("{t053}", detail.OrthopaedicExamCervicalRightLateralFlexionComment);
            req.AddWordReplacement("{t054}", detail.OrthopaedicExamShoulderRightInternalRotationROM.ToString());
            req.AddWordReplacement("{t055}", detail.OrthopaedicExamShoulderRightInternalRotationERP.ToString());
            req.AddWordReplacement("{t056}", detail.OrthopaedicExamShoulderRightInternalRotationComment);

            req.AddWordReplacement("{t061}", detail.OrthopaedicExamCervicalLeftLateralFlexionROM.ToString());
            req.AddWordReplacement("{t062}", detail.OrthopaedicExamCervicalLeftLateralFlexionERP.ToString());
            req.AddWordReplacement("{t063}", detail.OrthopaedicExamCervicalLeftLateralFlexionComment);
            req.AddWordReplacement("{t064}", detail.OrthopaedicExamShoulderLeftInternalRotationROM.ToString());
            req.AddWordReplacement("{t065}", detail.OrthopaedicExamShoulderLeftInternalRotationERP.ToString());
            req.AddWordReplacement("{t066}", detail.OrthopaedicExamShoulderLeftInternalRotationComment);

            req.AddWordReplacement("{t074}", detail.OrthopaedicExamShoulderRightExternalRotationROM.ToString());
            req.AddWordReplacement("{t075}", detail.OrthopaedicExamShoulderRightExternalRotationERP.ToString());
            req.AddWordReplacement("{t076}", detail.OrthopaedicExamShoulderRightExternalRotationComment);

            req.AddWordReplacement("{t084}", detail.OrthopaedicExamShoulderLeftExternalRotationROM.ToString());
            req.AddWordReplacement("{t085}", detail.OrthopaedicExamShoulderLeftExternalRotationERP.ToString());
            req.AddWordReplacement("{t086}", detail.OrthopaedicExamShoulderLeftExternalRotationComment);

            req.AddWordReplacement("{t101}", detail.OrthopaedicExamLumbarFlexionROM.ToString());
            req.AddWordReplacement("{t102}", detail.OrthopaedicExamLumbarFlexionERP.ToString());
            req.AddWordReplacement("{t103}", detail.OrthopaedicExamLumbarFlexionComment);
            req.AddWordReplacement("{t104}", detail.OrthopaedicExamElbowRightFlexionROM.ToString());
            req.AddWordReplacement("{t105}", detail.OrthopaedicExamElbowRightFlexionERP.ToString());
            req.AddWordReplacement("{t106}", detail.OrthopaedicExamElbowRightFlexionComment);

            req.AddWordReplacement("{t111}", detail.OrthopaedicExamLumbarExtensionROM.ToString());
            req.AddWordReplacement("{t112}", detail.OrthopaedicExamLumbarExtensionERP.ToString());
            req.AddWordReplacement("{t113}", detail.OrthopaedicExamLumbarExtensionComment);
            req.AddWordReplacement("{t114}", detail.OrthopaedicExamElbowLeftFlexionROM.ToString());
            req.AddWordReplacement("{t115}", detail.OrthopaedicExamElbowLeftFlexionERP.ToString());
            req.AddWordReplacement("{t116}", detail.OrthopaedicExamElbowLeftFlexionComment);

            req.AddWordReplacement("{t121}", detail.OrthopaedicExamLumbarRightLateralFlexionROM.ToString());
            req.AddWordReplacement("{t122}", detail.OrthopaedicExamLumbarRightLateralFlexionERP.ToString());
            req.AddWordReplacement("{t123}", detail.OrthopaedicExamLumbarRightLateralFlexionComment);
            req.AddWordReplacement("{t124}", detail.OrthopaedicExamElbowRightExtensionROM.ToString());
            req.AddWordReplacement("{t125}", detail.OrthopaedicExamElbowRightExtensionERP.ToString());
            req.AddWordReplacement("{t126}", detail.OrthopaedicExamElbowRightExtensionComment);

            req.AddWordReplacement("{t131}", detail.OrthopaedicExamLumbarLeftLateralFlexionROM.ToString());
            req.AddWordReplacement("{t132}", detail.OrthopaedicExamLumbarLeftLateralFlexionERP.ToString());
            req.AddWordReplacement("{t133}", detail.OrthopaedicExamLumbarLeftLateralFlexionComment);
            req.AddWordReplacement("{t134}", detail.OrthopaedicExamElbowLeftExtensionROM.ToString());
            req.AddWordReplacement("{t135}", detail.OrthopaedicExamElbowLeftExtensionERP.ToString());
            req.AddWordReplacement("{t136}", detail.OrthopaedicExamElbowLeftExtensionComment);

            req.AddWordReplacement("{t141}", detail.OrthopaedicExamLumbarRightRotationROM.ToString());
            req.AddWordReplacement("{t142}", detail.OrthopaedicExamLumbarRightRotationERP.ToString());
            req.AddWordReplacement("{t143}", detail.OrthopaedicExamLumbarRightRotationComment);

            req.AddWordReplacement("{t151}", detail.OrthopaedicExamLumbarLeftRotationROM.ToString());
            req.AddWordReplacement("{t152}", detail.OrthopaedicExamLumbarLeftRotationERP.ToString());
            req.AddWordReplacement("{t153}", detail.OrthopaedicExamLumbarLeftRotationComment);

            req.AddWordReplacement("{t171}", detail.OrthopaedicExamHipRightFlexionROM.ToString());
            req.AddWordReplacement("{t172}", detail.OrthopaedicExamHipRightFlexionERP.ToString());
            req.AddWordReplacement("{t173}", detail.OrthopaedicExamHipRightFlexionComment);
            req.AddWordReplacement("{t174}", detail.OrthopaedicExamWristRightFlexionROM.ToString());
            req.AddWordReplacement("{t175}", detail.OrthopaedicExamWristRightFlexionERP.ToString());
            req.AddWordReplacement("{t176}", detail.OrthopaedicExamWristRightFlexionComment);

            req.AddWordReplacement("{t181}", detail.OrthopaedicExamHipLeftFlexionROM.ToString());
            req.AddWordReplacement("{t182}", detail.OrthopaedicExamHipLeftFlexionERP.ToString());
            req.AddWordReplacement("{t183}", detail.OrthopaedicExamHipLeftFlexionComment);
            req.AddWordReplacement("{t184}", detail.OrthopaedicExamWristLeftFlexionROM.ToString());
            req.AddWordReplacement("{t185}", detail.OrthopaedicExamWristLeftFlexionERP.ToString());
            req.AddWordReplacement("{t186}", detail.OrthopaedicExamWristLeftFlexionComment);

            req.AddWordReplacement("{t191}", detail.OrthopaedicExamHipRightExtensionROM.ToString());
            req.AddWordReplacement("{t192}", detail.OrthopaedicExamHipRightExtensionERP.ToString());
            req.AddWordReplacement("{t193}", detail.OrthopaedicExamHipRightExtensionComment);
            req.AddWordReplacement("{t194}", detail.OrthopaedicExamWristRightExtensionROM.ToString());
            req.AddWordReplacement("{t195}", detail.OrthopaedicExamWristRightExtensionERP.ToString());
            req.AddWordReplacement("{t196}", detail.OrthopaedicExamWristRightExtensionComment);

            req.AddWordReplacement("{t201}", detail.OrthopaedicExamHipLeftExtensionROM.ToString());
            req.AddWordReplacement("{t202}", detail.OrthopaedicExamHipLeftExtensionERP.ToString());
            req.AddWordReplacement("{t203}", detail.OrthopaedicExamHipLeftExtensionComment);
            req.AddWordReplacement("{t204}", detail.OrthopaedicExamWristLeftExtensionROM.ToString());
            req.AddWordReplacement("{t205}", detail.OrthopaedicExamWristLeftExtensionERP.ToString());
            req.AddWordReplacement("{t206}", detail.OrthopaedicExamWristLeftExtensionComment);

            req.AddWordReplacement("{t211}", detail.OrthopaedicExamHipRightAbductionROM.ToString());
            req.AddWordReplacement("{t212}", detail.OrthopaedicExamHipRightAbductionERP.ToString());
            req.AddWordReplacement("{t213}", detail.OrthopaedicExamHipRightAbductionComment);
            req.AddWordReplacement("{t214}", detail.OrthopaedicExamWristRightRadialDeviationROM.ToString());
            req.AddWordReplacement("{t215}", detail.OrthopaedicExamWristRightRadialDeviationERP.ToString());
            req.AddWordReplacement("{t216}", detail.OrthopaedicExamWristRightRadialDeviationComment);

            req.AddWordReplacement("{t221}", detail.OrthopaedicExamHipLeftAbductionROM.ToString());
            req.AddWordReplacement("{t222}", detail.OrthopaedicExamHipLeftAbductionERP.ToString());
            req.AddWordReplacement("{t223}", detail.OrthopaedicExamHipLeftAbductionComment);
            req.AddWordReplacement("{t224}", detail.OrthopaedicExamWristLeftRadialDeviationROM.ToString());
            req.AddWordReplacement("{t225}", detail.OrthopaedicExamWristLeftRadialDeviationERP.ToString());
            req.AddWordReplacement("{t226}", detail.OrthopaedicExamWristLeftRadialDeviationComment);

            req.AddWordReplacement("{t231}", detail.OrthopaedicExamHipRightInternalRotationROM.ToString());
            req.AddWordReplacement("{t232}", detail.OrthopaedicExamHipRightInternalRotationERP.ToString());
            req.AddWordReplacement("{t233}", detail.OrthopaedicExamHipRightInternalRotationComment);
            req.AddWordReplacement("{t234}", detail.OrthopaedicExamWristRightUlnarDeviationROM.ToString());
            req.AddWordReplacement("{t235}", detail.OrthopaedicExamWristRightUlnarDeviationERP.ToString());
            req.AddWordReplacement("{t236}", detail.OrthopaedicExamWristRightUlnarDeviationComment);

            req.AddWordReplacement("{t241}", detail.OrthopaedicExamHipLeftInternalRotationROM.ToString());
            req.AddWordReplacement("{t242}", detail.OrthopaedicExamHipLeftInternalRotationERP.ToString());
            req.AddWordReplacement("{t243}", detail.OrthopaedicExamHipLeftInternalRotationComment);
            req.AddWordReplacement("{t244}", detail.OrthopaedicExamWristLeftUlnarDeviationROM.ToString());
            req.AddWordReplacement("{t245}", detail.OrthopaedicExamWristLeftUlnarDeviationERP.ToString());
            req.AddWordReplacement("{t246}", detail.OrthopaedicExamWristLeftUlnarDeviationComment);

            req.AddWordReplacement("{t251}", detail.OrthopaedicExamHipRightExternalRotationROM.ToString());
            req.AddWordReplacement("{t252}", detail.OrthopaedicExamHipRightExternalRotationERP.ToString());
            req.AddWordReplacement("{t253}", detail.OrthopaedicExamHipRightExternalRotationComment);

            req.AddWordReplacement("{t261}", detail.OrthopaedicExamHipLeftExternalRotationROM.ToString());
            req.AddWordReplacement("{t262}", detail.OrthopaedicExamHipLeftExternalRotationERP.ToString());
            req.AddWordReplacement("{t263}", detail.OrthopaedicExamHipLeftExternalRotationComment);

            req.AddWordReplacement("{t281}", detail.OrthopaedicExamKneeRightFlexionROM.ToString());
            req.AddWordReplacement("{t282}", detail.OrthopaedicExamKneeRightFlexionERP.ToString());
            req.AddWordReplacement("{t283}", detail.OrthopaedicExamKneeRightFlexionComment);
            req.AddWordReplacement("{t284}", detail.OrthopaedicExamAnkleRightDorsiflexionROM.ToString());
            req.AddWordReplacement("{t285}", detail.OrthopaedicExamAnkleRightDorsiflexionERP.ToString());
            req.AddWordReplacement("{t286}", detail.OrthopaedicExamAnkleRightDorsiflexionComment);

            req.AddWordReplacement("{t291}", detail.OrthopaedicExamKneeLeftFlexionROM.ToString());
            req.AddWordReplacement("{t292}", detail.OrthopaedicExamKneeLeftFlexionERP.ToString());
            req.AddWordReplacement("{t293}", detail.OrthopaedicExamKneeLeftFlexionComment);
            req.AddWordReplacement("{t294}", detail.OrthopaedicExamAnkleLeftDorsiflexionROM.ToString());
            req.AddWordReplacement("{t295}", detail.OrthopaedicExamAnkleLeftDorsiflexionERP.ToString());
            req.AddWordReplacement("{t296}", detail.OrthopaedicExamAnkleLeftDorsiflexionComment);

            req.AddWordReplacement("{t301}", detail.OrthopaedicExamKneeRightExtensionROM.ToString());
            req.AddWordReplacement("{t302}", detail.OrthopaedicExamKneeRightExtensionERP.ToString());
            req.AddWordReplacement("{t303}", detail.OrthopaedicExamKneeRightExtensionComment);
            req.AddWordReplacement("{t304}", detail.OrthopaedicExamAnkleRightPlantarFlexionROM.ToString());
            req.AddWordReplacement("{t305}", detail.OrthopaedicExamAnkleRightPlantarFlexionERP.ToString());
            req.AddWordReplacement("{t306}", detail.OrthopaedicExamAnkleRightPlantarFlexionComment);

            req.AddWordReplacement("{t311}", detail.OrthopaedicExamKneeLeftExtensionROM.ToString());
            req.AddWordReplacement("{t312}", detail.OrthopaedicExamKneeLeftExtensionERP.ToString());
            req.AddWordReplacement("{t313}", detail.OrthopaedicExamKneeLeftExtensionComment);
            req.AddWordReplacement("{t314}", detail.OrthopaedicExamAnkleLeftPlantarFlexionROM.ToString());
            req.AddWordReplacement("{t315}", detail.OrthopaedicExamAnkleLeftPlantarFlexionERP.ToString());
            req.AddWordReplacement("{t316}", detail.OrthopaedicExamAnkleLeftPlantarFlexionComment);

            req.AddWordReplacement("{t324}", detail.OrthopaedicExamAnkleRightInversionROM.ToString());
            req.AddWordReplacement("{t325}", detail.OrthopaedicExamAnkleRightInversionERP.ToString());
            req.AddWordReplacement("{t326}", detail.OrthopaedicExamAnkleRightInversionComment);

            req.AddWordReplacement("{t331}", detail.OrthopaedicExamKneeSquatROM.ToString());
            req.AddWordReplacement("{t332}", detail.OrthopaedicExamKneeSquatERP.ToString());
            req.AddWordReplacement("{t333}", detail.OrthopaedicExamKneeSquatComment);
            req.AddWordReplacement("{t334}", detail.OrthopaedicExamAnkleLeftInversionROM.ToString());
            req.AddWordReplacement("{t335}", detail.OrthopaedicExamAnkleLeftInversionERP.ToString());
            req.AddWordReplacement("{t336}", detail.OrthopaedicExamAnkleLeftInversionComment);

            req.AddWordReplacement("{t344}", detail.OrthopaedicExamAnkleRightEversionROM.ToString());
            req.AddWordReplacement("{t345}", detail.OrthopaedicExamAnkleRightEversionERP.ToString());
            req.AddWordReplacement("{t346}", detail.OrthopaedicExamAnkleRightEversionComment);

            req.AddWordReplacement("{t354}", detail.OrthopaedicExamAnkleLeftEversionROM.ToString());
            req.AddWordReplacement("{t355}", detail.OrthopaedicExamAnkleLeftEversionERP.ToString());
            req.AddWordReplacement("{t356}", detail.OrthopaedicExamAnkleLeftEversionComment);
            #endregion Page 4

            #region Page 5
            //Chiropractic diagram
            List<PictureReplacement> picDiagram = new List<PictureReplacement>();
            string digramImagePath = Program.CreateDiagramImageBy(ucDetail.DiagramPictureBox);
            picDiagram.Add(new PictureReplacement(digramImagePath));
            req.Replacements.Add("{diagram}", picDiagram);

            req.AddWordReplacement("{diagram note}", detail.ChiropracticDiagramNote);
            #endregion Page 5

            #region Page 6
            req.Replacements.Add("{f011}", detail.FlagSeverityOfSpecificSymptoms ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f012}", detail.FlagOlderAge ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f021}", detail.FlagEmergenceOfRadicularIrritation ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f022}", detail.FlagPriorHistoryOfPsychologicalDisturbance ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f031}", detail.FlagPreviousHistoryOfHeadInjury ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f032}", detail.FlagPregnancy ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f041}", detail.FlagPreExistAutoimmuneDisease ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f042}", detail.FlagFeverChills ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f051}", detail.FlagSystemicOrChronicDiseasePresent ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f052}", detail.FlagRecentInfection ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f061}", detail.FlagSignificantRecentWeightLossGain ? checkReplacements : uncheckReplacements);
            req.AddWordReplacement("{f063}", detail.FlagOther);
            req.Replacements.Add("{f071}", detail.FlagHeadRotatedOutOfPosition ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f081}", detail.FlagMultipleImpact ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{f091}", detail.FlagShowingSignsOfPTS ? checkReplacements : uncheckReplacements);

            req.AddWordReplacement("{diagnosis}", detail.Diagnosis);
            req.AddWordReplacement("{recommendations}", detail.Recommendations);
            #endregion Page 6


            //Date/duration
            req.AddWordReplacement("{date}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", EditingInitialTreatment.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", EditingInitialTreatment.TreatmentDurationMinutes.ToString());
            //Therapist
            req.AddWordReplacement("{therapist}", EditingInitialTreatment.Therapist.ToString());

            //Header
            req.AddWordReplacement("{patient name}", EditingInitialTreatment.Patient.DisplayName);
            req.AddWordReplacement("{treatment type}", EditingInitialTreatment.TreatmentType.DisplayName);

            #region Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(EditingInitialTreatment.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }
            #endregion Signature
        }

        private void buildPhysiotherapyRequest(ProcessRequest req, object obj) {
            UCPhysiotherapyDetail ucDetail = obj as UCPhysiotherapyDetail;

            //PhysiotherapyDetail detail = EditingTreatmentDetail as PhysiotherapyDetail;
            PhysiotherapyDetail detail = ucDetail.EditingPhysiotherapyDetail;

            #region Diagrams
            //Body diagram
            List<PictureReplacement> picBodyDiagram = new List<PictureReplacement>();
            string bodyDiagramImagePath = Program.CreateDiagramImageBy(ucDetail.PainDiagramPictureBox);
            picBodyDiagram.Add(new PictureReplacement(bodyDiagramImagePath));
            req.Replacements.Add("{BodyDiagram}", picBodyDiagram);

            //Spine diagram
            List<PictureReplacement> picSpineDiagram = new List<PictureReplacement>();
            string spineDiagramImagePath = Program.CreateDiagramImageBy(ucDetail.SpineDiagramPictureBox);
            picSpineDiagram.Add(new PictureReplacement(spineDiagramImagePath));
            req.Replacements.Add("{SpineDiagram}", picSpineDiagram);

            #endregion Diagrams

            #region Checkboxes
            //Load check image
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckMarkRed.png");
            Image imgCheck = Image.FromStream(s);
            string checkImagePath = Program.CreateImageFile(imgCheck);
            s.Close();
            s = myAssembly.GetManifestResourceStream("EHD.Admin.images.CheckBoxEmpty.png");
            Image imgCheckEmpty = Image.FromStream(s);
            string checkEmptyImagePath = Program.CreateImageFile(imgCheckEmpty);
            s.Close();

            //Check picture replacement
            List<PictureReplacement> checkReplacements = new List<PictureReplacement>();
            checkReplacements.Add(new PictureReplacement(checkImagePath));

            //Uncheck replacement
            List<PictureReplacement> uncheckReplacements = new List<PictureReplacement>();
            uncheckReplacements.Add(new PictureReplacement(checkEmptyImagePath));

            //1) Pain Scale
            req.AddWordReplacement("{num1}", detail.PainScaleNow.ToString());
            req.AddWordReplacement("{num2}", detail.PainScaleBest.ToString());
            req.AddWordReplacement("{num3}", detail.PainScaleWorst.ToString());

            //2) Symptom Trend
            if(detail.SymptomTrend == Trend.Increasing) {
                req.Replacements.Add("{ct1}", checkReplacements);
            } else {
                req.Replacements.Add("{ct1}", uncheckReplacements);
            }
            if(detail.SymptomTrend == Trend.Static) {
                req.Replacements.Add("{ct2}", checkReplacements);
            } else {
                req.Replacements.Add("{ct2}", uncheckReplacements);
            }
            if(detail.SymptomTrend == Trend.Decreasing) {
                req.Replacements.Add("{ct3}", checkReplacements);
            } else {
                req.Replacements.Add("{ct3}", uncheckReplacements);
            }

            //3) Irritability
            if(detail.IrritabilityLevel == IrritabilityLevels.Low) {
                req.Replacements.Add("{ci1}", checkReplacements);
            } else {
                req.Replacements.Add("{ci1}", uncheckReplacements);
            }
            if(detail.IrritabilityLevel == IrritabilityLevels.Moderate) {
                req.Replacements.Add("{ci2}", checkReplacements);
            } else {
                req.Replacements.Add("{ci2}", uncheckReplacements);
            }
            if(detail.IrritabilityLevel == IrritabilityLevels.High) {
                req.Replacements.Add("{ci3}", checkReplacements);
            } else {
                req.Replacements.Add("{ci3}", uncheckReplacements);
            }

            //4) Consent for Treatment
            if(detail.ConsentForTreatment == ConsentForTreaments.Verbal) {
                req.Replacements.Add("{cc1}", checkReplacements);
            } else {
                req.Replacements.Add("{cc1}", uncheckReplacements);
            }
            #endregion Checkboxes

            #region Text

            //0) Template Title
            req.AddWordReplacement("{treatment note title}", _treatmentNoteTitle);

            //Patient patient = detail.InitialTreatment.Patient;
            Patient patient = EditingInitialTreatment.Patient;

            //0.1) Patient name
            //req.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            req.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            req.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            req.AddWordReplacement("{file number}", patient.FileNumber);
            
            //1) Description of Symptoms
            req.AddWordReplacement("{txt1}", detail.DescriptionOfSymptoms);

            //2) Constant vs. Intermittent
            req.AddWordReplacement("{txt2}", detail.ConstantVsIntermittent);

            //3) Day Change
            req.AddWordReplacement("{txt3}", detail.DayChanges);

            //4) Aggravated By
            req.AddWordReplacement("{txt4}", detail.AggravatedBy);

            //5) Eased By
            req.AddWordReplacement("{txt5}", detail.EasedBy);

            //6) Occupation
            req.AddWordReplacement("{txt6}", detail.Occupation);

            //7) Work Status
            req.AddWordReplacement("{txt7}", detail.WorkStatus);

            //8) History of Presenting Complaint
            req.AddWordReplacement("{txt8}", detail.HistoryOfPresentingComplaint);

            //9) Joint Sounds/Abnormalities
            req.AddWordReplacement("{txt9}", detail.JointSoundsAndAbnormalities);

            //10) Past Relevant History
            req.AddWordReplacement("{txt10}", detail.PastRelevantHistoryAndTreatment);

            //11) Investigations
            req.AddWordReplacement("{txt11}", detail.Investigations);

            //12) Medications
            req.AddWordReplacement("{txt12}", detail.Medications);

            //13) General Medical History
            req.AddWordReplacement("{txt13}", detail.GeneralMedicalHistoryAndMedications);

            //14) Social History
            req.AddWordReplacement("{txt14}", detail.SocialHistoryAndRecreationalActivities);

            //15) Goals
            req.AddWordReplacement("{txt15}", detail.Goals);

            //16) Red Flag/Precautions
            req.AddWordReplacement("{txt16}", detail.RedFlagsAndPrecautions);

            //17) Observation/Posture/Gait
            req.AddWordReplacement("{txt17}", detail.ObservationPostureGait);

            //18) Active Range of Motion
            req.AddWordReplacement("{txt18}", detail.ActiveRangeOfMotion);

            //19) Muscle Strength/Myotomes
            req.AddWordReplacement("{txt19}", detail.MuscleStrengthAndMyotomes);

            //20) Palpation
            req.AddWordReplacement("{txt20}", detail.Palpation);

            //21) Neurological
            req.AddWordReplacement("{txt21}", detail.Neurological);

            //22) Reflexes
            req.AddWordReplacement("{txt22}", detail.Reflexes);

            //23) Sensation
            req.AddWordReplacement("{txt23}", detail.Sensation);

            //24) Passive Range of Motion
            req.AddWordReplacement("{txt24}", detail.PassiveRangeOfMotion);

            //25) Stability Tests
            req.AddWordReplacement("{txt25}", detail.StabilityTestsAndSpecialTests);

            //26) Analysis
            req.AddWordReplacement("{txt26}", detail.Analysis);

            //27) Treatment Plan
            req.AddWordReplacement("{txt27}", detail.TreatmentPlan);
            #endregion Text

            // Date/duration
            req.AddWordReplacement("{date}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", EditingInitialTreatment.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", EditingInitialTreatment.TreatmentDurationMinutes.ToString());

            // Therapist
            req.AddWordReplacement("{therapist}", EditingInitialTreatment.Therapist.ToString());

            // Patient
            req.AddWordReplacement("{patient name}", EditingInitialTreatment.Patient.DisplayName);

            // Treatment Type
            req.AddWordReplacement("{treatment type}", EditingInitialTreatment.TreatmentType.DisplayName);

            #region Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(EditingInitialTreatment.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }
            #endregion Signature
        }

        private void buildAcupunctureRequest(ProcessRequest req, object obj) {
            UCAcupunctureDetail ucDetail = obj as UCAcupunctureDetail;
            AcupunctureDetail detail = ucDetail.EditingAcupunctureDetail;

            //0) Template Title
            req.AddWordReplacement("{treatment note title}", _treatmentNoteTitle);

            //Patient patient = detail.InitialTreatment.Patient;
            Patient patient = EditingInitialTreatment.Patient;

            //0.1) Patient name
            //req.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            req.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            req.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            req.AddWordReplacement("{file number}", patient.FileNumber);


            //1) Diagram
            List<PictureReplacement> picDiagram = new List<PictureReplacement>();
            string diagramImagePath = Program.CreateDiagramImageBy(ucDetail.TongueDiagramPictureBox);
            picDiagram.Add(new PictureReplacement(diagramImagePath));
            req.Replacements.Add("{diagram}", picDiagram);

            //2) Tongue 
            req.AddWordReplacement("{tongue}", detail.Tongue);

            //3) Pulse 
            req.AddWordReplacement("{pulse}", detail.Pulse);

            //4) Blood pressure
            req.AddWordReplacement("{blood pressure}", detail.BloodPressure);

            //5) Blood Sugar 
            req.AddWordReplacement("{blood sugar}", detail.BloodSugar);

            //6) Subjective 
            req.AddWordReplacement("{subjective}", detail.Subjective);

            //7) TCM Diagnosis 
            req.AddWordReplacement("{tcm diagnosis}", detail.TCMDiagnosis);

            //8) Treatment plan 
            req.AddWordReplacement("{treatment plan}", detail.TreatmentPlan);

            //9) Date/duration
            req.AddWordReplacement("{date}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", EditingInitialTreatment.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", EditingInitialTreatment.TreatmentDurationMinutes.ToString());
            //10) Therapist
            req.AddWordReplacement("{therapist}", EditingInitialTreatment.Therapist.ToString());

            //Header
            req.AddWordReplacement("{patient name}", EditingInitialTreatment.Patient.DisplayName);
            req.AddWordReplacement("{treatment type}", EditingInitialTreatment.TreatmentType.DisplayName);
            
            //11) Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(EditingInitialTreatment.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }
        }

        private void buildMassageRequest(ProcessRequest req, object obj) {
            UCMassageDetail ucDetail = obj as UCMassageDetail;
            MassageDetail detail = ucDetail.EditingMassageDetail;

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

            //Patient patient = detail.InitialTreatment.Patient;
            Patient patient = EditingInitialTreatment.Patient;

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
            req.AddWordReplacement("{activity limitation}", detail.ActivityLimitations);
            req.AddWordReplacement("{treatment goal}", detail.TreatmentGoal);
            req.AddWordReplacement("{treatment focus}", detail.TreatmentFocus);
            req.AddWordReplacement("{treatment frequency}", detail.TreatmentFrequency);
            req.AddWordReplacement("{treatment duration}", detail.TreatmentDuration);

            req.AddWordReplacement("{discussed}",
                (detail.TreatmentPlanDiscussedWithClient.HasValue ? (
                    detail.TreatmentPlanDiscussedWithClient.Value ? "Yes" : "No") 
                    : ""));
            req.AddWordReplacement("{consent received}",
                (detail.ConsentReceived.HasValue ? (
                    detail.ConsentReceived.Value ? "Yes" : "No")
                    : ""));

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

            req.AddWordReplacement("{other areas to be treated}", detail.TreatmentAreaOther);

            req.AddWordReplacement("{assessments performed}", detail.AssessmentsPerformed);
            req.AddWordReplacement("{results of assessments}", detail.ResultsOfAssessments);
            req.AddWordReplacement("{reassessment schedule}", detail.ReassessmentSchedule);
            req.AddWordReplacement("{referrals}", detail.Referrals);
            req.AddWordReplacement("{anticipated progression of responses}", detail.AnticipatedProgressionOfResponses);
            req.AddWordReplacement("{remedial exercises recommendations}", detail.RemedialExercisesRecommended);
            req.AddWordReplacement("{risks}", detail.Risks);

            //2) Diagram
            List<PictureReplacement> picDiagram = new List<PictureReplacement>();
            string diagramImagePath = Program.CreateDiagramImageBy(ucDetail.MassageDiagramPictureBox);
            picDiagram.Add(new PictureReplacement(diagramImagePath));
            req.Replacements.Add("{diagram}", picDiagram);

            //Header
            req.AddWordReplacement("{patient name}", EditingInitialTreatment.Patient.DisplayName);
            req.AddWordReplacement("{treatment type}", EditingInitialTreatment.TreatmentType.DisplayName);

            //3) Date/duration
            req.AddWordReplacement("{date}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", EditingInitialTreatment.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", EditingInitialTreatment.TreatmentDurationMinutes.ToString());

            //4) Therapist
            req.AddWordReplacement("{therapist}", EditingInitialTreatment.Therapist.ToString());

            //5) Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(EditingInitialTreatment.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }
        }

        private void buildOsteopathyRequest(ProcessRequest req, object obj) {
            UCOsteopathyDetail ucDetail = obj as UCOsteopathyDetail;
            OsteopathyDetail detail = ucDetail.EditingOsteopathyDetail;

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

            //Patient patient = detail.InitialTreatment.Patient;
            Patient patient = EditingInitialTreatment.Patient;

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
            req.AddWordReplacement("{activity limitation}", detail.ActivityLimitations);
            req.AddWordReplacement("{treatment goal}", detail.TreatmentGoal);
            req.AddWordReplacement("{treatment focus}", detail.TreatmentFocus);
            req.AddWordReplacement("{treatment frequency}", detail.TreatmentFrequency);
            req.AddWordReplacement("{treatment duration}", detail.TreatmentDuration);

            req.AddWordReplacement("{discussed}",
                (detail.TreatmentPlanDiscussedWithClient.HasValue ? (
                    detail.TreatmentPlanDiscussedWithClient.Value ? "Yes" : "No")
                    : ""));
            req.AddWordReplacement("{consent received}",
                (detail.ConsentReceived.HasValue ? (
                    detail.ConsentReceived.Value ? "Yes" : "No")
                    : ""));

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

            req.AddWordReplacement("{other areas to be treated}", detail.TreatmentAreaOther);

            req.AddWordReplacement("{assessments performed}", detail.AssessmentsPerformed);
            req.AddWordReplacement("{results of assessments}", detail.ResultsOfAssessments);
            req.AddWordReplacement("{reassessment schedule}", detail.ReassessmentSchedule);
            req.AddWordReplacement("{referrals}", detail.Referrals);
            req.AddWordReplacement("{anticipated progression of responses}", detail.AnticipatedProgressionOfResponses);
            req.AddWordReplacement("{remedial exercises recommendations}", detail.RemedialExercisesRecommended);
            req.AddWordReplacement("{risks}", detail.Risks);

            //2) Diagram
            List<PictureReplacement> picDiagram = new List<PictureReplacement>();
            string diagramImagePath = Program.CreateDiagramImageBy(ucDetail.OsteopathyDiagramPictureBox);
            picDiagram.Add(new PictureReplacement(diagramImagePath));
            req.Replacements.Add("{diagram}", picDiagram);

            //Header
            req.AddWordReplacement("{patient name}", EditingInitialTreatment.Patient.DisplayName);
            req.AddWordReplacement("{treatment type}", EditingInitialTreatment.TreatmentType.DisplayName);

            //3) Date/duration
            req.AddWordReplacement("{date}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", EditingInitialTreatment.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", EditingInitialTreatment.TreatmentDurationMinutes.ToString());

            //4) Therapist
            req.AddWordReplacement("{therapist}", EditingInitialTreatment.Therapist.ToString());

            //5) Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(EditingInitialTreatment.Therapist.SignatureImage);
            if (signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }
        }

        private void buildNaturopathicRequest(ProcessRequest req, object obj) {
            UCNaturopathicDetail ucDetail = obj as UCNaturopathicDetail;
            NaturopathicDetail detail = ucDetail.EditingNaturopathicDetail;

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

            //Patient patient = detail.InitialTreatment.Patient;
            Patient patient = EditingInitialTreatment.Patient;

            //0.1) Patient name
            //req.AddWordReplacement("{patient name}", patient.ToString());
            //0.2) Patient birth day
            req.AddWordReplacement("{dob}", patient.DateOfBirth.ToString(Constants.DATE_FORMAT));
            //0.3) Patient address
            req.AddWordReplacement("{patient address}",
                patient.ContactInfo != null ? (patient.ContactInfo.Address != null ? patient.ContactInfo.Address.ToString() : string.Empty) : string.Empty);
            //0.4) Patient File #
            req.AddWordReplacement("{file number}", patient.FileNumber);


            //1) Your current Health
            req.AddWordReplacement("{health state}", detail.HealthState == HealthState.Unknown ? "  " : detail.HealthState.ToString());
            req.AddWordReplacement("{energy scale}", detail.CurrentEnergyLevel > 0 ? detail.CurrentEnergyLevel.ToString() : "  ");
            req.AddWordReplacement("{morning energy level}", detail.MorningEnergyLevel > 0 ? detail.MorningEnergyLevel.ToString() : "  ");
            req.AddWordReplacement("{current weight}", detail.CurrentWeight);
            req.AddWordReplacement("{weight year ago}", detail.YearAgoWeight);
            req.AddWordReplacement("{ideal weight}", detail.IdealWeight);
            req.AddWordReplacement("{height}", detail.CurrentHeight);

            //2) Stress Management
            req.AddWordReplacement("{stress level}", detail.CurrentStressLevel > 0 ? detail.CurrentStressLevel.ToString() : "  ");
            req.AddWordReplacement("{stress identified by}", detail.StressIdentifiedBy == SelfOther.Unknown ? "  " : detail.StressIdentifiedBy.ToString());
            req.AddWordReplacement("{stress behavior}", detail.StressedBehavior);
            req.AddWordReplacement("{stress localized of body}", detail.StressLocalizedOfBody);
            req.AddWordReplacement("{stress handling tool}", detail.StressHandlingTools);

            //3) Allergies
            req.AddWordReplacement("{drug allergy yes no}", detail.HasDrugAllergy.HasValue ? (detail.HasDrugAllergy.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{drug allergies}", detail.HasDrugAllergy.HasValue && detail.HasDrugAllergy.Value ? detail.DrugAllergies : "  ");
            req.AddWordReplacement("{food allergy yes no}", detail.HasFoodAllergy.HasValue ? (detail.HasFoodAllergy.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{food allergies}", detail.HasFoodAllergy.HasValue && detail.HasFoodAllergy.Value ? detail.FoodAllergies : "  ");
            req.AddWordReplacement("{animal allergy yes no}", detail.HasAnimalAllergy.HasValue ? (detail.HasAnimalAllergy.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{animal allergies}", detail.HasAnimalAllergy.HasValue && detail.HasAnimalAllergy.Value ? detail.AnimalAllergies : "  ");
            req.AddWordReplacement("{plant allergy yes no}", detail.HasPlantAllergy.HasValue ? (detail.HasPlantAllergy.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{plant allergies}", detail.HasPlantAllergy.HasValue && detail.HasPlantAllergy.Value ? detail.PlantAllergies : "  ");
            req.AddWordReplacement("{env allergy yes no}", detail.HasEnvironmentalAllergy.HasValue ? (detail.HasEnvironmentalAllergy.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{env allergies}", detail.HasEnvironmentalAllergy.HasValue && detail.HasEnvironmentalAllergy.Value ? detail.EnvironmentalAllergies : "  ");

            //4) Accidents / Hospitalizations
            req.AddWordReplacement("{major injuries}", detail.MajorInjuries);
            req.AddWordReplacement("{surgeries}", detail.Surgeries);
            req.AddWordReplacement("{flu vaccine yes no}", detail.HadFluVaccineLastFiveYears.HasValue ? (detail.HadFluVaccineLastFiveYears.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{vaccine reaction yes no}", detail.HadReactionToVaccine.HasValue ? (detail.HadReactionToVaccine.Value ? "Yes" : "No") : "  ");

            //5) Current / Past Conditions
            req.Replacements.Add("{chkCP11C}", detail.C_Allergies.HasValue && detail.C_Allergies.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP11P}", detail.P_Allergies.HasValue && detail.P_Allergies.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP12C}", detail.C_Anemia.HasValue && detail.C_Anemia.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP12P}", detail.P_Anemia.HasValue && detail.P_Anemia.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP13C}", detail.C_Stroke.HasValue && detail.C_Stroke.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP13P}", detail.P_Stroke.HasValue && detail.P_Stroke.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP14C}", detail.C_Depression.HasValue && detail.C_Depression.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP14P}", detail.P_Depression.HasValue && detail.P_Depression.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP21C}", detail.C_Asthma.HasValue && detail.C_Asthma.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP21P}", detail.P_Asthma.HasValue && detail.P_Asthma.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP22C}", detail.C_Measles.HasValue && detail.C_Measles.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP22P}", detail.P_Measles.HasValue && detail.P_Measles.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP23C}", detail.C_HeartDisease.HasValue && detail.C_HeartDisease.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP23P}", detail.P_HeartDisease.HasValue && detail.P_HeartDisease.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP24C}", detail.C_EmotionalAbuse.HasValue && detail.C_EmotionalAbuse.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP24P}", detail.P_EmotionalAbuse.HasValue && detail.P_EmotionalAbuse.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP31C}", detail.C_Eczema.HasValue && detail.C_Eczema.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP31P}", detail.P_Eczema.HasValue && detail.P_Eczema.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP32C}", detail.C_Mumps.HasValue && detail.C_Mumps.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP32P}", detail.P_Mumps.HasValue && detail.P_Mumps.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP33C}", detail.C_RheumaticFever.HasValue && detail.C_RheumaticFever.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP33P}", detail.P_RheumaticFever.HasValue && detail.P_RheumaticFever.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP34C}", detail.C_PhysicalMentalAbuse.HasValue && detail.C_PhysicalMentalAbuse.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP34P}", detail.P_PhysicalMentalAbuse.HasValue && detail.P_PhysicalMentalAbuse.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP41C}", detail.C_Psoriasis.HasValue && detail.C_Psoriasis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP41P}", detail.P_Psoriasis.HasValue && detail.P_Psoriasis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP42C}", detail.C_ChickenPox.HasValue && detail.C_ChickenPox.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP42P}", detail.P_ChickenPox.HasValue && detail.P_ChickenPox.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP43C}", detail.C_HighBloodPressure.HasValue && detail.C_HighBloodPressure.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP43P}", detail.P_HighBloodPressure.HasValue && detail.P_HighBloodPressure.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP44C}", detail.C_NumbnessTingling.HasValue && detail.C_NumbnessTingling.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP44P}", detail.P_NumbnessTingling.HasValue && detail.P_NumbnessTingling.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP51C}", detail.C_HayFever.HasValue && detail.C_HayFever.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP51P}", detail.P_HayFever.HasValue && detail.P_HayFever.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP52C}", detail.C_WhoopingCough.HasValue && detail.C_WhoopingCough.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP52P}", detail.P_WhoopingCough.HasValue && detail.P_WhoopingCough.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP53C}", detail.C_HighCholesterol.HasValue && detail.C_HighCholesterol.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP53P}", detail.P_HighCholesterol.HasValue && detail.P_HighCholesterol.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP54C}", detail.C_ColdHandsFeet.HasValue && detail.C_ColdHandsFeet.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP54P}", detail.P_ColdHandsFeet.HasValue && detail.P_ColdHandsFeet.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP61C}", detail.C_Pneumonia.HasValue && detail.C_Pneumonia.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP61P}", detail.P_Pneumonia.HasValue && detail.P_Pneumonia.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP62C}", detail.C_Shingles.HasValue && detail.C_Shingles.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP62P}", detail.P_Shingles.HasValue && detail.P_Shingles.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP63C}", detail.C_Cancer.HasValue && detail.C_Cancer.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP63P}", detail.P_Cancer.HasValue && detail.P_Cancer.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP64C}", detail.C_ThyroidProblems.HasValue && detail.C_ThyroidProblems.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP64P}", detail.P_ThyroidProblems.HasValue && detail.P_ThyroidProblems.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP71C}", detail.C_EarInfections.HasValue && detail.C_EarInfections.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP71P}", detail.P_EarInfections.HasValue && detail.P_EarInfections.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP72C}", detail.C_Diphtheria.HasValue && detail.C_Diphtheria.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP72P}", detail.P_Diphtheria.HasValue && detail.P_Diphtheria.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP73C}", detail.C_Type1Diabetes.HasValue && detail.C_Type1Diabetes.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP73P}", detail.P_Type1Diabetes.HasValue && detail.P_Type1Diabetes.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP74C}", detail.C_Warts.HasValue && detail.C_Warts.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP74P}", detail.P_Warts.HasValue && detail.P_Warts.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP81C}", detail.C_StrepThroat.HasValue && detail.C_StrepThroat.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP81P}", detail.P_StrepThroat.HasValue && detail.P_StrepThroat.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP82C}", detail.C_ScarletFever.HasValue && detail.C_ScarletFever.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP82P}", detail.P_ScarletFever.HasValue && detail.P_ScarletFever.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP83C}", detail.C_Type2Diabetes.HasValue && detail.C_Type2Diabetes.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP83P}", detail.P_Type2Diabetes.HasValue && detail.P_Type2Diabetes.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP84C}", detail.C_Mono.HasValue && detail.C_Mono.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP84P}", detail.P_Mono.HasValue && detail.P_Mono.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCP91C}", detail.C_Tonsillitis.HasValue && detail.C_Tonsillitis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP91P}", detail.P_Tonsillitis.HasValue && detail.P_Tonsillitis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP92C}", detail.C_Polio.HasValue && detail.C_Polio.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP92P}", detail.P_Polio.HasValue && detail.P_Polio.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP93C}", detail.C_KidneyDisease.HasValue && detail.C_KidneyDisease.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP93P}", detail.P_KidneyDisease.HasValue && detail.P_KidneyDisease.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP94C}", detail.C_RheumatoidArthritis.HasValue && detail.C_RheumatoidArthritis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCP94P}", detail.P_RheumatoidArthritis.HasValue && detail.P_RheumatoidArthritis.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCPa1C}", detail.C_CankerSores.HasValue && detail.C_CankerSores.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPa1P}", detail.P_CankerSores.HasValue && detail.P_CankerSores.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPa2C}", detail.C_Smallpox.HasValue && detail.C_Smallpox.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPa2P}", detail.P_Smallpox.HasValue && detail.P_Smallpox.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPa3C}", detail.C_VisualProblems.HasValue && detail.C_VisualProblems.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPa3P}", detail.P_VisualProblems.HasValue && detail.P_VisualProblems.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPa4C}", detail.C_Osteoarthritis.HasValue && detail.C_Osteoarthritis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPa4P}", detail.P_Osteoarthritis.HasValue && detail.P_Osteoarthritis.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCPb1C}", detail.C_Jaundice.HasValue && detail.C_Jaundice.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPb1P}", detail.P_Jaundice.HasValue && detail.P_Jaundice.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPb2C}", detail.C_Tuberculosis.HasValue && detail.C_Tuberculosis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPb2P}", detail.P_Tuberculosis.HasValue && detail.P_Tuberculosis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPb3C}", detail.C_AutoimmuneDisease.HasValue && detail.C_AutoimmuneDisease.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPb3P}", detail.P_AutoimmuneDisease.HasValue && detail.P_AutoimmuneDisease.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPb4C}", detail.C_WeightProblems.HasValue && detail.C_WeightProblems.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPb4P}", detail.P_WeightProblems.HasValue && detail.P_WeightProblems.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCPc1C}", detail.C_Alcoholism.HasValue && detail.C_Alcoholism.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPc1P}", detail.P_Alcoholism.HasValue && detail.P_Alcoholism.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPc2C}", detail.C_Malaria.HasValue && detail.C_Malaria.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPc2P}", detail.P_Malaria.HasValue && detail.P_Malaria.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPc3C}", detail.C_Epilepsy.HasValue && detail.C_Epilepsy.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPc3P}", detail.P_Epilepsy.HasValue && detail.P_Epilepsy.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPc4C}", detail.C_Gout.HasValue && detail.C_Gout.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPc4P}", detail.P_Gout.HasValue && detail.P_Gout.Value ? checkReplacements : uncheckReplacements);

            req.Replacements.Add("{chkCPd2C}", detail.C_Hepatitis.HasValue && detail.C_Hepatitis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPd2P}", detail.P_Hepatitis.HasValue && detail.P_Hepatitis.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPd3C}", detail.C_Gallstones.HasValue && detail.C_Gallstones.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPd3P}", detail.P_Gallstones.HasValue && detail.P_Gallstones.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPd4C}", detail.C_VaricoseVeins.HasValue && detail.C_VaricoseVeins.Value ? checkReplacements : uncheckReplacements);
            req.Replacements.Add("{chkCPd4P}", detail.P_VaricoseVeins.HasValue && detail.P_VaricoseVeins.Value ? checkReplacements : uncheckReplacements);

            req.AddWordReplacement("{CP other}", detail.CP_Other);
            req.AddWordReplacement("{CP never well since}", detail.CP_NeverWellSince);
            
            //6) Currently use
            req.AddWordReplacement("{CU alcohol}", detail.CU_Alcohol);
            req.AddWordReplacement("{CU tobacco}", detail.CU_Tobacco);
            req.AddWordReplacement("{CU hormones}", detail.CU_Hormones);
            req.AddWordReplacement("{CU coffee}", detail.CU_Coffee);
            req.AddWordReplacement("{CU cortisone}", detail.CU_Cortisone);
            req.AddWordReplacement("{CU blacktea}", detail.CU_BlackTea);
            req.AddWordReplacement("{CU sedatives}", detail.CU_Sedatives);
            req.AddWordReplacement("{CU laxatives}", detail.CU_Laxatives);
            req.AddWordReplacement("{CU antacids}", detail.CU_Antacids);
            
            //7) Current Prescription Medications
            req.AddWordReplacement("{CPM name 1}", detail.CPM_Name1);
            req.AddWordReplacement("{CPM reason 1}", detail.CPM_Reason1);
            req.AddWordReplacement("{CPM amount 1}", detail.CPM_Amount1);
            req.AddWordReplacement("{CPM prescription date 1}", detail.CPM_PrescriptionDate1);
            req.AddWordReplacement("{CPM drug category 1}", detail.CPM_DrugCategory1);

            req.AddWordReplacement("{CPM name 2}", detail.CPM_Name2);
            req.AddWordReplacement("{CPM reason 2}", detail.CPM_Reason2);
            req.AddWordReplacement("{CPM amount 2}", detail.CPM_Amount2);
            req.AddWordReplacement("{CPM prescription date 2}", detail.CPM_PrescriptionDate2);
            req.AddWordReplacement("{CPM drug category 2}", detail.CPM_DrugCategory2);

            req.AddWordReplacement("{CPM name 3}", detail.CPM_Name3);
            req.AddWordReplacement("{CPM reason 3}", detail.CPM_Reason3);
            req.AddWordReplacement("{CPM amount 3}", detail.CPM_Amount3);
            req.AddWordReplacement("{CPM prescription date 3}", detail.CPM_PrescriptionDate3);
            req.AddWordReplacement("{CPM drug category 3}", detail.CPM_DrugCategory3);

            req.AddWordReplacement("{CPM name 4}", detail.CPM_Name4);
            req.AddWordReplacement("{CPM reason 4}", detail.CPM_Reason4);
            req.AddWordReplacement("{CPM amount 4}", detail.CPM_Amount4);
            req.AddWordReplacement("{CPM prescription date 4}", detail.CPM_PrescriptionDate4);
            req.AddWordReplacement("{CPM drug category 4}", detail.CPM_DrugCategory4);

            req.AddWordReplacement("{CPM name 5}", detail.CPM_Name5);
            req.AddWordReplacement("{CPM reason 5}", detail.CPM_Reason5);
            req.AddWordReplacement("{CPM amount 5}", detail.CPM_Amount5);
            req.AddWordReplacement("{CPM prescription date 5}", detail.CPM_PrescriptionDate5);
            req.AddWordReplacement("{CPM drug category 5}", detail.CPM_DrugCategory5);
            
            //8) Vitamins / Herbs
            req.AddWordReplacement("{VH name 1}", detail.VH_Name1);
            req.AddWordReplacement("{VH brand 1}", detail.VH_Brand1);
            req.AddWordReplacement("{VH amount 1}", detail.VH_Amount1);
            req.AddWordReplacement("{VH reason 1}", detail.VH_Reason1);
            req.AddWordReplacement("{VH suggested by 1}", detail.VH_SuggestedBy1 == SelfOther.Unknown ? "" : detail.VH_SuggestedBy1.ToString());
            req.AddWordReplacement("{VH helped 1}", detail.VH_Helped1 == YesNo.Unknown ? "" : detail.VH_Helped1.ToString());

            req.AddWordReplacement("{VH name 2}", detail.VH_Name2);
            req.AddWordReplacement("{VH brand 2}", detail.VH_Brand2);
            req.AddWordReplacement("{VH amount 2}", detail.VH_Amount2);
            req.AddWordReplacement("{VH reason 2}", detail.VH_Reason2);
            req.AddWordReplacement("{VH suggested by 2}", detail.VH_SuggestedBy2 == SelfOther.Unknown ? "" : detail.VH_SuggestedBy2.ToString());
            req.AddWordReplacement("{VH helped 2}", detail.VH_Helped2 == YesNo.Unknown ? "" : detail.VH_Helped2.ToString());

            req.AddWordReplacement("{VH name 3}", detail.VH_Name3);
            req.AddWordReplacement("{VH brand 3}", detail.VH_Brand3);
            req.AddWordReplacement("{VH amount 3}", detail.VH_Amount3);
            req.AddWordReplacement("{VH reason 3}", detail.VH_Reason3);
            req.AddWordReplacement("{VH suggested by 3}", detail.VH_SuggestedBy3 == SelfOther.Unknown ? "" : detail.VH_SuggestedBy3.ToString());
            req.AddWordReplacement("{VH helped 3}", detail.VH_Helped3 == YesNo.Unknown ? "" : detail.VH_Helped3.ToString());

            req.AddWordReplacement("{VH name 4}", detail.VH_Name4);
            req.AddWordReplacement("{VH brand 4}", detail.VH_Brand4);
            req.AddWordReplacement("{VH amount 4}", detail.VH_Amount4);
            req.AddWordReplacement("{VH reason 4}", detail.VH_Reason4);
            req.AddWordReplacement("{VH suggested by 4}", detail.VH_SuggestedBy4 == SelfOther.Unknown ? "" : detail.VH_SuggestedBy4.ToString());
            req.AddWordReplacement("{VH helped 4}", detail.VH_Helped4 == YesNo.Unknown ? "" : detail.VH_Helped4.ToString());

            req.AddWordReplacement("{VH name 5}", detail.VH_Name5);
            req.AddWordReplacement("{VH brand 5}", detail.VH_Brand5);
            req.AddWordReplacement("{VH amount 5}", detail.VH_Amount5);
            req.AddWordReplacement("{VH reason 5}", detail.VH_Reason5);
            req.AddWordReplacement("{VH suggested by 5}", detail.VH_SuggestedBy5 == SelfOther.Unknown ? "" : detail.VH_SuggestedBy5.ToString());
            req.AddWordReplacement("{VH helped 5}", detail.VH_Helped5 == YesNo.Unknown ? "" : detail.VH_Helped5.ToString());

            req.AddWordReplacement("{VH other yes no}", detail.VH_HasOtherSupplementation.HasValue ? (detail.VH_HasOtherSupplementation.Value ? "Yes" : "No") : "");
            req.AddWordReplacement("{VH other}", detail.VH_HasOtherSupplementation.HasValue && detail.VH_HasOtherSupplementation.Value ? detail.VH_OtherSupplementation : "");

            //9) Family History
            req.AddWordReplacement("{FH mother 1}", detail.FH_CancerMother);
            req.AddWordReplacement("{FH father 1}", detail.FH_CancerFather);
            req.AddWordReplacement("{FH sibling 1}", detail.FH_CancerSibling);
            req.AddWordReplacement("{FH grandparent 1}", detail.FH_CancerGrandparent);

            req.AddWordReplacement("{FH mother 2}", detail.FH_RheumatoidMother);
            req.AddWordReplacement("{FH father 2}", detail.FH_RheumatoidFather);
            req.AddWordReplacement("{FH sibling 2}", detail.FH_RheumatoidSibling);
            req.AddWordReplacement("{FH grandparent 2}", detail.FH_RheumatoidGrandparent);

            req.AddWordReplacement("{FH mother 3}", detail.FH_TuberculosisMother);
            req.AddWordReplacement("{FH father 3}", detail.FH_TuberculosisFather);
            req.AddWordReplacement("{FH sibling 3}", detail.FH_TuberculosisSibling);
            req.AddWordReplacement("{FH grandparent 3}", detail.FH_TuberculosisGrandparent);

            req.AddWordReplacement("{FH mother 4}", detail.FH_OsteoarthritisMother);
            req.AddWordReplacement("{FH father 4}", detail.FH_OsteoarthritisFather);
            req.AddWordReplacement("{FH sibling 4}", detail.FH_OsteoarthritisSibling);
            req.AddWordReplacement("{FH grandparent 4}", detail.FH_OsteoarthritisGrandparent);

            req.AddWordReplacement("{FH mother 5}", detail.FH_HeartDiseaseMother);
            req.AddWordReplacement("{FH father 5}", detail.FH_HeartDiseaseFather);
            req.AddWordReplacement("{FH sibling 5}", detail.FH_HeartDiseaseSibling);
            req.AddWordReplacement("{FH grandparent 5}", detail.FH_HeartDiseaseGrandparent);

            req.AddWordReplacement("{FH mother 6}", detail.FH_AllergiesMother);
            req.AddWordReplacement("{FH father 6}", detail.FH_AllergiesFather);
            req.AddWordReplacement("{FH sibling 6}", detail.FH_AllergiesSibling);
            req.AddWordReplacement("{FH grandparent 6}", detail.FH_AllergiesGrandparent);

            req.AddWordReplacement("{FH mother 7}", detail.FH_StrokeMother);
            req.AddWordReplacement("{FH father 7}", detail.FH_StrokeFather);
            req.AddWordReplacement("{FH sibling 7}", detail.FH_StrokeSibling);
            req.AddWordReplacement("{FH grandparent 7}", detail.FH_StrokeGrandparent);

            req.AddWordReplacement("{FH mother 8}", detail.FH_AsthmaMother);
            req.AddWordReplacement("{FH father 8}", detail.FH_AsthmaFather);
            req.AddWordReplacement("{FH sibling 8}", detail.FH_AsthmaSibling);
            req.AddWordReplacement("{FH grandparent 8}", detail.FH_AsthmaGrandparent);

            req.AddWordReplacement("{FH mother 9}", detail.FH_HighBloodPressureMother);
            req.AddWordReplacement("{FH father 9}", detail.FH_HighBloodPressureFather);
            req.AddWordReplacement("{FH sibling 9}", detail.FH_HighBloodPressureSibling);
            req.AddWordReplacement("{FH grandparent 9}", detail.FH_HighBloodPressureGrandparent);

            req.AddWordReplacement("{FH mother a}", detail.FH_DiabetesTypeIMother);
            req.AddWordReplacement("{FH father a}", detail.FH_DiabetesTypeIFather);
            req.AddWordReplacement("{FH sibling a}", detail.FH_DiabetesTypeISibling);
            req.AddWordReplacement("{FH grandparent a}", detail.FH_DiabetesTypeIGrandparent);

            req.AddWordReplacement("{FH mother b}", detail.FH_DiabetesTypeIIMother);
            req.AddWordReplacement("{FH father b}", detail.FH_DiabetesTypeIIFather);
            req.AddWordReplacement("{FH sibling b}", detail.FH_DiabetesTypeIISibling);
            req.AddWordReplacement("{FH grandparent b}", detail.FH_DiabetesTypeIIGrandparent);

            req.AddWordReplacement("{FH mother c}", detail.FH_HighCholesterolMother);
            req.AddWordReplacement("{FH father c}", detail.FH_HighCholesterolFather);
            req.AddWordReplacement("{FH sibling c}", detail.FH_HighCholesterolSibling);
            req.AddWordReplacement("{FH grandparent c}", detail.FH_HighCholesterolGrandparent);

            req.AddWordReplacement("{FH mother d}", detail.FH_DepressionMother);
            req.AddWordReplacement("{FH father d}", detail.FH_DepressionFather);
            req.AddWordReplacement("{FH sibling d}", detail.FH_DepressionSibling);
            req.AddWordReplacement("{FH grandparent d}", detail.FH_DepressionGrandparent);

            req.AddWordReplacement("{FH mother e}", detail.FH_KidneyDiseaseMother);
            req.AddWordReplacement("{FH father e}", detail.FH_KidneyDiseaseFather);
            req.AddWordReplacement("{FH sibling e}", detail.FH_KidneyDiseaseSibling);
            req.AddWordReplacement("{FH grandparent e}", detail.FH_KidneyDiseaseGrandparent);

            req.AddWordReplacement("{FH other name}", detail.FH_OtherName);
            req.AddWordReplacement("{FH mother f}", detail.FH_OtherMother);
            req.AddWordReplacement("{FH father f}", detail.FH_OtherFather);
            req.AddWordReplacement("{FH sibling f}", detail.FH_OtherSibling);
            req.AddWordReplacement("{FH grandparent f}", detail.FH_OtherGrandparent);

            //10) Personal Habits
            req.AddWordReplacement("{PH enjoy most}", detail.PH_EnjoyMost);
            req.AddWordReplacement("{PH main interests}", detail.PH_MainInterests);
            req.AddWordReplacement("{PH worry most}", detail.PH_WorryMost);
            req.AddWordReplacement("{PH what nurtures}", detail.PH_WhatNurtures);
            req.AddWordReplacement("{PH exercise yes no}", detail.PH_DoExercise.HasValue ? (detail.PH_DoExercise.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{PH exercise}", detail.PH_DoExercise.HasValue && detail.PH_DoExercise.Value ? detail.PH_ExerciseDetails : "  ");
            req.AddWordReplacement("{PH religious practice yes no}", detail.PH_ReligiousPractice.HasValue ? (detail.PH_ReligiousPractice.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{PH body temperature}", detail.PH_BodyTemperature == BodyTemperature.Unknown ? "  " : detail.PH_BodyTemperature.ToString());
            req.AddWordReplacement("{PH enjoy work yes no}", detail.PH_EnjoyWork.HasValue ? (detail.PH_EnjoyWork.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{PH take vacations yes no}", detail.PH_TakeVacations.HasValue ? (detail.PH_TakeVacations.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{PH cold flu details}", detail.PH_ColdFluDetails);

            //11) Sleep Habits
            req.AddWordReplacement("{SH sleep quality}", detail.SH_SleepQuality > 0 ? detail.SH_SleepQuality.ToString() : "  ");
            req.AddWordReplacement("{SH falling asleep problem yes no}", detail.SH_FallingAsleepProblem.HasValue ? (detail.SH_FallingAsleepProblem.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{SH staying asleep problem yes no}", detail.SH_StayingAsleepProblem.HasValue ? (detail.SH_StayingAsleepProblem.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{SH hours in sleep}", detail.SH_HoursInSleep);
            req.AddWordReplacement("{SH hours needed in sleep}", detail.SH_HoursNeededInSleep);
            req.AddWordReplacement("{SH wake refreshed yes no}", detail.SH_WakeRefreshed.HasValue ? (detail.SH_WakeRefreshed.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{SH take nap yes no}", detail.SH_TakeNap.HasValue ? (detail.SH_TakeNap.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{SH nap duration}", detail.SH_TakeNap.HasValue && detail.SH_TakeNap.Value ? detail.SH_NapDuration : "  ");

            //12) Female
            req.AddWordReplacement("{F first menses age}", detail.F_FirstMensesAge);
            req.AddWordReplacement("{F periods stopped age}", detail.F_PeriodsStoppedAge);
            req.AddWordReplacement("{F cycles regular yes no}", detail.F_CyclesRegular.HasValue ? (detail.F_CyclesRegular.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F period begins every days}", detail.F_CyclesRegular.HasValue && detail.F_CyclesRegular.Value ? detail.F_PeriodBeginsEveryDays : "  ");
            req.AddWordReplacement("{F period lasts days}", detail.F_CyclesRegular.HasValue && detail.F_CyclesRegular.Value ? detail.F_PeriodLastsDays : "  ");
            req.AddWordReplacement("{F period flow}", detail.F_PeriodFlow);
            req.AddWordReplacement("{F period blood color}", detail.F_PeriodBloodColor);
            req.AddWordReplacement("{F any clots yes no}", detail.F_AnyClots.HasValue ? (detail.F_AnyClots.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F any cramps yes no}", detail.F_AnyCramps.HasValue ? (detail.F_AnyCramps.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F spotting bleeding yes no}", detail.F_AnySpottingBleeding.HasValue ? (detail.F_AnySpottingBleeding.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F spotting bleeding every month yes no}", detail.F_AnySpottingBleeding.HasValue && detail.F_AnySpottingBleeding.Value ? (detail.F_SpottingBleedingEveryMonth.HasValue ? (detail.F_SpottingBleedingEveryMonth.Value ? "Yes" : "No") : "  ") : "  ");
            req.AddWordReplacement("{F yeast infection yes no}", detail.F_YeastInfectionInPast.HasValue ? (detail.F_YeastInfectionInPast.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F yeast infection frequency}", detail.F_YeastInfectionInPast.HasValue && detail.F_YeastInfectionInPast.Value ? detail.F_YeastInfectionFrequency : "  ");
            req.AddWordReplacement("{F yeast infection treatment}", detail.F_YeastInfectionInPast.HasValue && detail.F_YeastInfectionInPast.Value ? detail.F_YeastInfectionTreatment : "  ");
            req.AddWordReplacement("{F premenstrual symptoms yes no}", detail.F_AnyPremenstrualSymptoms.HasValue ? (detail.F_AnyPremenstrualSymptoms.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F premenstrual symptoms details}", detail.F_AnyPremenstrualSymptoms.HasValue && detail.F_AnyPremenstrualSymptoms.Value ? detail.F_PremenstrualSymptomsDetails : "  ");
            req.AddWordReplacement("{F number of pregnancies}", detail.F_NumberOfPregnancies);
            req.AddWordReplacement("{F number of miscarriages}", detail.F_NumberOfMiscarriages);
            req.AddWordReplacement("{F number of live births}", detail.F_NumberOfLiveBirths);
            req.AddWordReplacement("{F pregnancy problem}", detail.F_AnyPregancyProblem);
            req.AddWordReplacement("{F regular PAP yes no}", detail.F_RegularPAP.HasValue ? (detail.F_RegularPAP.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F abnormal PAP yes no}", detail.F_AnyAbnormalPAP.HasValue ? (detail.F_AnyAbnormalPAP.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F regular breast self-exam yes no}", detail.F_RegularBreastSelfExam.HasValue ? (detail.F_RegularBreastSelfExam.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{F any breast lumps yes no}", detail.F_NoticedBreastLumps.HasValue ? (detail.F_NoticedBreastLumps.Value ? "Yes" : "No") : "  ");

            //13) Digestion and Elimination
            req.AddWordReplacement("{DE gas problem yes no}", detail.DE_ProblemWithGasBloating.HasValue ? (detail.DE_ProblemWithGasBloating.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE HemoRectalParas}", string.Format("{0}/{1}/{2}",
                detail.DE_Hemorrhoids.HasValue && detail.DE_Hemorrhoids.Value ? "Hemorrhoids" : "",
                detail.DE_RectalBleeding.HasValue && detail.DE_RectalBleeding.Value ? "Rectal bleeding" : "",
                detail.DE_Parasites.HasValue && detail.DE_Parasites.Value ? "Parasites" : ""));
            req.AddWordReplacement("{DE problem frequency}", detail.DE_ProblemFrequency == Frequency.Unknow ? "  " : detail.DE_ProblemFrequency.ToString());
            req.AddWordReplacement("{DE problem severity}", detail.DE_ProblemSeverity);
            req.AddWordReplacement("{DE problem duration}", detail.DE_ProblemDuration);
            req.AddWordReplacement("{DE bowel movements}", detail.DE_BowelMovements);
            req.AddWordReplacement("{DE blood in stool}", detail.DE_AnyBloodInStool);
            req.AddWordReplacement("{DE black stool yes no}", detail.DE_HadBlackTarryGrayStool.HasValue ? (detail.DE_HadBlackTarryGrayStool.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE yellow stool yes no}", detail.DE_HadYellowLightColoredStool.HasValue ? (detail.DE_HadYellowLightColoredStool.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE rectal itching yes no}", detail.DE_HadRectalItching.HasValue ? (detail.DE_HadRectalItching.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE stools formed or loose}", detail.DE_StoolsFormedOrLoose);
            req.AddWordReplacement("{DE constipation diarrhea yes no}", detail.DE_HadConstipationDiarrhea.HasValue ? (detail.DE_HadConstipationDiarrhea.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE constipation diarrhea details}", detail.DE_HadConstipationDiarrhea.HasValue && detail.DE_HadConstipationDiarrhea.Value ? detail.DE_ConstipationDiarrheaDetails : "  ");
            req.AddWordReplacement("{DE strain to pass stool yes no}", detail.DE_HaveToStrainToPassStool.HasValue ? (detail.DE_HaveToStrainToPassStool.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE strain to pass stool details}", detail.DE_HaveToStrainToPassStool.HasValue && detail.DE_HaveToStrainToPassStool.Value ? detail.DE_HaveToStrainDetails : "  ");
            req.AddWordReplacement("{DE pass gas frequently}", detail.DE_PassGasFrequently);
            req.AddWordReplacement("{DE burp frequently}", detail.DE_BurpFrequently);
            req.AddWordReplacement("{DE strong odor yes no}", detail.DE_StrongDisagreeableOdor.HasValue ? (detail.DE_StrongDisagreeableOdor.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE traveled outside yes no}", detail.DE_TraveledOutsideOfCanadaPast5Year.HasValue ? (detail.DE_TraveledOutsideOfCanadaPast5Year.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE traveled countries}", detail.DE_TraveledOutsideOfCanadaPast5Year.HasValue && detail.DE_TraveledOutsideOfCanadaPast5Year.Value ? detail.DE_TraveledCountries : "  ");
            req.AddWordReplacement("{DE been camping yes no}", detail.DE_BeenCammpingPast5Year.HasValue ? (detail.DE_BeenCammpingPast5Year.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE had fasted yes no}", detail.DE_HadFasted.HasValue ? (detail.DE_HadFasted.Value ? "Yes" : "No") : "  ");
            req.AddWordReplacement("{DE fast type}", detail.DE_HadFasted.HasValue && detail.DE_HadFasted.Value ? detail.DE_FastType : "  ");

            //Header
            req.AddWordReplacement("{patient name}", EditingInitialTreatment.Patient.DisplayName);
            req.AddWordReplacement("{treatment type}", EditingInitialTreatment.TreatmentType.DisplayName);

            //Date/duration
            req.AddWordReplacement("{date}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{datetime}", EditingInitialTreatment.TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            req.AddWordReplacement("{time_from}", EditingInitialTreatment.TreatmentTime.ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{time_to}", EditingInitialTreatment.TreatmentTime.AddMinutes(EditingInitialTreatment.TreatmentDurationMinutes).ToString(Constants.TIME_FORMAT));
            req.AddWordReplacement("{duration}", EditingInitialTreatment.TreatmentDurationMinutes.ToString());

            //Therapist
            req.AddWordReplacement("{therapist}", EditingInitialTreatment.Therapist.ToString());

            //Signature
            List<PictureReplacement> picSignature = new List<PictureReplacement>();
            string signatureImagePath = Program.CreateImageFile(EditingInitialTreatment.Therapist.SignatureImage);
            if(signatureImagePath.Length > 0) {
                picSignature.Add(new PictureReplacement(signatureImagePath));
                req.Replacements.Add("{signature}", picSignature);
            } else {
                req.AddWordReplacement("{signature}", "");
            }
        }

        private void btnOpenToWord_Click(object sender, EventArgs e) {

            if (EditingInitialTreatment == null || EditingInitialTreatment.Id == default(int)) {
                MessageBox.Show(this,
                    "Please save the new Initial Treatment before opening to Word.",
                    "Warning",
                    MessageBoxButtons.OK);
                return;
            }

            //Start the printing process
            _printProcessCompleted = false;
            printWorker.RunWorkerAsync();

            //Show waiting form
            if (_waitForm == null) {
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

        private void openOrPrintToWord(bool toPrintOnly) {
            if(EditingTreatmentDetail == null) return;

            WordExportHelper wordExporter = null;

            //Get treatment type
            string treatmentTypeName = EditingInitialTreatment.TreatmentType.TherapyTypeName;

            if( treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower()))
            {
                wordExporter = new WordExportHelper(
                    pnlTreatmentDetail.Controls[0] as UCChiropracticDetail,
                    Constants.CFG_TEMPLATE_FILE_CHIROPRACTIC,
                    new PopulateExportRequestDelegate(buildChiropracticRequest),
                    new ProgressReporterDelegate(wordProgressReporter));
            } else if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {
                wordExporter = new WordExportHelper(
                    pnlTreatmentDetail.Controls[0] as UCOsteopathyDetail,
                    Constants.CFG_TEMPLATE_FILE_OSTEOPATHY,
                    new PopulateExportRequestDelegate(buildOsteopathyRequest),
                    new ProgressReporterDelegate(wordProgressReporter));
            } else if (treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {
                wordExporter = new WordExportHelper(
                    pnlTreatmentDetail.Controls[0] as UCAcupunctureDetail,
                    Constants.CFG_TEMPLATE_FILE_ACUPUNCTURE,
                    new PopulateExportRequestDelegate(buildAcupunctureRequest),
                    new ProgressReporterDelegate(wordProgressReporter));
            }else if(treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.Trim().ToLower())){
                wordExporter = new WordExportHelper(
                    pnlTreatmentDetail.Controls[0] as UCPhysiotherapyDetail,
                    Constants.CFG_TEMPLATE_FILE_PHYSIOTHERAPY,
                    new PopulateExportRequestDelegate(buildPhysiotherapyRequest),
                    new ProgressReporterDelegate(wordProgressReporter));
            } else if(treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {
                wordExporter = new WordExportHelper(
                    pnlTreatmentDetail.Controls[0] as UCMassageDetail,
                    Constants.CFG_TEMPLATE_FILE_MASSAGE,
                    new PopulateExportRequestDelegate(buildMassageRequest),
                    new ProgressReporterDelegate(wordProgressReporter));
            } else if(treatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.Trim().ToLower())) {
                wordExporter = new WordExportHelper(
                    pnlTreatmentDetail.Controls[0] as UCNaturopathicDetail,
                    Constants.CFG_TEMPLATE_FILE_NATUROPATHIC,
                    new PopulateExportRequestDelegate(buildNaturopathicRequest),
                    new ProgressReporterDelegate(wordProgressReporter));
            } else {
                throw new ApplicationException("This treatment type " + treatmentTypeName + " has not been implemented yet in openOrPrintToWord() function.");
            }

            wordExporter.Export();
            if(wordExporter.HasError) {
                MessageBox.Show(wordExporter.ErrorMessage);
            } else {
                if(toPrintOnly) {
                    //wordExporter.PrintWordFile(wordExporter.WordExported);
                    MessageBox.Show(this, "Sorry. Not avaialbe for printing.");
                } else {
                    wordExporter.OpenWordFile(wordExporter.WordExported);
                }
            }
        }

        private void btnOpenToTherapist_Click(object sender, EventArgs e) {

            //Do nothing if it's view only mode
            if(_viewOnly) return;

            setOpenToTherapist(!getOpenToTherapist());

            if(EditingInitialTreatment != null 
                && EditingInitialTreatment.Id != default(int)
                && btnSave.Enabled
                && btnSave.Visible) {
                btnSave_Click(null, null);
            }
        }

        private void setOpenToTherapist(bool open) {
            if(open) {
                //btnOpenToTherapist.BackColor = Color.LightGreen;
                btnOpenToTherapist.ImageKey = "Open";
                btnOpenToTherapist.Text = "Open to therapist";
                btnOpenToTherapist.Tag = true;
                tooltip.SetToolTip(btnOpenToTherapist, "The assigned therapist is able to view/edit this form.");
            } else {
                //btnOpenToTherapist.BackColor = Color.MistyRose;
                btnOpenToTherapist.ImageKey = "Closed";
                btnOpenToTherapist.Text = "Closed to therapist";
                btnOpenToTherapist.Tag = false;
                tooltip.SetToolTip(btnOpenToTherapist, "Therapists is NOT able to view/edit this form.");
            }
        }

        private bool getOpenToTherapist() {
            bool open = false;

            if(btnOpenToTherapist.Tag != null) {
                open = (bool)btnOpenToTherapist.Tag;
            }

            return open;
        }

        private void EditInitialTreatmentForm_Load(object sender, EventArgs e) {
            if(_viewOnly) {
                //Set all children readonly
                FormHelper helper = new FormHelper(Constants.DATE_TIME_FORMAT);
                helper.SetControlsReadonly(this);

                //Hide buttons
                btnSave.Visible = false;
                btnComplete.Visible = false;
                btnCopyFromTemplate.Enabled = false;

                //Disable OpenToTherapist button
                //btnOpenToTherapist.Enabled = false;
            }

            if (Program.LogonUser.UserType == UserType.Therapist) {
                btnOpenToWord.Visible = false;
            }
        }

        private void printWorker_DoWork(object sender, DoWorkEventArgs e) {
            openOrPrintToWord(false);
        }

        private void printWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            _printProcessCompleted = true;
            if(_waitForm != null) {
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

        //Resize form when user control has been added
        private void pnlTreatmentDetail_ControlAdded(object sender, ControlEventArgs e) {

            int pnlHeight = pnlTreatmentDetail.Height;
            int pnlPreferredHeight = pnlTreatmentDetail.PreferredSize.Height + 10;
            int deltaHeight = pnlPreferredHeight - pnlHeight;
            int newFormHeight = Math.Min(this.Height + deltaHeight, _defaultFormHeight);
            this.Height = newFormHeight;
        }

        private void btnComplete_Click(object sender, EventArgs e) {
            save(true);
        }

        private void btnSaveAsTemplate_Click(object sender, EventArgs e) {
            if (pnlTreatmentDetail.Controls.Count > 0 && pnlTreatmentDetail.Visible && pnlTreatmentDetail.Visible) {

                //Get Treatment type
                TherapyType treatmentType = EditingInitialTreatment.TreatmentType;

                //Get detail id
                int detailId;

                ITreatmentDetailUserControl uc = pnlTreatmentDetail.Controls[0] as ITreatmentDetailUserControl;

                if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.Trim().ToLower())) {

                    detailId = (uc.TreatmentDetail as PhysiotherapyDetail).Id;

                }else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower())) {

                    detailId = (uc.TreatmentDetail as ChiropracticDetail).Id;

                } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {

                    detailId = (uc.TreatmentDetail as OsteopathyDetail).Id;

                } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {

                    detailId = (uc.TreatmentDetail as MassageDetail).Id;

                } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {

                    detailId = (uc.TreatmentDetail as AcupunctureDetail).Id;

                } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.Trim().ToLower())) {

                    detailId = (uc.TreatmentDetail as NaturopathicDetail).Id;

                } else {
                    throw new ApplicationException("Unknown treatment type.");
                }

                //Save
                using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    try {

                        //Try to retrieve the existing template
                        TemplateTreatmentDetail template = session.QueryOver<TemplateTreatmentDetail>()
                            .Where(x => x.IsInitial == true &&
                                x.TreatmentType.Id == treatmentType.Id &&
                                x.DetailId == detailId)
                            .SingleOrDefault<TemplateTreatmentDetail>();

                        string caption = template == null ?
                            "Please enter a Note for this template:" :
                            "It has been set as a template already. You can update the Note if you want.";
                        MessageInputForm noteForm = new MessageInputForm(caption);
                        noteForm.InputText = template == null ? string.Empty : template.Note;
                        if (DialogResult.OK == noteForm.ShowDialog(this)) {

                            if (template == null) {
                                template = new TemplateTreatmentDetail {
                                    IsInitial = true,
                                    TreatmentType = treatmentType,
                                    DetailId = detailId,
                                    Note = noteForm.InputText,
                                    IsTestData = Program.LogonUser.IsTestData,
                                    CreatedTime = DateTime.Now,
                                    CreatedBy = Program.LogonUser.DisplayName
                                };
                            } else {
                                template.Note = noteForm.InputText;
                                template.UpdatedBy = Program.LogonUser.DisplayName;
                                template.UpdatedTime = DateTime.Now;
                            }

                            session.Save(template);
                            tr.Commit();
                            MessageBox.Show(this, "Template has been saved.");
                        } else {
                            tr.Commit();
                        }

                    } catch (Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, "Failed to update weekly availability. " + ex.Message);
                        Program.log.Error("Failed to update weekly availability.", ex);
                    }
                }

            }else {
                MessageBox.Show(this, "Select a Treatment Type first!");
            }
        }

        private void btnCopyFromTemplate_Click(object sender, EventArgs e) {

            //Get Treatment type
            TherapyType treatmentType = EditingInitialTreatment.TreatmentType;

            if(treatmentType == null) {
                MessageBox.Show(this, "Please select a Treatment Type first.");
                return;
            }

            //1) Popup a list of templates for selection
            IList<TemplateTreatmentDetail> templates = null;

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    templates = session.QueryOver<TemplateTreatmentDetail>()
                        .Where(x => x.IsInitial == true &&
                            x.TreatmentType.Id == treatmentType.Id)
                        .List<TemplateTreatmentDetail>();

                    TemplateListForm frmTemplateList = new TemplateListForm();

                    IList<SimpleListItem> lstTemlate = new List<SimpleListItem>();
                    foreach (TemplateTreatmentDetail template in templates) {
                        lstTemlate.Add(new SimpleListItem { Id = template.Id, Text = template.Note });
                    }

                    frmTemplateList.Templates = lstTemlate;
                    if (DialogResult.OK == frmTemplateList.ShowDialog(this)) {

                        //Get selected template
                        TemplateTreatmentDetail selectedTemplate = templates.Where(x => x.Id == frmTemplateList.SelectedTemplateId).SingleOrDefault();
                        if (selectedTemplate != null) {

                            ITreatmentDetail templateDetail = null;

                            if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.Trim().ToLower())) {

                                templateDetail = session.Get<PhysiotherapyDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower())) {

                                templateDetail = session.Get<ChiropracticDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {

                                templateDetail = session.Get<OsteopathyDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {

                                templateDetail = session.Get<MassageDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {

                                templateDetail = session.Get<AcupunctureDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.Trim().ToLower())) {

                                templateDetail = session.Get<NaturopathicDetail>(selectedTemplate.DetailId);

                            } else {
                                throw new ApplicationException("Unknown treatment type.");
                            }
                            
                            ITreatmentDetailUserControl uc = pnlTreatmentDetail.Controls[0] as ITreatmentDetailUserControl;
                            if (uc != null) {
                                uc.LoadFromTemplate(templateDetail);
                            }

                        }
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to retrieve templates. " + ex.Message);
                    Program.log.Error("Failed to retrieve templates.", ex);
                }
            }

        }
    }
}
