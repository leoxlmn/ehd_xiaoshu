using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class InvoiceNoteReference : EntityBase, IComparable<InvoiceNoteReference> {

        public virtual string Text { get; set; }

        public InvoiceNoteReference() : base() { }

        public virtual int CompareTo(InvoiceNoteReference other) {
            if (other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return Text;
        }
    }

    //Extension Class
    public static class InvoiceNoteReferenceExtension {
        public static bool HasMandatoryValues(this InvoiceNoteReference note) {
            return note.Text != null && note.Text.Trim().Length > 0;
        }
    }
}
