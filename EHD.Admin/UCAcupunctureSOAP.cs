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
    public partial class UCAcupunctureSOAP : UCTreatmentDetailBase, IFollowUpDetailUserControl {
        private bool _viewOnly = false;

        public AcupunctureFollowUpDetail EditingAcupunctureSOAP { get; set; }

        public IFollowUpDetail FollowUpDetail {
            get {
                return EditingAcupunctureSOAP;
            }
        }

        public PictureBox TongueDiagramPictureBox {
            get {
                return this.picTongueDiagram;
            }
        }

        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingAcupunctureSOAP.UpdatedBy = Program.LogonUser.DisplayName;
            EditingAcupunctureSOAP.UpdatedTime = DateTime.Now;

            AcupunctureFollowUpDetail detailToSave = session.Merge(EditingAcupunctureSOAP);
            updateAcupunctureSOAPInput(detailToSave);
            session.Save(detailToSave);

            //Save points
            foreach(PointOnAcupunctureFollowUp p in _pointsToRemove.Values) {
                PointOnAcupunctureFollowUp pToDelete = session.Get<PointOnAcupunctureFollowUp>(p.Id);
                //Update UpdateBy field so the audit listener would know who is deleting
                pToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                session.Delete(pToDelete);
            }
            foreach(PointOnAcupunctureFollowUp p in _pointsToAdd.Values) {

                //Since AcupunctureFollowUpDetail has been updated with a new instance, the reference for PointOnAcupunctureFollowUp need to be updated.
                p.AcupunctureFollowUpDetail = detailToSave;

                PointOnAcupunctureFollowUp pToAdd = session.Merge(p);
                session.Save(pToAdd);
            }
        }

        public void LoadFromTemplate(IFollowUpDetail templateDetail) {
            AcupunctureFollowUpDetail detail = templateDetail as AcupunctureFollowUpDetail;
            populateAcupunctureSOAPDetail(detail);
        }

        //Update detail with user input
        private void updateAcupunctureSOAPInput(AcupunctureFollowUpDetail detail) {
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
            PointOnAcupunctureFollowUp point = new PointOnAcupunctureFollowUp {
                IsTestData = Program.LogonUser.IsTestData,
                CreatedBy = Program.LogonUser.DisplayName,
                CreatedTime = DateTime.Now,
                Diagram = (picDiagram.Tag as Diagram),
                AcupunctureFollowUpDetail = EditingAcupunctureSOAP
            };

            return point;
        }

        private void picTongueDiagram_MouseClick(object sender, MouseEventArgs e) {

            lblPictureFocus.Focus();

            if(_viewOnly) return;

            PainKeys painKey = PainKeys.None;
            addPointToDiagram(picTongueDiagram, painKey, e);
        }

        public UCAcupunctureSOAP(AcupunctureFollowUpDetail AcupunctureSOAPDetail, bool ViewOnly) {
            if (AcupunctureSOAPDetail == null)
                throw new ArgumentException("AcupunctureSOAPDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingAcupunctureSOAP = AcupunctureSOAPDetail;
        }

        private void UCAcupunctureSOAP_Load(object sender, EventArgs e) {
            lblPictureFocus.Text = "";
            loadDetail();
        }

        //Populate form
        protected override void loadDetail() {
            populateAcupunctureSOAPDetail(EditingAcupunctureSOAP);
        }

        private void populateAcupunctureSOAPDetail(AcupunctureFollowUpDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession()) {
                    if(detail != null) {

                        //Populate Acupuncture SOAP fields
                        txtPulse.Text = detail.Pulse;
                        txtTongue.Text = detail.Tongue;
                        txtBloodPressure.Text = detail.BloodPressure;
                        txtBloodSugar.Text = detail.BloodSugar;
                        txtSubjective.Text = detail.Subjective;
                        txtTCMDiagnosis.Text = detail.TCMDiagnosis;
                        txtTreatmentPlan.Text = detail.TreatmentPlan;

                        if(detail.TongueDiagram != null) {
                            Diagram dg = session.Merge<Diagram>(detail.TongueDiagram);
                            MemoryStream stream = new MemoryStream(dg.Image);
                            Image img = Image.FromStream(stream);
                            Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                            using(Graphics g = Graphics.FromImage(imgResized)) {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                            }

                            stream.Close();

                            picTongueDiagram.Image = imgResized;
                            picTongueDiagram.Tag = dg;

                            //Get points from DB
                            IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetAcupunctureFollowUpTonguePoints(detail, Program.config.MaxPointsPerDiagram);

                            //When loading details from template, remove all existing points and add new points from template
                            if (detail.Id != EditingAcupunctureSOAP.Id) {

                                _pointsToRemove.Clear();
                                _pointsToAdd.Clear();
                                picTongueDiagram.Controls.Clear();

                                IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetAcupunctureFollowUpTonguePoints(EditingAcupunctureSOAP, Program.config.MaxPointsPerDiagram);

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
                                    PointOnAcupunctureFollowUp newP = new PointOnAcupunctureFollowUp {
                                        AcupunctureFollowUpDetail = EditingAcupunctureSOAP,
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
                    }
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load Acupuncture SOAP Details. " + ex.Message);
                Program.log.Error("Failed to load Acupuncture SOAP Details.", ex);
            }
        }
    }
}
