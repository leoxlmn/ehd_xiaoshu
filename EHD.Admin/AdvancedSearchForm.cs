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
using NHibernate.Transform;
using NHibernate.Criterion;
using EHD.Model.DTO;
using NHibernate.SqlCommand;


namespace EHD.Admin {

    public partial class AdvancedSearchForm : Form {

        private const string IMG_KEY_MALE = "Male";
        private const string IMG_KEY_FEMALE = "Female";
        private const string IMG_KEY_PATIENT = "Patient";
        private const string IMG_KEY_DOCTOR = "Doctor";
        private const string IMG_KEY_INIT_TREATMENT = "InitialTreatment";
        private const string IMG_KEY_FUP_TREATMENT = "FollowUpTreatment";

        private const int IMG_INDEX_MALE = 0;
        private const int IMG_INDEX_FEMALE = 1;
        private const int IMG_INDEX_PATIENT = 2;
        private const int IMG_INDEX_DOCTOR = 3;
        private const int IMG_INDEX_INIT_TREATMENT = 4;
        private const int IMG_INDEX_FUP_TREATMENT = 5;

        private readonly Color PATIENT_NODE_COLOR = Color.FromArgb(0, 30, 130);
        private readonly Font PATIENT_NODE_FONT = new Font("Courier New", 11, FontStyle.Bold);

        public AdvancedSearchForm() {
            InitializeComponent();
        }

        private void AdvancedSearchForm_Load(object sender, EventArgs e) {
            //Initiate pager
            pager.Page = 1;
            pager.GoToPageEvent += new GoToPageEventHandler(navigate);

            loadCheckedListBoxes();
            reloadList();
        }

        private void navigate(int page) {
            reloadList();
        }

