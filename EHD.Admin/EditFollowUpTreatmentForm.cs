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
using EHD.FormUtitlity;
using HolidayCalculator;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.SqlCommand;

namespace EHD.Admin {
    public partial class EditFollowUpTreatmentForm : Form {

        private bool _viewOnly = false;

        public FollowUpTreatment EditingFollowUpTreatment { get; set; }

        private IFollowUpDetail EditingFollowUpDetail {
            get {
                if (pnlSOAP.Controls.Count <= 0) return null;

                IFollowUpDetailUserControl uc = pnlSOAP.Controls[0] as IFollowUpDetailUserControl;
                return uc.FollowUpDetail;
            }
        }

        /*
        public EditFollowUpTreatmentForm(FollowUpTreatment FollowUpTreatment, bool ViewOnly = false) {

            _viewOnly = ViewOnly;

            if (FollowUpTreatment == null || FollowUpTreatment.InitialTreatment == null)
                throw new ArgumentException("FollowUpTreatment and it's Parent cannot be null.");

            InitializeComponent();

            EditingFollowUpTreatment = FollowUpTreatment;

            //Populate dropdownlists
            populateDropDownLists();

            //Populate form
            loadFollowUpTreatment();

            //Some form controls are not for therapists
            if (Program.LogonUser.UserType == UserType.Therapist) {
                btnOpenToTherapist.Visible = false;
                drpTherapist.Enabled = false;
                dtTreatmentDate.Enabled = false;
                dtTreatmentTime.Enabled = false;
                numTreatmentDuration.Enabled = false;
            }

            //Hide "Save as Template" button when it's an empty treatment note
            if(EditingFollowUpTreatment.Id == default(int)) {
                btnSaveAsTemplate.Visible = false;
            }else {
                btnSaveAsTemplate.Visible = true;
            }
        }*/

        public EditFollowUpTreatmentForm(int InitialTreatmentId, int FollowupTreatmentId, bool ViewOnly = false, DateTime? treatmentTime = null) {
            _viewOnly = ViewOnly;

            if (InitialTreatmentId == 0 && FollowupTreatmentId == 0)
                throw new ArgumentException("InitialTreatmentId and FollowupTreatmentId cannot both be 0s.");

            InitializeComponent();

            //Populate dropdownlists
            populateDropDownLists();

            //Populate form
            loadFollowUpTreatment(InitialTreatmentId, FollowupTreatmentId, treatmentTime);

            //Some form controls are not for therapists
            if (Program.LogonUser.UserType == UserType.Therapist) {
                btnOpenToTherapist.Visible = false;
                drpTherapist.Enabled = false;
                dtTreatmentDate.Enabled = false;
                dtTreatmentTime.Enabled = false;
                numTreatmentDuration.Enabled = false;
            }

            //Hide "Save as Template" button when it's an empty treatment note
            if (FollowupTreatmentId == default(int)) {
                btnSaveAsTemplate.Visible = false;
            } else {
                btnSaveAsTemplate.Visible = true;
            }
        }


        private void populateDropDownLists() {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {

                    /*
                    IList<User> lstTherapist = session.QueryOver<User>().List<User>();
                    for (int i = lstTherapist.Count - 1; i >= 0; i--) {
                        User usr = lstTherapist[i];
                        if (usr.UserType != UserType.Therapist) {
                            lstTherapist.Remove(usr);
                        }
                    }
                    //Add an empty Therapist to the dropdown
                    lstTherapist.Insert(0, new User());
                    drpTherapist.DataSource = lstTherapist;
                    drpTherapist.ValueMember = "Id";
                    drpTherapist.DisplayMember = "DisplayName";
                    */

                    SimpleUserItem aliasUserItem = null;
                    User aUser = null;
                    UserTitle aTitle = null;

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

                    //Therapist
                    List<SimpleUserItem> lstTherapist = qryTherapist.ToList();
                    lstTherapist.Insert(0, new SimpleUserItem());
                    drpTherapist.DataSource = lstTherapist;
                    drpTherapist.ValueMember = "Id";
                    drpTherapist.DisplayMember = "DisplayName";

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load FollowUpTreatment. " + ex.Message);
                Program.log.Error("Failed to load FollowUpTreatment.", ex);
            }
        }

        private void loadFollowUpTreatment() {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {

                    if (EditingFollowUpTreatment.Id == default(int)) {
                        EditingFollowUpTreatment.CreatedBy = Program.LogonUser.DisplayName;
                        EditingFollowUpTreatment.CreatedTime = DateTime.Now;
                    } else {
                        EditingFollowUpTreatment = session.Get<FollowUpTreatment>(EditingFollowUpTreatment.Id);
                    }

                    populateFollowUpTreatmentDetail(EditingFollowUpTreatment);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load InitialTreatment. " + ex.Message);
                Program.log.Error("Failed to load InitialTreatment.", ex);
            }
        }

