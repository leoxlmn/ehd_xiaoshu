using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using EHD.Constant;
using NHibernate;
using NHibernate.Criterion;

namespace EHD.Admin {
    public partial class EditTherapyTypeForm : Form {

        public TherapyType EditingTherapyType { get; set; }

        public EditTherapyTypeForm() {
            InitializeComponent();
        }

        public EditTherapyTypeForm(TherapyType therapy)
            : this() {
            EditingTherapyType = therapy;
        }

        private void EditTherapyTypeForm_Load(object sender, EventArgs e) {
            loadTherapyType();
        }

        private void loadTherapyType() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingTherapyType == null || EditingTherapyType.Id == default(int)) {
                        EditingTherapyType = new TherapyType();
                        EditingTherapyType.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingTherapyType.CreatedBy = Program.LogonUser.DisplayName;
                        EditingTherapyType.CreatedTime = DateTime.Now;
                        EditingTherapyType.MinutesPerTreatment = 60;
                    } else {
                        EditingTherapyType = session.Merge<TherapyType>(EditingTherapyType);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load TherapyType. " + ex.Message);
                    Program.log.Error("Failed to load TherapyType.", ex);
                }
            }

            populateTherapyTypeDetail(EditingTherapyType);
        }

        private void populateTherapyTypeDetail(TherapyType therapy) {
            if(therapy != null) {
                txtTherapyTypeName.Text = therapy.TherapyTypeName;
                chkEnableMinutesPerTreatment.Checked = therapy.EnableMinutesPerTreatment;
                numMinutesPerTreatment.Value = therapy.MinutesPerTreatment;
                chkEnableMaxTreatmentsPerDay.Checked = therapy.EnableMaxTreatmentsPerDay;
                numMaxTreatmentsPerDay.Value = therapy.MaxTreatmentsPerDay;
                txtDefaultPrice.Text = therapy.DefaultPrice.ToString("0.00");
                chkShowDurationOnInvoice.Checked = therapy.ShowDurationOnInvoice;
                chkAllowTimeOverlap.Checked = therapy.AllowTimeOverlap;
            }
        }

        private void updateTherapyTypeInput(TherapyType therapy) {
            therapy.TherapyTypeName = txtTherapyTypeName.Text.Trim();
            therapy.EnableMinutesPerTreatment = chkEnableMinutesPerTreatment.Checked;
            therapy.MinutesPerTreatment = Convert.ToInt32(numMinutesPerTreatment.Value);
            therapy.EnableMaxTreatmentsPerDay = chkEnableMaxTreatmentsPerDay.Checked;
            therapy.MaxTreatmentsPerDay = Convert.ToInt32(numMaxTreatmentsPerDay.Value);
            therapy.ShowDurationOnInvoice = chkShowDurationOnInvoice.Checked;
            therapy.AllowTimeOverlap = chkAllowTimeOverlap.Checked;

            double amount = -1;
            if (double.TryParse(txtDefaultPrice.Text, out amount)) {
                txtDefaultPrice.Text = amount.ToString("0.00");
                therapy.DefaultPrice = Convert.ToDouble(txtDefaultPrice.Text);
            } else {
                throw new ApplicationException("Default Price is not valid.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingTherapyType == null || EditingTherapyType.Id == default(int)) {
                        EditingTherapyType = new TherapyType();
                        EditingTherapyType.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingTherapyType.CreatedBy = Program.LogonUser.DisplayName;
                        EditingTherapyType.CreatedTime = DateTime.Now;
                    } else {
                        EditingTherapyType = session.QueryOver<TherapyType>()
                            .Where(x => x.Id == EditingTherapyType.Id)
                            .SingleOrDefault();

                        EditingTherapyType.UpdatedTime = DateTime.Now;
                        EditingTherapyType.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateTherapyTypeInput(EditingTherapyType);

                    if(!EditingTherapyType.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingTherapyType);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save TherapyType. " + ex.Message);
                    Program.log.Error("Failed to save TherapyType.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
