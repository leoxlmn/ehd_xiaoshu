using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class PointOnOsteopathy : PointOnDiagram {
        public virtual OsteopathyDetail OsteopathyDetail { get; set; }
    }
}
