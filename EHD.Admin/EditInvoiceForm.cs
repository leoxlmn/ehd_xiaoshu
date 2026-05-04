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
using EHD.FormUtitlity;
using Utility;
using WordFileProcessor;
using NHibernate.Criterion;
using HolidayCalculator;
using NHibernate.Transform;
using NHibernate.SqlCommand;
using NHibernate.Event.Default;

namespace EHD.Admin {
    public partial class EditInvoiceForm : Form {

        private bool _viewOnly = false;
        private Patient _patient = null;
        private PleaseWaitForm _waitForm = null;

        //Open session for the form
        private Guid _sessionKey = Guid.NewGuid();
        private ISession session;

        public Invoice EditingInvoice { get; set; }

        public EditInvoiceForm(int invoiceId, bool ViewOnly = false) {

            //Open session
            session = SessionFactory.GetOpenSession(_sessionKey);

            _viewOnly = ViewOnly;

            InitializeComponent();

            //Populate dropdownlists
            populateDropdowns();

            //Set default tax
            setDefaultTax();

            using (ITransaction tr = session.BeginTransaction()) {
                EditingInvoice = session.QueryOver<Invoice>()
                    .Where(x => x.Id == invoiceId)
                    //.Fetch(x => x.Patient).Eager
                    //.Fetch(x => x.InvoiceItems).Eager
                    .SingleOrDefault<Invoice>();

                tr.Commit();
            }
        }

        public EditInvoiceForm(object nullHolder, int patientId) {

            //Open session
            session = SessionFactory.GetOpenSession(_sessionKey);

            InitializeComponent();

            //Populate dropdownlists
            populateDropdowns();

            //Set default tax
            setDefaultTax();

            using (ITransaction tr = session.BeginTransaction()) {
                _patient = session.Get<Patient>(patientId);
                tr.Commit();
            }

        }

        private void populateDropdowns() {
            try {
                IList<SimpleUserItem> lstTherapist = null;
                IList<SimpleListItem> lstTreatmentType = null;

                using (ITransaction tr = session.BeginTransaction()) {

                    SimpleListItem aliasListItem = null;
                    SimpleUserItem aliasUserItem = null;
                    User aUser = null;
                    UserTitle aTitle = null;

                    var qryTreatmentType = session.QueryOver<TherapyType>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.TherapyTypeName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .Future<SimpleListItem>();

                    var qryTherapist = session.QueryOver<User>(() => aUser)
                        .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                        .Where(() => aUser.UserType == UserType.Therapist)
                        .SelectList(list => list
                            .Select(() => aUser.Id).WithAlias(() => aliasUserItem.Id)
                            .Select(() => aTitle.Name).WithAlias(() => aliasUserItem.Title)
                            .Select(() => aUser.FirstName).WithAlias(() => aliasUserItem.FirstName)
                            .Select(() => aUser.LastName).WithAlias(() => aliasUserItem.LastName)
                            .Select(() => aUser.MiddleName).WithAlias(() => aliasUserItem.MiddleName)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleUserItem>())
                        .OrderBy(() => aUser.FirstName).Asc
                        .OrderBy(() => aUser.LastName).Asc
                        .Take(Program.config.MaxTherapists)
                        .Future<SimpleUserItem>();

                    lstTreatmentType = qryTreatmentType.ToList();
                    lstTreatmentType.Insert(0, new SimpleListItem());

                    lstTherapist = qryTherapist.ToList();
                    lstTherapist.Insert(0, new SimpleUserItem());

                    tr.Commit();
                }

                drpTreatmentType.SelectedIndexChanged -= drpTreatmentType_SelectedIndexChanged;
                drpTreatmentType.DataSource = lstTreatmentType;
                drpTreatmentType.ValueMember = "Id";
                drpTreatmentType.DisplayMember = "Text";
                drpTreatmentType.SelectedIndexChanged += drpTreatmentType_SelectedIndexChanged;

                drpTherapist.SelectedIndexChanged -= drpTherapist_SelectedIndexChanged;
                drpTherapist.DataSource = lstTherapist;
                drpTherapist.ValueMember = "Id";
                drpTherapist.DisplayMember = "DisplayName";
                drpTherapist.SelectedIndexChanged += drpTherapist_SelectedIndexChanged;

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Therapist/Treatment Type List. " + ex.Message);
                Program.log.Error("Failed to load Therapist/Treatment Type List.", ex);
            }
        }

