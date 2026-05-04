using System;
using System.Collections.Generic;

namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public class Patient : EntityBase, IComparable<Patient> {
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }

        public virtual IList<InitialTreatment> InitialTreatments { get; set; }

        public Patient()
            : base() {
                InitialTreatments = new List<InitialTreatment>();
        }

        public virtual int CompareTo(Patient other) {
            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return
                (MiddleName != null && MiddleName.Trim().Length > 0) ?
                string.Format("{0} {1}, {2}", FirstName, MiddleName, LastName) :
                string.Format("{0}, {1}", FirstName, LastName);
        }
    }
}
