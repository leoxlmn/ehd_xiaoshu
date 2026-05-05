using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using EHD.Login;
using Utility;
using NHibernate;
using EHD.Repository;
using EHD.Model.Entity;
using EHD.Constant;
using EHD.Service;
using FilePathExtender;
using WordFileProcessor;
using System.IO;
using System.Drawing.Drawing2D;
using System.Reflection;
using NHibernate.Criterion;
using System.Drawing.Printing;
using NHibernate.Transform;
using NHibernate.SqlCommand;
using EHD.Model.DTO;
using NHibernate.Mapping.ByCode.Impl;
using NHibernate.Type;

namespace EHD.Admin {

    public partial class InvoiceSearchForm : Form {

        private const string IMG_KEY_MALE = "Male";
        private const string IMG_KEY_FEMALE = "Female";
        private const string IMG_KEY_PATIENT = "Patient";
        private const string IMG_KEY_DOCTOR = "Doctor";
        private const string IMG_KEY_INIT_TREATMENT = "InitialTreatment";
        private const string IMG_KEY_FUP_TREATMENT = "FollowUpTreatment";
        private const string IMG_KEY_INVOICE = "Invoice";
        private const string IMG_KEY_INVOICE_ITEM = "InvoiceItem";

        private const int IMG_INDEX_MALE = 0;
        private const int IMG_INDEX_FEMALE = 1;
        private const int IMG_INDEX_PATIENT = 2;
        private const int IMG_INDEX_DOCTOR = 3;
        private const int IMG_INDEX_INIT_TREATMENT = 4;
        private const int IMG_INDEX_FUP_TREATMENT = 5;
        private const int IMG_INDEX_INVOICE = 6;
        private const int IMG_INDEX_INVOICE_ITEM = 7;

        private readonly Color PATIENT_NODE_COLOR = Color.FromArgb(0, 30, 130);
        private readonly Font PATIENT_NODE_FONT = new Font("Courier New", 11, FontStyle.Bold);

        private TreeNode curNode = null;
        private TreeNode nodeToPrint;

        private int defaultPatientId;

        public InvoiceSearchForm() {
            InitializeComponent();
        }

        public InvoiceSearchForm(int PatientId) {
            InitializeComponent();

            defaultPatientId = PatientId;
        }

        private void InvoiceSearchForm_Load(object sender, EventArgs e) {

            //Initiate pager
            pager.Page = 1;
            pager.GoToPageEvent += new GoToPageEventHandler(invoiceNavigate);

            loadCheckedListBoxes();

            if (defaultPatientId != default(int)) {
                loadDefaultPatient();
            } else {
                reloadList();
            }
        }

        private void invoiceNavigate(int page) {
            reloadList();
        }

        private void loadCheckedListBoxes() {
            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get session
            ISession session = SessionFactory.GetOpenSession(key);

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    SimpleListItem aliasListItem = null;
                    SimpleUserItem aliasUserItem = null;

                    User aUser = null;
                    UserTitle aTitle = null;

                    //Load all reference tables in one batch query
                    var lstInsurer = session.QueryOver<Insurer>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.InsurerName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.InsurerName).Asc
                        .Take(Program.config.MaxInsurers)
                        .Future<SimpleListItem>();

                    var lstTreatmentType = session.QueryOver<TherapyType>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.TherapyTypeName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .Future<SimpleListItem>();

                    var lstTherapist = session.QueryOver<User>(() => aUser)
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

                    //Insurer
                    foreach (SimpleListItem insurer in lstInsurer) {
                        ucInsurer.AddItem(insurer, true);
                    }
                    ucInsurer.CheckCompleted += new EventHandler(filter_selected);

                    //Treatment Types
                    foreach (SimpleListItem tt in lstTreatmentType) {
                        ucTreatment.AddItem(tt, true);
                    }
                    ucTreatment.CheckCompleted += new EventHandler(filter_selected);

                    //Therapist
                    foreach (SimpleUserItem usr in lstTherapist) {
                        ucTherapist.AddItem(usr, true);
                    }
                    ucTherapist.CheckCompleted += new EventHandler(filter_selected);

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Filter lists. " + ex.Message);
                Program.log.Error("Failed to load Filter lists.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }
        }


        void filter_selected(object sender, EventArgs e) {

            //reset pager
            pager.Page = 1;

            //reload list
            reloadList();
        }

        private IList<SimpleListItem> selectedInsurers {
            get {
                IList<SimpleListItem> _selectedInsurers = new List<SimpleListItem>();
                foreach (object item in ucInsurer.CheckedItems) {
                    _selectedInsurers.Add(item as SimpleListItem);
                }
                return _selectedInsurers;
            }
        }

        private IList<SimpleUserItem> selectedTherapists {
            get {
                IList<SimpleUserItem> _selectedTherapists = new List<SimpleUserItem>();
                foreach (object item in ucTherapist.CheckedItems) {
                    _selectedTherapists.Add(item as SimpleUserItem);
                }
                return _selectedTherapists;
            }
        }

        private IList<SimpleListItem> selectedTreatmentTypes {
            get {
                IList<SimpleListItem> _selectedTreatmentTypes = new List<SimpleListItem>();
                foreach (object item in ucTreatment.CheckedItems) {
                    _selectedTreatmentTypes.Add(item as SimpleListItem);
                }
                return _selectedTreatmentTypes;
            }
        }

        //Add Insurer filter
        private IQueryOver<Patient, Patient> setInsurerFilter(IQueryOver<Patient, Patient> query, IProjection projection) {

            if (!ucInsurer.AllChecked) {
                if (ucInsurer.AllNonBlanks) {
                    query.Where(Restrictions.IsNotNull(projection));
                } else if (ucInsurer.BlanksOnly) {
                    query.Where(Restrictions.IsNull(projection));
                } else if (!ucInsurer.IsBlankChecked) {
                    query.Where(Restrictions.In(projection, selectedInsurers.Select(x => x.Id).ToArray()));
                } else {
                    query.Where(Expression.Disjunction()
                        .Add(Restrictions.IsNull(projection))
                        .Add(Restrictions.In(projection, selectedInsurers.Where(x => x != null).Select(x => x.Id).ToArray()))
                        );
                }
            }

            return query;
        }


        //Add Therapist filter
        private IQueryOver<Patient, Patient> setTherapistFilter(IQueryOver<Patient, Patient> query, IProjection projection) {

            if (!ucTherapist.AllChecked) {
                if (ucTherapist.AllNonBlanks) {
                    query.Where(Restrictions.IsNotNull(projection));
                } else if (ucTherapist.BlanksOnly) {
                    query.Where(Restrictions.IsNull(projection));
                } else if (!ucTherapist.IsBlankChecked) {
                    query.Where(Restrictions.In(projection, selectedTherapists.Select(x => x.Id).ToArray()));
                } else {
                    query.Where(Expression.Disjunction()
                        .Add(Restrictions.IsNull(projection))
                        .Add(Restrictions.In(projection, selectedTherapists.Where(x => x != null).Select(x => x.Id).ToArray()))
                        );
                }
            }

            return query;
        }

        //Add TreatmentType filter
        private IQueryOver<Patient, Patient> setTreatmentTypeFilter(IQueryOver<Patient, Patient> query, IProjection projection) {

            if (!ucTreatment.AllChecked) {
                if (ucTreatment.AllNonBlanks) {
                    query.Where(Restrictions.IsNotNull(projection));
                } else if (ucTreatment.BlanksOnly) {
                    query.Where(Restrictions.IsNull(projection));
                } else if (!ucTreatment.IsBlankChecked) {
                    query.Where(Restrictions.In(projection, selectedTreatmentTypes.Select(x => x.Id).ToArray()));
                } else {
                    query.Where(Expression.Disjunction()
                        .Add(Restrictions.IsNull(projection))
                        .Add(Restrictions.In(projection, selectedTreatmentTypes.Where(x => x != null).Select(x => x.Id).ToArray()))
                        );
                }
            }

            return query;
        }

        //Add Date range filter
        private IQueryOver<Patient, Patient> setDateRangeFilter(IQueryOver<Patient, Patient> query, IProjection invoiceStatementDateProjection, IProjection invoiceItemServiceDateProjection) {

            if (rdoServiceAndStatementDates.Checked) //Search both service date and statement date. Return more results.
            {
                if (dtFrom.Checked || dtTo.Checked)
                {

                    if (dtFrom.Checked && dtTo.Checked)
                    {
                        query.Where(Expression.Disjunction()
                                .Add(Expression.Conjunction()
                                    .Add(Restrictions.Ge(invoiceStatementDateProjection, dtFrom.Value.Date))
                                    .Add(Restrictions.Lt(invoiceStatementDateProjection, dtTo.Value.AddDays(1).Date))
                                )
                                .Add(Expression.Conjunction()
                                    .Add(Restrictions.Ge(invoiceItemServiceDateProjection, dtFrom.Value.Date))
                                    .Add(Restrictions.Lt(invoiceItemServiceDateProjection, dtTo.Value.AddDays(1).Date))
                                )
                            );
                    }
                    else if (dtFrom.Checked)
                    {
                        query.Where(Expression.Disjunction()
                                .Add(Restrictions.Ge(invoiceStatementDateProjection, dtFrom.Value.Date))
                                .Add(Restrictions.Ge(invoiceItemServiceDateProjection, dtFrom.Value.Date))
                            );
                    }
                    else if (dtTo.Checked)
                    {
                        query.Where(Expression.Disjunction()
                                .Add(Restrictions.Lt(invoiceStatementDateProjection, dtTo.Value.AddDays(1).Date))
                                .Add(Restrictions.Lt(invoiceItemServiceDateProjection, dtTo.Value.AddDays(1).Date))
                            );
                    }
                }
            }
            else //Search only Service date. Less results but more accurate.
            {
                if (dtFrom.Checked || dtTo.Checked)
                {

                    if (dtFrom.Checked && dtTo.Checked)
                    {
                        query.Where(Expression.Conjunction()
                                    .Add(Restrictions.Ge(invoiceItemServiceDateProjection, dtFrom.Value.Date))
                                    .Add(Restrictions.Lt(invoiceItemServiceDateProjection, dtTo.Value.AddDays(1).Date)));
                    }
                    else if (dtFrom.Checked)
                    {
                        query.Where(Restrictions.Ge(invoiceItemServiceDateProjection, dtFrom.Value.Date));
                    }
                    else if (dtTo.Checked)
                    {
                        query.Where(Restrictions.Lt(invoiceItemServiceDateProjection, dtTo.Value.AddDays(1).Date));
                    }
                }
            }


            return query;
        }

