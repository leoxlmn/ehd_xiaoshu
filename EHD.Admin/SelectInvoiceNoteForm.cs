using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using EHD.Repository;
using EHD.Model.Entity;

namespace EHD.Admin {
    public partial class SelectInvoiceNoteForm : Form {

        public SelectInvoiceNoteForm() {
            InitializeComponent();
        }

        public string SelectedInvoiceNote { get; set; }

        private void SelectInvoiceNoteForm_Load(object sender, EventArgs e) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession()) {

                    IList<InvoiceNoteReference> lstNote = session.QueryOver<InvoiceNoteReference>()
                        .List<InvoiceNoteReference>();

                    lstInvoiceNote.DataSource = lstNote;
                    lstInvoiceNote.ValueMember = "Id";
                    lstInvoiceNote.DisplayMember = "DisplayName";
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Invoice Notes. " + ex.Message);
                Program.log.Error("Failed to load Invoice Notes.", ex);
            }

        }

        private void lstInvoiceNote_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (lstInvoiceNote.SelectedIndex >= 0) {
                SelectedInvoiceNote = (lstInvoiceNote.Items[lstInvoiceNote.SelectedIndex] as InvoiceNoteReference).Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
