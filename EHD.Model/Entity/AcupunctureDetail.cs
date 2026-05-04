using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class AcupunctureDetail : EntityBase, IComparable<AcupunctureDetail>, ITreatmentDetail {

        public virtual InitialTreatment InitialTreatment { get; set; }

        public virtual Diagram TongueDiagram { get; set; }
        public virtual string Pulse { get; set; }
        public virtual string Tongue { get; set; }
        public virtual string BloodPressure { get; set; }
        public virtual string BloodSugar { get; set; }
        public virtual string Subjective { get; set; }
        public virtual string TCMDiagnosis { get; set; }
        public virtual string TreatmentPlan { get; set; }

        public override string ToString() {
            return base.ToString();
        }

        public virtual int CompareTo(AcupunctureDetail other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