        //Load invoice list by default patient
        private void loadDefaultPatient() {
            if (defaultPatientId != default(int)) {
                //Create key for session access
                Guid key = Guid.NewGuid();

                //Start to update tree view
                trvList.BeginUpdate();
                trvList.Nodes.Clear();

                //
                PatientDTO aliasPatient = null;
                InvoiceDTO aliasInvoice = null;
                InvoiceItemDTO aliasInvoiceItem = null;

                PatientDTO DefaultPatient = null;
                List<InvoiceDTO> Invoices = new List<InvoiceDTO>();
                List<InvoiceItemDTO> InvoiceItems = new List<InvoiceItemDTO>();

                ISession session = SessionFactory.GetOpenSession(key);

                try {
                    using (ITransaction tr = session.BeginTransaction()) {

                        Patient aPatient = null;
                        Insurer aInsurer = null;
                        Invoice aInvoice = null;
                        InvoiceItem aInvItem = null;
                        TherapyType aTherapyType = null;
                        User aUser = null;

                        var qryPatient = session.QueryOver<Patient>(() => aPatient)
                            .JoinQueryOver(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin)
                            .Where(() => aPatient.Id == defaultPatientId)
                            .SelectList(list => list
                                .Select(() => aPatient.Id).WithAlias(() => aliasPatient.Id)
                                .Select(() => aPatient.Version).WithAlias(() => aliasPatient.Version)
                                .Select(() => aPatient.FirstName).WithAlias(() => aliasPatient.FirstName)
                                .Select(() => aPatient.LastName).WithAlias(() => aliasPatient.LastName)
                                .Select(() => aPatient.MiddleName).WithAlias(() => aliasPatient.MiddleName)
                                .Select(() => aPatient.FileNumber).WithAlias(() => aliasPatient.FileNumber)
                                .Select(() => aPatient.Sex).WithAlias(() => aliasPatient.Sex)
                                .Select(() => aInsurer.InsurerName).WithAlias(() => aliasPatient.Insurer)
                            )
                            .TransformUsing(Transformers.AliasToBean<PatientDTO>())
                            .Future<PatientDTO>();

                        var qryInvoices = session.QueryOver<Invoice>(() => aInvoice)
                            .JoinQueryOver<TherapyType>(() => aInvoice.TreatmentType, () => aTherapyType, JoinType.InnerJoin)
                            .JoinQueryOver<User>(() => aInvoice.Therapist, () => aUser, JoinType.InnerJoin)
                            .Where(() => aInvoice.Patient.Id == defaultPatientId)
                            .SelectList(list => list
                                .Select(() => aInvoice.Id).WithAlias(() => aliasInvoice.Id)
                                .Select(() => aInvoice.Version).WithAlias(() => aliasInvoice.Version)
                                .Select(() => aInvoice.Patient.Id).WithAlias(() => aliasInvoice.PatientId)
                                .Select(() => aInvoice.StatementDate).WithAlias(() => aliasInvoice.StatementDate)
                                .Select(() => aInvoice.TaxRate).WithAlias(() => aliasInvoice.TaxRate)
                                .Select(() => aUser.FirstName).WithAlias(() => aliasInvoice.TherapistFirstName)
                                .Select(() => aUser.LastName).WithAlias(() => aliasInvoice.TherapistLastName)
                                .Select(() => aUser.MiddleName).WithAlias(() => aliasInvoice.TherapistMiddleName)
                                .Select(() => aInvoice.InvoiceNumber).WithAlias(() => aliasInvoice.InvoiceNumber)
                                .Select(() => aTherapyType.TherapyTypeName).WithAlias(() => aliasInvoice.TreatmentType)
                            )
                            .OrderBy(() => aInvoice.StatementDate).Asc
                            .ThenBy(() => aInvoice.Id).Asc
                            .TransformUsing(Transformers.AliasToBean<InvoiceDTO>())
                            .Future<InvoiceDTO>();

                        var qryInvItems = session.QueryOver<InvoiceItem>(() => aInvItem)
                            .JoinQueryOver<Invoice>(() => aInvItem.Invoice, () => aInvoice, JoinType.InnerJoin)
                            .Where(() => aInvoice.Patient.Id == defaultPatientId)
                            .SelectList(list => list
                                .Select(() => aInvItem.Id).WithAlias(() => aliasInvoiceItem.Id)
                                .Select(() => aInvItem.Version).WithAlias(() => aliasInvoiceItem.Version)
                                .Select(() => aInvoice.Id).WithAlias(() => aliasInvoiceItem.InvoiceId)
                                .Select(() => aInvItem.ServiceDate).WithAlias(() => aliasInvoiceItem.ServiceDate)
                                .Select(() => aInvItem.ServiceDuration).WithAlias(() => aliasInvoiceItem.ServiceDuration)
                                .Select(() => aInvItem.ServiceDescription).WithAlias(() => aliasInvoiceItem.ServiceDescription)
                                .Select(() => aInvItem.Amount).WithAlias(() => aliasInvoiceItem.Amount)
                            )
                            .OrderBy(() => aInvoice.StatementDate).Asc
                            .ThenBy(() => aInvoice.Id).Asc
                            .ThenBy(() => aInvItem.ServiceDate).Asc
                            .TransformUsing(Transformers.AliasToBean<InvoiceItemDTO>())
                            .Future<InvoiceItemDTO>();

                        DefaultPatient = qryPatient.SingleOrDefault<PatientDTO>();
                        Invoices = qryInvoices.ToList();
                        InvoiceItems = qryInvItems.ToList();

                        //Update total number
                        //lblPatientNumber.Text = "1";

                        //Set pager
                        pager.TotalPage = 1;

                        tr.Commit();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to retrieve the default patient with invoice info. " + ex.Message);
                    Program.log.Error("Failed to retrieve the default patient with invoice info.", ex);
                } finally {
                    SessionFactory.TryCloseSession(key);
                }

                //double totalAmount = 0;
                //double totalAfterTaxAmount = 0;
                //double numberOfTreatment = 0;

                if (DefaultPatient != null) {

                    TreeNode pNode = createPatientNode(DefaultPatient);

                    foreach (InvoiceDTO inv in Invoices) {

                        TreeNode invNode = createInvoiceNode(inv);

                        double patientTotal = 0.0;
                        foreach (InvoiceItemDTO item in InvoiceItems.Where(x => x.InvoiceId == inv.Id)) {
                            TreeNode itemNode = createInvoiceItemNode(item);
                            invNode.Nodes.Add(itemNode);

                            patientTotal += item.Amount;
                            //numberOfTreatment++;
                        }
                        double invoiceAfterTaxTotal = patientTotal * (inv.TaxRate.HasValue ? 1 + inv.TaxRate.Value : 1);

                        invNode.Text = invNode.Text.Replace("{total}", string.Format("{0:C2}", patientTotal))
                            .Replace("{total after tax}", string.Format("{0:C2}", invoiceAfterTaxTotal));

                        pNode.Nodes.Add(invNode);

                        //totalAmount += patientTotal;
                        //totalAfterTaxAmount += invoiceAfterTaxTotal;
                    }

                    trvList.Nodes.Add(pNode);
                }

                trvList.EndUpdate();

                expandCollapseTree();

                //lblTotalAmount.Text = string.Format("{0:C2}", totalAmount);
                //lblTotalAfterTax.Text = string.Format("{0:C2}", totalAfterTaxAmount);
                //lblTotalTreatment.Text = numberOfTreatment.ToString();
                updateTotals(defaultPatientId);
            }
        }

        //Load invoice list by searching criteria
        private void reloadList(bool loadAll = false) {

            //Create key for session access
            Guid key = Guid.NewGuid();

            //Start to update tree view
            trvList.BeginUpdate();
            trvList.Nodes.Clear();

            //Retrieve all patients with invoice info
            IList<PatientDTO> lstPatient = new List<PatientDTO>();

            ISession session = SessionFactory.GetOpenSession(key);

            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    InvoiceSearchResultDTO rs = null;
                    Patient aPatient = null;
                    Invoice aInvoice = null;
                    InvoiceItem aItem = null;
                    User aUser = null;
                    Insurer aInsurer = null;
                    UserTitle aTitle = null;
                    TherapyType aType = null;

                    //Prepare search query for patient ids
                    var qryId = session.QueryOver<Patient>(() => aPatient);

                    //Add filters
                    //1) Patient name
                    if (txtPatient.Text.Trim().Length > 0) {
                        string patientNameSearch = "%" + txtPatient.Text.Trim() + "%";
                        qryId.Where(Restrictions.Disjunction()
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FirstName), patientNameSearch))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.LastName), patientNameSearch))
                            .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.MiddleName), patientNameSearch))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                patientNameSearch,
                                NHibernateUtil.String))
                            .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_MiddleName, ' ', PTN_LastName) LIKE ?",
                                patientNameSearch,
                                NHibernateUtil.String)));
                    }

                    //2) Insurer
                    if (!ucInsurer.AllChecked) {
                        qryId.JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin);
                        setInsurerFilter(qryId, Projections.Property(() => aInsurer.Id));
                    }

                    //3,4,5 Treatment Type / Therapist / Date range
                    if (!ucTreatment.AllChecked || !ucTherapist.AllChecked || dtFrom.Checked || dtTo.Checked) {
                        qryId.JoinQueryOver<Invoice>(() => aPatient.Invoices, () => aInvoice, JoinType.LeftOuterJoin);
                        setTreatmentTypeFilter(qryId, Projections.Property(() => aInvoice.TreatmentType.Id));
                        setTherapistFilter(qryId, Projections.Property(() => aInvoice.Therapist.Id));
                    }
                    //5 Date range
                    if (dtFrom.Checked || dtTo.Checked) {
                        qryId.JoinQueryOver<InvoiceItem>(() => aInvoice.InvoiceItems, () => aItem, JoinType.LeftOuterJoin);
                        setDateRangeFilter(qryId, Projections.Property(() => aInvoice.StatementDate), Projections.Property(() => aItem.ServiceDate));
                    }

                    //Select patient ids for paging
                    /* Will cause exeception "... ORDER BY clause is not in SELECT list
                    var patientIds = (qryId.Clone())
                        .OrderBy(() => aPatient.FileNumber).Asc
                        .Select(Projections.Distinct(Projections.Property(() => aPatient.Id)))
                        .TransformUsing(Transformers.DistinctRootEntity)
                        .Skip((pager.Page - 1) * pager.PageSize)
                        .Take(pager.PageSize)
                        .Future<int>();*/
                    //Re-wrote
                    var patientIds =
                        loadAll ?
                        (qryId.Clone())
                        .Select(Projections.Group<Patient>(p => p.Id))
                        .OrderBy(() => aPatient.FileNumber).Asc
                        .TransformUsing(Transformers.DistinctRootEntity)
                        .Future<int>()
                        :
                        (qryId.Clone())
                        .Select(Projections.Group<Patient>(p => p.Id))
                        .OrderBy(() => aPatient.FileNumber).Asc
                        .TransformUsing(Transformers.DistinctRootEntity)
                        .Skip((pager.Page - 1) * pager.PageSize)
                        .Take(pager.PageSize)
                        .Future<int>();

                    //Get patient total count
                    int totalPatientCount = qryId
                        .Select(Projections.CountDistinct(() => aPatient.Id))
                        .FutureValue<int>()
                        .Value;

                    //Get search result data
                    //1) Get Patients & Invoices first with filters
                    var qryData = session.QueryOver<Patient>(() => aPatient);
                    qryData
                        .JoinQueryOver<Invoice>(() => aPatient.Invoices, () => aInvoice, JoinType.LeftOuterJoin)
                        .JoinQueryOver<InvoiceItem>(() => aInvoice.InvoiceItems, () => aItem, JoinType.LeftOuterJoin)
                        .JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin)
                        .JoinQueryOver<User>(() => aInvoice.Therapist, () => aUser, JoinType.LeftOuterJoin)
                        .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                        .JoinQueryOver<TherapyType>(() => aInvoice.TreatmentType, () => aType, JoinType.LeftOuterJoin)
                        .Where(Restrictions.In(Projections.Property(() => aPatient.Id), patientIds.ToArray()))
                        .Select(
                            Projections.Distinct(
                                Projections.ProjectionList()
                                    .Add(Projections.Property(() => aPatient.Id).WithAlias(() => rs.PatientId))
                                    .Add(Projections.Property(() => aPatient.Version).WithAlias(() => rs.PatientVersion))
                                    .Add(Projections.Property(() => aPatient.FileNumber).WithAlias(() => rs.FileNumber))
                                    .Add(Projections.Property(() => aPatient.Sex).WithAlias(() => rs.Sex))
                                    .Add(Projections.Property(() => aPatient.FirstName).WithAlias(() => rs.FirstName))
                                    .Add(Projections.Property(() => aPatient.LastName).WithAlias(() => rs.LastName))
                                    .Add(Projections.Property(() => aPatient.MiddleName).WithAlias(() => rs.MiddleName))
                                    .Add(Projections.Property(() => aInsurer.InsurerName).WithAlias(() => rs.Insurer))

                                    .Add(Projections.Property(() => aInvoice.Id).WithAlias(() => rs.InvoiceId))
                                    .Add(Projections.Property(() => aInvoice.Version).WithAlias(() => rs.InvoiceVersion))
                                    .Add(Projections.Property(() => aInvoice.InvoiceNumber).WithAlias(() => rs.InvoiceNumber))
                                    .Add(Projections.Property(() => aInvoice.StatementDate).WithAlias(() => rs.StatementDate))
                                    .Add(Projections.Property(() => aInvoice.TaxRate).WithAlias(() => rs.TaxRate))
                                    .Add(Projections.Property(() => aType.TherapyTypeName).WithAlias(() => rs.TreatmentType))
                                    .Add(Projections.Property(() => aUser.FirstName).WithAlias(() => rs.TherapistFirstName))
                                    .Add(Projections.Property(() => aUser.LastName).WithAlias(() => rs.TherapistLastName))
                                    .Add(Projections.Property(() => aUser.MiddleName).WithAlias(() => rs.TherapistMiddleName))
                                    .Add(Projections.Property(() => aTitle.Name).WithAlias(() => rs.TherapistTitle))
                            )
                        );
                    //Add filters
                    setInsurerFilter(qryData, Projections.Property(() => aInsurer.Id));
                    setTreatmentTypeFilter(qryData, Projections.Property(() => aInvoice.TreatmentType.Id));
                    setTherapistFilter(qryData, Projections.Property(() => aInvoice.Therapist.Id));
                    setDateRangeFilter(qryData, Projections.Property(() => aInvoice.StatementDate), Projections.Property(() => aItem.ServiceDate));

                    var searchData = qryData
                        .OrderBy(() => aPatient.FileNumber).Asc
                        .ThenBy(() => aInvoice.StatementDate).Asc
                        .ThenBy(() => aInvoice.Id).Asc
                        .TransformUsing(Transformers.AliasToBean<InvoiceSearchResultDTO>())
                        .List<InvoiceSearchResultDTO>();

                    #region Populate lstPatient
                    //Populate lstPatient
                    int lastPatientId = 0;
                    int lastInvoiceId = 0;
                    PatientDTO patient = null;
                    InvoiceDTO invoice = null;

                    //This invoice id list will be used to retrieve their invoice items
                    IList<int> invIds = new List<int>();

                    foreach (InvoiceSearchResultDTO row in searchData) {

                        //Add a patient
                        if (row.PatientId != lastPatientId) {
                            patient = new PatientDTO {
                                Id = row.PatientId,
                                Version = row.PatientVersion,
                                FileNumber = row.FileNumber,
                                Sex = row.Sex,
                                FirstName = row.FirstName,
                                LastName = row.LastName,
                                MiddleName = row.MiddleName,
                                Insurer = row.Insurer,
                                Invoices = new List<InvoiceDTO>()
                            };
                            lstPatient.Add(patient);
                            lastPatientId = patient.Id;
                        }

                        //Add an invoice
                        if (row.InvoiceId.HasValue && row.InvoiceId.Value != lastInvoiceId) {
                            invoice = new InvoiceDTO {
                                Id = row.InvoiceId.Value,
                                Version = row.InvoiceVersion.Value,
                                PatientId = row.PatientId,
                                InvoiceNumber = row.InvoiceNumber,
                                StatementDate = row.StatementDate.Value,
                                TaxRate = row.TaxRate,
                                TherapistFirstName = row.TherapistFirstName,
                                TherapistLastName = row.TherapistLastName,
                                TherapistMiddleName = row.TherapistMiddleName,
                                TherapistTitle = row.TherapistTitle,
                                TreatmentType = row.TreatmentType,
                                InvoiceItems = new List<InvoiceItemDTO>()
                            };
                            patient.Invoices.Add(invoice);
                            lastInvoiceId = invoice.Id;
                            invIds.Add(invoice.Id);
                        }
                    }
                    #endregion Populate lstPatient

                    //2)Retrieve all invoice items for invoices
                    if (invIds.Count > 0) {
                        InvoiceItemDTO aInvItemDTO = null;
                        InvoiceItem aInvItem = null;
                        var invItemDTOs = session.QueryOver<InvoiceItem>(() => aInvItem)
                            .Where(Restrictions.In(Projections.Property(() => aInvItem.Invoice.Id), invIds.ToArray()))
                            //Added the time range filter for Winnie to generate a more accurate report
                            .And(Restrictions.Between(Projections.Property(() => aInvItem.ServiceDate), 
                                dtFrom.Checked ? dtFrom.Value.Date : new DateTime(2000, 1, 1),
                                dtTo.Checked ? dtTo.Value.AddDays(1).Date : new DateTime(3000, 12, 31)))
                            .SelectList(list => list
                                .Select(() => aInvItem.Id).WithAlias(() => aInvItemDTO.Id)
                                .Select(() => aInvItem.Version).WithAlias(() => aInvItemDTO.Version)
                                .Select(() => aInvItem.Invoice.Id).WithAlias(() => aInvItemDTO.InvoiceId)
                                .Select(() => aInvItem.ServiceDate).WithAlias(() => aInvItemDTO.ServiceDate)
                                .Select(() => aInvItem.ServiceDuration).WithAlias(() => aInvItemDTO.ServiceDuration)
                                .Select(() => aInvItem.ServiceDescription).WithAlias(() => aInvItemDTO.ServiceDescription)
                                .Select(() => aInvItem.Amount).WithAlias(() => aInvItemDTO.Amount)
                            )
                            .OrderBy(() => aInvItem.Invoice.Id).Asc
                            .ThenBy(() => aInvItem.ServiceDate).Asc
                            .TransformUsing(Transformers.AliasToBean<InvoiceItemDTO>())
                            .List<InvoiceItemDTO>();

                        //Load InvoiceItemDTOs into lstPatient
                        foreach (PatientDTO p in lstPatient) {
                            foreach (InvoiceDTO inv in p.Invoices) {
                                foreach (InvoiceItemDTO ini in invItemDTOs.Where(x => x.InvoiceId == inv.Id)) {
                                    inv.InvoiceItems.Add(ini);
                                }
                            }
                        }
                    }

                    //Update total number
                    //lblPatientNumber.Text = totalPatientCount.ToString();

                    //Set pager
                    if (loadAll) {
                            pager.TotalPage = 1;
                    } else {
                        if (totalPatientCount == 0) {
                            pager.TotalPage = 1;
                        } else {
                            pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalPatientCount - 1) / pager.PageSize)) + 1;
                        }
                    }

                    tr.Commit();

                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to retrieve patients with invoice info. " + ex.Message);
                Program.log.Error("Failed to retrieve patients with invoice info.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }


            //double total = 0.0;
            //double totalAfterTax = 0.0;
            //double numberOfTreatments = 0;
            foreach (PatientDTO p in lstPatient) {

                TreeNode pNode = createPatientNode(p);

                foreach (InvoiceDTO inv in p.Invoices) {

                    TreeNode invNode = createInvoiceNode(inv);

                    double patientTotal = 0.0;
                    foreach (InvoiceItemDTO item in inv.InvoiceItems) {
                        TreeNode itemNode = createInvoiceItemNode(item);
                        invNode.Nodes.Add(itemNode);

                        patientTotal += item.Amount;
                        //numberOfTreatments++;
                    }
                    double patientTotalAfterTax = patientTotal * (inv.TaxRate.HasValue ? 1 + inv.TaxRate.Value : 1);

                    invNode.Text = invNode.Text.Replace("{total}", string.Format("{0:C2}", patientTotal))
                        .Replace("{total after tax}", string.Format("{0:C2}", patientTotalAfterTax));

                    pNode.Nodes.Add(invNode);

                    //total += patientTotal;
                    //totalAfterTax += patientTotalAfterTax;
                }

                trvList.Nodes.Add(pNode);
            }

            //lblTotalAmount.Text = string.Format("{0:C2}", total);
            //lblTotalAfterTax.Text = string.Format("{0:C2}", totalAfterTax);
            //lblTotalTreatment.Text = numberOfTreatments.ToString();
            updateTotals();

            if (rdoCollapseAll.Checked) {
                trvList.CollapseAll();
            } else {
                trvList.ExpandAll();
            }

            trvList.EndUpdate();
        }

        private void reloadNode(TreeNode node) {

            //Create key for session access
            Guid key = Guid.NewGuid();

            //Start to update tree view under the given node
            trvList.BeginUpdate();
            //node.Nodes.Clear();

            //Get session
            ISession session = SessionFactory.GetOpenSession(key);

            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    object nodeData = node.Tag;

                    if (nodeData is PatientDTO) {

                        //1) Retrieve Patient with Invoices
                        int patientId = (nodeData as PatientDTO).Id;
                        Patient aPatient = null;
                        Insurer aInsurer = null;
                        Invoice aInvoice = null;
                        InvoiceItem aInvItem = null;
                        TherapyType aType = null;
                        User aUser = null;
                        UserTitle aTitle = null;
                        InvoiceSearchResultDTO rs = null;

                        var qry = session.QueryOver<Patient>(() => aPatient);
                        qry
                            .JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin)
                            .JoinQueryOver<Invoice>(() => aPatient.Invoices, () => aInvoice, JoinType.LeftOuterJoin)
                            .JoinQueryOver<InvoiceItem>(() => aInvoice.InvoiceItems, () => aInvItem, JoinType.LeftOuterJoin)
                            .JoinQueryOver<TherapyType>(() => aInvoice.TreatmentType, () => aType, JoinType.LeftOuterJoin)
                            .JoinQueryOver<User>(() => aInvoice.Therapist, () => aUser, JoinType.LeftOuterJoin)
                            .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                            .Where(() => aPatient.Id == patientId)
                            .Select(
                                Projections.Distinct(
                                    Projections.ProjectionList()
                                        .Add(Projections.Property(() => aPatient.Id).WithAlias(() => rs.PatientId))
                                        .Add(Projections.Property(() => aPatient.Version).WithAlias(() => rs.PatientVersion))
                                        .Add(Projections.Property(() => aPatient.FileNumber).WithAlias(() => rs.FileNumber))
                                        .Add(Projections.Property(() => aPatient.Sex).WithAlias(() => rs.Sex))
                                        .Add(Projections.Property(() => aPatient.FirstName).WithAlias(() => rs.FirstName))
                                        .Add(Projections.Property(() => aPatient.LastName).WithAlias(() => rs.LastName))
                                        .Add(Projections.Property(() => aPatient.MiddleName).WithAlias(() => rs.MiddleName))
                                        .Add(Projections.Property(() => aInsurer.InsurerName).WithAlias(() => rs.Insurer))

                                        .Add(Projections.Property(() => aInvoice.Id).WithAlias(() => rs.InvoiceId))
                                        .Add(Projections.Property(() => aInvoice.Version).WithAlias(() => rs.InvoiceVersion))
                                        .Add(Projections.Property(() => aInvoice.InvoiceNumber).WithAlias(() => rs.InvoiceNumber))
                                        .Add(Projections.Property(() => aInvoice.StatementDate).WithAlias(() => rs.StatementDate))
                                        .Add(Projections.Property(() => aInvoice.TaxRate).WithAlias(() => rs.TaxRate))
                                        .Add(Projections.Property(() => aType.TherapyTypeName).WithAlias(() => rs.TreatmentType))
                                        .Add(Projections.Property(() => aUser.FirstName).WithAlias(() => rs.TherapistFirstName))
                                        .Add(Projections.Property(() => aUser.LastName).WithAlias(() => rs.TherapistLastName))
                                        .Add(Projections.Property(() => aUser.MiddleName).WithAlias(() => rs.TherapistMiddleName))
                                        .Add(Projections.Property(() => aTitle.Name).WithAlias(() => rs.TherapistTitle))
                                )
                            );
                        //Add filters
                        setInsurerFilter(qry, Projections.Property(() => aInsurer.Id));
                        setTreatmentTypeFilter(qry, Projections.Property(() => aInvoice.TreatmentType.Id));
                        setTherapistFilter(qry, Projections.Property(() => aInvoice.Therapist.Id));
                        setDateRangeFilter(qry, Projections.Property(() => aInvoice.StatementDate), Projections.Property(() => aInvItem.ServiceDate));

                        var searchData = qry
                            .ThenBy(() => aInvoice.StatementDate).Asc
                            .ThenBy(() => aInvoice.Id).Asc
                            .TransformUsing(Transformers.AliasToBean<InvoiceSearchResultDTO>())
                            .List<InvoiceSearchResultDTO>();

                        PatientDTO patientDTO = null;
                        IList<int> invIds = new List<int>();

                        if (searchData.Count > 0) {
                            patientDTO = new PatientDTO {
                                Id = patientId,
                                Version = searchData[0].PatientVersion,
                                FileNumber = searchData[0].FileNumber,
                                FirstName = searchData[0].FirstName,
                                LastName = searchData[0].LastName,
                                MiddleName = searchData[0].MiddleName,
                                Sex = searchData[0].Sex,
                                Insurer = searchData[0].Insurer,
                                Invoices = new List<InvoiceDTO>()
                            };

                            foreach (InvoiceSearchResultDTO row in searchData.Where(x => x.InvoiceId.HasValue)) {
                                InvoiceDTO invoiceDTO = new InvoiceDTO {
                                    Id = row.InvoiceId.Value,
                                    Version = row.InvoiceVersion.Value,
                                    PatientId = row.PatientId,
                                    InvoiceNumber = row.InvoiceNumber,
                                    StatementDate = row.StatementDate.Value,
                                    TaxRate = row.TaxRate,
                                    TherapistFirstName = row.TherapistFirstName,
                                    TherapistLastName = row.TherapistLastName,
                                    TherapistMiddleName = row.TherapistMiddleName,
                                    TherapistTitle = row.TherapistTitle,
                                    TreatmentType = row.TreatmentType,
                                    InvoiceItems = new List<InvoiceItemDTO>()
                                };
                                patientDTO.Invoices.Add(invoiceDTO);
                                invIds.Add(invoiceDTO.Id);
                            }

                            //2) Retrieve InvoiceItems for each Invoice
                            if (invIds.Count > 0) {
                                InvoiceItemDTO aItemDTO = null;
                                InvoiceItem aItem = null;
                                var invItemDTOs = session.QueryOver<InvoiceItem>(() => aItem)
                                    .Where(Restrictions.In(Projections.Property(() => aItem.Invoice.Id), invIds.ToArray()))
                                    .SelectList(list => list
                                        .Select(() => aItem.Id).WithAlias(() => aItemDTO.Id)
                                        .Select(() => aItem.Version).WithAlias(() => aItemDTO.Version)
                                        .Select(() => aItem.Invoice.Id).WithAlias(() => aItemDTO.InvoiceId)
                                        .Select(() => aItem.ServiceDate).WithAlias(() => aItemDTO.ServiceDate)
                                        .Select(() => aItem.ServiceDuration).WithAlias(() => aItemDTO.ServiceDuration)
                                        .Select(() => aItem.ServiceDescription).WithAlias(() => aItemDTO.ServiceDescription)
                                        .Select(() => aItem.Amount).WithAlias(() => aItemDTO.Amount)
                                    )
                                    .OrderBy(() => aItem.Invoice.Id).Asc
                                    .ThenBy(() => aItem.ServiceDate).Asc
                                    .TransformUsing(Transformers.AliasToBean<InvoiceItemDTO>())
                                    .List<InvoiceItemDTO>();

                                //Load InvoiceItemDTOs into InvoiceDTOs
                                foreach (InvoiceDTO inv in patientDTO.Invoices) {
                                    foreach (InvoiceItemDTO ini in invItemDTOs.Where(x => x.InvoiceId == inv.Id)) {
                                        inv.InvoiceItems.Add(ini);
                                    }
                                }
                            }

                            //Retrieve old treatment count, totals
                            double oldPatientTotal = 0;
                            double oldPatientTotalAfterTax = 0;
                            int oldPatientItemCount = 0;
                            retrieveOldValuesFromPatientNode(node, out oldPatientTotal, out oldPatientTotalAfterTax, out oldPatientItemCount);

                            //Update node
                            updatePatientNode(node, patientDTO);

                            //Clear children
                            node.Nodes.Clear();

                            double patientTotal = 0;
                            double patientTotalAfterTax = 0;
                            int patientItemCount = 0;

                            foreach (InvoiceDTO inv in patientDTO.Invoices) {

                                TreeNode invNode = createInvoiceNode(inv);

                                double total = 0.0;
                                int itemCount = 0;
                                foreach (InvoiceItemDTO item in inv.InvoiceItems) {
                                    TreeNode itemNode = createInvoiceItemNode(item);
                                    invNode.Nodes.Add(itemNode);

                                    total += item.Amount;
                                    itemCount++;
                                }
                                double totalAfterTax = total * (inv.TaxRate.HasValue ? 1 + inv.TaxRate.Value : 1);

                                patientTotal += total;
                                patientTotalAfterTax += totalAfterTax;
                                patientItemCount += itemCount;

                                invNode.Text = invNode.Text.Replace("{total}", string.Format("{0:C2}", total))
                                    .Replace("{total after tax}", string.Format("{0:C2}", totalAfterTax));

                                node.Nodes.Add(invNode);
                            }

                            //Update overall totals
                            //lblTotalAmount.Text = string.Format("{0:C2}",
                            //    double.Parse(lblTotalAmount.Text.Trim(new char[] { '$' })) + (patientTotal - oldPatientTotal));
                            //lblTotalAfterTax.Text = string.Format("{0:C2}",
                            //    double.Parse(lblTotalAfterTax.Text.Trim(new char[] { '$' })) + (patientTotalAfterTax - oldPatientTotalAfterTax));
                            //lblTotalTreatment.Text = string.Format("{0}",
                            //    int.Parse(lblTotalTreatment.Text) + (patientItemCount - oldPatientItemCount));
                            updateTotals();
                        }

                        /*
                        //Prepare query to retrieve Patient with insurer info
                        int patientId = (nodeData as Patient).Id;
                        var qryP = session.QueryOver<Patient>()
                            .Fetch(x => x.Insurer).Eager
                            .Where(x => x.Id == patientId)
                            .Future<Patient>();

                        //Prepare Invoice subquery to apply ServiceType & date range filters
                        var dc = DetachedCriteria.For<Invoice>("inv")
                            .SetProjection(Projections.Property("inv.Id"))
                            .CreateAlias("inv.InvoiceItems", "item", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

                        if (dtFrom.Checked && dtTo.Checked) {
                            dc = dc
                                .Add(Expression.Disjunction()
                                    .Add(Expression.Conjunction()
                                        .Add(Restrictions.Ge("inv.StatementDate", dtFrom.Value.Date))
                                        .Add(Restrictions.Le("inv.StatementDate", dtTo.Value.Date))
                                    )
                                    .Add(Expression.Conjunction()
                                        .Add(Restrictions.Ge("item.ServiceDate", dtFrom.Value.Date))
                                        .Add(Restrictions.Le("item.ServiceDate", dtTo.Value.Date))
                                    )
                                );
                        } else if (dtFrom.Checked) {
                            dc = dc
                                .Add(Expression.Disjunction()
                                    .Add(Restrictions.Ge("inv.StatementDate", dtFrom.Value.Date))
                                    .Add(Restrictions.Ge("item.ServiceDate", dtFrom.Value.Date))
                                );
                        }else if (dtTo.Checked) {
                            dc = dc
                                .Add(Expression.Disjunction()
                                    .Add(Restrictions.Le("inv.StatementDate", dtTo.Value.Date))
                                    .Add(Restrictions.Le("item.ServiceDate", dtTo.Value.Date))
                                );
                        }

                        //Build query
                        var query = session.CreateCriteria<Invoice>("inv")
                            .Add(Restrictions.Eq("Patient.Id", patientId));
                        query = setTherapistFilter(query, "inv.Therapist");
                        query = setTreatmentTypeFilter(query, "inv.TreatmentType");
                        query = query.Add(Subqueries.PropertyIn("inv.Id", dc));
                        query = query.CreateAlias("inv.InvoiceItems", "item", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

                        //Put into a batch query
                        var invoices = query
                            .AddOrder(new Order("inv.StatementDate", true))
                            .AddOrder(new Order("item.ServiceDate", true))
                            .SetMaxResults(Program.config.MaxInvoicesPerPatient * Program.config.MaxInvoiceItemsPerInvoice)
                            .Future<Invoice>();

                        //Force to load other collections within the same round trip in other queries
                        session.CreateCriteria<TherapyType>().Future<TherapyType>();
                        session.CreateCriteria<User>().SetFetchMode("Title", FetchMode.Eager).Future<User>();
                        session.CreateCriteria<TherapistOrganizationRegistrationGroup>().Future<TherapistOrganizationRegistrationGroup>();

                        //Run query and retrieve data
                        List<Invoice> lstInvoice = invoices
                            .ToList<Invoice>().Distinct<Invoice>()
                            .OrderBy(x => x.StatementDate)
                            .ToList<Invoice>();

                        updatePatientNode(node, qryP.SingleOrDefault());

                        foreach (Invoice inv in lstInvoice) {

                            TreeNode invNode = createInvoiceNode(inv);

                            double total = 0.0;
                            foreach (InvoiceItem item in inv.InvoiceItems) {
                                TreeNode itemNode = createInvoiceItemNode(item);
                                invNode.Nodes.Add(itemNode);

                                total += item.Amount;
                            }
                            invNode.Text = invNode.Text.Replace("{total}", string.Format("{0:C2}", total));
                            node.Nodes.Add(invNode);
                        }*/

                    } else if (nodeData is InvoiceDTO) {

                        int invId = (nodeData as InvoiceDTO).Id;
                        InvoiceSearchResultDTO rsDTO = null;
                        Invoice aInvoice = null;
                        TherapyType aType = null;
                        User aUser = null;
                        UserTitle aTitle = null;
                        InvoiceItem aInvItem = null;
                        var searchData = session.QueryOver<Invoice>(() => aInvoice)
                            .JoinQueryOver<InvoiceItem>(() => aInvoice.InvoiceItems, () => aInvItem, JoinType.LeftOuterJoin)
                            .JoinQueryOver<TherapyType>(() => aInvoice.TreatmentType, () => aType, JoinType.LeftOuterJoin)
                            .JoinQueryOver<User>(() => aInvoice.Therapist, () => aUser, JoinType.LeftOuterJoin)
                            .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                            .Where(() => aInvoice.Id == invId)
                            .SelectList(list => list
                                .Select(() => aInvoice.Id).WithAlias(() => rsDTO.InvoiceId)
                                .Select(() => aInvoice.Version).WithAlias(() => rsDTO.InvoiceVersion)
                                .Select(() => aInvoice.Patient.Id).WithAlias(() => rsDTO.PatientId)
                                .Select(() => aInvoice.InvoiceNumber).WithAlias(() => rsDTO.InvoiceNumber)
                                .Select(() => aInvoice.StatementDate).WithAlias(() => rsDTO.StatementDate)
                                .Select(() => aInvoice.TaxRate).WithAlias(() => rsDTO.TaxRate)
                                .Select(() => aUser.FirstName).WithAlias(() => rsDTO.TherapistFirstName)
                                .Select(() => aUser.LastName).WithAlias(() => rsDTO.TherapistLastName)
                                .Select(() => aUser.MiddleName).WithAlias(() => rsDTO.TherapistMiddleName)
                                .Select(() => aTitle.Name).WithAlias(() => rsDTO.TherapistTitle)
                                .Select(() => aType.TherapyTypeName).WithAlias(() => rsDTO.TreatmentType)

                                .Select(() => aInvItem.Id).WithAlias(() => rsDTO.InvoiceItemId)
                                .Select(() => aInvItem.Version).WithAlias(() => rsDTO.InvoiceItemVersion)
                                .Select(() => aInvItem.ServiceDate).WithAlias(() => rsDTO.ServiceDate)
                                .Select(() => aInvItem.ServiceDuration).WithAlias(() => rsDTO.ServiceDuration)
                                .Select(() => aInvItem.ServiceDescription).WithAlias(() => rsDTO.ServiceDescription)
                                .Select(() => aInvItem.Amount).WithAlias(() => rsDTO.Amount)
                            )
                            .OrderBy(() => aInvItem.ServiceDate).Asc
                            .TransformUsing(Transformers.AliasToBean<InvoiceSearchResultDTO>())
                            .List<InvoiceSearchResultDTO>();

                        InvoiceDTO invoice = new InvoiceDTO();
                        if (searchData.Count > 0) {
                            invoice.Id = searchData[0].InvoiceId.Value;
                            invoice.Version = searchData[0].InvoiceVersion.Value;
                            invoice.PatientId = searchData[0].PatientId;
                            invoice.InvoiceNumber = searchData[0].InvoiceNumber;
                            invoice.StatementDate = searchData[0].StatementDate.Value;
                            invoice.TaxRate = searchData[0].TaxRate;
                            invoice.TherapistFirstName = searchData[0].TherapistFirstName;
                            invoice.TherapistLastName = searchData[0].TherapistLastName;
                            invoice.TherapistMiddleName = searchData[0].TherapistMiddleName;
                            invoice.TherapistTitle = searchData[0].TherapistTitle;
                            invoice.TreatmentType = searchData[0].TreatmentType;

                            //Retrieve old treatment count, totals before updating invoice node
                            int oldItemCount = 0;
                            double oldTotal = 0;
                            double oldTotalAfterTax = 0;
                            retrieveOldValuesFromInvoiceNode(node, out oldTotal, out oldTotalAfterTax, out oldItemCount);

                            updateInvoiceNode(node, invoice);

                            //Clear children
                            node.Nodes.Clear();

                            double total = 0.0;
                            double totalAfterTax = 0.0;
                            int itemCount = 0;
                            foreach (InvoiceSearchResultDTO row in searchData.Where(x => x.InvoiceItemId.HasValue)) {

                                InvoiceItemDTO item = new InvoiceItemDTO {
                                    Id = row.InvoiceItemId.Value,
                                    Version = row.InvoiceItemVersion.Value,
                                    InvoiceId = invoice.Id,
                                    ServiceDate = row.ServiceDate.Value,
                                    ServiceDescription = row.ServiceDescription,
                                    Amount = row.Amount.Value
                                };

                                TreeNode itemNode = createInvoiceItemNode(item);
                                node.Nodes.Add(itemNode);

                                total += item.Amount;
                                itemCount++;
                            }
                            totalAfterTax = total * (invoice.TaxRate.HasValue ? 1 + invoice.TaxRate.Value : 1);

                            node.Text = node.Text.Replace("{total}", string.Format("{0:C2}", total))
                                .Replace("{total after tax}", string.Format("{0:C2}", totalAfterTax));

                            //Update overall totals
                            //lblTotalAmount.Text = string.Format("{0:C2}", 
                            //    double.Parse(lblTotalAmount.Text.Trim(new char[] { '$' })) + (total - oldTotal));
                            //lblTotalAfterTax.Text = string.Format("{0:C2}",
                            //    double.Parse(lblTotalAfterTax.Text.Trim(new char[] { '$' })) + (totalAfterTax - oldTotalAfterTax));
                            //lblTotalTreatment.Text = string.Format("{0}", 
                            //    int.Parse(lblTotalTreatment.Text) + (itemCount - oldItemCount));
                            updateTotals();
                        }

                    } else if (nodeData is InvoiceItemDTO) {
                        //Never happen
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to reload node list. " + ex.Message);
                Program.log.Error("Failed to reload node list.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            if (rdoCollapseAll.Checked) {
                trvList.CollapseAll();
            } else {
                trvList.ExpandAll();
            }

            trvList.EndUpdate();
        }

        private TreeNode createPatientNode(PatientDTO p) {
            TreeNode pNode = new TreeNode(formatPatient(p));
            pNode.ImageIndex = (p.Sex == Sex.Female ? IMG_INDEX_FEMALE : p.Sex == Sex.Male ? IMG_INDEX_MALE : IMG_INDEX_PATIENT);
            pNode.SelectedImageIndex = pNode.ImageIndex;
            pNode.Tag = p;
            pNode.NodeFont = PATIENT_NODE_FONT;
            pNode.ForeColor = PATIENT_NODE_COLOR;

            return pNode;
        }

        private TreeNode createInvoiceNode(InvoiceDTO invoice) {
            TreeNode invNode = new TreeNode(formatInvoice(invoice));
            invNode.ImageIndex = IMG_INDEX_INVOICE;
            invNode.SelectedImageIndex = invNode.ImageIndex;
            invNode.Tag = invoice;

            return invNode;
        }

        private TreeNode createInvoiceItemNode(InvoiceItemDTO item) {
            TreeNode itemNode = new TreeNode(formatInvoiceItem(item));
            itemNode.ImageIndex = IMG_INDEX_INVOICE_ITEM;
            itemNode.SelectedImageIndex = itemNode.ImageIndex;
            itemNode.Tag = item;

            return itemNode;
        }

        private void updatePatientNode(TreeNode pNode, PatientDTO p) {
            pNode.Text = formatPatient(p);
            pNode.ImageIndex = (p.Sex == Sex.Female ? IMG_INDEX_FEMALE : p.Sex == Sex.Male ? IMG_INDEX_MALE : IMG_INDEX_PATIENT);
            pNode.SelectedImageIndex = pNode.ImageIndex;
            pNode.Tag = p;
            pNode.NodeFont = PATIENT_NODE_FONT;
            pNode.ForeColor = PATIENT_NODE_COLOR;
        }

        private void updateInvoiceNode(TreeNode invNode, InvoiceDTO invoice) {
            invNode.Text = formatInvoice(invoice);
            invNode.ImageIndex = IMG_INDEX_INVOICE;
            invNode.SelectedImageIndex = invNode.ImageIndex;
            invNode.Tag = invoice;
        }

        private void updateInvoiceItemNode(TreeNode itemNode, InvoiceItemDTO item) {
            itemNode.Text = formatInvoiceItem(item);
            itemNode.ImageIndex = IMG_INDEX_INVOICE_ITEM;
            itemNode.SelectedImageIndex = itemNode.ImageIndex;
            itemNode.Tag = item;
        }

        private void ucInsurer_UpdateDataList(object sender, EventArgs e) {
            ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
            using (ISession session = sessionFactory.OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    SimpleListItem aliasListItem = null;
                    var lstInsurer = session.QueryOver<Insurer>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.InsurerName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.InsurerName).Asc
                        .Take(Program.config.MaxInsurers)
                        .List<SimpleListItem>();
                    ucInsurer.UpdateListItems<SimpleListItem>(lstInsurer);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Insurer lists. " + ex.Message);
                    Program.log.Error("Failed to load Insurer lists.", ex);
                }
            }
        }

        private void ucTreatment_UpdateDataList(object sender, EventArgs e) {
            ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
            using (ISession session = sessionFactory.OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Treatment
                    SimpleListItem aliasListItem = null;
                    var lstTreatmentType = session.QueryOver<TherapyType>()
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => aliasListItem.Id)
                            .Select(x => x.TherapyTypeName).WithAlias(() => aliasListItem.Text)
                        )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .List<SimpleListItem>();
                    ucTreatment.UpdateListItems<SimpleListItem>(lstTreatmentType);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Treatment Type lists. " + ex.Message);
                    Program.log.Error("Failed to load Treatment Type lists.", ex);
                }
            }
        }

        private void ucTherapist_UpdateDataList(object sender, EventArgs e) {
            ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
            using (ISession session = sessionFactory.OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {

                try {
                    //Therapist
                    SimpleUserItem aliasUserItem = null;
                    User aUser = null;
                    UserTitle aTitle = null;
                    var lstTherapist = session.QueryOver<User>(() => aUser)
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
                        .List<SimpleUserItem>();
                    ucTherapist.UpdateListItems<SimpleUserItem>(lstTherapist);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Therapist lists. " + ex.Message);
                    Program.log.Error("Failed to load Therapist lists.", ex);
                }
            }
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e) {
            //reset pager
            pager.Page = 1;

            //reload list
            reloadList();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e) {
            //reset pager
            pager.Page = 1;

            //reload list
            reloadList();
        }

        private string formatPatient(PatientDTO p) {
            return string.Format("{0} - {1} - [Insurer:{2}]",
                p.FileNumber,
                p.DisplayName.PadRight(20, ' '),
                p.Insurer
                );
        }

        private string formatInvoice(InvoiceDTO invoice) {
            return string.Format("[{0}] - {1} - {2} - {3} - {4} - {5}",
                invoice.TherapistDisplayName.PadRight(20, ' '),
                invoice.InvoiceNumber.PadRight(10, ' '),
                invoice.StatementDate.ToString("ddd, MMM dd, yyyy"),
                "{total}",
                "(after tax: {total after tax})",
                invoice.TreatmentType
                );
        }

        private string formatInvoiceItem(InvoiceItemDTO item) {
            return string.Format("{0} [{1}] - {2:C2} - {3}",
                item.ServiceDate.ToString(item.ServiceDate.Hour == 0 ? "ddd, MMM dd, yyyy" : "ddd, MMM dd, yyyy hh:mmtt"),
                item.ServiceDuration.HasValue ? item.ServiceDuration.Value.ToString() + " minutes" : "",
                item.Amount,
                item.ServiceDescription
                );
        }

        private void retrieveOldValuesFromPatientNode(TreeNode patientNode, out double total, out double totalAfterTax, out int oldItemCount) {

            total = 0;
            totalAfterTax = 0;
            oldItemCount = 0;

            foreach (TreeNode node in patientNode.Nodes) {
                string[] texts = node.Text.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                double invTotal = 0;
                double invTotalAfterTax = 0;
                int invItemCount = 0;
                retrieveOldValuesFromInvoiceNode(node, out invTotal, out invTotalAfterTax, out invItemCount);

                total += invTotal;
                totalAfterTax += invTotalAfterTax;
                oldItemCount += invItemCount;
            }
        }

        private void retrieveOldValuesFromInvoiceNode(TreeNode node, out double total, out double totalAfterTax, out int oldItemCount) {

            string[] texts = node.Text.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            //The third last is total after tax, the second last is total before tax
            string sTotal = texts[texts.Length - 3]
                .Trim(new char[] { ' ', '$', '-' });
            string sTotalAfterTax = texts[texts.Length - 2]
                .Replace("after tax:", "")
                .Trim(new char[] { ' ', '$', '-', ')', '(' });

            total = double.Parse(sTotal);
            totalAfterTax = double.Parse(sTotalAfterTax);
            oldItemCount = node.Nodes.Count;
        }

        private void rdoCollapseAll_Click(object sender, EventArgs e) {
            expandCollapseTree();
        }

        private void rdoExpandAll_Click(object sender, EventArgs e) {
            expandCollapseTree();
        }

        private void expandCollapseTree() {
            trvList.BeginUpdate();
            if(rdoCollapseAll.Checked) {
                trvList.CollapseAll();
            } else {
                trvList.ExpandAll();
            }
            trvList.EndUpdate();
        }

        private void trvList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if(e.Button == System.Windows.Forms.MouseButtons.Right) {
                trvList.SelectedNode = e.Node;
                menu.Show(sender as Control, e.Location);
                setMenusFor(e.Node.Tag);
            }
        }

        private void setMenusFor(object o) {
            if(o is PatientDTO) {
                mnuView.Enabled = true;
                mnuAddNewInvoice.Enabled = true;
                mnuEditInvoice.Enabled = false;
                mnuDeleteInvoice.Enabled = false;
                mnuAddInvoiceItem.Enabled = false;
                mnuEditInvoiceItem.Enabled = false;
                mnuDeleteInvoiceItem.Enabled = false;
            } else if(o is InvoiceDTO) {
                mnuView.Enabled = true;
                mnuAddNewInvoice.Enabled = false;
                mnuEditInvoice.Enabled = true;
                mnuDeleteInvoice.Enabled = true;
                mnuAddInvoiceItem.Enabled = true;
                mnuEditInvoiceItem.Enabled = false;
                mnuDeleteInvoiceItem.Enabled = false;
            } else if(o is InvoiceItemDTO) {
                mnuView.Enabled = false;
                mnuAddNewInvoice.Enabled = false;
                mnuEditInvoice.Enabled = false;
                mnuDeleteInvoice.Enabled = false;
                mnuAddInvoiceItem.Enabled = false;
                mnuEditInvoiceItem.Enabled = true;
                mnuDeleteInvoiceItem.Enabled = true;
            }
        }

        private void mnuView_Click(object sender, EventArgs e) {
            if(trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if(nodeData is PatientDTO) {
                EditPatientForm patientForm = new EditPatientForm((nodeData as PatientDTO).Id, true);
                patientForm.ShowDialog(this);
            } else if(nodeData is InvoiceDTO) {
                (new EditInvoiceForm((nodeData as InvoiceDTO).Id, true)).ShowDialog();
            }
        }

        private void mnuAddNewInvoice_Click(object sender, EventArgs e) {
            if(trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if(nodeData is PatientDTO) {
                (new EditInvoiceForm(null, (nodeData as PatientDTO).Id)).ShowDialog(this);
                //reloadList();
                reloadNode(trvList.SelectedNode);
            }
        }

        private void mnuEditInvoice_Click(object sender, EventArgs e) {
            if(trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if(nodeData is InvoiceDTO) {
                (new EditInvoiceForm((nodeData as InvoiceDTO).Id)).ShowDialog(this);
                //reloadList();
                reloadNode(trvList.SelectedNode);
            }
        }

        private void mnuDeleteInvoice_Click(object sender, EventArgs e) {
            if (trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if (nodeData is InvoiceDTO) {

                int invId = (nodeData as InvoiceDTO).Id;

                if (DialogResult.Yes != MessageBox.Show(this,
                    "Do you want to delete this Invoice?",
                    "Delete Invoice",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) return;

                ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    try {
                        Invoice invToDelete = session.QueryOver<Invoice>()
                            .Where(x => x.Id == invId)
                            //.Fetch(x => x.Patient).Eager
                            .SingleOrDefault<Invoice>();
                        invToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                        invToDelete.UpdatedTime = DateTime.Now;

                        //Determine weather to delete all invoice items
                        IList<InvoiceItem> itemsToDelete = session.QueryOver<InvoiceItem>()
                            .Where(x => x.Invoice.Id == invToDelete.Id)
                            .List();
                        if (itemsToDelete.Count > 0) {
                            if (DialogResult.Yes != MessageBox.Show(this,
                                string.Format("There are {0} invoice item(s)!\n\nDo you still want to DELETE All of them?", itemsToDelete.Count),
                                "Delete Invoice with Invoice Items",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)) {
                                tr.Rollback();
                                return;
                            }
                            for (int i = itemsToDelete.Count - 1; i >= 0; i--) {
                                InvoiceItem item = itemsToDelete[i];
                                session.Delete(item);
                            }
                        }

                        session.Delete(invToDelete);
                        tr.Commit();
                    } catch (Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, "Failed to delete Invoice. " + ex.Message);
                        Program.log.Error("Failed to delete Invoice.", ex);
                    }
                }

                //reloadList();
                reloadNode(trvList.SelectedNode.Parent);
            }
        }

        private void mnuAddInvoiceItem_Click(object sender, EventArgs e) {
            if (trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if (nodeData is InvoiceDTO) {
                (new EditInvoiceItemForm(null, (nodeData as InvoiceDTO).Id)).ShowDialog(this);
                reloadNode(trvList.SelectedNode);
            }
        }

        private void mnuEditInvoiceItem_Click(object sender, EventArgs e) {
            if(trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if(nodeData is InvoiceItemDTO) {
                (new EditInvoiceItemForm((nodeData as InvoiceItemDTO).Id)).ShowDialog(this);
                //reloadList();
                reloadNode(trvList.SelectedNode.Parent);
            }
        }

        private void mnuDeleteInvoiceItem_Click(object sender, EventArgs e) {
            if (trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if (nodeData is InvoiceItemDTO) {

                int invItemId = (nodeData as InvoiceItemDTO).Id;

                if (DialogResult.Yes != MessageBox.Show(this,
                    "Do you want to delete this Invoice Item?",
                    "Delete Invoice Item",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) return;

                ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    try {
                        InvoiceItem invItemToDelete = session.QueryOver<InvoiceItem>()
                            .Where(x => x.Id == invItemId)
                            .Fetch(x => x.Invoice).Eager
                            .Fetch(x => x.Invoice.Patient).Eager
                            .SingleOrDefault<InvoiceItem>();

                        invItemToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                        invItemToDelete.UpdatedTime = DateTime.Now;
                        session.Delete(invItemToDelete);
                        tr.Commit();
                    } catch (Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, "Failed to delete Invoice Item. " + ex.Message);
                        Program.log.Error("Failed to delete Invoice Item.", ex);
                    }
                }

                //reloadList();
                reloadNode(trvList.SelectedNode.Parent);
            }
        }

        private void btnSearchPatient_Click(object sender, EventArgs e) {
            //reset pager
            pager.Page = 1;

            //reload list
            reloadList();
        }

        private void btnPrintInvoiceList_Click(object sender, EventArgs e) {
            try {
                //reload list with all data
                pager.Page = 1;
                reloadList(true);

                //Set page orientation, margins
                printDoc.DefaultPageSettings.Landscape = true;
                printDoc.DefaultPageSettings.Margins = new Margins(30, 30, 30, 30);

                PrintDialog dlgPrint = new PrintDialog();
                dlgPrint.Document = printDoc;

                if (DialogResult.OK == dlgPrint.ShowDialog(this)) {
                    curNode = null;
                    nodeToPrint = nextNode();
                    printDoc.Print();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to print invoice list. " + ex.Message);
                Program.log.Error("Failed to print invoice list.", ex);
            }
        }


        private TreeNode nextNode() {
            if(trvList.Nodes.Count <= 0) return null;

            if (curNode == null) {
                curNode = trvList.Nodes[0];
            } else if(curNode.Nodes.Count > 0) {
                curNode = curNode.Nodes[0];
            } else if(curNode.NextNode != null) {
                curNode = curNode.NextNode;
            } else {
                curNode = nextParentNode(curNode);
            }

            return curNode;
        }

        private TreeNode nextParentNode(TreeNode node) {

            if (node == null || node.Parent == null) return null;

            TreeNode parentSibling = node.Parent.NextNode;

            if (parentSibling != null) {
                return parentSibling;
            } else {
                return nextParentNode(node.Parent);
            }
        }

        // The PrintPage event is raised for each page to be printed. 
        private void pd_PrintPage(object sender, PrintPageEventArgs ev) {

            if(nodeToPrint == null) return;

            int pageHeight = ev.MarginBounds.Height;
            int pageWidth = ev.MarginBounds.Width;
            int top = ev.MarginBounds.Top;
            int left = ev.MarginBounds.Left;
            Brush brushToPrint = Brushes.Black;
            Font fontToPrint = trvList.Font;
            Graphics gc = ev.Graphics;

            #region Print search filters and totals

            const int FILTER_LABEL_WIDTH = 20;
            
            //1) Date range
            string strToPrint = string.Format("{0}{1} - {2}",
                "Date Range:".PadRight(FILTER_LABEL_WIDTH, ' '),
                dtFrom.Checked ? dtFrom.Value.ToString("MMM dd, yyyy") : "(Frist Day)",
                dtTo.Checked ? dtTo.Value.ToString("MMM dd, yyyy") : "(Today)");
            SizeF size = gc.MeasureString(strToPrint, fontToPrint);
            RectangleF recToPrint = new RectangleF(left, top, pageWidth, size.Height);

            gc.DrawString(strToPrint, fontToPrint, brushToPrint, recToPrint);
            top += Convert.ToInt32(Math.Ceiling(recToPrint.Height));

            //2) Therapist
            strToPrint = string.Format("{0}{1}",
                "Therapist:".PadRight(FILTER_LABEL_WIDTH, ' '),
                ucTherapist.AllChecked ? "(All)" : String.Join(", ", selectedTherapists.Select(x => x != null ? x.DisplayName : "(No Therapist)")));
            size = gc.MeasureString(strToPrint, fontToPrint);
            int lines = Convert.ToInt32(Math.Ceiling(size.Width / pageWidth));
            recToPrint = new RectangleF(left, top, pageWidth, size.Height * lines);

            gc.DrawString(strToPrint, fontToPrint, brushToPrint, recToPrint);
            top += Convert.ToInt32(Math.Ceiling(recToPrint.Height));

            //3) Treatment Type
            strToPrint = string.Format("{0}{1}",
                "Treatment Type:".PadRight(FILTER_LABEL_WIDTH, ' '),
                ucTreatment.AllChecked ? "(All)" : String.Join(", ", selectedTreatmentTypes.Select(x => x != null ? x.Text : "(No Treatment Type)")));
            size = gc.MeasureString(strToPrint, fontToPrint);
            lines = Convert.ToInt32(Math.Ceiling(size.Width / pageWidth));
            recToPrint = new RectangleF(left, top, pageWidth, size.Height * lines);

            gc.DrawString(strToPrint, fontToPrint, brushToPrint, recToPrint);
            top += Convert.ToInt32(Math.Ceiling(recToPrint.Height));

            //4) Insurer
            strToPrint = string.Format("{0}{1}",
                "Insurer:".PadRight(FILTER_LABEL_WIDTH, ' '),
                ucInsurer.AllChecked ? "(All)" : String.Join(", ", selectedInsurers.Select(x => x != null ? x.Text : "(No Insurer)")));
            size = gc.MeasureString(strToPrint, fontToPrint);
            lines = Convert.ToInt32(Math.Ceiling(size.Width / pageWidth));
            recToPrint = new RectangleF(left, top, pageWidth, size.Height * lines);

            gc.DrawString(strToPrint, fontToPrint, brushToPrint, recToPrint);
            top += Convert.ToInt32(Math.Ceiling(recToPrint.Height));

            //5) Totals
            top += 20;
            strToPrint = $"Total Patients: {lblPatientNumber.Text}, Total Treatments: {lblTotalTreatment.Text}, Total Amount: {lblTotalAmount.Text}, Total Amount After Tax: {lblTotalAfterTax.Text}";
            size = gc.MeasureString(strToPrint, fontToPrint);
            lines = Convert.ToInt32(Math.Ceiling(size.Width / pageWidth));
            recToPrint = new RectangleF(left, top, pageWidth, size.Height * lines);

            gc.DrawString(strToPrint, fontToPrint, brushToPrint, recToPrint);
            top += Convert.ToInt32(Math.Ceiling(recToPrint.Height));

            # endregion Print search filters and totals

            //Start to print list content
            top += 20;
            strToPrint = " ".PadRight((nodeToPrint.Level * 4), ' ') + nodeToPrint.Text;
            fontToPrint = (nodeToPrint.NodeFont != null ? nodeToPrint.NodeFont : trvList.Font);
            size = gc.MeasureString(strToPrint, fontToPrint);
            
            while(nodeToPrint != null && size.Height + top <= pageHeight) {

                lines = Convert.ToInt32(Math.Ceiling(size.Width / pageWidth));
                recToPrint = new RectangleF(left, top, pageWidth, size.Height * lines);

                ev.Graphics.DrawString(strToPrint, fontToPrint, brushToPrint, recToPrint);

                top += Convert.ToInt32(Math.Ceiling(recToPrint.Height));

                nodeToPrint = nextNode();
                if (nodeToPrint != null) {
                    strToPrint = " ".PadRight((nodeToPrint.Level * 4), ' ') + nodeToPrint.Text;
                    fontToPrint = (nodeToPrint.NodeFont != null ? nodeToPrint.NodeFont : trvList.Font); 
                    size = gc.MeasureString(strToPrint, fontToPrint);
                }
            }

            ev.HasMorePages = (nodeToPrint != null);
        }

        private void txtPatient_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchPatient_Click(null, null);
            }
        }

        // Update Totals based on the current search criteria
        private void updateTotals(int patientId = 0)
        {
            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get session
            ISession session = SessionFactory.GetOpenSession(key);

            try
            {
                // Use existing transaction if present, otherwise create a local one.
                ITransaction tr = session.Transaction;
                bool localTransaction = false;
                if (tr == null || !tr.IsActive)
                {
                    tr = session.BeginTransaction();
                    localTransaction = true;
                }

                try
                {
                    Patient aPatient = null;
                    Invoice aInvoice = null;
                    InvoiceItem aItem = null;
                    Insurer aInsurer = null;

                    // Prepare search query
                    var qry = session.QueryOver<Patient>(() => aPatient);

                    if(patientId > 0) {
                        qry.Where(() => aPatient.Id == patientId);
                    } else {
                        // Apply Patient filter
                        if (txtPatient.Text.Trim().Length > 0) {
                            string patientNameSearch = "%" + txtPatient.Text.Trim() + "%";
                            qry.Where(Restrictions.Disjunction()
                                .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.FirstName), patientNameSearch))
                                .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.LastName), patientNameSearch))
                                .Add(Restrictions.InsensitiveLike(Projections.Property(() => aPatient.MiddleName), patientNameSearch))
                                .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_LastName) LIKE ?",
                                    patientNameSearch,
                                    NHibernateUtil.String))
                                .Add(Expression.Sql("Concat(PTN_FirstName, ' ', PTN_MiddleName, ' ', PTN_LastName) LIKE ?",
                                    patientNameSearch,
                                    NHibernateUtil.String)));
                        }

                        // Join Insurer table if needed and apply filter
                        if (!ucInsurer.AllChecked) {
                            qry.JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin);
                            setInsurerFilter(qry, Projections.Property(() => aInsurer.Id));
                        }
                    }

                    // Always join Invoice and InvoiceItem tables
                    qry.JoinQueryOver<Invoice>(() => aPatient.Invoices, () => aInvoice, JoinType.InnerJoin);
                    qry.JoinQueryOver<InvoiceItem>(() => aInvoice.InvoiceItems, () => aItem, JoinType.InnerJoin);

                    // Apply more filters
                    setTreatmentTypeFilter(qry, Projections.Property(() => aInvoice.TreatmentType.Id));
                    setTherapistFilter(qry, Projections.Property(() => aInvoice.Therapist.Id));
                    setDateRangeFilter(qry, Projections.Property(() => aInvoice.StatementDate), Projections.Property(() => aItem.ServiceDate));

                    // Get total patient count
                    lblPatientNumber.Text = qry.Clone()
                        .Select(Projections.CountDistinct(() => aPatient.Id))
                        .SingleOrDefault<int>()
                        .ToString();

                    // Get total invoice item (treatment) count
                    lblTotalTreatment.Text = qry.Clone()
                        .Select(Projections.CountDistinct(() => aItem.Id))
                        .SingleOrDefault<int>()
                        .ToString();

                    // Get total Amount 
                    lblTotalAmount.Text = "$ " + qry.Clone()
                        .Select(Projections.Sum(() => aItem.Amount))
                        .SingleOrDefault<double>()
                        .ToString("0.00");

                    // Get total Amount after tax
                    lblTotalAfterTax.Text = "$ " + qry.Clone()
                        .Select(Projections.Sum(
                                Projections.SqlProjection(
                                    "INI_Amount * (1 + INV_TaxRate) as AmountWithTax",
                                    new[] { "AmountWithTax" },
                                    new IType[] { NHibernateUtil.Double }
                                )
                            ))
                        .SingleOrDefault<double>()
                        .ToString("0.00");

                    // Commit only if we created the local transaction. Read-only operations don't need commit when using an ambient transaction.
                    if (localTransaction)
                    {
                        tr.Commit();
                    }
                }
                catch
                {
                    // Rollback only if we created the transaction here.
                    if (localTransaction && tr != null && tr.IsActive)
                    {
                        tr.Rollback();
                    }
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to retrieve the totals. " + ex.Message);
                Program.log.Error("Failed to retrieve the totals. ", ex);
            }
            finally
            {
                SessionFactory.TryCloseSession(key);
            }
        }

        private void rdoServiceDateOnly_CheckedChanged(object sender, EventArgs e) {
            if(dtFrom.Checked || dtTo.Checked) {
                //reset pager
                pager.Page = 1;

                //reload list
                reloadList();
            }
        }

        private void rdoServiceAndStatementDates_CheckedChanged(object sender, EventArgs e) {
            if (dtFrom.Checked || dtTo.Checked) {
                //reset pager
                pager.Page = 1;

                //reload list
                reloadList();
            }
        }
    }
}
