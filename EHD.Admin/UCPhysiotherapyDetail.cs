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
    public partial class UCPhysiotherapyDetail : UCTreatmentDetailBase, ITreatmentDetailUserControl {

        private bool _viewOnly = false;

        public PhysiotherapyDetail EditingPhysiotherapyDetail { get; set; }

        public ITreatmentDetail TreatmentDetail {
            get {
                return EditingPhysiotherapyDetail;
            }
        }

        public PictureBox PainDiagramPictureBox {
            get {
                return this.picPainScale;
            }
        }

        public PictureBox SpineDiagramPictureBox {
            get {
                return this.picSpine;
            }
        }

        public UCPhysiotherapyDetail(PhysiotherapyDetail PhysiotherapyDetail, bool ViewOnly) {
            if (PhysiotherapyDetail == null)
                throw new ArgumentException("PhysiotherapyDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingPhysiotherapyDetail = PhysiotherapyDetail;
        }

        //Populate form
        protected override void loadDetail() {
            populatePhysiotherapyDetail(EditingPhysiotherapyDetail);
        }

        private void populatePhysiotherapyDetail(PhysiotherapyDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    if (detail != null) {

                        numPainScaleNow.Value = detail.PainScaleNow;
                        numPainScaleBest.Value = detail.PainScaleBest;
                        numPainScaleWorst.Value = detail.PainScaleWorst;

                        txtDescOfSymp.Text = detail.DescriptionOfSymptoms;

                        rdoSymptomTrendIncreasing.Checked = (detail.SymptomTrend == Trend.Increasing);
                        rdoSymptomTrendStatic.Checked = (detail.SymptomTrend == Trend.Static);
                        rdoSymptomTrendDecreasing.Checked = (detail.SymptomTrend == Trend.Decreasing);

                        txtConstantVsIntermittent.Text = detail.ConstantVsIntermittent;

                        txtDayChanges.Text = detail.DayChanges;

                        rdoIrritabilityLow.Checked = (detail.IrritabilityLevel == IrritabilityLevels.Low);
                        rdoIrritabilityModerate.Checked = (detail.IrritabilityLevel == IrritabilityLevels.Moderate);
                        rdoIrritabilityHigh.Checked = (detail.IrritabilityLevel == IrritabilityLevels.High);

                        txtAggravatedBy.Text = detail.AggravatedBy;

                        txtEasedBy.Text = detail.EasedBy;

                        txtOccupation.Text = detail.Occupation;

                        txtWorkStatus.Text = detail.WorkStatus;

                        txtGoal.Text = detail.Goals;

                        txtRedFlag.Text = detail.RedFlagsAndPrecautions;

                        txtHistoryOfPresentingComplaint.Text = detail.HistoryOfPresentingComplaint;

                        txtJointSoundsAbnormalities.Text = detail.JointSoundsAndAbnormalities;

                        txtPastRelevantHistory.Text = detail.PastRelevantHistoryAndTreatment;

                        txtInvestigations.Text = detail.Investigations;

                        txtMedications.Text = detail.Medications;

                        txtGeneralMedicalHistory.Text = detail.GeneralMedicalHistoryAndMedications;

                        txtSocialHistory.Text = detail.SocialHistoryAndRecreationalActivities;

                        txtObservation.Text = detail.ObservationPostureGait;

                        txtActiveRangeOfMotion.Text = detail.ActiveRangeOfMotion;

                        txtMuscleStrength.Text = detail.MuscleStrengthAndMyotomes;

                        txtPalpation.Text = detail.Palpation;

                        txtNeurological.Text = detail.Neurological;

                        txtReflexes.Text = detail.Reflexes;

                        txtSensation.Text = detail.Sensation;

                        txtPassiveRangeOfMotion.Text = detail.PassiveRangeOfMotion;

                        txtStabilityTests.Text = detail.StabilityTestsAndSpecialTests;

                        txtAnalysis.Text = detail.Analysis;

                        txtTreatmentPlains.Text = detail.TreatmentPlan;

                        chkConsentForTreatmentVerbal.Checked = (detail.ConsentForTreatment == ConsentForTreaments.Verbal);

                        //When loading details from template, remove all existing points and add new points from template
                        if (detail.Id != EditingPhysiotherapyDetail.Id) {
                            _pointsToRemove.Clear();
                            _pointsToAdd.Clear();
                        }

                        if (detail.PainScaleDiagram != null) {
                            Diagram dg = session.Merge<Diagram>(detail.PainScaleDiagram);
                            MemoryStream stream = new MemoryStream(dg.Image);
                            Image img = Image.FromStream(stream);
                            Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                            using (Graphics g = Graphics.FromImage(imgResized)) {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                            }

                            stream.Close();

                            picPainScale.Image = imgResized;
                            picPainScale.Tag = dg;

                            //Get points from DB
                            IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetPhysiotherapyPoints(detail, dg, Program.config.MaxPointsPerDiagram);

                            //When loading details from template, remove all existing points and add new points from template
                            if (detail.Id != EditingPhysiotherapyDetail.Id) {

                                picPainScale.Controls.Clear();

                                Diagram dgPainScale = session.Merge<Diagram>(EditingPhysiotherapyDetail.PainScaleDiagram);
                                IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetPhysiotherapyPoints(EditingPhysiotherapyDetail, dgPainScale, Program.config.MaxPointsPerDiagram);

                                foreach (PointOnDiagram p in existingPoints) {
                                    string pointKey = string.Format("{0}_{1}_{2}",
                                        p.Diagram.Id,
                                        p.X,
                                        p.Y);
                                    _pointsToRemove.Add(pointKey, p);
                                }

                                IList<PointOnDiagram> newPoints = new List<PointOnDiagram>();

                                foreach (PointOnDiagram p in painPoints) {
                                    string pointKey = string.Format("{0}_{1}_{2}",
                                        p.Diagram.Id,
                                        p.X,
                                        p.Y);
                                    PointOnPhysiotherapy newP = new PointOnPhysiotherapy {
                                        PhysiotherapyDetail = EditingPhysiotherapyDetail,
                                        CreatedBy = Program.LogonUser.DisplayName,
                                        CreatedTime = DateTime.Now,
                                        IsTestData = Program.LogonUser.IsTestData,
                                        PainKey = p.PainKey,
                                        X = p.X,
                                        Y = p.Y,
                                        Diagram = p.Diagram
                                    };
                                    _pointsToAdd.Add(pointKey, newP);
                                    newPoints.Add(newP);
                                }

                                populatePoints(picPainScale, newPoints, _viewOnly);
                            } else {
                                //Put points on diagram image
                                populatePoints(picPainScale, painPoints, _viewOnly);
                            }

                        }

                        if (detail.PassiveAccessoryMovementsAndPalpationOfSpineDiagram != null) {
                            Diagram dgSpine = session.Merge<Diagram>(detail.PassiveAccessoryMovementsAndPalpationOfSpineDiagram);
                            MemoryStream stream = new MemoryStream(dgSpine.Image);
                            Image img = Image.FromStream(stream);

                            /*
                            stream.Close();

                            picSpine.Image = img;
                            picSpine.Width = dgSpine.ImageWidth;
                            picSpine.Height = dgSpine.ImageHeight;
                            picSpine.SizeMode = PictureBoxSizeMode.StretchImage;
                            picSpine.Tag = dgSpine;
                            */
                            Bitmap imgResized = new Bitmap(dgSpine.ImageWidth, dgSpine.ImageHeight);
                            using (Graphics g = Graphics.FromImage(imgResized)) {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                            }

                            stream.Close();

                            picSpine.Image = imgResized;
                            picSpine.Tag = dgSpine;

                            //Get points from DB
                            IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetPhysiotherapyPoints(detail, dgSpine, Program.config.MaxPointsPerDiagram);

                            //When loading details from template, remove all existing points and add new points from template
                            if (detail.Id != EditingPhysiotherapyDetail.Id) {

                                picSpine.Controls.Clear();

                                Diagram editingSpineDiagram = session.Merge<Diagram>(EditingPhysiotherapyDetail.PassiveAccessoryMovementsAndPalpationOfSpineDiagram);
                                IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetPhysiotherapyPoints(EditingPhysiotherapyDetail, editingSpineDiagram, Program.config.MaxPointsPerDiagram);

                                foreach (PointOnDiagram p in existingPoints) {
                                    string pointKey = string.Format("{0}_{1}_{2}",
                                        p.Diagram.Id,
                                        p.X,
                                        p.Y);
                                    _pointsToRemove.Add(pointKey, p);
                                }

                                IList<PointOnDiagram> newPoints = new List<PointOnDiagram>();

                                foreach (PointOnDiagram p in painPoints) {
                                    string pointKey = string.Format("{0}_{1}_{2}",
                                        p.Diagram.Id,
                                        p.X,
                                        p.Y);
                                    PointOnPhysiotherapy newP = new PointOnPhysiotherapy {
                                        PhysiotherapyDetail = EditingPhysiotherapyDetail,
                                        CreatedBy = Program.LogonUser.DisplayName,
                                        CreatedTime = DateTime.Now,
                                        IsTestData = Program.LogonUser.IsTestData,
                                        PainKey = p.PainKey,
                                        X = p.X,
                                        Y = p.Y,
                                        Diagram = p.Diagram
                                    };
                                    _pointsToAdd.Add(pointKey, newP);
                                    newPoints.Add(newP);
                                }

                                populatePoints(picSpine, newPoints, _viewOnly);
                            } else {
                                //Put points on diagram image
                                populatePoints(picSpine, painPoints, _viewOnly);
                            }
                        }
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Physiotherapy Details. " + ex.Message);
                Program.log.Error("Failed to load Physiotherapy Details.", ex);
            }
        }

        //Save changes
        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingPhysiotherapyDetail.UpdatedBy = Program.LogonUser.DisplayName;
            EditingPhysiotherapyDetail.UpdatedTime = DateTime.Now;

            PhysiotherapyDetail detailToSave = session.Merge(EditingPhysiotherapyDetail);
            updatePhysiotherapyDetailInput(detailToSave);
            session.Save(detailToSave);

            //Save points
            foreach (PointOnPhysiotherapy p in _pointsToRemove.Values) {
                PointOnPhysiotherapy pToDelete = session.Get<PointOnPhysiotherapy>(p.Id);
                //Update UpdateBy field so the audit listener would know who is deleting
                pToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                session.Delete(pToDelete);
            }
            foreach (PointOnPhysiotherapy p in _pointsToAdd.Values) {

                //Since PhysiotherapyDetail has been updated with a new instance, the reference for PointOnPhysiotherapy need to be updated.
                p.PhysiotherapyDetail = detailToSave;

                PointOnPhysiotherapy pToAdd = session.Merge(p);
                session.Save(pToAdd);
            }
        }

        public void LoadFromTemplate(ITreatmentDetail templateDetail) {
            PhysiotherapyDetail detail = templateDetail as PhysiotherapyDetail;
            populatePhysiotherapyDetail(detail);
        }

        private void updatePhysiotherapyDetailInput(PhysiotherapyDetail detail) {

            detail.PainScaleNow = Convert.ToInt32(numPainScaleNow.Value);
            detail.PainScaleBest = Convert.ToInt32(numPainScaleBest.Value);
            detail.PainScaleWorst = Convert.ToInt32(numPainScaleWorst.Value);

            detail.DescriptionOfSymptoms = txtDescOfSymp.Text;

            detail.SymptomTrend = rdoSymptomTrendDecreasing.Checked ? Trend.Decreasing :
                rdoSymptomTrendIncreasing.Checked ? Trend.Increasing :
                rdoSymptomTrendStatic.Checked ? Trend.Static :
                Trend.None;

            detail.ConstantVsIntermittent = txtConstantVsIntermittent.Text;

            detail.DayChanges = txtDayChanges.Text;

            detail.IrritabilityLevel = rdoIrritabilityHigh.Checked ? IrritabilityLevels.High :
                rdoIrritabilityModerate.Checked ? IrritabilityLevels.Moderate :
                rdoIrritabilityLow.Checked ? IrritabilityLevels.Low :
                IrritabilityLevels.None;

            detail.AggravatedBy = txtAggravatedBy.Text;

            detail.EasedBy = txtEasedBy.Text;

            detail.Occupation = txtOccupation.Text;

            detail.WorkStatus = txtWorkStatus.Text;

            detail.Goals = txtGoal.Text;

            detail.RedFlagsAndPrecautions = txtRedFlag.Text;

            detail.HistoryOfPresentingComplaint = txtHistoryOfPresentingComplaint.Text;

            detail.JointSoundsAndAbnormalities = txtJointSoundsAbnormalities.Text;

            detail.PastRelevantHistoryAndTreatment = txtPastRelevantHistory.Text;

            detail.Investigations = txtInvestigations.Text;

            detail.Medications = txtMedications.Text;

            detail.GeneralMedicalHistoryAndMedications = txtGeneralMedicalHistory.Text;

            detail.SocialHistoryAndRecreationalActivities = txtSocialHistory.Text;

            detail.ObservationPostureGait = txtObservation.Text;

            detail.ActiveRangeOfMotion = txtActiveRangeOfMotion.Text;

            detail.MuscleStrengthAndMyotomes = txtMuscleStrength.Text;

            detail.Palpation = txtPalpation.Text;

            detail.Neurological = txtNeurological.Text;

            detail.Reflexes = txtReflexes.Text;

            detail.Sensation = txtSensation.Text;

            detail.PassiveRangeOfMotion = txtPassiveRangeOfMotion.Text;

            detail.StabilityTestsAndSpecialTests = txtStabilityTests.Text;

            detail.Analysis = txtAnalysis.Text;

            detail.TreatmentPlan = txtTreatmentPlains.Text;

            detail.ConsentForTreatment = chkConsentForTreatmentVerbal.Checked ? ConsentForTreaments.Verbal :
                ConsentForTreaments.None;
        }

        //Create a new point based on diagram
        protected override PointOnDiagram createNewPoint(PictureBox picDiagram) {
            PointOnPhysiotherapy point = new PointOnPhysiotherapy {
                IsTestData = Program.LogonUser.IsTestData,
                CreatedBy = Program.LogonUser.DisplayName,
                CreatedTime = DateTime.Now,
                Diagram = (picDiagram.Tag as Diagram),
                PhysiotherapyDetail = EditingPhysiotherapyDetail
            };

            return point;
        }

        //Add a point when click on the diagram
        private void picPainScale_MouseClick(object sender, MouseEventArgs e) {

            if (_viewOnly) return;

            if (!rdoKeyPain.Checked &&
                !rdoKeyNumbness.Checked &&
                !rdoKeyPinAndNeedles.Checked) {

                MessageBox.Show(this, "Please select one of the legend keys under the diagram.");
                return;
            }

            PainKeys painKey = (rdoKeyPain.Checked ? PainKeys.Pain :
                (rdoKeyNumbness.Checked ? PainKeys.Numbness :
                (rdoKeyPinAndNeedles.Checked ? PainKeys.PinsAndNeedles : PainKeys.None)));

            addPointToDiagram(picPainScale, painKey, e);
        }

        private void picSpine_MouseClick(object sender, MouseEventArgs e) {

            if (_viewOnly) return;

            PainKeys painKey = PainKeys.None;

            addPointToDiagram(picSpine, painKey, e);
        }

        private void UCPhysiotherapyDetail_Load(object sender, EventArgs e) {
            /*
             * Load controls in the OnLoad event other than in the contructor. 
             * Otherwise, the control locations will not be accurate.
             * Still don't know why.
             */
            loadDetail();
        }
    }
}
