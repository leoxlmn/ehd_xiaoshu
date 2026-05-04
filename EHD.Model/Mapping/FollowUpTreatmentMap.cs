using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class FollowUpTreatmentMap : ClassMap<FollowUpTreatment> {
        public FollowUpTreatmentMap() {
            Id(x => x.Id).Column("FTR_FTRId")
                .GeneratedBy.HiLo("1");
            References(x => x.InitialTreatment).Column("FTR_ITRId")
                .Not.Nullable();
                //.Not.LazyLoad();
            References(x => x.Therapist).Column("FTR_USRId")
                .Not.Nullable();
                //.Not.LazyLoad();
            Map(x => x.TreatmentTime).Column("FTR_TreatmentTime")
                .Not.Nullable();
            Map(x => x.TreatmentDurationMinutes).Column("FTR_TreatmentDurationMinutes")
                .Not.Nullable();
            Map(x => x.Note).Column("FTR_Note");
            Map(x => x.OpenToTherapist).Column("FTR_OpenToTherapist")
                .Not.Nullable();
            Map(x => x.IsComplete).Column("FTR_IsComplete")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("FTR_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("FTR_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("FTR_UpdatedTime");
            Map(x => x.CreatedBy).Column("FTR_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("FTR_UpdatedBy");
            Version(x => x.Version).Column("FTR_Version");
        }
    }
}
