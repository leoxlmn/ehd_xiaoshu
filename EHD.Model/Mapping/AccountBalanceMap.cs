using EHD.Model.Entity;
using FluentNHibernate.Mapping;

namespace EHD.Model.Mapping {
    public class AccountBalanceMap : ClassMap<AccountBalance> {
        public AccountBalanceMap() {
            Id(x => x.Id).Column("ACB_ACBId")
                .GeneratedBy.HiLo("1");
            Map(x => x.TransactionDescription).Column("ACB_TransactionDescription")
                .Not.Nullable()
                .Length(50);
            Map(x => x.TransactionTime).Column("ACB_TransactionTime")
                .Not.Nullable();
            Map(x => x.TransactionAmount).Column("ACB_TransactionAmount")
                .Not.Nullable();

            References(x => x.Patient).Column("ACB_PTNId")
                .Not.Nullable();

            //EntityBase mappings
            Map(x => x.IsTestData).Column("ACB_IsTestData")
                .Not.Nullable();
            Map(x => x.CreatedTime).Column("ACB_CreatedTime")
                .Not.Nullable();
            Map(x => x.UpdatedTime).Column("ACB_UpdatedTime");
            Map(x => x.CreatedBy).Column("ACB_CreatedBy")
                .Not.Nullable();
            Map(x => x.UpdatedBy).Column("ACB_UpdatedBy");
            Version(x => x.Version).Column("ACB_Version");
        }
    }
}
