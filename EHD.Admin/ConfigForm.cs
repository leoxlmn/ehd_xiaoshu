using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EHD.Admin {
    public partial class ConfigForm : Form {
        public ConfigForm() {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            saveConfig();

            MessageBox.Show(this, "Application still restart.");
            Application.Restart();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void loadConfig() {
            /*
            txtConnectionString.Text = Program.config.ConnectionString;
            txtSignatureMaxHeight.Text = Program.config.SignatureMaxHeight.ToString();
            txtSignatureMaxWidth.Text = Program.config.SignatureMaxWidth.ToString();*/

            txtServer.Text = Program.config.Server;
            txtPort.Text = Program.config.Port.ToString();
            txtDatabase.Text = Program.config.Database;
            txtUser.Text = Program.config.DBUser;
            txtPassword.Text = Program.config.DBPassword;
            txtMaxSigWidth.Text = Program.config.SignatureMaxWidth.ToString();
            txtMaxSigHeight.Text = Program.config.SignatureMaxHeight.ToString();
            chkEnableGoogleCalendar.Checked = false;// Program.config.EnableGoogleCalendar;
            chkEnablePractitionerCalendar.Checked = Program.config.EnablePractitionerCalendar;
            numWorkingHourStart.Value = Program.config.WorkingHourStart;
            numWorkingHourEnd.Value = Program.config.WorkingHourEnd;
            chkDebugMode.Checked = Program.config.DebugMode;
            txtMaxInsurers.Text = Program.config.MaxInsurers.ToString();
            txtMaxTherapists.Text = Program.config.MaxTherapists.ToString();
            txtMaxTreatmentTypes.Text = Program.config.MaxTreatmentTypes.ToString();
            txtMaxTreatmentsPerPatient.Text = Program.config.MaxInitialTreatmentsPerPatient.ToString();
            txtMaxFollowUpsPerTreatment.Text = Program.config.MaxFollowUpsPerTreatment.ToString();
            txtMaxPointsPerDiagram.Text = Program.config.MaxPointsPerDiagram.ToString();
            txtMaxInvoicesPerPatient.Text = Program.config.MaxInvoicesPerPatient.ToString();
            txtMaxInvoiceItemPerInvoice.Text = Program.config.MaxInvoiceItemsPerInvoice.ToString();
            txtMaxDocumentType.Text = Program.config.MaxDocumentType.ToString();
        }

        private void saveConfig() {
            try {
                /*
                Program.config.ConnectionString = txtConnectionString.Text;
                Program.config.SignatureMaxHeight = int.Parse(txtSignatureMaxHeight.Text);
                Program.config.SignatureMaxWidth = int.Parse(txtSignatureMaxWidth.Text);*/

                Program.config.Server = txtServer.Text;
                Program.config.Port = int.Parse(txtPort.Text);
                Program.config.Database = txtDatabase.Text;
                Program.config.DBUser = txtUser.Text;
                Program.config.DBPassword = txtPassword.Text;
                Program.config.SignatureMaxWidth = int.Parse(txtMaxSigWidth.Text);
                Program.config.SignatureMaxHeight = int.Parse(txtMaxSigHeight.Text);
                Program.config.EnableGoogleCalendar = chkEnableGoogleCalendar.Checked;
                Program.config.EnablePractitionerCalendar = chkEnablePractitionerCalendar.Checked;
                Program.config.WorkingHourStart = Convert.ToInt32(numWorkingHourStart.Value);
                Program.config.WorkingHourEnd = Convert.ToInt32(numWorkingHourEnd.Value);
                Program.config.DebugMode = chkDebugMode.Checked;
                Program.config.MaxInsurers = int.Parse(txtMaxInsurers.Text);
                Program.config.MaxTherapists = int.Parse(txtMaxTherapists.Text);
                Program.config.MaxTreatmentTypes = int.Parse(txtMaxTreatmentTypes.Text);
                Program.config.MaxInitialTreatmentsPerPatient = int.Parse(txtMaxTreatmentsPerPatient.Text);
                Program.config.MaxFollowUpsPerTreatment = int.Parse(txtMaxFollowUpsPerTreatment.Text);
                Program.config.MaxPointsPerDiagram = int.Parse(txtMaxPointsPerDiagram.Text);
                Program.config.MaxInvoicesPerPatient = int.Parse(txtMaxInvoicesPerPatient.Text);
                Program.config.MaxInvoiceItemsPerInvoice = int.Parse(txtMaxInvoiceItemPerInvoice.Text);
                Program.config.MaxDocumentType = int.Parse(txtMaxDocumentType.Text);

                Program.config.Save();
            } catch(Exception ex) {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void ConfigForm_Load(object sender, EventArgs e) {
            loadConfig();

            //BringToFront doesn't work at all
            //this.BringToFront();
            IntPtr hConfigForm = FindWindow(null, "J.C Configuration");
            if(hConfigForm != null) {
                SetForegroundWindow(hConfigForm);
            }
        }
         
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
    }
}
