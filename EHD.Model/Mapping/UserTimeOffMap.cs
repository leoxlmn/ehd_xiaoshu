using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class UserTimeOffMap : ClassMap<UserTimeOff> {
        public UserTimeOffMap() {
            Id(x => x.Id).Column("UTO_UTOId")
                .GeneratedBy.HiLo("1");

            References(x => x.Practitioner).Column("UTO_USRId")
                .Not.Nullable()
                .Not.LazyLoad();

            Map(x => x.StartTime).Column("UTO_StartTime")
                .Not.Nullable();
            Map(x => x.EndTime).Column("UTO_EndTime")
                .Not.Nullable();
            Map(x => x.Reason).Column("UTO_Reason")
                .Length(255);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("UTO_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("UTO_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("UTO_UpdatedTime");
            Map(x => x.CreatedBy).Column("UTO_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("UTO_UpdatedBy");
            Version(x => x.Version).Column("UTO_Version");
        }
    }
}
