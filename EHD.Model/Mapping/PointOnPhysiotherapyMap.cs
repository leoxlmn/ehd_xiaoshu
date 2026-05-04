using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PointOnPhysiotherapyMap : ClassMap<PointOnPhysiotherapy> {
        public PointOnPhysiotherapyMap() {
            Id(x => x.Id).Column("PPT_PPTId")
                .GeneratedBy.HiLo("1");
            References(x => x.Diagram).Column("PPT_DGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.PhysiotherapyDetail).Column("PPT_TRDId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.X).Column("PPT_X")
                .Not.Nullable();
            Map(x => x.Y).Column("PPT_Y")
                .Not.Nullable();
            Map(x => x.PainKey).Column("PPT_PainKey")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("PPT_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("PPT_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("PPT_UpdatedTime");
            Map(x => x.CreatedBy).Column("PPT_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("PPT_UpdatedBy");
            Version(x => x.Version).Column("PPT_Version");
        }
    }
}
