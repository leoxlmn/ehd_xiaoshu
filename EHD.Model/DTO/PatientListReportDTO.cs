using EHD.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.DTO {
    public class PatientListReportDTO : IComparable {
        public string FileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string DateOfBirth { get; set; }

        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string Insurer { get; set; }

        public override string ToString() {
            return $"{FirstName} {LastName}";
        }

        private int? oldHashCode;

        public override int GetHashCode() {
            if (oldHashCode.HasValue) return oldHashCode.Value;

            if (String.IsNullOrEmpty(FileNumber)) {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }

            return FileNumber.GetHashCode();
        }

        public override bool Equals(object obj) {
            var that = obj as PatientListReportDTO;
            if (that == null) return false;

            if (String.IsNullOrEmpty(FileNumber) && String.IsNullOrEmpty(that.FileNumber)) {
                return ReferenceEquals(this, that);
            }

            return String.Equals(FileNumber, that.FileNumber, StringComparison.OrdinalIgnoreCase);
        }

        public static bool operator ==(PatientListReportDTO lhs, PatientListReportDTO rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(PatientListReportDTO lhs, PatientListReportDTO rhs) {
            return !Equals(lhs, rhs);
        }

        int IComparable.CompareTo(object obj) {
            if (obj == null) return 0;

            return this.ToString().Equals((obj as PatientListReportDTO).ToString())
                ? this.FileNumber.CompareTo((obj as PatientListReportDTO).FileNumber)
                : this.ToString().CompareTo((obj as PatientListReportDTO).ToString());
        }
    }
}
