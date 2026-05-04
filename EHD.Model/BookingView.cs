using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model {
    public class BookingView {

        private int patientId;
        private int practitionerId;
        private int treatmentTypeId;

        public int Id { get; set; }
        public string Patient { get; set; }
        public string Practitioner { get; set; }
        public string TreatmentType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Note { get; set; }

        public void SetPatientId(int id) {
            patientId = id;
        }

        public int GetPatientId() {
            return patientId;
        }

        public void SetPractitionerId(int id) {
            practitionerId = id;
        }

        public int GetPractitionerId() {
            return practitionerId;
        }

        public void SetTreatmentTypeId(int id) {
            treatmentTypeId = id;
        }

        public int GetTreatmentTypeId() {
            return treatmentTypeId;
        }
    }
}
