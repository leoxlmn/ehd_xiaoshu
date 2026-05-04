using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class AcupunctureFollowUpDetailMap : ClassMap<AcupunctureFollowUpDetail> {
        public AcupunctureFollowUpDetailMap() {
            Id(x => x.Id).Column("AFD_AFDId")
                .GeneratedBy.HiLo("1");
            References(x => x.FollowUpTreatment).Column("AFD_FTRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.TongueDiagram).Column("AFD_TongueDGRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.Pulse).Column("AFD_Pulse")
                .Nullable()
                .Length(250);
            Map(x => x.Tongue).Column("AFD_Tongue")
                .Nullable()
                .Length(250);
            Map(x => x.BloodPressure).Column("AFD_BloodPressure")
                .Nullable()
                .Length(250);
            Map(x => x.BloodSugar).Column("AFD_BloodSugar")
                .Nullable()
                .Length(250);
            Map(x => x.Subjective).Column("AFD_Subjective")
                .Nullable()
                .Length(1000);
            Map(x => x.TCMDiagnosis).Column("AFD_TCMDiagnosis")
                .Nullable()
                .Length(250);
            Map(x => x.TreatmentPlan).Column("AFD_TreatmentPlan");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("AFD_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("AFD_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("AFD_UpdatedTime");
            Map(x => x.CreatedBy).Column("AFD_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("AFD_UpdatedBy");
            Version(x => x.Version).Column("AFD_Version");
        }
    }
}
