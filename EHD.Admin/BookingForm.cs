using EHD.Model;
using EHD.Model.Entity;
using EHD.Repository;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EHD.Admin {

    public enum CalendarMode{
        Monthly,
        Weekly,
        Daily
    }

    public partial class BookingForm : Form {

        #region Private Members
        private CalendarMode calendarMode = CalendarMode.Daily;
        private DateTime calendarDate = DateTime.Now.Date;
        //private TableLayoutPanelEx tblCalendar;
        private MonthCalendar calendar = null;
        private bool[,] _cellAvail = null;

        private int MIN_TIME_INTERVAL = (Properties.Settings.Default.BookingMinimumMinutes == null
            || Properties.Settings.Default.BookingMinimumMinutes.Length <= 0 ? 30 :
            Convert.ToInt32(Properties.Settings.Default.BookingMinimumMinutes));
        private int ROW_PER_HOUR;// = 60 / MIN_TIME_INTERVAL;//rows per hour.

        private bool isSearching = false;
        #endregion Private Members

        public BookingForm(CalendarMode CalendarMode, DateTime CalendarDate) {
            InitializeComponent();

            this.calendarMode = CalendarMode;
            this.calendarDate = CalendarDate;

        }

        private void BookingForm_Load(object sender, EventArgs e) {

            ROW_PER_HOUR = 60 / MIN_TIME_INTERVAL;//rows per hour.

            //get last booking mode
            if (Properties.Settings.Default.CalendarMode != null && Properties.Settings.Default.CalendarMode.Length > 0) {
                this.calendarMode = (CalendarMode)Convert.ToInt32(Properties.Settings.Default.CalendarMode);
            }

            switch (this.calendarMode) {
                case CalendarMode.Daily:
                    rdoDaily.Checked = true;
                    break;
                case CalendarMode.Weekly:
                    rdoWeekly.Checked = true;
                    break;
                case CalendarMode.Monthly:
                    rdoMonthly.Checked = true;
                    break;
            }

            //Populate dropdown lists
            populateDropdowns();

            //Initiate pager
            pager.Page = 1;
            pager.GoToPageEvent += new GoToPageEventHandler(bookingSearchNavigate);

            //check all practitioners by default
            if (Properties.Settings.Default.ViewPractitionerIds == null || Properties.Settings.Default.ViewPractitionerIds.Length <= 0) {
                chkCheckAll_Click(null, null);
            } else {

                string[] ids = Properties.Settings.Default.ViewPractitionerIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                lstPractitioner.ItemCheck -= lstPractitioner_ItemCheck;
                for (int i = 0; i < lstPractitioner.Items.Count; i++) {
                    SimpleListItem item = lstPractitioner.Items[i] as SimpleListItem;
                    if (ids.Contains(item.Id.ToString())) {
                        lstPractitioner.SetItemChecked(i, chkCheckAll.Checked);
                    }
                }
                lstPractitioner.ItemCheck += lstPractitioner_ItemCheck;

                displayCalendar();
            }
        }

        private void bookingSearchNavigate(int page) {
            searchBookings();
        }

        private List<SimpleListItem> SelectedPractitioners {
            get {
                List<SimpleListItem> users = new List<SimpleListItem>();

                foreach (SimpleListItem user in lstPractitioner.CheckedItems) {
                    users.Add(user);
                }

                return users;
            }
        }

        private List<int> SelectedPractitionerIds {
            get {
                return SelectedPractitioners.Select(x => x.Id).ToList<int>();
            }
        }

        private void rdoCalendarMode_CheckedChanged(object sender, EventArgs e) {

            RadioButton rdo = sender as RadioButton;
            if (!rdo.Checked) return;
            
            if (rdoDaily.Checked) {
                this.calendarMode = CalendarMode.Daily;
                btnThisDate.Text = "Today";
            } else if (rdoWeekly.Checked) {
                this.calendarMode = CalendarMode.Weekly;
                btnThisDate.Text = "This Week";
            } else if (rdoMonthly.Checked) {
                this.calendarMode = CalendarMode.Monthly;
                btnThisDate.Text = "This Month";
            }

            drpBookingMinMinutes.Enabled = (rdoDaily.Checked);

            Properties.Settings.Default.CalendarMode = ((int)this.calendarMode).ToString();
            Properties.Settings.Default.Save();

            displayCalendar();
        }

        private void btnThisDate_Click(object sender, EventArgs e) {
            this.calendarDate = DateTime.Now.Date;
            displayCalendar();
        }

        private void displayCalendar() {

            drpBookingMinMinutes.SelectedIndexChanged -= drpBookingMinMinutes_SelectedIndexChanged;
            drpBookingMinMinutes.SelectedItem = MIN_TIME_INTERVAL.ToString();
            drpBookingMinMinutes.SelectedIndexChanged += drpBookingMinMinutes_SelectedIndexChanged;

            //date range
            DateTime dtStart = (
                this.calendarMode == CalendarMode.Daily ? this.calendarDate.Date : 
                this.calendarMode == CalendarMode.Weekly ? this.calendarDate.Date.AddDays(-Convert.ToInt32(this.calendarDate.DayOfWeek)) :
                this.calendarMode == CalendarMode.Monthly ? new DateTime(this.calendarDate.Year, this.calendarDate.Month, 1) : 
                DateTime.Now.Date);

            DateTime dtEnd = (
                this.calendarMode == CalendarMode.Daily ? dtStart.AddDays(1) :
                this.calendarMode == CalendarMode.Weekly ? dtStart.AddDays(7) :
                this.calendarMode == CalendarMode.Monthly ? dtStart.AddMonths(1) :
                dtStart);

            switch (this.calendarMode) {
                case CalendarMode.Daily:
                    lblCalendarDate.Text = dtStart.ToString("ddd, MMM dd, yyyy");
                    break;
                case CalendarMode.Weekly:
                    lblCalendarDate.Text = string.Format("{0:MMM} {0:dd} - {2}{1:dd}, {0:yyyy}",
                        dtStart, dtEnd,
                        (dtStart.Month == dtEnd.Month ? "" : dtEnd.ToString("MMM ")));
                    break;
                case CalendarMode.Monthly:
                    lblCalendarDate.Text = dtStart.ToString("MMMM yyyy");
                    break;
            }

            //Start to redraw table layout panel
            //tblCalendar.SuspendLayout(); ///!!!! This will create problem for dynamically changing table rows/columns. It works if no rows/columns changes.

            //Clear calendar
            tblCalendar.Controls.Clear();
            tblCalendar.RowStyles.Clear();
            tblCalendar.ColumnStyles.Clear();
            tblCalendar.ColumnCount = 0;
            tblCalendar.RowCount = 0;

            //If no practitioner has been selected, nothing to display
            if (SelectedPractitionerIds.Count <= 0) return;

            //Get bookings
            #region Get bookings
            IList<BookingView> lstBookings = new List<BookingView>();
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Get bookings to display
                    var bookings = session.QueryOver<Booking>()
                        .Fetch(x => x.Patient).Eager
                        .Fetch(x => x.Therapist).Eager
                        .Fetch(x => x.TreatmentType).Eager
                        .Where(x => x.StartTime >= dtStart)
                        .And(x => x.EndTime < dtEnd)
                        .AndRestrictionOn(x => x.Therapist).IsIn(SelectedPractitionerIds)
                        .OrderBy(x => x.StartTime).Asc
                        .List<Booking>();

                    foreach (Booking booking in bookings) {
                        BookingView bkView = new BookingView {
                            Id = booking.Id,
                            Patient = booking.Patient.DisplayName,
                            Practitioner = booking.Therapist.DisplayName,
                            TreatmentType = booking.TreatmentType.DisplayName,
                            StartTime = booking.StartTime,
                            EndTime = booking.EndTime,
                            Note = booking.Note
                        };
                        bkView.SetPatientId(booking.Patient.Id);
                        bkView.SetPractitionerId(booking.Therapist.Id);
                        bkView.SetTreatmentTypeId(booking.TreatmentType.Id);
                        lstBookings.Add(bkView);
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to retrieve bookings. " + ex.Message);
                    Program.log.Error("Failed to retrieve bookings.", ex);
                }
            }
            #endregion Get bookings

            //Prepare variables
            float rowPercent = 100F;//as long as the percent is the same value, the rows will be the same height, the actual percentage value is not important;
            float colPercent = 100F / SelectedPractitioners.Count;

            #region Calendar Modes
            switch (calendarMode) {
                case CalendarMode.Daily:

                    int timeRowCount = (Program.config.WorkingHourEnd - Program.config.WorkingHourStart) * ROW_PER_HOUR;

                    //Create header columns with practitioner names
                    tblCalendar.ColumnCount = SelectedPractitioners.Count + 1;
                    tblCalendar.RowCount = 1;
                    tblCalendar.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                    tblCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
                    for (int i = 1; i < tblCalendar.ColumnCount; i++) {
                        tblCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPercent));
                        Label lbl = new Label {
                            Margin = new Padding(0),
                            Padding = new Padding(0),
                            Text = SelectedPractitioners[i - 1].Text,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Fill,
                            Font = new Font(tblCalendar.Font, FontStyle.Bold),
                            BackColor = Color.White
                        };
                        toolTip1.SetToolTip(lbl, lbl.Text);
                        tblCalendar.Controls.Add(lbl, i, 0);
                    }

                    //Create rows with time lines
                    tblCalendar.RowCount = timeRowCount + 1;
                    for (int i = 1; i < tblCalendar.RowCount; i++) {
                        tblCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
                        double nTime = Program.config.WorkingHourStart + (i - 1f) / ROW_PER_HOUR;
                        int nTimeMinute = Convert.ToInt32(Math.Round((nTime - Math.Truncate(nTime)) * 10));
                        if (nTimeMinute == 0 || nTimeMinute == 5) {
                            string sTime = string.Format("{0:00}:{1:00} {2}",
                                nTime >= 13 ? (int)Math.Floor(nTime) - 12 : (int)Math.Floor(nTime),
                                60 * (nTime - Math.Truncate(nTime)),
                                nTime >= 12 ? "PM" : "AM");
                            Label lbl = new Label() {
                                Text = sTime,
                                TextAlign = ContentAlignment.TopRight,
                                BackColor = Color.White,
                                Margin = new Padding(0),
                                Dock = DockStyle.Fill,
                            };
                            tblCalendar.Controls.Add(lbl, 0, i);
                            int lblSpan = 30 / MIN_TIME_INTERVAL;
                            if (lblSpan < 1) lblSpan = 1;
                            tblCalendar.SetRowSpan(lbl, lblSpan);
                        }
                    }

                    //Populate with booking boxes
                    for (int i = 1; i < tblCalendar.ColumnCount; i++) {
                        int pracId = SelectedPractitioners[i - 1].Id;

                        foreach (BookingView bk in lstBookings.Where(x => x.GetPractitionerId() == pracId).OrderByDescending(x => x.StartTime)) {
                            UCTimeBox box = new UCTimeBox(bk.Id,
                                string.Format("{0} - {1}\n[{2}-{3}][{4}]\n{5}",
                                    bk.TreatmentType.Substring(0, 1).ToUpper(),
                                    bk.Patient,
                                    bk.StartTime.ToString("hh:mm"),
                                    bk.EndTime.ToString("hh:mmtt"),
                                    bk.Practitioner,
                                    bk.Note),
                                getDetailedBookingInfo(bk));
                            box.Dock = DockStyle.Fill;
                            box.Margin = new Padding(0);
                            box.OnDelete += box_OnDelete;
                            box.onEdit += box_onEdit;
                            int row = (int)Math.Truncate((bk.StartTime.Hour * 60 + bk.StartTime.Minute - Program.config.WorkingHourStart * 60) / (60.0 / ROW_PER_HOUR)) + 1;
                            if (row < 1) row = 1;//Should not happen
                            int rowSpan = (int)Math.Truncate((bk.EndTime.Hour * 60 + bk.EndTime.Minute - Program.config.WorkingHourStart * 60 - 1) / (60.0 / ROW_PER_HOUR)) + 1 - row + 1;
                            if (rowSpan < 1) rowSpan = 1;

                            //Check if there is already timebox in the table cells
                            bool cellTaken = false;
                            for (int r = row; r < row + rowSpan; r++) {
                                if (null != tblCalendar.GetControlFromPosition(i, r)) {
                                    cellTaken = true;
                                    break;
                                }
                            }

                            if (cellTaken) {
                                MessageBox.Show(this,
                                    string.Format("{0}\n\n{1}\n\n{2}",
                                        "This booking cannot be displayed as it overlaps other bookings.",
                                        box.DisplayedText,
                                        "You can switch to Weekly or Monthly mode to view hidden bookings."),
                                    "Booking Not Displayed",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                            } else {
                                tblCalendar.Controls.Add(box, i, row);
                                tblCalendar.SetRowSpan(box, rowSpan);
                            }
                        }

                    }

                    //Add clickable panels in available cells for adding new bookings
                    #region Mark cell availabilities

                    _cellAvail = new bool[tblCalendar.ColumnCount, tblCalendar.RowCount];

                    using (ISession session = SessionFactory.GetSessionFactory().OpenSession()) {
                        Service.JCService service = new Service.JCService(session, Program.LogonUser);
                        for (int c = 1; c < tblCalendar.ColumnCount; c++) {

                            int pracId = SelectedPractitioners[c - 1].Id;
                            User prac = session.Get<User>(pracId);
                            TimeRangeCollection availableHours =
                                service.GetPractitionerAvailableHours(prac,
                                    dtStart.Date,
                                    Service.JCService.AvailabilityCheckType.Booking,
                                    Program.config.EnablePractitionerCalendar);

                            for (int r = 1; r < tblCalendar.RowCount; r++) {

                                if (null == tblCalendar.GetControlFromPosition(c, r)) {

                                    DateTime boxStartTime = dtStart.Date.AddHours(Program.config.WorkingHourStart).AddMinutes(MIN_TIME_INTERVAL * (r - 1));
                                    DateTime boxEndTime = boxStartTime.AddMinutes(MIN_TIME_INTERVAL);

                                    //Adding too many Lable or Panel controls to table cells makes program run slow. Use 2-D array to mark the cells' availabilities and paint the background in CellPaint event in stead.
                                    if (availableHours.Cover(new TimeRange(boxStartTime, boxEndTime))) {
                                        _cellAvail[c, r] = true;
                                    } else {
                                        _cellAvail[c, r] = false;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    break;

                case CalendarMode.Weekly:

                    //Create header columns with practitioner names
                    tblCalendar.ColumnCount = SelectedPractitioners.Count + 1;
                    tblCalendar.RowCount = 1;
                    tblCalendar.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                    tblCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
                    for (int i = 1; i < tblCalendar.ColumnCount; i++) {
                        tblCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPercent));
                        Label lbl = new Label {
                            Text = SelectedPractitioners[i - 1].Text,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Fill,
                            Font = new Font(tblCalendar.Font, FontStyle.Bold)
                        };
                        toolTip1.SetToolTip(lbl, lbl.Text);
                        tblCalendar.Controls.Add(lbl, i, 0);
                    }

                    //Create rows with weekday names
                    tblCalendar.RowCount = 8;//7 weekdays plus header row
                    for (int i = 1; i < tblCalendar.RowCount; i++) {
                        tblCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
                        string weekdayName = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[i - 1];
                        Label lbl = new Label() {
                            Text = string.Format("{0}\n{1:MMM dd, yyyy}",
                                weekdayName,
                                dtStart.AddDays(i - 1)),
                            TextAlign = ContentAlignment.MiddleCenter,
                            BackColor = Color.White,
                            Margin = new Padding(0),
                            Dock = DockStyle.Fill,
                            Font = new Font(tblCalendar.Font, FontStyle.Bold)
                        };
                        tblCalendar.Controls.Add(lbl, 0, i);

                        if (i - 1 == (int)DateTime.Now.DayOfWeek) {
                            lbl.BackColor = Color.Orange;
                            lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                        }
                    }

                    //Populate with booking boxes
                    for (int i = 1; i < tblCalendar.ColumnCount; i++) {
                        int pracId = SelectedPractitioners[i - 1].Id;

                        foreach (BookingView bk in lstBookings.Where(x => x.GetPractitionerId() == pracId).OrderByDescending(x => x.StartTime)) {
                            UCTimeBox box = new UCTimeBox(bk.Id,
                                string.Format("{0} - {1}[{2}-{3}]",
                                    bk.TreatmentType.Substring(0, 1).ToUpper(),
                                    bk.Patient,
                                    bk.StartTime.ToString("hh:mm"),
                                    bk.EndTime.ToString("hh:mmtt")
                                    ),
                                getDetailedBookingInfo(bk));
                            box.Dock = DockStyle.Top;
                            box.Margin = new Padding(0);
                            box.OnDelete += box_OnDelete;
                            box.onEdit += box_onEdit;
                            int row = (int)bk.StartTime.DayOfWeek + 1;

                            //create panel if not created yet for each cell
                            Panel pnl = tblCalendar.GetControlFromPosition(i, row) as Panel;
                            if (pnl == null) {
                                pnl = new Panel {
                                    Margin = new Padding(0),
                                    Padding = new Padding(0),
                                    Dock = DockStyle.Fill,
                                    AutoScroll = true
                                };

                                tblCalendar.Controls.Add(pnl, i, row);
                            }

                            //Add timebox to panel
                            pnl.Controls.Add(box);
                        }

                    }

                    break;

                case CalendarMode.Monthly:

                    //Create header columns with weekday names
                    tblCalendar.ColumnCount = 7;
                    tblCalendar.RowCount = 1;
                    tblCalendar.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                    for (int i = 0; i < tblCalendar.ColumnCount; i++) {
                        tblCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPercent));
                        Label lbl = new Label {
                            Text = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[i],
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Fill,
                            Font = new Font(tblCalendar.Font, FontStyle.Bold)
                        };
                        toolTip1.SetToolTip(lbl, lbl.Text);
                        tblCalendar.Controls.Add(lbl, i, 0);
                    }

                    //Determine how many weeks this month has
                    DateTime dtFirst = dtStart.AddDays(-(int)dtStart.DayOfWeek);
                    DateTime dtLast = dtEnd.AddDays(-1).AddDays(6 - (int)dtEnd.AddDays(-1).DayOfWeek);
                    int weekCount = (Convert.ToInt32((dtLast - dtFirst).TotalDays) + 1) / 7;

                    //Create rows with dates/timeboxes
                    tblCalendar.RowCount = weekCount + 1;
                    for (int r = 1; r < tblCalendar.RowCount; r++) {
                        tblCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));

                        for (int c = 0; c < 7; c++) {

                            DateTime dtCell = dtFirst.AddDays((r - 1) * 7 + c);

                            //Create split panel with date
                            int dateLabelHeight = Convert.ToInt32(Label.DefaultFont.GetHeight());
                            SplitContainer splPnl = new SplitContainer {
                                Margin = new Padding(0),
                                Orientation = Orientation.Horizontal,
                                Dock = DockStyle.Fill,
                                IsSplitterFixed = true,
                                SplitterWidth = 1,
                                Panel1MinSize = dateLabelHeight,
                                SplitterDistance = dateLabelHeight
                            };

                            Label lblDate = new Label {
                                Text = dtCell.Day.ToString(),
                                Dock = DockStyle.Fill,
                                TextAlign = ContentAlignment.TopRight
                            };
                            splPnl.Panel1.Controls.Add(lblDate);
                            if (dtCell == DateTime.Now.Date) {
                                lblDate.BackColor = Color.Orange;
                                lblDate.Font = new Font(lblDate.Font, FontStyle.Bold);
                                lblDate.Text = "(Today) " + lblDate.Text;
                            }

                            splPnl.Panel2.Margin = new Padding(0);
                            splPnl.Panel2.Padding = new Padding(0);
                            splPnl.Panel2.AutoScroll = true;

                            //Add timeboxes into cell panel
                            foreach (BookingView bk in lstBookings.Where(x => x.StartTime.Date == dtCell).OrderByDescending(x => x.StartTime)) {
                                UCTimeBox box = new UCTimeBox(bk.Id,
                                    string.Format("{0} - {1}[{2}][{3}-{4}]",
                                        bk.TreatmentType.Substring(0, 1).ToUpper(),
                                        bk.Patient,
                                        bk.Practitioner,
                                        bk.StartTime.ToString("hh:mm"),
                                        bk.EndTime.ToString("hh:mmtt")
                                        ),
                                    getDetailedBookingInfo(bk));
                                box.Dock = DockStyle.Top;
                                box.Margin = new Padding(0);
                                box.OnDelete += box_OnDelete;
                                box.onEdit += box_onEdit;

                                splPnl.Panel2.Controls.Add(box);
                            }

                            tblCalendar.Controls.Add(splPnl, c, r);
                        }
                    }

                    break;
            }
            #endregion Calendar Modes

        }

        void box_onEdit(object sender, EventArgs e) {
            UCTimeBox bkBox = sender as UCTimeBox;

            if (bkBox == null) return;

            //Get booking Id
            int id = bkBox.Id;

            if (id <= 0) return;

            DialogResult ret = (new EditBookingForm(id)).ShowDialog(this);
            if (DialogResult.OK == ret) {
                displayCalendar();
            }
        }

        void box_OnDelete(object sender, EventArgs e) {
            UCTimeBox bkBox = sender as UCTimeBox;

            if (bkBox == null) return;

            //Get booking Id
            int id = bkBox.Id;

            if (id <= 0) return;

            if (DialogResult.OK != MessageBox.Show(this, "Press [OK] to confirm you want to DELETE this booking.", "Confirm to DELETE", MessageBoxButtons.OKCancel)) {
                return;
            }

            //Remove booking from DB
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Load Patient and collections to prevent from NHibernate "... collection was not flushed..." error
                    //Booking bkToDelete = session.Get<Booking>(id).;
                    Booking bkToDelete = session.QueryOver<Booking>()
                        .Where(x => x.Id == id)
                        .Fetch(x => x.Patient).Eager
                        .Fetch(x => x.Patient.Invoices).Eager
                        .Fetch(x => x.Therapist).Eager
                        .Fetch(x => x.TreatmentType).Eager
                        .SingleOrDefault<Booking>();
                    if (bkToDelete != null) {
                        bkToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                        bkToDelete.UpdatedTime = DateTime.Now;

                        session.Delete(bkToDelete);
                    }

                    tr.Commit();

                    //Remove from screen
                    bkBox.Parent.Controls.Remove(bkBox);
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to delete booking. " + ex.Message);
                    Program.log.Error("Failed to delete booking.", ex);
                }
            }

        }

        private void populateDropdowns() {

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    #region Practitioner List
                    
                    //Clear practitioner list
                    lstPractitioner.Items.Clear();

                    //Get all practitioners
                    SimpleListItem lstItemAlias = null;

                    IList<SimpleListItem> lstPrac = session.QueryOver<User>()
                        .Where(x => x.UserType == UserType.Therapist)
                        .OrderBy(x => x.FirstName).Asc
                        .ThenBy(x => x.LastName).Asc
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => lstItemAlias.Id)
                            .Select(x => Projections.Concat(x.FirstName, " ", x.LastName)).WithAlias(() => lstItemAlias.Text)
                            )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .Take(Program.config.MaxTherapists)
                        .List<SimpleListItem>();

                    lstPractitioner.Items.AddRange(lstPrac.ToArray<SimpleListItem>());
                    #endregion Practitioner List

                    #region Practitioner Dropdown
                    lstPrac.Insert(0, new SimpleListItem { Id = 0, Text = "" });
                    drpUser.DataSource = lstPrac;
                    drpUser.DisplayMember = "Text";
                    drpUser.ValueMember = "Id";
                    #endregion Practitioner Dropdown

                    #region Treatment Type Dropdown
                    IList<SimpleListItem> lstTreatments = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => lstItemAlias.Id)
                            .Select(x => x.TherapyTypeName).WithAlias(() => lstItemAlias.Text)
                            )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .Take(Program.config.MaxTherapists)
                        .List<SimpleListItem>();

                    lstTreatments.Insert(0, new SimpleListItem { Id = 0, Text = "" });
                    drpTreatmentType.DataSource = lstTreatments;
                    drpTreatmentType.DisplayMember = "Text";
                    drpTreatmentType.ValueMember = "Id";
                    #endregion Treatment Type Dropdown

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to retrieve practitioners. " + ex.Message);
                    Program.log.Error("Failed to retrieve practitioners.", ex);
                }
            }

        }

        private void btnPreDate_Click(object sender, EventArgs e) {
            nextCalendarDate(-1);
        }

        private void btnNextDate_Click(object sender, EventArgs e) {
            nextCalendarDate(1);
        }

        private void nextCalendarDate(int direction) {
            switch (this.calendarMode) {
                case CalendarMode.Daily:
                    this.calendarDate = this.calendarDate.AddDays(1 * direction);
                    break;
                case CalendarMode.Weekly:
                    this.calendarDate = this.calendarDate.AddDays(7 * direction);
                    break;
                case CalendarMode.Monthly:
                    this.calendarDate = this.calendarDate.AddMonths(1 * direction);
                    break;
            }

            displayCalendar();
        }

        private void btnAddNewBooking_Click(object sender, EventArgs e) {
            addNewBooking(null, null);
        }

        private void addNewBooking(int? pracId, DateTime? bookingDateTime) {

            EditBookingForm bookingForm = null;

            if (pracId.HasValue && bookingDateTime.HasValue) {
                bookingForm = new EditBookingForm(pracId.Value, bookingDateTime.Value);
            } else {
                bookingForm = new EditBookingForm();
            }

            DialogResult ret = bookingForm.ShowDialog(this);
            if (DialogResult.OK == ret) {
                displayCalendar();
            }
        }

        private void chkCheckAll_Click(object sender, EventArgs e) {

            lstPractitioner.ItemCheck -= lstPractitioner_ItemCheck;

            for (int i = 0; i < lstPractitioner.Items.Count; i++) {
                lstPractitioner.SetItemChecked(i, chkCheckAll.Checked);
            }

            lstPractitioner.ItemCheck += lstPractitioner_ItemCheck;

            displayCalendar();

            Properties.Settings.Default.ViewPractitionerIds = "";
            Properties.Settings.Default.Save();
        }

        private void lstPractitioner_ItemCheck(object sender, ItemCheckEventArgs e) {
            
            //ItemCheck event fires before the item actually being checked/unchecked.
            //Since there is no ItemChecked event handler, we mimic the ItemChecked effect here
            lstPractitioner.ItemCheck -= lstPractitioner_ItemCheck;
            lstPractitioner.SetItemCheckState(e.Index, e.NewValue);
            lstPractitioner.ItemCheck += lstPractitioner_ItemCheck;
            //Now, Item has been checked/unchecked

            displayCalendar();

            //Save checked practitioner ids into setting
            List<int> ids = new List<int>();

            foreach (SimpleListItem user in lstPractitioner.CheckedItems) {
                ids.Add(user.Id);
            }

            Properties.Settings.Default.ViewPractitionerIds = String.Join(",", ids);
            Properties.Settings.Default.Save();
        }

        private void btnShowDatePicker_Click(object sender, EventArgs e) {

            if (null != calendar && calendar.Visible) {
                calendar.Visible = false;
            } else {
                if (null == calendar) {
                    calendar = new MonthCalendar();
                    calendar.DateSelected += new DateRangeEventHandler(calendar_DateSelected);
                    calendar.Visible = false;
                    this.Controls.Add(calendar);
                }

                calendar.SetDate(this.calendarDate);
                calendar.Visible = true;
                calendar.BringToFront();

                calendar.Location = btnShowDatePicker.FindForm().PointToClient(
                    btnShowDatePicker.Parent.PointToScreen(btnShowDatePicker.Location));
                calendar.Top += btnShowDatePicker.Height;
                if (calendar.Right > this.ClientSize.Width) {
                    calendar.Left -= (calendar.Right - this.ClientSize.Width);
                }
            }

        }

        void calendar_DateSelected(object sender, DateRangeEventArgs e) {
            calendar.Visible = false;
            this.calendarDate = e.Start;
            displayCalendar();
        }

        private void BookingForm_Resize(object sender, EventArgs e) {
            //update popup calendar location
            if (null != calendar && calendar.Visible) {
                calendar.Location = btnShowDatePicker.FindForm().PointToClient(
                    btnShowDatePicker.Parent.PointToScreen(btnShowDatePicker.Location));
                calendar.Top += btnShowDatePicker.Height;
                if (calendar.Right > this.ClientSize.Width) {
                    calendar.Left -= (calendar.Right - this.ClientSize.Width);
                }
            }
        }

        private void lblCalendarDate_TextChanged(object sender, EventArgs e) {
            if (null != calendar && calendar.Visible) {
                calendar.Visible = false;
            }
        }

        private string getDetailedBookingInfo(BookingView bk) {
            return bk == null ? string.Empty :
                string.Format("{0} - {1}\nTime: {2} {3}-{4}\nPractitioner: {5}\n{6}",
                    bk.TreatmentType,
                    bk.Patient,
                    bk.StartTime.ToString("MMM dd, yyyy"),
                    bk.StartTime.ToString("hh:mm"),
                    bk.EndTime.ToString("hh:mmtt"),
                    bk.Practitioner,
                    bk.Note);
        }

        private void tblCalendar_MouseClick(object sender, MouseEventArgs e) {
            Point loc = e.Location;

        }

        private void tblCalendar_CellPaint(object sender, TableLayoutCellPaintEventArgs e) {


            if (this.calendarMode == CalendarMode.Daily && _cellAvail != null) {

                //ignore the row and column headers
                if (e.Column == 0 || e.Row == 0) return;
                
                if (_cellAvail[e.Column, e.Row]) {
                    if ( (e.Row-1) / ROW_PER_HOUR % 2 == 0) {
                        e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.CellBounds);
                    } else {
                        e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
                    }
                } else {
                    e.Graphics.FillRectangle(Brushes.LightGray, e.CellBounds);
                }
            }
        }

        private void tblCalendar_Click(object sender, EventArgs e) {

            //Determin clicked column and row (C & R)
            MouseEventArgs evt = e as MouseEventArgs;
            if (evt == null) return;

            int[] rowHeights = tblCalendar.GetRowHeights();
            int[] colWidths = tblCalendar.GetColumnWidths();

            if (rowHeights == null || rowHeights.Length < 2) return;
            if (colWidths == null || colWidths.Length < 2) return;

            int headerHeight = rowHeights[0];
            int rowHeight = rowHeights[1];
            int headerWidth = colWidths[0];
            int colWidth = colWidths[1];

            int C = 0;
            int R = 0;

            if (evt.X >= headerWidth) {
                C = (evt.X - headerWidth) / colWidth + 1;
                if (C >= tblCalendar.ColumnCount) C = tblCalendar.ColumnCount - 1;
            }
            if (evt.Y >= headerHeight) {
                R = (evt.Y - headerHeight) / rowHeight + 1;
                if (R >= tblCalendar.RowCount) R = tblCalendar.RowCount - 1;
            }

            //Get PractitionerId and DateTime based on the clicked column/row
            if(calendarMode == CalendarMode.Daily){

                if (C <= 0 || R <= 0) return;
                if (_cellAvail != null && !_cellAvail[C, R]) return;
                
                int pracId = SelectedPractitioners[C - 1].Id;
                DateTime date = calendarDate.Date
                    .AddHours(Program.config.WorkingHourStart)
                    .AddMinutes((R - 1) * MIN_TIME_INTERVAL);

                addNewBooking(pracId, date);
            }
        }

        private void drpBookingMinMinutes_SelectedIndexChanged(object sender, EventArgs e) {
            string interval = drpBookingMinMinutes.SelectedItem.ToString();
            MIN_TIME_INTERVAL = Convert.ToInt32(interval);
            ROW_PER_HOUR = 60 / MIN_TIME_INTERVAL;//rows per hour.
            Properties.Settings.Default.BookingMinimumMinutes = interval;
            Properties.Settings.Default.Save();

            displayCalendar();
        }

        private void ucPatient_MinInputLengthReached(object sender, EventArgs e) {

            if (isSearching) return;

            isSearching = true;

            //Search patients
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession()) 
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

        private void btnSearch_Click(object sender, EventArgs e) {
            pager.Page = 1;
            searchBookings();
        }

        private void searchBookings() {

            //Clear existing data
            grdBookingList.DataSource = null;

            //Create booking list for viewing only
            IList<BookingView> lstBookings = new List<BookingView>();

            //Get search result
            #region Search from Database
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {

                    //Get selected patient
                    Patient patient = null;
                    if (ucPatient.SelectedItem != null && ucPatient.SelectedItem.Id != default(int)) {
                        patient = session.Get<Patient>(ucPatient.SelectedItem.Id);
                    }

                    //Get selected Practitioner
                    SimpleListItem practitioner = null;
                    if (drpUser.SelectedItem != null) {
                        practitioner = drpUser.SelectedItem as SimpleListItem;
                    }

                    //Get selected Treatment Type
                    SimpleListItem treatmentType = null;
                    if (drpTreatmentType.SelectedItem != null) {
                        treatmentType = drpTreatmentType.SelectedItem as SimpleListItem;
                    }

                    //Search bookings
                    var qry = session.QueryOver<Booking>()
                        .Fetch(x => x.Patient).Eager
                        .Fetch(x => x.Therapist).Eager
                        .Fetch(x => x.TreatmentType).Eager;

                    if (patient != null && patient.Id != 0) qry.Where(x => x.Patient.Id == patient.Id);
                    if (practitioner != null && practitioner.Id != 0) qry.Where(x => x.Therapist.Id == practitioner.Id);
                    if (treatmentType != null && treatmentType.Id != 0) qry.Where(x => x.TreatmentType.Id == treatmentType.Id);
                    if (dtDateFrom.Checked) qry.Where(x => x.StartTime >= dtDateFrom.Value.Date);
                    if (dtDateTo.Checked) qry.Where(x => x.EndTime < dtDateTo.Value.Date.AddDays(1));

                    var qryBookings = qry
                        .OrderBy(x => x.StartTime).Asc
                        .Skip((pager.Page - 1) * pager.PageSize)
                        .Take(pager.PageSize)
                        .Future<Booking>();

                    //Get total count
                    int totalCount = (qry.Clone())
                        .RowCount();

                    lblTotalFound.Text = totalCount.ToString();

                    //Get bookings
                    var bookings = qryBookings.ToList<Booking>();

                    //Set pager
                    if (totalCount == 0) {
                        pager.TotalPage = 1;
                    } else {
                        pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalCount - 1) / pager.PageSize)) + 1;
                    }

                    foreach (Booking bk in bookings) {
                        lstBookings.Add(new BookingView {
                            Id = bk.Id,
                            Patient = bk.Patient.DisplayName,
                            Practitioner = bk.Therapist.DisplayName,
                            TreatmentType = bk.TreatmentType.DisplayName,
                            StartTime = bk.StartTime,
                            EndTime = bk.EndTime,
                            Note = bk.Note
                        });
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to search bookings. " + ex.Message);
                    Program.log.Error("Failed to search bookings.", ex);
                }
            }
            #endregion Search from Database

            //Display search result
            grdBookingList.DataSource = lstBookings;
        }
    }
}
