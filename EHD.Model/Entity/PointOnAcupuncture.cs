using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class PointOnAcupuncture : PointOnDiagram {
        public virtual AcupunctureDetail AcupunctureDetail { get; set; }
    }
}
