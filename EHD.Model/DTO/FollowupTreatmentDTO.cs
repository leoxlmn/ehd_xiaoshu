using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.DTO {
    public class FollowupTreatmentDTO : IComparable {
        public int Id { get; set; }
        public int Version { get; set; }
        public string TherapistDisplayName {
            get {
                string name =
                    (TherapistMiddleName != null && TherapistMiddleName.Trim().Length > 0) ?
                    string.Format("{0} {1} {2}", TherapistFirstName, TherapistMiddleName, TherapistLastName) :
                    string.Format("{0} {1}", TherapistFirstName, TherapistLastName);

                return TherapistTitle != null && TherapistTitle.Trim().Length > 0 ?
                    string.Format("{0} {1}", TherapistTitle, name) :
                    name;
            }
        }
        public DateTime TreatmentTime { get; set; }
        public int Duration { get; set; }
        public string Note { get; set; }
        public bool IsOpenToTherapist { get; set; }
        public bool IsCompleted { get; set; }

        public int InitialTreatmentId { get; set; }
        public string TherapistFirstName { get; set; }
        public string TherapistLastName { get; set; }
        public string TherapistMiddleName { get; set; }
        public string TherapistTitle { get; set; }



        public override string ToString() {
            return string.Format("{0} - {1}", TreatmentTime, Duration);
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
            var that = obj as FollowupTreatmentDTO;
            if (that == null) return false;

            if (Id == 0 && that.Id == 0) {
                return ReferenceEquals(this, that);
            }

            return Id == that.Id;
        }

        public static bool operator ==(FollowupTreatmentDTO lhs, FollowupTreatmentDTO rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(FollowupTreatmentDTO lhs, FollowupTreatmentDTO rhs) {
            return !Equals(lhs, rhs);
        }

        int IComparable.CompareTo(object obj) {
            if (obj == null) return 0;

            return TreatmentTime.CompareTo((obj as FollowupTreatmentDTO).TreatmentTime);
        }
    }
}
