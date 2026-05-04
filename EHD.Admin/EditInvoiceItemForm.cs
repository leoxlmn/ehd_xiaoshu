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
using HolidayCalculator;
//using Google.Apis.Calendar.v3.Data;

namespace EHD.Admin {
    public partial class EditInvoiceItemForm : Form {

        private Invoice _invoice = null;

        private bool _viewOnly = false;

        private Guid _sessionKey = Guid.NewGuid();

        public InvoiceItem EditingInvoiceItem { get; set; }

        public EditInvoiceItemForm(InvoiceItem item, bool ViewOnly = false) {

            if (item == null)
                throw new ArgumentException("Invoice Item cannot be null.");

            _viewOnly = ViewOnly;

            //Get session and register session key
            SessionFactory.GetOpenSession(_sessionKey);

            InitializeComponent();

            EditingInvoiceItem = item;
            _invoice = item.Invoice;

            loadInvoiceItem();
        }

        public EditInvoiceItemForm(object nullHolder, int invoiceId) {

            //Get session and register session key
            SessionFactory.GetOpenSession(_sessionKey);

            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);
            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    _invoice = session.Get<Invoice>(invoiceId);
                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to retrieve Invoice. " + ex.Message);
                Program.log.Error("Failed to retrieve Invoice.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            InitializeComponent();

            loadInvoiceItem();
        }

        public EditInvoiceItemForm(int invoiceItemId, bool ViewOnly = false) {

            _viewOnly = ViewOnly;

            //Get session and register session key
            SessionFactory.GetOpenSession(_sessionKey);

            InitializeComponent();

            //Load InvoiceItem
            InvoiceItem item = null;
            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);
            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    item = session.QueryOver<InvoiceItem>()
                        .Where(x => x.Id == invoiceItemId)
                        .Fetch(x => x.Invoice).Eager
                        .Fetch(x => x.Invoice.TreatmentType).Eager
                        .SingleOrDefault<InvoiceItem>();
                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to retrieve Invoice Item. " + ex.Message);
                Program.log.Error("Failed to retrieve Invoice Item.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            EditingInvoiceItem = item;
            _invoice = item.Invoice;
            loadInvoiceItem();

        }

