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
    public partial class UCOsteopathyDetail : UCTreatmentDetailBase, ITreatmentDetailUserControl {
        private bool _viewOnly = false;

        public OsteopathyDetail EditingOsteopathyDetail { get; set; }

        public ITreatmentDetail TreatmentDetail {
            get {
                return EditingOsteopathyDetail;
            }
        }

        public PictureBox OsteopathyDiagramPictureBox {
            get {
                return this.picOsteopathyDiagram;
            }
        }

        public UCOsteopathyDetail(OsteopathyDetail OsteopathyDetail, bool ViewOnly) {
            if (OsteopathyDetail == null)
                throw new ArgumentException("OsteopathyDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingOsteopathyDetail = OsteopathyDetail;
        }

        //Populate form
        protected override void loadDetail() {
            populateOsteopathyDetail(EditingOsteopathyDetail);
        }

        private void populateOsteopathyDetail(OsteopathyDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    if (detail != null) {

                        txtActivityLimitation.Text = detail.ActivityLimitations;
                        txtTreatmentGoal.Text = detail.TreatmentGoal;
                        txtTreatmentFocus.Text = detail.TreatmentFocus;
                        txtTreatmentFrequency.Text = detail.TreatmentFrequency;
                        txtTreatmentDuration.Text = detail.TreatmentDuration;
                        rdoDiscussedYes.Checked = (detail.TreatmentPlanDiscussedWithClient.HasValue && detail.TreatmentPlanDiscussedWithClient.Value);
                        rdoDiscussedNo.Checked = (detail.TreatmentPlanDiscussedWithClient.HasValue && !detail.TreatmentPlanDiscussedWithClient.Value);
                        rdoConsentReceivedYes.Checked = (detail.ConsentReceived.HasValue && detail.ConsentReceived.Value);
                        rdoConsentReceivedNo.Checked = (detail.ConsentReceived.HasValue && !detail.ConsentReceived.Value);

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

                        txtOther.Text = detail.TreatmentAreaOther;

                        txtAssessmentsPerformed.Text = detail.AssessmentsPerformed;
                        txtResultsOfAssessments.Text = detail.ResultsOfAssessments;
                        txtReassessmentSchedule.Text = detail.ReassessmentSchedule;
                        txtReferrals.Text = detail.Referrals;
                        txtAnticipatedProgressionOfResponses.Text = detail.AnticipatedProgressionOfResponses;
                        txtRemedialExercisesRecommendations.Text = detail.RemedialExercisesRecommended;
                        txtRisks.Text = detail.Risks;

                        if (detail.OsteopathyDiagram != null) {
                            Diagram dg = session.Merge<Diagram>(detail.OsteopathyDiagram);
                            MemoryStream stream = new MemoryStream(dg.Image);
                            Image img = Image.FromStream(stream);
                            Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                            using (Graphics g = Graphics.FromImage(imgResized)) {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                            }

                            stream.Close();

                            picOsteopathyDiagram.Image = imgResized;
                            picOsteopathyDiagram.Tag = dg;

                            //Get points from DB
                            IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetOsteopathyPoints(detail, dg, Program.config.MaxPointsPerDiagram);

                            //When loading details from template, remove all existing points and add new points from template
                            if (detail.Id != EditingOsteopathyDetail.Id) {

                                _pointsToRemove.Clear();
                                _pointsToAdd.Clear();
                                picOsteopathyDiagram.Controls.Clear();

                                Diagram editingDg = session.Merge<Diagram>(EditingOsteopathyDetail.OsteopathyDiagram);
                                IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetOsteopathyPoints(EditingOsteopathyDetail, editingDg, Program.config.MaxPointsPerDiagram);

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
                                    PointOnOsteopathy newP = new PointOnOsteopathy {
                                        OsteopathyDetail = EditingOsteopathyDetail,
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

                                populatePoints(picOsteopathyDiagram, newPoints, _viewOnly);
                            } else {
                                //Put points on diagram image
                                populatePoints(picOsteopathyDiagram, painPoints, _viewOnly);
                            }

                        }
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Osteopathy Details. " + ex.Message);
                Program.log.Error("Failed to load Osteopathy Details.", ex);
            }
        }

        //Save changes
        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingOsteopathyDetail.UpdatedBy = Program.LogonUser.DisplayName;
            EditingOsteopathyDetail.UpdatedTime = DateTime.Now;

            OsteopathyDetail detailToSave = session.Merge(EditingOsteopathyDetail);
            updateOsteopathyDetailInput(detailToSave);
            session.Save(detailToSave);

            //Save points
            foreach (PointOnOsteopathy p in _pointsToRemove.Values) {
                PointOnOsteopathy pToDelete = session.Get<PointOnOsteopathy>(p.Id);
                //Update UpdateBy field so the audit listener would know who is deleting
                pToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                session.Delete(pToDelete);
            }
            foreach (PointOnOsteopathy p in _pointsToAdd.Values) {

                //Since OsteopathyDetail has been updated with a new instance, the reference for PointOnOsteopathy need to be updated.
                p.OsteopathyDetail = detailToSave;

                PointOnOsteopathy pToAdd = session.Merge(p);
                session.Save(pToAdd);
            }
        }

        public void LoadFromTemplate(ITreatmentDetail templateDetail) {
            OsteopathyDetail detail = templateDetail as OsteopathyDetail;
            populateOsteopathyDetail(detail);
        }

        private void updateOsteopathyDetailInput(OsteopathyDetail detail) {
            detail.ActivityLimitations = txtActivityLimitation.Text;
            detail.TreatmentGoal = txtTreatmentGoal.Text;
            detail.TreatmentFocus = txtTreatmentFocus.Text;
            detail.TreatmentFrequency = txtTreatmentFrequency.Text;
            detail.TreatmentDuration = txtTreatmentDuration.Text;
            if (rdoDiscussedYes.Checked) {
                detail.TreatmentPlanDiscussedWithClient = true;
            } else if (rdoDiscussedNo.Checked) {
                detail.TreatmentPlanDiscussedWithClient = false;
            } else {
                detail.TreatmentPlanDiscussedWithClient = null;
            }
            if (rdoConsentReceivedYes.Checked) {
                detail.ConsentReceived = true;
            } else if (rdoConsentReceivedNo.Checked) {
                detail.ConsentReceived = false;
            } else {
                detail.ConsentReceived = null;
            }

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

            detail.TreatmentAreaOther = txtOther.Text;

            detail.AssessmentsPerformed = txtAssessmentsPerformed.Text;
            detail.ResultsOfAssessments = txtResultsOfAssessments.Text;
            detail.ReassessmentSchedule = txtReassessmentSchedule.Text;
            detail.Referrals = txtReferrals.Text;
            detail.AnticipatedProgressionOfResponses = txtAnticipatedProgressionOfResponses.Text;
            detail.RemedialExercisesRecommended = txtRemedialExercisesRecommendations.Text;
            detail.Risks = txtRisks.Text;
        }

        //Create a new point based on diagram
        protected override PointOnDiagram createNewPoint(PictureBox picDiagram) {
            PointOnOsteopathy point = new PointOnOsteopathy {
                IsTestData = Program.LogonUser.IsTestData,
                CreatedBy = Program.LogonUser.DisplayName,
                CreatedTime = DateTime.Now,
                Diagram = (picDiagram.Tag as Diagram),
                OsteopathyDetail = EditingOsteopathyDetail
            };

            return point;
        }

        private void picOsteopathyDiagram_MouseClick(object sender, MouseEventArgs e) {

            if (_viewOnly) return;

            PainKeys painKey = PainKeys.None;

            addPointToDiagram(picOsteopathyDiagram, painKey, e);
        }

        private void UCOsteopathyDetail_Load(object sender, EventArgs e) {
            /*
             * Load controls in the OnLoad event other than in the contructor. 
             * Otherwise, the control locations will not be accurate.
             * Still don't know why.
             */
            loadDetail();
        }
    }
}
