using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class Insurer : EntityBase, IComparable<Insurer> {
        public virtual string InsurerName { get; set; }
        //public virtual int WarnTreatmentPerDay { get; set; }
        //public virtual int MaxTreatmentPerDay { get; set; }
        public virtual string Comment { get; set; }

        public Insurer() : base() { }

        public virtual int CompareTo(Insurer other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return InsurerName;
        }
    }

    //Extension Class
    public static class InsurerExtension {
        public static bool HasMandatoryValues(this Insurer insurer) {
            return insurer.InsurerName != null && insurer.InsurerName.Trim().Length > 0;
        }
    }
}
