using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EHD.Model.Entity;

namespace EHD.Model.DTO {
    public class InvoiceSearchResultDTO {
        //Patient Info
        public int PatientId { get; set; }
        public int PatientVersion { get; set; }
        public Sex Sex { get; set; }
        public string FileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Insurer { get; set; }

        //Invoice Info
        public int? InvoiceId { get; set; }
        public int? InvoiceVersion { get; set; }
        public string TreatmentType { get; set; }
        public string TherapistFirstName { get; set; }
        public string TherapistLastName { get; set; }
        public string TherapistMiddleName { get; set; }
        public string TherapistTitle { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? StatementDate { get; set; }
        public double? TaxRate { get; set; }

        //Invoice Item Info
        public int? InvoiceItemId { get; set; }
        public int? InvoiceItemVersion { get; set; }
        public DateTime? ServiceDate { get; set; }
        public int? ServiceDuration { get; set; }
        public string ServiceDescription { get; set; }
        public double? Amount { get; set; }

    }
}
