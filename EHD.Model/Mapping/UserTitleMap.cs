using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class UserTitleMap : ClassMap<UserTitle> {
        public UserTitleMap() {
            Id(x => x.Id).Column("TTL_TTLId")
                .GeneratedBy.HiLo("1");
            Map(x => x.Name).Column("TTL_Name")
                .Not.Nullable()
                .Length(10);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("TTL_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("TTL_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("TTL_UpdatedTime");
            Map(x => x.CreatedBy).Column("TTL_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("TTL_UpdatedBy");
            Version(x => x.Version).Column("TTL_Version");
        }
    }
}
