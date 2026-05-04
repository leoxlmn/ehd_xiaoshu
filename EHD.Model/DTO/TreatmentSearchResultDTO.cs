using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EHD.Model.Entity;

namespace EHD.Model.DTO {
    public class TreatmentSearchResultDTO {
        //Patient Info
        public int PatientId { get; set; }
        public int PatientVersion { get; set; }
        public Sex Sex { get; set; }
        public string FileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Insurer { get; set; }

        //Initial Treatment Info
        public int? InitialTreatmentId { get; set; }
        public int? InitialTreatmentVersion { get; set; }
        public string TreatmentType { get; set; }
        public string InitialTherapistFirstName { get; set; }
        public string InitialTherapistLastName { get; set; }
        public string InitialTherapistMiddleName { get; set; }
        public string InitialTherapistTitle { get; set; }
        public DateTime? InitialTreatmentTime { get; set; }
        public int InitialDuration { get; set; }

        //Followup Treatment Info
        public int? FollowupTreatmentId { get; set; }
        public int? FollowupTreatmentVersion { get; set; }
        public string FollowupTherapistFirstName { get; set; }
        public string FollowupTherapistLastName { get; set; }
        public string FollowupTherapistMiddleName { get; set; }
        public string FollowupTherapistTitle { get; set; }
        public DateTime? FollowupTreatmentTime { get; set; }
        public int? FollowupDuration { get; set; }


    }
}
