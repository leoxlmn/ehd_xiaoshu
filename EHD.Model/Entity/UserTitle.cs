using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class UserTitle : EntityBase, IComparable<UserTitle> {
        public virtual string Name { get; set; }

        public UserTitle() : base() { }

        public virtual int CompareTo(UserTitle other) {
            if (other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return Name;
        }
    }

    //Extension Class
    public static class UserTitleExtension {
        public static bool HasMandatoryValues(this UserTitle title) {
            return title.Name != null && title.Name.Trim().Length > 0;
        }
    }
}
