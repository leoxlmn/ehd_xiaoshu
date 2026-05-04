using System;
using System.Text;
using EHD.Constant;

namespace EHD.Model.Entity {
    [Serializable]
    public class FollowUpTreatment : EntityBase, IComparable<FollowUpTreatment> {

        public virtual InitialTreatment InitialTreatment { get; set; }
        public virtual User Therapist { get; set; }
        public virtual DateTime TreatmentTime { get; set; }
        public virtual int TreatmentDurationMinutes { get; set; }
        public virtual string Note { get; set; }
        public virtual bool OpenToTherapist { get; set; }
        public virtual bool IsComplete { get; set; }

        public FollowUpTreatment() : base() {
            TreatmentTime = DateTime.Now;
            TreatmentDurationMinutes = 30;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            sb.Append(" - ");
            sb.Append(Note);
            return sb.ToString();
        }

        public virtual int CompareTo(FollowUpTreatment other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }

    //Extension Class
    public static class FollowUpTreatmentExtension {
        public static bool HasMandatoryValues(this FollowUpTreatment treatment) {
            return treatment.InitialTreatment != null && treatment.Therapist != null
                /*&& treatment.Note != null && treatment.Note.Trim().Length > 0*/;
        }
    }
}
