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
using WordFileProcessor;
using System.IO;
using FilePathExtender;
using System.Reflection;
using NHibernate.Criterion;
using Utility;
using System.Collections;

namespace EHD.Admin {
    public partial class HistoryForm : Form {
        public HistoryForm() {
            InitializeComponent();
        }

        private void HistoryForm_Load(object sender, EventArgs e) {
            populateDropdowns();
        }

        private void populateDropdowns() {

            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    //Load Operator List
                    IList<User> lstUser = session.QueryOver<User>()
                        .OrderBy(x => x.FirstName).Asc
                        .OrderBy(x => x.LastName).Asc
                        .List();
                    lstUser.Insert(0, new User());
                    drpOperator.DataSource = lstUser;
                    drpOperator.ValueMember = "Id";
                    drpOperator.DisplayMember = "DisplayName";

                    //Load Entity Type list
                    IList<string> lstEntityType = session.QueryOver<HistoryTracking>()
                        .Select(Projections.Distinct(Projections.Property("ObjectName")))
                        .OrderBy(Projections.Property("ObjectName")).Asc
                        .List<string>();
                    lstEntityType.Insert(0, "");
                    drpEntityType.DataSource = lstEntityType;

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load dropdown lists. " + ex.Message);
                    Program.log.Error("Failed to load dropdown lists.", ex);
                }
            }

        }

        private void search() {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    //Build criteria
                    var qry = session.CreateCriteria<HistoryTracking>();
                    if (drpEntityType.SelectedItem != null && drpEntityType.SelectedItem.ToString().Length > 0) {
                        qry.Add(Restrictions.Eq("ObjectName", drpEntityType.SelectedItem.ToString()));
                    }
                    if (drpOperator.SelectedItem != null && (drpOperator.SelectedItem as User).Id != default(int)) {
                        qry.Add(Restrictions.Eq("ActionBy", (drpOperator.SelectedItem as User).DisplayName));
                    }
                    if (txtId.Text.Trim().Length > 0) {
                        qry.Add(Restrictions.Eq("ObjectId", txtId.Text.Trim()));
                    }
                    if (dtFrom.Checked) {
                        qry.Add(Restrictions.Ge("ActionTime", (DateTime)(dtFrom.Value).Date));
                    }
                    if (dtTo.Checked) {
                        qry.Add(Restrictions.Lt("ActionTime", (DateTime)(dtTo.Value).Date.AddDays(1)));
                    }

                    //Run query to retrieve history
                    var rowCount = (qry.Clone() as ICriteria).SetProjection(Projections.RowCount()).FutureValue<Int32>();

                    IList<HistoryTracking> lstHistory = qry
                        .AddOrder(new Order("ActionTime", true))
                        .SetFirstResult(Convert.ToInt32((numPageNumber.Value - 1) * numPageSize.Value))
                        .SetMaxResults(Convert.ToInt32(numPageSize.Value))
                        .List<HistoryTracking>();

                    int totalCount = rowCount.Value;
                    int totalPage = Convert.ToInt32(Math.Ceiling(totalCount / numPageSize.Value));
                    lblTotalPage.Text = totalPage.ToString();
                    //Remove eventhandler before setting the contorl value
                    numPageNumber.ValueChanged -= numPageNumber_ValueChanged;
                    numPageNumber.Maximum = totalPage;
                    numPageNumber.Minimum = 1;
                    numPageNumber.ValueChanged += numPageNumber_ValueChanged;

                    //Add sorting support
                    //SortableBindingList<HistoryTracking> histories = new SortableBindingList<HistoryTracking>();
                    //foreach (HistoryTracking history in lstHistory) {
                    //    histories.Add(history);
                    //}

                    //Bind data to datagrid
                    //grdHistory.DataSource = histories;
                    grdHistory.DataSource = lstHistory;

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load dropdown lists. " + ex.Message);
                    Program.log.Error("Failed to load dropdown lists.", ex);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            search();
        }

        private void grdHistory_SelectionChanged(object sender, EventArgs e) {

            if(grdHistory.Rows.Count <= 0 || grdHistory.SelectedRows.Count <= 0) return;

            HistoryTracking history = grdHistory.SelectedRows[0].DataBoundItem as HistoryTracking;

            Cursor curCursor = this.Cursor;
            Cursor = Cursors.WaitCursor;

            try {

                if(null == history) {
                    txtOldValue.Text = string.Empty;
                    txtNewValue.Text = string.Empty;
                    return;
                } else {
                    //Color table: 1-black, 2-white, 3-red
                    const string RTF_COLOR_TABLE = @"{\colortbl;\red0\green0\blue0;\red255\green255\blue255;\red255\green0\blue0;}";
                    //Font table: Curier New
                    const string RTF_FONT_TABLE = @"{\fonttbl{\f0\fnil\fcharset0 Courier New;}}";

                    StringBuilder sbOld = new StringBuilder();
                    Object objOld = null;
                    sbOld.AppendLine(@"{\rtf1\deff0");
                    sbOld.AppendLine(RTF_COLOR_TABLE);
                    sbOld.AppendLine(RTF_FONT_TABLE);

                    StringBuilder sbNew = new StringBuilder();
                    Object objNew = null;
                    sbNew.AppendLine(@"{\rtf1\deff0");
                    sbNew.AppendLine(RTF_COLOR_TABLE);
                    sbNew.AppendLine(RTF_FONT_TABLE);

                    //Get Object type
                    string className = typeof(User).AssemblyQualifiedName;
                    className = className.Substring(className.IndexOf(','));
                    className = typeof(User).Namespace + "." + history.ObjectName + className;
                    Type objType = Type.GetType(className);

                    if(objType != null) {
                        if(history.OldValue != null) {
                            objOld = typeof(JSONHelper).GetMethod("GetObject")
                                .MakeGenericMethod(objType)
                                .Invoke(null, new object[] { history.OldValue });
                        }

                        if(history.NewValue != null) {
                            objNew = typeof(JSONHelper).GetMethod("GetObject")
                                .MakeGenericMethod(objType)
                                .Invoke(null, new object[] { history.NewValue });
                        }
                    }

                    if(null != objOld && null != objNew) {
                        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(
                            objOld.GetType());
                        foreach(PropertyDescriptor prop in properties) {
                            Object valOld = prop.GetValue(objOld);
                            Object valNew = prop.GetValue(objNew);
                            string strValOld = null != valOld ?
                                ((valOld is IEnumerable && !(valOld is string)) ? JSONHelper.EnumerableToString(valOld as IEnumerable) : valOld.ToString())
                                : string.Empty;
                            string strValNew = null != valNew ?
                                ((valNew is IEnumerable && !(valNew is string)) ? JSONHelper.EnumerableToString(valNew as IEnumerable) : valNew.ToString())
                                : string.Empty;

                            sbOld.AppendLine(@"{\cb2\cf1\fs18{" + prop.Name.PadLeft(30) + @": ");
                            sbNew.AppendLine(@"{\cb2\cf1\fs18{" + prop.Name.PadLeft(30) + @": ");

                            if(strValOld.Equals(strValNew)) {
                                sbOld.AppendLine(strValOld);
                                sbNew.AppendLine(strValNew);
                            } else {
                                sbOld.Append(@"{\cb2\cf3\fs18 " + strValOld + @"}");
                                sbNew.Append(@"{\cb2\cf3\fs18 " + strValNew + @"}");
                            }

                            sbOld.AppendLine(@"}\line}");
                            sbNew.AppendLine(@"}\line}");
                        }
                    } else {
                        if(null != objOld) {
                            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(
                                objOld.GetType());
                            foreach(PropertyDescriptor prop in properties) {
                                Object valOld = prop.GetValue(objOld);
                                string strValOld = null != valOld ?
                                    ((valOld is IEnumerable && !(valOld is string)) ? JSONHelper.EnumerableToString(valOld as IEnumerable) : valOld.ToString())
                                    : string.Empty;
                                sbOld.AppendLine(@"{\cb2\cf1\fs18{" +
                                    prop.Name.PadLeft(30) +
                                    @": " +
                                    strValOld +
                                    @"}\line}");
                            }
                        } else if(history.OldValue != null) {
                            //The old object failed to be unserialized back to it's original type. Show raw string instead.
                            sbOld.AppendLine(@"{\cb2\cf1\fs18{" +
                                    processRawString(history.OldValue) +
                                    @"}\line}");
                        }
                        if(null != objNew) {
                            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(
                                objNew.GetType());
                            foreach(PropertyDescriptor prop in properties) {
                                Object valNew = prop.GetValue(objNew);
                                string strValNew = null != valNew ?
                                    ((valNew is IEnumerable && !(valNew is string)) ? JSONHelper.EnumerableToString(valNew as IEnumerable) : valNew.ToString())
                                    : string.Empty;
                                sbNew.AppendLine(@"{\cb2\cf1\fs18{" +
                                    prop.Name.PadLeft(30) +
                                    @": " +
                                    strValNew +
                                    @"}\line}");
                            }
                        } else if(history.NewValue != null) {
                            //The new object failed to be unserialized back to it's original type. Show raw string instead.
                            sbNew.AppendLine(@"{\cb2\cf1\fs18{" +
                                    processRawString(history.NewValue) +
                                    @"}\line}");
                        }
                    }

                    sbOld.Append(@"}");
                    sbNew.Append(@"}");

                    txtOldValue.Rtf = sbOld.ToString();
                    txtNewValue.Rtf = sbNew.ToString();
                }
            } catch(Exception ex) {
                MessageBox.Show("Failed to display the history info. " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } finally {
                Cursor = curCursor;
            }
        }

        private void grdHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            foreach(DataGridViewRow row in grdHistory.Rows) {
                HistoryTracking history = row.DataBoundItem as HistoryTracking;
                switch(history.ActionType) {
                    case ActionType.Insert:
                        row.Cells["ActionType"].Style.BackColor = Color.LightGreen;
                        break;
                    case ActionType.Update:
                        row.Cells["ActionType"].Style.BackColor = Color.Yellow;
                        break;
                    case ActionType.Delete:
                        row.Cells["ActionType"].Style.BackColor = Color.Red;
                        break;
                    default:
                        break;
                }
            }
        }

        private string processRawString(string raw) {
            StringBuilder sb = new StringBuilder();
            char[] arrRaw = raw.ToCharArray();
            int indentLevel = 0;
            const int INDENT = 4;
            bool isInString = false;

            for(int i = 0; i < arrRaw.Length; i++) {
                char c = arrRaw[i];

                switch(c) {
                    case '\\':
                        sb.Append(c);
                        i++;
                        sb.Append(arrRaw[i]);
                        break;
                    case '"':
                        isInString = !isInString;
                        //sb.Append(c);
                        break;
                    case '{':
                    case '[':
                        if(isInString) {
                            sb.Append(c);
                        } else {
                            sb.Append(@"\line");
                            indentLevel++;
                            sb.Append(" ".PadRight(INDENT * indentLevel));
                        }
                        break;
                    case '}':
                    case ']':
                        if(isInString) {
                            sb.Append(c);
                        } else {
                            sb.Append(@"\line");
                            sb.Append(" ".PadRight(INDENT * indentLevel));
                            indentLevel--;
                        }
                        break;
                    case ',':
                        if(isInString) {
                            sb.Append(c);
                        } else {
                            sb.Append(c);
                            sb.Append(@"\line");
                            sb.Append(" ".PadRight(INDENT * indentLevel));
                        }
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }

        private void numPageNumber_ValueChanged(object sender, EventArgs e) {
            search();
        }

        private void btnFirst_Click(object sender, EventArgs e) {
            numPageNumber.Value = numPageNumber.Minimum;
        }

        private void btnLast_Click(object sender, EventArgs e) {
            numPageNumber.Value = numPageNumber.Maximum;
        }
    }
}
