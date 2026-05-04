using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class PointOnAcupunctureFollowUp : PointOnDiagram {
        public virtual AcupunctureFollowUpDetail AcupunctureFollowUpDetail { get; set; }
    }
}
