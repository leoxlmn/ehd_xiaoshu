using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EHD.Admin {
    public partial class TemplateListForm : Form {


        public int SelectedTemplateId {
            get {
                if (lstTemplate.SelectedItem != null) {
                    return (lstTemplate.SelectedItem as SimpleListItem).Id;
                } else {
                    return 0;
                }
            }
            set {
                for (int i = 0; i < lstTemplate.Items.Count; i++) {
                    SimpleListItem item = lstTemplate.Items[i] as SimpleListItem;
                    if (item.Id == SelectedTemplateId) {
                        lstTemplate.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public IList<SimpleListItem> Templates {
            set {
                lstTemplate.Items.Clear();

                foreach (SimpleListItem item in value) {
                    lstTemplate.Items.Add(item);
                }
            }
        }

        public TemplateListForm() {
            InitializeComponent();

            lstTemplate.ValueMember = "Id";
            lstTemplate.DisplayMember = "Text";
        }

        private void btnOK_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void lstTemplate_ItemCheck(object sender, ItemCheckEventArgs e) {
            for (int i = 0; i < lstTemplate.Items.Count; i++) {
                if (e.Index != i) lstTemplate.SetItemChecked(i, false);
            }
        }
    }
}
