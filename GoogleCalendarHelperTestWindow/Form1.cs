using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Calendar.v3.Data;

namespace GoogleCalendarHelperTestWindow {
    public partial class Form1 : Form {

        private GoogleCalendarHelper.CalendarAPI calendar = new GoogleCalendarHelper.CalendarAPI();

        public Form1() {
            InitializeComponent();
        }

        private void btnListEvents_Click(object sender, EventArgs e) {
            DateTime? dtFrom = null;
            DateTime? dtTo = null;
            if(dtpGetFrom.Checked) dtFrom = dtpGetFrom.Value;
            if(dtpGetTo.Checked) dtTo = dtpGetTo.Value;

            IList<Event> events = calendar.GetCalendarEvents(txtCalendarId.Text, dtFrom, dtTo);

            grdEventList.DataSource = events;
        }

        private void btnAddEvent_Click(object sender, EventArgs e) {
            Event newEvent = new Event();
            newEvent.Summary = txtNewTitle.Text;
            newEvent.Description = txtNewDescription.Text;
            newEvent.Location = txtNewLocation.Text;
            newEvent.Start = new EventDateTime { DateTime = dtpNewFrom.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss-00:00") };
            newEvent.End = new EventDateTime { DateTime = dtpNewTo.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss-00:00") };

            calendar.AddCalendarEvent(txtCalendarId.Text, newEvent);
        }
    }
}
