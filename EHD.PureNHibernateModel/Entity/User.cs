using System;
using System.Collections.Generic;

namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public class User : EntityBase, IComparable<User> {

        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual IList<TherapyType> TherapistTypes { get; set; }
        public virtual DateTime LastLoginTime { get; set; }

        public virtual int CompareTo(User other) {
            if (other == null) return 1;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public User()
            : base() {
            UserType = UserType.Unknown;
            TherapistTypes = new List<TherapyType>();
        }

        public virtual bool CanDo(string therapyTypeName){
            foreach(TherapyType type in TherapistTypes) {
                if(type.TherapyTypeName.Trim().ToLower().Equals(
                    therapyTypeName.Trim().ToLower())) {
                    return true;
                }
            }

            return false;
        }

        public override string ToString() {
            return
                (MiddleName != null && MiddleName.Trim().Length > 0) ?
                string.Format("{0} {1}, {2}", FirstName, MiddleName, LastName) :
                string.Format("{0}, {1}", FirstName, LastName);
        }
    }
}
