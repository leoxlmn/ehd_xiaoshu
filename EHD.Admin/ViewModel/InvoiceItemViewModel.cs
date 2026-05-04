using System;

namespace EHD.Admin.ViewModel {
    public class InvoiceItemViewModel {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int PractitionerId { get; set; }
        public string PractitionerName { get; set; }
        public int TreatmentTypeId { get; set; }
        public string TreatmentTypeName { get; set; }
        public int InvoiceItemId { get; set; }
        public string Service { get; set; }
        public DateTime ServiceDate { get; set; }
    }
}
