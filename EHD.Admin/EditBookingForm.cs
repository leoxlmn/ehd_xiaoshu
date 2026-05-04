using EHD.Constant;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class EditBookingForm : Form {

        #region Private members
        private int _bookingId = default(int);
        private Booking editingBooking = null;
        private ISession session = null;
        private bool isSearching = false;

        private int _defaultPractitionerId = default(int);
        private DateTime? _defaultDateTime = null;
        private int MIN_TIME_INTERVAL = (Properties.Settings.Default.BookingMinimumMinutes == null 
            || Properties.Settings.Default.BookingMinimumMinutes.Length <= 0 ? 30 : 
            Convert.ToInt32(Properties.Settings.Default.BookingMinimumMinutes));
        #endregion Private members

        #region Constructors

        public EditBookingForm(int BookingId) {
            InitializeComponent();

            _bookingId = BookingId;
        }

        public EditBookingForm() : this(default(int)) { }

        public EditBookingForm(int PractitionerId, DateTime BookingDateTime) : this(default(int)) {
            _defaultPractitionerId = PractitionerId;
            _defaultDateTime = BookingDateTime;
        }

        #endregion Constructors

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void EditBookingForm_Load(object sender, EventArgs e) {

            //Open Session
            if(session == null) session = SessionFactory.GetSessionFactory().OpenSession();

            //Populate dropdowns
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    //Practitioners
                    var practitioners = session.QueryOver<User>()
                        .Where(x => x.UserType == UserType.Therapist)
                        .Fetch(x => x.Title).Eager
                        .OrderBy(x => x.FirstName).Asc
                        .ThenBy(x => x.LastName).Asc
                        .Take(Program.config.MaxTherapists)
                        .List<User>();

                    drpUser.DataSource = practitioners;
                    drpUser.ValueMember = "Id";
                    drpUser.DisplayMember = "DisplayName";

                    //Therapy Type
                    var therapyTypes = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .List();

                    drpTreatmentType.DataSource = therapyTypes;
                    drpTreatmentType.ValueMember = "Id";
                    drpTreatmentType.DisplayMember = "DisplayName";

                    //Loading current booking info
                    if (_bookingId != default(int)) {

                        this.Text = "Update Booking";

                        editingBooking = session.Get<Booking>(_bookingId);
                        if (editingBooking != null) {
                            ucPatient.SelectedItem = new SimpleListItem {
                                Id = editingBooking.Patient.Id,
                                Text = string.Format("{0} - {1} {2}", editingBooking.Patient.FileNumber, editingBooking.Patient.FirstName, editingBooking.Patient.LastName)
                            };
                            drpUser.SelectedItem = editingBooking.Therapist;
                            drpTreatmentType.SelectedItem = editingBooking.TreatmentType;
                            dtDate.Value = editingBooking.StartTime.Date;
                            dtStartTime.Value = editingBooking.StartTime;
                            dtEndTime.Value = editingBooking.EndTime;
                            txtNote.Text = editingBooking.Note;
                        }
                    } else {//Set default values

                        this.Text = "Add New Booking";

                        if (_defaultPractitionerId != default(int)) {
                            drpUser.SelectedValue = _defaultPractitionerId;
                        }

                        if (_defaultDateTime.HasValue) {
                            dtDate.Value = _defaultDateTime.Value.Date;
                            dtStartTime.Value = _defaultDateTime.Value;
                        } else {
                            DateTime dtCurrent = DateTime.Now;
                            dtDate.Value = dtCurrent;
                            dtStartTime.Value = new DateTime(dtCurrent.Year, dtCurrent.Month, dtCurrent.Day,
                                dtCurrent.Hour, dtCurrent.Minute / MIN_TIME_INTERVAL * MIN_TIME_INTERVAL, 0);
                        }
                        dtEndTime.Value = dtStartTime.Value.AddMinutes(MIN_TIME_INTERVAL);
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load data from database. " + ex.Message);
                    Program.log.Error("Failed to load data from database.", ex);
                }

            }

        }

        private void EditBookingForm_FormClosing(object sender, FormClosingEventArgs e) {
            //Close session
            if (session != null) session.Dispose();
        }

        private void ucPatient_MinInputLengthReached(object sender, EventArgs e) {

            if (isSearching) return;

            isSearching = true;

            //Search patients
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    //Search top 20 matches
                    var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Insurer).Eager
                        .Where(Restrictions.Disjunction()
                            .Add(Restrictions.Eq(Projections.Property<Patient>(x => x.FileNumber), ucPatient.InputText))
                            .Add(Restrictions.On<Patient>(x => x.FirstName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.LastName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            )
                        .OrderBy(x => x.FirstName).Asc
                        .Take(20)
                        .List<Patient>();

                    IList<SimpleListItem> lstPatient = new List<SimpleListItem>();
                    foreach (Patient patient in patients) {
                        lstPatient.Add(new SimpleListItem {
                            Id = patient.Id,
                            Text = string.Format("{0} - {1} {2}", patient.FileNumber, patient.FirstName, patient.LastName)
                        });
                    }

                    ucPatient.UpdateList(lstPatient);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to search patient. " + ex.Message);
                    Program.log.Error("Failed to search patient.", ex);
                } finally {
                    isSearching = false;
                }
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
            Patient Patient = session.Get<Patient>(ucPatient.SelectedItem.Id);

            //Check if Practitioner has been selected
            if (drpUser.SelectedItem == null) {
                MessageBox.Show(this, "Please select a Practitioner.");
                drpUser.Focus();
                drpUser.Select();
                return;
            }
            User Practitioner = drpUser.SelectedItem as User;

            //Check if Treatment Type has been selected
            if (drpTreatmentType.SelectedItem == null) {
                MessageBox.Show(this, "Please select a Treatment Type.");
                drpTreatmentType.Focus();
                drpTreatmentType.Select();
                return;
            }
            TherapyType TreatmentType = drpTreatmentType.SelectedItem as TherapyType;

            //Round up minutes to the MIN_TIME_INTERVAL (5, 10, 15, 20, 30, 60)
            dtStartTime.Value = new DateTime(dtStartTime.Value.Year, dtStartTime.Value.Month, dtStartTime.Value.Day,
                dtStartTime.Value.Hour, dtStartTime.Value.Minute / MIN_TIME_INTERVAL * MIN_TIME_INTERVAL, 0);
            dtEndTime.Value = new DateTime(dtEndTime.Value.Year, dtEndTime.Value.Month, dtEndTime.Value.Day,
                dtEndTime.Value.Hour, dtEndTime.Value.Minute / MIN_TIME_INTERVAL * MIN_TIME_INTERVAL, 0);

            //Validate start/end time
            if (dtStartTime.Value.Hour * 60 + dtStartTime.Value.Minute >= dtEndTime.Value.Hour * 60 + dtEndTime.Value.Minute) {
                MessageBox.Show(this, "Start Time must be earlier than End Time.");
                dtStartTime.Focus();
                dtStartTime.Select();
                return;
            }
            DateTime StartTime = dtDate.Value.Date.AddHours(dtStartTime.Value.Hour).AddMinutes(dtStartTime.Value.Minute);
            DateTime EndTime = dtDate.Value.Date.AddHours(dtEndTime.Value.Hour).AddMinutes(dtEndTime.Value.Minute);

            //Validate practitioner availability
            #region Check practitioner availability

            JCService service = new JCService(session, Program.LogonUser);

            IList<JCService.AvailableStatus> pracStatus =
                service.CheckPractitionerAvailability(
                    drpUser.SelectedItem as User,
                    StartTime,
                    EndTime,
                    JCService.AvailabilityCheckType.Booking,
                    editingBooking == null ? default(int) : editingBooking.Id);

            //Check not available error first
            if (pracStatus.Contains(JCService.AvailableStatus.NotAvailable)) {
                MessageBox.Show(this,
                    Constants.ERROR_NOT_AVAILABLE_BOOKING,
                    "Not Available",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                dtStartTime.Focus();
                dtStartTime.Select();
                return;
            }

            //Check other warnings
            if (Program.config.EnablePractitionerCalendar) {
                if (pracStatus.Contains(JCService.AvailableStatus.NoneWorkingHour)) {
                    if (DialogResult.OK != MessageBox.Show(this,
                        Constants.WARNING_NOT_WORKING_TIME,
                        "Working Hour Warning",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning)) {
                        dtStartTime.Focus();
                        dtStartTime.Select();

                        return;
                    }
                }

                if (pracStatus.Contains(JCService.AvailableStatus.TimeOff)) {
                    if (DialogResult.OK != MessageBox.Show(this,
                        Constants.WARNING_TIME_OFF,
                        "Time Off Warning",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning)) {
                        dtStartTime.Focus();
                        dtStartTime.Select();

                        return;
                    }
                }
            }

            if (pracStatus.Contains(JCService.AvailableStatus.Holiday)) {
                if (DialogResult.Yes != MessageBox.Show(this,
                    string.Format(Constants.WARNING_HOLIDAY, StartTime.Date),
                    "Holiday Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)) {
                    dtStartTime.Focus();
                    dtStartTime.Select();

                    return;
                }
            }

            #endregion Check practitioner availability


            //Create/update booking
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    if (editingBooking == null) {
                        editingBooking = new Booking {
                            Patient = session.Get<Patient>(ucPatient.SelectedItem.Id),
                            Therapist = drpUser.SelectedItem as User,
                            TreatmentType = drpTreatmentType.SelectedItem as TherapyType,
                            StartTime = dtDate.Value.Date.AddHours(dtStartTime.Value.Hour).AddMinutes(dtStartTime.Value.Minute),
                            EndTime = dtDate.Value.Date.AddHours(dtEndTime.Value.Hour).AddMinutes(dtEndTime.Value.Minute),
                            Note = txtNote.Text,
                            IsTestData = Program.LogonUser.IsTestData,
                            CreatedBy = Program.LogonUser.Username,
                            CreatedTime = DateTime.Now
                        };
                    } else {
                        editingBooking.Patient = session.Get<Patient>(ucPatient.SelectedItem.Id);
                        editingBooking.Therapist = drpUser.SelectedItem as User;
                        editingBooking.TreatmentType = drpTreatmentType.SelectedItem as TherapyType;
                        editingBooking.StartTime = dtDate.Value.Date.AddHours(dtStartTime.Value.Hour).AddMinutes(dtStartTime.Value.Minute);
                        editingBooking.EndTime = dtDate.Value.Date.AddHours(dtEndTime.Value.Hour).AddMinutes(dtEndTime.Value.Minute);
                        editingBooking.Note = txtNote.Text;
                        editingBooking.UpdatedBy = Program.LogonUser.Username;
                        editingBooking.UpdatedTime = DateTime.Now;
                    }

                    session.Save(editingBooking);

                    tr.Commit();

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to create Booking. " + ex.Message);
                    Program.log.Error("Failed to create Booking.", ex);
                }
            }

        }
    }
}
