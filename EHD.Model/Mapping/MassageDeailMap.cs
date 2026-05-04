using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class MassageDeailMap : ClassMap<MassageDetail> {

        public MassageDeailMap() {
            Id(x => x.Id).Column("MSG_MSGId")
                .GeneratedBy.HiLo("1");
            References(x => x.InitialTreatment).Column("MSG_ITRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.MassageDiagram).Column("MSG_MassageDGRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.ActivityLimitations).Column("MSG_ActivityLimitation");
            Map(x => x.TreatmentGoal).Column("MSG_TreatmentGoal");
            Map(x => x.TreatmentFocus).Column("MSG_TreatmentFocus");
            Map(x => x.TreatmentFrequency).Column("MSG_TreatmentFrequency");
            Map(x => x.TreatmentDuration).Column("MSG_TreatmentDuration");
            Map(x => x.TreatmentPlanDiscussedWithClient).Column("MSG_TreatmentPlanDiscussedWithClient");
            Map(x => x.ConsentReceived).Column("MSG_ConsentReceived");
            Map(x => x.TreatmentAreaBack).Column("MSG_TreatmentAreaBack");
            Map(x => x.TreatmentAreaNeck).Column("MSG_TreatmentAreaNeck");
            Map(x => x.TreatmentAreaShoulders).Column("MSG_TreatmentAreaShoulders");
            Map(x => x.TreatmentAreaFace).Column("MSG_TreatmentAreaFace");
            Map(x => x.TreatmentAreaLeftArm).Column("MSG_TreatmentAreaLeftArm");
            Map(x => x.TreatmentAreaRightArm).Column("MSG_TreatmentAreaRightArm");
            Map(x => x.TreatmentAreaLeftLeg).Column("MSG_TreatmentAreaLeftLeg");
            Map(x => x.TreatmentAreaRightLeg).Column("MSG_TreatmentAreaRightLeg");
            Map(x => x.TreatmentAreaGluteus).Column("MSG_TreatmentAreaGluteus");
            Map(x => x.TreatmentAreaAbdominals).Column("MSG_TreatmentAreaAbdominals");
            Map(x => x.TreatmentAreaChest).Column("MSG_TreatmentAreaChest");
            Map(x => x.TreatmentAreaBreast).Column("MSG_TreatmentAreaBreast");
            Map(x => x.TreatmentAreaOther).Column("MSG_TreatmentAreaOther");

            Map(x => x.AssessmentsPerformed).Column("MSG_AssessmentsPerformed");
            Map(x => x.ResultsOfAssessments).Column("MSG_ResultsOfAssessments");
            Map(x => x.ReassessmentSchedule).Column("MSG_ReassessmentSchedule");
            Map(x => x.Referrals).Column("MSG_Referrals");
            Map(x => x.AnticipatedProgressionOfResponses).Column("MSG_AnticipatedProgressionOfResponses");
            Map(x => x.RemedialExercisesRecommended).Column("MSG_RemedialExercisesRecommended");
            Map(x => x.Risks).Column("MSG_Risks");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("MSG_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("MSG_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("MSG_UpdatedTime");
            Map(x => x.CreatedBy).Column("MSG_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("MSG_UpdatedBy");
            Version(x => x.Version).Column("MSG_Version");
        }
    }
}
