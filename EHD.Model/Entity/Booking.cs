using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class Booking : EntityBase, IComparable<Booking> {

        public virtual Patient Patient { get; set; }
        public virtual User Therapist { get; set; }
        public virtual TherapyType TreatmentType { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime {get;set;}
        public virtual string Note { get; set; }

        public Booking() : base() { }

        public virtual int CompareTo(Booking other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public override string ToString() {
            return string.Format("{0:hh:mm tt} - {1:hh:mm tt}", StartTime, EndTime);
        }
    }

    //Extension Class
    public static class BookingExtension {
        public static bool HasMandatoryValues(this Booking booking) {
            return booking.Patient != null &&
                booking.Therapist != null &&
                booking.TreatmentType != null;
        }
    }
}
