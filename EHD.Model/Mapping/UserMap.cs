using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class UserMap : ClassMap<User>{
        public UserMap() {
            Id(x => x.Id).Column("USR_USRId")
                .GeneratedBy.HiLo("1");

            References(x => x.Title).Column("USR_TTLId")
                .Not.LazyLoad();

            Map(x => x.Username).Column("USR_Username")
                .Not.Nullable()
                .Unique()
                .Length(20);
            Map(x => x.Password).Column("USR_Password")
                .Not.Nullable()
                .Length(20);
            Map(x => x.FirstName).Column("USR_FirstName")
                .Not.Nullable()
                .Length(50);
            Map(x => x.MiddleName).Column("USR_MiddleName")
                .Length(50);
            Map(x => x.LastName).Column("USR_LastName")
                .Not.Nullable()
                .Length(50);
            Map(x => x.GoogleCalendarId).Column("USR_GoogleCalendarId")
                .Length(50);
            Map(x => x.UserType).Column("USR_UserType")
                .Not.Nullable();
            /*
            HasManyToMany(x => x.TherapistTypes)
                .Cascade.All()
                .Table("UserTherapyType");*/
            Map(x => x.LastLoginTime).Column("USR_LastLoginTime");
            Map(x => x.SignatureImage).Column("USR_Signature");

            //EntityBase mappings
            Map(x => x.IsTestData).Column("USR_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("USR_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("USR_UpdatedTime");
            Map(x => x.CreatedBy).Column("USR_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("USR_UpdatedBy");
            Version(x => x.Version).Column("USR_Version");
        }
    }
}
