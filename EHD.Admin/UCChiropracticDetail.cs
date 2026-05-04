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
    public partial class UCChiropracticDetail : UCTreatmentDetailBase, ITreatmentDetailUserControl {

        private bool _viewOnly = false;

        public ChiropracticDetail EditingChiropracticDetail { get; set; }

        public ITreatmentDetail TreatmentDetail {
            get {
                return EditingChiropracticDetail;
            }
        }

        public PictureBox DiagramPictureBox {
            get {
                return this.picChiropractic;
            }
        }

        public UCChiropracticDetail(ChiropracticDetail ChiropracticDetail, bool ViewOnly) {
            if (ChiropracticDetail == null)
                throw new ArgumentException("ChiropracticDetail cannot be null.");

            _viewOnly = ViewOnly;

            InitializeComponent();

            EditingChiropracticDetail = ChiropracticDetail;
        }

        //Populate form
        protected override void loadDetail() {
            populateChiropracticDetail(EditingChiropracticDetail);
        }

        private void populateChiropracticDetail(ChiropracticDetail detail) {
            ISessionFactory sessionFactory = null;
            try {
                sessionFactory = SessionFactory.GetSessionFactory();
                using (ISession session = sessionFactory.OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    if (detail != null) {

                        //Automatically populate most of the fields
                        autoPopValues(this, detail);
                        //manuall populate other fileds
                        numCareGiverNeeded.Value = detail.NumOfCareGiverNeeded;
                        if (detail.LastWorkDate.HasValue) {
                            dtLastWorkDate.Value = detail.LastWorkDate.Value;
                            dtLastWorkDate.Checked = true;
                        } else {
                            dtLastWorkDate.Checked = false;
                        }
                        rdoDominantHandLeft.Checked = (detail.DominantHand == DominantHand.Left);
                        rdoDominantHandRight.Checked = (detail.DominantHand == DominantHand.Right);
                        rdoDominantHandAmbidextrous.Checked = (detail.DominantHand == DominantHand.Ambidextrous);

                        if (detail.ChiropracticDiagram != null) {
                            Diagram dg = session.Merge<Diagram>(detail.ChiropracticDiagram);
                            MemoryStream stream = new MemoryStream(dg.Image);
                            Image img = Image.FromStream(stream);
                            Bitmap imgResized = new Bitmap(dg.ImageWidth, dg.ImageHeight);
                            using (Graphics g = Graphics.FromImage(imgResized)) {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.DrawImage(img, 0, 0, imgResized.Width, imgResized.Height);
                            }

                            stream.Close();

                            picChiropractic.Image = imgResized;
                            picChiropractic.Tag = dg;

                            //Get points from DB
                            IList<PointOnDiagram> painPoints = (new JCService(session, Program.LogonUser)).GetChiropracticPoints(detail, Program.config.MaxPointsPerDiagram);

                            //When loading details from template, remove all existing points and add new points from template
                            if (detail.Id != EditingChiropracticDetail.Id) {

                                _pointsToRemove.Clear();
                                _pointsToAdd.Clear();
                                picChiropractic.Controls.Clear();

                                IList<PointOnDiagram> existingPoints = (new JCService(session, Program.LogonUser)).GetChiropracticPoints(EditingChiropracticDetail, Program.config.MaxPointsPerDiagram);

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
                                    PointOnChiropractic newP = new PointOnChiropractic {
                                        ChiropracticDetail = EditingChiropracticDetail,
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

                                populatePoints(picChiropractic, newPoints, _viewOnly);
                            } else {
                                //Put points on diagram image
                                populatePoints(picChiropractic, painPoints, _viewOnly);
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
            EditingChiropracticDetail.UpdatedBy = Program.LogonUser.DisplayName;
            EditingChiropracticDetail.UpdatedTime = DateTime.Now;

            ChiropracticDetail detailToSave = session.Merge(EditingChiropracticDetail);
            updateChiropracticDetailInput(detailToSave);
            session.Save(detailToSave);

            //Save points
            foreach (PointOnChiropractic p in _pointsToRemove.Values) {
                PointOnChiropractic pToDelete = session.Get<PointOnChiropractic>(p.Id);
                //Update UpdateBy field so the audit listener would know who is deleting
                pToDelete.UpdatedBy = Program.LogonUser.DisplayName;
                session.Delete(pToDelete);
            }
            foreach (PointOnChiropractic p in _pointsToAdd.Values) {

                //Since ChiropracticDetail has been updated with a new instance, the reference for PointOnChiropractic need to be updated.
                p.ChiropracticDetail = detailToSave;

                PointOnChiropractic pToAdd = session.Merge(p);
                session.Save(pToAdd);
            }
        }

        public void LoadFromTemplate(ITreatmentDetail templateDetail) {
            ChiropracticDetail detail = templateDetail as ChiropracticDetail;
            populateChiropracticDetail(detail);
        }

        private void updateChiropracticDetailInput(ChiropracticDetail detail) {

            //Automatically update most of the fields
            autoSetValues(this, detail);

            //Manually update other fields
            detail.NumOfCareGiverNeeded = Convert.ToInt32(numCareGiverNeeded.Value);
            if (dtLastWorkDate.Checked) {
                detail.LastWorkDate = dtLastWorkDate.Value;
            } else {
                detail.LastWorkDate = null;
            }
            detail.DominantHand = rdoDominantHandLeft.Checked ? DominantHand.Left :
                rdoDominantHandRight.Checked ? DominantHand.Right :
                rdoDominantHandAmbidextrous.Checked ? DominantHand.Ambidextrous :
                DominantHand.Unknown;

        }

        //Create a new point based on diagram
        protected override PointOnDiagram createNewPoint(PictureBox picDiagram) {
            PointOnChiropractic point = new PointOnChiropractic {
                IsTestData = Program.LogonUser.IsTestData,
                CreatedBy = Program.LogonUser.DisplayName,
                CreatedTime = DateTime.Now,
                Diagram = (picDiagram.Tag as Diagram),
                ChiropracticDetail = EditingChiropracticDetail
            };

            return point;
        }

        //Add a point when click on the diagram
        private void picChiropractic_MouseClick(object sender, MouseEventArgs e) {

            if (_viewOnly) return;

            PainKeys painKey = PainKeys.None;

            addPointToDiagram(picChiropractic, painKey, e);
        }

        private void UCChiropracticDetail_Load(object sender, EventArgs e) {
            /*
             * Load controls in the OnLoad event other than in the contructor. 
             * Otherwise, the control locations will not be accurate.
             * Still don't know why.
             */
            loadDetail();
        }


        private void autoSetValues(Control container, object valueObj) {
            foreach (Control ctr in container.Controls) {
                if (ctr is CheckBox) {

                    //Normal checkboxes
                    string chkName = ctr.Name;
                    if (chkName.Length < 4 || !chkName.StartsWith("chk")) continue;
                    string propName = chkName.Substring(3);
                    PropertyInfo prop = valueObj.GetType().GetProperty(propName);
                    if (prop != null && prop.PropertyType == typeof(bool)) {
                        prop.SetValue(valueObj, (ctr as CheckBox).Checked, null);
                    }

                    //LeftRight checkboxes
                    else if (ctr.Name.EndsWith("L") || ctr.Name.EndsWith("R")) {
                        LeftRight val = LeftRight.None;
                        bool L = false;
                        bool R = false;
                        propName = chkName.Substring(3, chkName.Length - 4);

                        if (chkName.EndsWith("L")) {
                            L = (ctr as CheckBox).Checked;
                            Control[] searchCtrls = ctr.Parent.Controls.Find("chk" + propName + "R", false);
                            if (searchCtrls.Length == 1 && searchCtrls[0] is CheckBox) {
                                R = (searchCtrls[0] as CheckBox).Checked;
                            } else {
                                continue;
                            }
                        } else {
                            R = (ctr as CheckBox).Checked;
                            Control[] searchCtrls = ctr.Parent.Controls.Find("chk" + propName + "L", false);
                            if (searchCtrls.Length == 1 && searchCtrls[0] is CheckBox) {
                                L = (searchCtrls[0] as CheckBox).Checked;
                            } else {
                                continue;
                            }
                        }

                        val = (L && R ? LeftRight.Both :
                            (L ? LeftRight.Left :
                            (R ? LeftRight.Right : LeftRight.None)));

                        prop = valueObj.GetType().GetProperty(propName);
                        if (prop != null && prop.PropertyType == typeof(LeftRight)) {
                            prop.SetValue(valueObj, val, null);
                        }
                    }
                } else if (ctr is TextBox) {
                    string txtName = ctr.Name;
                    if (txtName.Length < 4 && !txtName.StartsWith("txt")) continue;
                    string propName = txtName.Substring(3);
                    PropertyInfo prop = valueObj.GetType().GetProperty(propName);
                    if (prop != null) {
                        if (prop.PropertyType == typeof(string)) {
                            prop.SetValue(valueObj, (ctr as TextBox).Text.Trim(), null);
                        } else if (prop.PropertyType == typeof(int)) {
                            int intVal = 0;
                            if (int.TryParse((ctr as TextBox).Text.Trim(), out intVal)) {
                                prop.SetValue(valueObj, intVal, null);
                            }
                        }
                    }
                } else if (ctr.HasChildren) {
                    autoSetValues(ctr, valueObj);
                }
            }
        }

        private void autoPopValues(Control container, object valueObj) {
            foreach (Control ctr in container.Controls) {
                if (ctr is CheckBox) {
                    string chkName = ctr.Name;
                    if (chkName.Length < 4 || !chkName.StartsWith("chk")) continue;
                    string propName = chkName.Substring(3);
                    PropertyInfo prop = valueObj.GetType().GetProperty(propName);
                    if (prop != null && prop.PropertyType == typeof(bool)) {
                        (ctr as CheckBox).Checked = Convert.ToBoolean(prop.GetValue(valueObj, null));
                    } else if (chkName.EndsWith("L") || chkName.EndsWith("R")) {
                        LeftRight val = LeftRight.None;
                        propName = chkName.Substring(3, chkName.Length - 4);

                        prop = valueObj.GetType().GetProperty(propName);
                        if (prop != null) {
                            val = (LeftRight)Enum.Parse(typeof(LeftRight), prop.GetValue(valueObj, null).ToString());
                            if (chkName.EndsWith("L")) {
                                ((CheckBox)ctr).Checked = (val == LeftRight.Both || val == LeftRight.Left);
                            } else if (chkName.EndsWith("R")) {
                                ((CheckBox)ctr).Checked = (val == LeftRight.Both || val == LeftRight.Right);
                            }
                        }
                    }
                } else if (ctr is TextBox) {
                    string txtName = ctr.Name;
                    if (txtName.Length < 4 && !txtName.StartsWith("txt")) continue;
                    string propName = txtName.Substring(3);
                    PropertyInfo prop = valueObj.GetType().GetProperty(propName);
                    if (prop != null) {
                        object val = prop.GetValue(valueObj, null);
                        if (val != null) (ctr as TextBox).Text = val.ToString();
                    }
                } else if (ctr.HasChildren) {
                    autoPopValues(ctr, valueObj);
                }
            }
        }
    }
}
