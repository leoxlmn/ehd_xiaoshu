using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class PopupForm : Form {

        private TextBox _textBox;
        public event EventHandler OKEvent;
        public event EventHandler UpdateDataList;
        private const string SELECT_ALL = "(Select All)";
        private const string BLANKS = "(Blanks)";
        private const int MAX_HEIGHT = 400;

        private double _listItemHeight = 0;

        public PopupForm(TextBox TriggerTextBox) {
            InitializeComponent();

            _textBox = TriggerTextBox;
            initiateList();

            if(_textBox == null) throw new ArgumentException("TriggerTextBox cannot be null.");
        }

        private void initiateList() {
            lstItem.Items.Clear();
            lstItem.Items.Add(SELECT_ALL, false);
            lstItem.Items.Add(BLANKS, true);
        }

        public IEnumerable<object> CheckedItems {
            get {
                List<object> checkedItems = new List<object>();
                foreach(object item in lstItem.CheckedItems) {
                    if(item is string && item.ToString().Equals(SELECT_ALL)) continue;
                    checkedItems.Add(item);
                }
                return checkedItems;
            }
        }

        public CheckedListBox CheckedListBox {
            get {
                return lstItem;
            }
        }

        public bool IsSelectAllChecked {
            get {
                foreach(object checkedItem in lstItem.CheckedItems) {
                    if(checkedItem.ToString().Equals(SELECT_ALL)) return true;
                }
                return false;
            }
        }

        public bool IsBlankChecked{
            get{
                foreach(object checkedItem in lstItem.CheckedItems) {
                    if(checkedItem.ToString().Equals(BLANKS)) return true;
                }
                return false;
            }
        }

        public bool AllNonBlanks {
            get {
                if(IsSelectAllChecked) {
                    return (!IsBlankChecked && lstItem.CheckedItems.Count + 1 >= lstItem.Items.Count);
                } else {
                    return (!IsBlankChecked && lstItem.CheckedItems.Count + 2 >= lstItem.Items.Count);
                }
            }
        }

        public bool BlanksOnly {
            get {
                if(IsSelectAllChecked) {
                    return (IsBlankChecked && lstItem.CheckedItems.Count == 2);
                } else {
                    return (IsBlankChecked && lstItem.CheckedItems.Count == 1);
                }
            }
        }

        public bool NoneChecked {
            get {
                if(IsSelectAllChecked) {
                    return (lstItem.CheckedItems.Count == 1);
                } else {
                    return (lstItem.CheckedItems.Count < 1);
                }
            }
        }

        public bool AllChecked {
            get {
                if(IsSelectAllChecked) {
                    return (lstItem.CheckedItems.Count >= lstItem.Items.Count);
                } else {
                    return (lstItem.CheckedItems.Count + 1 >= lstItem.Items.Count);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if(OKEvent != null) {
                OKEvent.Invoke(sender, e);
                this.Hide();
            }
        }

        private void lstItem_ItemCheck(object sender, ItemCheckEventArgs e) {
            CheckedListBox lst = sender as CheckedListBox;
            string selectAll = lst.Items[e.Index] as string;
            if(selectAll != null && selectAll.Equals(SELECT_ALL)) {
                if(e.NewValue == CheckState.Checked) {
                    //Check all items
                    for(int i = 1; i < lst.Items.Count; i++) {
                        lst.SetItemChecked(i, true);
                    }
                } else if(e.NewValue == CheckState.Unchecked) {
                    //Uncheck all items
                    for(int i = 1; i < lst.Items.Count; i++) {
                        lst.SetItemChecked(i, false);
                    }
                }
            } else {
                lst.SetItemCheckState(0, CheckState.Indeterminate);
                _textBox.Text = string.Empty;
                string newCheckedItem = lst.Items[e.Index].ToString();
                foreach(object checkedItem in lst.CheckedItems) {
                    if(checkedItem.ToString().Equals(SELECT_ALL)) continue;
                    if(checkedItem.ToString().Equals(newCheckedItem) && e.NewValue == CheckState.Unchecked) continue;
                    _textBox.Text += (checkedItem.ToString() + " + ");
                }
                if(e.NewValue == CheckState.Checked) {
                    _textBox.Text += (lst.Items[e.Index].ToString() + " + ");
                }
                if(_textBox.Text.Length > 0) {
                    _textBox.Text = _textBox.Text.Substring(0, _textBox.Text.Length - 3);
                }
            }

        }

        private void PopupForm_Load(object sender, EventArgs e) {

            if(UpdateDataList != null) {
                UpdateDataList.Invoke(null, null);
            }
            
            //Resize popup form height
            if(_listItemHeight <= 0) {
                _listItemHeight = (double)lstItem.PreferredHeight / lstItem.Items.Count;
            }

            int preferredHeight = Convert.ToInt32(Math.Ceiling(_listItemHeight * (lstItem.Items.Count + 1)))
                + SystemInformation.HorizontalScrollBarHeight;
            int deltaHeight = preferredHeight - lstItem.Height;

            if(this.Height + deltaHeight <= MAX_HEIGHT) {
                this.Height += deltaHeight;
                lstItem.Height += deltaHeight;
            } else {
                deltaHeight = MAX_HEIGHT - this.Height;
                this.Height = MAX_HEIGHT;
                lstItem.Height += deltaHeight;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Hide();
        }
    }
}
