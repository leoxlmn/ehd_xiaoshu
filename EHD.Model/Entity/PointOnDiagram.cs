using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public abstract class PointOnDiagram : EntityBase, IComparable<PointOnDiagram> {

        public virtual Diagram Diagram { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual PainKeys PainKey { get; set; }

        public PointOnDiagram() : base() { }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(X);
            sb.Append(',');
            sb.Append(Y);
            sb.Append(']');
            return sb.ToString();
        }

        public virtual int CompareTo(PointOnDiagram other) {
            if(other == null) return 0;

            return X == other.X ? Y.CompareTo(other.Y)
                : X.CompareTo(other.X);
        }
    }
}
