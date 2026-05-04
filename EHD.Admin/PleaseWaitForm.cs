using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class PleaseWaitForm : Form {
        public delegate void SetProgressDelegate(string msg, double percent);
        public event EventHandler ProcessCheckHandler;

        private void _setProgressBarValue(string msg, double percent) {
            lblMessage.Text = msg;
            int val = Convert.ToInt32(percent * 100);
            pg.Value = (val >= 100 ? 100 : val >= 0 ? val : 0);
        }

        public PleaseWaitForm() {
            InitializeComponent();

            pg.Minimum = 0;
            pg.Maximum = 100;
            lblMessage.Text = "";
        }

        public void SetProgress(string msg, double percent) {
            SetProgressDelegate d = new SetProgressDelegate(_setProgressBarValue);
            this.Invoke(d, new object []{ msg, percent });
        }

        private void timer_Tick(object sender, EventArgs e) {
            if (ProcessCheckHandler != null) {
                this.Invoke(ProcessCheckHandler);
            }
        }
    }
}
