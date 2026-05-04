using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class UserWeeklyAvailabilityMap : ClassMap<UserWeeklyAvailability> {
        public UserWeeklyAvailabilityMap() {
            Id(x => x.Id).Column("UWA_UWAId")
                .GeneratedBy.HiLo("1");

            References(x => x.Practitioner).Column("UWA_USRId")
                .Not.Nullable()
                .Not.LazyLoad();
            
            Map(x => x.WeekDay).Column("UWA_WeekDay")
                .Not.Nullable();
            Map(x => x.FullDayAvailable).Column("UWA_FullDayAvailable")
                .Not.Nullable();
            Map(x => x.StartHour).Column("UWA_StartHour")
                .Not.Nullable();
            Map(x => x.EndHour).Column("UWA_EndHour")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("UWA_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("UWA_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("UWA_UpdatedTime");
            Map(x => x.CreatedBy).Column("UWA_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("UWA_UpdatedBy");
            Version(x => x.Version).Column("UWA_Version");
        }
    }
}
