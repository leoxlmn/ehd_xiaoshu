using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PhysiotherapyDetailMap : ClassMap<PhysiotherapyDetail> {
        public PhysiotherapyDetailMap() {
            Id(x => x.Id).Column("TRD_TRDId")
                .GeneratedBy.HiLo("1");
            References(x => x.InitialTreatment).Column("TRD_ITRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.PainScaleDiagram).Column("TRD_PainScaleDGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            /*
            HasMany(x => x.PainLocations)
                .Cascade.All()
                .Inverse();*/
            Map(x => x.PainScaleNow).Column("TRD_PainScaleNow");
            Map(x => x.PainScaleBest).Column("TRD_PainScaleBest");
            Map(x => x.PainScaleWorst).Column("TRD_PainScaleWorst");
            //Map(x => x.PainKey).Column("TRD_PainKey");
            Map(x => x.DescriptionOfSymptoms).Column("TRD_DescriptionOfSymptoms");
            Map(x => x.SymptomTrend).Column("TRD_SymptomTrend");
            Map(x => x.ConstantVsIntermittent).Column("TRD_ConstantVsIntermittent");
            Map(x => x.DayChanges).Column("TRD_DayChanges");
            Map(x => x.IrritabilityLevel).Column("TRD_IrritabilityLevel");
            Map(x => x.AggravatedBy).Column("TRD_AggravatedBy");
            Map(x => x.EasedBy).Column("TRD_EasedBy");
            Map(x => x.Occupation).Column("TRD_Occupation");
            Map(x => x.WorkStatus).Column("TRD_WorkStatus");
            Map(x => x.HistoryOfPresentingComplaint).Column("TRD_HistoryOfPresentingComplaint");
            Map(x => x.JointSoundsAndAbnormalities).Column("TRD_JointSoundsAndAbnormalities");
            Map(x => x.PastRelevantHistoryAndTreatment).Column("TRD_PastRelevantHistoryAndTreatment");
            Map(x => x.Investigations).Column("TRD_Investigations");
            Map(x => x.Medications).Column("TRD_Medications");
            Map(x => x.GeneralMedicalHistoryAndMedications).Column("TRD_GeneralMedicalHistoryAndMedications");
            Map(x => x.SocialHistoryAndRecreationalActivities).Column("TRD_SocialHistoryAndRecreationalActivities");
            Map(x => x.Goals).Column("TRD_Goals");
            Map(x => x.RedFlagsAndPrecautions).Column("TRD_RedFlagsAndPrecautions");
            Map(x => x.ObservationPostureGait).Column("TRD_ObservationPostureGait");
            Map(x => x.ActiveRangeOfMotion).Column("TRD_ActiveRangeOfMotion");
            Map(x => x.MuscleStrengthAndMyotomes).Column("TRD_MuscleStrengthAndMyotomes");
            Map(x => x.Palpation).Column("TRD_Palpation");
            References(x => x.PassiveAccessoryMovementsAndPalpationOfSpineDiagram).Column("TRD_SpineDGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            /*
            HasMany(x => x.LocationsOnSpine)
                .Cascade.All()
                .Inverse();*/
            Map(x => x.Neurological).Column("TRD_Neurological");
            Map(x => x.Reflexes).Column("TRD_Reflexes");
            Map(x => x.Sensation).Column("TRD_Sensation");
            Map(x => x.PassiveRangeOfMotion).Column("TRD_PassiveRangeOfMotion");
            Map(x => x.StabilityTestsAndSpecialTests).Column("TRD_StabilityTestsAndSpecialTests");
            Map(x => x.Analysis).Column("TRD_Analysis");
            Map(x => x.TreatmentPlan).Column("TRD_TreatmentPlan");
            Map(x => x.ConsentForTreatment).Column("TRD_ConsentForTreatment");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("TRD_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("TRD_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("TRD_UpdatedTime");
            Map(x => x.CreatedBy).Column("TRD_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("TRD_UpdatedBy");
            Version(x => x.Version).Column("TRD_Version");
        }
    }
}