        private void loadFollowUpTreatment(int initialTreatmentId, int followupTreatmentId, DateTime? treatmentTime = null) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {

                    if (followupTreatmentId == 0) {
                        EditingFollowUpTreatment = new FollowUpTreatment();
                        EditingFollowUpTreatment.InitialTreatment = session.Get<InitialTreatment>(initialTreatmentId);
                        EditingFollowUpTreatment.CreatedBy = Program.LogonUser.DisplayName;
                        EditingFollowUpTreatment.CreatedTime = DateTime.Now;
                        if (treatmentTime.HasValue) {
                            EditingFollowUpTreatment.TreatmentTime = treatmentTime.Value;
                        }
                    } else {
                        EditingFollowUpTreatment = session.Get<FollowUpTreatment>(followupTreatmentId);
                    }

                    populateFollowUpTreatmentDetail(EditingFollowUpTreatment);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load InitialTreatment. " + ex.Message);
                Program.log.Error("Failed to load InitialTreatment.", ex);
            }
        }

        private void populateFollowUpTreatmentDetail(FollowUpTreatment ft) {
            txtPatientName.Text = ft.InitialTreatment.Patient.DisplayName;
            txtInitialTreatment.Text = ft.InitialTreatment.DisplayName;
            txtInitialTherapist.Text = ft.InitialTreatment.Therapist.DisplayName;
            setTherapist(ft.Therapist);
            dtTreatmentDate.Value = ft.TreatmentTime;
            dtTreatmentTime.Value = ft.TreatmentTime;
            numTreatmentDuration.Value = ft.TreatmentDurationMinutes;
            txtNote.Text = ft.Note;

            setOpenToTherapist(ft.OpenToTherapist);

            picComplete.Visible = ft.IsComplete;
            btnComplete.Visible = !ft.IsComplete;

            //Try to add FollowUp detail UserControl if there is any
            UserControl uc = UCTreatmentDetailFactory.CreateSOAPUserControl(EditingFollowUpTreatment, _viewOnly);
            if (uc != null) {
                pnlSOAP.Controls.Add(uc);
            } else {
                grpSOAP.Visible = false;
                this.Height -= grpSOAP.Height;
            }
        }

        private void updateFollowUpTreatmentInput(FollowUpTreatment ft) {
            ft.TreatmentTime = new DateTime(dtTreatmentDate.Value.Year,
                dtTreatmentDate.Value.Month,
                dtTreatmentDate.Value.Day,
                dtTreatmentTime.Value.Hour,
                dtTreatmentTime.Value.Minute,
                0);
            ft.TreatmentDurationMinutes = (int)numTreatmentDuration.Value;
            ft.Note = txtNote.Text.Trim();
            ft.OpenToTherapist = getOpenToTherapist();
        }

        private void setTherapist(User therapist) {
            if (therapist == null) return;

            drpTherapist.SelectedValue = therapist.Id;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            save(false);
        }

