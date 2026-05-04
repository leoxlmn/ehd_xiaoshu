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
    public partial class SelectTaxForm : Form {

        public string SelectedTaxName { get; private set; }
        public decimal SelectedTaxRate { get; private set; }

        public SelectTaxForm() {
            InitializeComponent();
        }

        private void SelectTaxForm_Load(object sender, EventArgs e) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession()) {

                    IList<Tax> taxes = session.QueryOver<Tax>()
                        .OrderBy(x => x.Name).Asc
                        .List()
                        .ToList();

                    lstTax.DataSource = taxes;
                    lstTax.ValueMember = "Id";
                    lstTax.DisplayMember = "DisplayName";

                    //Check all items by default
                    for (int i=0; i<lstTax.Items.Count; i++) {
                        lstTax.SetItemChecked(i, true);
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Taxs. " + ex.Message);
                Program.log.Error("Failed to load Taxes.", ex);
            }
        }

        private void btnApply_Click(object sender, EventArgs e) {
            
            SelectedTaxName = string.Empty;
            SelectedTaxRate = 0;

            if (lstTax.CheckedItems.Count > 0) {
                if (lstTax.CheckedItems.Count == 1) {
                    Tax tax = lstTax.CheckedItems[0] as Tax;
                    SelectedTaxName = tax.Name;
                    SelectedTaxRate = (decimal) tax.Rate * 100;
                } else {
                    SelectedTaxName = "Tax";
                    foreach (var tax in lstTax.CheckedItems) {
                        SelectedTaxRate += (decimal)(tax as Tax).Rate * 100;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