        private void loadCheckedListBoxes() {

            #region Rewrote below
            /*
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    //Insurer
                    IList<Insurer> lstInsurer = session.QueryOver<Insurer>()
                        .OrderBy(x => x.InsurerName).Asc
                        .List<Insurer>();
                    foreach(Insurer insurer in lstInsurer) {
                        ucInsurer.AddItem(insurer, true);
                    }
                    ucInsurer.CheckCompleted += new EventHandler(filter_selected);


                    //Treatment
                    IList<TherapyType> lstTreatmentType = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .List<TherapyType>();
                    foreach(TherapyType tt in lstTreatmentType) {
                        ucTreatment.AddItem(tt, true);
                    }
                    ucTreatment.CheckCompleted += new EventHandler(filter_selected);

                    //Therapist
                    IList<User> lstTherapist = session.QueryOver<User>()
                        .Where(x => x.UserType == UserType.Therapist)
                        .OrderBy(x => x.FirstName).Asc
                        .OrderBy(x => x.LastName).Asc
                        .List<User>();
                    foreach(User usr in lstTherapist) {
                        if(usr.UserType == UserType.Therapist) {
                            ucTherapist.AddItem(usr, true);
                        }
                    }
                    ucTherapist.CheckCompleted += new EventHandler(filter_selected);

                    tr.Commit();
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load Filter lists. " + ex.Message);
                Program.log.Error("Failed to load Filter lists.", ex);
            }
            */
            #endregion Rewrote below

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
            pager.Page = 1;
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

        //Set other dropdownlist filter
        private IQueryOver<Patient, Patient> setFilter(UCCheckBoxDropDown ucDropDown, IQueryOver<Patient, Patient> query, IProjection projection) {

            if (!ucDropDown.AllChecked) {

                List<int> ids = new List<int>();
                foreach (object item in ucDropDown.CheckedItems) {
                    if (null == item) continue;
                    if (item is SimpleListItem) {
                        ids.Add((item as SimpleListItem).Id);
                    } else if (item is SimpleUserItem) {
                        ids.Add((item as SimpleUserItem).Id);
                    }
                }

                if (ucDropDown.AllNonBlanks) {
                    query = query.Where(Restrictions.IsNotNull(projection));
                } else if (ucDropDown.BlanksOnly) {
                    query = query.Where(Restrictions.IsNull(projection));
                } else if (!ucDropDown.IsBlankChecked) {
                    query = query.Where(Restrictions.In(projection, ids.ToArray()));
                } else {
                    query = query.Where(Expression.Disjunction()
                        .Add(Restrictions.IsNull(projection))
                        .Add(Restrictions.In(projection, ids.ToArray()))
                        );
                }
            }

            return query;
        }

        //Set Therapist filter
        private IQueryOver<Patient, Patient> setTherapistFilter(IQueryOver<Patient, Patient> query, IProjection ItTherapistId, IProjection FtTherapistId) {

            if (!ucTherapist.AllChecked) {
                if (ucTherapist.AllNonBlanks) {
                    query.Where(Expression.Disjunction()
                        .Add(Restrictions.IsNotNull(ItTherapistId))
                        .Add(Restrictions.IsNotNull(FtTherapistId))
                        );
                } else if (ucTherapist.BlanksOnly) {
                    query.Where(Expression.Disjunction()
                        .Add(Restrictions.IsNull(ItTherapistId))
                        .Add(Restrictions.IsNull(FtTherapistId))
                        );
                } else if (!ucTherapist.IsBlankChecked) {
                    query.Where(Expression.Disjunction()
                        .Add(Restrictions.In(ItTherapistId, selectedTherapists.Where(x => x != null).Select(x => x.Id).ToArray()))
                        .Add(Restrictions.In(FtTherapistId, selectedTherapists.Where(x => x != null).Select(x => x.Id).ToArray()))
                        );
                } else {
                    query.Where(Expression.Disjunction()
                        .Add(Restrictions.IsNull(ItTherapistId))
                        .Add(Restrictions.IsNull(FtTherapistId))
                        .Add(Restrictions.In(ItTherapistId, selectedTherapists.Where(x => x != null).Select(x => x.Id).ToArray()))
                        .Add(Restrictions.In(FtTherapistId, selectedTherapists.Where(x => x != null).Select(x => x.Id).ToArray()))
                        );
                }
            }

            return query;
        }

        //Set date range filter
        private IQueryOver<Patient, Patient> setDateRangeFilter(IQueryOver<Patient, Patient> query, IProjection dtItTime, IProjection dtFtTime) {

            if (dtFrom.Checked && dtTo.Checked) {
                query
                    .Where(Expression.Disjunction()
                        .Add(Expression.Conjunction()
                            .Add(Restrictions.Ge(dtItTime, dtFrom.Value.Date))
                            .Add(Restrictions.Lt(dtItTime, dtTo.Value.Date.AddDays(1)))
                        )
                        .Add(Expression.Conjunction()
                            .Add(Restrictions.Ge(dtFtTime, dtFrom.Value.Date))
                            .Add(Restrictions.Lt(dtFtTime, dtTo.Value.Date.AddDays(1)))
                        )
                    );
            } else if (dtFrom.Checked) {
                query
                    .Where(Expression.Disjunction()
                        .Add(Restrictions.Ge(dtItTime, dtFrom.Value.Date))
                        .Add(Restrictions.Ge(dtFtTime, dtFrom.Value.Date))
                    );
            } else if (dtTo.Checked) {
                query
                    .Where(Expression.Disjunction()
                        .Add(Restrictions.Lt(dtItTime, dtTo.Value.Date.AddDays(1)))
                        .Add(Restrictions.Lt(dtFtTime, dtTo.Value.Date.AddDays(1)))
                    );
            }

            return query;
        }

        private void reloadList() {

            //Start to update tree view
            trvList.BeginUpdate();
            trvList.Nodes.Clear();

            #region Rewrote below
            /*
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession()) 
                using(ITransaction tr = session.BeginTransaction()) {

                    //Apply filters for teatments
                    var dc = DetachedCriteria.For<InitialTreatment>("itr");
                    dc = setTreatmentTypeFilter(dc, "itr.TreatmentType");
                    dc.CreateCriteria("itr.FollowUpTreatments", "ftr", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
                    if (!ucTherapist.AllChecked) {
                        if (ucTherapist.AllNonBlanks) {
                            dc = dc.Add(Restrictions.Disjunction()
                                    .Add(Restrictions.IsNotNull("itr.Therapist"))
                                    .Add(Restrictions.IsNotNull("ftr.Therapist"))
                                );
                        } else if (ucTherapist.BlanksOnly) {
                            dc = dc.Add(Restrictions.Disjunction()
                                    .Add(Restrictions.IsNull("itr.Therapist"))
                                    .Add(Restrictions.IsNull("ftr.Therapist"))
                                );
                        } else if (!ucTherapist.IsBlankChecked) {
                            dc = dc.Add(Restrictions.Disjunction()
                                    .Add(Restrictions.In("itr.Therapist", selectedTherapists.ToArray<User>()))
                                    .Add(Restrictions.In("ftr.Therapist", selectedTherapists.ToArray<User>()))
                                );
                        } else {
                            dc = dc.Add(Expression.Disjunction()
                                    .Add(Restrictions.IsNull("itr.Therapist"))
                                    .Add(Restrictions.In("itr.Therapist", selectedTherapists.ToArray<User>()))
                                    .Add(Restrictions.IsNull("ftr.Therapist"))
                                    .Add(Restrictions.In("ftr.Therapist", selectedTherapists.ToArray<User>()))
                                );
                        }
                    }
                    if (dtFrom.Checked && dtTo.Checked) {
                        dc = dc
                            .Add(Expression.Disjunction()
                                .Add(Expression.Conjunction()
                                    .Add(Restrictions.Ge("itr.TreatmentTime", dtFrom.Value.Date))
                                    .Add(Restrictions.Lt("itr.TreatmentTime", dtTo.Value.Date.AddDays(1)))
                                )
                                .Add(Expression.Conjunction()
                                    .Add(Restrictions.Ge("ftr.TreatmentTime", dtFrom.Value.Date))
                                    .Add(Restrictions.Lt("ftr.TreatmentTime", dtTo.Value.Date.AddDays(1)))
                                )
                            );
                    } else if (dtFrom.Checked) {
                        dc = dc
                            .Add(Expression.Disjunction()
                                .Add(Restrictions.Ge("itr.TreatmentTime", dtFrom.Value.Date))
                                .Add(Restrictions.Ge("ftr.TreatmentTime", dtFrom.Value.Date))
                            );
                    } else if (dtTo.Checked) {
                        dc = dc
                            .Add(Expression.Disjunction()
                                .Add(Restrictions.Lt("itr.TreatmentTime", dtTo.Value.Date.AddDays(1)))
                                .Add(Restrictions.Lt("ftr.TreatmentTime", dtTo.Value.Date.AddDays(1)))
                            );
                    }
                    dc = dc.SetProjection(Projections.Distinct(Projections.Property("itr.Id")));


                    //Prepare query for patient ids
                    var qry = session.CreateCriteria<Patient>("p")
                        .CreateCriteria("p.Insurer", "ins", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

                    //Apply insurer filter
                    qry = setInsurerFilter(qry, "p.Insurer");

                    qry = qry
                        .CreateCriteria("p.InitialTreatments", "itr", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .CreateCriteria("itr.FollowUpTreatments", "ftr", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Subqueries.PropertyIn("itr.Id", dc));

                    //Select patient ids for paging
                    var patientIds = ((qry.Clone()) as ICriteria)
                        .AddOrder(new Order("p.LastName", true))
                        .AddOrder(new Order("p.FirstName", true))
                        .AddOrder(new Order("itr.TreatmentTime", true))
                        .SetProjection(Projections.Distinct(Projections.Id()))
                        .SetFirstResult((pager.Page - 1) * pager.PageSize)
                        .SetMaxResults(pager.PageSize)
                        .Future<int>();

                    //Get patient total count
                    int totalPatientCount = ((qry.Clone()) as ICriteria)
                        .SetProjection(Projections.CountDistinct("p.Id"))
                        .FutureValue<int>()
                        .Value;

                    //Set pager
                    if (totalPatientCount == 0) {
                        pager.TotalPage = 1;
                    } else {
                        pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalPatientCount - 1) / pager.PageSize)) + 1;
                    }

                    //Get patient and treatment info
                    //Start a batch query
                    var patients = qry
                        .Add(Restrictions.In("p.Id", patientIds.ToList<int>()))
                        .SetResultTransformer(Transformers.DistinctRootEntity)
                        .AddOrder(new Order("p.LastName", true))
                        .AddOrder(new Order("p.FirstName", true))
                        .AddOrder(new Order("itr.TreatmentTime", true))
                        .SetMaxResults(pager.PageSize * Program.config.MaxInitialTreatmentsPerPatient * Program.config.MaxFollowUpsPerTreatment)
                        .Future<Patient>();

                    //Force to load other collections within the same round trip in other queries
                    session.CreateCriteria<Insurer>().Future<Insurer>();
                    session.CreateCriteria<TherapyType>().Future<TherapyType>();
                    session.CreateCriteria<User>().SetFetchMode("Title", FetchMode.Eager).Future<User>();
                    session.CreateCriteria<TherapistOrganizationRegistrationGroup>().Future<TherapistOrganizationRegistrationGroup>();

                    #region Get exact number of treatments based on the search criteria

                    //prepare count query
                    var qryTreatmentCount = session.CreateCriteria<Patient>("p")
                        .CreateCriteria("p.Insurer", "ins", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
                    qryTreatmentCount = setInsurerFilter(qryTreatmentCount, "p.Insurer");

                    //prepare initial treatment count qry
                    var qryInitCount = ((qryTreatmentCount.Clone()) as ICriteria)
                        .CreateCriteria("p.InitialTreatments", "itr");
                    qryInitCount = setTreatmentTypeFilter(qryInitCount, "itr.TreatmentType");
                    qryInitCount = setTherapistFilter(qryInitCount, "itr.Therapist");
                    qryInitCount = setTimeRangeFilter(qryInitCount, "itr.TreatmentTime");
                    qryInitCount.SetProjection(Projections.Count(Projections.Id()));

                    //prepare follow up treatment count qry
                    var qryFollCount = ((qryTreatmentCount.Clone()) as ICriteria)
                        .CreateCriteria("p.InitialTreatments", "itr")
                        .CreateCriteria("itr.FollowUpTreatments", "ftr");
                    qryFollCount = setTreatmentTypeFilter(qryFollCount, "itr.TreatmentType");
                    qryFollCount = setTherapistFilter(qryFollCount, "ftr.Therapist");
                    qryFollCount = setTimeRangeFilter(qryFollCount, "ftr.TreatmentTime");
                    qryFollCount.SetProjection(Projections.Count(Projections.Id()));

                    var initCount = qryInitCount.FutureValue<int>();
                    int treatmentCount = qryFollCount.FutureValue<int>().Value + initCount.Value;
                    
                    #endregion Get exact number of treatments based on the search criteria

                    //Run the batch query
                    IList<Patient> lstPatient = patients.ToList<Patient>();

                    //Update total number
                    lblPatientNumber.Text = totalPatientCount.ToString();
                    lblTreatmentNumber.Text = treatmentCount.ToString();

                    foreach (Patient p in lstPatient) {
                        TreeNode pNode = new TreeNode(formatPatient(p));
                        pNode.ImageIndex = (p.Sex == Sex.Female ? IMG_INDEX_FEMALE : p.Sex == Sex.Male ? IMG_INDEX_MALE : IMG_INDEX_PATIENT);
                        pNode.SelectedImageIndex = pNode.ImageIndex;
                        pNode.Tag = p;
                        pNode.NodeFont = PATIENT_NODE_FONT;
                        pNode.ForeColor = PATIENT_NODE_COLOR;

                        //Load initial treatments
                        var lstInitTreatment = p.InitialTreatments.Distinct<InitialTreatment>();

                        foreach (InitialTreatment it in lstInitTreatment) {
                            TreeNode itNode = new TreeNode(formatInitialTreatment(it));
                            itNode.ImageIndex = IMG_INDEX_INIT_TREATMENT;
                            itNode.SelectedImageIndex = itNode.ImageIndex;
                            itNode.Tag = it;

                            //Load follow up treatments
                            var lstFollowUpTreatment = it.FollowUpTreatments
                                .Where(x => selectedTreatmentTypes.Contains(x.InitialTreatment.TreatmentType)
                                    && selectedTherapists.Contains(x.Therapist)
                                    && (!dtFrom.Checked || dtFrom.Value.Date <= x.TreatmentTime)
                                    && (!dtTo.Checked || dtTo.Value.Date.AddDays(1) > x.TreatmentTime)
                                );

                            foreach (FollowUpTreatment ft in lstFollowUpTreatment) {
                                TreeNode ftNode = new TreeNode(formatFollowUpTreatment(ft));
                                ftNode.ImageIndex = IMG_INDEX_FUP_TREATMENT;
                                ftNode.SelectedImageIndex = ftNode.ImageIndex;
                                ftNode.Tag = ft;

                                itNode.Nodes.Add(ftNode);
                            }

                            pNode.Nodes.Add(itNode);
                        }

                        trvList.Nodes.Add(pNode);
                    }
                    
                    
                    tr.Commit();
                        
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load search list. " + ex.Message);
                Program.log.Error("Failed to load search list.", ex);
            }
            */
            #endregion Rewrote below

            IList<PatientDTO> lstPatient = new List<PatientDTO>();

            //Create key for session access
            Guid key = Guid.NewGuid();
            ISession session = SessionFactory.GetOpenSession(key);

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    TreatmentSearchResultDTO rs = null;
                    Patient aPatient = null;
                    InitialTreatment aIT = null;
                    FollowUpTreatment aFT = null;
                    User aItTherapist = null;
                    User aFtTherapist = null;
                    Insurer aInsurer = null;
                    UserTitle aItTherapistTitle = null;
                    UserTitle aFtTherapistTitle = null;
                    TherapyType aType = null;

                    //Prepare search query for patient ids
                    var qryId = session.QueryOver<Patient>(() => aPatient);
                    qryId
                        .JoinQueryOver<InitialTreatment>(() => aPatient.InitialTreatments, () => aIT, JoinType.InnerJoin)
                        .JoinQueryOver<FollowUpTreatment>(() => aIT.FollowUpTreatments, () => aFT, JoinType.LeftOuterJoin);

                    //Add filters
                    //1) Insurer
                    if (!ucInsurer.AllChecked) {
                        qryId.JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin);
                        setFilter(ucInsurer, qryId, Projections.Property(() => aInsurer.Id));
                    }

                    //2) Treatment Type
                    setFilter(ucTreatment, qryId, Projections.Property(() => aIT.TreatmentType.Id));

                    //3) Therapist
                    setTherapistFilter(qryId, Projections.Property(() => aIT.Therapist.Id), Projections.Property(() => aFT.Therapist.Id));

                    //4 Date range
                    setDateRangeFilter(qryId, Projections.Property(() => aIT.TreatmentTime), Projections.Property(() => aFT.TreatmentTime));

                    //Select patient ids for paging
                    /*
                    var patientIds = (qryId.Clone())
                        .OrderBy(() => aPatient.FileNumber).Asc
                        .Select(Projections.Distinct(Projections.Property(() => aPatient.Id)))
                        .TransformUsing(Transformers.DistinctRootEntity)
                        .Skip((pager.Page - 1) * pager.PageSize)
                        .Take(pager.PageSize)
                        .Future<int>();*/
                    //Re-wrote with GROUP BY to fix the MySQL 3065 error.
                    var patientIds = (qryId.Clone())
                        .Select(Projections.Group<Patient>(p => p.Id))
                        .OrderBy(() => aPatient.FileNumber).Asc
                        //.Select(Projections.Distinct(Projections.Property(() => aPatient.Id)))
                        .TransformUsing(Transformers.DistinctRootEntity)
                        .Skip((pager.Page - 1) * pager.PageSize)
                        .Take(pager.PageSize)
                        .Future<int>();


                    //Get patient total count
                    int totalPatientCount = (qryId.Clone())
                        .Select(Projections.CountDistinct(() => aPatient.Id))
                        .FutureValue<int>()
                        .Value;

                    //Get treatment totla count
                    int totalTreatmentCount = (qryId.Clone())
                        .Select(Projections.CountDistinct(() => aIT.Id))
                        .FutureValue<int>()
                        .Value +
                        (qryId.Clone())
                        .Select(Projections.CountDistinct(() => aFT.Id))
                        .FutureValue<int>()
                        .Value;

                    //Get search result data
                    //1) Get Patients & Invoices first with filters
                    var qryData = session.QueryOver<Patient>(() => aPatient);
                    qryData
                        .JoinQueryOver<InitialTreatment>(() => aPatient.InitialTreatments, () => aIT, JoinType.InnerJoin)
                        .JoinQueryOver<FollowUpTreatment>(() => aIT.FollowUpTreatments, () => aFT, JoinType.LeftOuterJoin)
                        .JoinQueryOver<Insurer>(() => aPatient.Insurer, () => aInsurer, JoinType.LeftOuterJoin)
                        .JoinQueryOver<TherapyType>(() => aIT.TreatmentType, () => aType, JoinType.LeftOuterJoin)
                        .JoinQueryOver<User>(() => aIT.Therapist, () => aItTherapist, JoinType.LeftOuterJoin)
                        .JoinQueryOver<UserTitle>(() => aItTherapist.Title, () => aItTherapistTitle, JoinType.LeftOuterJoin)
                        .JoinQueryOver<User>(() => aFT.Therapist, () => aFtTherapist, JoinType.LeftOuterJoin)
                        .JoinQueryOver<UserTitle>(() => aFtTherapist.Title, () => aFtTherapistTitle, JoinType.LeftOuterJoin)
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

                                    .Add(Projections.Property(() => aIT.Id).WithAlias(() => rs.InitialTreatmentId))
                                    .Add(Projections.Property(() => aIT.Version).WithAlias(() => rs.InitialTreatmentVersion))
                                    .Add(Projections.Property(() => aType.TherapyTypeName).WithAlias(() => rs.TreatmentType))
                                    .Add(Projections.Property(() => aIT.TreatmentTime).WithAlias(() => rs.InitialTreatmentTime))
                                    .Add(Projections.Property(() => aIT.TreatmentDurationMinutes).WithAlias(() => rs.InitialDuration))
                                    .Add(Projections.Property(() => aItTherapist.FirstName).WithAlias(() => rs.InitialTherapistFirstName))
                                    .Add(Projections.Property(() => aItTherapist.LastName).WithAlias(() => rs.InitialTherapistLastName))
                                    .Add(Projections.Property(() => aItTherapist.MiddleName).WithAlias(() => rs.InitialTherapistMiddleName))
                                    .Add(Projections.Property(() => aItTherapistTitle.Name).WithAlias(() => rs.InitialTherapistTitle))

                                    .Add(Projections.Property(() => aFT.Id).WithAlias(() => rs.FollowupTreatmentId))
                                    .Add(Projections.Property(() => aFT.Version).WithAlias(() => rs.FollowupTreatmentVersion))
                                    .Add(Projections.Property(() => aFT.TreatmentTime).WithAlias(() => rs.FollowupTreatmentTime))
                                    .Add(Projections.Property(() => aFT.TreatmentDurationMinutes).WithAlias(() => rs.FollowupDuration))
                                    .Add(Projections.Property(() => aFtTherapist.FirstName).WithAlias(() => rs.FollowupTherapistFirstName))
                                    .Add(Projections.Property(() => aFtTherapist.LastName).WithAlias(() => rs.FollowupTherapistLastName))
                                    .Add(Projections.Property(() => aFtTherapist.MiddleName).WithAlias(() => rs.FollowupTherapistMiddleName))
                                    .Add(Projections.Property(() => aFtTherapistTitle.Name).WithAlias(() => rs.FollowupTherapistTitle))
                            )
                        );

