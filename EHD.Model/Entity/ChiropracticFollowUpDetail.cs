using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class ChiropracticFollowUpDetail : EntityBase, IComparable<ChiropracticFollowUpDetail>, IFollowUpDetail {
        public virtual FollowUpTreatment FollowUpTreatment { get; set; }
        public virtual Diagram ChiropracticSOAPDiagram { get; set; }
        public virtual int VAS { get; set; }
        public virtual ChiropracticSOAPConditionChanges ConditionChange { get; set; }
        public virtual string ConditionChangeNote { get; set; }
        public virtual string O { get; set; }
        public virtual string A { get; set; }
        public virtual bool OsseousManipulation { get; set; }
        public virtual bool Massage { get; set; }
        public virtual bool Stretch { get; set; }
        public virtual bool TriggerPointTherapy { get; set; }
        public virtual bool Traction { get; set; }
        public virtual bool TheraputicExercise { get; set; }
        public virtual string ExerciseNote { get; set; }
        public virtual bool Heat { get; set; }
        public virtual bool Cold { get; set; }
        public virtual bool UltraSound { get; set; }
        public virtual int UltraSoundValue { get; set; }
        public virtual bool Electrotherapy { get; set; }
        public virtual string ElectrotherapyNote { get; set; }
        public virtual bool NoDxChange { get; set; }
        public virtual bool NewDiagnosis { get; set; }
        public virtual string NewDiagnosisNote { get; set; }
        public virtual int PTRDays { get; set; }
        public virtual int PTRWeeks { get; set; }
        public virtual int PTRMonths { get; set; }
        public virtual bool PRN { get; set; }
        public virtual bool Referral { get; set; }
        public virtual string ReferralNote { get; set; }
        public virtual string HomeCare { get; set; }
        public virtual string Comments { get; set; }

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(ChiropracticFollowUpDetail other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }

}
