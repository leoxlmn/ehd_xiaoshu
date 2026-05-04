using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PointOnOsteopathyMap : ClassMap<PointOnOsteopathy> {
        public PointOnOsteopathyMap() {
            Id(x => x.Id).Column("POS_POSId")
                .GeneratedBy.HiLo("1");
            References(x => x.Diagram).Column("POS_DGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.OsteopathyDetail).Column("POS_TRDId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.X).Column("POS_X")
                .Not.Nullable();
            Map(x => x.Y).Column("POS_Y")
                .Not.Nullable();
            Map(x => x.PainKey).Column("POS_PainKey")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("POS_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("POS_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("POS_UpdatedTime");
            Map(x => x.CreatedBy).Column("POS_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("POS_UpdatedBy");
            Version(x => x.Version).Column("POS_Version");
        }
    }
}
