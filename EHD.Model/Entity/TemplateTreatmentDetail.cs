using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class TemplateTreatmentDetail : EntityBase, IComparable<TemplateTreatmentDetail> {

        public virtual TherapyType TreatmentType { get; set; }
        public virtual bool IsInitial { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual int DetailId { get; set; }
        public virtual string TreatmentNote { get; set; }
        public virtual string Note { get; set; }

        public TemplateTreatmentDetail() : base() { }

        public virtual int CompareTo(TemplateTreatmentDetail other) {
            if(other == null) return 0;

            if (this.IsInitial != other.IsInitial) {
                return this.IsInitial.CompareTo(other.IsInitial);
            } else if (this.TreatmentType != other.TreatmentType) {
                return this.TreatmentType.CompareTo(other.TreatmentType);
            } else {
                return this.CreatedTime.CompareTo(other.CreatedTime);
            }
        }

        public override string ToString() {
            return Note;
        }
    }

    //Extension Class
    public static class TemplateTreatmentDetailExtension {
        public static bool HasMandatoryValues(this TemplateTreatmentDetail template) {
            return template.DetailId != default(int) && 
                template.Note != null && template.Note.Trim().Length > 0;
        }
    }
}
