using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class PointOnChiropracticFollowUp : PointOnDiagram {
        public virtual ChiropracticFollowUpDetail ChiropracticFollowUpDetail { get; set; }
    }
}