                    //Add filters
                    //setFilter(ucInsurer, qryData, Projections.Property(() => aInsurer.Id)); //Not necessary as it has been applied in qryId
                    setFilter(ucTreatment, qryData, Projections.Property(() => aIT.TreatmentType.Id));
                    setTherapistFilter(qryData, Projections.Property(() => aIT.Therapist.Id), Projections.Property(() => aFT.Therapist.Id));
                    setDateRangeFilter(qryData, Projections.Property(() => aIT.TreatmentTime), Projections.Property(() => aFT.TreatmentTime));

                    var searchData = qryData
                        .OrderBy(() => aPatient.FileNumber).Asc
                        .ThenBy(() => aIT.TreatmentTime).Asc
                        .ThenBy(() => aIT.Id).Asc
                        .ThenBy(() => aFT.TreatmentTime).Asc
                        .ThenBy(() => aFT.Id).Asc
                        .TransformUsing(Transformers.AliasToBean<TreatmentSearchResultDTO>())
                        .List<TreatmentSearchResultDTO>();

                    #region Populate lstPatient with treatments
                    //Populate lstPatient
                    int lastPatientId = 0;
                    int lastItId = 0;
                    PatientDTO patient = null;
                    InitialTreatmentDTO it = null;
                    FollowupTreatmentDTO ft = null;

