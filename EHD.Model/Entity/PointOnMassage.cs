using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class PointOnMassage : PointOnDiagram {
        public virtual MassageDetail MassageDetail { get; set; }
    }
}
