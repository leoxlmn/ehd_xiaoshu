using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class BookingMap : ClassMap<Booking> {
        public BookingMap() {
            Id(x => x.Id).Column("BOK_BOKId")
                .GeneratedBy.HiLo("1");
            Map(x => x.StartTime).Column("BOK_StartTime")
                .Not.Nullable();
            Map(x => x.EndTime).Column("BOK_EndTime")
                .Not.Nullable();
            Map(x => x.Note).Column("BOK_Note")
                .Length(255);

            References(x => x.Patient).Column("BOK_PTNId")
                .Not.Nullable();
            References(x => x.Therapist).Column("BOK_USRId")
                .Not.Nullable();
            References(x => x.TreatmentType).Column("BOK_THPId")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("BOK_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("BOK_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("BOK_UpdatedTime");
            Map(x => x.CreatedBy).Column("BOK_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("BOK_UpdatedBy");
            Version(x => x.Version).Column("BOK_Version");
        }
    }
}
