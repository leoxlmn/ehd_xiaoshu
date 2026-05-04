using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class OsteopathyDeailMap : ClassMap<OsteopathyDetail> {

        public OsteopathyDeailMap() {
            Id(x => x.Id).Column("OSP_OSPId")
                .GeneratedBy.HiLo("1");
            References(x => x.InitialTreatment).Column("OSP_ITRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.OsteopathyDiagram).Column("OSP_OsteopathyDGRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.ActivityLimitations).Column("OSP_ActivityLimitation");
            Map(x => x.TreatmentGoal).Column("OSP_TreatmentGoal");
            Map(x => x.TreatmentFocus).Column("OSP_TreatmentFocus");
            Map(x => x.TreatmentFrequency).Column("OSP_TreatmentFrequency");
            Map(x => x.TreatmentDuration).Column("OSP_TreatmentDuration");
            Map(x => x.TreatmentPlanDiscussedWithClient).Column("OSP_TreatmentPlanDiscussedWithClient");
            Map(x => x.ConsentReceived).Column("OSP_ConsentReceived");
            Map(x => x.TreatmentAreaBack).Column("OSP_TreatmentAreaBack");
            Map(x => x.TreatmentAreaNeck).Column("OSP_TreatmentAreaNeck");
            Map(x => x.TreatmentAreaShoulders).Column("OSP_TreatmentAreaShoulders");
            Map(x => x.TreatmentAreaFace).Column("OSP_TreatmentAreaFace");
            Map(x => x.TreatmentAreaLeftArm).Column("OSP_TreatmentAreaLeftArm");
            Map(x => x.TreatmentAreaRightArm).Column("OSP_TreatmentAreaRightArm");
            Map(x => x.TreatmentAreaLeftLeg).Column("OSP_TreatmentAreaLeftLeg");
            Map(x => x.TreatmentAreaRightLeg).Column("OSP_TreatmentAreaRightLeg");
            Map(x => x.TreatmentAreaGluteus).Column("OSP_TreatmentAreaGluteus");
            Map(x => x.TreatmentAreaAbdominals).Column("OSP_TreatmentAreaAbdominals");
            Map(x => x.TreatmentAreaChest).Column("OSP_TreatmentAreaChest");
            Map(x => x.TreatmentAreaBreast).Column("OSP_TreatmentAreaBreast");
            Map(x => x.TreatmentAreaOther).Column("OSP_TreatmentAreaOther");

            Map(x => x.AssessmentsPerformed).Column("OSP_AssessmentsPerformed");
            Map(x => x.ResultsOfAssessments).Column("OSP_ResultsOfAssessments");
            Map(x => x.ReassessmentSchedule).Column("OSP_ReassessmentSchedule");
            Map(x => x.Referrals).Column("OSP_Referrals");
            Map(x => x.AnticipatedProgressionOfResponses).Column("OSP_AnticipatedProgressionOfResponses");
            Map(x => x.RemedialExercisesRecommended).Column("OSP_RemedialExercisesRecommended");
            Map(x => x.Risks).Column("OSP_Risks");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("OSP_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("OSP_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("OSP_UpdatedTime");
            Map(x => x.CreatedBy).Column("OSP_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("OSP_UpdatedBy");
            Version(x => x.Version).Column("OSP_Version");
        }
    }
}
