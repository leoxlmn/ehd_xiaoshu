using EHD.Constant;
using EHD.Model.Entity;
using EHD.Repository;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;
using System.Security;

namespace EHD.Admin {
    public partial class EditTaxForm : Form {

        public Tax EditingTax { get; set; }

        public EditTaxForm() {
            InitializeComponent();
        }

        public EditTaxForm(Tax type) : this() {
            EditingTax = type;
        }
        private void EditTaxForm_Load(object sender, EventArgs e) {
            loadTax();
        }

        private void loadTax() {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    if (EditingTax == null || EditingTax.Id == default(int)) {
                        EditingTax = new Tax();
                        EditingTax.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingTax.CreatedBy = Program.LogonUser.DisplayName;
                        EditingTax.CreatedTime = DateTime.Now;
                    } else {
                        EditingTax = session.Merge<Tax>(EditingTax);
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Tax. " + ex.Message);
                    Program.log.Error("Failed to load Tax.", ex);
                }
            }

            populateTaxDetail(EditingTax);
        }

        private void populateTaxDetail(Tax tax) {
            if (tax != null) {
                txtName.Text = tax.Name;
                numPercent.Value = (decimal)(tax.Rate * 100);
            }
        }

        private void updateTaxInput(Tax tax) {
            tax.Name = txtName.Text.Trim();
            tax.Rate = (double)numPercent.Value / 100.0;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    if (EditingTax == null || EditingTax.Id == default(int)) {
                        EditingTax = new Tax();
                        EditingTax.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingTax.CreatedBy = Program.LogonUser.DisplayName;
                        EditingTax.CreatedTime = DateTime.Now;
                    } else {
                        EditingTax = session.QueryOver<Tax>()
                            .Where(x => x.Id == EditingTax.Id)
                            .SingleOrDefault();

                        EditingTax.UpdatedTime = DateTime.Now;
                        EditingTax.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateTaxInput(EditingTax);

                    if (!EditingTax.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingTax);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save Tax. " + ex.Message);
                    Program.log.Error("Failed to save Tax.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
