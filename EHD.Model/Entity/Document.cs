using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class Document : EntityBase, IComparable<Document> {

        public virtual Patient Patient { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual DateTime DocumentCreationTime { get; set; }
        public virtual string DocumentName { get; set; }
        public virtual string Note { get; set; }

        [NonSerialized]
        private byte[] _docBLOB;

        [ScriptIgnore]
        public virtual byte[] DocumentBLOB {
            get { return _docBLOB; }
            set { _docBLOB = value; }
        }

        public Document() : base() { }

        public virtual int CompareTo(Document other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return DocumentName;
        }
    }

    //Extension Class
    public static class DocumentExtension {
        public static bool HasMandatoryValues(this Document doc) {
            return doc.Patient != null &&
                doc.DocumentType != null &&
                doc.DocumentName != null && doc.DocumentName.Trim().Length > 0 &&
                doc.DocumentBLOB != null;
        }
    }
}
