using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EHD.Model.Entity;
using EHD.Repository;
using NHibernate;
using System.IO;
using NHibernate.Transform;
using NHibernate.Criterion;

namespace EHD.Test {
    [TestClass]
    public class UnitTest1 {

        #region Private methods
        private void outputException(Exception ex) {
            Console.WriteLine("Exception: " + ex.Message);
            Console.WriteLine(ex.StackTrace);

            while(ex.InnerException != null) {
                Exception innerEx = ex.InnerException;
                Console.WriteLine("Inner Exception: " + innerEx.Message);
                Console.WriteLine(ex.StackTrace);
                ex = innerEx;
            }
        }

        private byte[] getImageBytes(string filePath) {
            using(FileStream stream = new FileStream(filePath, FileMode.Open))
            using(BinaryReader reader = new BinaryReader(stream)) {
                byte[] buff = new byte[stream.Length - 1];
                reader.Read(buff, 0, buff.Length);
                return buff;
            }
        }
        #endregion

        [TestMethod]
        public void QueryTherapyTypes() {
            Console.WriteLine("Start to test [Query Therapy Types]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var TherapyTypes = session.QueryOver<TherapyType>().List<TherapyType>();
                    Console.WriteLine("List all Therapy Types:");
                    foreach(TherapyType type in TherapyTypes) {
                        Console.WriteLine(type.TherapyTypeName);
                    }
                    Console.WriteLine("List all Therapy Types - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }

        }

        [TestMethod]
        public void AddTherapyType() {
            Console.WriteLine("Start to test [Add a new Therapy Type]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    TherapyType newType = new TherapyType();
                    newType.TherapyTypeName = "Physiotherapy";
                    newType.IsTestData = true;
                    newType.CreatedBy = "Leo";
                    newType.CreatedTime = DateTime.Now;

                    session.Save(newType);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("Therapy Type has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }

        }

        [TestMethod]
        public void AddUser() {
            Console.WriteLine("Start to test [Add a new User]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    //Get a therapy type
                    TherapyType therapyType = session.Get<TherapyType>(88);

                    User user = new User();
                    user.Username = "username";
                    user.Password = "password";
                    user.FirstName = "Leo";
                    user.LastName = "Xie";
                    //user.TherapistTypes.Add(therapyType);
                    user.IsTestData = true;
                    user.CreatedBy = "Leo";
                    user.CreatedTime = DateTime.Now;

                    session.Save(user);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("User has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }

        }

        [TestMethod]
        public void QueryUsers() {
            Console.WriteLine("Start to test [Query Users]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var users = session.QueryOver<User>().List<User>();
                    Console.WriteLine("List all Users:");
                    foreach(User user in users) {
                        Console.WriteLine(user.DisplayName);
                        /*
                        Console.Write("Therapy Types: ");
                        foreach(TherapyType type in user.TherapistTypes) {
                            Console.Write(type.DisplayName);
                            Console.Write(", ");
                        }*/
                        Console.WriteLine();
                    }
                    Console.WriteLine("List all Users - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void AddDiagram() {
            Console.WriteLine("Start to test [Add a new Diagram]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    Diagram diagram = new Diagram();
                    diagram.DiagramName = "PhysiotherapySpine";
                    diagram.Image = getImageBytes(@"C:\My Projects\EHD\images\normalspine.gif");
                    diagram.ImageType = "gif";
                    diagram.ImageWidth = 200;
                    diagram.ImageHeight = 300;
                    diagram.IsTestData = true;
                    diagram.CreatedBy = "Leo";
                    diagram.CreatedTime = DateTime.Now;

                    session.Save(diagram);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("Diagram has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void QueryDiagrams() {
            Console.WriteLine("Start to test [Query Diagrams]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var diagrams = session.QueryOver<Diagram>().List<Diagram>();
                    Console.WriteLine("List all Diagrams:");
                    foreach(Diagram diagram in diagrams) {
                        Console.WriteLine(diagram.DisplayName);
                    }
                    Console.WriteLine("List all Diagrams - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void AddPatient() {
            Console.WriteLine("Start to test [Add a new Patient]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    Address addr = new Address();
                    addr.AddressLine1 = "address line 1";
                    addr.AddressLine2 = "address line 2";
                    ContactInfo contact = new ContactInfo();
                    contact.Address = addr;
                    contact.HomePhone = "416-123-4567";
                    contact.CellPhone = "647-321-1232";
                    contact.HomeFax = "416-987-6543";
                    contact.Email = "abc@email.com";

                    Patient p = new Patient();
                    p.FirstName = "Patient 1";
                    p.MiddleName = "abc";
                    p.LastName = "x";
                    p.ContactInfo = contact;
                    p.IsTestData = true;
                    p.CreatedBy = "Leo";
                    p.CreatedTime = DateTime.Now;

                    session.Save(p);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("Patient has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void QueryPatients() {
            Console.WriteLine("Start to test [Query Patients]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var patients = session.QueryOver<Patient>().List<Patient>();
                    Console.WriteLine("List all Patients:");
                    foreach(Patient p in patients) {
                        Console.WriteLine(p.Id);
                        Console.WriteLine(p.DisplayName);
                        Console.WriteLine("Version:" + p.Version.ToString());
                    }
                    Console.WriteLine("List all Patients - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void UpdatePatent() {
            Console.WriteLine("Start to test [Update a Patient]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    //Retrieve the patient - 36
                    Patient p = session.Get<Patient>(36);
                    p.UpdatedBy = "Xie";
                    p.UpdatedTime = DateTime.Now;
                    p.LastName = "Updated again";

                    session.Save(p);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("Patient has been updated.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void QueryInitialTreatment() {
            Console.WriteLine("Start to test [Query InitialTreatments]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var iniTreatments = session.QueryOver<InitialTreatment>().List<InitialTreatment>();
                    Console.WriteLine("List all InitialTreatments:");
                    foreach(InitialTreatment t in iniTreatments) {
                        Console.WriteLine(t.DisplayName);
                        Console.WriteLine("Version:" + t.Version.ToString());
                    }
                    Console.WriteLine("List all InitialTreatments - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void AddInitialTreatmentTest() {
            Console.WriteLine("Start to test [Add a new InitialTreatment]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    InitialTreatment iniTreatment = new InitialTreatment();
                    iniTreatment.Patient = session.Get<Patient>(36);
                    iniTreatment.Therapist = session.Get<User>(26);
                    iniTreatment.TreatmentType = session.Get<TherapyType>(88);
                    iniTreatment.IsTestData = true;
                    iniTreatment.CreatedBy = "Leo";
                    iniTreatment.CreatedTime = DateTime.Now;

                    session.Save(iniTreatment);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("InitialTreatment has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void QueryFollowUpTreatment() {
            Console.WriteLine("Start to test [Query FollowUpTreatments]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var treatments = session.QueryOver<FollowUpTreatment>().List<FollowUpTreatment>();
                    Console.WriteLine("List all FollowUpTreatments:");
                    foreach(FollowUpTreatment ft in treatments) {
                        Console.WriteLine(ft.DisplayName);
                        Console.WriteLine("Version:" + ft.Version.ToString());
                    }
                    Console.WriteLine("List all FollowUpTreatments - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void AddFollowUpTreatment() {
            Console.WriteLine("Start to test [Add a new FollowUpTreatment]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    FollowUpTreatment treatment = new FollowUpTreatment();
                    treatment.InitialTreatment = session.Get<InitialTreatment>(58);
                    treatment.Therapist = session.Get<User>(26);
                    treatment.IsTestData = true;
                    treatment.CreatedBy = "Leo";
                    treatment.CreatedTime = DateTime.Now;

                    Console.WriteLine("about to save into db.");
                    session.Save(treatment);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("FollowUpTreatment has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void UpdateFollowUpTreatment() {
        }

        [TestMethod]
        public void QueryPointOnPhysiotherapy() {
            Console.WriteLine("Start to test [Query PointOnPhysiotherapies]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var points = session.QueryOver<PointOnPhysiotherapy>().List<PointOnPhysiotherapy>();
                    Console.WriteLine("List all PointOnPhysiotherapies:");
                    foreach(PointOnPhysiotherapy p in points) {
                        Console.WriteLine(p.DisplayName);
                        Console.WriteLine("Version:" + p.Version.ToString());
                    }
                    Console.WriteLine("List all PointOnPhysiotherapies - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void AddPointOnPhysiotherapy() {
            Console.WriteLine("Start to test [Add a new PointOnPhysiotherapy]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    PointOnPhysiotherapy point = new PointOnPhysiotherapy();
                    point.PhysiotherapyDetail = session.Get<PhysiotherapyDetail>(144);
                    point.Diagram = session.Get<Diagram>(32);
                    point.IsTestData = true;
                    point.CreatedBy = "Leo";
                    point.CreatedTime = DateTime.Now;

                    Console.WriteLine("about to save into db.");
                    session.Save(point);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("FollowUpTreatment has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void QueryPhysiotherapyDetail() {
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            Console.WriteLine("Start to test [Query PhysiotherapyDetails]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var details = session.QueryOver<PhysiotherapyDetail>().List<PhysiotherapyDetail>();
                    Console.WriteLine("List all PhysiotherapyDetails:");
                    foreach(PhysiotherapyDetail d in details) {
                        Console.WriteLine(d.DisplayName);
                        Console.WriteLine("Version:" + d.Version.ToString());
                    }
                    Console.WriteLine("List all PhysiotherapyDetails - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }


        [TestMethod]
        public void AddPhysiotherapyDetail() {
            Console.WriteLine("Start to test [Add a new PhysiotherapyDetail]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    PhysiotherapyDetail detail = new PhysiotherapyDetail();
                    detail.InitialTreatment = session.Get<InitialTreatment>(58);
                    detail.PainScaleDiagram = session.Get<Diagram>(32);
                    detail.PassiveAccessoryMovementsAndPalpationOfSpineDiagram = session.Get<Diagram>(140);
                    detail.IsTestData = true;
                    detail.CreatedBy = "Leo";
                    detail.CreatedTime = DateTime.Now;

                    Console.WriteLine("about to save into db.");
                    session.Save(detail);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("PhysiotherapyDetail has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void QueryChiropracticDetail() {
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            Console.WriteLine("Start to test [Query ChiropracticDetails]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {

                    var details = session.QueryOver<ChiropracticDetail>().List<ChiropracticDetail>();
                    Console.WriteLine("List all ChiropracticDetails:");
                    foreach(ChiropracticDetail d in details) {
                        Console.WriteLine(d.DisplayName);
                        Console.WriteLine("Version:" + d.Version.ToString());
                    }
                    Console.WriteLine("List all ChiropracticDetails - END");
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        [TestMethod]
        public void AddChiropracticDetail() {
            Console.WriteLine("Start to test [Add a new ChiropracticDetail]");
            try {
                ISessionFactory sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    ChiropracticDetail detail = new ChiropracticDetail();
                    detail.InitialTreatment = session.Get<InitialTreatment>(58);
                    detail.ChiropracticDiagram = session.Get<Diagram>(32);
                    detail.IsTestData = true;
                    detail.CreatedBy = "Leo";
                    detail.CreatedTime = DateTime.Now;

                    Console.WriteLine("about to save into db.");
                    session.Save(detail);
                    tr.Commit();
                }
                sessionFactory.Close();
                Console.WriteLine("ChiropracticDetail has been added.");
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }

        /*
        [TestMethod]
        public void TestGenericRepository() {
            Console.WriteLine("Start to test [GenericRespository]");
            try {
                ISessionFactory sessionFactory = SessionFactory.GetSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) {
                    GenericRepository<User> userRepository = new GenericRepository<User>(session);
                    IList<User> users = userRepository.GetAll();
                    Console.WriteLine("List all user below:");
                    foreach(User u in users) {
                        Console.WriteLine(u.ToString());
                    }
                }
                sessionFactory.Close();
            } catch(Exception ex) {
                outputException(ex);
            } finally {
            }
        }*/

        [TestMethod]
        public void TestRetrieveTree() {
            Console.WriteLine("Start to test [TestRetrieveTree]");

            //Activate the NHibernate Profiler
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession()) 
                using(ITransaction tr = session.BeginTransaction())
                {
                    // Try to find the best solution to select multiple level hierarchy children
                    // with no duplicates, fast and efficient

                    /* No good, with no duplicates but too many queries when lazy-loading all the children. SELEC N+1 problem
                    var patients = session.QueryOver<Patient>().List();

                    /* Combine tables into joins, so less queries with NO SELECT N+1 problem, BUT, the returned collection contains duplicates
                    var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Invoices).Eager
                        .Fetch(x => x.Invoices[0].InvoiceItems).Eager
                        .List();*/

                    /* A little better, the root collection has no duplicates now, but the second level, invoices, still have duplicates
                    var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Invoices).Eager
                        .Fetch(x => x.Invoices[0].InvoiceItems).Eager
                        .Fetch(x => x.Insurer).Eager
                        .TransformUsing(new DistinctRootEntityResultTransformer()) //this will remove the Patient duplicates
                        .List();*/

                    /* HQL version - still with duplicated Invoices
                    var patients = session.CreateCriteria(typeof(Patient))
                        .SetFetchMode("Invoices", FetchMode.Eager)
                        .SetFetchMode("Invoices.InvoiceItems", FetchMode.Eager)
                        .SetFetchMode("Insurer", FetchMode.Eager)
                        .SetResultTransformer(new DistinctRootEntityResultTransformer())
                        .List<Patient>();*/
                    
                    /* No good, SELECT N+1 problem
                    Patient aliasP = null;
                    Invoice aliasInv = null;
                    InvoiceItem aliasItem = null;
                    var patients = session.QueryOver<Patient>(() => aliasP).Future<Patient>();
                    var patientInvoices = session.QueryOver<Invoice>(() => aliasInv)
                        .JoinAlias(inv => inv.Patient, () => aliasP)
                        .Future<Invoice>();
                    var patientInvoiceItems = session.QueryOver<InvoiceItem>(() => aliasItem)
                        .JoinAlias(item => item.Invoice, () => aliasInv)
                        .Future<InvoiceItem>(); */

                    // Best solution for now
                    // Load all records in one join query, then remove duplicates on the client by using Distinct
                    /*var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Invoices).Eager
                        .Fetch(x => x.Invoices[0].InvoiceItems).Eager
                        .Fetch(x => x.Insurer).Eager
                        .List();*/

                    
                    var dc = DetachedCriteria.For<InvoiceItem>()
                        .SetProjection(Projections.Property("Invoice.Id"))
                        .Add(Restrictions.Ge("Amount", 0.0));

                    var patients = session.CreateCriteria<Patient>().SetFetchMode("Insurer", FetchMode.Eager)
                        .CreateCriteria("Invoices", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Subqueries.PropertyIn("Id", dc))
                        .CreateCriteria("InvoiceItems", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .List<Patient>();


                    foreach(Patient p in patients.Distinct<Patient>()) {
                        Console.WriteLine(p.DisplayName);
                        foreach(Invoice inv in p.Invoices.Distinct<Invoice>()) {
                            Console.WriteLine("    " + inv.DisplayName);
                            foreach(InvoiceItem item in inv.InvoiceItems.Distinct<InvoiceItem>()) {
                                Console.WriteLine("        " + item.DisplayName);
                            }
                        }
                    }

                    tr.Commit();
                }
            } catch(Exception ex) {
                outputException(ex);
            } finally {
                if(sessionFactory != null) sessionFactory.Close();
            }
        }

        /*
        [TestMethod]
        public void TestAddInvoiceItem() {
            Console.WriteLine("Start to test [TestAddInvoiceItem]");

            //Activate the NHibernate Profiler
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    Invoice inv = session.QueryOver<Invoice>().List().FirstOrDefault();
                    ServiceType service = session.QueryOver<ServiceType>().List().FirstOrDefault();

                    InvoiceItem item = new InvoiceItem();
                    item.CreatedBy = "LeoTest";
                    item.CreatedTime = DateTime.Now;
                    item.IsTestData = true;
                    item.Invoice = inv;
                    item.Service = service;
                    item.ServiceDate = DateTime.Now;
                    item.Amount = 20;

                    session.Save(item);

                    tr.Commit();
                }
            } catch(Exception ex) {
                outputException(ex);
                
            } finally {
                if(sessionFactory != null) sessionFactory.Close();
            }
        }

        [TestMethod]
        public void TestAddInvoice() {
            Console.WriteLine("Start to test [TestAddInvoice]");

            //Activate the NHibernate Profiler
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.InitiateSessionFactory("Server=localhost;Database=EHD;Uid=root;Pwd=Pa33w0rd;");
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    Patient p = session.QueryOver<Patient>().List().FirstOrDefault();
                    ServiceType service = session.QueryOver<ServiceType>().List().FirstOrDefault();
                    User therapist = session.QueryOver<User>()
                        .Where(u => u.UserType == UserType.Therapist)
                        .List()
                        .FirstOrDefault();
                    TherapistOrganizationRegistrationGroup regGrp = session.QueryOver<TherapistOrganizationRegistrationGroup>()
                        .Where(x => x.Therapist.Id == therapist.Id)
                        .List()
                        .FirstOrDefault();

                    Invoice inv = new Invoice();
                    inv.CreatedBy = "LeoTest";
                    inv.CreatedTime = DateTime.Now;
                    inv.IsTestData = true;
                    inv.Patient = p;
                    inv.Therapist = therapist;
                    inv.RegistrationGroup = regGrp;
                    inv.InvoiceNubmer = "1234fgr";
                    inv.HSTNumber = "23423";
                    inv.Title = "Title";
                    inv.Note = "Note";

                    session.Save(inv);

                    tr.Commit();
                }
            } catch(Exception ex) {
                outputException(ex);
            } finally {
                if(sessionFactory != null) sessionFactory.Close();
            }
        }*/
    }
}
