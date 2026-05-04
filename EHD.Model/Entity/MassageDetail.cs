using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class MassageDetail : EntityBase, IComparable<MassageDetail>, ITreatmentDetail {

        public virtual InitialTreatment InitialTreatment { get; set; }
        public virtual Diagram MassageDiagram { get; set; }
        public virtual string ActivityLimitations { get; set; }
        public virtual string TreatmentGoal { get; set; }
        public virtual string TreatmentFocus { get; set; }
        public virtual string TreatmentFrequency { get; set; }
        public virtual string TreatmentDuration { get; set; }
        public virtual bool? TreatmentPlanDiscussedWithClient { get; set; }
        public virtual bool? ConsentReceived { get; set; }
        public virtual bool TreatmentAreaBack { get; set; }
        public virtual bool TreatmentAreaNeck{ get; set; }
        public virtual bool TreatmentAreaShoulders { get; set; }
        public virtual bool TreatmentAreaFace { get; set; }
        public virtual bool TreatmentAreaLeftArm { get; set; }
        public virtual bool TreatmentAreaRightArm { get; set; }
        public virtual bool TreatmentAreaLeftLeg { get; set; }
        public virtual bool TreatmentAreaRightLeg { get; set; }
        public virtual bool TreatmentAreaGluteus { get; set; }
        public virtual bool TreatmentAreaAbdominals { get; set; }
        public virtual bool TreatmentAreaChest { get; set; }
        public virtual bool TreatmentAreaBreast { get; set; }
        public virtual string TreatmentAreaOther { get; set; }
        public virtual string AssessmentsPerformed { get; set; }
        public virtual string ResultsOfAssessments { get; set; }
        public virtual string ReassessmentSchedule { get; set; }
        public virtual string Referrals { get; set; }
        public virtual string AnticipatedProgressionOfResponses { get; set; }
        public virtual string RemedialExercisesRecommended { get; set; }
        public virtual string Risks { get; set; }

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(MassageDetail other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
