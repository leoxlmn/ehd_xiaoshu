using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class InsurerMap : ClassMap<Insurer> {
        public InsurerMap() {
            Id(x => x.Id).Column("INS_INSId")
                .GeneratedBy.HiLo("1");
            Map(x => x.InsurerName).Column("INS_InsurerName")
                .Not.Nullable()
                .Length(100);
            /*
            Map(x => x.WarnTreatmentPerDay).Column("INS_WarnTreatmentPerDay")
                .Not.Nullable();
            Map(x => x.MaxTreatmentPerDay).Column("INS_MaxTreatmentPerDay")
                .Not.Nullable();*/
            Map(x => x.Comment).Column("INS_Comment")
                .Length(255);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("INS_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("INS_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("INS_UpdatedTime");
            Map(x => x.CreatedBy).Column("INS_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("INS_UpdatedBy");
            Version(x => x.Version).Column("INS_Version");
        }
    }
}
