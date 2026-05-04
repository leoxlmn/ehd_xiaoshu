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
    public partial class EditUserForm : Form {

        public User EditingUser { get; set; }
        private Image NewSignatureImage { get; set; }

        public EditUserForm() {
            InitializeComponent();
        }

        public EditUserForm(User user)
            : this() {
            EditingUser = user;
        }

        private void EditUserForm_Load(object sender, EventArgs e) {
            populateUserTitles();
            populateUserTypes();
            loadUser();
        }

        private void populateUserTitles() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    IList<UserTitle> lstTitle = session.QueryOver<UserTitle>().List();
                    //Add an empty UserTitle to the dropdown
                    lstTitle.Insert(0, new UserTitle());
                    drpUserTitle.DataSource = lstTitle;
                    drpUserTitle.ValueMember = "Id";
                    drpUserTitle.DisplayMember = "DisplayName";

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load User Title. " + ex.Message);
                    Program.log.Error("Failed to load User Title.", ex);
                }
            }
        }

        private void populateUserTypes() {
            List<string> lstUserTypes = new List<string>();
            foreach(string userType in Enum.GetNames(typeof(UserType))) {
                lstUserTypes.Add(userType);
            }
            drpUserTypes.DataSource = lstUserTypes;

            //Set None as default value
            drpUserTypes.SelectedItem = UserType.None.ToString();
        }

        private void loadUser() {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingUser == null || EditingUser.Id == default(int)) {
                        EditingUser = new User();
                        EditingUser.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingUser.CreatedBy = Program.LogonUser.DisplayName;
                        EditingUser.CreatedTime = DateTime.Now;
                    } else {
                        EditingUser = session.Merge<User>(EditingUser);
                    }

                    tr.Commit();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to load User. " + ex.Message);
                    Program.log.Error("Failed to load User.", ex);
                }
            }

            populateUserDetail(EditingUser);
        }

        private void populateUserDetail(User user) {
            if(user != null) {
                txtFirstName.Text = user.FirstName;
                txtMiddleName.Text = user.MiddleName;
                txtLastName.Text = user.LastName;
                txtUsername.Text = user.Username;
                txtPassword.Text = user.Password;
                txtCalendarId.Text = user.GoogleCalendarId;

                drpUserTypes.SelectedItem = user.UserType.ToString();
                drpUserTitle.SelectedValue = (user.Title != null ? user.Title.Id : 0);

                if(user.SignatureImage != null) {
                    MemoryStream stream = new MemoryStream(user.SignatureImage);
                    Image img = Image.FromStream(stream);
                    stream.Close();

                    picSignature.Image = img;
                } else {
                    picSignature.Image = null;
                }
            }
        }

        private void updateUserInput(User user) {
            user.FirstName = txtFirstName.Text.Trim();
            user.MiddleName = txtMiddleName.Text.Trim();
            user.LastName = txtLastName.Text.Trim();
            user.Username = txtUsername.Text.Trim();
            user.Password = txtPassword.Text;
            user.GoogleCalendarId = txtCalendarId.Text.Trim();
            if(drpUserTypes.SelectedItem != null) {
                user.UserType = (UserType)Enum.Parse(typeof(UserType), drpUserTypes.SelectedItem.ToString());
            }

            if(NewSignatureImage != null) {
                using(MemoryStream mem = new MemoryStream()) {
                    NewSignatureImage.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                    user.SignatureImage = mem.ToArray();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using(ITransaction tr = session.BeginTransaction()) {

                try {
                    if(EditingUser == null || EditingUser.Id == default(int)) {
                        EditingUser = new User();
                        EditingUser.IsTestData = (Program.LogonUser.IsTestData || Program.LogonUser.UserType == UserType.Developer);
                        EditingUser.CreatedBy = Program.LogonUser.DisplayName;
                        EditingUser.CreatedTime = DateTime.Now;
                    } else {
                        EditingUser = session.QueryOver<User>()
                            .Where(x => x.Id == EditingUser.Id)
                            .SingleOrDefault();

                        EditingUser.UpdatedTime = DateTime.Now;
                        EditingUser.UpdatedBy = Program.LogonUser.DisplayName;
                    }

                    //Verify the username is unique if it's changed.
                    if(!EditingUser.Username.Trim().ToLower().Equals(txtUsername.Text.Trim().ToLower())) {
                        JCService service = new JCService(session, Program.LogonUser);
                        if(service.UsernameExists(txtUsername.Text)) {
                            txtUsername.Focus();
                            txtUsername.SelectAll();
                            MessageBox.Show(this, "The Username has been taken.");
                            return;
                        }
                    }

                    //Verify the confirm password, if the password has been changed.
                    if(!EditingUser.Password.Equals(txtPassword.Text)) {
                        if(!txtPassword.Text.Equals(txtConfirmPassword.Text)) {
                            txtPassword.Focus();
                            txtPassword.SelectAll();
                            MessageBox.Show(this, "Confirm Password does NOT match Password.");
                            return;
                        }
                    }

                    //Update dropdown value
                    if(drpUserTitle.SelectedValue != null)
                        EditingUser.Title =
                            session.Get<UserTitle>(int.Parse(drpUserTitle.SelectedValue.ToString()));

                    updateUserInput(EditingUser);

                    if(!EditingUser.HasMandatoryValues()) {
                        MessageBox.Show(Constants.MSG_MANDATORY_FIELD_MISSING,
                            "Missing Values",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    session.Save(EditingUser);

                    tr.Commit();
                    DialogResult = DialogResult.OK;
                    this.Close();
                } catch(Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to save User. " + ex.Message);
                    Program.log.Error("Failed to save User.", ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnChangeSignature_Click(object sender, EventArgs e) {
            try {
                if(DialogResult.OK == dlgOpenFile.ShowDialog(this)) {
                    using(FileStream stream = new FileStream(dlgOpenFile.FileName, FileMode.Open)) {
                        using(BinaryReader reader = new BinaryReader(stream)) {
                            byte[] buff = new byte[stream.Length - 1];
                            reader.Read(buff, 0, buff.Length);
                            MemoryStream mem = new MemoryStream(buff);
                            Image img = Image.FromStream(mem);
                            //Resize the image if needed
                            if(img.Width > Program.config.SignatureMaxWidth ||
                                img.Height > Program.config.SignatureMaxHeight) {
                                //Determine the new size of the image
                                int newWidth = 0;
                                int newHeight = 0;
                                double imgRatio = Convert.ToDouble(img.Width) / Convert.ToDouble(img.Height);
                                double deltaWidth = Convert.ToDouble(img.Width) / Program.config.SignatureMaxWidth;
                                double deltaHeight = Convert.ToDouble(img.Height) / Program.config.SignatureMaxHeight;
                                if(deltaWidth > deltaHeight) {
                                    newWidth = Program.config.SignatureMaxWidth;
                                    newHeight = Convert.ToInt32(Convert.ToDouble(newWidth) / imgRatio);
                                } else {
                                    newHeight = Program.config.SignatureMaxHeight;
                                    newWidth = Convert.ToInt32(newHeight * imgRatio);
                                }
                                //
                                Bitmap newImage = new Bitmap(newWidth, newHeight);
                                using(Graphics gr = Graphics.FromImage(newImage)) {
                                    gr.SmoothingMode = SmoothingMode.HighQuality;
                                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    gr.DrawImage(img, new Rectangle(0, 0, newWidth, newHeight));
                                }
                                NewSignatureImage = newImage;
                            } else {
                                NewSignatureImage = img;
                            }

                            picSignature.Image = NewSignatureImage;
                        }
                    }
                }
            } catch(Exception ex) {
                NewSignatureImage = null;
                MessageBox.Show(this, "Cannot load signature image. " + ex.Message);
                Program.log.Error("Cannot load signature image.", ex);
            }

        }
    }
}
