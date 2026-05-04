using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class InvoiceMap : ClassMap<Invoice> {
        public InvoiceMap() {
            Id(x => x.Id).Column("INV_INVId")
                .GeneratedBy.HiLo("1");
            Map(x => x.InvoiceNumber).Column("INV_InvoiceNumber")
                .Not.Nullable()
                .Length(15);
            Map(x => x.StatementDate).Column("INV_StatementDate")
                .Not.Nullable();
            Map(x => x.HSTNumber).Column("INV_HSTNumber")
                //.Not.Nullable()
                .Length(15);
            Map(x => x.Title).Column("INV_Title")
                .Not.Nullable()
                .Length(255);
            Map(x => x.Note).Column("INV_Note")
                .Not.Nullable()
                .Length(255);
            Map(x => x.TaxRate).Column("INV_TaxRate");
            Map(x => x.TaxName).Column("INV_TaxName")
                .Length(5);

            References(x => x.Patient).Column("INV_PTNId")
                .Not.Nullable();
                //.Not.LazyLoad();
            References(x => x.Therapist).Column("INV_USRId")
                .Not.Nullable();
                //.Not.LazyLoad();
            //References(x => x.RegistrationGroup).Column("INV_TRGId")
            //    .Not.Nullable();
                //.Not.LazyLoad();
            Map(x => x.TherapistRegistration).Column("INV_TherapistRegistration")
                .Not.Nullable()
                .Length(500);
            References(x => x.TreatmentType).Column("INV_THPId")
                .Not.Nullable();
                //.Not.LazyLoad();

            HasMany(x => x.InvoiceItems).KeyColumn("INI_INVId")
                .Cascade.AllDeleteOrphan()
                .Inverse();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("INV_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("INV_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("INV_UpdatedTime");
            Map(x => x.CreatedBy).Column("INV_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("INV_UpdatedBy");
            Version(x => x.Version).Column("INV_Version");
        }
    }
}
