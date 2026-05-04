using System;
using System.Text;

namespace EHD.PureNHibernateModel.Entity {
    [Serializable]
    public class ContactInfo {
        public virtual Address Address { get; set; }
        public virtual string HomePhone { get; set; }
        public virtual string CellPhone { get; set; }
        public virtual string HomeFax { get; set; }
        public virtual string Email { get; set; }
    }

    [Serializable]
    public class Address {
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
    }
}
