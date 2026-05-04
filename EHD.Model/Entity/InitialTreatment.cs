using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class InitialTreatment : EntityBase, IComparable<InitialTreatment> {

        public virtual Patient Patient { get; set; }
        public virtual User Therapist { get; set; }
        public virtual TherapyType TreatmentType { get; set; }
        public virtual DateTime TreatmentTime { get; set; }
        public virtual int TreatmentDurationMinutes { get; set; }
        public virtual ITreatmentDetail TreatmentDetail { get; set; }
        [ScriptIgnore]
        public virtual IList<FollowUpTreatment> FollowUpTreatments { get; set; }
        public virtual bool OpenToTherapist { get; set; }
        public virtual bool IsComplete { get; set; }

        public InitialTreatment()
            : base() {
            //FollowUpTreatments = new List<FollowUpTreatment>();
            
            TreatmentTime = DateTime.Now;
            TreatmentDurationMinutes = 30;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(Patient);
            sb.Append(" - ");
            sb.Append(TreatmentType);
            sb.Append(" - ");
            sb.Append(TreatmentTime);
            sb.Append(" - ");
            sb.Append(TreatmentDurationMinutes);
            return sb.ToString();
        }

        public virtual int CompareTo(InitialTreatment other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }

    //Extension Class
    public static class InitialTreatmentExtension {
        public static bool HasMandatoryValues(this InitialTreatment treatment) {
            return treatment.Patient != null && treatment.Therapist != null &&
                treatment.TreatmentType != null;
        }
    }
}
