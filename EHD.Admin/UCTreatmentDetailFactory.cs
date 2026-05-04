using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using EHD.Constant;
using NHibernate;
using System.Windows.Forms;

namespace EHD.Admin {
    public static class UCTreatmentDetailFactory {
        public static UserControl CreateTreatmentDetailUserControl(
            InitialTreatment InitialTreatment, string TreatmentTypeName, bool ViewOnly) {
                UserControl uc = null;

            try {
                ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    //Create JCService
                    JCService service = new JCService(session, Program.LogonUser);

                    //Get TreatmentDetail
                    ITreatmentDetail detail = service.GetInitialTreatmentDetail(InitialTreatment.Id, TreatmentTypeName);

                    if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_PHYSIOTHERAPY.ToLower())) {
                        //Populate control
                        if(detail == null) {
                            PhysiotherapyDetail d = new PhysiotherapyDetail();
                            d.InitialTreatment = InitialTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = InitialTreatment.IsTestData;
                            d.PainScaleDiagram = session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_PAIN_SCALE)
                                .SingleOrDefault<Diagram>();
                            d.PassiveAccessoryMovementsAndPalpationOfSpineDiagram =
                                session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_SPINE)
                                .SingleOrDefault<Diagram>();

                            detail = d;
                        } else {
                            //((PhysiotherapyDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((PhysiotherapyDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCPhysiotherapyDetail(detail as PhysiotherapyDetail, ViewOnly);
                    } else if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.ToLower())) {
                        //Populate control
                        if(detail == null) {
                            ChiropracticDetail d = new ChiropracticDetail();
                            d.InitialTreatment = InitialTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = InitialTreatment.IsTestData;
                            d.ChiropracticDiagram = session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_CHIROPRACTIC)
                                .SingleOrDefault<Diagram>();
                            detail = d;
                        } else {
                            //((ChiropracticDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((ChiropracticDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCChiropracticDetail(detail as ChiropracticDetail, ViewOnly);

                    } else if (TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.ToLower())) {
                        //Populate control
                        if (detail == null) {
                            OsteopathyDetail d = new OsteopathyDetail();
                            d.InitialTreatment = InitialTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = InitialTreatment.IsTestData;
                            d.OsteopathyDiagram = session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_OSTEOPATHY)
                                .SingleOrDefault<Diagram>();
                            detail = d;
                        } else {
                        }

                        uc = new UCOsteopathyDetail(detail as OsteopathyDetail, ViewOnly);
                    } else if (TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.ToLower())) {
                        //Populate control
                        if(detail == null) {
                            AcupunctureDetail d = new AcupunctureDetail();
                            d.InitialTreatment = InitialTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = InitialTreatment.IsTestData;
                            d.TongueDiagram = session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_ACUPUNCTURE)
                                .SingleOrDefault<Diagram>();
                            detail = d;
                        } else {
                            //((AcupunctureDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((AcupunctureDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCAcupunctureDetail(detail as AcupunctureDetail, ViewOnly);
                    } else if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.ToLower())) {
                        //Populate control
                        if(detail == null) {
                            MassageDetail d = new MassageDetail();
                            d.InitialTreatment = InitialTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = InitialTreatment.IsTestData;
                            d.MassageDiagram = session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_MASSAGE)
                                .SingleOrDefault<Diagram>();
                            detail = d;
                        } else {
                            //((MassageDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((MassageDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCMassageDetail(detail as MassageDetail, ViewOnly);
                    } else if(TreatmentTypeName.Trim().ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_NATUROPATHIC.ToLower())) {
                        //Populate control
                        if(detail == null) {
                            NaturopathicDetail d = new NaturopathicDetail();
                            d.InitialTreatment = InitialTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = InitialTreatment.IsTestData;
                            detail = d;
                        } else {
                            //((NaturopathicDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((NaturopathicDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCNaturopathicDetail(detail as NaturopathicDetail, ViewOnly);
                    } else {
                        throw new ApplicationException("System doesn't support Therapy Type: [" + TreatmentTypeName + "]");
                    }

                    tr.Commit();
                }
            } catch(Exception ex) {
                throw new ApplicationException("Failed to retrieve Initial Treatment Details. " + ex.Message, ex);
            }

            return uc;
        }

        public static UserControl CreateSOAPUserControl(FollowUpTreatment FollowUpTreatment, bool ViewOnly) {
            UserControl uc = null;

            try {
                ISessionFactory sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    //Create JCService
                    JCService service = new JCService(session, Program.LogonUser);

                    //Get FollowUpTreatmentDetail
                    IFollowUpDetail detail = service.GetFollowUpTreatmentDetail(FollowUpTreatment);

                    string strTreatmentType = FollowUpTreatment.InitialTreatment.TreatmentType.TherapyTypeName.Trim();
                    if(strTreatmentType.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_CHIROPRACTIC.Trim().ToLower()))
                    {

                        //Populate control
                        if(detail == null) {
                            ChiropracticFollowUpDetail d = new ChiropracticFollowUpDetail();
                            d.FollowUpTreatment = FollowUpTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = FollowUpTreatment.IsTestData;
                            //Assign Diagram
                            d.ChiropracticSOAPDiagram = session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_CHIROPRACTIC_SOAP)
                                .SingleOrDefault<Diagram>();
                            detail = d;
                        } else {
                            //((ChiropracticFollowUpDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((ChiropracticFollowUpDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCChiropracticSOAP(detail as ChiropracticFollowUpDetail, ViewOnly);

                    } else if (strTreatmentType.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_OSTEOPATH.Trim().ToLower())) {

                        //Populate control
                        if (detail == null) {
                            OsteopathyFollowUpDetail d = new OsteopathyFollowUpDetail();
                            d.FollowUpTreatment = FollowUpTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = FollowUpTreatment.IsTestData;
                            detail = d;
                        } else {
                        }

                        uc = new UCOsteopathySOAP(detail as OsteopathyFollowUpDetail, ViewOnly);

                    } else if(strTreatmentType.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_MASSAGE.Trim().ToLower())) {

                        //Populate control
                        if(detail == null) {
                            MassageFollowUpDetail d = new MassageFollowUpDetail();
                            d.FollowUpTreatment = FollowUpTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = FollowUpTreatment.IsTestData;
                            detail = d;
                        } else {
                            //((MassageFollowUpDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((MassageFollowUpDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCMassageSOAP(detail as MassageFollowUpDetail, ViewOnly);
                    } else if (strTreatmentType.ToLower().Equals(Constants.DEFINED_TREATMENT_TYPE_ACUPUNCTURE.Trim().ToLower())) {
                        //Populate control
                        if (detail == null) {
                            AcupunctureFollowUpDetail d = new AcupunctureFollowUpDetail();
                            d.FollowUpTreatment = FollowUpTreatment;
                            d.CreatedBy = Program.LogonUser.DisplayName;
                            d.CreatedTime = DateTime.Now;
                            d.UpdatedBy = d.CreatedBy;
                            d.UpdatedTime = d.CreatedTime;
                            d.IsTestData = FollowUpTreatment.IsTestData;

                            d.TongueDiagram = session.QueryOver<Diagram>()
                                .Where(x => x.DiagramName == Constants.DEFINED_DIAGRAM_NAME_ACUPUNCTURE)
                                .SingleOrDefault<Diagram>();
                            detail = d;
                        } else {
                            //((AcupunctureFollowUpDetail)detail).UpdatedBy = Program.LogonUser.DisplayName;
                            //((AcupunctureFollowUpDetail)detail).UpdatedTime = DateTime.Now;
                        }

                        uc = new UCAcupunctureSOAP(detail as AcupunctureFollowUpDetail, ViewOnly);
                    }

                    tr.Commit();
                }
            } catch(Exception ex) {
                throw new ApplicationException("Failed to retrieve FollowUp Treatment Details. " + ex.Message, ex);
            }

            return uc;
        }
    }
}
