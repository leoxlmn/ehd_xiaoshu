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
    public partial class UCChiropracticSOAP : UCTreatmentDetailBase, IFollowUpDetailUserControl {

        private bool _viewOnly = false;

        public ChiropracticFollowUpDetail EditingChiropracticSOAP { get; set; }

        public IFollowUpDetail FollowUpDetail {
            get {
                return EditingChiropracticSOAP;
            }
        }

        public PictureBox ChirapracticSOAPDiagramPictureBox {
            get {
                return this.picChiropracticSOAPDiagram;
            }
        }

        public void Save(ISession session) {
            //Assign UpdatedBy/UpdatedTime fields
            EditingChiropracticSOAP.UpdatedBy = Program.LogonUser.DisplayName;
            EditingChiropracticSOAP.UpdatedTime = DateTime.Now;

            ChiropracticFollowUpDetail detailToSave = session.Merge(EditingChiropracticSOAP);
            updateChiropracticSOAPInput(detailToSave);
            session.Save(detailToSave);

            //Save points
            foreach(PointOnChiropracticFollowUp p in _pointsToRemove.Values) {
                PointOnChiropracticFollowUp pToDelete = session.Get<PointOnChiropracticFollowUp>(p.Id);
                //Update UpdateBy field so the audit listener would know who is deleting
                pToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                session.Delete(pToDelete);
            }
            foreach(PointOnChiropracticFollowUp p in _pointsToAdd.Values) {

                //Since ChriopracticFollowUpDetail has been updated with a new instance, the reference for PointOnChriopracticFollowUp need to be updated.
                p.ChiropracticFollowUpDetail = detailToSave;

                PointOnChiropracticFollowUp pToAdd = session.Merge(p);
                session.Save(pToAdd);
            }
        }

        public void LoadFromTemplate(IFollowUpDetail templateDetail) {
            ChiropracticFollowUpDetail detail = templateDetail as ChiropracticFollowUpDetail;
            populateChiropracticSOAPDetail(detail);
        }

        //Update detail with user input
        private void updateChiropracticSOAPInput(ChiropracticFollowUpDetail detail) {
            int intValue = 0;
            detail.VAS = (numVAS.Text.Length > 0 ? int.Parse(numVAS.Text) : 0);
            detail.ConditionChange = rdoConditionImproving.Checked ? ChiropracticSOAPConditionChanges.Improving :
                rdoConditionNoChange.Checked ? ChiropracticSOAPConditionChanges.NoChange :
                rdoConditionWorsening.Checked ? ChiropracticSOAPConditionChanges.Worsening :
                rdoConditionAggravation.Checked ? ChiropracticSOAPConditionChanges.Aggravation :
                rdoConditionNew.Checked ? ChiropracticSOAPConditionChanges.NewCondition :
                ChiropracticSOAPConditionChanges.Unknown;
            detail.ConditionChangeNote = txtConditionNote.Text;
            detail.O = txtO.Text;
            detail.OsseousManipulation = chkOsseousManipulation.Checked;
            detail.Massage = chkMassage.Checked;
            detail.Stretch = chkStretch.Checked;
            detail.TriggerPointTherapy = chkTriggerPointTherapy.Checked;
            detail.Traction = chkTraction.Checked;
            detail.TheraputicExercise = chkTheraputicExercise.Checked;
            detail.ExerciseNote = txtExerciseNote.Text;
            detail.Heat = chkHeat.Checked;
            detail.Cold = chkCold.Checked;
            detail.UltraSound = chkUltrasound.Checked;
            if(int.TryParse(txtUltrasound.Text, out intValue)){
                detail.UltraSoundValue = intValue;
            }
            detail.Electrotherapy = chkElectrotherapy.Checked;
            detail.ElectrotherapyNote = txtElectrotherapy.Text;
            detail.NoDxChange = chkNoDxChange.Checked;
            detail.NewDiagnosis = chkNewDiagnosis.Checked;
            detail.NewDiagnosisNote = txtNewDiagnosisNote.Text;
            if(int.TryParse(txtPTRDays.Text, out intValue)) {
                detail.PTRDays = intValue;
            }
            if(int.TryParse(txtPTRWeeks.Text, out intValue)) {
                detail.PTRWeeks = intValue;
            }
            if(int.TryParse(txtPTRMonth.Text, out intValue)) {
                detail.PTRMonths = intValue;
            }
            detail.PRN = chkPRN.Checked;
            detail.Referral = chkReferral.Checked;
            detail.ReferralNote = txtReferral.Text;
            detail.HomeCare = txtHomeCare.Text;
            detail.Comments = txtComment.Text;
        }

        //Create a new point based on diagram
        protected override PointOnDiagram createNewPoint(PictureBox picDiagram) {
            PointOnChiropracticFollowUp point = new PointOnChiropracticFollowUp {
                IsTestData = Program.LogonUser.IsTestData,
                CreatedBy = Program.LogonUser.DisplayName,
                CreatedTime = DateTime.Now,
                Diagram = (picDiagram.Tag as Diagram),
                ChiropracticFollowUpDetail = EditingChiropracticSOAP
            };

            return point;
        }

        private void picChiropracticSOAPDiagram_MouseClick(object sender, MouseEventArgs e) {

            lblPictureFocus.Focus();

            if(_viewOnly) return;

            PainKeys painKey = PainKeys.None;
            addPointToDiagram(picChiropracticSOAPDiagram, painKey, e);
        }

        public UCChiropracticSOAP(ChiropracticFollowUpDetail ChiropracticSOAPDetail, bool ViewOnly) {
            if(ChiropracticSOAPDetail == null)
                throw new ArgumentException("ChiropracticSOAPDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingChiropracticSOAP = ChiropracticSOAPDetail;
        }

        private void UCChiropracticSOAP_Load(object sender, EventArgs e) {

            lblPictureFocus.Text = "";

            loadDetail();
        }

        //Populate form
        protected override void loadDetail() {
            populateChiropracticSOAPDetail(EditingChiropracticSOAP);
        }

        private void populateChiropracticSOAPDetail(ChiropracticFollowUpDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using(ISession session = sessionFactory.OpenSession()) {
                    if(detail != null) {

                        //TO DO: Populate chiropractic SOAP fields
                        numVAS.Text = detail.VAS.ToString();
                        rdoConditionImproving.Checked = (detail.ConditionChange == ChiropracticSOAPConditionChanges.Improving);
                        rdoConditionNoChange.Checked = (detail.ConditionChange == ChiropracticSOAPConditionChanges.NoChange);
                        rdoConditionWorsening.Checked = (detail.ConditionChange == ChiropracticSOAPConditionChanges.Worsening);
                        rdoConditionAggravation.Checked = (detail.ConditionChange == ChiropracticSOAPConditionChanges.Aggravation);
                        rdoConditionNew.Checked = (detail.ConditionChange == ChiropracticSOAPConditionChanges.NewCondition);
                        txtConditionNote.Text = detail.ConditionChangeNote;
                        txtO.Text = detail.O;

                        chkOsseousManipulation.Checked = detail.OsseousManipulation;
                        chkMassage.Checked = detail.Massage;
                        chkStretch.Checked = detail.Stretch;
                        chkTriggerPointTherapy.Checked = detail.TriggerPointTherapy;
                        chkTraction.Checked = detail.Traction;
                        chkTheraputicExercise.Checked = detail.TheraputicExercise;
                        txtExerciseNote.Text = detail.ExerciseNote;
                        chkHeat.Checked = detail.Heat;
                        chkCold.Checked = detail.Cold;
                        chkUltrasound.Checked = detail.UltraSound;
                        txtUltrasound.Text = detail.UltraSoundValue.ToString();
                        chkElectrotherapy.Checked = detail.Electrotherapy;
                        txtElectrotherapy.Text = detail.ElectrotherapyNote;
                        chkNoDxChange.Checked = detail.NoDxChange;
                        chkNewDiagnosis.Checked = detail.NewDiagnosis;
                        txtNewDiagnosisNote.Text = detail.NewDiagnosisNote;
                        txtPTRDays.Text = detail.PTRDays.ToString();
                        txtPTRWeeks.Text = detail.PTRWeeks.ToString();
                        txtPTRMonth.Text = detail.PTRMonths.ToString();
                        chkPRN.Checked = detail.PRN;
                        chkReferral.Checked = detail.Referral;
                        txtReferral.Text = detail.ReferralNote;
                        txtHomeCare.Text = detail.HomeCare;
                        txtComment.Text = detail.Comments;

                        if(detail.ChiropracticSOAPDiagram != null) {
                            Diagram dg = session.Merge<Diagram>(detail.ChiropracticSOAPDiagram);
                            MemoryStream stream = new MemoryStream(dg.Image);
                            Image img = Image.FromStream(stream);
                            Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                            using(Graphics g = Graphics.FromImage(imgResized)) {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                            }

                            stream.Close();

                            picChiropracticSOAPDiagram.Image = imgResized;
                            picChiropracticSOAPDiagram.Tag = dg;

                            //Get points from DB
                            IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetChiropracticSOAPPoints(detail, Program.config.MaxPointsPerDiagram);

                            //When loading details from template, remove all existing points and add new points from template
                            if (detail.Id != EditingChiropracticSOAP.Id) {

                                _pointsToRemove.Clear();
                                _pointsToAdd.Clear();
                                picChiropracticSOAPDiagram.Controls.Clear();

                                IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetChiropracticSOAPPoints(EditingChiropracticSOAP, Program.config.MaxPointsPerDiagram);

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
                                    PointOnChiropracticFollowUp newP = new PointOnChiropracticFollowUp {
                                        ChiropracticFollowUpDetail = EditingChiropracticSOAP,
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

                                populatePoints(picChiropracticSOAPDiagram, newPoints, _viewOnly);
                            } else {
                                //Put points on diagram image
                                populatePoints(picChiropracticSOAPDiagram, painPoints, _viewOnly);
                            }
                        }

                    }
                }
            } catch(Exception ex) {
                MessageBox.Show(this, "Failed to load Chiropractic SOAP Details. " + ex.Message);
                Program.log.Error("Failed to load Chiropractic SOAP Details.", ex);
            }
        }
    }
}
