using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class InvoiceNoteReferenceMap : ClassMap<InvoiceNoteReference> {
        public InvoiceNoteReferenceMap() {
            Id(x => x.Id).Column("INO_INOId")
                .GeneratedBy.HiLo("1");
            Map(x => x.Text).Column("INO_Text")
                .Not.Nullable()
                .Length(255);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("INO_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("INO_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("INO_UpdatedTime");
            Map(x => x.CreatedBy).Column("INO_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("INO_UpdatedBy");
            Version(x => x.Version).Column("INO_Version");
        }
    }
}
