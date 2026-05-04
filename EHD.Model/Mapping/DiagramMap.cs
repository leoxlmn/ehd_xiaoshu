using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class DiagramMap : ClassMap<Diagram> {
        public DiagramMap() {
            Id(x => x.Id).Column("DGR_DGRId")
                .GeneratedBy.HiLo("1");
            Map(x => x.DiagramName).Column("DGR_DiagramName")
                .Not.Nullable()
                .Length(50);
            Map(x => x.ImageType).Column("DGR_ImageType")
                .Not.Nullable()
                .Length(5);
            Map(x => x.ImageWidth).Column("DGR_ImageWidth")
                .Not.Nullable();
            Map(x => x.ImageHeight).Column("DGR_ImageHeight")
                .Not.Nullable();
            Map(x => x.Image).Column("DGR_Image")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("DGR_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("DGR_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("DGR_UpdatedTime");
            Map(x => x.CreatedBy).Column("DGR_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("DGR_UpdatedBy");
            Version(x => x.Version).Column("DGR_Version");
        }
    }
}
