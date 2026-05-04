using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;

namespace EHD.Login
{
    public delegate User LoginActionHandler(string Username, string Password, string newPassword);
    public delegate void HotKeyHandler();

    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        //Public properties
        public bool Success { get; set; }
        public string LoginErrorMessage { get; set; }
        public User LogonUser { get; set; }
        public LoginActionHandler LoginAction { get; set; }
        public HotKeyHandler HotKeyAction { get; set; }

        private int loginTry { get; set; }

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
            var NewPassword = txtNewPassword.Text;
            var ConfirmNewPassword = txtConfirmNewPassword.Text;

            if(Username.Trim().Length <= 0) {
                MessageBox.Show(this, "Please enter your username.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtUsername.Focus();
                txtUsername.SelectAll();
            } else if(Password.Trim().Length <= 0) {
                MessageBox.Show(this, "Please enter your password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtPassword.Focus();
                txtPassword.SelectAll();
            } else {//Verity login and set LogonUser if successful
                if(LoginAction == null)
                    throw new Exception("No LoginAction specified.");

                //Check new passwords
                if(pnlNewPassword.Visible){
                    if(!NewPassword.Equals(ConfirmNewPassword)) {
                        MessageBox.Show(this, "New Password and Confirm do NOT match.");
                        txtNewPassword.Focus();
                        txtNewPassword.SelectAll();
                        return;
                    }else if(NewPassword.Length <= 0){
                        MessageBox.Show(this, "Please enter a New Password.");
                        txtNewPassword.Focus();
                        txtNewPassword.SelectAll();
                        return;
                    }
                } else {
                    NewPassword = Password;
                }

                LogonUser = this.Invoke(LoginAction, new string[]{ Username, Password, NewPassword }) as User;

                if(LogonUser == null) {
                    MessageBox.Show(this, "The username or password you entered is incorrect.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    loginTry++;

                    txtUsername.Focus();
                    txtUsername.SelectAll();

                    if(loginTry >= 3) {
                        this.Close();
                    }
                } else {
                    Success = true;
                    LoginErrorMessage = "";
                    loginTry = 0;
                    this.Close();
                }
            }
        }

        private void showPasswordChangePanel(bool show) {
            pnlNewPassword.Visible = show;
            btnChangePassword.Visible = !show;
            btnCancelChange.Visible = show;

            this.Height += (show ? pnlNewPassword.Height : -pnlNewPassword.Height);
        }

        private void btnChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            showPasswordChangePanel(true);
        }

        private void btnCancelChange_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            showPasswordChangePanel(false);
        }

        private void LoginForm_Load(object sender, EventArgs e) {
            Success = false;
            LoginErrorMessage = "Login cancelled.";
            loginTry = 0;

            showPasswordChangePanel(false);
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e) {
            //Press Control+Shift+F12 hot key to do custom action
            if(e.KeyValue == 123 && e.Control && e.Shift) {
                if(HotKeyAction != null) HotKeyAction();
            }
        }
    }
}
