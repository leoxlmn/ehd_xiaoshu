using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EHD.Model.Entity;

namespace EHD.Model.DTO {
    public class InitialTreatmentSearchResultDTO {
        //Patient Info
        public int PatientId { get; set; }
        public int PatientVersion { get; set; }
        public Sex Sex { get; set; }
        public string FileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        //Initial Treatment Info
        public int TreatmentId { get; set; }
        public int TreatmentVersion { get; set; }
        public string TreatmentType { get; set; }
        public DateTime TreatmentTime { get; set; }
        public int Duration { get; set; }
        public bool IsCompleted { get; set; }

    }
}
