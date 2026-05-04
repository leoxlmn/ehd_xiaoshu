using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;

namespace EHD.Admin {
    public partial class UCAddTimeBlock : UserControl {

        public DayOfWeek WeekDay { get; set; }

        public event EventHandler OnAdd;

        public decimal StartHour {
            get { return Convert.ToDecimal(dtFrom.Value.Hour) + Convert.ToDecimal(dtFrom.Value.Minute) / 60; }
            set {
                DateTime dt = DateTimeHelper.DoubleHourToTime((double)value);
                dtFrom.Value = dtFrom.Value.Date.AddHours(dt.Hour).AddMinutes(dt.Minute);
            }
        }

        public decimal EndHour {
            get { return Convert.ToDecimal(dtTo.Value.Hour) + Convert.ToDecimal(dtTo.Value.Minute) / 60; }
            set {
                DateTime dt = DateTimeHelper.DoubleHourToTime((double)value);
                dtTo.Value = dtTo.Value.Date.AddHours(dt.Hour).AddMinutes(dt.Minute);
            }
        }

        public UCAddTimeBlock(DayOfWeek WeekDay) {
            InitializeComponent();

            this.WeekDay = WeekDay;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (OnAdd != null) {
                OnAdd.Invoke(this, e);
            }
        }
    }
}
