using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class UCAutoCompleteTextBox : UserControl {

        private ListBox _popupList;
        private Form _popupForm;
        private Timer _timer;

        public event EventHandler MinInputLengthReached;
        public uint ActionDelayMilliseconds { get; set; }
        public uint MinInputLengthToStartSearch { get; set; }
        public string Tooltip { get; set; }

        public string InputText {
            get {
                return txtInput.Text;
            }
            set {
                txtInput.Text = value;
            }
        }

        private SimpleListItem _selectedItem = null;
        public SimpleListItem SelectedItem {
            get {
                return _selectedItem;
            }
            set {
                _selectedItem = value;
                txtInput.TextChanged -= txtInput_TextChanged;
                if (value != null) {
                    txtInput.Text = _selectedItem.Text;
                } else {
                    txtInput.Text = string.Empty;
                }
                txtInput.TextChanged += txtInput_TextChanged;
            }
        }

        public UCAutoCompleteTextBox() {
            InitializeComponent();

            _popupList = new ListBox();
            _popupList.Dock = DockStyle.Fill;
            _popupList.HorizontalScrollbar = true;
            _popupList.SelectedIndexChanged += _popupList_SelectedIndexChanged;

            _popupForm = new Form();
            _popupForm.ControlBox = false;
            _popupForm.FormBorderStyle = FormBorderStyle.None;
            _popupForm.ShowIcon = false;
            _popupForm.ShowInTaskbar = false;
            _popupForm.TopMost = true;
            _popupForm.SizeGripStyle = SizeGripStyle.Hide;
            _popupForm.Size = new Size((this.ClientSize.Width > 100 ? this.ClientSize.Width : 100), 200);
            _popupForm.StartPosition = FormStartPosition.Manual;
            _popupForm.Location = this.PointToScreen(new Point(txtInput.Left, txtInput.Top + txtInput.Height));

            _popupForm.Controls.Add(_popupList);
        }

        void _timer_Tick(object sender, EventArgs e) {
            _timer.Stop();
            if (txtInput.Text.Length >= MinInputLengthToStartSearch && MinInputLengthReached != null) {
                MinInputLengthReached.Invoke(sender, e);
            }
        }

        public void UpdateList(IList<SimpleListItem> items) {

            _popupList.SelectedIndexChanged -= _popupList_SelectedIndexChanged;

            _popupList.DataSource = items;
            _popupList.ValueMember = "Id";
            _popupList.DisplayMember = "Text";
            if (_popupList.Items.Count > 0) {
                _popupForm.Location = this.PointToScreen(new Point(txtInput.Left, txtInput.Top + txtInput.Height));
                if (!_popupForm.Visible) _popupForm.Show(this);
                txtInput.Focus();
            } else if (_popupForm.Visible) {
                _popupForm.Hide();
            }

            _popupList.SelectedIndexChanged += _popupList_SelectedIndexChanged;
        }

        void _popupList_SelectedIndexChanged(object sender, EventArgs e) {
            _selectedItem = _popupList.SelectedItem as SimpleListItem;
            _popupForm.Hide();

            if (_selectedItem != null) {
                txtInput.TextChanged -= txtInput_TextChanged;
                txtInput.Text = _selectedItem.Text;
                txtInput.TextChanged += txtInput_TextChanged;
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e) {

            //Clear SelectedItem
            _selectedItem = null;

            if (txtInput.Text.Length < MinInputLengthToStartSearch) {
                _popupForm.Hide();
            }

            //Create timer if needed
            if (_timer == null && this.ActionDelayMilliseconds > 0) {
                _timer = new Timer();
                _timer.Interval = (int)this.ActionDelayMilliseconds;
                _timer.Tick += _timer_Tick;
            }

            if (_timer != null) {
                _timer.Stop();
                _timer.Start();
            } else {
                if (txtInput.Text.Length >= MinInputLengthToStartSearch && MinInputLengthReached != null) {
                    MinInputLengthReached.Invoke(sender, e);
                }
            }

        }

        private void txtInput_Leave(object sender, EventArgs e) {
            _popupForm.Hide();
        }

        private void txtInput_Enter(object sender, EventArgs e) {

            if (this.Tooltip != null && this.Tooltip.Trim().Length > 0) {
                ToolTip tt = new ToolTip();
                tt.Show(this.Tooltip, txtInput, 0, -txtInput.ClientSize.Height, 5000);
            }

            if (_popupList.Items.Count > 0) {
                _popupForm.Location = this.PointToScreen(new Point(txtInput.Left, txtInput.Top + txtInput.Height));
                if (!_popupForm.Visible) _popupForm.Show(this);
                txtInput.Focus();
            }
        }

    }
}
