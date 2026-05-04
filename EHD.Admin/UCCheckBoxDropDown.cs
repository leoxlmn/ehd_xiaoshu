using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class UCCheckBoxDropDown : UserControl {

        private PopupForm _popup;

        public UCCheckBoxDropDown() {
            InitializeComponent();

            _popup = new PopupForm(txtSelectedText);
            _popup.OKEvent += new EventHandler(_popup_OKEvent);
            _popup.UpdateDataList += new EventHandler(_popup_UpdateDataList);
        }

        void _popup_UpdateDataList(object sender, EventArgs e) {
            if(UpdateDataList != null) UpdateDataList.Invoke(sender, e);
        }

        void _popup_OKEvent(object sender, EventArgs e) {
            if(CheckCompleted != null) CheckCompleted.Invoke(sender, null);
        }

        public TextBox TextBox {
            get { return txtSelectedText; }
        }

        public bool IsSelectAllChecked {
            get { return _popup.IsSelectAllChecked; }
        }

        public bool IsBlankChecked {
            get { return _popup.IsBlankChecked; }
        }

        public bool AllNonBlanks {
            get { return _popup.AllNonBlanks; }
        }

        public bool BlanksOnly {
            get { return _popup.BlanksOnly; }
        }

        public bool NoneChecked {
            get { return _popup.NoneChecked; }
        }

        public bool AllChecked {
            get {
                return _popup.AllChecked;
            }
        }

        public void AddItem(object item) {
            if(!_popup.CheckedListBox.Items.Contains(item))
                _popup.CheckedListBox.Items.Add(item);
        }

        public void AddItem(object item, bool isChecked) {
            if(!_popup.CheckedListBox.Items.Contains(item))
                _popup.CheckedListBox.Items.Add(item, isChecked);
        }

        public void AddItem(object item, CheckState state) {
            if(!_popup.CheckedListBox.Items.Contains(item))
                _popup.CheckedListBox.Items.Add(item, state);
        }

        public void UpdateListItems<T>(IList<T> UpdatedItems) {
            //Remove items that have been deleted
            for(int i=_popup.CheckedListBox.Items.Count-1; i>=0; i--){

                if(_popup.CheckedListBox.Items[i] is T) {
                    T item = (T)_popup.CheckedListBox.Items[i];
                    if(!UpdatedItems.Contains(item)) {
                        _popup.CheckedListBox.Items.RemoveAt(i);
                    } else {
                        //Update item
                        T updateItem = UpdatedItems[UpdatedItems.IndexOf(item)];
                        bool itemChecked = _popup.CheckedListBox.CheckedItems.Contains(item);
                        _popup.CheckedListBox.Items.RemoveAt(i);
                        _popup.CheckedListBox.Items.Insert(i, updateItem);
                        if (itemChecked) _popup.CheckedListBox.SetItemChecked(i, true);
                    }
                }
            }

            //Add new items
            foreach(T newItem in UpdatedItems) {
                if(!_popup.CheckedListBox.Items.Contains(newItem)) {
                    _popup.CheckedListBox.Items.Add(newItem);
                }
            }
        }

        public IEnumerable<object> CheckedItems {
            get {
                return _popup.CheckedItems;
            }
        }

        public event EventHandler CheckCompleted;
        public event EventHandler UpdateDataList;

        private void txtSelectedText_Click(object sender, EventArgs e) {
            Point cornerPoint = this.PointToScreen(new Point(txtSelectedText.Left, txtSelectedText.Top + txtSelectedText.Height));
            _popup.Location = cornerPoint;
            _popup.ShowDialog(this.Parent);
        }
    }
}
