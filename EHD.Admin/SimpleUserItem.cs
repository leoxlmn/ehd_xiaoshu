using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Admin {
    public class SimpleUserItem : IComparable {
        public int Id { get; set; }

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string DisplayName {
            get {
                return this.ToString();
            }
        }
        public override string ToString() {
            string name =
                (MiddleName != null && MiddleName.Trim().Length > 0) ?
                string.Format("{0} {1} {2}", FirstName, MiddleName, LastName) :
                string.Format("{0} {1}", FirstName, LastName);

            return Title != null && Title.Trim().Length > 0 ?
                string.Format("{0} {1}", Title, name) :
                name;
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
            var that = obj as SimpleUserItem;
            if (that == null) return false;

            if (Id == 0 && that.Id == 0) {
                return ReferenceEquals(this, that);
            }

            return Id == that.Id;
        }

        public static bool operator ==(SimpleUserItem lhs, SimpleUserItem rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(SimpleUserItem lhs, SimpleUserItem rhs) {
            return !Equals(lhs, rhs);
        }

        int IComparable.CompareTo(object obj) {
            if (obj == null) return 0;

            return ToString().CompareTo((obj as SimpleUserItem).ToString());
        }
    }
}
