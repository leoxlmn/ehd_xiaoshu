using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PointOnMassageMap : ClassMap<PointOnMassage> {
        public PointOnMassageMap() {
            Id(x => x.Id).Column("PMG_PMGId")
                .GeneratedBy.HiLo("1");
            References(x => x.Diagram).Column("PMG_DGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.MassageDetail).Column("PMG_TRDId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.X).Column("PMG_X")
                .Not.Nullable();
            Map(x => x.Y).Column("PMG_Y")
                .Not.Nullable();
            Map(x => x.PainKey).Column("PMG_PainKey")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("PMG_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("PMG_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("PMG_UpdatedTime");
            Map(x => x.CreatedBy).Column("PMG_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("PMG_UpdatedBy");
            Version(x => x.Version).Column("PMG_Version");
        }
    }
}
