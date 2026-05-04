using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class ChiropracticFollowUpDetailMap : ClassMap<ChiropracticFollowUpDetail> {
        public ChiropracticFollowUpDetailMap() {
            Id(x => x.Id).Column("CFD_CFDId")
                .GeneratedBy.HiLo("1");
            References(x => x.FollowUpTreatment).Column("CFD_FTRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.ChiropracticSOAPDiagram).Column("CFD_ChiropracticFollowUpDGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.VAS).Column("CFD_VAS");
            Map(x => x.ConditionChange).Column("CFD_ConditionChange");
            Map(x => x.ConditionChangeNote).Column("CFD_ConditionChangeNote");
            Map(x => x.O).Column("CFD_O");
            Map(x => x.A).Column("CFD_A");
            Map(x => x.OsseousManipulation).Column("CFD_OsseousManipulation");
            Map(x => x.Massage).Column("CFD_Massage");
            Map(x => x.Stretch).Column("CFD_Stretch");
            Map(x => x.TriggerPointTherapy).Column("CFD_TriggerPointTherapy");
            Map(x => x.Traction).Column("CFD_Traction");
            Map(x => x.TheraputicExercise).Column("CFD_TheraputicExercise");
            Map(x => x.ExerciseNote).Column("CFD_ExerciseNote");
            Map(x => x.Heat).Column("CFD_Heat");
            Map(x => x.Cold).Column("CFD_Cold");
            Map(x => x.UltraSound).Column("CFD_UltraSound");
            Map(x => x.UltraSoundValue).Column("CFD_UltraSoundValue");
            Map(x => x.Electrotherapy).Column("CFD_Electrotherapy");
            Map(x => x.ElectrotherapyNote).Column("CFD_ElectrotherapyNote");
            Map(x => x.NoDxChange).Column("CFD_NoDxChange");
            Map(x => x.NewDiagnosis).Column("CFD_NewDiagnosis");
            Map(x => x.NewDiagnosisNote).Column("CFD_NewDiagnosisNote");
            Map(x => x.PTRDays).Column("CFD_PTRDays");
            Map(x => x.PTRWeeks).Column("CFD_PTRWeeks");
            Map(x => x.PTRMonths).Column("CFD_PTRMonths");
            Map(x => x.PRN).Column("CFD_PRN");
            Map(x => x.Referral).Column("CFD_Referral");
            Map(x => x.ReferralNote).Column("CFD_ReferralNote");
            Map(x => x.HomeCare).Column("CFD_HomeCare");
            Map(x => x.Comments).Column("CFD_Comments");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("CFD_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("CFD_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("CFD_UpdatedTime");
            Map(x => x.CreatedBy).Column("CFD_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("CFD_UpdatedBy");
            Version(x => x.Version).Column("CFD_Version");

        }
    }
}
