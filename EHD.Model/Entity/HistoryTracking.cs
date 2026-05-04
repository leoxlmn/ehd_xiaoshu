using System;

namespace EHD.Model.Entity {
    [Serializable]
    public class HistoryTracking : IComparable<HistoryTracking> {
        private int? oldHashCode;

        public override int GetHashCode() {
            if(oldHashCode.HasValue) return oldHashCode.Value;

            if(Id == 0) {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }

            return Id.GetHashCode();
        }

        /// <summary>
        /// Primary Key Field
        /// </summary>
        public virtual int Id { get; protected set; }

        public override bool Equals(object obj) {
            var that = obj as HistoryTracking;
            if(that == null) return false;

            if(Id == 0 && that.Id == 0) {
                return ReferenceEquals(this, that);
            }

            return Id == that.Id;
        }

        public static bool operator ==(HistoryTracking lhs, HistoryTracking rhs) {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(HistoryTracking lhs, HistoryTracking rhs) {
            return !Equals(lhs, rhs);
        }

        public virtual string ObjectName { get; set; }
        public virtual string ObjectId { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual ActionType ActionType { get; set; }
        public virtual DateTime ActionTime { get; set; }
        public virtual string ActionBy { get; set; }

        public virtual int CompareTo(HistoryTracking other) {
            if(other == null) return 1;

            return this.ActionTime.CompareTo(other.ActionTime);
        }
    }


}
