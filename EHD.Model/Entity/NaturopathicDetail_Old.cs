using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class NaturopathicDetail : EntityBase, IComparable<NaturopathicDetail>, ITreatmentDetail {

        public virtual InitialTreatment InitialTreatment { get; set; }
        public virtual string Height { get; set; }
        public virtual string Weight { get; set; }
        public virtual string Sex { get; set; }
        public virtual bool Acne { get; set; }
        public virtual bool Eczema { get; set; }
        public virtual bool DrySkin { get; set; }
        public virtual bool Psoriasis { get; set; }
        public virtual bool NightSweats { get; set; }
        public virtual bool PainUrinating { get; set; }
        public virtual bool BloodInUrine { get; set; }
        public virtual bool PoorVision { get; set; }
        public virtual bool EyeDryness { get; set; }
        public virtual bool EyeDischarge { get; set; }
        public virtual bool Glaucoma { get; set; }
        public virtual bool Cataracts { get; set; }
        public virtual bool Heartburn { get; set; }
        public virtual bool Anxiety { get; set; }
        public virtual bool MouthDryness { get; set; }
        public virtual bool Constipation { get; set; }
        public virtual bool MuscleWeakness { get; set; }
        public virtual bool RectalBleeding { get; set; }
        public virtual bool SwollenNeckGlands { get; set; }
        public virtual bool WeightGainLoss { get; set; }
        public virtual bool NeckStiffness { get; set; }
        public virtual bool SinusProblems { get; set; }
        public virtual bool HeartPalpitations { get; set; }
        public virtual bool AnkleSwelling { get; set; }
        public virtual bool AppetiteChagnes { get; set; }
        public virtual bool ThirstChanges { get; set; }
        public virtual bool ColdHandsFeet { get; set; }
        public virtual bool ThyroidProblems { get; set; }
        public virtual bool Headaches { get; set; }
        public virtual bool NeckPain { get; set; }
        public virtual bool NoseBleeds { get; set; }
        public virtual bool Cough { get; set; }
        public virtual bool Wheeze { get; set; }
        public virtual bool ChestPain { get; set; }
        public virtual bool ShortBreath { get; set; }
        public virtual bool Asthma { get; set; }
        public virtual bool Bronchitis { get; set; }
        public virtual bool Emphysema { get; set; }
        public virtual bool BreastLump { get; set; }
        public virtual bool BreastPain { get; set; }
        public virtual bool ProstateSymptoms { get; set; }
        public virtual bool Arthritis { get; set; }
        public virtual bool HotFlushes { get; set; }
        public virtual bool HeavyPeriods { get; set; }
        public virtual bool EasyBruising { get; set; }
        public virtual bool Allergies { get; set; }
        public virtual bool Depression { get; set; }
        public virtual bool HeartDisease { get; set; }
        public virtual bool PoorSleep { get; set; }
        public virtual bool Anemia { get; set; }
        public virtual bool JointPain { get; set; }
        public virtual bool Fatigue { get; set; }
        public virtual bool Dizziness { get; set; }
        public virtual bool Seizures { get; set; }
        public virtual bool PMS { get; set; }
        public virtual bool Diarrhea { get; set; }
        public virtual bool SeenNaturopathicDoctorBefore { get; set; }
        public virtual DateTime SeenNaturopathicDoctorDate { get; set; }
        public virtual bool VisitPurposeGeneral { get; set; }
        public virtual bool VisitPurposeSpecificConcern { get; set; }
        public virtual string VisitPurposeSpecificConcernDescription { get; set; }

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(NaturopathicDetail other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
