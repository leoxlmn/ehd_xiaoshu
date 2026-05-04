using System;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class Diagram : EntityBase, IComparable<Diagram> {

        [NonSerialized]
        private byte[] _diagramImage;

        public virtual string DiagramName { get; set; }

        [ScriptIgnore]
        public virtual byte[] Image {
            get { return _diagramImage; }
            set { _diagramImage = value; }
        }

        public virtual string ImageType { get; set; }
        public virtual int ImageWidth { get; set; }
        public virtual int ImageHeight { get; set; }


        public Diagram() : base() { }

        public override string ToString() {
            return DiagramName;
        }

        public virtual int CompareTo(Diagram other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }

    public static class DiagramExtension {
        public static bool HasMandatoryValues(this Diagram diagram) {
            return diagram.DiagramName != null && diagram.DiagramName.Trim().Length > 0 &&
                diagram.Image != null &&
                diagram.ImageHeight > 0 &&
                diagram.ImageWidth > 0 &&
                diagram.ImageType != null && diagram.ImageType.Trim().Length > 0;
        }
    }
}
