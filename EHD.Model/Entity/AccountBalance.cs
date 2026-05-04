using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class AccountBalance : EntityBase, IComparable<AccountBalance> {

        public virtual Patient Patient { get; set; }
        public virtual double TransactionAmount { get; set; }
        public virtual DateTime TransactionTime { get; set; }
        public virtual string TransactionDescription{get;set;}

        public virtual int CompareTo(AccountBalance other) {
            if (other == null) return 0;

            return this.TransactionTime.CompareTo(other.TransactionTime);
        }

        public AccountBalance() : base() { }

        public override string ToString() {
            return TransactionTime.ToString("yyyy-MM-dd") + " - " + TransactionDescription + " - " + TransactionAmount.ToString();
        }
    }

    //Extension Class
    public static class AccountBalanceExtension {
        public static bool HasMandatoryValues(this AccountBalance accountBalance) {
            return accountBalance.Patient != null && accountBalance.Patient.Id != default(int)
                && accountBalance.TransactionDescription != null && accountBalance.TransactionDescription.Length > 0
                && accountBalance.TransactionTime != null && accountBalance.TransactionTime != default(DateTime)
                && accountBalance.TransactionAmount != 0;
        }
    }
}
