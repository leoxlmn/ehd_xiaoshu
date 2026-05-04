using System;
using System.Collections.Generic;
using System.Text;

namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public class InitialTreatment : EntityBase, IComparable<InitialTreatment> {

        public virtual Patient Patient { get; set; }
        public virtual User Therapist { get; set; }
        public virtual TherapyType TreatmentType { get; set; }
        public virtual DateTime TreatmentTime { get; set; }
        public virtual int TreatmentDurationMinutes { get; set; }
        public virtual ITreatmentDetail TreatmentDetail { get; set; }
        public virtual IList<FollowUpTreatment> FollowUpTreatments { get; set; }

        public InitialTreatment()
            : base() {
            FollowUpTreatments = new List<FollowUpTreatment>();
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
            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
