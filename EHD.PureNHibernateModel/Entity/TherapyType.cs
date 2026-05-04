using System;
using System.Collections.Generic;

namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public class TherapyType : EntityBase, IComparable<TherapyType> {
        public virtual string TherapyTypeName { get; set; }
        //public virtual IList<User> Therapists { get; set; }

        public TherapyType()
            : base() {
                //Therapists = new List<User>();
        }

        public virtual int CompareTo(TherapyType other) {
            if (other == null) return 1;

            return this.TherapyTypeName.CompareTo(other.TherapyTypeName);
        }

        public override string ToString() {
            return TherapyTypeName;
        }

    }
}
