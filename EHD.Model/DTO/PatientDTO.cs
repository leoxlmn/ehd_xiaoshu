using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EHD.Model.Entity;

namespace EHD.Model.DTO {
    public class PatientDTO : IComparable {
        public int Id { get; set; }
        public int Version { get; set; }
        public int? FamilyPatientId { get; set; }
        public string FileNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string HomeFax { get; set; }
        public string Email { get; set; }
        public string ContactInfo {
            get {
                StringBuilder sb = new StringBuilder();

                if (Address1 != null && Address1.Trim().Length > 0) {
                    sb.AppendLine(Address1);
                    sb.Append(" ");
                }

                if (Address2 != null && Address2.Trim().Length > 0) {
                    sb.AppendLine(Address2);
                    sb.Append(" ");
                }

                if (PostalCode != null && PostalCode.Trim().Length > 0) {
                    sb.AppendLine(PostalCode);
                    sb.Append(" ");
                }

                if (HomePhone != null && HomePhone.Trim().Length > 0) {
                    sb.Append("Home Phone: ");
                    sb.AppendLine(HomePhone);
                    sb.Append(" ");
                }

                if (CellPhone != null && CellPhone.Trim().Length > 0) {
                    sb.Append("Cell Phone: ");
                    sb.AppendLine(CellPhone);
                    sb.Append(" ");
                }

                if (HomeFax != null && HomeFax.Trim().Length > 0) {
                    sb.Append("Home Fax: ");
                    sb.AppendLine(HomeFax);
                    sb.Append(" ");
                }

                if (Email != null && Email.Trim().Length > 0) {
                    sb.Append("Email: ");
                    sb.AppendLine(Email);
                }

                return sb.ToString();
            }
        }

        public string Note { get; set; }
        public uint Age {
            get {
                int age = 0;

                DateTime dtNow = DateTime.Now;
                if (DateOfBirth != null && dtNow > DateOfBirth) {
                    age = dtNow.Year - DateOfBirth.Year;
                    if (dtNow.Month < DateOfBirth.Month) {
                        age--;
                    } else if (dtNow.Month == DateOfBirth.Month && dtNow.Day < DateOfBirth.Day) {
                        age--;
                    }
                    if (age < 0) age = 0;
                }

                return (uint)age;
            }
        }
        public Sex Sex { get; set; }
        public string Insurer { get; set; }

        public string DisplayName {
            get {
                return
                    (MiddleName != null && MiddleName.Trim().Length > 0) ?
                    string.Format("{0} {1} {2}", FirstName, MiddleName, LastName) :
                    string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public IList<InvoiceDTO> Invoices { get; set; }

        public IList<InitialTreatmentDTO> InitialTreatments { get; set; }

        public override string ToString() {
            return DisplayName;
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
            var that = obj as PatientDTO;
            if (that == null) return false;

            if (Id == 0 && that.Id == 0) {
                return ReferenceEquals(this, that);
            }

            return Id == that.Id;
        }

        public static bool operator ==(PatientDTO lhs, PatientDTO rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(PatientDTO lhs, PatientDTO rhs) {
            return !Equals(lhs, rhs);
        }

        int IComparable.CompareTo(object obj) {
            if (obj == null) return 0;

            return DisplayName.CompareTo((obj as PatientDTO).DisplayName);
        }
    }
}
