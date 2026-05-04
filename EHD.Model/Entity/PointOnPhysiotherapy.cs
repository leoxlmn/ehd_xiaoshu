using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class PointOnPhysiotherapy : PointOnDiagram {
        public virtual PhysiotherapyDetail PhysiotherapyDetail { get; set; }
    }
    /*
    public class PointOnPhysiotherapy : EntityBase, IComparable<PointOnPhysiotherapy> {

        public virtual Diagram Diagram { get; set; }
        public virtual PhysiotherapyDetail PhysiotherapyDetail { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual PainKeys PainKey { get; set; }

        public PointOnPhysiotherapy() : base() { }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(X);
            sb.Append(',');
            sb.Append(Y);
            sb.Append(']');
            return sb.ToString();
        }

        public virtual int CompareTo(PointOnPhysiotherapy other) {
            return X == other.X ? Y.CompareTo(other.Y)
                : X.CompareTo(other.X);
        }
    }*/
}
