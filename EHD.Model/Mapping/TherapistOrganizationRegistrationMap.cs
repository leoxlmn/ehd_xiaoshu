using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class TherapistOrganizationRegistrationMap : ClassMap<TherapistOrganizationRegistration> {
        public TherapistOrganizationRegistrationMap() {
            Id(x => x.Id).Column("TOR_TORId")
                .GeneratedBy.HiLo("1");
            Map(x => x.RegistrationNumber).Column("TOR_RegistrationNumber")
                .Not.Nullable()
                .Length(50);

            References(x => x.Therapist).Column("TOR_USRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.Organization).Column("TOR_ORGId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.RegistrationGroup).Column("TOR_TRGId")
                .Not.Nullable()
                .Not.LazyLoad();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("TOR_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("TOR_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("TOR_UpdatedTime");
            Map(x => x.CreatedBy).Column("TOR_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("TOR_UpdatedBy");
            Version(x => x.Version).Column("TOR_Version");
        }
    }
}
