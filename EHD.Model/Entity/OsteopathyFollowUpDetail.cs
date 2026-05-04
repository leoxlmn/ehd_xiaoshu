using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class OsteopathyFollowUpDetail : EntityBase, IComparable<OsteopathyFollowUpDetail>, IFollowUpDetail {

        public virtual FollowUpTreatment FollowUpTreatment { get; set; }
        public virtual bool TreatmentUsedRocking { get; set; }
        public virtual bool TreatmentUsedPetrissage { get; set; }
        public virtual bool TreatmentUsedFriction { get; set; }
        public virtual bool TreatmentUsedVibration { get; set; }
        public virtual bool TreatmentUsedTapotement { get; set; }
        public virtual bool TreatmentUsedMyofacialRelease { get; set; }
        public virtual bool TreatmentUsedTenderPointRelease { get; set; }
        public virtual bool TreatmentUsedMuscleEnergy { get; set; }
        public virtual bool TreatmentUsedTotalBodyAdjustment { get; set; }
        public virtual bool TreatmentUsedStillTechnique { get; set; }
        public virtual bool TreatmentUsedCraniosacral { get; set; }
        public virtual bool TreatmentUsedVisceral { get; set; }
        public virtual bool TreatmentUsedSoftTissueJointMobilization { get; set; }
        public virtual bool TreatmentUsedStretch { get; set; }
        public virtual bool TreatmentUsedIntraOral { get; set; }
        public virtual string TreatmentUsedOther { get; set; }

        public virtual string TreatmentNote { get; set; }

        public virtual bool TreatmentAreaBack { get; set; }
        public virtual bool TreatmentAreaNeck { get; set; }
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

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(OsteopathyFollowUpDetail other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