        private void save(bool setComplete) {
            if (EditingFollowUpTreatment != null) {
                ISessionFactory sessionFactory = null;
                try {
                    sessionFactory = SessionFactory.GetSessionFactory();
                    using (ISession session = sessionFactory.OpenSession())
                    using (ITransaction tr = session.BeginTransaction()) {

                        if (EditingFollowUpTreatment.Id == default(int)) {
                            EditingFollowUpTreatment.IsTestData = Program.LogonUser.IsTestData;
                            EditingFollowUpTreatment.CreatedBy = Program.LogonUser.DisplayName;
                            EditingFollowUpTreatment.CreatedTime = DateTime.Now;
                        } else {
                            EditingFollowUpTreatment = session.Get<FollowUpTreatment>(EditingFollowUpTreatment.Id);
                            EditingFollowUpTreatment.UpdatedTime = DateTime.Now;
                            EditingFollowUpTreatment.UpdatedBy = Program.LogonUser.DisplayName;
                        }

                        //update with dropdown selection
                        if (drpTherapist.SelectedValue != null)
                            EditingFollowUpTreatment.Therapist =
                                session.Get<User>(int.Parse(drpTherapist.SelectedValue.ToString()));

                        //update complete status
                        if (setComplete) EditingFollowUpTreatment.IsComplete = setComplete;

                        //Update with other inputs
                        updateFollowUpTreatmentInput(EditingFollowUpTreatment);

                        if (EditingFollowUpTreatment.HasMandatoryValues()) {

                            JCService service = new JCService(session, Program.LogonUser);

                            //Validate Insurer Treatment Per Day
                            /*
                            if(EditingFollowUpTreatment.InitialTreatment.Patient.Insurer != null) {

                                Insurer insurer = session.Merge(EditingFollowUpTreatment.InitialTreatment.Patient.Insurer);

                                //For some reason, the TherapyType won't be refreshed if it has been updated. Use Get<> to force to reload.
                                //TherapyType treatmentType = EditingFollowUpTreatment.InitialTreatment.TreatmentType;
                                TherapyType treatmentType = session.Get<TherapyType>(EditingFollowUpTreatment.InitialTreatment.TreatmentType.Id);

                                //1) ======== Check number of treatments for this therapist for the same insurer on the same day ============
                                //Get the number of treatments except this treatment
                                double numTreatment = service.GetNumberOfTreatmentFor(EditingFollowUpTreatment.Therapist.Id,
                                    insurer.Id,
                                    EditingFollowUpTreatment.TreatmentTime,
                                    0,
                                    EditingFollowUpTreatment.Id);
                                //Get the number of this treatment
                                double numThisTreatment = (treatmentType.EnableMinutesPerTreatment ?
                                    (double)EditingFollowUpTreatment.TreatmentDurationMinutes / treatmentType.MinutesPerTreatment
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
                                } else if (insurer.WarnTreatmentPerDay > 0 && numTotalTreatment > insurer.WarnTreatmentPerDay) {
                                    MessageBox.Show(this,
                                        string.Format("This is a warning...\n\nYou can still go ahead to add this assignment,\nbut for the same therapist and insurer [{0}],\nyou can only assign {1:0.##} more treatments for the same day.",
                                            insurer.DisplayName,
                                            insurer.MaxTreatmentPerDay - numTotalTreatment
                                        ),
                                        "Warning",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                }

                                //2) ============ For a specific treatment, check if this therapist has too many treatments/minutes for the same insurer on the same day
                                if (treatmentType.EnableMaxTreatmentsPerDayPerInsurer) {
                                    //Get the number of treatments except this treatment
                                    numTreatment = service.GetNumberOfTreatmentFor(treatmentType,
                                        EditingFollowUpTreatment.Therapist,
                                        insurer,
                                        EditingFollowUpTreatment.TreatmentTime,
                                        0,
                                        EditingFollowUpTreatment.Id);
                                    //Get the number of this treatment
                                    numThisTreatment = (treatmentType.EnableMinutesPerTreatment ?
                                        (double)EditingFollowUpTreatment.TreatmentDurationMinutes / treatmentType.MinutesPerTreatment
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
                            TherapyType treatmentType = session.Get<TherapyType>(EditingFollowUpTreatment.InitialTreatment.TreatmentType.Id);

                            if (treatmentType.EnableMaxTreatmentsPerDay) {
                            
                                //Get the number of treatments except this treatment
                                double numTreatment = service.GetNumberOfTreatmentFor(treatmentType,
                                    EditingFollowUpTreatment.Therapist,
                                    EditingFollowUpTreatment.TreatmentTime,
                                    0,
                                    EditingFollowUpTreatment.Id);
                                //Get the number of this treatment
                                double numThisTreatment = (treatmentType.EnableMinutesPerTreatment ?
                                    (double)EditingFollowUpTreatment.TreatmentDurationMinutes / treatmentType.MinutesPerTreatment
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


                            #region Check practitioner availability

                            List<JCService.AvailableStatus> pracStatus = service.CheckPractitionerAvailability(
                                EditingFollowUpTreatment.Therapist,
                                EditingFollowUpTreatment.TreatmentTime,
                                EditingFollowUpTreatment.TreatmentTime.AddMinutes(EditingFollowUpTreatment.TreatmentDurationMinutes),
                                JCService.AvailabilityCheckType.Treatment,
                                EditingFollowUpTreatment.Id,
                                !EditingFollowUpTreatment.InitialTreatment.TreatmentType.AllowTimeOverlap);

                            if (pracStatus.Contains(JCService.AvailableStatus.Holiday)) {
                                if (DialogResult.Yes != MessageBox.Show(this,
                                    string.Format(Constants.WARNING_HOLIDAY, EditingFollowUpTreatment.TreatmentTime.Date),
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

                            session.Save(EditingFollowUpTreatment);

                            //Save SOAP Detail
                            if (pnlSOAP.Controls.Count > 0 && pnlSOAP.Visible && grpSOAP.Visible) {
                                IFollowUpDetailUserControl uc = pnlSOAP.Controls[0] as IFollowUpDetailUserControl;
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
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to save FollowUPTreatment. " + ex.Message);
                    Program.log.Error("Failed to save FollowUPTreatment.", ex);
                }
            }
        }

        private void btnOpenToTherapist_Click(object sender, EventArgs e) {

            //Do nothing if it's view only mode
            if (_viewOnly) return;

            setOpenToTherapist(!getOpenToTherapist());

            if (EditingFollowUpTreatment != null
                && EditingFollowUpTreatment.Id != default(int)
                && btnSave.Enabled
                && btnSave.Visible) {
                btnSave_Click(null, null);
            }
        }

        private void setOpenToTherapist(bool open) {
            if (open) {
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

            if (btnOpenToTherapist.Tag != null) {
                open = (bool)btnOpenToTherapist.Tag;
            }

            return open;
        }

        private void EditFollowUpTreatmentForm_Load(object sender, EventArgs e) {
            if (_viewOnly) {
                //Set all children readonly
                FormHelper helper = new FormHelper(Constants.DATE_TIME_FORMAT);
                helper.SetControlsReadonly(this);

                //Hide Save button
                btnSave.Visible = false;
                btnComplete.Visible = false;
                btnCopyFromTemplate.Enabled = false;

                //Disable OpenToTherapist button
                //btnOpenToTherapist.Enabled = false;
            }
        }

        private void btnComplete_Click(object sender, EventArgs e) {
            save(true);
        }

        private void btnSaveAsTemplate_Click(object sender, EventArgs e) {

            //Get Treatment type
            TherapyType treatmentType = EditingFollowUpTreatment.InitialTreatment.TreatmentType;

            //Get detail id 
            //NOTE: for treatment types having no Details, such as Physio and Natrupathic, the detail id is the follow up treatment id.
            int detailId;

            if (pnlSOAP.Controls.Count > 0 && pnlSOAP.Visible && grpSOAP.Visible) {

                IFollowUpDetailUserControl uc = pnlSOAP.Controls[0] as IFollowUpDetailUserControl;

                if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower())) {

                    detailId = (uc.FollowUpDetail as ChiropracticFollowUpDetail).Id;

                } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {

                    detailId = (uc.FollowUpDetail as OsteopathyFollowUpDetail).Id;

                } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {

                    detailId = (uc.FollowUpDetail as MassageFollowUpDetail).Id;

                } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {

                    detailId = (uc.FollowUpDetail as AcupunctureFollowUpDetail).Id;

                } else {
                    throw new ApplicationException("Unknown treatment type.");
                }
            } else {//Use follow up treatment id as the detail id
                detailId = EditingFollowUpTreatment.Id;
            }

            //Save
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    //Try to retrieve the existing template
                    TemplateTreatmentDetail template = session.QueryOver<TemplateTreatmentDetail>()
                        .Where(x => x.IsInitial == false &&
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
                                IsInitial = false,
                                TreatmentType = treatmentType,
                                DetailId = detailId,
                                TreatmentNote = txtNote.Text,
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
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to set treatment template. " + ex.Message);
                    Program.log.Error("Failed to set treatment template.", ex);
                }
            }
        }

        private void btnCopyFromTemplate_Click(object sender, EventArgs e) {

            //Get Treatment type
            TherapyType treatmentType = EditingFollowUpTreatment.InitialTreatment.TreatmentType;

            //1) Popup a list of templates for selection
            IList<TemplateTreatmentDetail> templates = null;

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    templates = session.QueryOver<TemplateTreatmentDetail>()
                        .Where(x => x.IsInitial == false &&
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

                            IFollowUpDetail templateDetail = null;

                            if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower())) {

                                templateDetail = session.Get<ChiropracticFollowUpDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {

                                templateDetail = session.Get<OsteopathyFollowUpDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {

                                templateDetail = session.Get<MassageFollowUpDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {

                                templateDetail = session.Get<AcupunctureFollowUpDetail>(selectedTemplate.DetailId);

                            } else if (treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.Trim().ToLower()) ||
                                treatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.Trim().ToLower())) {
                            } else {
                                throw new ApplicationException("Unknown treatment type.");
                            }

                            if (pnlSOAP.Controls.Count > 0) {
                                IFollowUpDetailUserControl uc = pnlSOAP.Controls[0] as IFollowUpDetailUserControl;
                                if (uc != null && templateDetail != null) {
                                    uc.LoadFromTemplate(templateDetail);
                                }
                            }

                            if (selectedTemplate.TreatmentNote != null) {
                                txtNote.Text = selectedTemplate.TreatmentNote;
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
