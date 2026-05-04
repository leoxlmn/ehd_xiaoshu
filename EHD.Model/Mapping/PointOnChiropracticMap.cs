using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PointOnChiropracticMap : ClassMap<PointOnChiropractic> {
        public PointOnChiropracticMap() {
            Id(x => x.Id).Column("PCH_PCHId")
                .GeneratedBy.HiLo("1");
            References(x => x.Diagram).Column("PCH_DGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.ChiropracticDetail).Column("PCH_TRDId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.X).Column("PCH_X")
                .Not.Nullable();
            Map(x => x.Y).Column("PCH_Y")
                .Not.Nullable();
            Map(x => x.PainKey).Column("PCH_PainKey")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("PCH_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("PCH_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("PCH_UpdatedTime");
            Map(x => x.CreatedBy).Column("PCH_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("PCH_UpdatedBy");
            Version(x => x.Version).Column("PCH_Version");
        }
    }
}
