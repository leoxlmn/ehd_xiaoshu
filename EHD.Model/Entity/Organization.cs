using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class Organization : EntityBase, IComparable<Organization> {
        public virtual string OrganizationName { get; set; }

        public Organization() : base() { }

        public virtual int CompareTo(Organization other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return OrganizationName;
        }
    }

    //Extension Class
    public static class OrganizationExtension {
        public static bool HasMandatoryValues(this Organization org) {
            return org.OrganizationName != null && org.OrganizationName.Trim().Length > 0;
        }
    }
}
