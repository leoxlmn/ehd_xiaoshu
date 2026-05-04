using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class TemplateTreatmentDetailMap : ClassMap<TemplateTreatmentDetail> {
        public TemplateTreatmentDetailMap() {
            Id(x => x.Id).Column("TTD_TTDId")
                .GeneratedBy.HiLo("1");

            References(x => x.TreatmentType).Column("TTD_THPId")
                .Not.Nullable();

            Map(x => x.IsInitial).Column("TTD_IsInitial")
                .Not.Nullable();

            Map(x => x.IsDefault).Column("TTD_IsDefault")
                .Not.Nullable();

            Map(x => x.DetailId).Column("TTD_DetailId")
                .Not.Nullable();

            Map(x => x.TreatmentNote).Column("TTD_TreatmentNote");

            Map(x => x.Note).Column("TTD_Note")
                .Not.Nullable()
                .Length(255);

            //EntityBase mappings
            Map(x => x.IsTestData).Column("TTD_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("TTD_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("TTD_UpdatedTime");
            Map(x => x.CreatedBy).Column("TTD_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("TTD_UpdatedBy");
            Version(x => x.Version).Column("TTD_Version");
        }
    }
}
