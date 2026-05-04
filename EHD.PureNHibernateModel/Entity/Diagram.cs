using System;
using System.Web.Script.Serialization;

namespace EHD.PureNHibernateModel.Entity {
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
            return this.DisplayName.CompareTo(other.DisplayName);
        }
    }
}
