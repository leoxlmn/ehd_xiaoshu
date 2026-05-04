using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class NextFileNumberMap : ClassMap<NextFileNumber> {
        public NextFileNumberMap() {

            Table("next_file_number");

            Id(x => x.Key).Column("next_key")
                .Length(1)
                .GeneratedBy.Assigned();
            Map(x => x.NextNumber).Column("next_number")
                .Not.Nullable();
        }
    }
}
