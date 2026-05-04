using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class CalendarScheduleDialog : Form {

        private DataTable dt;

        public CalendarScheduleDialog() {
            InitializeComponent();

            dt = new DataTable();
            dt.Columns.Add("Start Time");
            dt.Columns.Add("End Time");
            dt.Columns.Add("Event Summary");
            dt.Columns.Add("Event Details");
        }

        public void SetTitle(string doctorName, DateTime date) {
            lblScheduleTitle.Text = string.Format("{0} on {1:ddd, MMM dd, yyyy}", 
                doctorName, date);
        }

        public void AddEvent(string startTime, string endTime, string summary, string details) {
            dt.Rows.Add(new string[] { startTime, endTime, summary, details });
        }

        private void btnOK_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void CalendarScheduleDialog_Load(object sender, EventArgs e) {
            grdSchedule.DataSource = dt.DefaultView;
        }
    }
}
