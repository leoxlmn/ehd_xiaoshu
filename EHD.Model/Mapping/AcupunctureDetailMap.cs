using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class AcupunctureDetailMap : ClassMap<AcupunctureDetail> {
        public AcupunctureDetailMap() {
            Id(x => x.Id).Column("ACP_ACPId")
                .GeneratedBy.HiLo("1");
            References(x => x.InitialTreatment).Column("ACP_ITRId")
                .Not.Nullable()
                .Not.LazyLoad();

            References(x => x.TongueDiagram).Column("ACP_TongueDGRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.Pulse).Column("ACP_Pulse")
                .Nullable()
                .Length(250);
            Map(x => x.Tongue).Column("ACP_Tongue")
                .Nullable()
                .Length(250);
            Map(x => x.BloodPressure).Column("ACP_BloodPressure")
                .Nullable()
                .Length(250);
            Map(x => x.BloodSugar).Column("ACP_BloodSugar")
                .Nullable()
                .Length(250);
            Map(x => x.Subjective).Column("ACP_Subjective")
                .Nullable()
                .Length(1000);
            Map(x => x.TCMDiagnosis).Column("ACP_TCMDiagnosis")
                .Nullable()
                .Length(250);
            Map(x => x.TreatmentPlan).Column("ACP_TreatmentPlan");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("ACP_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("ACP_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("ACP_UpdatedTime");
            Map(x => x.CreatedBy).Column("ACP_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("ACP_UpdatedBy");
            Version(x => x.Version).Column("ACP_Version");
        }
    }
}
