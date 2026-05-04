//using Google.Apis.Calendar.v3.Data;
using EHD.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public static class DoctorCalendarHelper {

        /// <summary>
        /// Check if the doctor is available
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="calendarId"></param>
        /// <param name="dateToCheck"></param>
        /// <returns>Return true when the doctor is available or the user decided to continue anyways.</returns>
        public static bool IsDoctorAvailable(IWin32Window handle, string doctorName, string calendarId, DateTime dateToCheck) {
            bool isAvailable = true;

            #region Check if the doctor is available through the Google Calendar
            /*
            if (Program.config.EnableGoogleCalendar && Program._calendar != null) {

                //Check if the doctor's Google Calendar Id has been setup
                if (calendarId == null || calendarId.Trim().Length <= 0) {

                    if (DialogResult.Yes == MessageBox.Show(handle,
                        Constants.WARNING_GOOGLE_CALENDAR_ID_MISSING,
                        "Doctor's Google Calendar Id is missing",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning)) {

                        isAvailable = true;
                    } else {
                        isAvailable = false;
                    }
                } else {//Check if the doctor is available on the given date

                    try {
                        IList<Event> events = Program._calendar.GetCalendarEvents(
                            calendarId,
                            dateToCheck.Date,
                            dateToCheck.Date.AddDays(1));

                        if (events != null && events.Count > 0) {

                            //Initial schedule dialog
                            CalendarScheduleDialog dlg = new CalendarScheduleDialog();
                            dlg.SetTitle(doctorName, dateToCheck);

                            foreach (Event evt in events) {
                                string startTime = (evt.Start.DateTime != null ? evt.Start.DateTime : evt.Start.Date);
                                string endTime = (evt.End.DateTime != null ? evt.End.DateTime : evt.End.Date);

                                DateTime? dtStart = Program._calendar.GetLocalTime(startTime);
                                if (dtStart.HasValue) {
                                    //startTime = dtStart.Value.ToString("ddd, MMM dd, yyyy HH:mm tt");
                                    startTime = dtStart.Value.ToString("HH:mm tt");
                                }
                                DateTime? dtEnd = Program._calendar.GetLocalTime(endTime);
                                if (dtEnd.HasValue) {
                                    //endTime = dtEnd.Value.ToString("ddd, MMM dd, yyyy HH:mm tt");
                                    endTime = dtEnd.Value.ToString("HH:mm tt");
                                }

                                dlg.AddEvent(startTime, endTime, evt.Summary, evt.Description);
                            }

                            if (DialogResult.OK == dlg.ShowDialog(handle)) {
                                isAvailable = true;
                            } else {
                                isAvailable = false;
                            }
                        }
                    } catch (Exception ex) {
                        MessageBox.Show(handle,
                            "Google Calendar Error:\n" + ex.Message,
                            "Google Calendar Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        isAvailable = false;
                    }
                }
            }*/
            #endregion

            return isAvailable;
        }

        /// <summary>
        /// Try to add the assignment/invoice to the doctor's calendar
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="calendarId"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns>Return true is succeeded or user decided to continue anyways.</returns>
        public static bool AddToCalendar(IWin32Window handle, string calendarId, 
            DateTime dateStart, DateTime dateEnd, string title, string description) {

                bool toContinue = true;

            #region Check if the doctor is available through the Google Calendar
            /*
            if (Program.config.EnableGoogleCalendar && Program._calendar != null) {

                //Check if the doctor's Google Calendar Id has been setup
                if (calendarId == null || calendarId.Trim().Length <= 0) {

                    if (DialogResult.Yes == MessageBox.Show(handle,
                        Constants.WARNING_GOOGLE_CALENDAR_ID_MISSING,
                        "Doctor's Google Calendar Id is missing",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning)) {

                        toContinue = true;
                    } else {
                        toContinue = false;
                    }
                } else {
                    try {
                        Event evt = new Event();

                        evt.Summary = title;
                        evt.Description = description;
                        evt.Location = "Clinic";
                        evt.Start = new EventDateTime { DateTime = dateStart.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss-00:00") };
                        evt.End = new EventDateTime { DateTime = dateEnd.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss-00:00") };

                        Program._calendar.AddCalendarEvent(calendarId, evt);

                    } catch (Exception ex) {
                        MessageBox.Show(handle,
                            "Google Calendar Error:\n" + ex.Message,
                            "Google Calendar Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        toContinue = false;
                    }
                }
            }*/
            #endregion

            return toContinue;
        }
    }
}
