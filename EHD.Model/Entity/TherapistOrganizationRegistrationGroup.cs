using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class TherapistOrganizationRegistrationGroup : EntityBase, IComparable<TherapistOrganizationRegistrationGroup> {

        public virtual User Therapist { get; set; }

        public TherapistOrganizationRegistrationGroup() : base() { }

        public virtual int CompareTo(TherapistOrganizationRegistrationGroup other) {
            if(other == null) return 0;

            return this.Therapist.CompareTo(other.Therapist);
        }

        public override string ToString() {
            return Id.ToString();
        }
    }

    //Extension Class
    public static class TherapistOrganizationRegistrationGroupExtension {
        public static bool HasMandatoryValues(this TherapistOrganizationRegistrationGroup group) {
            return group.Therapist != null;
        }
    }
}
