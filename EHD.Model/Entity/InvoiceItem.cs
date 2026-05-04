using System;
using System.Collections.Generic;

namespace EHD.Model.Entity {
    [Serializable]
    public class InvoiceItem : EntityBase, IComparable<InvoiceItem> {

        public virtual Invoice Invoice{get;set;}
        public virtual string ServiceDescription { get; set; }
        public virtual DateTime ServiceDate { get; set; }
        public virtual int? ServiceDuration { get; set; }
        public virtual double Amount { get; set; }

        public InvoiceItem() : base() { }

        public virtual int CompareTo(InvoiceItem other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return ServiceDate.ToString("yyyy-MM-dd") + " - " + ServiceDescription + " - " + Amount.ToString();
        }
    }

    //Extension Class
    public static class InvoiceItemExtension {
        public static bool HasMandatoryValues(this InvoiceItem item) {
            return item.Invoice != null &&
                item.ServiceDescription != null && item.ServiceDescription.Length > 0;
        }
    }
}
