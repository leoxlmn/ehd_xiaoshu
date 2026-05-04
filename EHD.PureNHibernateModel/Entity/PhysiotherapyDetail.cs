using System;
using System.Collections.Generic;

namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public class PhysiotherapyDetail : EntityBase, IComparable<PhysiotherapyDetail>, ITreatmentDetail {

        public virtual Diagram PainScaleDiagram { get; set; }
        public virtual IList<PointOnPhysiotherapy> PainLocations { get; set; }
        public virtual PainKeys PainKey { get; set; }
        public virtual string DescriptionOfSymptoms { get; set; }
        public virtual Trend SymptomTrend { get; set; }
        public virtual string ConstantVsIntermittent { get; set; }
        public virtual string DayChanges { get; set; }
        public virtual IrritabilityLevel IrritabilityLevel { get; set; }
        public virtual string AggravatedBy { get; set; }
        public virtual string EasedBy { get; set; }
        public virtual string Occupation { get; set; }
        public virtual string WorkStatus { get; set; }
        public virtual string HistoryOfPresentingComplaint { get; set; }
        public virtual string JointSoundsAndAbnormalities { get; set; }
        public virtual string PastRelevantHistoryAndTreatment { get; set; }
        public virtual string Investigations { get; set; }
        public virtual string Medications { get; set; }
        public virtual string GeneralMedicalHistoryAndMedications { get; set; }
        public virtual string SocialHistoryAndRecreationalActivities { get; set; }
        public virtual string Goals { get; set; }
        public virtual string RedFlagsAndPrecautions { get; set; }
        public virtual string ObservationPostureGait { get; set; }
        public virtual string ActiveRangeOfMotion { get; set; }
        public virtual string MuscleStrengthAndMyotomes { get; set; }
        public virtual string Palpation { get; set; }
        public virtual Diagram PassiveAccessoryMovementsAndPalpationOfSpineDiagram { get; set; }
        public virtual IList<PointOnPhysiotherapy> LocationsOnSpine { get; set; }
        public virtual string Neurological { get; set; }
        public virtual string Reflexes { get; set; }
        public virtual string Sensation { get; set; }
        public virtual string PassiveRangeOfMotion { get; set; }
        public virtual string StabilityTestsAndSpecialTests { get; set; }
        public virtual string Analysis { get; set; }
        public virtual string TreatmentPlan { get; set; }
        public virtual ConsentForTreament ConsentForTreatment { get; set; }

        public PhysiotherapyDetail()
            : base() {
                PainLocations = new List<PointOnPhysiotherapy>();
                LocationsOnSpine = new List<PointOnPhysiotherapy>();
        }

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(PhysiotherapyDetail other) {
            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }

    public enum PainKeys {
        Pain,
        Numbness,
        PinsAndNeedles,
        None
    }

    public enum Trend {
        Increasing,
        Static,
        Decreasing,
        None
    }

    public enum IrritabilityLevel {
        Low,
        Moderate,
        High,
        None
    }

    public enum ConsentForTreament {
        Verbal,
        None
    }
}
