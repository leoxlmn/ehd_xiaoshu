using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Admin {
    public class SimpleListItem : IComparable<SimpleListItem>, IComparable {
        public int Id { get; set; }
        public string Text { get; set; }

        public override string ToString() {
            return Text;
        }

        private int? oldHashCode;

        public override int GetHashCode() {
            if (oldHashCode.HasValue) return oldHashCode.Value;

            if (Id == 0) {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }

            return Id.GetHashCode();
        }

        public override bool Equals(object obj) {
            var that = obj as SimpleListItem;
            if (that == null) return false;

            if (Id == 0 && that.Id == 0) {
                return ReferenceEquals(this, that);
            }

            return Id == that.Id;
        }

        public static bool operator ==(SimpleListItem lhs, SimpleListItem rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(SimpleListItem lhs, SimpleListItem rhs) {
            return !Equals(lhs, rhs);
        }

        public int CompareTo(SimpleListItem other) {
            if (other == null) return 0;

            return Text.CompareTo(other.Text);
        }

        public int CompareTo(object obj) {
            if (obj == null) return 0;

            return Text.CompareTo((obj as SimpleListItem).Text);
        }
    }
}
