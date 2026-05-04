using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class TherapyType : EntityBase, IComparable<TherapyType>, IComparable {
        public virtual string TherapyTypeName { get; set; }
        //public virtual IList<User> Therapists { get; set; }

        public virtual bool EnableMinutesPerTreatment { get; set; }
        public virtual int MinutesPerTreatment { get; set; }
        //public virtual bool EnableMaxTreatmentsPerDayPerInsurer { get; set; }
        //public virtual int MaxTreatmentsPerDayPerInsurer { get; set; }
        public virtual bool EnableMaxTreatmentsPerDay { get; set; }
        public virtual int MaxTreatmentsPerDay { get; set; }
        public virtual double DefaultPrice { get; set; }
        public virtual bool ShowDurationOnInvoice { get; set; }
        public virtual bool AllowTimeOverlap { get; set; }

        public TherapyType()
            : base() {
                //Therapists = new List<User>();
        }

        public virtual int CompareTo(TherapyType other) {
            if (other == null) return 1;

            return this.TherapyTypeName.CompareTo(other.TherapyTypeName);
        }

        public override string ToString() {
            return TherapyTypeName;
        }
    }

    //Extension Class
    public static class TherapyTypeExtension {
        public static bool HasMandatoryValues(this TherapyType type) {
            return type.TherapyTypeName != null && type.TherapyTypeName.Trim().Length > 0;
        }
    }
}
