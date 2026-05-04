using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class PointOnAcupunctureFollowUpMap : ClassMap<PointOnAcupunctureFollowUp> {
        public PointOnAcupunctureFollowUpMap() {
            Id(x => x.Id).Column("PAF_PAFId")
                .GeneratedBy.HiLo("1");
            References(x => x.Diagram).Column("PAF_DGRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.AcupunctureFollowUpDetail).Column("PAF_AFDId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.X).Column("PAF_X")
                .Not.Nullable();
            Map(x => x.Y).Column("PAF_Y")
                .Not.Nullable();
            Map(x => x.PainKey).Column("PAF_PainKey")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("PAF_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("PAF_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("PAF_UpdatedTime");
            Map(x => x.CreatedBy).Column("PAF_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("PAF_UpdatedBy");
            Version(x => x.Version).Column("PAF_Version");
        }
    }
}
