using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class TherapyTypeMap : ClassMap<TherapyType>{
        public TherapyTypeMap() {
            Id(x => x.Id).Column("THP_THPId")
                .GeneratedBy.HiLo("1");
            Map(x => x.TherapyTypeName).Column("THP_TherapyType")
                .Not.Nullable()
                .Length(100);
            Map(x => x.EnableMinutesPerTreatment).Column("THP_EnableMinutesPerTreatment")
                .Not.Nullable();
            Map(x => x.MinutesPerTreatment).Column("THP_MinutesPerTreatment")
                .Not.Nullable();
            /*
            Map(x => x.EnableMaxTreatmentsPerDayPerInsurer).Column("THP_EnableMaxTreatmentsPerDayPerInsurer")
                .Not.Nullable();
            Map(x => x.MaxTreatmentsPerDayPerInsurer).Column("THP_MaxTreatmentsPerDayPerInsurer")
                .Not.Nullable();
            */
            Map(x => x.EnableMaxTreatmentsPerDay).Column("THP_EnableMaxTreatmentsPerDay")
                .Not.Nullable();
            Map(x => x.MaxTreatmentsPerDay).Column("THP_MaxTreatmentsPerDay")
                .Not.Nullable();
            Map(x => x.DefaultPrice).Column("THP_DefaultPrice")
                .Not.Nullable();
            Map(x => x.ShowDurationOnInvoice).Column("THP_ShowDurationOnInvoice")
                .Not.Nullable();
            Map(x => x.AllowTimeOverlap).Column("THP_AllowTimeOverlap")
                .Not.Nullable();

            /*
            HasManyToMany(x => x.Therapists)
                .Cascade.All()
                .Table("UserTherapyType");*/

            //EntityBase mappings
            Map(x => x.IsTestData).Column("THP_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("THP_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("THP_UpdatedTime");
            Map(x => x.CreatedBy).Column("THP_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("THP_UpdatedBy");
            Version(x => x.Version).Column("THP_Version");
        }
    }
}
