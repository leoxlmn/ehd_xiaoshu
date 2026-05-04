using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using EHD.Constant;
using NHibernate;
using System.IO;
using System.Reflection;

namespace EHD.Admin {
    public partial class UCMassageSOAP : UCTreatmentDetailBase, IFollowUpDetailUserControl {

        private bool _viewOnly = false;

        public MassageFollowUpDetail EditingMassageSOAP { get; set; }

        public IFollowUpDetail FollowUpDetail {
            get {
                return EditingMassageSOAP;
            }
        }

        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingMassageSOAP.UpdatedBy = Program.LogonUser.DisplayName;
            EditingMassageSOAP.UpdatedTime = DateTime.Now;

            MassageFollowUpDetail detailToSave = session.Merge(EditingMassageSOAP);
            updateMassageSOAPInput(detailToSave);
            session.Save(detailToSave);
        }

        public void LoadFromTemplate(IFollowUpDetail templateDetail) {
            MassageFollowUpDetail detail = templateDetail as MassageFollowUpDetail;
            populateMassageSOAPDetail(detail);
        }

        //Update detail with user input
        private void updateMassageSOAPInput(MassageFollowUpDetail detail) {
            detail.TreatmentUsedStroking = chkStroking.Checked;
            detail.TreatmentUsedRocking = chkRocking.Checked;
            detail.TreatmentUsedEffleurage = chkEffleurage.Checked;
            detail.TreatmentUsedPetrissage = chkPetrissage.Checked;
            detail.TreatmentUsedFriction = chkFriction.Checked;
            detail.TreatmentUsedVibration = chkVibration.Checked;
            detail.TreatmentUsedTapotement = chkTapotement.Checked;
            detail.TreatmentUsedFacial = chkFacial.Checked;
            detail.TreatmentUsedMyoFacialTriggerPoint = chkMyoFacialTiggerPoint.Checked;
            detail.TreatmentUsedHighGradeJointMobilization = chkHighGradeJointMobilization.Checked;
            detail.TreatmentUsedLowGradeJointMobilization = chkLowGradeJointMobilization.Checked;
            detail.TreatmentUsedStretch = chkStretch.Checked;
            detail.TreatmentUsedIntraOral = chkIntraOral.Checked;
            detail.TreatmentUsedBreastMassage = chkBreastMassage.Checked;
            detail.TreatmentUsedOther = txtTechniqueUsedOther.Text;

            detail.TreatmentNote = txtNote.Text;

            detail.TreatmentAreaBack = chkBack.Checked;
            detail.TreatmentAreaNeck = chkNeck.Checked;
            detail.TreatmentAreaShoulders = chkShoulders.Checked;
            detail.TreatmentAreaFace = chkFace.Checked;
            detail.TreatmentAreaLeftArm = chkLeftArm.Checked;
            detail.TreatmentAreaRightArm = chkRightArm.Checked;
            detail.TreatmentAreaLeftLeg = chkLeftLeg.Checked;
            detail.TreatmentAreaRightLeg = chkRightLeg.Checked;
            detail.TreatmentAreaGluteus = chkGluteus.Checked;
            detail.TreatmentAreaAbdominals = chkAbdominals.Checked;
            detail.TreatmentAreaChest = chkChest.Checked;
            detail.TreatmentAreaBreast = chkBreast.Checked;
            detail.TreatmentAreaOther = txtAreaTreatedOther.Text;
        }

        public UCMassageSOAP(MassageFollowUpDetail MassageSOAPDetail, bool ViewOnly) {
            if(MassageSOAPDetail == null)
                throw new ArgumentException("MassageSOAPDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingMassageSOAP = MassageSOAPDetail;
        }

        private void UCMassageSOAP_Load(object sender, EventArgs e) {
            loadDetail();
        }

        //Populate form
        protected override void loadDetail() {
            populateMassageSOAPDetail(EditingMassageSOAP);
        }

        private void populateMassageSOAPDetail(MassageFollowUpDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession()) {
                    if (detail != null) {
                        chkStroking.Checked = detail.TreatmentUsedStroking;
                        chkRocking.Checked = detail.TreatmentUsedRocking;
                        chkEffleurage.Checked = detail.TreatmentUsedEffleurage;
                        chkPetrissage.Checked = detail.TreatmentUsedPetrissage;
                        chkFriction.Checked = detail.TreatmentUsedFriction;
                        chkVibration.Checked = detail.TreatmentUsedVibration;
                        chkTapotement.Checked = detail.TreatmentUsedTapotement;
                        chkFacial.Checked = detail.TreatmentUsedFacial;
                        chkMyoFacialTiggerPoint.Checked = detail.TreatmentUsedMyoFacialTriggerPoint;
                        chkHighGradeJointMobilization.Checked = detail.TreatmentUsedHighGradeJointMobilization;
                        chkLowGradeJointMobilization.Checked = detail.TreatmentUsedLowGradeJointMobilization;
                        chkStretch.Checked = detail.TreatmentUsedStretch;
                        chkIntraOral.Checked = detail.TreatmentUsedIntraOral;
                        chkBreastMassage.Checked = detail.TreatmentUsedBreastMassage;
                        txtTechniqueUsedOther.Text = detail.TreatmentUsedOther;

                        txtNote.Text = detail.TreatmentNote;

                        chkBack.Checked = detail.TreatmentAreaBack;
                        chkNeck.Checked = detail.TreatmentAreaNeck;
                        chkShoulders.Checked = detail.TreatmentAreaShoulders;
                        chkFace.Checked = detail.TreatmentAreaFace;
                        chkLeftArm.Checked = detail.TreatmentAreaLeftArm;
                        chkRightArm.Checked = detail.TreatmentAreaRightArm;
                        chkLeftLeg.Checked = detail.TreatmentAreaLeftLeg;
                        chkRightLeg.Checked = detail.TreatmentAreaRightLeg;
                        chkGluteus.Checked = detail.TreatmentAreaGluteus;
                        chkAbdominals.Checked = detail.TreatmentAreaAbdominals;
                        chkChest.Checked = detail.TreatmentAreaChest;
                        chkBreast.Checked = detail.TreatmentAreaBreast;
                        txtAreaTreatedOther.Text = detail.TreatmentAreaOther;
                    }
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load Massage SOAP Details. " + ex.Message);
                Program.log.Error("Failed to load Massage SOAP Details.", ex);
            }
        }
    }
}
