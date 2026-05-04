using EHD.Model.Entity;
using MySqlX.XDevAPI;
using NHibernate.Criterion;
using NHibernate.Transform;
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
using EHD.Repository;
using EHD.Model.DTO;
using NHibernate.SqlCommand;
using Utility;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Diagnostics;
using NHibernate.Hql.Ast;

namespace EHD.Admin {
    public partial class ReportsForm : Form {

        //Open session for the form
        private Guid _sessionKey = Guid.NewGuid();
        private ISession session;

        #region Private members
        bool _isSearchingPatient = false;
        #endregion Private members

        public ReportsForm() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void ucPatient_MinInputLengthReached(object sender, EventArgs e) {
            if (_isSearchingPatient) return;

            _isSearchingPatient = true;

            //Search patients
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    //Search top 20 matches
                    SimpleListItem alias = null;
                    var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Insurer).Eager
                        .Where(Restrictions.Disjunction()
                            .Add(Restrictions.Eq(Projections.Property<Patient>(x => x.FileNumber), ucPatient.InputText))
                            .Add(Restrictions.On<Patient>(x => x.FirstName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.LastName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.ContactInfo.Address.AddressLine1).IsInsensitiveLike(ucPatient.InputText + "%"))
                            )
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => alias.Id)
                            .Select(x => Projections.Concat(x.FileNumber, " - ", x.FirstName, " ", x.LastName, " [", x.ContactInfo.Address.AddressLine1, " ", x.ContactInfo.Address.AddressLine2, "]")).WithAlias(() => alias.Text)
                            )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.FirstName).Asc
                        .Take(20)
                        .List<SimpleListItem>();

                    ucPatient.UpdateList(patients);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to search patient. " + ex.Message);
                    Program.log.Error("Failed to search patient.", ex);
                } finally {
                    _isSearchingPatient = false;
                }
            }

        }

        private void ReportsForm_Load(object sender, EventArgs e) {
            //Open session
            if (session == null) session = SessionFactory.GetOpenSession(_sessionKey);
        }

        private void ReportsForm_FormClosed(object sender, FormClosedEventArgs e) {
            //Try to close session
            SessionFactory.TryCloseSession(_sessionKey);
        }

        private void enableDates() {
            dtFrom.Enabled = chkFromDate.Checked;
            dtTo.Enabled = chkToDate.Checked;
        }

        private void btnExportServices_Click(object sender, EventArgs e) {
            //Check if Patient has been selected
            if (ucPatient.SelectedItem == null || ucPatient.SelectedItem.Id == default(int)) {
                MessageBox.Show(this, "Please select a Patient.");
                ucPatient.Focus();
                ucPatient.Select();
                return;
            }

            PatientListReportDTO patient = null;
            SortableBindingList<InvoiceItemListReportDTO> services = new SortableBindingList<InvoiceItemListReportDTO>();

            try {
                using(ITransaction tr = session.BeginTransaction()) {

                    //Get patient info
                    PatientListReportDTO dtoPatient = null;
                    Patient aPatient = null;
                    Insurer aInsurer = null;

                    patient = session.QueryOver<Patient>(() => aPatient)
                        .JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin)
                        .Where(() => aPatient.Id == ucPatient.SelectedItem.Id)
                        .SelectList(list => list
                            .Select(() => aPatient.FirstName).WithAlias(() => dtoPatient.FirstName)
                            .Select(() => aPatient.LastName).WithAlias(() => dtoPatient.LastName)
                            .Select(() => aInsurer.InsurerName).WithAlias(() => dtoPatient.Insurer)
                            .Select(() => aPatient.ContactInfo.CellPhone).WithAlias(() => dtoPatient.CellPhone)
                            .Select(() => aPatient.ContactInfo.Email).WithAlias(() => dtoPatient.Email)
                        )
                        .TransformUsing(Transformers.AliasToBean<PatientListReportDTO>())
                        .SingleOrDefault<PatientListReportDTO>();

                    //Get invoice items
                    Invoice aInvoice = null;
                    InvoiceItem aInvoiceItem = null;
                    InvoiceItemDTO dtoInvoiceItem = null;

                    var qry = session.QueryOver<Patient>(() => aPatient)
                        .JoinQueryOver<Invoice>(() => aPatient.Invoices, () => aInvoice, JoinType.InnerJoin)
                        .JoinQueryOver<InvoiceItem>(() => aInvoice.InvoiceItems, () => aInvoiceItem, JoinType.InnerJoin)
                        .Where(() => aPatient.Id == ucPatient.SelectedItem.Id);

                    if (dtFrom.Enabled) {
                        qry.Where(() => aInvoiceItem.ServiceDate >= dtFrom.Value.Date);
                    }
                    if (dtTo.Enabled) {
                        qry.Where(() => aInvoiceItem.ServiceDate < dtTo.Value.Date.AddDays(1));
                    }

                    var invItems = qry
                        .SelectList(list => list
                            .Select(() => aInvoiceItem.ServiceDate).WithAlias(() => dtoInvoiceItem.ServiceDate)
                            .Select(() => aInvoiceItem.ServiceDescription).WithAlias(() => dtoInvoiceItem.ServiceDescription)
                            .Select(() => aInvoiceItem.ServiceDuration).WithAlias(() => dtoInvoiceItem.ServiceDuration)
                            .Select(() => aInvoiceItem.Amount).WithAlias(() => dtoInvoiceItem.Amount)
                            .Select(() => aInvoice.TaxRate).WithAlias(() => dtoInvoiceItem.TaxRate)
                        )
                        .TransformUsing(Transformers.AliasToBean<InvoiceItemDTO>())
                        .OrderBy(() => aInvoiceItem.ServiceDate).Asc()
                        .List<InvoiceItemDTO>();

                    //Add patient list into SortableBindingList
                    int sequence = 1;
                    foreach (InvoiceItemDTO item in invItems) {
                        services.Add(new InvoiceItemListReportDTO {
                            Sequence = sequence++,
                            Date = item.ServiceDate.ToString("MMM dd, yyyy"),
                            ServiceProvided = item.ServiceDescription,
                            Duration = item.ServiceDuration?.ToString(),
                            TimeIn = item.ServiceDate.ToString("hh:mm tt"),
                            Amount = item.Amount.ToString("0.00"),
                            AmountAfterTax = (item.Amount * (1 + item.TaxRate)).ToString("0.00"),
                            Initial = string.Empty,
                            Note = string.Empty
                        });
                    }

                    //Comment transaction
                    tr.Commit();
                }

            } catch (Exception ex) {
                MessageBox.Show(this, ex.Message);
                Program.log.Error("Failed to retrieve patient info.", ex);
            }

            //Populate GridView
            GridView gv = new GridView();
            gv.DataSource = services;
            gv.DataBind();

            //Update column header text
            if (gv.HeaderRow != null) {
                foreach (TableCell headerCell in gv.HeaderRow.Cells) {
                    headerCell.Text = headerCell.Text.ToHumanReadableString();
                    if (headerCell.Text == "Sequence") {
                        headerCell.Text = "#";
                    }
                }
            }

            //Export to Excel
            string filePath = Path.Combine(Path.GetTempPath(), $"Services_{patient.LastName}_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.xls");

            //Report header with patient info
            StringBuilder sbReportHeader = new StringBuilder();
            sbReportHeader.Append("<table style=\"font-weight:bold;\">");
            sbReportHeader.Append("<tr>");
            sbReportHeader.Append($"<td colspan=\"4\">Name of Client: {patient.FirstName} {patient.LastName}</td>");
            sbReportHeader.Append($"<td colspan=\"5\">Insurance Company: {patient.Insurer}</td>");
            sbReportHeader.Append("</tr>");
            sbReportHeader.Append("<tr>");
            sbReportHeader.Append($"<td colspan=\"4\">Email: {patient.Email}</td>");
            sbReportHeader.Append($"<td colspan=\"5\">Cell: {patient.CellPhone.Trim(new char[] { ' ', '(', ')', '-' })}</td>");
            sbReportHeader.Append("</tr>");
            sbReportHeader.Append("</table>");

            FileHelper.ExportExcel(gv, filePath, "Sunsmile Rehab Centre", sbReportHeader.ToString());
        }

        private void btnExportAllPatients_Click(object sender, EventArgs e) {

            //Prepare a SortableBindingList for patient grid
            SortableBindingList<PatientListReportDTO> patients = new SortableBindingList<PatientListReportDTO>();

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    PatientDTO dtoPatient = null;
                    Patient aPatient = null;
                    Insurer aInsurer = null;

                    var qry = session.QueryOver<Patient>(() => aPatient)
                        .JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin);

                    //Retrieve patient list
                    var patientList = (qry.Clone())
                        .SelectList(list => list
                            .Select(() => aPatient.Id).WithAlias(() => dtoPatient.Id)
                            .Select(() => aPatient.Version).WithAlias(() => dtoPatient.Version)
                            .Select(() => aPatient.FamilyPatientId).WithAlias(() => dtoPatient.FamilyPatientId)
                            .Select(() => aPatient.Sex).WithAlias(() => dtoPatient.Sex)
                            .Select(() => aPatient.FileNumber).WithAlias(() => dtoPatient.FileNumber)
                            .Select(() => aPatient.FirstName).WithAlias(() => dtoPatient.FirstName)
                            .Select(() => aPatient.LastName).WithAlias(() => dtoPatient.LastName)
                            .Select(() => aPatient.MiddleName).WithAlias(() => dtoPatient.MiddleName)
                            .Select(() => aPatient.DateOfBirth).WithAlias(() => dtoPatient.DateOfBirth)

                            .Select(() => aPatient.ContactInfo.Address.AddressLine1).WithAlias(() => dtoPatient.Address1)
                            .Select(() => aPatient.ContactInfo.Address.AddressLine2).WithAlias(() => dtoPatient.Address2)
                            .Select(() => aPatient.ContactInfo.Address.PostalCode).WithAlias(() => dtoPatient.PostalCode)
                            .Select(() => aPatient.ContactInfo.HomePhone).WithAlias(() => dtoPatient.HomePhone)
                            .Select(() => aPatient.ContactInfo.CellPhone).WithAlias(() => dtoPatient.CellPhone)
                            .Select(() => aPatient.ContactInfo.HomeFax).WithAlias(() => dtoPatient.HomeFax)
                            .Select(() => aPatient.ContactInfo.Email).WithAlias(() => dtoPatient.Email)

                            .Select(() => aPatient.Note).WithAlias(() => dtoPatient.Note)
                            .Select(() => aInsurer.InsurerName).WithAlias(() => dtoPatient.Insurer)
                        )
                        .TransformUsing(Transformers.AliasToBean<PatientDTO>())
                        .OrderBy(() => aPatient.FirstName).Asc()
                        .ThenBy(() => aPatient.LastName).Asc()
                        .ThenBy(() => aPatient.Id).Asc()
                        .List<PatientDTO>();
                        
                    //Add patient list into SortableBindingList
                    foreach (PatientDTO p in patientList) {
                        patients.Add(new PatientListReportDTO {
                            FileNumber = p.FileNumber,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Address = (
                                (p.Address1 == null || p.Address1.Trim().Length <= 0 ? string.Empty : p.Address1 + ", \n") +
                                (p.Address2 == null || p.Address2.Trim().Length <= 0 ? string.Empty : p.Address2 + ", \n") +
                                (p.PostalCode == null || p.PostalCode.Trim().Length <= 0 ? string.Empty : p.PostalCode)
                                ).Trim(new char[] { ' ', ',', '\n' }),
                            CellPhone = p.CellPhone != null && p.CellPhone.Trim(new char[] { ' ', '-', '(', ')' }).Length > 0 ? p.CellPhone : string.Empty,
                            HomePhone = p.HomePhone != null && p.HomePhone.Trim(new char[] { ' ', '-', '(', ')' }).Length > 0 ? p.HomePhone : string.Empty,
                            Email = p.Email,
                            Sex = p.Sex == Sex.Unknown ? string.Empty : p.Sex.ToString(),
                            DateOfBirth = p.DateOfBirth == default(DateTime) ? "" : p.DateOfBirth.ToString("MMM dd, yyyy"),
                            Insurer = p.Insurer
                        });
                    }

                    //Comment transaction
                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, ex.Message);
                Program.log.Error("Failed to retrieve all patients.", ex);
            } finally {
            }

            //Populate GridView
            GridView gv = new GridView();
            gv.DataSource = patients;
            gv.DataBind();

            //Update column header text
            foreach(TableCell headerCell in gv.HeaderRow.Cells) {
                headerCell.Text = headerCell.Text.ToHumanReadableString();
            }

            //Set grid view style
            gv.RowStyle.BackColor = Color.White;
            gv.AlternatingRowStyle.BackColor= Color.LightGoldenrodYellow;

            //Export to Excel
            string filePath = Path.Combine(Path.GetTempPath(), $"Patients_{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.xls");

            FileHelper.ExportExcel(gv, filePath, "Client Information");
        }

        private void chkFromDate_CheckedChanged(object sender, EventArgs e) {
            enableDates();
        }

        private void chkToDate_CheckedChanged(object sender, EventArgs e) {
            enableDates();
        }
    }
}
