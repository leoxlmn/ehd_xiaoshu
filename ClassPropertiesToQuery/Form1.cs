using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ClassPropertiesToQuery {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (DialogResult.OK == dlgOpenFile.ShowDialog(this)) {
                txtPropertyListFile.Text = dlgOpenFile.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (txtPropertyListFile.Text.Trim().Length > 0) {

                StringBuilder sb = new StringBuilder();

                using (StreamReader reader = File.OpenText(txtPropertyListFile.Text)) {

                    string line;
                    string propType;
                    string propName;
                    string dbType;
                    string dbName;
                    string lastDbName = "NPC_ITRId";

                    while ( null != (line = reader.ReadLine())) {

                        if (line.Trim().Length <= 0) continue;

                        string[] arr = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (arr.Length != 2) continue;

                        propType = arr[0].Trim();
                        propName = arr[1].Trim();

                        switch (propType) {
                            case "int":
                                dbType = "INT";
                                break;
                            case "uint":
                                dbType = "INT UNSIGNED";
                                break;
                            case "string":
                                dbType = "VARCHAR(255)";
                                break;
                            case "bool?":
                                dbType = "BIT";
                                break;
                            default:
                                dbType = "INT";
                                break;
                        }

                        dbName = "NPC_" + propName;

                        if (sb.Length > 0) {
                            sb.AppendLine(", ");
                        }
                        sb.Append("ADD COLUMN ");
                        sb.Append(dbName);
                        sb.Append(" ");
                        sb.Append(dbType);
                        sb.Append(" NULL AFTER ");
                        sb.Append(lastDbName);

                        lastDbName = dbName;
                    }

                    if (sb.Length > 0) {
                        sb.AppendLine(";");
                    }


                    reader.Close();
                }

                txtMySQLQuery.Text = sb.ToString();
                txtMySQLQuery.SelectionStart = 0;
                txtMySQLQuery.SelectionLength = txtMySQLQuery.Text.Length;

                MessageBox.Show(this, "Done.");
            } else {
                MessageBox.Show(this, "Please select a file which containing all the class property names. Each line looks like:\n\nint Name1\nbool? Name2\n(...)");
            }
        }
    }
}