        private void loadInvoiceItem() {
            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get session
            ISession session = SessionFactory.GetOpenSession(key);

            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    if (EditingInvoiceItem == null || EditingInvoiceItem.Id == default(int)) {
                        EditingInvoiceItem = new InvoiceItem();
                        EditingInvoiceItem.Invoice = _invoice;
                        EditingInvoiceItem.ServiceDescription = _invoice.TreatmentType.DisplayName + " Treatment";
                        EditingInvoiceItem.ServiceDuration = 60;
                        EditingInvoiceItem.Amount = _invoice.TreatmentType.DefaultPrice;
                        EditingInvoiceItem.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingInvoiceItem.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInvoiceItem.CreatedTime = DateTime.Now;
                    } else {
                        EditingInvoiceItem = session.Merge<InvoiceItem>(EditingInvoiceItem);
                    }

                    populateInvoiceItemDetail(EditingInvoiceItem);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Invoice Item. " + ex.Message);
                Program.log.Error("Failed to load Invoice Item.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }

        private void populateInvoiceItemDetail(InvoiceItem item) {
            if(item != null) {
                if(item.ServiceDate != default(DateTime)) {
                    dtServiceDate.Value = item.ServiceDate.Date;
                    dtServiceTime.Value = item.ServiceDate;
                }
                txtServiceDescription.Text = item.ServiceDescription;
                txtAmount.Text = string.Format("{0:N2}", item.Amount);
                numServiceDuration.Value = item.ServiceDuration.HasValue ? item.ServiceDuration.Value : 60;
            }
        }

        private void updateInvoiceItemDetailInput(InvoiceItem item) {
            item.ServiceDescription = txtServiceDescription.Text.Trim();
            item.ServiceDate = dtServiceDate.Value.Date.AddHours(dtServiceTime.Value.Hour).AddMinutes(dtServiceTime.Value.Minute);
            item.ServiceDuration = (int)numServiceDuration.Value;
            double amount = -1;
            if(double.TryParse(txtAmount.Text, out amount)) {
                item.Amount = amount;
            } else {
                throw new ApplicationException("Amount is not valid.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get session
            ISession session = SessionFactory.GetOpenSession(key);

            try {

                using (ITransaction tr = session.BeginTransaction()) {

                    if (EditingInvoiceItem == null || EditingInvoiceItem.Id == default(int)) {
                        EditingInvoiceItem = new InvoiceItem();
                        EditingInvoiceItem.Invoice = _invoice;
                        EditingInvoiceItem.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingInvoiceItem.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInvoiceItem.CreatedTime = DateTime.Now;
                    } else {
                        EditingInvoiceItem = session.QueryOver<InvoiceItem>()
                            .Where(x => x.Id == EditingInvoiceItem.Id)
                            .SingleOrDefault();
                    }

                    //Check if it's holiday
                    if (CanadaHolidays.IsHoliday(dtServiceDate.Value.Date)) {
                        if (DialogResult.Yes != MessageBox.Show(this,
                            string.Format(Constants.WARNING_HOLIDAY, dtServiceDate.Value.Date),
                            "Holiday Warning",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning)) {
                            dtServiceDate.Focus();
                            dtServiceDate.Select();

                            tr.Rollback();
                            return;
                        }
                    }

                    //Check if the practitioner is off
                    if (Program.config.EnablePractitionerCalendar) {

                        bool isAvailable = false;

                        IList<UserWeeklyAvailability> avails = session.QueryOver<UserWeeklyAvailability>()
                            .Where(x => x.Practitioner.Id == EditingInvoiceItem.Invoice.Therapist.Id)
                            .Where(x => x.WeekDay == dtServiceDate.Value.DayOfWeek)
                            .List<UserWeeklyAvailability>();

                        foreach (UserWeeklyAvailability avail in avails) {
                            if (avail.FullDayAvailable) {
                                isAvailable = true;
                                break;
                            } else {
                                decimal serviceStartHour = (decimal)dtServiceTime.Value.Hour + (decimal)dtServiceTime.Value.Minute / 60;
                                decimal serviceEndHour = (decimal)dtServiceTime.Value.AddMinutes((int)numServiceDuration.Value).Hour
                                    + (decimal)dtServiceTime.Value.AddMinutes((int)numServiceDuration.Value).Minute / 60;

                                if (avail.StartHour <= serviceStartHour && avail.EndHour >= serviceEndHour) {
                                    isAvailable = true;
                                    break;
                                }
                            }
                        }

                        if (isAvailable) {

                            //Continue to check time offs
                            DateTime dtCheckStart = dtServiceDate.Value.Date.AddHours(dtServiceTime.Value.Hour).AddMinutes(dtServiceTime.Value.Minute);
                            DateTime dtCheckEnd = dtCheckStart.AddMinutes((int)numServiceDuration.Value);
                            int timeOffCount = session.QueryOver<UserTimeOff>()
                                .Where(x => x.Practitioner.Id == EditingInvoiceItem.Invoice.Therapist.Id)
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
                                    dtServiceDate.Focus();
                                    dtServiceDate.Select();

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
                                dtServiceDate.Focus();
                                dtServiceDate.Select();

                                tr.Rollback();
                                return;
                            }
                        }
                    }

                    //Only to save when it's parent Invoice item has been created in database
                    if (EditingInvoiceItem.Invoice.Id != default(int)) {

                        JCService service = new JCService(session, Program.LogonUser);

                        //Validate Insurer Treatment Per Day
                        /*
                        Insurer insurer = EditingInvoiceItem.Invoice.Patient.Insurer;
                        
                        if (insurer != null) {

                            TherapyType serviceType = EditingInvoiceItem.Invoice.TreatmentType;

                            // 1) ============== Check number of invoice items for insurer =================
                            //Get the number of invoice items except this invoice item
                            double numInvoiceItem = service.GetNumberOfInvoiceItemFor(EditingInvoiceItem.Invoice.Therapist,
                                insurer,
                                dtServiceDate.Value.Date,
                                EditingInvoiceItem.Id);
                            //Get the number of this invoice item
                            double numThisInvoiceItem = (serviceType.EnableMinutesPerTreatment ?
                                (double)numServiceDuration.Value / serviceType.MinutesPerTreatment
                                : 1);
                            //Total number of invoice item will be
                            double numTotalInvoiceItem = numInvoiceItem + numThisInvoiceItem;

                            if (insurer.MaxTreatmentPerDay > 0 && numTotalInvoiceItem > insurer.MaxTreatmentPerDay) {
                                MessageBox.Show(this,
                                    string.Format("You are trying to assign {0:0.##} treatments for this therapist for insurer [{1}], which is more than the maximum number [{2}] allowed. \n\nYou cannot assign more on the same day.",
                                        numTotalInvoiceItem,
                                        insurer.DisplayName,
                                        insurer.MaxTreatmentPerDay
                                    ),
                                    "Max Number of Treatments Reached",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);

                                tr.Rollback();
                                return;
                            } else if (insurer.WarnTreatmentPerDay > 0 && numInvoiceItem >= insurer.WarnTreatmentPerDay) {
                                MessageBox.Show(this,
                                    string.Format("This is a warning...\n\nYou can still go ahead to add this assignment,\nbut for the same therapist and insurer [{0}],\nyou can only assign {1:0.##} more treatments for the same day.",
                                        insurer.DisplayName,
                                        insurer.MaxTreatmentPerDay - numTotalInvoiceItem
                                    ),
                                    "Warning",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }

                            // 2) ======= Check number of invoice items for treatment type, insurer ==========
                            if (serviceType.EnableMaxTreatmentsPerDayPerInsurer) {
                                //Get the number of invoice items except this invoice item
                                numInvoiceItem = service.GetNumberOfInvoiceItemFor(serviceType,
                                    EditingInvoiceItem.Invoice.Therapist,
                                    insurer,
                                    dtServiceDate.Value.Date,
                                    EditingInvoiceItem.Id);
                                //Get the number of this invoice item
                                numThisInvoiceItem = (serviceType.EnableMinutesPerTreatment ?
                                    (double)numServiceDuration.Value / serviceType.MinutesPerTreatment
                                    : 1);
                                //Total number of invoice item will be
                                numTotalInvoiceItem = numInvoiceItem + numThisInvoiceItem;

                                double numAvailable = serviceType.MaxTreatmentsPerDayPerInsurer - numTotalInvoiceItem;
                                if (numAvailable < 0) {
                                    MessageBox.Show(this,
                                        string.Format("You are trying to assign {0:0.##} {1} invoice items for this therapist for insurer [{2}], which is more than the maximum number [{3}].",
                                            numTotalInvoiceItem,
                                            serviceType.DisplayName,
                                            insurer.DisplayName,
                                            serviceType.MaxTreatmentsPerDayPerInsurer
                                        ),
                                        string.Format("Max Number of Invoice Items for {0} Reached", serviceType.DisplayName),
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                                    tr.Rollback();
                                    return;
                                } else if (numAvailable < 3) {//Warning when close to Max
                                    string msg = string.Format("This is a warning...\n\n{0} has a maximum limit [{1}] for per therapist per insurer per day.\n\n",
                                            serviceType.DisplayName,
                                            serviceType.MaxTreatmentsPerDayPerInsurer);
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

                            //3) Check if other invoice items have taken the time slot
                            var sameDayInsurerPracInvoices = session.CreateCriteria<InvoiceItem>("item")
                                .CreateCriteria("item.Invoice", "inv", NHibernate.SqlCommand.JoinType.InnerJoin)
                                .CreateCriteria("inv.Patient", "p", NHibernate.SqlCommand.JoinType.InnerJoin)
                                .Add(Restrictions.Not(Restrictions.Eq("item.Id", EditingInvoiceItem.Id)))
                                .Add(Restrictions.Eq("p.Insurer.Id", insurer.Id))
                                .Add(Restrictions.Disjunction()
                                    .Add(Restrictions.Eq("inv.Patient.Id", EditingInvoiceItem.Invoice.Patient.Id))
                                    .Add(Restrictions.Eq("inv.Therapist.Id", EditingInvoiceItem.Invoice.Therapist.Id)))
                                .Add(Restrictions.Le("item.ServiceDate", dtServiceDate.Value.Date.AddDays(1)))
                                .Add(Restrictions.Ge("item.ServiceDate", dtServiceDate.Value.Date));
                            foreach (InvoiceItem item in sameDayInsurerPracInvoices.List<InvoiceItem>()) {
                                if (item.ServiceDate >= dtServiceDate.Value.Date.AddHours(dtServiceTime.Value.Hour).AddMinutes(dtServiceTime.Value.Minute).AddMinutes((double)numServiceDuration.Value) ||
                                    item.ServiceDate.AddMinutes(item.ServiceDuration.HasValue ? item.ServiceDuration.Value : 0) <= dtServiceDate.Value.Date.AddHours(dtServiceTime.Value.Hour).AddMinutes(dtServiceTime.Value.Minute)) {
                                    //No Overlap, ignore
                                } else {
                                    MessageBox.Show(this,
                                        string.Format("The date & time you entered overlap the following invoice item:\n\nPatient:{0}\nPractitioner:{1}\nDate & time:{2}\nDuration:{3}",
                                            item.Invoice.Patient,
                                            item.Invoice.Therapist,
                                            item.ServiceDate,
                                            item.ServiceDuration),
                                        "Invoice Overlap",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                                    tr.Rollback();
                                    return;

                                }
                            }
                        }
                        */

                        //Validate number of treatments per user per day per treatment type
                        TherapyType serviceType = EditingInvoiceItem.Invoice.TreatmentType;

                        if (serviceType.EnableMaxTreatmentsPerDay) {

                            //Get the number of invoice items except this invoice item
                            double numInvoiceItem = service.GetNumberOfInvoiceItemFor(serviceType,
                                EditingInvoiceItem.Invoice.Therapist,
                                dtServiceDate.Value.Date,
                                EditingInvoiceItem.Id);
                            //Get the number of this invoice item
                            double numThisInvoiceItem = (serviceType.EnableMinutesPerTreatment ?
                                (double)numServiceDuration.Value / serviceType.MinutesPerTreatment
                                : 1);
                            //Total number of invoice item will be
                            double numTotalInvoiceItem = numInvoiceItem + numThisInvoiceItem;

                            double numAvailable = serviceType.MaxTreatmentsPerDay - numTotalInvoiceItem;
                            if (numAvailable < 0) {
                                MessageBox.Show(this,
                                    string.Format("You are trying to assign {0:0.##} {1} invoice items for this therapist, which is more than the maximum number [{2}].",
                                        numTotalInvoiceItem,
                                        serviceType.DisplayName,
                                        serviceType.MaxTreatmentsPerDay
                                    ),
                                    string.Format("Max Number of Invoice Items for {0} Reached", serviceType.DisplayName),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                                tr.Rollback();
                                return;
                            } else if (numAvailable < 3) {//Warning when close to Max
                                string msg = string.Format("This is a warning...\n\n{0} has a maximum limit [{1}] for per therapist per day.\n\n",
                                        serviceType.DisplayName,
                                        serviceType.MaxTreatmentsPerDay);
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

                        //3) Check if other invoice items have taken the time slot
                        if (!serviceType.AllowTimeOverlap) {
                            var sameDayPracInvoices = session.CreateCriteria<InvoiceItem>("item")
                                .CreateCriteria("item.Invoice", "inv", NHibernate.SqlCommand.JoinType.InnerJoin)
                                .Add(Restrictions.Not(Restrictions.Eq("item.Id", EditingInvoiceItem.Id)))
                                .Add(Restrictions.Disjunction()
                                    .Add(Restrictions.Eq("inv.Patient.Id", EditingInvoiceItem.Invoice.Patient.Id))
                                    .Add(Restrictions.Eq("inv.Therapist.Id", EditingInvoiceItem.Invoice.Therapist.Id)))
                                .Add(Restrictions.Le("item.ServiceDate", dtServiceDate.Value.Date.AddDays(1)))
                                .Add(Restrictions.Ge("item.ServiceDate", dtServiceDate.Value.Date))
                                .CreateCriteria("inv.TreatmentType", "type", NHibernate.SqlCommand.JoinType.InnerJoin)
                                .Add(Restrictions.Eq("type.AllowTimeOverlap", false));
                            foreach (InvoiceItem item in sameDayPracInvoices.List<InvoiceItem>()) {
                                if (item.ServiceDate >= dtServiceDate.Value.Date.AddHours(dtServiceTime.Value.Hour).AddMinutes(dtServiceTime.Value.Minute).AddMinutes((double)numServiceDuration.Value) ||
                                    item.ServiceDate.AddMinutes(item.ServiceDuration.HasValue ? item.ServiceDuration.Value : 0) <= dtServiceDate.Value.Date.AddHours(dtServiceTime.Value.Hour).AddMinutes(dtServiceTime.Value.Minute)) {
                                    //No Overlap, ignore
                                } else {
                                    MessageBox.Show(this,
                                        string.Format("The date & time you entered overlap the following invoice item:\n\nPatient: {0}\nPractitioner: {1}\nTreatment type: {2}\nDate & time: {3}\nDuration: {4}",
                                            item.Invoice.Patient,
                                            item.Invoice.Therapist,
                                            item.Invoice.TreatmentType,
                                            item.ServiceDate,
                                            item.ServiceDuration),
                                        "Invoice Overlap",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Stop);
                                    tr.Rollback();
                                    return;

                                }
                            }
                        }


                        //Update InvoiceItem entity
                        if (EditingInvoiceItem.Id != default(int)) {
                            EditingInvoiceItem.UpdatedTime = DateTime.Now;
                            EditingInvoiceItem.UpdatedBy = Program.LogonUser.DisplayName;
                        }
                        updateInvoiceItemDetailInput(EditingInvoiceItem);

                        if (!EditingInvoiceItem.HasMandatoryValues()) {
                            MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                                "Missing Values",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            tr.Rollback();
                            return;
                        }

                        //Save
                        session.Save(EditingInvoiceItem);
                        session.Flush();

                        //Update invoice statement date
                        int invoiceId = EditingInvoiceItem.Invoice.Id;
                        var qryInv = session.QueryOver<Invoice>()
                            .Where(x => x.Id == invoiceId)
                            //.Fetch(x => x.Patient).Eager
                            //.Fetch(x => x.Patient.Insurer).Eager
                            .Future<Invoice>();
                        //Force to load other collections within the same round trip in other queries
                        /*
                        session.CreateCriteria<InvoiceItem>()
                            .Add(Restrictions.Eq("Invoice.Id", invoiceId));
                        session.CreateCriteria<TherapyType>().Future<TherapyType>();
                        session.CreateCriteria<User>().SetFetchMode("Title", FetchMode.Eager).Future<User>();
                        session.CreateCriteria<TherapistOrganizationRegistrationGroup>().Future<TherapistOrganizationRegistrationGroup>();
                        */
                        //Run query
                        Invoice invToUpdate = qryInv.SingleOrDefault();

                        invToUpdate.UpdatedBy = Program.LogonUser.DisplayName;
                        invToUpdate.UpdatedTime = DateTime.Now;
                        service.UpdateInvoiceStatementDate(invToUpdate);

                        //Update Invoice # - using statement date to populate the invoice #
                        string fileNo = invToUpdate.Patient.FileNumber;
                        string treatmentType = invToUpdate.TreatmentType.DisplayName;
                        DateTime dtStatement = invToUpdate.StatementDate;
                        string newInvNum = service.GetNextInvoiceNumber(fileNo, treatmentType, dtStatement, invoiceId);
                        invToUpdate.UpdatedBy = Program.LogonUser.DisplayName;
                        invToUpdate.UpdatedTime = DateTime.Now;
                        invToUpdate.InvoiceNumber = newInvNum;

                        session.Save(invToUpdate);
                        session.Flush();

                    }

                    tr.Commit();

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to save Invoice Item. " + ex.Message);
                Program.log.Error("Failed to save Invoice Item.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }

        private void EditInvoiceItemForm_Load(object sender, EventArgs e) {
            //Get session and register session key
            //SessionFactory.GetOpenSession(_sessionKey);
            if (_viewOnly) {
                new FormUtitlity.FormHelper().SetControlsReadonly(this, true);
            }
        }

        private void EditInvoiceItemForm_FormClosed(object sender, FormClosedEventArgs e) {
            //Try to close session
            SessionFactory.TryCloseSession(_sessionKey);
        }

        private void btnPrevDate_Click(object sender, EventArgs e) {
            searchNextAvailableTime(false);
        }

        private void btnNextDate_Click(object sender, EventArgs e) {
            searchNextAvailableTime(true);
        }

        private void searchNextAvailableTime(bool forward = true) {

            int dayOffset = forward ? 1 : -1;

            //Get current datetime
            DateTime dtCurrent = dtServiceDate.Value.Date.AddHours(dtServiceTime.Value.Hour).AddMinutes(dtServiceTime.Value.Minute);

            //Get patient id
            int patientId = _invoice.Patient.Id;

            //Get practitioner
            User practitioner = _invoice.Therapist;

            //Get therapy type
            TherapyType therapyType = _invoice.TreatmentType;

            //Get insurer
            Insurer insurer = _invoice.Patient.Insurer;

            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get session
            ISession session = SessionFactory.GetOpenSession(key);

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    //Try to find an available time slot based on patientId, practitionerId, therapyTypeId and duration
                    DateTime dtNext = dtCurrent;

                    if (Program.config.EnablePractitionerCalendar) {
                        //1) Get practitioner availabilities
                        var avails = session.QueryOver<UserWeeklyAvailability>()
                            .Where(x => x.Practitioner.Id == practitioner.Id)
                            .Future<UserWeeklyAvailability>();
                        //2) Get practitioner timeoffs
                        var timeOffs = session.QueryOver<UserTimeOff>()
                            .Where(x => x.Practitioner.Id == practitioner.Id)
                            .And(x => x.StartTime > dtCurrent.AddYears(-1) && x.StartTime < dtCurrent.AddYears(1)) //Two years timeoff data should be enough
                            .List<UserTimeOff>();
                        //3 Get the next practitioner's available time
                        if (avails.Count() <= 0) {
                            MessageBox.Show(this, "The Practitioner Calendar has been enabled, but this practitioner's weekly availability has not been setup properly. Please go to the Practitioner Calendar to set which week days this practitioner will be working.");
                            tr.Commit();
                            return;
                        } else {
                            dtNext = getNextAvailableTime(session, therapyType, practitioner, dtCurrent, (int)numServiceDuration.Value, avails.ToList<UserWeeklyAvailability>(), timeOffs, forward, EditingInvoiceItem.Id);
                        }
                    } else {
                        dtNext = getNextAvailableTime(session, therapyType, practitioner, dtCurrent, (int)numServiceDuration.Value, null, null, forward, EditingInvoiceItem.Id);
                    }

                    if (dtNext == dtCurrent) {
                        MessageBox.Show(this, "The Date and Time you picked is fine.");
                    } else {
                        dtServiceDate.Value = dtNext;
                        dtServiceTime.Value = dtNext;
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to search an available date. " + ex.Message);
                Program.log.Error("Failed to search an available date.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

        }

        /// <summary>
        /// If the given dtStart and duration is available for the given practitioner, return the same value.
        /// Otherwise, return the next available time by searching forward or backward
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="durationMinutes"></param>
        /// <param name="avails"></param>
        /// <param name="timeoffs"></param>
        /// <param name="goForward"></param>
        /// <returns></returns>
        private DateTime getNextAvailableTime(ISession session, TherapyType therapyType, User practitioner,
            DateTime dtStart, int durationMinutes,
            IList<UserWeeklyAvailability> avails, IList<UserTimeOff> timeoffs,
            bool goForward = true, int curInvoiceItemId = default(int)) {

            //Validate parameters
            if (durationMinutes <= 0) throw new ArgumentException("The duration must be a positive integer!");
            if (durationMinutes > (Program.config.WorkingHourEnd - Program.config.WorkingHourStart) * 60) {
                throw new ArgumentException("The duration is longer than your business hours!");
            }

            //Calcuate start and end time to be checked
            DateTime dt = dtStart;
            DateTime dtTo = dt.AddMinutes(durationMinutes);

            //Adjust date/time when the given time slot is over business hours
            if (dt < dt.Date.AddHours(Program.config.WorkingHourStart)) {
                if (goForward) {
                    dt = dt.Date.AddHours(Program.config.WorkingHourStart);
                    dtTo = dt.AddMinutes(durationMinutes);
                } else {
                    dtTo = dt.Date.AddDays(-1).AddHours(Program.config.WorkingHourEnd);
                    dt = dtTo.AddMinutes(-durationMinutes);
                }
            } else if (dt >= dt.Date.AddHours(Program.config.WorkingHourEnd)) {
                if (goForward) {
                    dt = dt.Date.AddDays(1).AddHours(Program.config.WorkingHourStart);
                    dtTo = dt.AddMinutes(durationMinutes);
                } else {
                    dtTo = dt.Date.AddHours(Program.config.WorkingHourEnd);
                    dt = dtTo.AddMinutes(-durationMinutes);
                }
            } else if (dtTo > dt.Date.AddHours(Program.config.WorkingHourEnd)) {
                if (goForward) {
                    dt = dtStart.Date.AddHours(Program.config.WorkingHourStart);
                    dtTo = dt.AddMinutes(durationMinutes);
                } else {
                    dtTo = dtStart.Date.AddHours(Program.config.WorkingHourEnd);
                    dt = dtTo.AddMinutes(-durationMinutes);
                }
            }

            //1) cannot be holiday
            #region Check Holiday
            if (CanadaHolidays.IsHoliday(dt)) {
                if (goForward) {
                    dt = dt.Date.AddDays(1).AddHours(Program.config.WorkingHourStart);
                } else {
                    dt = dt.Date.AddDays(-1).AddHours(Program.config.WorkingHourEnd).AddMinutes(-durationMinutes);
                }
                return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
            }
            #endregion Check Holiday

            //2) if avails != null, practitioner calendar is enabled, check weekly availabilities and time offs
            #region Check weekly availabilities and time offs
            if (avails != null) {

                //2.a) weekday for today has to be fully or partially available
                bool timeAdjusted = false;
                IList<UserWeeklyAvailability> todayAvails = avails.Where(x => x.WeekDay == dt.DayOfWeek).ToList<UserWeeklyAvailability>();
                if (todayAvails.Count <= 0) {
                    if (goForward) {
                        dt = dt.Date.AddDays(1).AddHours(Program.config.WorkingHourStart);
                    } else {
                        dt = dt.Date.AddDays(-1).AddHours(Program.config.WorkingHourEnd).AddMinutes(-durationMinutes);
                    }
                    return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                } else if (todayAvails[0].FullDayAvailable) {//Full day available today for this practitioner
                    //Do nothing
                } else {//Partially available
                    if (goForward) {
                        UserWeeklyAvailability possibleAvail = todayAvails.Where(x => x.EndHour >= (dtTo.Hour + dtTo.Minute / 60M)).OrderBy(x => x.StartHour).FirstOrDefault();
                        if (possibleAvail == null) {
                            dt = dt.Date.AddDays(1).AddHours(Program.config.WorkingHourStart);
                            return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                        } else if (possibleAvail.StartHour > (dt.Hour + dt.Minute / 60M)) {
                            dt = dt.Date.AddHours(Convert.ToDouble(possibleAvail.StartHour));
                            return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                        } else {
                            //Found an available time slot. Do Nothing
                        }
                    } else {
                        UserWeeklyAvailability possibleAvail = todayAvails.Where(x => x.StartHour <= (dt.Hour + dt.Minute / 60M)).OrderByDescending(x => x.EndHour).FirstOrDefault();
                        if (possibleAvail == null) {
                            dt = dt.Date.AddDays(-1).AddHours(Program.config.WorkingHourEnd).AddMinutes(-durationMinutes);
                            return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                        } else if (possibleAvail.EndHour < (dtTo.Hour + dtTo.Minute / 60M)) {
                            dt = dt.Date.AddHours(Convert.ToDouble(possibleAvail.EndHour)).AddMinutes(-durationMinutes);
                            return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                        } else {
                            //Found an available time slot. Do Nothing
                        }
                    }
                }

                //2.b) continue to check timeoffs
                if (timeoffs != null) {
                    timeAdjusted = false;
                    if (goForward) {
                        foreach (UserTimeOff timeOff in timeoffs.Where(x => x.StartTime.Date == dt.Date).OrderBy(x => x.StartTime)) {
                            if (!(timeOff.EndTime <= dt || timeOff.StartTime >= dtTo)) {
                                dt = timeOff.EndTime;
                                dtTo = dt.AddMinutes(durationMinutes);
                                timeAdjusted = true;
                            }
                        }
                    } else {
                        foreach (UserTimeOff timeOff in timeoffs.Where(x => x.StartTime.Date == dt.Date).OrderByDescending(x => x.EndTime)) {
                            if (!(timeOff.EndTime <= dt || timeOff.StartTime >= dtTo)) {
                                dtTo = timeOff.StartTime;
                                dt = dtTo.AddMinutes(-durationMinutes);
                                timeAdjusted = true;
                            }
                        }
                    }
                    if (timeAdjusted) {
                        return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                    }
                }
            }
            #endregion Check weekly availabilities and time offs

            //3) Check if the time slot has been taken by other invoice items
            #region Check if the time slot has been taken by other invoice items
            var todayParientInvoices = session.QueryOver<InvoiceItem>()
                .Where(x => x.ServiceDate >= dt.Date)
                .And(x => x.ServiceDate < dt.Date.AddDays(1))
                .And(x => x.ServiceDuration != null)
                .And(x => x.Id != curInvoiceItemId)
                .Inner.JoinQueryOver<Invoice>(x => x.Invoice)
                .Where(x => x.Patient.Id == _invoice.Patient.Id);
            var todayPractitionerInvoices = session.QueryOver<InvoiceItem>()
                .And(x => x.ServiceDate >= dt.Date)
                .And(x => x.ServiceDate < dt.Date.AddDays(1))
                .And(x => x.ServiceDuration != null)
                .And(x => x.Id != curInvoiceItemId)
                .Inner.JoinQueryOver<Invoice>(x => x.Invoice)
                .Where(x => x.Therapist.Id == _invoice.Therapist.Id)
                .Inner.JoinQueryOver<TherapyType>(x => x.TreatmentType)
                .Where(x => x.AllowTimeOverlap == false);

            List<InvoiceItem> todayInvoices = new List<InvoiceItem>(todayParientInvoices.List<InvoiceItem>());
            todayInvoices.AddRange(todayPractitionerInvoices.List<InvoiceItem>());

            foreach (InvoiceItem inv in todayInvoices) {
                if (inv.ServiceDate >= dtTo || inv.ServiceDate.AddMinutes(inv.ServiceDuration.Value) <= dt) {
                    //No overlap, ignore
                } else {
                    if (goForward) {
                        dt = inv.ServiceDate.AddMinutes(inv.ServiceDuration.Value);
                        return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                    } else {
                        dt = inv.ServiceDate.AddMinutes(-durationMinutes);
                        return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                    }
                }
            }
            #endregion Check if the time slot has been taken by other invoice items WITH THE SAME INSURER

            //4)Check on insurer treatment-per-day restrictions
            #region Check insurer treatment-per-day restrictions
            /*
            if (insurer != null) {

                JCService service = new JCService(session, Program.LogonUser);

                if (insurer.MaxTreatmentPerDay > 0) {//Max treatment-per-day has been defined for this insurer
                    //Get the number of invoice items except this invoice item
                    double numInvoiceItem = service.GetNumberOfInvoiceItemFor(practitioner,
                        insurer,
                        dt.Date,
                        curInvoiceItemId);
                    //Get the number of this invoice item
                    double numThisInvoiceItem = (therapyType.EnableMinutesPerTreatment ?
                        (double)durationMinutes / therapyType.MinutesPerTreatment
                        : 1);
                    //Total number of invoice item will be
                    double numTotalInvoiceItem = numInvoiceItem + numThisInvoiceItem;
                    if (numTotalInvoiceItem > insurer.MaxTreatmentPerDay) {
                        if (goForward) {
                            dt = dt.Date.AddDays(1).AddHours(Program.config.WorkingHourStart);
                        } else {
                            dt = dt.Date.AddDays(-1).AddHours(Program.config.WorkingHourEnd).AddMinutes(-durationMinutes);
                        }
                        return getNextAvailableTime(session, insurer, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                    }
                }

                if (therapyType.EnableMaxTreatmentsPerDayPerInsurer) {//This therapy type has Max treatment-per-day-per-insurer defined
                    //Get the number of invoice items except this invoice item
                    double numInvoiceItem = service.GetNumberOfInvoiceItemFor(therapyType,
                        practitioner,
                        insurer,
                        dt.Date,
                        curInvoiceItemId);
                    //Get the number of this invoice item
                    double numThisInvoiceItem = (therapyType.EnableMinutesPerTreatment ?
                        (double)durationMinutes / therapyType.MinutesPerTreatment
                        : 1);
                    //Total number of invoice item will be
                    double numTotalInvoiceItem = numInvoiceItem + numThisInvoiceItem;

                    double numAvailable = therapyType.MaxTreatmentsPerDayPerInsurer - numTotalInvoiceItem;
                    if (numAvailable < 0) {
                        if (goForward) {
                            dt = dt.Date.AddDays(1).AddHours(Program.config.WorkingHourStart);
                        } else {
                            dt = dt.Date.AddDays(-1).AddHours(Program.config.WorkingHourEnd).AddMinutes(-durationMinutes);
                        }
                        return getNextAvailableTime(session, insurer, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                    }
                }

            }
            */
            #endregion Check insurer treatment-per-day restrictions

            #region Check therapy teatment-per-day restriction

            JCService service = new JCService(session, Program.LogonUser);

            if (therapyType.EnableMaxTreatmentsPerDay) {
                //Get the number of invoice items except this invoice item
                double numInvoiceItem = service.GetNumberOfInvoiceItemFor(therapyType,
                    practitioner,
                    dt.Date,
                    curInvoiceItemId);
                //Get the number of this invoice item
                double numThisInvoiceItem = (therapyType.EnableMinutesPerTreatment ?
                    (double)durationMinutes / therapyType.MinutesPerTreatment
                    : 1);
                //Total number of invoice item will be
                double numTotalInvoiceItem = numInvoiceItem + numThisInvoiceItem;

                double numAvailable = therapyType.MaxTreatmentsPerDay - numTotalInvoiceItem;
                if (numAvailable < 0) {
                    if (goForward) {
                        dt = dt.Date.AddDays(1).AddHours(Program.config.WorkingHourStart);
                    } else {
                        dt = dt.Date.AddDays(-1).AddHours(Program.config.WorkingHourEnd).AddMinutes(-durationMinutes);
                    }
                    return getNextAvailableTime(session, therapyType, practitioner, dt, durationMinutes, avails, timeoffs, goForward, curInvoiceItemId);
                }
            }
            #endregion Check therapy teatment-per-day restriction

            //Return after passing all the validatations
            return dt;
        }
    }
}
