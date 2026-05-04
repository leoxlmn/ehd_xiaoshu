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
using NHibernate.Transform;
using EHD.Admin.ViewModel;

namespace EHD.Admin {

    public partial class PendingTreatmentAlertForm : Form {

        private bool isSearching = false;

        public PendingTreatmentAlertForm() {
            InitializeComponent();
        }

        /*
        private void populateDropDownLists() {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    IList<TherapyType> lstTreatmentType = session.QueryOver<TherapyType>().List<TherapyType>();
                    drpTreatmentType.DataSource = lstTreatmentType;
                    drpTreatmentType.ValueMember = "Id";
                    drpTreatmentType.DisplayMember = "TherapyTypeName";
                    foreach(TherapyType type in lstTreatmentType) {
                        if(type.DisplayName.Contains("Massage")) {
                            drpTreatmentType.SelectedValue = type.Id;
                        }
                    }

                    tr.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load InitialTreatment. " + ex.Message);
                Program.log.Error("Failed to load InitialTreatment.", ex);
            }
        }*/

        private void reloadList() {

            lblNumOfPending.Text = "Searching...";
            int numFound = 0;
            grdList.Rows.Clear();

            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    //Get invoice items with no matching initial treatments
                    string qry = @"
SELECT 
    p.PTN_PTNId as PatientId,
    Concat(p.PTN_FirstName, ' ', p.PTN_LastName) as PatientName,
    inv.INV_INVId as InvoiceId,
    inv.INV_InvoiceNumber as InvoiceNumber,
    prac.USR_USRId as PractitionerId,
    Concat(prac.USR_FirstName, ' ', prac.USR_LastName) as PractitionerName,
    type.THP_THPId as TreatmentTypeId,
    type.THP_TherapyType as TreatmentTypeName,
    item.INI_INIId as InvoiceItemId, 
    item.INI_ServiceDescription as Service, 
    item.INI_ServiceDate as ServiceDate
FROM invoice as inv
	INNER JOIN invoiceitem as item ON item.INI_INVId = inv.INV_INVId
	INNER JOIN patient as p ON inv.INV_PTNId = p.PTN_PTNId
    INNER JOIN user as prac ON inv.INV_USRId = prac.USR_USRId
    INNER JOIN therapytype type ON inv.INV_THPId = type.THP_THPId
	LEFT JOIN initialtreatment as it 
		ON it.ITR_PTNId = p.PTN_PTNId
			And it.ITR_USRId = inv.INV_USRId
			And it.ITR_THPId = type.THP_THPId
			And 
				Year(it.ITR_TreatmentTime) = Year(item.INI_ServiceDate) And
				Month(it.ITR_TreatmentTime) = Month(item.INI_ServiceDate) And
				Day(it.ITR_TreatmentTime) = Day(item.INI_ServiceDate)
WHERE it.ITR_ITRId is null
";
                    //Add filters
                    qry = setFilters(qry);

                    //Add order
                    qry += @"
ORDER BY p.PTN_FirstName, p.PTN_LastName, type.THP_TherapyType, item.INI_ServiceDate
";

                    var invItemsNoInitialTreatment = session.CreateSQLQuery(qry)
                        .AddScalar("PatientId", NHibernateUtil.Int32)
                        .AddScalar("PatientName", NHibernateUtil.String)
                        .AddScalar("InvoiceId", NHibernateUtil.Int32)
                        .AddScalar("InvoiceNumber", NHibernateUtil.String)
                        .AddScalar("PractitionerId", NHibernateUtil.Int32)
                        .AddScalar("PractitionerName", NHibernateUtil.String)
                        .AddScalar("TreatmentTypeId", NHibernateUtil.Int32)
                        .AddScalar("TreatmentTypeName", NHibernateUtil.String)
                        .AddScalar("InvoiceItemId", NHibernateUtil.Int32)
                        .AddScalar("Service", NHibernateUtil.String)
                        .AddScalar("ServiceDate", NHibernateUtil.DateTime)
                        .SetResultTransformer(Transformers.AliasToBean(typeof(InvoiceItemViewModel)))
                        .Future <InvoiceItemViewModel>();

                    //Get invoice items with no matching followup treatments

                    //a) prepare sub query filter
                    string subqryFilter = string.Empty;
                    if (ucPatient.SelectedItem != null) {
                        subqryFilter = subqryFilter + @"
AND pT.PTN_PTNId = " + ucPatient.SelectedItem.Id.ToString();
                    }

                    subqryFilter = setInsurerFilter(subqryFilter, "pT.PTN_INSId");
                    subqryFilter = setTherapistFilter(subqryFilter, "t2.FTR_USRId");
                    subqryFilter = setTreatmentTypeFilter(subqryFilter, "t1.ITR_THPId");

                    //b) prepare query
                    qry = @"
SELECT
    p.PTN_PTNId as PatientId,
    Concat(p.PTN_FirstName, ' ', p.PTN_LastName) as PatientName,
    inv.INV_INVId as InvoiceId,
    inv.INV_InvoiceNumber as InvoiceNumber,
    prac.USR_USRId as PractitionerId,
    Concat(prac.USR_FirstName, ' ', prac.USR_LastName) as PractitionerName,
    type.THP_THPId as TreatmentTypeId,
    type.THP_TherapyType as TreatmentTypeName,
    item.INI_INIId as InvoiceItemId, 
    item.INI_ServiceDescription as Service, 
    item.INI_ServiceDate as ServiceDate
FROM invoice as inv
	INNER JOIN invoiceitem as item ON item.INI_INVId = inv.INV_INVId
	INNER JOIN patient as p ON inv.INV_PTNId = p.PTN_PTNId
    INNER JOIN user as prac ON inv.INV_USRId = prac.USR_USRId
    INNER JOIN therapytype type ON inv.INV_THPId = type.THP_THPId
	LEFT JOIN  
			(
                SELECT t2.FTR_FTRId, t1.ITR_PTNId, t2.FTR_USRId, t2.FTR_TreatmentTime, t1.ITR_THPId
                FROM initialtreatment as t1
				    INNER JOIN followuptreatment as t2 ON t2.FTR_ITRId = t1.ITR_ITRId 
                    INNER JOIN patient as pT ON t1.ITR_PTNId = pT.PTN_PTNId
" + subqryFilter + @"
            ) as T
         ON T.ITR_PTNId = p.PTN_PTNId
			AND T.FTR_USRId = inv.INV_USRId
			And T.ITR_THPId = type.THP_THPId
            And Year(T.FTR_TreatmentTime) = Year(item.INI_ServiceDate)
            And Month(T.FTR_TreatmentTime) = Month(item.INI_ServiceDate)
            And Day(T.FTR_TreatmentTime) = Day(item.INI_ServiceDate)
WHERE T.FTR_FTRId is null
";
                    //Add filters
                    qry = setFilters(qry);

                    //Add order
                    qry += @"
ORDER BY p.PTN_FirstName, p.PTN_LastName, type.THP_TherapyType, item.INI_ServiceDate
";
                    var invItemsNoFollowupTreatment = session.CreateSQLQuery(qry)
                        .AddScalar("PatientId", NHibernateUtil.Int32)
                        .AddScalar("PatientName", NHibernateUtil.String)
                        .AddScalar("InvoiceId", NHibernateUtil.Int32)
                        .AddScalar("InvoiceNumber", NHibernateUtil.String)
                        .AddScalar("PractitionerId", NHibernateUtil.Int32)
                        .AddScalar("PractitionerName", NHibernateUtil.String)
                        .AddScalar("TreatmentTypeId", NHibernateUtil.Int32)
                        .AddScalar("TreatmentTypeName", NHibernateUtil.String)
                        .AddScalar("InvoiceItemId", NHibernateUtil.Int32)
                        .AddScalar("Service", NHibernateUtil.String)
                        .AddScalar("ServiceDate", NHibernateUtil.DateTime)
                        .SetResultTransformer(Transformers.AliasToBean(typeof(InvoiceItemViewModel)))
                        .Future<InvoiceItemViewModel>();

                    //Find all invoice items having neither initial nor followup treatments
                    List<InvoiceItemViewModel> invItemsNoTreatments = new List<InvoiceItemViewModel>();
                    foreach (InvoiceItemViewModel invItem in invItemsNoInitialTreatment) {
                        if (invItemsNoFollowupTreatment.Any(x => x.InvoiceItemId == invItem.InvoiceItemId)) {
                            invItemsNoTreatments.Add(invItem);
                        }
                    }
                    numFound = invItemsNoTreatments.Count;

                    //Set pager
                    if (numFound == 0) {
                        pager.TotalPage = 1;
                    } else {
                        pager.TotalPage = Convert.ToInt32(Math.Floor((double)(numFound - 1) / pager.PageSize)) + 1;
                    }

                    //Show search total result
                    lblNumOfPending.Text = numFound.ToString();

                    //Apply paging
                    int iStart = (pager.Page - 1) * pager.PageSize;
                    int iEnd = iStart + pager.PageSize - 1;
                    if (iEnd >= numFound) iEnd = numFound - 1;

                    for (int i = iStart; i <= iEnd; i++) {
                        InvoiceItemViewModel invItemVM = invItemsNoTreatments[i];

                        InvoiceItem invItem = session.QueryOver<InvoiceItem>()
                            .Fetch(x => x.Invoice).Eager
                            .Fetch(x => x.Invoice.Patient).Eager
                            .Fetch(x => x.Invoice.TreatmentType).Eager
                            .Fetch(x => x.Invoice.Therapist).Eager
                            .Where(x => x.Id == invItemVM.InvoiceItemId)
                            .SingleOrDefault<InvoiceItem>();

                        grdList.Rows.Add(invItem.Invoice.Patient,
                            invItem.Invoice,
                            invItem,
                            new CreateNewInitialTreatment(invItem.Invoice.Patient.Id, invItem.Invoice.TreatmentType.Id, invItem.ServiceDate),
                            new CreateNewFollowUp(invItem.Invoice.Patient.Id, invItem.Invoice.TreatmentType.Id, invItem.ServiceDate)
                            );
                    }

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to retrieve invoices. " + ex.Message);
                    Program.log.Error("Failed to retrieve invoices.", ex);
                }
            }
        }

        /* Rewrote
        private void reloadList() {

            lblNumOfPending.Text = "Searching...";
            int numFound = 0;
            grdList.Rows.Clear();

            //Get searching criteria
            int treatmentTypeId = int.Parse(drpTreatmentType.SelectedValue.ToString());
            DateTime searchFromDate = dtSearchFrom.Value;

            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {
                try {
                    //Get all invoice items
                    var qryInvoiceItems = session.QueryOver<InvoiceItem>()
                        .JoinQueryOver<Invoice>(x => x.Invoice)
                        .Where(x => x.TreatmentType.Id == treatmentTypeId)
                        .Where(x => x.StatementDate >= searchFromDate)
                        .Future();

                    //Get all initial treatments
                    var qryInit = session.QueryOver<InitialTreatment>()
                        .Where(x => x.TreatmentType.Id == treatmentTypeId)
                        .Where(x => x.TreatmentTime >= searchFromDate)
                        .Future();

                    //Get all follow up treatments
                    var qryFollowUp = session.QueryOver<FollowUpTreatment>()
                        .JoinQueryOver<InitialTreatment>(x => x.InitialTreatment)
                        .Where(x => x.TreatmentType.Id == treatmentTypeId)
                        .Where(x => x.TreatmentTime >= searchFromDate)
                        .Future();

                    //Run queries
                    var lstInvoiceItems = qryInvoiceItems.ToList<InvoiceItem>()
                        .OrderBy(x => x.Invoice.Patient)
                        .OrderBy(x => x.Invoice.StatementDate)
                        .OrderBy(x => x.ServiceDate)
                        .ToList();
                    var lstInit = qryInit.ToList<InitialTreatment>();
                    var lstFollowUp = qryFollowUp.ToList<FollowUpTreatment>();

                    //Update list view
                    foreach(InvoiceItem item in lstInvoiceItems) {
                        InitialTreatment matchedIt = null;
                        foreach(InitialTreatment it in lstInit) {
                            if(item.Invoice.Patient == it.Patient &&
                                item.Invoice.TreatmentType == it.TreatmentType &&
                                item.ServiceDate.Date == it.TreatmentTime.Date) {
                                    matchedIt = it;
                                    break;
                            }
                        }

                        FollowUpTreatment matchedFt = null;
                        foreach(FollowUpTreatment ft in lstFollowUp) {
                            InitialTreatment ftIt = ft.InitialTreatment;
                            if(item.Invoice.Patient == ftIt.Patient &&
                                item.Invoice.TreatmentType == ftIt.TreatmentType &&
                                item.ServiceDate.Date == ft.TreatmentTime.Date) {
                                matchedFt = ft;
                                break;
                            }
                        }

                        if((matchedIt != null && matchedIt.IsComplete) ||
                            (matchedFt != null && matchedFt.IsComplete)) {

                            //This invoice item is clear, do not show on the alert list
                            continue;
                        } else {
                            object grdValueInitTreatment = (matchedIt == null ?
                                (object)(new CreateNewInitialTreatment(item.Invoice.Patient, item.Invoice.TreatmentType, item.ServiceDate.Date))
                                : matchedIt);
                            object grdValueFollowUp = (matchedFt == null ?
                                (object)(new CreateNewFollowUp(item.Invoice.Patient, item.Invoice.TreatmentType, item.ServiceDate.Date))
                                : matchedFt);

                            grdList.Rows.Add(item.Invoice.Patient,
                                item.Invoice,
                                item,
                                grdValueInitTreatment,
                                grdValueFollowUp);
                            numFound++;
                        }

                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to retrieve patients with invoice info. " + ex.Message);
                    Program.log.Error("Failed to retrieve patients with invoice info.", ex);
                }
            }

            lblNumOfPending.Text = numFound.ToString();
        }
        */

        private void PendingTreatmentAlertForm_Load(object sender, EventArgs e) {
            //populateDropDownLists();
            //setSearchFromDate();

            //Initiate pager
            pager.Page = 1;
            pager.GoToPageEvent += new GoToPageEventHandler(pageNavigate);

            loadCheckedListBoxes();

            //reloadList();
        }

        private void pageNavigate(int pageNumber) {
            reloadList();
        }

        private void loadCheckedListBoxes() {
            //Create key for session access
            Guid key = Guid.NewGuid();

            //Get session
            ISession session = SessionFactory.GetOpenSession(key);

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    //Load all reference tables in one batch query
                    var lstInsurer = session.QueryOver<Insurer>()
                        .OrderBy(x => x.InsurerName).Asc
                        .Take(Program.config.MaxInsurers)
                        .Future<Insurer>();

                    var lstTreatmentType = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .Take(Program.config.MaxTreatmentTypes)
                        .Future<TherapyType>();

                    var lstTherapist = session.QueryOver<User>()
                        .Fetch(x => x.Title).Eager
                        .OrderBy(x => x.FirstName).Asc
                        .OrderBy(x => x.LastName).Asc
                        .Take(Program.config.MaxTherapists)
                        .Future<User>();

                    //Insurer
                    foreach (Insurer insurer in lstInsurer) {
                        ucInsurer.AddItem(insurer, true);
                    }
                    ucInsurer.CheckCompleted += new EventHandler(filter_selected);

                    //Treatment Types
                    foreach (TherapyType tt in lstTreatmentType) {
                        ucTreatment.AddItem(tt, true);
                    }
                    ucTreatment.CheckCompleted += new EventHandler(filter_selected);

                    //Therapist
                    foreach (User usr in lstTherapist) {
                        if (usr.UserType == UserType.Therapist) {
                            ucTherapist.AddItem(usr, true);
                        }
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

        private IList<Insurer> selectedInsurers {
            get {
                IList<Insurer> _selectedInsurers = new List<Insurer>();
                foreach (object item in ucInsurer.CheckedItems) {
                    _selectedInsurers.Add(item as Insurer);
                }
                return _selectedInsurers;
            }
        }

        private IList<User> selectedTherapists {
            get {
                IList<User> _selectedTherapists = new List<User>();
                foreach (object item in ucTherapist.CheckedItems) {
                    _selectedTherapists.Add(item as User);
                }
                return _selectedTherapists;
            }
        }

        private IList<TherapyType> selectedTreatmentTypes {
            get {
                IList<TherapyType> _selectedTreatmentTypes = new List<TherapyType>();
                foreach (object item in ucTreatment.CheckedItems) {
                    _selectedTreatmentTypes.Add(item as TherapyType);
                }
                return _selectedTreatmentTypes;
            }
        }

        //Add Insurer filter
        private ICriteria setInsurerFilter(ICriteria query, string fieldPath) {

            if (!ucInsurer.AllChecked) {
                if (ucInsurer.AllNonBlanks) {
                    query = query.Add(Restrictions.IsNotNull(fieldPath));
                } else if (ucInsurer.BlanksOnly) {
                    query = query.Add(Restrictions.IsNull(fieldPath));
                } else if (!ucInsurer.IsBlankChecked) {
                    query = query.Add(Restrictions.In(fieldPath, selectedInsurers.ToArray<Insurer>()));
                } else {
                    query = query.Add(Expression.Disjunction()
                        .Add(Restrictions.IsNull(fieldPath))
                        .Add(Restrictions.In(fieldPath, selectedInsurers.ToArray<Insurer>()))
                        );
                }
            }

            return query;
        }

        private string setInsurerFilter(string qry, string field) {

            if (!ucInsurer.AllChecked) {
                if (ucInsurer.AllNonBlanks) {
                    qry = qry + @"
AND " + field + " is not null";
                } else if (ucInsurer.BlanksOnly) {
                    qry = qry + @"
AND " + field + " is null";
                } else if (!ucInsurer.IsBlankChecked) {
                    if (selectedInsurers.Count > 0) {
                        qry = qry + @"
AND " + field + " in(" + string.Join(",", selectedInsurers.Select(x => x.Id).ToArray()) + ")";
                    }
                } else {//Checked Blanks and other insurers
                    qry = qry + @"
AND (" + field + " is null OR " + field + " in(" + string.Join(",", selectedInsurers.Select(x => x.Id).ToArray()) + "))";
                }
            }

            return qry;
        }

        //Add Therapist filter
        private ICriteria setTherapistFilter(ICriteria query, string fieldPath) {

            if (!ucTherapist.AllChecked) {
                if (ucTherapist.AllNonBlanks) {
                    query = query.Add(Restrictions.IsNotNull(fieldPath));
                } else if (ucTherapist.BlanksOnly) {
                    query = query.Add(Restrictions.IsNull(fieldPath));
                } else if (!ucTherapist.IsBlankChecked) {
                    query = query.Add(Restrictions.In(fieldPath, selectedTherapists.ToArray<User>()));
                } else {
                    query = query.Add(Expression.Disjunction()
                        .Add(Restrictions.IsNull(fieldPath))
                        .Add(Restrictions.In(fieldPath, selectedTherapists.ToArray<User>()))
                        );
                }
            }

            return query;
        }

        private string setTherapistFilter(string qry, string field) {

            if (!ucTherapist.AllChecked) {
                if (ucTherapist.AllNonBlanks) {
                    qry = qry + @"
AND " + field + " is not null";
                } else if (ucTherapist.BlanksOnly) {
                    qry = qry + @"
AND " + field + " is null";
                } else if (!ucTherapist.IsBlankChecked) {
                    if (selectedTherapists.Count > 0) {
                        qry = qry + @"
AND " + field + " in(" + string.Join(",", selectedTherapists.Select(x => x.Id).ToArray()) + ")";
                    }
                } else {
                    qry = qry + @"
AND (" + field + " is null OR " + field + " in(" + string.Join(",", selectedTherapists.Select(x => x.Id).ToArray()) + "))";
                }
            }

            return qry;
        }

        //Add TreatmentType filter
        private ICriteria setTreatmentTypeFilter(ICriteria query, string fieldPath) {

            if (!ucTreatment.AllChecked) {
                if (ucTreatment.AllNonBlanks) {
                    query = query.Add(Restrictions.IsNotNull(fieldPath));
                } else if (ucTreatment.BlanksOnly) {
                    query = query.Add(Restrictions.IsNull(fieldPath));
                } else if (!ucTreatment.IsBlankChecked) {
                    query = query.Add(Restrictions.In(fieldPath, selectedTreatmentTypes.ToArray<TherapyType>()));
                } else {
                    query = query.Add(Expression.Disjunction()
                        .Add(Restrictions.IsNull(fieldPath))
                        .Add(Restrictions.In(fieldPath, selectedTreatmentTypes.ToArray<TherapyType>()))
                        );
                }
            }

            return query;
        }

        private string setTreatmentTypeFilter(string qry, string field) {

            if (!ucTreatment.AllChecked) {
                if (ucTreatment.AllNonBlanks) {
                    qry = qry + @"
AND " + field + " is not null";
                } else if (ucTreatment.BlanksOnly) {
                    qry = qry + @"
AND " + field + " is null";
                } else if (!ucTreatment.IsBlankChecked) {
                    if (selectedTreatmentTypes.Count > 0) {
                        qry = qry + @"
AND " + field + " in(" + string.Join(",", selectedTreatmentTypes.Select(x => x.Id).ToArray()) + ")";
                    }
                } else {
                    qry = qry + @"
AND (" + field + " is null OR " + field + " in(" + string.Join(",", selectedTreatmentTypes.Select(x => x.Id).ToArray()) + "))";
                }
            }

            return qry;
        }

        private string setDateRanges(string qry) {
            if (dtCreatedFrom.Checked) qry = qry + "\n AND item.INI_CreatedTime >= '" + dtCreatedFrom.Value.ToString("yyyy-MM-dd") + "'";
            if (dtCreatedTo.Checked) qry = qry + "\n AND item.INI_CreatedTime < '" + dtCreatedTo.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
            if (dtServiceDateFrom.Checked) qry = qry + "\n AND item.INI_ServiceDate >= '" + dtServiceDateFrom.Value.ToString("yyyy-MM-dd") + "'";
            if (dtServiceDateTo.Checked) qry = qry + "\n AND item.INI_ServiceDate < '" + dtServiceDateTo.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";

            return qry;
        }

        private string setFilters(string qry) {

            //Add patient filter
            if (ucPatient.SelectedItem != null) {
                qry = qry + @"
AND p.PTN_PTNId = " + ucPatient.SelectedItem.Id.ToString();
            }

            //Add date ranges
            qry = setDateRanges(qry);

            //Add insurer filter
            qry = setInsurerFilter(qry, "p.PTN_INSId");

            //Add practitioner filter
            qry = setTherapistFilter(qry, "inv.INV_USRId");

            //Add treatment type filter
            qry = setTreatmentTypeFilter(qry, "inv.INV_THPId");

            return qry;
        }

        /*
        private void setSearchFromDate() {
            if (null == Program.GetUserCache(Constants.CACHE_KEY_INVOICE_ALERT_SEARCH_FROM)) {
                //By default, search invoices dating back to 1 month
                dtSearchFrom.Value = DateTime.Now.Date.AddMonths(-1);
            } else {
                dtSearchFrom.Value = (DateTime) Program.GetUserCache(Constants.CACHE_KEY_INVOICE_ALERT_SEARCH_FROM);
            }
        }*/

        /*
        private void btnSearch_Click(object sender, EventArgs e) {
            //Save search from date into cache
            Program.AddUserCache(Constants.CACHE_KEY_INVOICE_ALERT_SEARCH_FROM, dtSearchFrom.Value);

            reloadList();
        }*/

        private void grdList_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0) {
                object val = grdList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (val is Patient) {//do nothing
                } else if (val is Invoice) {
                    //Display invoice
                    (new EditInvoiceForm((val as Invoice).Id, true)).ShowDialog(this);
                } else if (val is InvoiceItem) {
                    //Display invoice item
                    (new EditInvoiceItemForm(val as InvoiceItem, true)).ShowDialog(this);
                } else if (val is InitialTreatment) {
                    //Edit initial treatment
                    if (DialogResult.OK == (new EditInitialTreatmentForm(0, (val as InitialTreatment).Id)).ShowDialog(this)) {
                        reloadList();
                    }
                } else if (val is FollowUpTreatment) {
                    //Edit follow up treatment
                    if (DialogResult.OK == (new EditFollowUpTreatmentForm(0, (val as FollowUpTreatment).Id)).ShowDialog(this)) {
                        reloadList();
                    }
                } else if (val is CreateNewInitialTreatment) {
                    CreateNewInitialTreatment createNewInit = val as CreateNewInitialTreatment;
                    if (DialogResult.OK == (new EditInitialTreatmentForm(createNewInit.PatientId, 0, false, createNewInit.TreatmentTypeId, createNewInit.TreatmentDate)).ShowDialog(this)) {
                        reloadList();
                    }
                } else if (val is CreateNewFollowUp) {
                    CreateNewFollowUp createNewFollowUp = val as CreateNewFollowUp;
                    SelectInitialTreatmentForm frmSelectInitTreatment =
                        new SelectInitialTreatmentForm(createNewFollowUp.PatientId, createNewFollowUp.TreatmentTypeId, createNewFollowUp.TreatmentDate);
                    DialogResult ret = frmSelectInitTreatment.ShowDialog(this);
                    if (DialogResult.OK == ret) {
                        if (DialogResult.OK == (new EditFollowUpTreatmentForm(frmSelectInitTreatment.SelectedInitialTreatmentId, 0, false, createNewFollowUp.TreatmentDate.Date)).ShowDialog(this)) {
                            reloadList();
                        }
                    } else if (DialogResult.Retry == ret) {//Create Initial Treatment Instead
                        if (DialogResult.OK == (new EditInitialTreatmentForm(createNewFollowUp.PatientId, 0, false, createNewFollowUp.TreatmentTypeId, createNewFollowUp.TreatmentDate.Date)).ShowDialog(this)) {
                            reloadList();
                        }
                    }
                } else {//do nothing
                }
            }
        }

        private void ucInsurer_UpdateDataList(object sender, EventArgs e) {
            ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
            using (ISession session = sessionFactory.OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    //Insurer
                    IList<Insurer> lstInsurer = session.QueryOver<Insurer>()
                        .OrderBy(x => x.InsurerName).Asc
                        .List<Insurer>();
                    ucInsurer.UpdateListItems<Insurer>(lstInsurer);

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
                    IList<TherapyType> lstService = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .List<TherapyType>();
                    ucTreatment.UpdateListItems<TherapyType>(lstService);

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
                    IList<User> lstTherapist = session.QueryOver<User>()
                        .Fetch(x => x.Title).Eager
                        .Where(x => x.UserType == UserType.Therapist)
                        .OrderBy(x => x.FirstName).Asc
                        .OrderBy(x => x.LastName).Asc
                        .List<User>();
                    ucTherapist.UpdateListItems<User>(lstTherapist);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Therapist lists. " + ex.Message);
                    Program.log.Error("Failed to load Therapist lists.", ex);
                }
            }
        }

        private void txtPatient_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                btnSearchPatient_Click(null, null);
            }
        }

        private void btnSearchPatient_Click(object sender, EventArgs e) {
            //reset pager
            pager.Page = 1;

            //reload list
            reloadList();
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

        private void ucPatient_MinInputLengthReached(object sender, EventArgs e) {
            if (isSearching) return;

            isSearching = true;

            //Search patients
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    //Search top 20 matches
                    var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Insurer).Eager
                        .Where(Restrictions.Disjunction()
                            .Add(Restrictions.Eq(Projections.Property<Patient>(x => x.FileNumber), ucPatient.InputText))
                            .Add(Restrictions.On<Patient>(x => x.FirstName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.LastName).IsInsensitiveLike(ucPatient.InputText + "%"))
                            )
                        .OrderBy(x => x.FirstName).Asc
                        .Take(20)
                        .List<Patient>();

                    IList<SimpleListItem> lstPatient = new List<SimpleListItem>();
                    foreach (Patient patient in patients) {
                        lstPatient.Add(new SimpleListItem {
                            Id = patient.Id,
                            Text = string.Format("{0} - {1} {2}", patient.FileNumber, patient.FirstName, patient.LastName)
                        });
                    }

                    ucPatient.UpdateList(lstPatient);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to search patient. " + ex.Message);
                    Program.log.Error("Failed to search patient.", ex);
                } finally {
                    isSearching = false;
                }
            }
        }

        private void btnAutoCreateFollowups_Click(object sender, EventArgs e) {
            int totalCreated = 0;
            if(grdList.RowCount > 0) {

                using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {

                    try {

                        //1) Get default followup treatment and detail templates for each treatment type
                        Dictionary<int, TemplateTreatmentDetail> templates = new Dictionary<int, TemplateTreatmentDetail>();
                        Dictionary<int, IFollowUpDetail> detailTemplates = new Dictionary<int, IFollowUpDetail>();

                        var qryTemplates = session.QueryOver<TemplateTreatmentDetail>()
                            .Where(x => x.IsInitial == false)
                            .And(x => x.IsDefault == true)
                            .List<TemplateTreatmentDetail>();

                        foreach(TemplateTreatmentDetail temp in qryTemplates) {
                            if (!templates.ContainsKey(temp.TreatmentType.Id)) {
                                templates.Add(temp.TreatmentType.Id, temp);

                                IFollowUpDetail templateDetail = null;

                                if (temp.TreatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower())) {

                                    templateDetail = session.Get<ChiropracticFollowUpDetail>(temp.DetailId);

                                } else if (temp.TreatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {

                                    templateDetail = session.Get<OsteopathyFollowUpDetail>(temp.DetailId);

                                } else if (temp.TreatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {

                                    templateDetail = session.Get<MassageFollowUpDetail>(temp.DetailId);

                                } else if (temp.TreatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {

                                    templateDetail = session.Get<AcupunctureFollowUpDetail>(temp.DetailId);

                                } else if (temp.TreatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.Trim().ToLower())) {

                                    //do nothing

                                } else if (temp.TreatmentType.TherapyTypeName.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.Trim().ToLower())) {
                                    //do nothing
                                } else {
                                    throw new ApplicationException("Unknown treatment type.");
                                }

                                detailTemplates.Add(temp.TreatmentType.Id, templateDetail);
                            }
                        }

                        //2) Go through all rows
                        #region Go through all rows
                        foreach(DataGridViewRow row in grdList.Rows) {

                            //a) Gather data from row
                            int patientId = (row.Cells[0].Value as Patient).Id;
                            int treatmentTypeId = (row.Cells[1].Value as Invoice).TreatmentType.Id;
                            DateTime treatmentTime = (row.Cells[2].Value as InvoiceItem).ServiceDate;
                            User therapist = (row.Cells[1].Value as Invoice).Therapist;

                            int duration = 30; //By default set the treatment to be 30 minutes if no service duration specified or it's 0. Otherwise, use the same duration as the invoice item.
                            InvoiceItem invItem = row.Cells[2].Value as InvoiceItem;
                            if (invItem.ServiceDuration.HasValue && invItem.ServiceDuration.Value > 0) {
                                duration = invItem.ServiceDuration.Value;
                            }

                            //b) Check if a template is available for the treatment type
                            if (!templates.ContainsKey(treatmentTypeId)) continue;

                            //c) Check if an Initial Treatment exists
                            var initTreatment = session.QueryOver<InitialTreatment>()
                                .Where(x => x.Patient.Id == patientId)
                                .And(x => x.TreatmentType.Id == treatmentTypeId)
                                .And(x => x.TreatmentTime <= treatmentTime)
                                .OrderBy(x => x.TreatmentType).Desc
                                .Take(1)
                                .SingleOrDefault<InitialTreatment>();
                            if (initTreatment == null) continue;

                            //d) Create a new Followup
                            FollowUpTreatment ft = new FollowUpTreatment {
                                CreatedBy = "auto",
                                CreatedTime = DateTime.Now,
                                IsComplete = true,
                                Therapist = therapist,
                                TreatmentTime = treatmentTime,
                                InitialTreatment = initTreatment,
                                Note = templates[treatmentTypeId].TreatmentNote,
                                TreatmentDurationMinutes = duration
                            };
                            session.Save(ft);

                            //e) Create a new Followup Detail
                            string treatmentTypeName = initTreatment.TreatmentType.TherapyTypeName.Trim();
                            if(treatmentTypeName.Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE, StringComparison.CurrentCultureIgnoreCase)) {
                                AcupunctureFollowUpDetail tempDetail = detailTemplates[treatmentTypeId] as AcupunctureFollowUpDetail;
                                AcupunctureFollowUpDetail aDetail = new AcupunctureFollowUpDetail {
                                    FollowUpTreatment = ft,
                                    CreatedBy = "auto",
                                    CreatedTime = DateTime.Now,
                                    BloodPressure = tempDetail.BloodPressure,
                                    BloodSugar = tempDetail.BloodSugar,
                                    Pulse = tempDetail.Pulse,
                                    Subjective = tempDetail.Subjective,
                                    TCMDiagnosis = tempDetail.TCMDiagnosis,
                                    Tongue = tempDetail.Tongue,
                                    TreatmentPlan = tempDetail.TreatmentPlan,
                                    TongueDiagram = tempDetail.TongueDiagram
                                };
                                session.Save(aDetail);

                                //Create points
                                IList<PointOnDiagram> points = (new JCService(session, Program.LogonUser)).GetAcupunctureFollowUpTonguePoints(tempDetail, Program.config.MaxPointsPerDiagram);
                                foreach (PointOnDiagram p in points) {
                                    PointOnAcupunctureFollowUp newP = new PointOnAcupunctureFollowUp {
                                        AcupunctureFollowUpDetail = aDetail,
                                        CreatedBy = "auto",
                                        CreatedTime = DateTime.Now,
                                        Diagram = tempDetail.TongueDiagram,
                                        PainKey = p.PainKey,
                                        X = p.X,
                                        Y = p.Y
                                    };
                                    session.Save(newP);
                                }

                            } else if (treatmentTypeName.Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC, StringComparison.CurrentCultureIgnoreCase)) {
                                ChiropracticFollowUpDetail tempDetail = detailTemplates[treatmentTypeId] as ChiropracticFollowUpDetail;
                                ChiropracticFollowUpDetail cDetail = new ChiropracticFollowUpDetail {
                                    A = tempDetail.A,
                                    Cold = tempDetail.Cold,
                                    Comments = tempDetail.Comments,
                                    ConditionChange = tempDetail.ConditionChange,
                                    ConditionChangeNote = tempDetail.ConditionChangeNote,
                                    CreatedBy = "auto",
                                    CreatedTime = DateTime.Now,
                                    Electrotherapy = tempDetail.Electrotherapy,
                                    ElectrotherapyNote = tempDetail.ElectrotherapyNote,
                                    ExerciseNote = tempDetail.ExerciseNote,
                                    FollowUpTreatment = ft,
                                    Heat = tempDetail.Heat,
                                    HomeCare = tempDetail.HomeCare,
                                    Massage = tempDetail.Massage,
                                    NewDiagnosis = tempDetail.NewDiagnosis,
                                    NewDiagnosisNote = tempDetail.NewDiagnosisNote,
                                    NoDxChange = tempDetail.NoDxChange,
                                    O = tempDetail.O,
                                    OsseousManipulation = tempDetail.OsseousManipulation,
                                    PRN = tempDetail.PRN,
                                    PTRDays = tempDetail.PTRDays,
                                    PTRMonths = tempDetail.PTRMonths,
                                    PTRWeeks = tempDetail.PTRWeeks,
                                    Referral = tempDetail.Referral,
                                    ReferralNote = tempDetail.ReferralNote,
                                    Stretch = tempDetail.Stretch,
                                    TheraputicExercise = tempDetail.TheraputicExercise,
                                    Traction = tempDetail.Traction,
                                    TriggerPointTherapy  = tempDetail.TriggerPointTherapy,
                                    UltraSound = tempDetail.UltraSound,
                                    UltraSoundValue = tempDetail.UltraSoundValue,
                                    VAS = tempDetail.VAS,
                                    ChiropracticSOAPDiagram = tempDetail.ChiropracticSOAPDiagram
                                };
                                session.Save(cDetail);

                                //Create points
                                IList<PointOnDiagram> points = (new JCService(session, Program.LogonUser)).GetChiropracticSOAPPoints(tempDetail, Program.config.MaxPointsPerDiagram);
                                foreach (PointOnDiagram p in points) {
                                    PointOnChiropracticFollowUp newP = new PointOnChiropracticFollowUp {
                                        ChiropracticFollowUpDetail = cDetail,
                                        CreatedBy = "auto",
                                        CreatedTime = DateTime.Now,
                                        Diagram = tempDetail.ChiropracticSOAPDiagram,
                                        PainKey = p.PainKey,
                                        X = p.X,
                                        Y = p.Y
                                    };
                                    session.Save(newP);
                                }
                            } else if (treatmentTypeName.Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE, StringComparison.CurrentCultureIgnoreCase)) {
                                MassageFollowUpDetail tempDetail = detailTemplates[treatmentTypeId] as MassageFollowUpDetail;
                                MassageFollowUpDetail mDetail = new MassageFollowUpDetail {
                                    CreatedBy = "auto",
                                    CreatedTime = DateTime.Now,
                                    FollowUpTreatment = ft,
                                    TreatmentAreaAbdominals = tempDetail.TreatmentAreaAbdominals,
                                    TreatmentAreaBack = tempDetail.TreatmentAreaBack,
                                    TreatmentAreaBreast = tempDetail.TreatmentAreaBreast,
                                    TreatmentAreaChest = tempDetail.TreatmentAreaChest,
                                    TreatmentAreaFace = tempDetail.TreatmentAreaFace,
                                    TreatmentAreaGluteus = tempDetail.TreatmentAreaGluteus,
                                    TreatmentAreaLeftArm = tempDetail.TreatmentAreaLeftArm,
                                    TreatmentAreaLeftLeg = tempDetail.TreatmentAreaLeftLeg,
                                    TreatmentAreaNeck = tempDetail.TreatmentAreaNeck,
                                    TreatmentAreaOther = tempDetail.TreatmentAreaOther,
                                    TreatmentAreaRightArm = tempDetail.TreatmentAreaRightArm,
                                    TreatmentAreaRightLeg = tempDetail.TreatmentAreaRightLeg,
                                    TreatmentAreaShoulders = tempDetail.TreatmentAreaShoulders,
                                    TreatmentNote = tempDetail.TreatmentNote,
                                    TreatmentUsedBreastMassage = tempDetail.TreatmentUsedBreastMassage,
                                    TreatmentUsedEffleurage = tempDetail.TreatmentUsedEffleurage,
                                    TreatmentUsedFacial = tempDetail.TreatmentUsedFacial,
                                    TreatmentUsedFriction = tempDetail.TreatmentUsedFriction,
                                    TreatmentUsedHighGradeJointMobilization = tempDetail.TreatmentUsedHighGradeJointMobilization,
                                    TreatmentUsedIntraOral = tempDetail.TreatmentUsedIntraOral,
                                    TreatmentUsedLowGradeJointMobilization = tempDetail.TreatmentUsedLowGradeJointMobilization,
                                    TreatmentUsedMyoFacialTriggerPoint = tempDetail.TreatmentUsedMyoFacialTriggerPoint,
                                    TreatmentUsedOther = tempDetail.TreatmentUsedOther,
                                    TreatmentUsedPetrissage = tempDetail.TreatmentUsedPetrissage,
                                    TreatmentUsedRocking = tempDetail.TreatmentUsedRocking,
                                    TreatmentUsedStretch = tempDetail.TreatmentUsedStretch,
                                    TreatmentUsedStroking = tempDetail.TreatmentUsedStroking,
                                    TreatmentUsedTapotement = tempDetail.TreatmentUsedTapotement,
                                    TreatmentUsedVibration = tempDetail.TreatmentUsedVibration
                                };
                                session.Save(mDetail);
                            } else if (treatmentTypeName.Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC, StringComparison.CurrentCultureIgnoreCase)) {
                                //no detail
                            } else if (treatmentTypeName.Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH, StringComparison.CurrentCultureIgnoreCase)) {
                                OsteopathyFollowUpDetail tempDetail = detailTemplates[treatmentTypeId] as OsteopathyFollowUpDetail;
                                OsteopathyFollowUpDetail oDetail = new OsteopathyFollowUpDetail {
                                    CreatedBy = "auto",
                                    CreatedTime = DateTime.Now,
                                    FollowUpTreatment = ft,
                                    TreatmentAreaAbdominals = tempDetail.TreatmentAreaAbdominals,
                                    TreatmentAreaBack = tempDetail.TreatmentAreaBack,
                                    TreatmentAreaBreast = tempDetail.TreatmentAreaBreast,
                                    TreatmentAreaChest = tempDetail.TreatmentAreaChest,
                                    TreatmentAreaFace = tempDetail.TreatmentAreaFace,
                                    TreatmentAreaGluteus = tempDetail.TreatmentAreaGluteus,
                                    TreatmentAreaLeftArm = tempDetail.TreatmentAreaLeftArm,
                                    TreatmentAreaLeftLeg = tempDetail.TreatmentAreaLeftLeg,
                                    TreatmentAreaNeck = tempDetail.TreatmentAreaNeck,
                                    TreatmentAreaOther = tempDetail.TreatmentAreaOther,
                                    TreatmentAreaRightArm = tempDetail.TreatmentAreaRightArm,
                                    TreatmentAreaRightLeg = tempDetail.TreatmentAreaRightLeg,
                                    TreatmentAreaShoulders = tempDetail.TreatmentAreaShoulders,
                                    TreatmentNote = tempDetail.TreatmentNote,
                                    TreatmentUsedCraniosacral = tempDetail.TreatmentUsedCraniosacral,
                                    TreatmentUsedFriction = tempDetail.TreatmentUsedFriction,
                                    TreatmentUsedIntraOral = tempDetail.TreatmentUsedIntraOral,
                                    TreatmentUsedMuscleEnergy = tempDetail.TreatmentUsedMuscleEnergy,
                                    TreatmentUsedMyofacialRelease= tempDetail.TreatmentUsedMyofacialRelease,
                                    TreatmentUsedOther = tempDetail.TreatmentUsedOther,
                                    TreatmentUsedPetrissage = tempDetail.TreatmentUsedPetrissage,
                                    TreatmentUsedRocking = tempDetail.TreatmentUsedRocking,
                                    TreatmentUsedSoftTissueJointMobilization = tempDetail.TreatmentUsedSoftTissueJointMobilization,
                                    TreatmentUsedStillTechnique = tempDetail.TreatmentUsedStillTechnique,
                                    TreatmentUsedStretch = tempDetail.TreatmentUsedStretch,
                                    TreatmentUsedTapotement = tempDetail.TreatmentUsedTapotement,
                                    TreatmentUsedTenderPointRelease = tempDetail.TreatmentUsedTenderPointRelease,
                                    TreatmentUsedTotalBodyAdjustment = tempDetail.TreatmentUsedTotalBodyAdjustment,
                                    TreatmentUsedVibration = tempDetail.TreatmentUsedVibration,
                                    TreatmentUsedVisceral = tempDetail.TreatmentUsedVisceral
                                };
                                session.Save(oDetail);
                            } else if (treatmentTypeName.Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY, StringComparison.CurrentCultureIgnoreCase)) {
                                //No detail
                            }

                            totalCreated++;
                        }
                        #endregion Go through all rows

                        tr.Commit();
                    }catch(Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, "Error: " + ex.Message);
                        Program.log.Error("Failed to auto create followups.", ex);
                    }

                }

                //Refresh list
                if(totalCreated > 0) reloadList();
            }

            MessageBox.Show(this, string.Format("{0} followups have been generated.", totalCreated));
        }
    }

    class CreateNewInitialTreatment {
        public int PatientId { get; set; }
        public int TreatmentTypeId { get; set; }
        public DateTime TreatmentDate { get; set; }
        public CreateNewInitialTreatment(int patientId, int treatmentTypeId, DateTime treatmentDate) {
            PatientId = patientId;
            TreatmentTypeId = treatmentTypeId;
            TreatmentDate = treatmentDate;
        }

        public override string ToString() {
            return "Create New Initial Treatment";
        }
    }

    class CreateNewFollowUp {
        public int PatientId { get; set; }
        public int TreatmentTypeId { get; set; }
        public DateTime TreatmentDate { get; set; }
        public CreateNewFollowUp(int patientId, int treatmentTypeId, DateTime treatmentDate) {
            PatientId = patientId;
            TreatmentTypeId = treatmentTypeId;
            TreatmentDate = treatmentDate;
        }

        public override string ToString() {
            return "Create New Follow Up";
        }
    }

}
