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
    public partial class EditInsurerForm : Form {

        public Insurer EditingInsurer { get; set; }

        public EditInsurerForm() {
            InitializeComponent();
        }

        public EditInsurerForm(Insurer insurer) : this() {
            EditingInsurer = insurer;
        }

        private void EditInsurerForm_Load(object sender, EventArgs e) {
            loadInsurer();
        }

        private void loadInsurer() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingInsurer == null || EditingInsurer.Id == default(int)) {
                        EditingInsurer = new Insurer();
                        EditingInsurer.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingInsurer.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInsurer.CreatedTime = DateTime.Now;
                    } else {
                        EditingInsurer = session.Merge<Insurer>(EditingInsurer);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Insurer. " + ex.Message);
                    Program.log.Error("Failed to load Insurer.", ex);
                }
            }

            populateInsurerDetail(EditingInsurer);
        }

        private void populateInsurerDetail(Insurer insurer) {
            if(insurer != null) {
                txtInsurerName.Text = insurer.InsurerName;
                txtComment.Text = insurer.Comment;
                //numMaxTreatmentPerDay.Value = insurer.MaxTreatmentPerDay;
                //numWarnTreatmentPerDay.Value = insurer.WarnTreatmentPerDay;
            }
        }

        private void updateInsurerInput(Insurer insurer) {
            insurer.InsurerName = txtInsurerName.Text.Trim();
            insurer.Comment = txtComment.Text.Trim();
            //insurer.MaxTreatmentPerDay = Convert.ToInt32(numMaxTreatmentPerDay.Value);
            //insurer.WarnTreatmentPerDay = Convert.ToInt32(numWarnTreatmentPerDay.Value);
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingInsurer == null || EditingInsurer.Id == default(int)) {
                        EditingInsurer = new Insurer();
                        EditingInsurer.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingInsurer.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInsurer.CreatedTime = DateTime.Now;
                    } else {
                        EditingInsurer = session.QueryOver<Insurer>()
                            .Where(x => x.Id == EditingInsurer.Id)
                            .SingleOrDefault();

                        EditingInsurer.UpdatedTime = DateTime.Now;
                        EditingInsurer.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateInsurerInput(EditingInsurer);

                    if(!EditingInsurer.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingInsurer);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save Insurer. " + ex.Message);
                    Program.log.Error("Failed to save Insurer.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
