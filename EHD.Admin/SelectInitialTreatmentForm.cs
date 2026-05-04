using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using EHD.Login;
using Utility;
using NHibernate;
using EHD.Repository;
using EHD.Model.Entity;
using EHD.Model.DTO;
using EHD.Constant;
using EHD.Service;
using System.IO;
using System.Reflection;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace EHD.Admin {
    public partial class SelectInitialTreatmentForm : Form {
        
        public int PatientId { get; set; }
        public int TreatmentTypeId { get; set; }
        public DateTime TreatmentDate { get; set; }
        public int SelectedInitialTreatmentId { get; set; }

        public SelectInitialTreatmentForm(int patientId, int treatmentTypeId, DateTime treatmentDate) {
            InitializeComponent();

            PatientId = patientId;
            TreatmentTypeId = treatmentTypeId;
            TreatmentDate = treatmentDate;
        }

        private void SelectInitialTreatmentForm_Load(object sender, EventArgs e) {
            showList();

            if (grdInitialTreatment.RowCount <= 0) {
                MessageBox.Show(this,
                    string.Format("There is no Initial Treatment found for this patient.\nPlease create a new Initial Treatment first."));

                DialogResult = System.Windows.Forms.DialogResult.Retry;//Use DialogResult.Retry to indicate an Initial Treatment is needed to create.
                this.Close();
            }
        }

        private void showList() {
            //Clear list first
            grdInitialTreatment.DataSource = new List<InitialTreatmentDTO>();

            if(PatientId != 0 && TreatmentTypeId != 0) {
                using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                using(ITransaction tr = session.BeginTransaction()) {

                    try {
                        /*
                        var qryInit = session.CreateCriteria<InitialTreatment>("init")
                            .CreateCriteria("Patient", "p")
                            .Add(Restrictions.Eq("p.Id", Patient.Id))
                            .CreateCriteria("init.TreatmentType", "type")
                            .Add(Restrictions.Eq("type.Id", TreatmentType.Id))
                            .Future<InitialTreatment>();

                        IList<InitialTreatment> lstInitTreatment = qryInit.ToList();
                        grdInitialTreatment.DataSource = lstInitTreatment;
                        */

                        InitialTreatment aIT = null;
                        TherapyType aType = null;
                        User aUser = null;
                        UserTitle aTitle = null;
                        InitialTreatmentDTO dtoIT = null;

                        var lstInitTreatment = session.QueryOver<InitialTreatment>(() => aIT)
                            .JoinQueryOver<User>(() => aIT.Therapist, () => aUser, JoinType.InnerJoin)
                            .JoinQueryOver<TherapyType>(() => aIT.TreatmentType, () => aType, JoinType.InnerJoin)
                            .JoinQueryOver<UserTitle>(() => aUser.Title, () => aTitle, JoinType.LeftOuterJoin)
                            .SelectList(list => list
                                .Select(() => aIT.Id).WithAlias(() => dtoIT.Id)
                                .Select(() => aIT.Version).WithAlias(() => dtoIT.Version)
                                .Select(() => aIT.Patient.Id).WithAlias(() => dtoIT.PatientId)
                                .Select(() => aType.TherapyTypeName).WithAlias(() => dtoIT.TreatmentType)
                                .Select(() => aUser.FirstName).WithAlias(() => dtoIT.TherapistFirstName)
                                .Select(() => aUser.LastName).WithAlias(() => dtoIT.TherapistLastName)
                                .Select(() => aUser.MiddleName).WithAlias(() => dtoIT.TherapistMiddleName)
                                .Select(() => aTitle.Name).WithAlias(() => dtoIT.TherapistTitle)
                                .Select(() => aIT.TreatmentTime).WithAlias(() => dtoIT.TreatmentTime)
                                .Select(() => aIT.TreatmentDurationMinutes).WithAlias(() => dtoIT.Duration)
                                .Select(() => aIT.OpenToTherapist).WithAlias(() => dtoIT.IsOpenToTherapist)
                                .Select(() => aIT.IsComplete).WithAlias(() => dtoIT.IsCompleted)
                            )
                            .TransformUsing(Transformers.AliasToBean<InitialTreatmentDTO>())
                            .Where(() => aIT.Patient.Id == PatientId)
                            .Where(() => aType.Id == TreatmentTypeId)
                            .Take(Program.config.MaxInitialTreatmentsPerPatient)
                            .List<InitialTreatmentDTO>();

                        grdInitialTreatment.DataSource = lstInitTreatment;

                        tr.Commit();
                    } catch(Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, ex.Message);
                        Program.log.Error("Failed to retrieve Initial Treatments.", ex);
                    }
                }
            }
        }

        private void grdInitialTreatment_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            if(e.ColumnIndex >= 0 && e.RowIndex >= 0) {
                SelectedInitialTreatmentId = (grdInitialTreatment.Rows[e.RowIndex].DataBoundItem as InitialTreatmentDTO).Id;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        /*
        private void btnAddInitTreatment_Click(object sender, EventArgs e) {
            InitialTreatment newIt = new InitialTreatment();
            newIt.Patient = Patient;
            newIt.Therapist = Practitioner;
            newIt.TreatmentType = TreatmentType;
            newIt.TreatmentTime = TreatmentDate;

            if(DialogResult.OK == (new EditInitialTreatmentForm(newIt)).ShowDialog(this)) {
                showList();
            }
        }

        private void btnDeleteInitialTreatment_Click(object sender, EventArgs e) {
            if(grdInitialTreatment.SelectedRows.Count > 0) {
                
                InitialTreatment it = grdInitialTreatment.SelectedRows[0].DataBoundItem as InitialTreatment;

                if(DialogResult.Yes == MessageBox.Show(this, "Do you want to DELETE the Initial Treatment [" + it.DisplayName + "]?",
                    "Delete Initial Treatment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning)) {

                    using(ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using(ITransaction tr = session.BeginTransaction()) {

                        try {
                            //Delete
                            JCService service = new JCService(session, Program.LogonUser);
                            service.DeleteInitialTreatment(it);

                            //Commit
                            tr.Commit();
                        } catch(Exception ex) {
                            MessageBox.Show(this, "Failed to delete Initial Treatment. " + ex.Message);
                            Program.log.Error("Failed to delete Initial Treatment.", ex);
                        }
                    }

                    showList();
                }

            }
        }

        private void btnEditInitialTreatment_Click(object sender, EventArgs e) {
            if(grdInitialTreatment.SelectedRows.Count > 0) {

                InitialTreatment it = grdInitialTreatment.SelectedRows[0].DataBoundItem as InitialTreatment;

                if(DialogResult.OK == (new EditInitialTreatmentForm(it)).ShowDialog(this)) {
                    showList();
                }
            }
        }
        */
    }
}
