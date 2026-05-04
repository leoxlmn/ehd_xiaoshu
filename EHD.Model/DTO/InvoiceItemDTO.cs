using NHibernate.Event.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.DTO {
    public class InvoiceItemDTO {
        public int Id { get; set; }
        public int Version { get; set; }
        public int InvoiceId { get; set; }
        public DateTime ServiceDate { get; set; }
        public int? ServiceDuration { get; set; }
        public string ServiceDescription { get; set; }
        public double Amount { get; set; }
        public double TaxRate { get; set; }
    }
}
