using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace EHD.Model.Entity {
    [Serializable]
    public class UserTimeOff : EntityBase, IComparable<UserTimeOff> {

        public virtual User Practitioner { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual string Reason { get; set; }

        public virtual int CompareTo(UserTimeOff other) {
            if(other == null) return 0;

            return this.DisplayName.CompareTo(other.DisplayName);
        }

        public UserTimeOff() : base() { }

        public override string ToString() {
            return string.Format("{0}-[{1} to {2}]",
                Practitioner != null ? Practitioner.DisplayName : string.Empty,
                StartTime.ToString(Constant.Constants.DATE_TIME_FORMAT),
                EndTime.ToString(Constant.Constants.DATE_TIME_FORMAT)
                );
        }
    }

    //Extension Class
    public static class UserTimeOffExtension {
        public static bool HasMandatoryValues(this UserTimeOff timeoff) {
            return timeoff.Practitioner != null &&
                timeoff.StartTime != default(DateTime) &&
                timeoff.EndTime != default(DateTime);
        }
    }
}
