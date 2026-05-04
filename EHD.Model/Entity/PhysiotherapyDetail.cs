using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class PhysiotherapyDetail : EntityBase, IComparable<PhysiotherapyDetail>, ITreatmentDetail {

        public virtual InitialTreatment InitialTreatment { get; set; }
        public virtual Diagram PainScaleDiagram { get; set; }
        //public virtual IList<PointOnPhysiotherapy> PainLocations { get; set; }
        public virtual int PainScaleNow { get; set; }
        public virtual int PainScaleBest { get; set; }
        public virtual int PainScaleWorst { get; set; }
        //public virtual PainKeys PainKey { get; set; }
        public virtual string DescriptionOfSymptoms { get; set; }
        public virtual Trend SymptomTrend { get; set; }
        public virtual string ConstantVsIntermittent { get; set; }
        public virtual string DayChanges { get; set; }
        public virtual IrritabilityLevels IrritabilityLevel { get; set; }
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
        //public virtual IList<PointOnPhysiotherapy> LocationsOnSpine { get; set; }
        public virtual string Neurological { get; set; }
        public virtual string Reflexes { get; set; }
        public virtual string Sensation { get; set; }
        public virtual string PassiveRangeOfMotion { get; set; }
        public virtual string StabilityTestsAndSpecialTests { get; set; }
        public virtual string Analysis { get; set; }
        public virtual string TreatmentPlan { get; set; }
        public virtual ConsentForTreaments ConsentForTreatment { get; set; }

        public PhysiotherapyDetail()
            : base() {
                //PainLocations = new List<PointOnPhysiotherapy>();
                //LocationsOnSpine = new List<PointOnPhysiotherapy>();

            //Initialization
            //PainKey = PainKeys.None;
            SymptomTrend = Trend.None;
            IrritabilityLevel = IrritabilityLevels.None;
            ConsentForTreatment = ConsentForTreaments.None;
        }

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(PhysiotherapyDetail other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }

}
