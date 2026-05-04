using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using EHD.Repository;
using EHD.Model.Entity;
using EHD.Constant;
using EHD.Service;
using Utility;
using System.IO;

namespace EHD.Admin {
    public partial class ManageDiagramForm : Form {

        private Diagram selectedDiagram = null;

        public ManageDiagramForm() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e) {
            if(DialogResult.OK == (new EditDiagramForm()).ShowDialog(this)) {
                populateDiagramList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(selectedDiagram != null) {

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to delete Diagram [" + selectedDiagram.DisplayName + "]?",
                    "Delete Diagram",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {
                        try {
                            //Check if there is any point linking to this diagram
                            int pointCount = session.QueryOver<PointOnChiropractic>()
                                .Where(x => x.Diagram.Id == selectedDiagram.Id)
                                .RowCount();
                            pointCount += session.QueryOver<PointOnChiropracticFollowUp>()
                                .Where(x => x.Diagram.Id == selectedDiagram.Id)
                                .RowCount();
                            pointCount += session.QueryOver<PointOnMassage>()
                                .Where(x => x.Diagram.Id == selectedDiagram.Id)
                                .RowCount();
                            pointCount += session.QueryOver<PointOnPhysiotherapy>()
                                .Where(x => x.Diagram.Id == selectedDiagram.Id)
                                .RowCount();

                            if(pointCount > 0) {
                                MessageBox.Show(this,
                                    "There are points on this diagram. It CANNOT be changed/removed.",
                                    "Cannot Remove",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                            } else {
                                Diagram diagram = session.Get<Diagram>(selectedDiagram.Id);
                                session.Delete(diagram);
                            }

                            tr.Commit();
                        } catch(Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete Diagram. " + ex.Message);
                            Program.log.Error("Failed to delete Diagram.", ex);
                        }
                    }

                    //Refresh Diagram List
                    populateDiagramList();
                }
            }
        }

        private void ManageDiagramsForm_Load(object sender, EventArgs e) {
            //Populate Diagram list
            populateDiagramList();
        }

        private void populateDiagramList() {
            //Clear existing list
            pnlImageFlow.Controls.Clear();

            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {

                    var diagrams = session.QueryOver<Diagram>().List<Diagram>();

                    foreach(Diagram diagram in diagrams) {
                        MemoryStream stream = new MemoryStream(diagram.Image);
                        Image img = Image.FromStream(stream);
                        stream.Close();

                        Button btnPic = new Button();
                        btnPic.Height = 120;
                        btnPic.Width = 120;
                        btnPic.FlatStyle = FlatStyle.Flat;
                        btnPic.FlatAppearance.BorderColor = Color.LightGray;
                        btnPic.FlatAppearance.BorderSize = 1;
                        btnPic.BackgroundImage = img;
                        btnPic.BackgroundImageLayout = ImageLayout.Stretch;
                        btnPic.Tag = diagram;
                        btnPic.Text = diagram.DiagramName;
                        btnPic.ForeColor = Color.Navy;
                        btnPic.Font = new Font("Verdana", 9, FontStyle.Bold | FontStyle.Italic);
                        btnPic.GotFocus += new EventHandler(pic_GotFocus);

                        pnlImageFlow.Controls.Add(btnPic);

                        if(selectedDiagram != null && diagram.Id == selectedDiagram.Id) {
                            btnPic.Focus();
                        }
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateDiagramList error.", ex);
                }
            }
        }

        void pic_GotFocus(object sender, EventArgs e) {
            //Clear picture highlights
            clearPicHighlights();

            Button btnPic = sender as Button;
            btnPic.FlatAppearance.BorderColor = Color.SkyBlue;
            btnPic.FlatAppearance.BorderSize = 2;
            Diagram diagram = btnPic.Tag as Diagram;

            selectedDiagram = diagram;
        }

        private void clearPicHighlights() {
            foreach(Control ctr in pnlImageFlow.Controls) {
                if(ctr is Button) {
                    Button btn = ctr as Button;
                    btn.FlatAppearance.BorderColor = Color.LightGray;
                    btn.FlatAppearance.BorderSize = 1;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if(selectedDiagram != null) {
                if(DialogResult.OK == (new EditDiagramForm(selectedDiagram)).ShowDialog(this)) {
                    populateDiagramList();
                }
            }
        }
    }
}
