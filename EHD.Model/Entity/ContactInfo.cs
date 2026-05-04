using System;
using System.Text;

namespace EHD.Model.Entity {
    [Serializable]
    public class ContactInfo : IComparable{
        public virtual Address Address { get; set; }
        public virtual string HomePhone { get; set; }
        public virtual string CellPhone { get; set; }
        public virtual string HomeFax { get; set; }
        public virtual string Email { get; set; }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            if(Address != null) {
                sb.AppendLine(Address.ToString());
            }

            if(HomePhone != null && HomePhone.Trim().Length > 0) {
                sb.Append("Home Phone: ");
                sb.AppendLine(HomePhone);
            }

            if(CellPhone != null && CellPhone.Trim().Length > 0) {
                sb.Append("Cell Phone: ");
                sb.AppendLine(CellPhone);
            }

            if(HomeFax != null && HomeFax.Trim().Length > 0) {
                sb.Append("Home Fax: ");
                sb.AppendLine(HomeFax);
            }

            if(Email != null && Email.Trim().Length > 0) {
                sb.Append("Email: ");
                sb.AppendLine(Email);
            }

            return sb.ToString();
        }

        public int CompareTo(object obj) {
            if(obj == null) return 0;

            return this.ToString().CompareTo((obj as ContactInfo).ToString());
        }
    }

    [Serializable]
    public class Address {
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string PostalCode { get; set; }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();

            if(AddressLine1 != null && AddressLine1.Trim().Length > 0) {
                //sb.Append("Address Line 1: ");
                sb.AppendLine(AddressLine1);
            }

            if(AddressLine2 != null && AddressLine2.Trim().Length > 0) {
                //sb.Append("Address Line 2: ");
                sb.AppendLine(AddressLine2);
            }

            if (PostalCode != null && PostalCode.Trim().Length > 0) {
                //sb.Append("Postal Code: ");
                sb.AppendLine(PostalCode);
            }

            return sb.ToString();
        }
    }
}
