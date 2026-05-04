using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PointOnAcupunctureMap : ClassMap<PointOnAcupuncture> {
        public PointOnAcupunctureMap() {
            Id(x => x.Id).Column("PAC_PACId")
                .GeneratedBy.HiLo("1");
            References(x => x.Diagram).Column("PAC_DGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.AcupunctureDetail).Column("PAC_TRDId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.X).Column("PAC_X")
                .Not.Nullable();
            Map(x => x.Y).Column("PAC_Y")
                .Not.Nullable();
            Map(x => x.PainKey).Column("PAC_PainKey")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("PAC_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("PAC_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("PAC_UpdatedTime");
            Map(x => x.CreatedBy).Column("PAC_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("PAC_UpdatedBy");
            Version(x => x.Version).Column("PAC_Version");
        }
    }
}
