using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Utility;

namespace EHD.Model.Entity {
    [Serializable]
    public class UserWeeklyAvailability : EntityBase, IComparable<UserWeeklyAvailability> {

        public virtual User Practitioner { get; set; }
        public virtual DayOfWeek WeekDay { get; set; }
        public virtual bool FullDayAvailable { get; set; }
        public virtual decimal StartHour { get; set; }
        public virtual decimal EndHour { get; set; }

        public virtual int CompareTo(UserWeeklyAvailability other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public UserWeeklyAvailability() : base() { }

        public override string ToString() {
            return string.Format("{0}-{1}-{2}",
                Practitioner != null ? Practitioner.DisplayName : string.Empty,
                WeekDay.ToString(),
                FullDayAvailable ? "Full Day" : string.Format("[{0} to {1}]", 
                    DateTimeHelper.DoubleHourToTime((double)StartHour, "h:mmtt"),
                    DateTimeHelper.DoubleHourToTime((double)EndHour, "h:mmtt"))
                );
        }
    }

    //Extension Class
    public static class UserWeeklyAvailabilityExtension {
        public static bool HasMandatoryValues(this UserWeeklyAvailability availability) {
            return availability.Practitioner != null;
        }
    }
}
