using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class MessageInputForm : Form {

        public string InputText {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        public MessageInputForm(string Caption = "Enter message:" ) {
            InitializeComponent();

            lblCaption.Text = Caption;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
