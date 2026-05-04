using EHD.Model.Entity;
using EHD.Repository;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class ManageTaxForm : Form {
        public ManageTaxForm() {
            InitializeComponent();
        }

        private void populateTaxList() {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    var types = session.QueryOver<Tax>()
                        .OrderBy(x => x.Name).Asc
                        .Take(10) //There can't be more than 10 types of taxes
                        .List();
                    grdTax.DataSource = types;

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populate tax list error.", ex);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if (DialogResult.OK == (new EditTaxForm()).ShowDialog(this)) {
                populateTaxList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if (grdTax.SelectedRows.Count > 0) {

                Tax selectedTax = grdTax.SelectedRows[0].DataBoundItem as Tax;

                if (DialogResult.OK == (new EditTaxForm(selectedTax)).ShowDialog(this)) {
                    populateTaxList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (grdTax.SelectedRows.Count > 0) {

                Tax selectedTax = grdTax.SelectedRows[0].DataBoundItem as Tax;

                if (DialogResult.Yes == MessageBox.Show(this, "Do you want to delete Tax: [" + selectedTax.DisplayName + "]?",
                    "Delete Tax",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using (ITransaction tr = session.BeginTransaction()) {
                        try {
                            Tax type = session.Get<Tax>(selectedTax.Id);
                            session.Delete(type);

                            tr.Commit();
                        } catch (Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete Tax. " + ex.Message);
                            Program.log.Error("Failed to delete Tax.", ex);
                        }
                    }

                    //Refresh Tax List
                    populateTaxList();
                }
            }
        }

        private void grdTax_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                Tax selectedTax = grdTax.Rows[e.RowIndex].DataBoundItem as Tax;

                if (DialogResult.OK == (new EditTaxForm(selectedTax)).ShowDialog(this)) {
                    populateTaxList();
                }
            }
        }

        private void grdTax_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if (grdTax.Columns != null) {
                if (grdTax.Columns.Contains("IsTestData")) grdTax.Columns["IsTestData"].Visible = false;
                if (grdTax.Columns.Contains("CreatedBy")) grdTax.Columns["CreatedBy"].Visible = false;
                if (grdTax.Columns.Contains("UpdatedBy")) grdTax.Columns["UpdatedBy"].Visible = false;
                if (grdTax.Columns.Contains("CreatedTime")) grdTax.Columns["CreatedTime"].Visible = false;
                if (grdTax.Columns.Contains("UpdatedTime")) grdTax.Columns["UpdatedTime"].Visible = false;
                if (grdTax.Columns.Contains("Version")) grdTax.Columns["Version"].Visible = false;
            }
        }

        private void ManageTaxForm_Load(object sender, EventArgs e) {
            populateTaxList();
        }
    }
}
