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
    public partial class UCAcupunctureDetail : UCTreatmentDetailBase, ITreatmentDetailUserControl {
        
        private bool _viewOnly = false;

        public AcupunctureDetail EditingAcupunctureDetail { get; set; }

        public ITreatmentDetail TreatmentDetail {
            get {
                return EditingAcupunctureDetail;
            }
        }

        public PictureBox TongueDiagramPictureBox {
            get {
                return this.picTongueDiagram;
            }
        }

        public UCAcupunctureDetail(AcupunctureDetail AcupunctureDetail, bool ViewOnly) {
            if (AcupunctureDetail == null)
                throw new ArgumentException("AcupunctureDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingAcupunctureDetail = AcupunctureDetail;
        }

        //Populate form
        protected override void loadDetail() {
            populateAcupunctureDetail(EditingAcupunctureDetail);
        }

        private void populateAcupunctureDetail(AcupunctureDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    if (detail != null) {

                        txtPulse.Text = detail.Pulse;
                        txtTongue.Text = detail.Tongue;
                        txtBloodPressure.Text = detail.BloodPressure;
                        txtBloodSugar.Text = detail.BloodSugar;
                        txtSubjective.Text = detail.Subjective;
                        txtTCMDiagnosis.Text = detail.TCMDiagnosis;
                        txtTreatmentPlan.Text = detail.TreatmentPlan;
                    }

                    if (detail.TongueDiagram != null) {
                        Diagram dg = session.Merge<Diagram>(detail.TongueDiagram);
                        MemoryStream stream = new MemoryStream(dg.Image);
                        Image img = Image.FromStream(stream);
                        Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                        using (Graphics g = Graphics.FromImage(imgResized)) {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                        }

                        stream.Close();

                        picTongueDiagram.Image = imgResized;
                        picTongueDiagram.Tag = dg;

                        //Get points from DB
                        IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetAcupunctureTonguePoints(detail, Program.config.MaxPointsPerDiagram);

                        //When loading details from template, remove all existing points and add new points from template
                        if (detail.Id != EditingAcupunctureDetail.Id) {

                            _pointsToRemove.Clear();
                            _pointsToAdd.Clear();
                            picTongueDiagram.Controls.Clear();

                            IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetAcupunctureTonguePoints(EditingAcupunctureDetail, Program.config.MaxPointsPerDiagram);

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
                                PointOnAcupuncture newP = new PointOnAcupuncture {
                                    AcupunctureDetail = EditingAcupunctureDetail,
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

                            populatePoints(picTongueDiagram, newPoints, _viewOnly);
                        } else {
                            //Put points on diagram image
                            populatePoints(picTongueDiagram, painPoints, _viewOnly);
                        }
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Acupuncture Details. " + ex.Message);
                Program.log.Error("Failed to load Acupuncture Details.", ex);
            }
        }

        //Save changes
        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingAcupunctureDetail.UpdatedBy = Program.LogonUser.DisplayName;
            EditingAcupunctureDetail.UpdatedTime = DateTime.Now;

            AcupunctureDetail detailToSave = session.Merge(EditingAcupunctureDetail);
            updateAcupunctureDetailInput(detailToSave);
            session.Save(detailToSave);

            //Save points
            foreach (PointOnAcupuncture p in _pointsToRemove.Values) {
                PointOnAcupuncture pToDelete = session.Get<PointOnAcupuncture>(p.Id);
                //Update UpdateBy field so the audit listener would know who is deleting
                pToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                session.Delete(pToDelete);
            }
            foreach (PointOnAcupuncture p in _pointsToAdd.Values) {

                //Since AcupunctureDetail has been updated with a new instance, the reference for PointOnAcupuncture need to be updated.
                p.AcupunctureDetail = detailToSave;

                PointOnAcupuncture pToAdd = session.Merge(p);
                session.Save(pToAdd);
            }
        }

        public void LoadFromTemplate(ITreatmentDetail templateDetail) {
            AcupunctureDetail detail = templateDetail as AcupunctureDetail;
            populateAcupunctureDetail(detail);
        }

        private void updateAcupunctureDetailInput(AcupunctureDetail detail) {
            detail.Pulse = txtPulse.Text.Trim();
            detail.Tongue = txtTongue.Text.Trim();
            detail.BloodPressure = txtBloodPressure.Text.Trim();
            detail.BloodSugar = txtBloodSugar.Text.Trim();
            detail.Subjective = txtSubjective.Text.Trim();
            detail.TCMDiagnosis = txtTCMDiagnosis.Text.Trim();
            detail.TreatmentPlan = txtTreatmentPlan.Text.Trim();
        }

        //Create a new point based on diagram
        protected override PointOnDiagram createNewPoint(PictureBox picDiagram) {
            PointOnAcupuncture point = new PointOnAcupuncture {
                IsTestData = Program.LogonUser.IsTestData,
                CreatedBy = Program.LogonUser.DisplayName,
                CreatedTime = DateTime.Now,
                Diagram = (picDiagram.Tag as Diagram),
                AcupunctureDetail = EditingAcupunctureDetail
            };

            return point;
        }

        private void UCAcupunctureDetail_Load(object sender, EventArgs e) {
            /*
             * Load controls in the OnLoad event other than in the contructor. 
             * Otherwise, the control locations will not be accurate.
             * Still don't know why.
             */
            loadDetail();
        }

        private void picTongueDiagram_MouseClick(object sender, MouseEventArgs e) {
            if (_viewOnly) return;

            PainKeys painKey = PainKeys.None;

            addPointToDiagram(picTongueDiagram, painKey, e);
        }
    }
}
