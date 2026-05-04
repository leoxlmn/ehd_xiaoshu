using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class InitialTreatmentMap : ClassMap<InitialTreatment> {
        public InitialTreatmentMap() {
            Id(x => x.Id).Column("ITR_ITRId")
                .GeneratedBy.HiLo("1");
            References(x => x.Patient).Column("ITR_PTNId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.Therapist).Column("ITR_USRId")
                .Not.Nullable()
                .Not.LazyLoad();
            References(x => x.TreatmentType).Column("ITR_THPId")
                .Not.Nullable()
                .Not.LazyLoad();
            Map(x => x.TreatmentTime).Column("ITR_TreatmentTime")
                .Not.Nullable();
            Map(x => x.TreatmentDurationMinutes).Column("ITR_TreatmentDurationMinutes")
                .Not.Nullable();
            Map(x => x.OpenToTherapist).Column("ITR_OpenToTherapist")
                .Not.Nullable();
            Map(x => x.IsComplete).Column("ITR_IsComplete")
                .Not.Nullable();
            /*
            HasOne(x => x.TreatmentDetail)
                .Cascade.All();*/
            HasMany(x => x.FollowUpTreatments).KeyColumn("FTR_ITRId")
                .Cascade.AllDeleteOrphan()
                .Inverse();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("ITR_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("ITR_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("ITR_UpdatedTime");
            Map(x => x.CreatedBy).Column("ITR_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("ITR_UpdatedBy");
            Version(x => x.Version).Column("ITR_Version");
        }
    }
}
