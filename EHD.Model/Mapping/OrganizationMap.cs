using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class OrganizationMap : ClassMap<Organization> {
        public OrganizationMap() {
            Id(x => x.Id).Column("ORG_ORGId")
                .GeneratedBy.HiLo("1");
            Map(x => x.OrganizationName).Column("ORG_OrganizationName")
                .Not.Nullable()
                .Length(255);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("ORG_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("ORG_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("ORG_UpdatedTime");
            Map(x => x.CreatedBy).Column("ORG_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("ORG_UpdatedBy");
            Version(x => x.Version).Column("ORG_Version");
        }
    }
}
