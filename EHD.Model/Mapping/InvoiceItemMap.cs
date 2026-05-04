using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class InvoiceItemMap : ClassMap<InvoiceItem> {
        public InvoiceItemMap() {
            Id(x => x.Id).Column("INI_INIId")
                .GeneratedBy.HiLo("1");
            Map(x => x.ServiceDescription).Column("INI_ServiceDescription")
                .Not.Nullable()
                .Length(100);
            Map(x => x.ServiceDate).Column("INI_ServiceDate")
                .Not.Nullable();
            Map(x => x.ServiceDuration).Column("INI_ServiceDuration");
            Map(x => x.Amount).Column("INI_Amount");

            References(x => x.Invoice).Column("INI_INVId")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("INI_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("INI_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("INI_UpdatedTime");
            Map(x => x.CreatedBy).Column("INI_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("INI_UpdatedBy");
            Version(x => x.Version).Column("INI_Version");
        }
    }
}
