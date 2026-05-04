using NHibernate.Event.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.DTO {
    public class InvoiceDTO {
        public int Id { get; set; }
        public int Version { get; set; }
        public int PatientId { get; set; }
        public string TreatmentType { get; set; }

        //Won't work for WithAlias(...)
        //public PeopleName TherapistName { get; set; }

        public string TherapistFirstName { get; set; }
        public string TherapistLastName { get; set; }
        public string TherapistMiddleName { get; set; }
        public string TherapistTitle { get; set; }

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

        public string InvoiceNumber { get; set; }
        public DateTime StatementDate { get; set; }

        public double? TaxRate { get; set; }

        public IList<InvoiceItemDTO> InvoiceItems { get; set; }
    }
}
