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
    public partial class UCMassageDetail : UCTreatmentDetailBase, ITreatmentDetailUserControl {
        private bool _viewOnly = false;

        public MassageDetail EditingMassageDetail { get; set; }

        public ITreatmentDetail TreatmentDetail {
            get {
                return EditingMassageDetail;
            }
        }

        public PictureBox MassageDiagramPictureBox {
            get {
                return this.picMassageDiagram;
            }
        }

        public UCMassageDetail(MassageDetail MassageDetail, bool ViewOnly) {
            if(MassageDetail == null)
                throw new ArgumentException("MassageDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingMassageDetail = MassageDetail;
        }

        //Populate form
        protected override void loadDetail() {
            populateMassageDetail(EditingMassageDetail);
        }

        private void populateMassageDetail(MassageDetail detail) {
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

                        if (detail.MassageDiagram != null) {
                            Diagram dg = session.Merge<Diagram>(detail.MassageDiagram);
                            MemoryStream stream = new MemoryStream(dg.Image);
                            Image img = Image.FromStream(stream);
                            Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                            using (Graphics g = Graphics.FromImage(imgResized)) {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                            }

                            stream.Close();

                            picMassageDiagram.Image = imgResized;
                            picMassageDiagram.Tag = dg;

                            //Get points from DB
                            IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetMassagePoints(detail, dg, Program.config.MaxPointsPerDiagram);

                            //When loading details from template, remove all existing points and add new points from template
                            if (detail.Id != EditingMassageDetail.Id) {

                                _pointsToRemove.Clear();
                                _pointsToAdd.Clear();
                                picMassageDiagram.Controls.Clear();

                                Diagram editingDg = session.Merge<Diagram>(EditingMassageDetail.MassageDiagram);
                                IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetMassagePoints(EditingMassageDetail, editingDg, Program.config.MaxPointsPerDiagram);

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
                                    PointOnMassage newP = new PointOnMassage {
                                        MassageDetail = EditingMassageDetail,
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

                                populatePoints(picMassageDiagram, newPoints, _viewOnly);
                            } else {
                                //Put points on diagram image
                                populatePoints(picMassageDiagram, painPoints, _viewOnly);
                            }

                        }
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Massage Details. " + ex.Message);
                Program.log.Error("Failed to load Massage Details.", ex);
            }
        }

        //Save changes
        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingMassageDetail.UpdatedBy = Program.LogonUser.DisplayName;
            EditingMassageDetail.UpdatedTime = DateTime.Now;

            MassageDetail detailToSave = session.Merge(EditingMassageDetail);
            updateMassageDetailInput(detailToSave);
            session.Save(detailToSave);

            //Save points
            foreach (PointOnMassage p in _pointsToRemove.Values) {
                PointOnMassage pToDelete = session.Get<PointOnMassage>(p.Id);
                //Update UpdateBy field so the audit listener would know who is deleting
                pToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                session.Delete(pToDelete);
            }
            foreach (PointOnMassage p in _pointsToAdd.Values) {

                //Since MassageDetail has been updated with a new instance, the reference for PointOnMassage need to be updated.
                p.MassageDetail = detailToSave;

                PointOnMassage pToAdd = session.Merge(p);
                session.Save(pToAdd);
            }
        }

        public void LoadFromTemplate(ITreatmentDetail templateDetail) {
            MassageDetail detail = templateDetail as MassageDetail;
            populateMassageDetail(detail);
        }

        private void updateMassageDetailInput(MassageDetail detail) {
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
            PointOnMassage point = new PointOnMassage {
                IsTestData = Program.LogonUser.IsTestData,
                CreatedBy = Program.LogonUser.DisplayName,
                CreatedTime = DateTime.Now,
                Diagram = (picDiagram.Tag as Diagram),
                MassageDetail = EditingMassageDetail
            };

            return point;
        }

        private void picMassageDiagram_MouseClick(object sender, MouseEventArgs e) {

            if (_viewOnly) return;

            PainKeys painKey = PainKeys.None;

            addPointToDiagram(picMassageDiagram, painKey, e);
        }

        private void UCMassageDetail_Load(object sender, EventArgs e) {
            /*
             * Load controls in the OnLoad event other than in the contructor. 
             * Otherwise, the control locations will not be accurate.
             * Still don't know why.
             */
            loadDetail();
        }
    }
}
