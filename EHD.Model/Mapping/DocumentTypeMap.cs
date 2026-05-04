using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class DocumentTypeMap : ClassMap<DocumentType> {
        public DocumentTypeMap() {
            Id(x => x.Id).Column("DTY_DTYId")
                .GeneratedBy.HiLo("1");
            Map(x => x.DocumentTypeName).Column("DTY_DocumentTypeName")
                .Not.Nullable()
                .Length(50);
            Map(x => x.Description).Column("DTY_Note")
                .Nullable()
                .Length(255);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("DTY_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("DTY_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("DTY_UpdatedTime");
            Map(x => x.CreatedBy).Column("DTY_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("DTY_UpdatedBy");
            Version(x => x.Version).Column("DTY_Version");
        }
    }
}
