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

namespace EHD.Admin {
    public partial class ManageTemplateForm : Form {
        public ManageTemplateForm() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (grdTemplate.SelectedRows.Count > 0) {

                TemplateTreatmentDetail selectedTemplate = grdTemplate.SelectedRows[0].DataBoundItem as TemplateTreatmentDetail;

                if (DialogResult.Yes == MessageBox.Show(this, "Do you want to delete this template [" + selectedTemplate.Note + "]?",
                    "Delete Template",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)) {

                    using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                    using (ITransaction tr = session.BeginTransaction()) {
                        try {
                            TemplateTreatmentDetail template = session.Get<TemplateTreatmentDetail>(selectedTemplate.Id);
                            session.Delete(template);

                            tr.Commit();
                        } catch (Exception ex) {
                            tr.Rollback();
                            MessageBox.Show(this, "Failed to delete Template. " + ex.Message);
                            Program.log.Error("Failed to delete Template.", ex);
                        }
                    }

                    //Refresh List
                    populateTemplateList();
                }
            }
        }

        private void populateTemplateList() {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {
                    var qry = session.QueryOver<TemplateTreatmentDetail>()
                        .Fetch(x => x.TreatmentType).Eager
                        .Where(x => x.IsInitial == rdoInitial.Checked);
                    if (drpTreatmentType.SelectedValue != null) {
                        qry.Where(x => x.TreatmentType.Id == int.Parse(drpTreatmentType.SelectedValue.ToString()));
                    }

                    var templates = qry
                        .OrderBy(x => x.Note).Asc
                        .List();
                    grdTemplate.DataSource = templates;

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateTemplateList error.", ex);
                }
            }
        }

        private void populateTreatmentTypeList() {
            using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
            using (ITransaction tr = session.BeginTransaction()) {
                try {

                    IList<TherapyType> lstTreatmentType = session.QueryOver<TherapyType>()
                        .OrderBy(x => x.TherapyTypeName).Asc
                        .List<TherapyType>();

                    drpTreatmentType.ValueMember = "Id";
                    drpTreatmentType.DisplayMember = "TherapyTypeName";
                    drpTreatmentType.DataSource = lstTreatmentType;

                    tr.Commit();
                } catch (Exception ex) {
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                    Program.log.Error("populateTreatmentTypeList error.", ex);
                }
            }
        }

        private void ManageTemplateForm_Load(object sender, EventArgs e) {
            populateTreatmentTypeList();
            populateTemplateList();
        }

        private void grdTemplate_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            //Hide auditing columns
            if (grdTemplate.Columns != null) {
                if (grdTemplate.Columns.Contains("IsTestData")) grdTemplate.Columns["IsTestData"].Visible = false;
                if (grdTemplate.Columns.Contains("CreatedBy")) grdTemplate.Columns["CreatedBy"].Visible = false;
                if (grdTemplate.Columns.Contains("UpdatedBy")) grdTemplate.Columns["UpdatedBy"].Visible = false;
                if (grdTemplate.Columns.Contains("CreatedTime")) grdTemplate.Columns["CreatedTime"].Visible = false;
                if (grdTemplate.Columns.Contains("UpdatedTime")) grdTemplate.Columns["UpdatedTime"].Visible = false;
                if (grdTemplate.Columns.Contains("Version")) grdTemplate.Columns["Version"].Visible = false;
            }
        }

        private void rdoInitial_CheckedChanged(object sender, EventArgs e) {
            populateTemplateList();
        }

        private void rdoFollowup_CheckedChanged(object sender, EventArgs e) {
            populateTemplateList();
        }

        private void drpTreatmentType_SelectedIndexChanged(object sender, EventArgs e) {
            populateTemplateList();
        }

        private void btnSetDefault_Click(object sender, EventArgs e) {
            if (grdTemplate.SelectedRows.Count > 0) {

                TemplateTreatmentDetail selectedTemplate = grdTemplate.SelectedRows[0].DataBoundItem as TemplateTreatmentDetail;

                using (ISession session = SessionFactory.GetSessionFactory().OpenSession())
                using (ITransaction tr = session.BeginTransaction()) {
                    try {
                        //Get selected template
                        TemplateTreatmentDetail template = session.Get<TemplateTreatmentDetail>(selectedTemplate.Id);

                        if (!template.IsDefault) {

                            //Clear current default flags for the selected Initial/Followup and Treatment Type
                            IList<TemplateTreatmentDetail> defaultTemplates = session.QueryOver<TemplateTreatmentDetail>()
                                .Where(x => x.IsDefault == true)
                                .And(x => x.IsInitial == template.IsInitial)
                                .And(x => x.TreatmentType.Id == template.TreatmentType.Id)
                                .List<TemplateTreatmentDetail>();
                            foreach(TemplateTreatmentDetail defaultTemp in defaultTemplates) {
                                defaultTemp.IsDefault = false;
                                session.Save(defaultTemp);
                            }

                            //Set the selected template as default
                            template.IsDefault = true;
                            session.Save(template);
                        }

                        tr.Commit();
                    } catch (Exception ex) {
                        tr.Rollback();
                        MessageBox.Show(this, "Failed to delete Template. " + ex.Message);
                        Program.log.Error("Failed to delete Template.", ex);
                    }
                }
            }

            //Refresh List
            populateTemplateList();
        }
    }
}
