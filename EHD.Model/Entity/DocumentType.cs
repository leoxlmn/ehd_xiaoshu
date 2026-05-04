using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class DocumentType : EntityBase, IComparable<DocumentType>, IComparable {
        public virtual string DocumentTypeName { get; set; }

        public virtual string Description { get; set; }

        public DocumentType()
            : base() {
        }

        public virtual int CompareTo(DocumentType other) {
            if (other == null) return 1;

            return this.DocumentTypeName.CompareTo(other.DocumentTypeName);
        }

        public override string ToString() {
            return DocumentTypeName;
        }
    }

    //Extension Class
    public static class DocumentTypeExtension {
        public static bool HasMandatoryValues(this DocumentType type) {
            return type.DocumentTypeName != null && type.DocumentTypeName.Trim().Length > 0;
        }
    }
}
