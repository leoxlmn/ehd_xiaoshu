using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class TherapistOrganizationRegistrationGroupMap : ClassMap<TherapistOrganizationRegistrationGroup> {
        public TherapistOrganizationRegistrationGroupMap() {
            Id(x => x.Id).Column("TRG_TRGId")
                .GeneratedBy.HiLo("1");

            References(x => x.Therapist).Column("TRG_USRId")
                .Not.Nullable()
                .Not.LazyLoad();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("TRG_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("TRG_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("TRG_UpdatedTime");
            Map(x => x.CreatedBy).Column("TRG_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("TRG_UpdatedBy");
            Version(x => x.Version).Column("TRG_Version");
        }
    }
}
