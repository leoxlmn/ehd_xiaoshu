using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class MessageBoxEx : Form {
        public MessageBoxEx(string message, string caption) {
            InitializeComponent();

            this.txtMessage.Text = message;
            this.Text = caption;
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
