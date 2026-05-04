using FluentNHibernate.Mapping;
using EHD.Model.Entity;

namespace EHD.Model.Mapping {
    public class HistoryTrackingMap : ClassMap<HistoryTracking> {
        public HistoryTrackingMap() {
            Id(x => x.Id).Column("HST_HSTID")
                .GeneratedBy.HiLo("1");
            Map(x => x.ObjectName).Column("HST_ObjectName")
                .Not.Nullable()
                .Length(50);
            Map(x => x.ObjectId).Column("HST_ObjectId")
                .Not.Nullable()
                .Length(50);
            Map(x => x.OldValue).Column("HST_OldValue");
            Map(x => x.NewValue).Column("HST_NewValue");
            Map(x => x.ActionType).Column("HST_ActionType")
                .Not.Nullable();
            Map(x => x.ActionTime).Column("HST_ActionTime")
                .Not.Nullable();
            Map(x => x.ActionBy).Column("HST_ActionBy")
                .Not.Nullable()
                .Length(50);
        }
    }
}
