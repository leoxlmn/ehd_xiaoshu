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
using NHibernate.Transform;

namespace EHD.Admin {
    public partial class EditPatientForm : Form {

        #region Private Members
        private bool isSearching = false;
        private ISession session = null;
        #endregion Private Members

        public Patient EditingPatient { get; set; }
        private int editingPatientId = 0;

        public EditPatientForm(int patientId = 0, bool viewOnly = false) {

            InitializeComponent();

            editingPatientId = patientId;

            if (viewOnly) {
                disableFormFields();
            }
        }

        private void populateInsurerList() {

            try {
                IList<Insurer> lstInsurer = null;

                using (ITransaction tr = session.BeginTransaction()) {

                    lstInsurer = session.QueryOver<Insurer>()
                        .OrderBy(x=>x.InsurerName).Asc
                        .Take(Program.config.MaxInsurers)
                        .List<Insurer>();

                    tr.Commit();
                }

                //Add an empty Insurer to the dropdown
                lstInsurer.Insert(0, new Insurer());
                drpInsurer.DataSource = lstInsurer;
                drpInsurer.ValueMember = "Id";
                drpInsurer.DisplayMember = "InsurerName";

            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Insurer List. " + ex.Message);
                Program.log.Error("Failed to load Insurer List.", ex);
            } finally {
            }
        }

        private void disableFormFields() {

            //Set controls to be readonly
            foreach(Control ctr in Controls) {
                if(ctr is TextBox) {
                    ((TextBox)ctr).ReadOnly = true;
                } else if(ctr is DateTimePicker) {
                    //Replace with readonly Textboxs
                    TextBox tb = new TextBox();
                    tb.Text = ((DateTimePicker)ctr).Value.ToString(Constants.DATE_FORMAT);
                    tb.ReadOnly = true;
                    ctr.Parent.Controls.Add(tb);
                    tb.Location = ctr.Location;
                    tb.Size = ctr.Size;

                    ctr.Parent.Controls.Remove(ctr);
                } else if(ctr is RadioButton) {
                    ((RadioButton)ctr).AutoCheck = false;
                }
            }

            //Hide buttons
            btnSave.Visible = false;
            btnAutoGenerateFileNumber.Visible = false;
            //Disable insurer link
            lnkInsurer.Enabled = false;
        }

