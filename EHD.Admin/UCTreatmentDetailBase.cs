using System.Windows.Forms;
using System.Collections.Generic;
using EHD.Model.Entity;
using System.Drawing;
using System;
using System.Reflection;
using System.IO;
using NHibernate;
using EHD.Repository;

namespace EHD.Admin {
    public class UCTreatmentDetailBase : UserControl {

        protected IDictionary<string, PointOnDiagram> _pointsToAdd = new Dictionary<string, PointOnDiagram>();
        protected IDictionary<string, PointOnDiagram> _pointsToRemove = new Dictionary<string, PointOnDiagram>();

        public UCTreatmentDetailBase() {
        }

        protected virtual void loadDetail(){}

        //Populate points on the given diagram
        protected void populatePoints(PictureBox picDiagram, IList<PointOnDiagram> points, bool ViewOnly) {
            if(points == null || points.Count <= 0) return;

            Graphics g = picDiagram.CreateGraphics();
            foreach(PointOnDiagram point in points) {

                PainKeys painKey = point.PainKey;
                Image painKeyImage = Program.PinPointImages[painKey];

                int deltaX = painKeyImage.Width / 2;
                int deltaY = painKeyImage.Height / 2;

                PictureBox picPoint = new PictureBox();
                picPoint.BackColor = Color.Transparent;
                picPoint.Margin = new Padding(0);
                picPoint.Padding = new Padding(0);
                picPoint.Image = painKeyImage;
                picPoint.Width = painKeyImage.Width;
                picPoint.Height = painKeyImage.Height;
                picPoint.SizeMode = PictureBoxSizeMode.AutoSize;
                picPoint.Left = point.X - deltaX;
                picPoint.Top = point.Y - deltaY;
                picPoint.Tag = point;

                if(!ViewOnly) {
                    picPoint.Click += new EventHandler(pic_Click);
                }

                picDiagram.Controls.Add(picPoint);
            }
        }

        //Remove point from diagram
        protected void pic_Click(object sender, EventArgs e) {
            PictureBox picPoint = (PictureBox)sender;
            PointOnDiagram point = picPoint.Tag as PointOnDiagram;

            //Remove from PointsToAdd list if it's there
            string pointKey = string.Format("{0}_{1}_{2}",
                point.Diagram.Id, //(picPainScale.Parent.Tag as Diagram).Id,
                point.X,
                point.Y);
            if(_pointsToAdd.ContainsKey(pointKey)) {
                _pointsToAdd.Remove(pointKey);
            } else {
                _pointsToRemove.Add(pointKey, point);
            }

            //Otherwise, add to PointsToRemove list
            picPoint.Parent.Controls.Remove(picPoint);
        }

        //Created a new point for the clicked PictureBox
        protected virtual PointOnDiagram createNewPoint(PictureBox picDiagram){
            return null;
        }

        //Add a point when click on the diagram
        protected void addPointToDiagram(PictureBox picDiagram, PainKeys painKey, MouseEventArgs e) {
            Image painKeyImage = Program.PinPointImages[painKey];

            Graphics g = picDiagram.CreateGraphics();

            //Calculate the center location
            Point p = new Point(e.X - painKeyImage.Width / 2, e.Y - painKeyImage.Height / 2);

            //Create a new Point
            PointOnDiagram point = createNewPoint(picDiagram);
            point.X = e.X;
            point.Y = e.Y;
            point.PainKey = painKey;

            //Add into PointsToAdd list
            string pointKey = string.Format("{0}_{1}_{2}",
                point.Diagram.Id,
                point.X,
                point.Y);
            _pointsToAdd.Add(pointKey, point);

            PictureBox picPoint = new PictureBox();
            picPoint.BackColor = Color.Transparent;
            picPoint.Width = painKeyImage.Width;
            picPoint.Height = painKeyImage.Height;
            picPoint.SizeMode = PictureBoxSizeMode.StretchImage;
            picPoint.Left = p.X;
            picPoint.Top = p.Y;
            picPoint.Tag = point;
            picPoint.Click += new EventHandler(pic_Click);
            picPoint.Image = painKeyImage;

            picDiagram.Controls.Add(picPoint);
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // UCTreatmentDetailBase
            // 
            this.Name = "UCTreatmentDetailBase";
            this.ResumeLayout(false);

        }

    }
}
