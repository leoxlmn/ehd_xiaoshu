using HolidayCalculator;
using EHD.Model.Entity;
using EHD.Repository;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace EHD.Admin {

    enum OVERLAP {
        None,
        Top,
        Bottom,
        Inside,
        Outside
    }

    public partial class AvailabilityCalendarForm : Form {

        const string AVAIL_FULLDAY = "Full Day";
        const string AVAIL_PARTLY = "Partly";
        const string AVAIL_OFF = "Off";

        enum Availablities {
            FullDay,
            Partly,
            Off
        }

        public AvailabilityCalendarForm() {
            InitializeComponent();
        }

        private void AvailabilityCalendarForm_Load(object sender, EventArgs e) {

            IList<SimpleListItem> practitioners = getPractitioners();

            if(practitioners == null || practitioners.Count <= 0) {
                MessageBox.Show(this, 
                    "No practitioner found!\n\nPlease create at least one practitioner first by going to \n\nSettings -> Users",
                    "Practitioner needed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation );
                this.Close();
                return;
            }

            //Populate Practitioner List
            lstUsers.DisplayMember = "Text";
            lstUsers.ValueMember = "Id";
            lstUsers.Items.AddRange(practitioners.ToArray());

            //Populate AddTimeOff panel
            drpUser.DisplayMember = "Text";
            drpUser.ValueMember = "Id";
            drpUser.Items.AddRange(practitioners.ToArray());
            DateTime now = DateTime.Now;
            dtFrom.Value = new DateTime(now.Year, now.Month, now.Day, Program.config.WorkingHourStart, 0, 0);
            dtTo.Value = new DateTime(now.Year, now.Month, now.Day, Program.config.WorkingHourEnd, 0, 0);
            txtReason.Text = "Vacation";

            //Populate Time Off tab
            for (int i = 1; i <= 12; i++) {
                drpMonth.Items.Add(new SimpleListItem {
                    Id = i,
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)
                });

                if (i == DateTime.Now.Month) {
                    drpMonth.SelectedIndex = i - 1;
                }
            }
            //Add index change event handler manually
            drpMonth.SelectedIndexChanged += drpMonth_SelectedIndexChanged;
            refreshYear(DateTime.Now.Year);
            displayTimeOffs();

            #region Populate Weekly Availability tab
            tblWeekly.SuspendLayout();

            //Add radio buttons
            int row = 1;
            for (int col = 0; col <= 6; col++) {
                Panel pnl = new Panel();
                pnl.Dock = DockStyle.Fill;

                UCAddTimeBlock ucAddTimeBlock = new UCAddTimeBlock((DayOfWeek)col);
                ucAddTimeBlock.StartHour = Program.config.WorkingHourStart;
                ucAddTimeBlock.EndHour = Program.config.WorkingHourEnd;
                ucAddTimeBlock.Dock = DockStyle.Top;
                ucAddTimeBlock.OnAdd += ucAddTimeBlock_OnAdd;
                pnl.Controls.Add(ucAddTimeBlock);

                RadioButton rdoPartly = new RadioButton();
                rdoPartly.Text = AVAIL_PARTLY;
                rdoPartly.Dock = DockStyle.Top;
                rdoPartly.Tag = col;
                //rdoPartly.CheckedChanged += weeklyAvailChanged;
                rdoPartly.AutoCheck = false;
                rdoPartly.Click += weeklyAvailChanged;
                pnl.Controls.Add(rdoPartly);

                RadioButton rdoNotAvail = new RadioButton();
                rdoNotAvail.Text = AVAIL_OFF;
                rdoNotAvail.Dock = DockStyle.Top;
                rdoNotAvail.Tag = col;
                //rdoNotAvail.CheckedChanged += weeklyAvailChanged;
                rdoNotAvail.AutoCheck = false;
                rdoNotAvail.Click += weeklyAvailChanged;
                pnl.Controls.Add(rdoNotAvail);

                RadioButton rdoFullDay = new RadioButton();
                rdoFullDay.Text = AVAIL_FULLDAY;
                rdoFullDay.Dock = DockStyle.Top;
                rdoFullDay.Tag = col;
                //rdoFullDay.CheckedChanged += weeklyAvailChanged;
                rdoFullDay.AutoCheck = false;
                rdoFullDay.Click += weeklyAvailChanged;
                pnl.Controls.Add(rdoFullDay);

                tblWeekly.Controls.Add(pnl, col, row);
            }

            tblWeekly.ResumeLayout();
            #endregion Populate Weekly Availability tab


            //Set Full Day for adding time offs by default
            chkFullDay.Checked = true;

            //Show TimeOff tab initially
            tabUserSchedule.SelectedIndex = 1;
        }

        private void weeklyAvailChanged(object sender, EventArgs e) {
            if (lstUsers.SelectedItem == null) return;

            RadioButton rdo = sender as RadioButton;
            if (rdo != null && !rdo.Checked) {

                //Get current column
                int col = (int)rdo.Tag;

                //Get previous checked radio button, and clear it's status
                RadioButton rdoPreChecked = null;
                Panel pnlButtons = tblWeekly.GetControlFromPosition(col, 1) as Panel;
                if (pnlButtons != null) {
                    foreach (Control ctrl in pnlButtons.Controls) {
                        if (ctrl is RadioButton && (ctrl as RadioButton).Checked) {
                            rdoPreChecked = ctrl as RadioButton;
                            break;
                        }
                    }
                }
                if(rdoPreChecked != null) rdoPreChecked.Checked = false;

                //Set checked status
                rdo.Checked = true;

                //Check if there is any existing availability record. Show warning if yes.
                Panel pnlDetail = tblWeekly.GetControlFromPosition(col, 2) as Panel;
                if (pnlDetail != null) {
                    foreach (Control ctrl in pnlDetail.Controls) {
                        if (ctrl is UCTimeBox) {
                            if (DialogResult.OK != MessageBox.Show(this, "The existing working time frames will be removed.\n\nPress [OK] to confirm.\nPress [Cancel] to cancel.", "Warning", MessageBoxButtons.OKCancel)) {
                                //cancel
                                rdo.Checked = false;
                                if (rdoPreChecked != null) rdoPreChecked.Checked = true;
                                return;
                            }
                            break;
                        }
                    }
                }

                //Set UCAddTimeBlock control
                foreach (Control ctrl in pnlButtons.Controls) {
                    if (ctrl is UCAddTimeBlock) {
                        (ctrl as UCAddTimeBlock).Visible = (rdo.Text == AVAIL_PARTLY);
                    }
                }

                //1) Get the current selected practitioner id
                int pracId = (lstUsers.SelectedItem as SimpleListItem).Id;

                //2) Get weekday
                DayOfWeek weekDay = (DayOfWeek)rdo.Tag;

                //3) Update DB
                using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    try {

                        //Clear all existing availability records
                        var existingAvails = session.QueryOver<UserWeeklyAvailability>()
                            .Where(x => x.Practitioner.Id == pracId)
                            .Where(x => x.WeekDay == weekDay)
                            .List<UserWeeklyAvailability>();

                        foreach (UserWeeklyAvailability avail in existingAvails) {
                            avail.UpdatedBy = Program.LogonUser.DisplayName;
                            avail.UpdatedTime = DateTime.Now;
                            session.Delete(avail);
                        }

                        if (rdo.Text == AVAIL_FULLDAY) {
                            //Insert a new Full Day weekly availability record
                            UserWeeklyAvailability newAvail = new UserWeeklyAvailability {
                                WeekDay = weekDay,
                                Practitioner = session.Get<User>(pracId),
                                FullDayAvailable = true,
                                IsTestData = Program.LogonUser.IsTestData,
                                CreatedBy = Program.LogonUser.DisplayName,
                                CreatedTime = DateTime.Now
                            };

                            session.Save(newAvail);
                        }

                        tr.Commit();
                    } catch (Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, "Failed to update weekly availability. " + ex.Message);
                        Program.log.Error("Failed to update weekly availability.", ex);
                    }
                }

                //4) Refresh right panel
                tblWeekly.SuspendLayout();

                tblWeekly.Controls.Remove(tblWeekly.GetControlFromPosition(col, 2));

                //Create panel for row 2
                Panel pnlNewDetail = new Panel();
                pnlNewDetail.Dock = DockStyle.Fill;

                if (rdo.Text == AVAIL_FULLDAY) {
                    Label lblFullDay = new Label();
                    lblFullDay.Text = AVAIL_FULLDAY;
                    lblFullDay.AutoSize = false;
                    lblFullDay.Dock = DockStyle.Fill;
                    lblFullDay.TextAlign = ContentAlignment.MiddleCenter;
                    pnlNewDetail.BackColor = Color.LightGreen;
                    pnlNewDetail.Controls.Add(lblFullDay);
                } else if (rdo.Text == AVAIL_OFF) {
                    Label lblOff = new Label();
                    lblOff.Text = AVAIL_OFF;
                    lblOff.AutoSize = false;
                    lblOff.Dock = DockStyle.Fill;
                    lblOff.TextAlign = ContentAlignment.MiddleCenter;
                    pnlNewDetail.BackColor = Color.FromArgb(230, 230, 230);
                    pnlNewDetail.Controls.Add(lblOff);
                }

                tblWeekly.Controls.Add(pnlNewDetail, col, 2);

                tblWeekly.ResumeLayout();
            }
        }

        private void ucAddTimeBlock_OnAdd(object sender, EventArgs e) {
            if (lstUsers.SelectedItem == null) return;

            UCAddTimeBlock uc = (UCAddTimeBlock)sender;

            DayOfWeek weekDay = uc.WeekDay;
            decimal startHour = uc.StartHour;
            decimal endHour = uc.EndHour;
            int pracId = (lstUsers.SelectedItem as SimpleListItem).Id;
            int col = (int)weekDay;

            //Start/end hours cannot exceed business hours
            if (startHour < (Program.config.WorkingHourStart)) {
                MessageBox.Show(this, "The start time cannot be earlier than the business start hour.", "Time Error", MessageBoxButtons.OK);
                return;
            }
            if (endHour > (Program.config.WorkingHourEnd)) {
                MessageBox.Show(this, "The end time cannot be later than the business end hour.", "Time Error", MessageBoxButtons.OK);
                return;
            }

            if (endHour <= startHour) {
                MessageBox.Show(this, "Start time has to be earlier the end time.", "Time Error", MessageBoxButtons.OK);
                return;
            }

            IList<UserWeeklyAvailability> weeklyAvails = null;

            //Insert into DB
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Get all existing working hours
                    IList<UserWeeklyAvailability> avails = session.QueryOver<UserWeeklyAvailability>()
                        .Where(x => x.WeekDay == weekDay)
                        .And(x => x.Practitioner.Id == pracId)
                        .OrderBy(x => x.StartHour).Asc
                        .List<UserWeeklyAvailability>();

                    if (avails == null || avails.Count <= 0) {
                        UserWeeklyAvailability newAvail = new UserWeeklyAvailability {
                            Practitioner = session.Get<User>(pracId),
                            FullDayAvailable = false,
                            StartHour = startHour,
                            EndHour = endHour,
                            WeekDay = weekDay,
                            CreatedBy = Program.LogonUser.DisplayName,
                            CreatedTime = DateTime.Now
                        };

                        session.Save(newAvail);
                    } else {
                        OVERLAP[] marks = new OVERLAP[avails.Count];
                        bool overlapped = false;
                        for (int i = 0; i < avails.Count; i++) {
                            if (avails[i].StartHour > endHour || avails[i].EndHour < startHour) {
                                marks[i] = OVERLAP.None;
                            } else if (avails[i].StartHour >= startHour && avails[i].EndHour <= endHour) {
                                marks[i] = OVERLAP.Inside;
                                overlapped = true;
                            } else if (avails[i].StartHour < startHour && avails[i].EndHour > endHour) {
                                marks[i] = OVERLAP.Outside;
                                overlapped = true;
                            } else if (avails[i].StartHour <= endHour && avails[i].EndHour > endHour) {
                                marks[i] = OVERLAP.Top;
                                overlapped = true;
                            } else if (avails[i].StartHour < startHour && avails[i].EndHour >= startHour) {
                                marks[i] = OVERLAP.Bottom;
                                overlapped = true;
                            } else {
                                throw new Exception("Application Logic Error! Check with the developer!");
                            }
                        }

                        if (!overlapped) {
                            UserWeeklyAvailability newAvail = new UserWeeklyAvailability {
                                Practitioner = session.Get<User>(pracId),
                                FullDayAvailable = false,
                                StartHour = startHour,
                                EndHour = endHour,
                                WeekDay = weekDay,
                                CreatedBy = Program.LogonUser.DisplayName,
                                CreatedTime = DateTime.Now
                            };

                            session.Save(newAvail);
                        } else {
                            int overlappedTopIndex = -1;
                            int overlappedBottomIndex = -1;
                            for (int i = 0; i < avails.Count; i++) {
                                if (marks[i] != OVERLAP.None) {
                                    if (overlappedTopIndex == -1) {
                                        overlappedTopIndex = i;
                                        overlappedBottomIndex = i;
                                    } else {
                                        overlappedBottomIndex = i;
                                        break;
                                    }
                                }
                            }

                            //Remove all overlapped avails in between
                            for (int i = overlappedTopIndex + 1; i <= overlappedBottomIndex - 1; i++) {
                                session.Delete(avails[i]);
                            }

                            //Merge overlapped avails
                            decimal mergedStart = Math.Min(avails[overlappedTopIndex].StartHour, startHour);
                            decimal mergedEnd = Math.Max(avails[overlappedBottomIndex].EndHour, endHour);
                            if (overlappedBottomIndex != overlappedTopIndex) {
                                session.Delete(avails[overlappedBottomIndex]);
                            }
                            avails[overlappedTopIndex].StartHour = mergedStart;
                            avails[overlappedTopIndex].EndHour = mergedEnd;
                            avails[overlappedTopIndex].UpdatedBy = Program.LogonUser.DisplayName;
                            avails[overlappedTopIndex].UpdatedTime = DateTime.Now;

                            session.Update(avails[overlappedTopIndex]);

                        }

                    }

                    //Retrieve all availabilities on this day
                    weeklyAvails = session.QueryOver<UserWeeklyAvailability>()
                        .Fetch(x => x.Practitioner).Eager
                        .Where(x => x.Practitioner.Id == pracId)
                        .Where(x => x.WeekDay == weekDay)
                        .List<UserWeeklyAvailability>();

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to add weekly availability. " + ex.Message);
                    Program.log.Error("Failed to add weekly availability.", ex);
                }
            }

            //Update right panel
            tblWeekly.SuspendLayout();

            Panel pnlDetail = tblWeekly.GetControlFromPosition(col, 2) as Panel;

            if (pnlDetail != null) {
                pnlDetail.Controls.Clear();

                foreach (UserWeeklyAvailability avail in weeklyAvails.OrderByDescending(x => x.StartHour)) {
                    UCTimeBox timeBlock = new UCTimeBox(avail.Id,
                        string.Format("{0}-{1}",
                            DateTimeHelper.DoubleHourToTime((double)avail.StartHour, "h:mmtt"),
                            DateTimeHelper.DoubleHourToTime((double)avail.EndHour, "h:mmtt")));
                    timeBlock.Dock = DockStyle.Top;
                    timeBlock.OnDelete += timeBlock_OnDelete;

                    pnlDetail.Controls.Add(timeBlock);
                }
            }

            tblWeekly.ResumeLayout();
        }

        /// <summary>
        /// Retrieve all practitioner id/names
        /// </summary>
        /// <returns></returns>
        private IList<SimpleListItem> getPractitioners() {

            IList<SimpleListItem> practitioners = null;

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    SimpleUserItem alias = new SimpleUserItem();
                    //Get all practitioners
                    var users = session.QueryOver<User>()
                        .Where(x => x.UserType == UserType.Therapist)
                        .SelectList(list => list
                            .Select(p => p.Id).WithAlias(() => alias.Id)
                            .Select(p => p.LastName).WithAlias(() => alias.LastName)
                            .Select(p => p.FirstName).WithAlias(() => alias.FirstName)
                            .Select(p => p.MiddleName).WithAlias(() => alias.MiddleName)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleUserItem>())
                        .List<SimpleUserItem>();

                    if (users != null && users.Count > 0) {
                        practitioners = new List<SimpleListItem>();
                        foreach (SimpleUserItem item in users) {
                            practitioners.Add(new SimpleListItem {
                                Id = item.Id,
                                Text = string.Format("{0}{1} {2}", 
                                    item.FirstName, 
                                    item.MiddleName != null && item.MiddleName.Trim().Length > 0 ? " " + item.MiddleName : string.Empty,
                                    item.LastName)
                            });
                        }
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load. " + ex.Message);
                    Program.log.Error("Failed to load Availability Calendar data.", ex);
                }
            }

            return practitioners;
        }

        /// <summary>
        /// Make the CheckedListBox to be single selection mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstUsers_ItemCheck(object sender, ItemCheckEventArgs e) {
            for (int i = 0; i < lstUsers.Items.Count; i++) {
                if (e.Index != i) lstUsers.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// Refresh the right panel when different practitioner has been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e) {

            if (lstUsers.SelectedItem == null) return;

            refreshWeeklyAvailability();
        }

        private void refreshWeeklyAvailability() {

            if (lstUsers.SelectedItem == null) return;

            //Get selected practitioner id
            int pracId = (lstUsers.SelectedItem as SimpleListItem).Id;

            //Get WeeklyAvailability for the selected practitioner
            IList<UserWeeklyAvailability> weeklyAvails = null;

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    weeklyAvails = session.QueryOver<UserWeeklyAvailability>()
                        .Fetch(x => x.Practitioner).Eager
                        .Where(x => x.Practitioner.Id == pracId)
                        .List<UserWeeklyAvailability>();

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to retrieve weekly availability. " + ex.Message);
                    Program.log.Error("Failed to retrieve weekly availability.", ex);
                }
            }

            //3) Display
            tblWeekly.SuspendLayout();

            for (int col = 0; col <= 6; col++) {

                //Get current week day
                DayOfWeek weekDay = (DayOfWeek)col;

                //Determine the available status on this week day
                Availablities availabilityStatus = Availablities.Off;

                IList<UserWeeklyAvailability> avails = weeklyAvails.Where(x => x.WeekDay == weekDay).ToList();
                
                if (avails == null || avails.Count <= 0) {//Off
                    availabilityStatus = Availablities.Off;
                } else {
                    availabilityStatus = Availablities.Partly;
                    foreach (UserWeeklyAvailability avail in avails) {
                        if (avail.FullDayAvailable) {
                            availabilityStatus = Availablities.FullDay;
                            break;
                        }
                    }
                }

                //Populate radio buttons, update UCAddTimeBlock status
                Panel pnl = tblWeekly.GetControlFromPosition(col, 1) as Panel;
                foreach (Control ctrl in pnl.Controls) {
                    RadioButton rdo = ctrl as RadioButton;
                    if (rdo != null) {
                        rdo.Click -= weeklyAvailChanged;
                        switch (rdo.Text) {
                            case AVAIL_FULLDAY:
                                rdo.Checked = (availabilityStatus == Availablities.FullDay);
                                break;
                            case AVAIL_PARTLY:
                                rdo.Checked = (availabilityStatus == Availablities.Partly);
                                break;
                            case AVAIL_OFF:
                                rdo.Checked = (availabilityStatus == Availablities.Off);
                                break;
                        }
                        rdo.Click += weeklyAvailChanged;
                    }

                    UCAddTimeBlock uc = ctrl as UCAddTimeBlock;
                    if (uc != null) {
                        uc.Visible = (availabilityStatus == Availablities.Partly);
                    }
                }


                //Remove existing controls first from row 2
                tblWeekly.Controls.Remove(tblWeekly.GetControlFromPosition(col, 2));

                //Create panel for row 2
                Panel pnlDetail = new Panel();
                pnlDetail.Dock = DockStyle.Fill;

                //
                switch (availabilityStatus) {
                    case Availablities.FullDay:
                        Label lblFullDay = new Label();
                        lblFullDay.Text = AVAIL_FULLDAY;
                        lblFullDay.AutoSize = false;
                        lblFullDay.Dock = DockStyle.Fill;
                        lblFullDay.TextAlign = ContentAlignment.MiddleCenter;
                        pnlDetail.BackColor = Color.LightGreen;
                        pnlDetail.Controls.Add(lblFullDay);
                        break;
                    case Availablities.Partly:
                        foreach (UserWeeklyAvailability avail in avails.OrderByDescending(x => x.StartHour)) {
                            UCTimeBox timeBlock = new UCTimeBox(avail.Id, 
                                string.Format("{0}-{1}", 
                                    DateTimeHelper.DoubleHourToTime((double)avail.StartHour, "h:mmtt"),
                                    DateTimeHelper.DoubleHourToTime((double)avail.EndHour, "h:mmtt")));
                            timeBlock.Dock = DockStyle.Top;
                            timeBlock.OnDelete += timeBlock_OnDelete;

                            pnlDetail.Controls.Add(timeBlock);
                        }
                        break;
                    case Availablities.Off:
                        Label lblOff = new Label();
                        lblOff.Text = AVAIL_OFF;
                        lblOff.AutoSize = false;
                        lblOff.Dock = DockStyle.Fill;
                        lblOff.TextAlign = ContentAlignment.MiddleCenter;
                        pnlDetail.BackColor = Color.FromArgb(230, 230, 230);
                        pnlDetail.Controls.Add(lblOff);
                        break;
                }

                tblWeekly.Controls.Add(pnlDetail, col, 2);
            }

            tblWeekly.ResumeLayout();

        }

        /// <summary>
        /// Switch between "Practitioner List" and "Add Time Off" in the left panel when different tab has been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabUserSchedule_Selected(object sender, TabControlEventArgs e) {
            if (e.TabPageIndex == 0) {//WeeklyAvailability
                lstUsers.Dock = DockStyle.Fill;
                lstUsers.Visible = true;

                pnlAddDayOff.Visible = false;
            } else if (e.TabPageIndex == 1) {//TimeOff
                pnlAddDayOff.Dock = DockStyle.Fill;
                pnlAddDayOff.Visible = true;

                lstUsers.Visible = false;
            }
        }

        /// <summary>
        /// Refresh the year dropdownlist based on the current selected year.
        /// Maximum 11 years will be listed.
        /// </summary>
        /// <param name="curYear"></param>
        private void refreshYear(int curYear) {
            drpYear.Items.Clear();

            int minYear = Math.Max(1900, curYear - 5);
            int maxYear = minYear + 10;

            for (int i = minYear, ind = 0; i <= maxYear; i++, ind++) {
                drpYear.Items.Add(new SimpleListItem {
                    Id = i,
                    Text = i.ToString()
                });

                if (curYear == i) {

                    //Disable index changed event
                    drpYear.SelectedIndexChanged -= drpYear_SelectedIndexChanged;
                    
                    drpYear.SelectedIndex = ind;

                    //Enable index changed event
                    drpYear.SelectedIndexChanged += drpYear_SelectedIndexChanged;
                }
            }
        }

        private void displayTimeOffs() {

            //1) Get current month
            DateTime from = new DateTime((drpYear.SelectedItem as SimpleListItem).Id,
                (drpMonth.SelectedItem as SimpleListItem).Id,
                1);
            DateTime to = from.AddMonths(1);

            //2) Retrieve all time offs for the current month
            IList<UserTimeOff> timeoffs = null;

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    timeoffs = session.QueryOver<UserTimeOff>()
                        .Fetch(x => x.Practitioner).Eager
                        .Where(x => 
                                (x.StartTime >= from && x.EndTime <= to) ||
                                (x.StartTime < from && x.EndTime > from) ||
                                (x.StartTime < to && x.EndTime > to)
                            )
                        .List<UserTimeOff>();

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to add time off. " + ex.Message);
                    Program.log.Error("Failed to add time off.", ex);
                }
            }

            //3) Display
            tblTimeOffs.SuspendLayout();

            //3.1) Get the first date on the very first table cell
            DateTime dt = from.AddDays(-Convert.ToInt32(from.DayOfWeek));
            for (int row = 2; row <= 7; row++) {

                //Stop if the first date on this row is already in the next month
                if (dt >= to) {
                    break;
                }

                for (int col = 0; col <= 6; col++, dt = dt.AddDays(1)) {

                    Panel pnl = new Panel();
                    pnl.Dock = DockStyle.Fill;
                    pnl.Margin = new Padding(1);
                    pnl.AutoScroll = true;

                    //Remove existing controls first
                    tblTimeOffs.Controls.Remove(tblTimeOffs.GetControlFromPosition(col, row));

                    //Add date
                    Label lblDate = new Label();
                    lblDate.Text = dt.Day.ToString();
                    lblDate.Dock = DockStyle.Bottom;
                    pnl.Controls.Add(lblDate);

                    //Set gray background for holidays and pre/next month days
                    if (CanadaHolidays.IsHoliday(dt)) {
                        Label lblHoliday = new Label();
                        lblHoliday.Text = CanadaHolidays.GetHolidayBy(dt).HolidayName;
                        lblHoliday.Dock = DockStyle.Fill;
                        lblHoliday.AutoSize = false;
                        lblHoliday.Dock = DockStyle.Fill;
                        lblHoliday.TextAlign = ContentAlignment.MiddleCenter;
                        pnl.Controls.Add(lblHoliday);
                    }
                    if (CanadaHolidays.IsHoliday(dt) ||
                        dt.Month != from.Month) {
                            
                        pnl.BackColor = Color.FromArgb(230, 230, 230);
                        tblTimeOffs.Controls.Add(pnl, col, row);   
                        continue;
                    }

                    //Add time offs to table cell
                    IEnumerable<UserTimeOff> timeoffsInCell = timeoffs.Where(x => 
                            (x.StartTime >= dt && x.EndTime <= dt.AddDays(1)) ||
                            (x.StartTime < dt && x.EndTime > dt) ||
                            (x.StartTime < dt.AddDays(1) && x.EndTime > dt.AddDays(1))
                        );
                    if(timeoffsInCell.Count() > 0){
                        foreach (UserTimeOff timeoff in timeoffsInCell) {
                            string timeoffText = string.Empty;
                            if (timeoff.StartTime == timeoff.StartTime.Date && timeoff.EndTime == timeoff.EndTime.Date) {
                                timeoffText = "Full Day";
                            } else if (timeoff.StartTime == timeoff.StartTime.Date) {
                                timeoffText = string.Format("until {0:h:mmtt}", timeoff.EndTime);
                            } else if (timeoff.EndTime == timeoff.EndTime.Date) {
                                timeoffText = string.Format("from {0:h:mmtt}", timeoff.StartTime);
                            } else {
                                timeoffText = string.Format("{0:h:mmtt} to {1:h:mmtt}", timeoff.StartTime, timeoff.EndTime);
                            }
                            UCTimeBox timeoffBox = new UCTimeBox(timeoff.Id,
                                string.Format("{0} [{1}]", timeoff.Practitioner.DisplayName, timeoffText));
                            timeoffBox.OnDelete += timeoffBox_OnDelete;
                            timeoffBox.Dock = DockStyle.Top;
                            pnl.Controls.Add(timeoffBox);
                        }
                    }

                    tblTimeOffs.Controls.Add(pnl, col, row);
                }
            }

            tblTimeOffs.ResumeLayout();
        }

        private void timeBlock_OnDelete(object sender, EventArgs e) {

            UCTimeBox uc = sender as UCTimeBox;

            if (uc == null) return;

            //Get weekly availability Id
            int id = uc.Id;

            if (id <= 0) return;

            if (DialogResult.OK != MessageBox.Show(this, "Press [OK] to confirm you want to DELETE this working time frame.", "Confirm to DELETE", MessageBoxButtons.OKCancel)) {
                return;
            }

            //Remove weekly availability from DB
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    UserWeeklyAvailability availToDelete = session.Get<UserWeeklyAvailability>(id);
                    if (availToDelete != null) {
                        availToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                        availToDelete.UpdatedTime = DateTime.Now;

                        session.Delete(availToDelete);
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to delete weekly availability. " + ex.Message);
                    Program.log.Error("Failed to delete weekly availability.", ex);
                }
            }

            //Refresh right panel
            tblWeekly.SuspendLayout();

            uc.Parent.Controls.Remove(uc);

            tblWeekly.ResumeLayout();
        }

        private void timeoffBox_OnDelete(object sender, EventArgs e) {

            UCTimeBox uc = sender as UCTimeBox;

            if (uc == null) return;

            //Get Time Off Id
            int id = uc.Id;

            if (id <= 0) return;

            if (DialogResult.OK != MessageBox.Show(this, "Press [OK] to confirm you want to DELETE this time off.", "Confirm to DELETE", MessageBoxButtons.OKCancel)) {
                return;
            }

            //Remove time off from DB
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    UserTimeOff timeoffToDelete = session.Get<UserTimeOff>(id);
                    if (timeoffToDelete != null) {
                        timeoffToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                        timeoffToDelete.UpdatedTime = DateTime.Now;

                        session.Delete(timeoffToDelete);
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to delete time off. " + ex.Message);
                    Program.log.Error("Failed to delete time off.", ex);
                }
            }

            //Refresh right time off panel
            displayTimeOffs();
        }

        private void drpYear_SelectedIndexChanged(object sender, EventArgs e) {

            //Get the selected year
            int curYear = (drpYear.SelectedItem as SimpleListItem).Id;

            //Refresh year list
            refreshYear(curYear);

            displayTimeOffs();
        }

        private void drpMonth_SelectedIndexChanged(object sender, EventArgs e) {
            displayTimeOffs();
        }

        private void btnPreMonth_Click(object sender, EventArgs e) {
            if (drpMonth.SelectedIndex > 0) {
                drpMonth.SelectedIndex--;
            } else {
                //Move to the previous year
                drpYear.SelectedIndexChanged -= drpYear_SelectedIndexChanged;
                drpYear.SelectedIndex--;
                drpYear.SelectedIndexChanged += drpYear_SelectedIndexChanged;

                //Move to the last month
                drpMonth.SelectedIndex = drpMonth.Items.Count - 1;
            }
        }

        private void btnNextMonth_Click(object sender, EventArgs e) {
            if (drpMonth.SelectedIndex < drpMonth.Items.Count - 1) {
                drpMonth.SelectedIndex++;
            } else {
                //Move to the next year
                drpYear.SelectedIndexChanged -= drpYear_SelectedIndexChanged;
                drpYear.SelectedIndex++;
                drpYear.SelectedIndexChanged += drpYear_SelectedIndexChanged;

                //Move to the first month
                drpMonth.SelectedIndex = 0;
            }
        }

        private void btnAddDayOff_Click(object sender, EventArgs e) {

            //Validate user input
            if (drpUser.SelectedItem == null) {
                MessageBox.Show(this, "Please select the Practitioner.");
                return;
            }
            DateTime from = dtFrom.Value;
            DateTime to = chkFullDay.Checked ? dtTo.Value.AddDays(1) : dtTo.Value;
            if (from >= to) {
                MessageBox.Show(this, "To date must be greater than From date.");
                return;
            }

            //Get selected practitioner id
            int pracId = (drpUser.SelectedItem as SimpleListItem).Id;

            //Get reason
            string reason = txtReason.Text.Trim();

            //Insert into DB
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //If the time off overlap multiple days, insert one record for reach day
                    DateTime dayStart = from;
                    DateTime dayEnd = (dayStart.Date.AddDays(1) > to ? to : dayStart.Date.AddDays(1));
                    while (true) {

                        //No need to insert timeoff if it's Holiday
                        if (!CanadaHolidays.IsHoliday(dayStart)) {

                            UserTimeOff timeoff = new UserTimeOff {
                                Practitioner = session.Get<User>(pracId),
                                StartTime = dayStart,
                                EndTime = dayEnd,
                                Reason = reason,
                                IsTestData = Program.LogonUser.IsTestData,
                                CreatedBy = Program.LogonUser.DisplayName,
                                CreatedTime = DateTime.Now
                            };

                            session.Save(timeoff);
                        }

                        if (dayEnd >= to) {
                            break;
                        } else {
                            dayStart = dayEnd;
                            dayEnd = (dayStart.Date.AddDays(1) > to ? to : dayStart.Date.AddDays(1));
                        }
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to add time off. " + ex.Message);
                    Program.log.Error("Failed to add time off.", ex);
                }
            }

            //Update right panel
            displayTimeOffs();
        }

        private void chkFullDay_CheckedChanged(object sender, EventArgs e) {
            if (chkFullDay.Checked) {
                dtFrom.CustomFormat = "ddd, MMM dd, yyyy";
                dtFrom.Value = new DateTime(dtFrom.Value.Year, dtFrom.Value.Month, dtFrom.Value.Day);
                dtTo.CustomFormat = "ddd, MMM dd, yyyy";
                dtTo.Value = new DateTime(dtTo.Value.Year, dtTo.Value.Month, dtTo.Value.Day);
            } else {
                dtFrom.CustomFormat = "ddd, MMM dd, yyyy hh:mm tt";
                dtTo.CustomFormat = "ddd, MMM dd, yyyy hh:mm tt";
            }
        }

        private void btnThisMonth_Click(object sender, EventArgs e) {
            //Set year
            refreshYear(DateTime.Now.Year);
            //Set month
            drpMonth.SelectedIndex = DateTime.Now.Month - 1;
        }
    }
}