        private void populateRegistrationList() {

            //Clear registration text
            txtRegistration.Text = string.Empty;

            //Get selected Therapist Id
            SimpleUserItem therapist = drpTherapist.SelectedItem as SimpleUserItem;
            if (therapist == null || therapist.Id == 0) return;

            try {
                IList<TherapistOrganizationRegistration> lstRegistration = null;
                
                using (ITransaction tr = session.BeginTransaction()) {

                    lstRegistration =
                        session.QueryOver<TherapistOrganizationRegistration>()
                        .Fetch(x => x.Organization).Eager
                        .Fetch(x => x.RegistrationGroup).Eager
                        .Fetch(x => x.RegistrationGroup.Therapist).Eager
                        .Where(x => x.Therapist.Id == therapist.Id)
                        .OrderBy(x => x.Organization).Asc
                        .List<TherapistOrganizationRegistration>();

                    tr.Commit();
                }

                //Add an empty Registration to the dropdown
                lstRegistration.Insert(0, new TherapistOrganizationRegistration());

                drpRegistration.SelectedIndexChanged -= drpRegistration_SelectedIndexChanged;
                drpRegistration.DataSource = lstRegistration;
                drpRegistration.ValueMember = "Id";
                drpRegistration.DisplayMember = "DisplayName";
                drpRegistration.SelectedIndexChanged += drpRegistration_SelectedIndexChanged;

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Therapist Organization Registration List. " + ex.Message);
                Program.log.Error("Failed to load Therapist Organization Registration List.", ex);
            }
        }

        private void populateSelectedRegistrationList() {
            //Get selected Registration Id
            int regId = -1;
            if(drpRegistration.SelectedValue != null) {
                int.TryParse(drpRegistration.SelectedValue.ToString(), out regId);
            }
            if(regId < 0) return;

            try {
                IList<TherapistOrganizationRegistration> lstRegistration = null;

                using (ITransaction tr = session.BeginTransaction()) {

                    var dc = DetachedCriteria.For<TherapistOrganizationRegistration>()
                        .SetProjection(Projections.Property("RegistrationGroup.Id"))
                        .Add(Restrictions.Eq("Id", regId));
                    
                    lstRegistration =
                        session.CreateCriteria<TherapistOrganizationRegistration>("reg")
                        .Add(Subqueries.PropertyEq("RegistrationGroup.Id", dc))
                        .CreateCriteria("reg.Therapist")
                        .CreateCriteria("reg.Organization")
                        .List<TherapistOrganizationRegistration>();

                    StringBuilder sb = new StringBuilder();
                    foreach (TherapistOrganizationRegistration reg in lstRegistration) {
                        sb.AppendLine(reg.RegistrationNumber + " - " + reg.Organization.OrganizationName);
                    }
                    txtRegistration.Text = sb.ToString();

                    tr.Commit();
                }

                /*
                lstSelectedRegistration.DataSource = lstRegistration;
                lstSelectedRegistration.ValueMember = "Id";
                lstSelectedRegistration.DisplayMember = "DisplayName";*/

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Selected Registration List. " + ex.Message);
                Program.log.Error("Failed to load Selected Registration List.", ex);
            }
        }

        private void loadInvoice() {
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    if (EditingInvoice == null || EditingInvoice.Id == default(int)) {

                        //Get Invoice Title, HST number form system setting
                        JCService service = new JCService(session, Program.LogonUser);
                        SystemSetting title = service.GetSettingBy(Constants.SETTING_NAME_INVOICE_TITLE);
                        SystemSetting HSTNumber = service.GetSettingBy(Constants.SETTING_NAME_HST_NUMBER);

                        EditingInvoice = new Invoice();
                        EditingInvoice.Title = (title != null ? title.Value : string.Empty);
                        EditingInvoice.HSTNumber = (HSTNumber != null ? HSTNumber.Value : string.Empty);
                        EditingInvoice.Patient = _patient;
                        EditingInvoice.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingInvoice.CreatedBy = Program.LogonUser.DisplayName;
                        EditingInvoice.CreatedTime = DateTime.Now;
                    } else {
                        EditingInvoice = session.QueryOver<Invoice>()
                            .Where(x => x.Id == EditingInvoice.Id)
                            .Fetch(x => x.Patient).Eager
                            .Fetch(x => x.Therapist).Eager
                            .Fetch(x => x.TreatmentType).Eager
                            .SingleOrDefault();
                    }
                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Invoice. " + ex.Message);
                    Program.log.Error("Failed to load Invoice.", ex);
                }
            }

