using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FilePathExtender;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Net;

namespace EHD.Admin {
    public partial class HelpForm : Form {
        public HelpForm() {
            InitializeComponent();
        }

        private void btnViewLog_Click(object sender, EventArgs e) {
            //Clear log output box
            txtLogOutput.Text = "";

            //Get the log file name
            string logFileName = getLogFileName();

            try {
                if(File.Exists(logFileName)) {
                    using(FileStream fs = File.OpenRead(logFileName))
                    using(StreamReader reader = new StreamReader(fs)) {
                        txtLogOutput.Text = reader.ReadToEnd();
                    }
                } else {
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to read log file.");
                Program.log.Error("Failed to read log file.", ex);
            }
        }

        //Zip and send to Leo by email
        private void btnSendLog_Click(object sender, EventArgs e)
        {
            //validate
            if (txtName.Text.Trim().Length <= 0)
            {
                MessageBox.Show(this, "Please enter your name.", "Form not complete", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtName.Focus();
                txtName.Select();
                return;
            }
            if (txtEmail.Text.Trim().Length <= 0)
            {
                MessageBox.Show(this, "Please enter your email address.", "Form not complete", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtEmail.Focus();
                txtEmail.Select();
                return;
            }
            if (txtSubject.Text.Trim().Length <= 0)
            {
                MessageBox.Show(this, "Please enter the email subject.", "Form not complete", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtSubject.Focus();
                txtSubject.Select();
                return;
            }
            if (txtEmailMessage.Text.Trim().Length <= 0)
            {
                MessageBox.Show(this, "Please explain the problem you are having in detail.", "Form not complete", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtEmailMessage.Focus();
                txtEmailMessage.Select();
                return;
            }

            MessageBox.Show(this, "Please wait and be a little patient. It may take more than 1 minute to send the log.");

            btnSendLog.Enabled = false;

            //Get the log file name
            string logFileName = getLogFileName();

            if (File.Exists(logFileName))
            {
                try
                {
                    //1) Zip the log file

                    //Remove existing zip file
                    string zipFileName = logFileName + ".gz";
                    if (File.Exists(zipFileName))
                    {
                        File.Delete(zipFileName);
                    }

                    //zip
                    using (FileStream src = File.OpenRead(logFileName))
                    using (FileStream tar = File.Create(zipFileName))
                    using (GZipStream zip = new GZipStream(tar, CompressionMode.Compress))
                    {
                        src.CopyTo(zip);
                    }

                    //2) Send email
                    using (SmtpClient smtp = new SmtpClient(Program.config.LogSMTPServer, Program.config.LogSMTPSSLPort))
                    {
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(Program.config.LogSenderAccount, Program.config.LogSenderPassword);

                        MailMessage mail = new MailMessage(Program.config.LogSenderAccount, Program.config.LogReceiverAccount);
                        mail.Subject = "Easy Healthcare Desktop - Help Request at [" + DateTime.Now.ToString("yyyyMMddHHmm") + "] by [" + txtName.Text.Trim() + ", " + txtEmail.Text.Trim() + ", " + txtPhone.Text.Trim() + "]: " + txtSubject.Text.Trim();
                        mail.IsBodyHtml = true;
                        mail.Body = txtEmailMessage.Text;
                        Attachment attatchment = new Attachment(zipFileName);
                        mail.Attachments.Add(attatchment);

                        smtp.Send(mail);
                        attatchment.Dispose();
                        MessageBox.Show(this, "Message sent.");
                    }

                    //3) Save form
                    Program.config.HelpFormName = txtName.Text.Trim();
                    Program.config.HelpFormEmail = txtEmail.Text.Trim();
                    Program.config.HelpFormPhone = txtPhone.Text.Trim();
                    Program.config.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to send log file.\n\n" + ex.Message);
                    Program.log.Error("Failed to send log file.", ex);
                }

            }
            else
            {
                MessageBox.Show(this, "No log file found.");
            }

            btnSendLog.Enabled = true;
        }

        private string getLogFileName() {
            //Get app root path
            string AppRootPath;
            AppRootPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            AppRootPath = AppRootPath.Substring(0, AppRootPath.LastIndexOf('\\'));

            //Get the log file name
            string logFileName = FileExtender.ConcatPhysicalPath(AppRootPath,
                    txtLogFileName.Text);

            return logFileName;
        }

        private void HelpForm_Load(object sender, EventArgs e) {
            txtLogFileName.Text = Program.config.LogFileName;
            txtName.Text = Program.config.HelpFormName;
            txtEmail.Text = Program.config.HelpFormEmail;
            txtPhone.Text = Program.config.HelpFormPhone;
            txtSubject.Text = Program.config.HelpFormSubject;

            if (txtName.Text.Length <= 0)
            {
                txtName.Select();
            }
            else if (txtEmail.Text.Length <= 0)
            {
                txtEmail.Select();
            }
            else
            {
                txtSubject.Select();
            }
        }
    }
}
