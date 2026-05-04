using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class Tax : EntityBase, IComparable<Tax> {

        public virtual double Rate { get; set; }
        public virtual string Name { get; set; }

        public Tax() : base() { }

        public virtual int CompareTo(Tax other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return string.Format("{0} - {1:0.00}", Name, Rate);
        }
    }

    //Extension Class
    public static class TaxExtension {
        public static bool HasMandatoryValues(this Tax tax) {
            return tax.Name != null && tax.Name.Trim().Length > 0;
        }
    }
}
