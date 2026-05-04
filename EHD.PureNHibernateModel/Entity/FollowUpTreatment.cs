using System;
using System.Text;
using EHD.Constant;

namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public class FollowUpTreatment : EntityBase, IComparable<FollowUpTreatment> {

        public virtual InitialTreatment InitialTreatment { get; set; }
        public virtual User Therapist { get; set; }
        public virtual DateTime TreatmentTime { get; set; }
        public virtual int TreatmentDurationMinutes { get; set; }
        public virtual string Note { get; set; }

        public FollowUpTreatment() : base() { }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(TreatmentTime.ToString(Constants.DATE_TIME_FORMAT));
            sb.Append(" - ");
            sb.Append(Note);
            return sb.ToString();
        }

        public virtual int CompareTo(FollowUpTreatment other) {
            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
