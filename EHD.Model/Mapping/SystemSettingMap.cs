using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class SystemSettingMap : ClassMap<SystemSetting> {
        public SystemSettingMap() {
            Id(x => x.Id).Column("SET_SETId")
                .GeneratedBy.HiLo("1");
            Map(x => x.Name).Column("SET_Name")
                .Not.Nullable()
                .Length(50);
            Map(x => x.Value).Column("SET_Value")
                .Not.Nullable()
                .Length(255);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("SET_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("SET_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("SET_UpdatedTime");
            Map(x => x.CreatedBy).Column("SET_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("SET_UpdatedBy");
            Version(x => x.Version).Column("SET_Version");
        }
    }
}
