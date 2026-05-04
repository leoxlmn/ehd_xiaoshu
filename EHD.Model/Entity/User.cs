using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class User : EntityBase, IComparable<User> {

        [NonSerialized]
        private byte[] _imgSignature;

        [ScriptIgnore]
        public virtual byte[] SignatureImage {
            get { return _imgSignature; }
            set { _imgSignature = value; }
        }

        public virtual UserTitle Title { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string GoogleCalendarId { get; set; }
        public virtual UserType UserType { get; set; }
        //public virtual IList<TherapyType> TherapistTypes { get; set; }
        public virtual DateTime? LastLoginTime { get; set; }

        public virtual int CompareTo(User other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public User()
            : base() {
            
            Username = string.Empty;
            Password = string.Empty;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            GoogleCalendarId = string.Empty;
            UserType = UserType.None;
        }

        public override string ToString() {
            string name =
                (MiddleName != null && MiddleName.Trim().Length > 0) ?
                string.Format("{0} {1} {2}", FirstName, MiddleName, LastName) :
                string.Format("{0} {1}", FirstName, LastName);

            return Title != null ?
                string.Format("{0} {1}", Title.DisplayName, name) :
                name;
        }
    }

    //Extension Class
    public static class UserExtension {
        public static bool HasMandatoryValues(this User user) {
            return user.Username != null && user.Username.Trim().Length > 0 &&
                user.Password != null && user.Password.Length > 0 &&
                user.FirstName != null && user.FirstName.Trim().Length > 0 &&
                user.LastName != null && user.LastName.Trim().Length > 0 &&
                user.UserType != UserType.None;
        }
    }
}
