using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JCManagement.LoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Public properties
        public bool Success { get; set; }
        public string LoginErrorMessage { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Success = false;
            LoginErrorMessage = "Login cancelled.";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Success = false;
            LoginErrorMessage = "Login cancelled.";
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var Username = txtUsername.Text.Trim();
            var Password = txtPassword.Text;

            //TO DO:Verity login and set return values
            Success = true;
            LoginErrorMessage = "";
        }
    }
}
