using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class PrintRangeForm : Form {

        public DateTime FromDateTime {
            get {
                return new DateTime(dtFrom.Value.Year, dtFrom.Value.Month, dtFrom.Value.Day, timeFrom.Value.Hour, timeFrom.Value.Minute, 0);
            }
            set {
                dtFrom.Value = value;
                timeFrom.Value = value;
            }
        }

        public DateTime ToDateTime {
            get {
                return new DateTime(dtTo.Value.Year, dtTo.Value.Month, dtTo.Value.Day, timeTo.Value.Hour, timeTo.Value.Minute, 0);
            }
            set {
                dtTo.Value = value;
                timeTo.Value = value;
            }
        }

        public bool ToPrintAll { get; set; }

        public PrintRangeForm() {
            InitializeComponent();
        }

        private void btnPrintRange_Click(object sender, EventArgs e) {
            ToPrintAll = false;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnPrintAll_Click(object sender, EventArgs e) {
            ToPrintAll = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
