using System;
using System.Collections.Generic;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class TherapistOrganizationRegistration : EntityBase, IComparable<TherapistOrganizationRegistration> {

        public virtual User Therapist { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual TherapistOrganizationRegistrationGroup RegistrationGroup { get; set; }
        public virtual string RegistrationNumber { get; set; }

        public TherapistOrganizationRegistration() : base() { }

        public virtual int CompareTo(TherapistOrganizationRegistration other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            if(Therapist != null) sb.Append(Therapist.DisplayName);

            if(Organization != null) {
                if(sb.Length > 0) sb.Append(" - ");
                sb.Append(Organization.DisplayName);
            }

            if(sb.Length > 0) sb.Append(RegistrationNumber);

            return sb.ToString();
        }
    }

    //Extension Class
    public static class TherapistOrganizationRegistrationExtension {
        public static bool HasMandatoryValues(this TherapistOrganizationRegistration reg) {
            return reg.Therapist != null && reg.Organization != null && reg.RegistrationGroup != null
                && reg.RegistrationNumber != null && reg.RegistrationNumber.Trim().Length > 0;
        }
    }
}