        private void loadPatient() {

            try {
                using (ITransaction tr = session.BeginTransaction()) {

                    if (editingPatientId == 0) {
                        EditingPatient = new Patient();
                        EditingPatient.CreatedBy = Program.LogonUser.DisplayName;
                        EditingPatient.CreatedTime = DateTime.Now;
                        btnAutoGenerateFileNumber.Visible = true;
                    } else {
                        EditingPatient = session.Get<Patient>(editingPatientId);
                        btnAutoGenerateFileNumber.Visible = false;
                    }

                    populatePatientDetail(EditingPatient);

                    if (EditingPatient.FamilyPatientId.HasValue) {
                        Patient primaryPatient = session.Get<Patient>(EditingPatient.FamilyPatientId.Value);
                        if(primaryPatient != null){
                            ucFamilyPrimaryMember.SelectedItem = new SimpleListItem {
                                Id = primaryPatient.Id,
                                Text = primaryPatient.DisplayName
                            };
                        }
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Patient. " + ex.Message);
                Program.log.Error("Failed to load Patient.", ex);
            } finally {
            }
        }

        private void populatePatientDetail(Patient patient) {
            if(patient != null) {
                txtFileNumber.Text = patient.FileNumber;
                txtFirstName.Text = patient.FirstName;
                txtMiddleName.Text = patient.MiddleName;
                txtLastName.Text = patient.LastName;
                dtDateOfBirth.Value = patient.DateOfBirth.Date;
                txtAge.Text = patient.Age.ToString();
                rdoMale.Checked = (patient.Sex == Sex.Male);
                rdoFemale.Checked = (patient.Sex == Sex.Female);
                txtHomePhone.Text = patient.ContactInfo.HomePhone;
                txtCellPhone.Text = patient.ContactInfo.CellPhone;
                txtFax.Text = patient.ContactInfo.HomeFax;
                txtEmail.Text = patient.ContactInfo.Email;
                txtAddress1.Text = patient.ContactInfo.Address.AddressLine1;
                txtAddress2.Text = patient.ContactInfo.Address.AddressLine2;
                txtPostalCode.Text = patient.ContactInfo.Address.PostalCode;
                txtNote.Text = patient.Note;
                setInsurer(patient.Insurer);

                txtId.Text = patient.Id.ToString();
                txtVersion.Text = patient.Version.ToString();
                txtCreatedBy.Text = patient.CreatedBy;
                txtCreatedTime.Text = patient.CreatedTime != null && patient.CreatedTime != default(DateTime) ?
                    patient.CreatedTime.ToString(Constants.DATE_TIME_FORMAT) : string.Empty;
                txtUpdatedBy.Text = patient.UpdatedBy;
                txtUpdatedTime.Text = patient.UpdatedTime.HasValue ? patient.UpdatedTime.Value.ToString(Constants.DATE_TIME_FORMAT) : string.Empty;

            }
        }

        private void setInsurer(Insurer insurer) {
            if(insurer == null) return;

            drpInsurer.SelectedValue = insurer.Id;
        }

        private void updatePatientDetailInput() {
            EditingPatient.FileNumber = txtFileNumber.Text;
            EditingPatient.FirstName = txtFirstName.Text.Trim();
            EditingPatient.MiddleName = txtMiddleName.Text.Trim();
            EditingPatient.LastName = txtLastName.Text.Trim();
            EditingPatient.DateOfBirth = dtDateOfBirth.Value.Date;
            uint age = 0;
            uint.TryParse(txtAge.Text, out age);
            EditingPatient.Sex = rdoMale.Checked ? Sex.Male : rdoFemale.Checked ? Sex.Female : Sex.Unknown;
            EditingPatient.ContactInfo = new ContactInfo() {
                Address = new Address() {
                    AddressLine1 = txtAddress1.Text.Trim(),
                    AddressLine2 = txtAddress2.Text.Trim(),
                    PostalCode = txtPostalCode.Text.Trim()
                },
                HomePhone = txtHomePhone.Text.Trim(),
                CellPhone = txtCellPhone.Text.Trim(),
                HomeFax = txtFax.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
            EditingPatient.Note = txtNote.Text;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {

            if(EditingPatient != null) {
                try {

                    using (ITransaction tr = session.BeginTransaction()) {

                        //Determine if it's needed to check the name duplication
                        bool toCheckName;

                        if (EditingPatient == null || EditingPatient.Id == default(int)) {
                            EditingPatient = new Patient();
                            EditingPatient.IsTestData = Program.LogonUser.IsTestData;
                            EditingPatient.CreatedBy = Program.LogonUser.DisplayName;
                            EditingPatient.CreatedTime = DateTime.Now;

                            //always check new patient's name
                            toCheckName = true;
                        } else {
                            EditingPatient = session.Get<Patient>(EditingPatient.Id);
                            //EditingPatient.UpdatedTime = DateTime.Now;
                            //EditingPatient.UpdatedBy = Program.LogonUser.DisplayName;

                            //only check name if it's been updated
                            toCheckName = (
                                !EditingPatient.FirstName.Trim().ToLower().Equals(txtFirstName.Text.Trim().ToLower()) ||
                                !EditingPatient.LastName.Trim().ToLower().Equals(txtLastName.Text.Trim().ToLower()));
                        }

                        //For new patient, the file number must start with 1 character and followed by 3 or more digits number
                        if (EditingPatient.Id == default(int)) {
                            //Remove spaces
                            txtFileNumber.Text = txtFileNumber.Text.Trim();
                            //at lease 4 digits
                            if (txtFileNumber.Text.Trim().Length < 4) {
                                MessageBox.Show(this, "The file number you entered is too short.");
                                txtFileNumber.SelectAll();
                                txtFileNumber.Focus();
                                tr.Rollback();
                                return;
                            }
                            //First digit must be A-Z. The second to the 4th digits must be numbers
                            char[] chrs = txtFileNumber.Text.Trim().ToUpper().ToCharArray();
                            if (chrs[0] < 'A' || chrs[0] > 'Z') {
                                MessageBox.Show(this, "The file number has to start with A to Z and followed by a number at least 3 digits long.");
                                txtFileNumber.SelectAll();
                                txtFileNumber.Focus();
                                tr.Rollback();
                                return;
                            }
                            if (chrs[1] < '0' || chrs[1] > '9' ||
                                chrs[2] < '0' || chrs[2] > '9' ||
                                chrs[3] < '0' || chrs[3] > '9') {
                                MessageBox.Show(this, "The file number has to start with A to Z and followed by a number at least 3 digits long.");
                                txtFileNumber.SelectAll();
                                txtFileNumber.Focus();
                                tr.Rollback();
                                return;
                            }
                        }

                        //Verify the File Number is unique
                        JCService service = new JCService(session, Program.LogonUser);
                        if (!service.isFileNumberUnique(EditingPatient.Id, txtFileNumber.Text)) {
                            MessageBox.Show(this, "The file number you entered has been taken.");
                            txtFileNumber.SelectAll();
                            txtFileNumber.Focus();
                            tr.Rollback();
                            return;
                        }

                        //update with dropdown selection
                        if (drpInsurer.SelectedValue != null && (int)drpInsurer.SelectedValue != 0) {
                            EditingPatient.Insurer =
                                session.Load<Insurer>(int.Parse(drpInsurer.SelectedValue.ToString()));
                        } else {
                            EditingPatient.Insurer = null;
                        }

                        //Check family primary member id
                        if (ucFamilyPrimaryMember.SelectedItem != null) {

                            //Should not point to her/himself as the primary family member. Leave it empty if it's the case.
                            if(EditingPatient.Id == ucFamilyPrimaryMember.SelectedItem.Id){
                                MessageBox.Show(this,
                                    "You cannot set the same person as the Family Primary Member.\nIf s/he is the Primary Member of her/his family, leave this field empty.",
                                    "Invalid Family Primary Member",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                ucFamilyPrimaryMember.SelectedItem = null;
                                ucFamilyPrimaryMember.Focus();
                                tr.Rollback();
                                return;
                            }

                            //Check if this person has been linked as a primary family member
                            var memberCount = session.QueryOver<Patient>()
                                .Where(x => x.FamilyPatientId == EditingPatient.Id)
                                .And(x => x.FamilyPatientId != default(int))
                                .RowCount();
                            if (memberCount > 0) {
                                MessageBox.Show(this,
                                    string.Format("This person has {0} family members. You cannot assign her/him to another family!", memberCount),
                                    "Invalid Family Primary Member",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                                ucFamilyPrimaryMember.SelectedItem = null;
                                ucFamilyPrimaryMember.Focus();
                                tr.Rollback();
                                return;
                            }

                            //Check if selected primary member belongs to another family, if yes, get his/her primary family member id.
                            var primMember = session.Get<Patient>(ucFamilyPrimaryMember.SelectedItem.Id);
                            if (primMember.FamilyPatientId.HasValue && primMember.FamilyPatientId != default(int)) {
                                EditingPatient.FamilyPatientId = primMember.FamilyPatientId;
                            } else {
                                EditingPatient.FamilyPatientId = ucFamilyPrimaryMember.SelectedItem.Id;
                            }
                            
                        } else {
                            EditingPatient.FamilyPatientId = null;
                        }

                        //Check name duplication if needed and warn if the duplicated names have been found
                        if (toCheckName) {
                            if (!service.isPatientNameUnique(EditingPatient.Id, txtFirstName.Text.Trim(), txtLastName.Text.Trim())) {
                                if (DialogResult.OK != MessageBox.Show(this,
                                        "There is a patient in the system having the same name!\n\nPress [OK] to confirm to enter a new patient with the same name.\nPress [Cancel] to cancel.",
                                        "Patient name exists",
                                        MessageBoxButtons.OKCancel)) {
                                    txtFirstName.SelectAll();
                                    txtFirstName.Focus();
                                    tr.Rollback();
                                    return;
                                }
                            }
                        }

                        //Update Patient entity
                        if (EditingPatient != null && EditingPatient.Id != default(int)) {
                            EditingPatient.UpdatedTime = DateTime.Now;
                            EditingPatient.UpdatedBy = Program.LogonUser.DisplayName;
                        }
                        updatePatientDetailInput();

                        if (!EditingPatient.HasMandatoryValues()) {
                            MessageBox.Show(this,
                                Constants.MSG_MANDATORY_FIELD_MISSING,
                                "Missing Values",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            tr.Rollback();
                            return;
                        }

                        session.Save(EditingPatient);

                        //Update NextFileNumber once Patient has been saved
                        service.UpdateNextFileNumber(EditingPatient.FileNumber);

                        tr.Commit();
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to save Patient. " + ex.Message);
                    Program.log.Error("Failed to save Patient.", ex);
                } finally {
                }
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e) {
            if( e.KeyChar != Convert.ToChar(8) && //Backspace
                (e.KeyChar < '0' || e.KeyChar > '9')) {
                //Cancel the keyPress event
                e.Handled = true;
                return;
            }
        }

        private void txtAge_TextChanged(object sender, EventArgs e) {
            if(txtAge.Text.Length <= 0) return;

            uint age = 0;
            if(!uint.TryParse(txtAge.Text, out age) || age >= 200) {
                txtAge.SelectAll();
                MessageBox.Show(this, "Age is incorrect!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void lnkInsurer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            (new ManageInsurerForm()).ShowDialog(this);

            populateInsurerList();
            setInsurer(EditingPatient.Insurer);
        }

        private void txtLastName_TextChanged(object sender, EventArgs e) {
            //Generate a new file number only when the user types in the first character
            if (txtLastName.Text.Trim().Length == 1) {
                autoGenerateFileNumber();
            }
        }

        private void btnAutoGenerateFileNumber_Click(object sender, EventArgs e) {
            autoGenerateFileNumber();
        }

        private void autoGenerateFileNumber() {

            if(txtLastName.Text.Trim().Length <= 0) return;

            if(EditingPatient == null || EditingPatient.Id == default(int)) {
                try {

                    using (ITransaction tr = session.BeginTransaction()) {
                        JCService service = new JCService(session, Program.LogonUser);
                        txtFileNumber.Text = service.GetNextFileNumber(txtLastName.Text.Trim());

                        tr.Commit();
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, "Failed to load Patient. " + ex.Message);
                    Program.log.Error("Failed to load Patient.", ex);
                } finally {
                }
            }
        }

        private void EditPatientForm_FormClosed(object sender, FormClosedEventArgs e) {
            //Close session
            if (session != null) session.Dispose();
        }

        private void ucFamilyPrimaryMember_MinInputLengthReached(object sender, EventArgs e) {

            if (isSearching) return;

            isSearching = true;

            //Search patients
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    //Search top 20 matches
                    SimpleListItem alias = null;
                    var patients = session.QueryOver<Patient>()
                        .Fetch(x => x.Insurer).Eager
                        .Where(Restrictions.Disjunction()
                            .Add(Restrictions.Eq(Projections.Property<Patient>(x => x.FileNumber), ucFamilyPrimaryMember.InputText))
                            .Add(Restrictions.On<Patient>(x => x.FirstName).IsInsensitiveLike(ucFamilyPrimaryMember.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.LastName).IsInsensitiveLike(ucFamilyPrimaryMember.InputText + "%"))
                            .Add(Restrictions.On<Patient>(x => x.ContactInfo.Address.AddressLine1).IsInsensitiveLike(ucFamilyPrimaryMember.InputText + "%"))
                            )
                        .SelectList(list => list
                            .Select(x => x.Id).WithAlias(() => alias.Id)
                            .Select(x => Projections.Concat(x.FileNumber, " - ", x.FirstName, " ", x.LastName, " [", x.ContactInfo.Address.AddressLine1, " ", x.ContactInfo.Address.AddressLine2, "]")).WithAlias(() => alias.Text)
                            )
                        .TransformUsing(Transformers.AliasToBean<SimpleListItem>())
                        .OrderBy(x => x.FirstName).Asc
                        .Take(20)
                        .List<SimpleListItem>();

                    ucFamilyPrimaryMember.UpdateList(patients);

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(this, "Failed to search patient. " + ex.Message);
                    Program.log.Error("Failed to search patient.", ex);
                } finally {
                    isSearching = false;
                }
            }

        }

        private void EditPatientForm_Load(object sender, EventArgs e) {

            //Create key for session access
            if (session == null) session = SessionFactory.GetSessionFactory().OpenSession();

            //Populate dropdownlists
            populateInsurerList();

            //Populate Patient info
            loadPatient();
        }

        /// <summary>
        /// Display all family members
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkAllMembers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {

            if (ucFamilyPrimaryMember.SelectedItem == null &&
                (EditingPatient == null || EditingPatient.Id == default(int))) return;

            int primaryFamilyMemberId = ucFamilyPrimaryMember.SelectedItem != null ? ucFamilyPrimaryMember.SelectedItem.Id : EditingPatient.Id;

            try {
                using (ITransaction tr = session.BeginTransaction()) {
                    var members = session.QueryOver<Patient>()
                        .Where(Restrictions.Or(
                            Restrictions.Where<Patient>(x => x.FamilyPatientId == primaryFamilyMemberId),
                            Restrictions.Where<Patient>(x =>x.Id == primaryFamilyMemberId)
                            ))
                        .OrderBy(x => x.FamilyPatientId).Asc
                        .ThenBy(x => x.DateOfBirth).Asc
                        .ThenBy(x => x.FirstName).Asc
                        .List<Patient>();

                    if (members != null && members.Count > 0) {
                        StringBuilder sb = new StringBuilder();
                        foreach (Patient p in members) {
                            sb.Append(p.FileNumber);
                            sb.Append(" - ");
                            sb.Append(p.FirstName);
                            if (p.MiddleName != null && p.MiddleName.Length > 0) {
                                sb.Append(" ");
                                sb.Append(p.MiddleName);
                            }
                            sb.Append(" ");
                            sb.Append(p.LastName);

                            if (p.Id == primaryFamilyMemberId) {
                                sb.AppendLine(" (Family Primary Member) ");
                            } else {
                                sb.AppendLine();
                            }
                        }
                        MessageBox.Show(this, sb.ToString());
                    }

                    tr.Commit();
                }
            } catch (Exception ex) {
                MessageBox.Show(this, "Failed to load Patient. " + ex.Message);
                Program.log.Error("Failed to load Patient.", ex);
            }
        }

        private void btnSearchFamilyFromAddress_Click(object sender, EventArgs e) {
            if (txtAddress1.Text.Trim().Length > 0) {
                ucFamilyPrimaryMember.InputText = txtAddress1.Text.Trim();
            }
        }
    }
}
