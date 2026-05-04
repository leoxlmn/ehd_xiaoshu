using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PointOnChiropracticFollowUpMap : ClassMap<PointOnChiropracticFollowUp> {
        public PointOnChiropracticFollowUpMap() {
            Id(x => x.Id).Column("PCF_PCFId")
                .GeneratedBy.HiLo("1");
            References(x => x.Diagram).Column("PCF_DGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.ChiropracticFollowUpDetail).Column("PCF_FUDId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.X).Column("PCF_X")
                .Not.Nullable();
            Map(x => x.Y).Column("PCF_Y")
                .Not.Nullable();
            Map(x => x.PainKey).Column("PCF_PainKey")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("PCF_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("PCF_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("PCF_UpdatedTime");
            Map(x => x.CreatedBy).Column("PCF_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("PCF_UpdatedBy");
            Version(x => x.Version).Column("PCF_Version");
        }
    }
}
