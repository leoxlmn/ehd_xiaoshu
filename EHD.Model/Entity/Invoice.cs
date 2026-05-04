using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class Invoice : EntityBase, IComparable<Invoice> {

        public virtual Patient Patient { get; set; }
        public virtual User Therapist { get; set; }
        //public virtual TherapistOrganizationRegistrationGroup RegistrationGroup { get; set; }
        public virtual string TherapistRegistration { get; set; }
        public virtual TherapyType TreatmentType { get; set; }

        public virtual string InvoiceNumber { get; set; }
        public virtual DateTime StatementDate { get; set; }
        public virtual string HSTNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string Note { get; set; }
        public virtual double? TaxRate { get; set; }
        public virtual string TaxName { get; set; }

        [ScriptIgnore]
        public virtual IList<InvoiceItem> InvoiceItems { get; set; }

        public Invoice() : base() { }

        public virtual int CompareTo(Invoice other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return InvoiceNumber;
        }
    }

    //Extension Class
    public static class InvoiceExtension {
        public static bool HasMandatoryValues(this Invoice invoice) {
            return invoice.Patient != null &&
                invoice.Therapist != null &&
                //invoice.RegistrationGroup != null &&
                invoice.TherapistRegistration != null && invoice.TherapistRegistration.Trim().Length > 0 &&
                invoice.InvoiceNumber != null && invoice.InvoiceNumber.Trim().Length > 0 &&
                invoice.TreatmentType != null &&
                invoice.Title != null && invoice.Title.Trim().Length > 0;
        }
    }
}
