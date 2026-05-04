using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class NaturopathicDeailMap : ClassMap<NaturopathicDetail> {

        public NaturopathicDeailMap() {
            Id(x => x.Id).Column("NPC_NPCId")
                .GeneratedBy.HiLo("1");
            References(x => x.InitialTreatment).Column("NPC_ITRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.Height).Column("NPC_Height");
            Map(x => x.Weight).Column("NPC_Weight");
            Map(x => x.Sex).Column("NPC_Sex");
            Map(x => x.Acne).Column("NPC_Acne");
            Map(x => x.Eczema).Column("NPC_Eczema");
            Map(x => x.DrySkin).Column("NPC_DrySkin");
            Map(x => x.Psoriasis).Column("NPC_Psoriasis");
            Map(x => x.NightSweats).Column("NPC_NightSweats");
            Map(x => x.PainUrinating).Column("NPC_PainUrinating");
            Map(x => x.BloodInUrine).Column("NPC_BloodInUrine");
            Map(x => x.PoorVision).Column("NPC_PoorVision");
            Map(x => x.EyeDryness).Column("NPC_EyeDryness");
            Map(x => x.EyeDischarge).Column("NPC_EyeDischarge");
            Map(x => x.Glaucoma).Column("NPC_Glaucoma");
            Map(x => x.Cataracts).Column("NPC_Cataracts");
            Map(x => x.Heartburn).Column("NPC_Heartburn");
            Map(x => x.Anxiety).Column("NPC_Anxiety");
            Map(x => x.MouthDryness).Column("NPC_MouthDryness");
            Map(x => x.Constipation).Column("NPC_Constipation");
            Map(x => x.MuscleWeakness).Column("NPC_MuscleWeakness");
            Map(x => x.RectalBleeding).Column("NPC_RectalBleeding");
            Map(x => x.SwollenNeckGlands).Column("NPC_SwollenNeckGlands");
            Map(x => x.WeightGainLoss).Column("NPC_WeightGainLoss");
            Map(x => x.NeckStiffness).Column("NPC_NeckStiffness");
            Map(x => x.SinusProblems).Column("NPC_SinusProblems");
            Map(x => x.HeartPalpitations).Column("NPC_HeartPalpitations");
            Map(x => x.AnkleSwelling).Column("NPC_AnkleSwelling");
            Map(x => x.AppetiteChagnes).Column("NPC_AppetiteChagnes");
            Map(x => x.ThirstChanges).Column("NPC_ThirstChanges");
            Map(x => x.ColdHandsFeet).Column("NPC_ColdHandsFeet");
            Map(x => x.ThyroidProblems).Column("NPC_ThyroidProblems");
            Map(x => x.Headaches).Column("NPC_Headaches");
            Map(x => x.NeckPain).Column("NPC_NeckPain");
            Map(x => x.NoseBleeds).Column("NPC_NoseBleeds");
            Map(x => x.Cough).Column("NPC_Cough");
            Map(x => x.Wheeze).Column("NPC_Wheeze");
            Map(x => x.ChestPain).Column("NPC_ChestPain");
            Map(x => x.ShortBreath).Column("NPC_ShortBreath");
            Map(x => x.Asthma).Column("NPC_Asthma");
            Map(x => x.Bronchitis).Column("NPC_Bronchitis");
            Map(x => x.Emphysema).Column("NPC_Emphysema");
            Map(x => x.BreastLump).Column("NPC_BreastLump");
            Map(x => x.BreastPain).Column("NPC_BreastPain");
            Map(x => x.ProstateSymptoms).Column("NPC_ProstateSymptoms");
            Map(x => x.Arthritis).Column("NPC_Arthritis");
            Map(x => x.HotFlushes).Column("NPC_HotFlushes");
            Map(x => x.HeavyPeriods).Column("NPC_HeavyPeriods");
            Map(x => x.EasyBruising).Column("NPC_EasyBruising");
            Map(x => x.Allergies).Column("NPC_Allergies");
            Map(x => x.Depression).Column("NPC_Depression");
            Map(x => x.HeartDisease).Column("NPC_HeartDisease");
            Map(x => x.PoorSleep).Column("NPC_PoorSleep");
            Map(x => x.Anemia).Column("NPC_Anemia");
            Map(x => x.JointPain).Column("NPC_JointPain");
            Map(x => x.Fatigue).Column("NPC_Fatigue");
            Map(x => x.Dizziness).Column("NPC_Dizziness");
            Map(x => x.Seizures).Column("NPC_Seizures");
            Map(x => x.PMS).Column("NPC_PMS");
            Map(x => x.Diarrhea).Column("NPC_Diarrhea");
            Map(x => x.SeenNaturopathicDoctorBefore).Column("NPC_SeenNaturopathicDoctorBefore");
            Map(x => x.SeenNaturopathicDoctorDate).Column("NPC_SeenNaturopathicDoctorDate");
            Map(x => x.VisitPurposeGeneral).Column("NPC_VisitPurposeGeneral");
            Map(x => x.VisitPurposeSpecificConcern).Column("NPC_VisitPurposeSpecificConcern");
            Map(x => x.VisitPurposeSpecificConcernDescription).Column("NPC_VisitPurposeSpecificConcernDescription");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("NPC_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("NPC_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("NPC_UpdatedTime");
            Map(x => x.CreatedBy).Column("NPC_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("NPC_UpdatedBy");
            Version(x => x.Version).Column("NPC_Version");
        }
    }
}
