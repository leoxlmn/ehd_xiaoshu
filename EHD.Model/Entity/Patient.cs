using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class Patient : EntityBase, IComparable<Patient> {
        public virtual string FileNumber { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual uint Age {
            get {
                int age = 0;

                DateTime dtNow = DateTime.Now;
                if(DateOfBirth != null && dtNow > DateOfBirth) {
                    age = dtNow.Year - DateOfBirth.Year;
                    if(dtNow.Month < DateOfBirth.Month) {
                        age--;
                    } else if(dtNow.Month == DateOfBirth.Month && dtNow.Day < DateOfBirth.Day) {
                        age--;
                    }
                    if(age < 0) age = 0;
                }

                return (uint)age;
            }
        }
        public virtual Sex Sex { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
        public virtual Insurer Insurer { get; set; }
        public virtual string Note { get; set; }
        public virtual int? FamilyPatientId { get; set; }

        [ScriptIgnore]
        public virtual IList<Invoice> Invoices { get; set; }
        [ScriptIgnore]
        public virtual IList<InitialTreatment> InitialTreatments { get; set; }
        [ScriptIgnore]
        public virtual IList<AccountBalance> Deposits { get; set; }

        [ScriptIgnore]
        public virtual IList<Patient> FamilyMembers { get; set; }

        public Patient()
            : base() {

            ContactInfo = new ContactInfo() {
                Address = new Address()
            };

            Insurer = null;
            Sex = Sex.Unknown;
            DateOfBirth = DateTime.Now;
        }

        public virtual int CompareTo(Patient other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return
                (MiddleName != null && MiddleName.Trim().Length > 0) ?
                string.Format("{0} {1} {2}", FirstName, MiddleName, LastName) :
                string.Format("{0} {1}", FirstName, LastName);
        }

        public virtual string BillingAddress {
            get {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(DisplayName);
                if(ContactInfo != null && ContactInfo.Address != null) {
                    sb.AppendLine(ContactInfo.Address.AddressLine1);
                    sb.AppendLine(ContactInfo.Address.AddressLine2);
                }

                return sb.ToString();
            }
        }
    }

    //Extension Class
    public static class PatientExtension {
        public static bool HasMandatoryValues(this Patient patient) {
            return patient.FileNumber != null && patient.FileNumber.Length >= 4 &&
                patient.FirstName != null && patient.LastName.Trim().Length > 0;
        }
    }
}