            populateInvoiceDetail(EditingInvoice);
            btnAddInvoiceItem.Visible = (EditingInvoice != null && EditingInvoice.Id != default(int));
            btnDeleteInvoiceItem.Visible = btnAddInvoiceItem.Visible;
            btnEditInvoiceItem.Visible = btnAddInvoiceItem.Visible;
        }

        private void populateInvoiceDetail(Invoice invoice) {
            if (invoice != null) {
                txtInvoiceTitle.Text = invoice.Title;
                if (invoice.Patient != null) {
                    lblBillTo.Text = invoice.Patient.BillingAddress;
                }
                if (invoice.StatementDate != null && invoice.StatementDate != default(DateTime)) {
                    dtStatementDate.Value = invoice.StatementDate.Date;
                }
                txtInvoiceNumber.Text = invoice.InvoiceNumber;
                txtHSTNumber.Text = invoice.HSTNumber;
                setTherapist(invoice.Therapist);
                setTreatmentType(invoice.TreatmentType);
                //setRegistration(invoice.RegistrationGroup);
                txtRegistration.Text = invoice.TherapistRegistration;
                txtInvoiceNote.Text = invoice.Note;
                lblCreatedBy.Text = invoice.CreatedBy;
                lblCreatedTime.Text = invoice.CreatedTime.ToString(Constants.DATE_TIME_FORMAT);
                lblUpdatedBy.Text = invoice.UpdatedBy;
                lblUpdatedTime.Text = (invoice.UpdatedTime.HasValue ?
                    invoice.UpdatedTime.Value.ToString(Constants.DATE_TIME_FORMAT) :
                    string.Empty);

                if (invoice.TaxRate.HasValue) {
                    txtTaxName.Text = invoice.TaxName;
                    numTaxPercent.Value = (decimal)invoice.TaxRate.Value * 100;
                }

                populateInvoiceItems(invoice);
            }
        }

        private void populateInvoiceItems(Invoice invoice) {

            if (invoice.Id == 0) return;

            IList<InvoiceItem> lstInvoiceItem = null;

            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    var qry = session.QueryOver<InvoiceItem>()
                        .Where(x => x.Invoice.Id == invoice.Id)
                        .OrderBy(x => x.ServiceDate).Asc
                        .Future<InvoiceItem>();
                    //Force to load collections
                    session.QueryOver<Invoice>()
                        .Where(x => x.Id == invoice.Id)
                        .Fetch(x => x.Patient).Eager
                        .Fetch(x => x.Patient.Insurer).Eager
                        .Fetch(x => x.Therapist).Eager
                        .Fetch(x => x.TreatmentType).Eager
                        .Future();
                    //session.QueryOver<UserTitle>().Future();

                    lstInvoiceItem = qry.ToList();

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, ex.Message);
                    Program.log.Error("Populate Invoice Item List error.", ex);
                }
            }

            SortableBindingList<InvoiceItem> invoiceItems = new SortableBindingList<InvoiceItem>();
            double subTotal = 0.0;
            foreach (InvoiceItem item in lstInvoiceItem) {
                invoiceItems.Add(item);
                subTotal += item.Amount;
            }

            grdInvoiceItem.DataSource = invoiceItems;
            lblSubTotal.Text = string.Format("{0:C2}", subTotal);
            calcuateTaxAndTotal(subTotal);
        }

        private void setTherapist(User therapist) {
            if(therapist == null) return;

            drpTherapist.SelectedValue = therapist.Id;
        }

        private void setTreatmentType(TherapyType therapyType) {
            if(therapyType == null) return;

            drpTreatmentType.SelectedValue = therapyType.Id;
        }

        private void updateInvoiceDetailInput(Invoice invoice) {
            invoice.Title = txtInvoiceTitle.Text.Trim();
            invoice.Note = txtInvoiceNote.Text.Trim();
            invoice.StatementDate = dtStatementDate.Value.Date;
            invoice.InvoiceNumber = txtInvoiceNumber.Text.Trim();
            invoice.HSTNumber = txtHSTNumber.Text.Trim();
            invoice.TherapistRegistration = txtRegistration.Text.Trim();
            invoice.TaxName = txtTaxName.Text.Trim();
            invoice.TaxRate = (double)numTaxPercent.Value / 100.0;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {

            if (EditingInvoice == null) return;

            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    if (EditingInvoice.Id != default(int)) {//To update
                        /*
                        EditingInvoice = //session.Merge<Invoice>(EditingInvoice);
                            session.Get<Invoice>(EditingInvoice.Id);*/
                        EditingInvoice.UpdatedTime = DateTime.Now;
                        EditingInvoice.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    //update with dropdown selection
                    EditingInvoice.Therapist = session.Load<User>((drpTherapist.SelectedItem as SimpleUserItem).Id);
                    EditingInvoice.TreatmentType = session.Load<TherapyType>((drpTreatmentType.SelectedItem as SimpleListItem).Id);
                    updateInvoiceDetailInput(EditingInvoice);

                    if (!EditingInvoice.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        tr.Rollback();
                        return;
                    }

                    #region Rewrote using JCService.CheckPractitionerAvailability function below
                    /*
                    //Check if it's holiday
                    if (CanadaHolidays.IsHoliday(EditingInvoice.StatementDate.Date)) {
                        if (DialogResult.Yes != MessageBox.Show(this,
                            string.Format(Constants.WARNING_HOLIDAY, EditingInvoice.StatementDate.Date),
                            "Holiday Warning",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning)) {
                            dtStatementDate.Focus();
                            dtStatementDate.Select();

                            tr.Rollback();
                            return;
                        }
                    }

                    //Check if the practitioner is off
                    if (Program.config.EnablePractitionerCalendar) {

                        bool isAvailable = false;

                        int availCount = session.QueryOver<UserWeeklyAvailability>()
                            .Where(x => x.Practitioner.Id == EditingInvoice.Therapist.Id)
                            .Where(x => x.WeekDay == EditingInvoice.StatementDate.DayOfWeek)
                            .RowCount();

                        if (availCount > 0) {
                            isAvailable = true;

                            //Continue to check time offs
                            DateTime dtCheckStart = EditingInvoice.StatementDate.Date;
                            DateTime dtCheckEnd = dtCheckStart.AddDays(1);
                            int timeOffCount = session.QueryOver<UserTimeOff>()
                                .Where(x => x.Practitioner.Id == EditingInvoice.Therapist.Id)
                                .Where(x => (x.StartTime >= dtCheckStart && x.EndTime <= dtCheckEnd) ||
                                            (x.StartTime < dtCheckStart && x.EndTime > dtCheckStart) ||
                                            (x.StartTime < dtCheckEnd && x.EndTime > dtCheckEnd))
                                .RowCount();

                            if (timeOffCount > 0) {
                                isAvailable = false;
                            }

                            if (!isAvailable) {
                                if (DialogResult.OK != MessageBox.Show(this,
                                    Constants.WARNING_TIME_OFF,
                                    "Time Off Warning",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Warning)) {
                                    dtStatementDate.Focus();
                                    dtStatementDate.Select();

                                    tr.Rollback();
                                    return;
                                }
                            }

                        } else {
                            if (DialogResult.OK != MessageBox.Show(this,
                                Constants.WARNING_NOT_WORKING_TIME,
                                "Working Hour Warning",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)) {
                                dtStatementDate.Focus();
                                dtStatementDate.Select();

                                tr.Rollback();
                                return;
                            }
                        }
                    }*/
                    #endregion Rewrote using JCService.CheckPractitionerAvailability function below

                    #region Check practitioner availability

                    JCService service = new JCService(session, Program.LogonUser);

                    List<JCService.AvailableStatus> pracStatus = service.CheckPractitionerAvailability(
                        EditingInvoice.Therapist,
                        EditingInvoice.StatementDate.Date,
                        EditingInvoice.StatementDate.Date.AddDays(1),
                        JCService.AvailabilityCheckType.Invoice);

                    if (Program.config.EnablePractitionerCalendar) {
                        if (pracStatus.Contains(JCService.AvailableStatus.NoneWorkingHour)) {
                            if (DialogResult.OK != MessageBox.Show(this,
                                Constants.WARNING_NOT_WORKING_TIME,
                                "Working Hour Warning",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)) {
                                dtStatementDate.Focus();
                                dtStatementDate.Select();

                                tr.Rollback();
                                return;
                            }
                        }

                        if (pracStatus.Contains(JCService.AvailableStatus.TimeOff)) {
                            if (DialogResult.OK != MessageBox.Show(this,
                                Constants.WARNING_TIME_OFF,
                                "Time Off Warning",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning)) {
                                dtStatementDate.Focus();
                                dtStatementDate.Select();

                                tr.Rollback();
                                return;
                            }
                        }
                    }

                    if (pracStatus.Contains(JCService.AvailableStatus.Holiday)) {
                        if (DialogResult.Yes != MessageBox.Show(this,
                            string.Format(Constants.WARNING_HOLIDAY, EditingInvoice.StatementDate.Date),
                            "Holiday Warning",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning)) {
                            dtStatementDate.Focus();
                            dtStatementDate.Select();

                            tr.Rollback();
                            return;
                        }
                    }

                    #endregion Check practitioner availability

                    session.Save(EditingInvoice);
                    tr.Commit();

                    MessageBox.Show(this, "Information Updated.");
                        
                    //Do not close Invoice form yet, if it's a new Invoice which needs the invoice items to be entered.
                    if (!btnAddInvoiceItem.Visible) {
                        //Enable Invoice Items buttons
                        btnAddInvoiceItem.Visible = true;
                        btnEditInvoiceItem.Visible = true;
                        btnDeleteInvoiceItem.Visible = true;
                    } else {
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save Invoice. " + ex.Message);
                    Program.log.Error("Failed to save Invoice.", ex);
                }
            }
        }

        private void EditInvoiceForm_Load(object sender, EventArgs e) {

            loadInvoice();

            if(_viewOnly) {
                //Set all children readonly
                FormHelper helper = new FormHelper(Constants.DATE_TIME_FORMAT);
                helper.SetControlsReadonly(this);

                //Hide Save button
                btnSave.Visible = false;
                btnAddInvoiceItem.Visible = false;
                btnDeleteInvoiceItem.Visible = false;
                btnEditInvoiceItem.Visible = false;
                lnkManageRegistration.Enabled = false;
                btnSelectNote.Visible = false;
                btnSetTax.Visible = false;
            }
        }

        private void drpRegistration_SelectedIndexChanged(object sender, EventArgs e) {
            populateSelectedRegistrationList();
        }

        private void drpTherapist_SelectedIndexChanged(object sender, EventArgs e) {
            populateRegistrationList();
        }

        private void btnAddInvoiceItem_Click(object sender, EventArgs e) {
            if(EditingInvoice.Id == default(int)) {
                MessageBox.Show(this, "Please save the Invoice before adding Invoice Items.");
                return;
            }

            if(DialogResult.OK == (new EditInvoiceItemForm(null, EditingInvoice.Id)).ShowDialog()) {
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                loadInvoice();
            }
        }

        private void lnkManageRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            (new ManageTherapistRegistrationForm()).ShowDialog(this);
            populateRegistrationList();
        }

        private void openToWord() {
            if (EditingInvoice == null) return;

            //Decide which template to use
            /*
            string templateName = Constants.CFG_TEMPLATE_FILE_INVOICE;
            try {
                using (ISession session = SessionFactory.GetSessionFactory().OpenSession()) 
                using (ITransaction tr = session.BeginTransaction()) {
                    
                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to build Invoice Request. " + ex.Message);
                Program.log.Error("Failed to build Invoice Request.", ex);
            }*/


            WordExportHelper wordExporter = null;

            wordExporter = new WordExportHelper(
                EditingInvoice,
                (EditingInvoice.TreatmentType.ShowDurationOnInvoice ? Constants.CFG_TEMPLATE_FILE_INVOICE_WITH_SERVICE_DURATION : Constants.CFG_TEMPLATE_FILE_INVOICE),
                new PopulateExportRequestDelegate(buildInvoiceRequest),
                new ProgressReporterDelegate(wordProgressReporter));
            wordExporter.Export();
            //wordExporter.Export(true);
            if (wordExporter.HasError) {
                MessageBox.Show(wordExporter.ErrorMessage);
            } else {
                wordExporter.OpenWordFile(wordExporter.WordExported);
            }
        }

        private void buildInvoiceRequest(ProcessRequest req, object obj) {
            Invoice invoice = obj as Invoice;

            req.AddWordReplacement("{title}", invoice.Title);
            req.AddWordReplacement("{bill to}", invoice.Patient.BillingAddress);
            req.AddWordReplacement("{bill to name}", invoice.Patient.DisplayName);
            req.AddWordReplacement("{bill to address}", invoice.Patient.ContactInfo != null && invoice.Patient.ContactInfo.Address != null ?
                invoice.Patient.ContactInfo.Address.ToString() : 
                "");
            req.AddWordReplacement("{statement date}", invoice.StatementDate.ToString(Constants.DATE_FORMAT));
            req.AddWordReplacement("{invoice number}", invoice.InvoiceNumber);
            req.AddWordReplacement("{therapist name}", invoice.Therapist.DisplayName);
            req.AddWordReplacement("{register number}", formatRegistrationNumber(invoice.TherapistRegistration));
            req.AddWordReplacement("{registered organization}", formatRegistrationOrg(invoice.TherapistRegistration));
            req.AddWordReplacement("{invoice note}", invoice.Note);
            double total = getInvoiceTotal(invoice);

            if (invoice.TaxRate.HasValue && invoice.TaxRate.Value > 0) {
                req.AddWordReplacement("{inv total lbl 1}", "Sub Total:");
                req.AddWordReplacement("{inv total 1}", string.Format("{0:N2}", total));
                req.AddWordReplacement("{inv total lbl 2}", invoice.TaxName + ":");
                req.AddWordReplacement("{inv total 2}", string.Format("{0:N2}", total * invoice.TaxRate.Value));
                req.AddWordReplacement("{inv total lbl 3}", "Total:");
                req.AddWordReplacement("{inv total 3}", string.Format("{0:N2}", roundMoney(total * (1 + invoice.TaxRate.Value))));
            } else {
                req.AddWordReplacement("{inv total lbl 1}", "");
                req.AddWordReplacement("{inv total 1}", "");
                req.AddWordReplacement("{inv total lbl 2}", "Total:");
                req.AddWordReplacement("{inv total 2}", string.Format("{0:N2}", roundMoney(total)));
                req.AddWordReplacement("{inv total lbl 3}", "");
                req.AddWordReplacement("{inv total 3}", "");
            }

            if (invoice.HSTNumber != null && invoice.HSTNumber.Trim().Length > 0) {
                req.AddWordReplacement("{HST number}", invoice.HSTNumber);
                req.AddWordReplacement("{HST label}", "");
            } else {//Delete textboxes for both HST label and HST number
                req.Replacements.Add("{HST number}", new List<DeleteReplacement> { new DeleteReplacement("TextBox") });
                req.Replacements.Add("{HST label}", new List<DeleteReplacement> { new DeleteReplacement("TextBox") });
            }

            //Invoice Items

            StringBuilder sbServiceDate = new StringBuilder();
            StringBuilder sbServiceType = new StringBuilder();
            StringBuilder sbServiceAmount = new StringBuilder();
            StringBuilder sbServiceDuration = new StringBuilder();

            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    /*
                    List<InvoiceItem> lstInvoiceItems = session.QueryOver<InvoiceItem>()
                        .List<InvoiceItem>()
                        .Where<InvoiceItem>(x => x.Invoice.Id == invoice.Id)
                        .OrderBy(x => x.ServiceDate)
                        .ToList<InvoiceItem>();*/
                    IList<InvoiceItem> lstInvoiceItems = session.QueryOver<InvoiceItem>()
                        .Where(x => x.Invoice.Id == invoice.Id)
                        .OrderBy(x => x.ServiceDate).Asc
                        .List<InvoiceItem>();

                    foreach (InvoiceItem item in lstInvoiceItems) {
                        sbServiceDate.AppendLine(item.ServiceDate.ToString(Constants.DATE_FORMAT));
                        sbServiceType.AppendLine(item.ServiceDescription);
                        sbServiceAmount.AppendLine(string.Format("{0:N2}", item.Amount));
                        sbServiceDuration.AppendLine(string.Format("{0}", item.ServiceDuration));
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to build Invoice Request. " + ex.Message);
                Program.log.Error("Failed to build Invoice Request.", ex);
            }

            req.AddWordReplacement("{service date list}", sbServiceDate.ToString());
            req.AddWordReplacement("{service description list}", sbServiceType.ToString());
            req.AddWordReplacement("{amount list}", sbServiceAmount.ToString());
            req.AddWordReplacement("{service duration list}", sbServiceDuration.ToString());
        }
        /*
        private string formatRegistrationNumber(Invoice invoice) {
            StringBuilder sb = new StringBuilder();
            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    var lstRegistration = session.QueryOver<TherapistOrganizationRegistration>()
                        .Where(x => x.RegistrationGroup.Id == invoice.RegistrationGroup.Id)
                        .List<TherapistOrganizationRegistration>();

                    foreach (TherapistOrganizationRegistration reg in lstRegistration) {
                        sb.AppendLine(reg.RegistrationNumber);
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to set Registration. " + ex.Message);
                Program.log.Error("Failed to set Registration.", ex);
            }

            return sb.ToString();
        }

        private string formatRegistrationOrg(Invoice invoice) {
            StringBuilder sb = new StringBuilder();
            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    var lstRegistration = session.QueryOver<TherapistOrganizationRegistration>()
                        .Where(x => x.RegistrationGroup.Id == invoice.RegistrationGroup.Id)
                        .List<TherapistOrganizationRegistration>();

                    foreach (TherapistOrganizationRegistration reg in lstRegistration) {
                        sb.AppendLine(reg.Organization.DisplayName);
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to set Registration. " + ex.Message);
                Program.log.Error("Failed to set Registration.", ex);
            }

            return sb.ToString();
        }*/

        /// <summary>
        /// The registration info should be in format: 
        ///     xxxxx - nnnnn
        ///     xxxxx - nnnnn
        /// xxxxx: registration number
        /// nnnnn: organization name
        /// It may contain multiple lines
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        private string formatRegistrationNumber(string registration) {
            StringBuilder sbRegNumber = new StringBuilder();

            if (registration != null) {
                string[] lines = registration.Split('\n');
                foreach (string line in lines) {
                    if (line != null) {
                        string[] regInfo = line.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                        if (regInfo != null && regInfo.Length > 0) {
                            sbRegNumber.AppendLine(regInfo[0]);
                        }
                    }
                }
            }

            return sbRegNumber.ToString();
        }
        private string formatRegistrationOrg(string registration) {
            StringBuilder sbRegNumber = new StringBuilder();

            if (registration != null) {
                string[] lines = registration.Split('\n');
                foreach (string line in lines) {
                    if (line != null) {
                        string[] regInfo = line.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                        if (regInfo != null && regInfo.Length > 1) {
                            sbRegNumber.AppendLine(regInfo[1]);
                        }
                    }
                }
            }

            return sbRegNumber.ToString();
        }

        private string formatInvoiceTotal(Invoice invoice) {
            double total = 0;

            var lstInvoiceItem = session.QueryOver<InvoiceItem>()
                .Where(x => x.Invoice.Id == invoice.Id)
                .List<InvoiceItem>();

            foreach (InvoiceItem invItem in lstInvoiceItem) {
                total += invItem.Amount;
            }

            return string.Format("{0:N2}", total);
        }

        private double getInvoiceTotal(Invoice invoice) {
            return session.QueryOver<InvoiceItem>()
                .Where(x => x.Invoice.Id == invoice.Id)
                .Select(Projections.Sum<InvoiceItem>(x => x.Amount))
                .UnderlyingCriteria.UniqueResult<double>();
        }

        private void btnOpenToWord_Click(object sender, EventArgs e) {

            if (EditingInvoice == null || EditingInvoice.Id == default(int)) {
                MessageBox.Show(this,
                    "Please save the new Invoice before opening to Word.",
                    "Warning",
                    MessageBoxButtons.OK);
                return;
            }

            //Start the printing process
            printWorker.RunWorkerAsync();

            //Show waiting form
            if (_waitForm == null) {
                _waitForm = new PleaseWaitForm();
            }
            _waitForm.ShowDialog(this);
        }

        private void printWorker_DoWork(object sender, DoWorkEventArgs e) {
            openToWord();
        }

        private void printWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (_waitForm != null) {
                _waitForm.Close();
                _waitForm.Dispose();
                _waitForm = null;
            }
        }

        private void wordProgressReporter(string msg, double percent) {
            if (_waitForm != null) {
                _waitForm.SetProgress(msg, percent);
            }
        }

        private void btnSelectNote_Click(object sender, EventArgs e) {
            SelectInvoiceNoteForm frmNote = new SelectInvoiceNoteForm();
            if (DialogResult.OK == frmNote.ShowDialog(this)) {
                txtInvoiceNote.Text = frmNote.SelectedInvoiceNote;
            }
        }

        private void grdInvoiceItem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Show/hide columns
            if (grdInvoiceItem.Columns != null) {
                if (grdInvoiceItem.Columns.Contains("Id")) grdInvoiceItem.Columns["Id"].Visible = false;
                if (grdInvoiceItem.Columns.Contains("IsTestData")) grdInvoiceItem.Columns["IsTestData"].Visible = false;
                //if (grdInvoiceItem.Columns.Contains("CreatedBy")) grdInvoiceItem.Columns["CreatedBy"].Visible = false;
                //if (grdInvoiceItem.Columns.Contains("UpdatedBy")) grdInvoiceItem.Columns["UpdatedBy"].Visible = false;
                //if (grdInvoiceItem.Columns.Contains("CreatedTime")) grdInvoiceItem.Columns["CreatedTime"].Visible = false;
                //if (grdInvoiceItem.Columns.Contains("UpdatedTime")) grdInvoiceItem.Columns["UpdatedTime"].Visible = false;
                if (grdInvoiceItem.Columns.Contains("Version")) grdInvoiceItem.Columns["Version"].Visible = false;
                if (grdInvoiceItem.Columns.Contains("Invoice")) grdInvoiceItem.Columns["Invoice"].Visible = false;
                if (grdInvoiceItem.Columns.Contains("DisplayName")) grdInvoiceItem.Columns["DisplayName"].Visible = false;
            }
        }

        private void dtStatementDate_ValueChanged(object sender, EventArgs e) {
            //Populate invoice number
            populateNewInvoiceNumber();
        }

        private void drpTreatmentType_SelectedIndexChanged(object sender, EventArgs e) {
            //Populate invoice number
            populateNewInvoiceNumber();
        }

        private void populateNewInvoiceNumber() {

            //No need to generate a new invoice number for an existing invoice
            if (EditingInvoice == null ||EditingInvoice.Id != default(int)) return;

            if (_patient == null || drpTreatmentType.SelectedItem == null || (drpTreatmentType.SelectedItem as SimpleListItem).Id == 0) return;

            string treatmentType = (drpTreatmentType.SelectedItem as SimpleListItem).Text;

            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    JCService service = new JCService(session, Program.LogonUser);
                    txtInvoiceNumber.Text = service.GetNextInvoiceNumber(
                        _patient.FileNumber,
                        treatmentType,
                        dtStatementDate.Value);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to generate a new invoice number. " + ex.Message);
                    Program.log.Error("Failed to generate a new invoice number.", ex);
                }
            }
        }

        private void grdInvoiceItem_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (_viewOnly) return;

            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;

            InvoiceItem invItem = grdInvoiceItem.Rows[rowIndex].DataBoundItem as InvoiceItem;
            if(DialogResult.OK == (new EditInvoiceItemForm(invItem.Id)).ShowDialog(this)) {
                loadInvoice();
            }
        }

        private void btnDeleteInvoiceItem_Click(object sender, EventArgs e) {
            if (grdInvoiceItem.SelectedRows.Count <= 0) return;

            InvoiceItem invItemToDelete = grdInvoiceItem.SelectedRows[0].DataBoundItem as InvoiceItem;

            if (DialogResult.Yes != MessageBox.Show(this,
                "Do you want to delete this Invoice Item [" + invItemToDelete.DisplayName + "]?",
                "Delete Invoice Item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)) return;

            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Get the Invoice before deleting InvoiceItem
                    int invId = invItemToDelete.Invoice.Id;

                    invItemToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                    invItemToDelete.UpdatedTime = DateTime.Now;
                    session.Delete(invItemToDelete);
                    session.Flush();

                    //Update invoice statement date
                    JCService service = new JCService(session, Program.LogonUser);
                    var qryInv = session.QueryOver<Invoice>()
                        .Where(x => x.Id == invId)
                        .Fetch(x => x.Patient).Eager
                        .Fetch(x => x.Patient.Insurer).Eager
                        .Future<Invoice>();
                    //Force to load other collections within the same round trip in other queries
                    session.CreateCriteria<InvoiceItem>()
                        .Add(Restrictions.Eq("Invoice.Id", invId));
                    session.CreateCriteria<TherapyType>().Future<TherapyType>();
                    session.CreateCriteria<User>().SetFetchMode("Title", FetchMode.Eager).Future<User>();
                    session.CreateCriteria<TherapistOrganizationRegistrationGroup>().Future<TherapistOrganizationRegistrationGroup>();

                    //Run query
                    Invoice invToUpdate = qryInv.SingleOrDefault();

                    invToUpdate.UpdatedBy = Program.LogonUser.DisplayName;
                    invToUpdate.UpdatedTime = DateTime.Now;
                    service.UpdateInvoiceStatementDate(invToUpdate);


                    //Update Invoice # - using statement date to populate the invoice #
                    string fileNo = invToUpdate.Patient.FileNumber;
                    string treatmentType = invToUpdate.TreatmentType.DisplayName;
                    DateTime dtStatement = invToUpdate.StatementDate;
                    string newInvNum = service.GetNextInvoiceNumber(fileNo, treatmentType, dtStatement, invId);
                    invToUpdate.UpdatedBy = Program.LogonUser.DisplayName;
                    invToUpdate.UpdatedTime = DateTime.Now;
                    invToUpdate.InvoiceNumber = newInvNum;

                    session.Save(invToUpdate);
                    session.Flush();

                    tr.Commit();

                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to delete Invoice Item. " + ex.Message);
                    Program.log.Error("Failed to delete Invoice Item.", ex);
                }
            }

            loadInvoice();
        }

        private void btnEditInvoiceItem_Click(object sender, EventArgs e) {
            if(grdInvoiceItem.SelectedRows.Count > 0) {
                InvoiceItem invItem = grdInvoiceItem.SelectedRows[0].DataBoundItem as InvoiceItem;
                if(DialogResult.OK == (new EditInvoiceItemForm(invItem.Id)).ShowDialog(this)) {
                    loadInvoice();
                }
            }
        }

        private void EditInvoiceForm_FormClosed(object sender, FormClosedEventArgs e) {
            //Try to close session
            SessionFactory.TryCloseSession(_sessionKey);
        }

        private void setDefaultTax() {

            //Get all taxes
            List<Tax> taxes = null;
            
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    taxes = session.QueryOver<Tax>()
                        .List<Tax>()
                        .ToList();

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to retrieve taxes. " + ex.Message);
                    Program.log.Error("Failed to retrieve taxes.", ex);
                }
            }

            if (taxes != null && taxes.Count > 0) {

                //Remove event change handler for numTaxPercent
                numTaxPercent.ValueChanged -= numTaxPercent_ValueChanged;

                if (taxes.Count == 1) {
                    txtTaxName.Text = taxes[0].Name;
                    numTaxPercent.Value = (decimal)taxes[0].Rate * 100;
                } else {
                    txtTaxName.Text = "Tax";
                    double taxRateTotal = 0;
                    foreach (Tax tax in taxes) {
                        taxRateTotal += tax.Rate;
                    }
                    numTaxPercent.Value = (decimal)(taxRateTotal * 100);
                }

                //Restore event change handler for numTaxPercent
                numTaxPercent.ValueChanged += numTaxPercent_ValueChanged;
            }
        }

        private void btnSetTax_Click(object sender, EventArgs e) {
            SelectTaxForm frmTax = new SelectTaxForm();
            if (DialogResult.OK == frmTax.ShowDialog(this)) {
                txtTaxName.Text = frmTax.SelectedTaxName;
                numTaxPercent.Value = frmTax.SelectedTaxRate;
            }
        }

        private double getTaxValue(double total) {
            return total * (double)numTaxPercent.Value / 100;
        }

        //Round money to 0 or 5 cents
        private double roundMoney(double money) {

            string sTotal = money.ToString("0.00");
            int lastDigit = Convert.ToInt16(sTotal.Substring(sTotal.Length - 1));
            double roundedLastDigit = lastDigit < 3 ? 0
                : lastDigit < 8 ? 5
                : 10;

            return Convert.ToDouble(sTotal.Substring(0, sTotal.Length - 1))
                + (roundedLastDigit / 100.0);
        }

        private void numTaxPercent_ValueChanged(object sender, EventArgs e) {
            calcuateTaxAndTotal(Convert.ToDouble(lblSubTotal.Text.TrimStart(new char[] { ' ', '$' })));
        }

        private void calcuateTaxAndTotal(double subTotal) {
            double taxTotal = getTaxValue(subTotal);
            double total = roundMoney(taxTotal + subTotal);

            if (taxTotal > 0) {
                lblTaxTotal.Text = taxTotal.ToString("C2");
                lblTaxTotal.Visible = true;
            } else {
                lblTaxTotal.Visible = false;
            }
            lblTotal.Text = total.ToString("C2");
        }

        private void txtTaxName_TextChanged(object sender, EventArgs e) {
            if (txtTaxName.Text.Trim().Length > 0) {
                lblTaxName.Text = txtTaxName.Text + ":";
                lblTaxName.Visible = true;
            } else {
                lblTaxName.Visible = false;
            }
        }
    }
}
