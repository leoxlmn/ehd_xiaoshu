using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EHD.Model.Entity;
using EHD.Repository;
using EHD.Service;
using EHD.Constant;
using NHibernate;
using NHibernate.Criterion;
using System.IO;
using System.Drawing.Drawing2D;

namespace EHD.Admin {
    public partial class EditDiagramForm : Form {

        public Diagram EditingDiagram { get; set; }
        private Image NewDiagramImage { get; set; }

        public EditDiagramForm() {
            InitializeComponent();
        }

        public EditDiagramForm(Diagram diagram)
            : this() {
            EditingDiagram = diagram;
        }

        private void EditDiagramForm_Load(object sender, EventArgs e) {
            loadDiagram();
        }

        private void loadDiagram() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingDiagram == null || EditingDiagram.Id == default(int)) {
                        EditingDiagram = new Diagram();
                        EditingDiagram.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingDiagram.CreatedBy = Program.LogonUser.DisplayName;
                        EditingDiagram.CreatedTime = DateTime.Now;
                    } else {
                        EditingDiagram = session.Merge<Diagram>(EditingDiagram);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load Diagram. " + ex.Message);
                    Program.log.Error("Failed to load Diagram.", ex);
                }
            }

            populateDiagramDetail(EditingDiagram);
        }

        private void populateDiagramDetail(Diagram diagram) {
            if(diagram != null) {
                txtDiagramName.Text = diagram.DiagramName;
                txtImageType.Text = diagram.ImageType;
                numImageWidth.Value = diagram.ImageWidth;
                numImageHeight.Value = diagram.ImageHeight;

                if(diagram.Image != null) {
                    MemoryStream stream = new MemoryStream(diagram.Image);
                    Image img = Image.FromStream(stream);
                    stream.Close();

                    picDiagram.Image = img;
                } else {
                    picDiagram.Image = null;
                }
            }
        }

        private void updateDiagramInput(Diagram diagram) {
            diagram.DiagramName = txtDiagramName.Text;
            diagram.ImageType = txtImageType.Text;
            diagram.ImageWidth = Convert.ToInt32(numImageWidth.Value);
            diagram.ImageHeight = Convert.ToInt32(numImageHeight.Value);

            if(NewDiagramImage != null) {
                using(MemoryStream mem = new MemoryStream()) {
                    NewDiagramImage.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                    diagram.Image = mem.ToArray();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingDiagram == null || EditingDiagram.Id == default(int)) {
                        EditingDiagram = new Diagram();
                        EditingDiagram.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingDiagram.CreatedBy = Program.LogonUser.DisplayName;
                        EditingDiagram.CreatedTime = DateTime.Now;
                    } else {
                        EditingDiagram = session.QueryOver<Diagram>()
                            .Where(x => x.Id == EditingDiagram.Id)
                            .SingleOrDefault();

                        EditingDiagram.UpdatedTime = DateTime.Now;
                        EditingDiagram.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    updateDiagramInput(EditingDiagram);

                    if(!EditingDiagram.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingDiagram);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save Diagram. " + ex.Message);
                    Program.log.Error("Failed to save Diagram.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnChangeDiagram_Click(object sender, EventArgs e) {
            try {
                if(DialogResult.OK == dlgOpenFile.ShowDialog(this)) {
                    using(FileStream stream = new FileStream(dlgOpenFile.FileName, FileMode.Open)) {
                        using(BinaryReader reader = new BinaryReader(stream)) {
                            byte[] buff = new byte[stream.Length - 1];
                            reader.Read(buff, 0, buff.Length);
                            MemoryStream mem = new MemoryStream(buff);
                            NewDiagramImage = Image.FromStream(mem);

                            picDiagram.Image = NewDiagramImage;
                            txtImageType.Text = Path.GetExtension(dlgOpenFile.FileName);
                        }
                    }
                }
            } catch(Exception ex) {
                NewDiagramImage = null;
                MessageBox.Show(this, "Cannot load diagram image. " + ex.Message);
                Program.log.Error("Cannot load diagram image.", ex);
            }

        }
    }
}
