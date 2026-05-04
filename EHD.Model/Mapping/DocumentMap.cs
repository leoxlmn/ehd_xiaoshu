using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class DocumentMap : ClassMap<Document> {
        public DocumentMap() {
            Id(x => x.Id).Column("DOC_DOCId")
                .GeneratedBy.HiLo("1");
            Map(x => x.DocumentCreationTime).Column("DOC_DocumentCreationTime")
                .Not.Nullable();
            Map(x => x.DocumentName).Column("DOC_DocumentName")
                .Not.Nullable()
                .Length(100);
            Map(x => x.Note).Column("DOC_Note")
                .Length(255);
            Map(x => x.DocumentBLOB).Column("DOC_Blob")
                .Not.Nullable();

            References(x => x.Patient).Column("DOC_PTNId")
                .Not.Nullable();
            References(x => x.DocumentType).Column("DOC_DTYId")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("DOC_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("DOC_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("DOC_UpdatedTime");
            Map(x => x.CreatedBy).Column("DOC_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("DOC_UpdatedBy");
            Version(x => x.Version).Column("DOC_Version");
        }
    }
}