                    foreach (TreatmentSearchResultDTO row in searchData) {

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
                                InitialTreatments = new List<InitialTreatmentDTO>()
                            };
                            lstPatient.Add(patient);
                            lastPatientId = patient.Id;
                        }

                        //Add an InitialTreatmentDTO
                        if (row.InitialTreatmentId.HasValue && row.InitialTreatmentId.Value != lastItId) {
                            it = new InitialTreatmentDTO {
                                Id = row.InitialTreatmentId.Value,
                                Version = row.InitialTreatmentVersion.Value,
                                PatientId = row.PatientId,
                                TherapistFirstName = row.InitialTherapistFirstName,
                                TherapistLastName = row.InitialTherapistLastName,
                                TherapistMiddleName = row.InitialTherapistMiddleName,
                                TherapistTitle = row.InitialTherapistTitle,
                                TreatmentType = row.TreatmentType,
                                TreatmentTime = row.InitialTreatmentTime.Value,
                                Duration = row.InitialDuration,
                                Followups = new List<FollowupTreatmentDTO>()
                            };
                            patient.InitialTreatments.Add(it);
                            lastItId = it.Id;
                        }

                        //Add a FollowupTreatmentDTO
                        if (row.FollowupTreatmentId.HasValue) {
                            ft = new FollowupTreatmentDTO {
                                Id = row.FollowupTreatmentId.Value,
                                Version = row.FollowupTreatmentVersion.Value,
                                InitialTreatmentId = row.InitialTreatmentId.Value,
                                TherapistFirstName = row.FollowupTherapistFirstName,
                                TherapistLastName = row.FollowupTherapistLastName,
                                TherapistMiddleName = row.FollowupTherapistMiddleName,
                                TherapistTitle = row.FollowupTherapistTitle,
                                TreatmentTime = row.FollowupTreatmentTime.Value,
                                Duration = row.FollowupDuration.Value
                            };
                            it.Followups.Add(ft);
                        }
                    }
                    #endregion Populate lstPatient with treatments


                    //Update total number
                    lblPatientNumber.Text = totalPatientCount.ToString();
                    lblTreatmentNumber.Text = totalTreatmentCount.ToString();

                    //Set pager
                    if (totalPatientCount == 0) {
                        pager.TotalPage = 1;
                    } else {
                        pager.TotalPage = Convert.ToInt32(Math.Floor((double)(totalPatientCount - 1) / pager.PageSize)) + 1;
                    }

                    tr.Commit();

                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to retrieve patients with treatments. " + ex.Message);
                Program.log.Error("Failed to retrieve patients with treatments.", ex);
            } finally {
                SessionFactory.TryCloseSession(key);
            }

            foreach (PatientDTO p in lstPatient) {
                TreeNode pNode = new TreeNode(formatPatient(p));
                pNode.ImageIndex = (p.Sex == Sex.Female ? IMG_INDEX_FEMALE : p.Sex == Sex.Male ? IMG_INDEX_MALE : IMG_INDEX_PATIENT);
                pNode.SelectedImageIndex = pNode.ImageIndex;
                pNode.Tag = p;
                pNode.NodeFont = PATIENT_NODE_FONT;
                pNode.ForeColor = PATIENT_NODE_COLOR;

                //Load initial treatments
                foreach (InitialTreatmentDTO it in p.InitialTreatments) {
                    TreeNode itNode = new TreeNode(formatInitialTreatment(it));
                    itNode.ImageIndex = IMG_INDEX_INIT_TREATMENT;
                    itNode.SelectedImageIndex = itNode.ImageIndex;
                    itNode.Tag = it;

                    foreach (FollowupTreatmentDTO ft in it.Followups) {
                        TreeNode ftNode = new TreeNode(formatFollowUpTreatment(ft));
                        ftNode.ImageIndex = IMG_INDEX_FUP_TREATMENT;
                        ftNode.SelectedImageIndex = ftNode.ImageIndex;
                        ftNode.Tag = ft;

                        itNode.Nodes.Add(ftNode);
                    }

                    pNode.Nodes.Add(itNode);
                }

                trvList.Nodes.Add(pNode);
            }

            trvList.EndUpdate();

            expandCollapseTree();
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
            reloadList();
        }

        private void dtTo_ValueChanged(object sender, EventArgs e) {
            reloadList();
        }

        private string formatPatient(PatientDTO p) {
            return string.Format("{0} - {1} - [Insurer:{2}]",
                p.FileNumber,
                p.DisplayName.PadRight(20, ' '),
                p.Insurer
                );
        }

        private string formatInitialTreatment(InitialTreatmentDTO it) {
            return string.Format("{0}[{1}] - {2} - {3}mins", 
                it.TreatmentType.PadRight(20, ' '),
                it.TherapistDisplayName.PadRight(20, ' '),
                it.TreatmentTime.ToString("ddd, MMM dd, yyyy HH:mm"),
                it.Duration.ToString().PadLeft(3, ' ')
                );
        }

        private string formatFollowUpTreatment(FollowupTreatmentDTO ft) {
            return string.Format("[{0}] - {1} - {2}mins",
                ft.TherapistDisplayName.PadRight(20, ' '),
                ft.TreatmentTime.ToString("ddd, MMM dd, yyyy HH:mm"),
                ft.Duration.ToString().PadLeft(3, ' ')
                );
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

        private void trvList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

            if(trvList.SelectedNode == null) return;

            object nodeData = trvList.SelectedNode.Tag;

            if(nodeData is PatientDTO) {
                EditPatientForm patientForm = new EditPatientForm((nodeData as PatientDTO).Id, true);
                patientForm.ShowDialog(this);
            } else if(nodeData is InitialTreatmentDTO) {
                EditInitialTreatmentForm itForm = new EditInitialTreatmentForm(0, (nodeData as InitialTreatmentDTO).Id, true);
                itForm.Show(this);
            } else if(nodeData is FollowupTreatmentDTO) {
                EditFollowUpTreatmentForm ftForm = new EditFollowUpTreatmentForm(0, (nodeData as FollowupTreatmentDTO).Id, true);
                ftForm.Show(this);
            }
        }

        private void trvList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if(e.Button == System.Windows.Forms.MouseButtons.Right) {
                trvList.SelectedNode = e.Node;
                menu.Show(sender as Control, e.Location);
            }
        }

        private void chkHideEmptyPatient_CheckedChanged(object sender, EventArgs e) {
            reloadList();
        }
    }
}
