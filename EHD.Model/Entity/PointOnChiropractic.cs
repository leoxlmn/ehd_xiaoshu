using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class PointOnChiropractic : PointOnDiagram {
        public virtual ChiropracticDetail ChiropracticDetail { get; set; }
    }
    /*
    public class PointOnChiropractic : EntityBase, IComparable<PointOnChiropractic> {

        public virtual Diagram Diagram { get; set; }
        public virtual ChiropracticDetail ChiropracticDetail { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual PainKeys PainKey { get; set; }

        public PointOnChiropractic() : base() { }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(X);
            sb.Append(',');
            sb.Append(Y);
            sb.Append(']');
            return sb.ToString();
        }

        public virtual int CompareTo(PointOnChiropractic other) {
            return X == other.X ? Y.CompareTo(other.Y)
                : X.CompareTo(other.X);
        }
    }*/
}
