using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class UCTimeBox : UserControl {

        public event EventHandler OnDelete;
        public event EventHandler onEdit;

        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string DisplayedText {
            get {
                return lblText.Text;
            }
        }
        public string DetailedText {get;set;}

        private Timer _timer = null;

        public UCTimeBox(int TimeId, string DisplayText) : this(TimeId, DisplayText, DisplayText) {
        }

        public UCTimeBox(int TimeId, string DisplayText, string DetailedText) {
            InitializeComponent();

            Id = TimeId;
            lblText.Text = DisplayText;
            this.DetailedText = DetailedText;
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (OnDelete != null) {
                OnDelete.Invoke(this, e);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if (onEdit != null) {
                onEdit.Invoke(this, e);
            }
        }

        private void lblText_MouseHover(object sender, EventArgs e) {
            btnEdit.Visible = (onEdit != null);
            btnDelete.Visible = (OnDelete != null);

            //Use timer to hide buttons
            if (_timer == null) {
                _timer = new Timer();
                _timer.Interval = 4000;
                _timer.Tick += _timer_Tick;
            }
            _timer.Start();

        }

        void _timer_Tick(object sender, EventArgs e) {
            _timer.Stop();
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }

        private void lblText_Layout(object sender, LayoutEventArgs e) {
            toolTip1.SetToolTip(lblText, DetailedText);
        }

    }
}
