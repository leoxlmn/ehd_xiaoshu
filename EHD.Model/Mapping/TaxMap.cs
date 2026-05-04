using EHD.Model.Entity;
using FluentNHibernate.Mapping;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace EHD.Model.Mapping {
    public class TaxMap : ClassMap<Tax> {
        public TaxMap() {
            Id(x => x.Id).Column("TAX_TAXId")
                .GeneratedBy.HiLo("1");
            Map(x => x.Rate).Column("TAX_Rate")
                .Not.Nullable();
            Map(x => x.Name).Column("TAX_Name")
                .Not.Nullable()
                .Length(5);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("TAX_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("TAX_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("TAX_UpdatedTime");
            Map(x => x.CreatedBy).Column("TAX_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("TAX_UpdatedBy");
            Version(x => x.Version).Column("TAX_Version");
        }
    }
}
