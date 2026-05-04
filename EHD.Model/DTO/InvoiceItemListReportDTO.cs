using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHD.Model.DTO {
    public class InvoiceItemListReportDTO : IComparable {

        public int Sequence { get; set; }
        public string Date { get; set; }
        public string ServiceProvided { get; set; }
        public string Duration { get; set; }
        public string TimeIn { get; set; }
        public string Amount { get; set; }
        public string AmountAfterTax { get; set; }

        public string Initial { get; set; }
        public string Note { get; set; }

        public override string ToString() {
            return $"{Date} {ServiceProvided}";
        }

        private int? oldHashCode;

        public override int GetHashCode() {
            if (oldHashCode.HasValue) return oldHashCode.Value;

            if (Sequence == 0) {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }

            return Sequence.GetHashCode();
        }

        public override bool Equals(object obj) {
            var that = obj as InvoiceItemListReportDTO;
            if (that == null) return false;

            if (Sequence == 0 && that.Sequence == 0) {
                return ReferenceEquals(this, that);
            }

            return Sequence == that.Sequence;
        }

        public static bool operator ==(InvoiceItemListReportDTO lhs, InvoiceItemListReportDTO rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(InvoiceItemListReportDTO lhs, InvoiceItemListReportDTO rhs) {
            return !Equals(lhs, rhs);
        }

        int IComparable.CompareTo(object obj) {
            if (obj == null) return 0;

            return Sequence.CompareTo((obj as InvoiceItemListReportDTO).Sequence);
        }
    }
}
