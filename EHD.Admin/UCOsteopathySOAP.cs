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
    public partial class UCOsteopathySOAP : UCTreatmentDetailBase, IFollowUpDetailUserControl {

        private bool _viewOnly = false;

        public OsteopathyFollowUpDetail EditingOsteopathySOAP { get; set; }

        public IFollowUpDetail FollowUpDetail {
            get {
                return EditingOsteopathySOAP;
            }
        }

        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingOsteopathySOAP.UpdatedBy = Program.LogonUser.DisplayName;
            EditingOsteopathySOAP.UpdatedTime = DateTime.Now;

            OsteopathyFollowUpDetail detailToSave = session.Merge(EditingOsteopathySOAP);
            updateOsteopathySOAPInput(detailToSave);
            session.Save(detailToSave);
        }

        public void LoadFromTemplate(IFollowUpDetail templateDetail) {
            OsteopathyFollowUpDetail detail = templateDetail as OsteopathyFollowUpDetail;
            populateOsteopathySOAPDetail(detail);
        }

        //Update detail with user input
        private void updateOsteopathySOAPInput(OsteopathyFollowUpDetail detail) {
            detail.TreatmentUsedRocking = chkRocking.Checked;
            detail.TreatmentUsedPetrissage = chkPetrissage.Checked;
            detail.TreatmentUsedFriction = chkFriction.Checked;
            detail.TreatmentUsedVibration = chkVibration.Checked;
            detail.TreatmentUsedTapotement = chkTapotement.Checked;
            detail.TreatmentUsedMyofacialRelease = chkMyofacialRelease.Checked;
            detail.TreatmentUsedTenderPointRelease = chkTenderPointRelease.Checked;
            detail.TreatmentUsedMuscleEnergy = chkMuscleEnergy.Checked;
            detail.TreatmentUsedTotalBodyAdjustment = chkTotalBodyAdjustment.Checked;
            detail.TreatmentUsedStillTechnique = chkStillTechnique.Checked;
            detail.TreatmentUsedCraniosacral = chkCraniosacral.Checked;
            detail.TreatmentUsedVisceral = chkVisceral.Checked;
            detail.TreatmentUsedSoftTissueJointMobilization = chkSoftTissueJointMobilization.Checked;
            detail.TreatmentUsedStretch = chkStretch.Checked;
            detail.TreatmentUsedIntraOral = chkIntraOral.Checked;
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

        public UCOsteopathySOAP(OsteopathyFollowUpDetail OsteopathySOAPDetail, bool ViewOnly) {
            if(OsteopathySOAPDetail == null)
                throw new ArgumentException("OsteopathySOAPDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingOsteopathySOAP = OsteopathySOAPDetail;
        }

        private void UCOsteopathySOAP_Load(object sender, EventArgs e) {
            loadDetail();
        }

        //Populate form
        protected override void loadDetail() {
            populateOsteopathySOAPDetail(EditingOsteopathySOAP);
        }

        private void populateOsteopathySOAPDetail(OsteopathyFollowUpDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession()) {
                    if (detail != null) {

                        chkRocking.Checked = detail.TreatmentUsedRocking;
                        chkPetrissage.Checked = detail.TreatmentUsedPetrissage;
                        chkFriction.Checked = detail.TreatmentUsedFriction;
                        chkVibration.Checked = detail.TreatmentUsedVibration;
                        chkTapotement.Checked = detail.TreatmentUsedTapotement;
                        chkMyofacialRelease.Checked = detail.TreatmentUsedMyofacialRelease;
                        chkTenderPointRelease.Checked = detail.TreatmentUsedTenderPointRelease;
                        chkMuscleEnergy.Checked = detail.TreatmentUsedMuscleEnergy;
                        chkTotalBodyAdjustment.Checked = detail.TreatmentUsedTotalBodyAdjustment;
                        chkStillTechnique.Checked = detail.TreatmentUsedStillTechnique;
                        chkCraniosacral.Checked = detail.TreatmentUsedCraniosacral;
                        chkVisceral.Checked = detail.TreatmentUsedVisceral;
                        chkSoftTissueJointMobilization.Checked = detail.TreatmentUsedSoftTissueJointMobilization;
                        chkStretch.Checked = detail.TreatmentUsedStretch;
                        chkIntraOral.Checked = detail.TreatmentUsedIntraOral;
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
                MessageBox.Show(this, "Failed to load Osteopathy SOAP Details. " + ex.Message);
                Program.log.Error("Failed to load Osteopathy SOAP Details.", ex);
            }
        }
    }
}
