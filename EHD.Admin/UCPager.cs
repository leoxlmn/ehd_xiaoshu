using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {

    public delegate void GoToPageEventHandler(int pageNumber);

    public partial class UCPager : UserControl {

        public UCPager() {
            InitializeComponent();
        }

        /*
        public event EventHandler FirstEvent;
        public event EventHandler LastEvent;
        public event EventHandler PreviousEvent;
        public event EventHandler NextEvent;
        public event EventHandler PageEvent;*/

        //Page navigate event handler
        public GoToPageEventHandler GoToPageEvent;

        //Current page number
        public int Page {
            get {
                int page = 1;
                if (int.TryParse(txtPage.Text, out page)) {
                    return page;
                } else {
                    return 1;
                }
            }
            set {
                txtPage.Text = value.ToString();
            }
        }

        //Page size
        public int PageSize {
            get {
                return Convert.ToInt32(numPageSize.Value);
            }
            set {
                numPageSize.Value = value;
            }
        }

        //Total pages
        private int pageTotal = 0;
        public int TotalPage {
            get {
                return pageTotal;
            }
            set {
                pageTotal = value;
                lblPageTotal.Text = pageTotal.ToString();
            }
        }

        //Navigate to the current page
        public void Navigate() {
            if (GoToPageEvent != null) {
                this.Invoke(GoToPageEvent, new object[] { Page });
            }
        }

        private void btnFirst_Click(object sender, EventArgs e) {
            Page = 1;
            Navigate();
        }

        private void btnPrev_Click(object sender, EventArgs e) {
            if (Page > 1) {
                Page--;
            }
            Navigate();
        }

        private void btnNext_Click(object sender, EventArgs e) {
            if (Page < TotalPage) {
                Page++;
            }
            Navigate();
        }

        private void btnLast_Click(object sender, EventArgs e) {
            Page = TotalPage;
            Navigate();
        }

        private void txtPage_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                if (txtPage.Text.Length > 0) {
                    int pageNumber = 1;
                    if (!int.TryParse(txtPage.Text, out pageNumber)) {
                        pageNumber = 1;
                    }

                    Page = pageNumber;
                    Navigate();
                }
            }
        }

        private void numPageSize_ValueChanged(object sender, EventArgs e) {
            Page = 1;
            Navigate();
        }
    }
}
