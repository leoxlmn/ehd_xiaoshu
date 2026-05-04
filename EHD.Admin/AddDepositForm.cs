using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using NHibernate;
using EHD.Repository;
using Utility;
using NHibernate.Transform;
using NHibernate.Criterion;
using HolidayCalculator;
using EHD.Constant;

namespace EHD.Admin {
    public partial class AddDepositForm : Form {
        private Patient _patient;

        public AddDepositForm(Patient patient) {
            InitializeComponent();

            _patient = patient;

            if (_patient == null || _patient.Id == default(int)) {
                throw new ApplicationException("Failed to open the add deposit screen as the patient does NOT exist.");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {

            //Validation on amount
            double amount = 0;
            double.TryParse(txtAmount.Text, out amount);
            if ( amount <= 0 ) {
                txtAmount.Focus();
                txtAmount.SelectAll();
                MessageBox.Show(this,
                    string.Format("Please enter a positive number for the amount of {0}.", (rdoDeposit.Checked ? "Deposit" : "Spend")),
                    "Input Error",
                    MessageBoxButtons.OK);
                return;
            }

            //Check if it's a holiday
            if (CanadaHolidays.IsHoliday(dtDeposit.Value)) {
                if (DialogResult.Yes != MessageBox.Show(this,
                    string.Format(Constants.WARNING_HOLIDAY, dtDeposit.Value.Date),
                    "Holiday Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)) {
                        dtDeposit.Focus();
                        dtDeposit.Select();
                        return;
                }
            }

            //Check if it's a future date
            if (dtDeposit.Value.Date > DateTime.Now.Date) {
                if (DialogResult.Yes != MessageBox.Show(this,
                    string.Format(Constants.WARNING_FUTURE_DATE, dtDeposit.Value.Date),
                    "Future Date Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)) {
                    dtDeposit.Focus();
                    dtDeposit.Select();
                    return;
                }
            }

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    AccountBalance newDeposit = new AccountBalance();
                    newDeposit.Patient = _patient;
                    newDeposit.TransactionAmount = (rdoDeposit.Checked ? amount : -amount);
                    newDeposit.TransactionTime = dtDeposit.Value.Date;
                    newDeposit.TransactionDescription = txtNote.Text;
                    newDeposit.CreatedTime = DateTime.Now;
                    newDeposit.CreatedBy = Program.LogonUser.DisplayName;
                    newDeposit.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);

                    session.Save(newDeposit);

                    tr.Commit();
                    this.Close();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Failed to save deposit. ", ex);
                }
            }
        }

        private void rdoDeposit_CheckedChanged(object sender, EventArgs e) {
            rdoTransactionTypeChanged();
        }

        private void rdoTransactionTypeChanged() {
            if (rdoDeposit.Checked) {
                lblAmount.Text = "$";
                lblAmount.ForeColor = Color.Green;
            } else {
                lblAmount.Text = "$ -";
                lblAmount.ForeColor = Color.Red;
            }
        }
    }
}
