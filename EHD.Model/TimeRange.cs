using System;
using System.Collections.Generic;

namespace EHD.Model {
    public class TimeRange {

        private DateTime _startTime;
        private DateTime _endTime;

        public DateTime StartTime { get { return _startTime; } }
        public DateTime EndTime { get { return _endTime; } }
        public TimeSpan Duration { get { return _endTime - _startTime; } }

        public TimeRange(DateTime dt1, DateTime dt2) {

            if (dt1 > dt2) {
                _startTime = dt2;
                _endTime = dt1;
            } else {
                _startTime = dt1;
                _endTime = dt2;
            }
        }

        /// <summary>
        /// Check if two TimeRanges overlap each other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Overlap(TimeRange other) {
            bool ret = false;

            if (other != null) {
                ret = !(other.EndTime <= this.StartTime ||
                    this.EndTime <= other.StartTime);
            }

            return ret;
        }

        /// <summary>
        /// Check if the TimeRange covers (contains) the given TimeRange
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Cover(TimeRange other) {
            bool ret = false;

            if (other != null) {
                ret = (this.StartTime <= other.StartTime &&
                    this.EndTime >= other.EndTime);
            }

            return ret;
        }

        /// <summary>
        /// Check if the two TimeRanges are connected (start time is the same as the other's end time)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Connect(TimeRange other) {
            bool ret = false;

            if (other != null) {
                ret = (this.StartTime == other.EndTime ||
                    this.EndTime == other.StartTime);
            }

            return ret;
        }

        /// <summary>
        /// Check if one of the two TimeRanges covers the other
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool FullyOverlap(TimeRange other) {
            bool ret = false;

            if (other != null) {
                ret = (this.Cover(other) ||
                    other.Cover(this));
            }

            return ret;
        }

        /// <summary>
        /// If two TimeRange overlaps each other, merge them together to create a larger TimeRange.
        /// If they do not overlaps, do nothing.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public void Merge(TimeRange other) {
            if (!this.Overlap(other) && !this.Connect(other)) return;

            _startTime = (_startTime < other.StartTime ? _startTime : other.StartTime);
            _endTime = (_endTime > other.EndTime ? _endTime : other.EndTime);
        }

        /// <summary>
        /// If the other TimeRange overlaps this TimeRange, remove the overlapped range from this TimeRange.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Return one or two TimeRanges in a list</returns>
        public IList<TimeRange> Crop(TimeRange other) {

            IList<TimeRange> timeRanges = new List<TimeRange>();

            if(!this.Overlap(other)){
                timeRanges.Add(this);
            } else if (this._startTime < other.StartTime && this._endTime > other.EndTime) {
                timeRanges.Add(new TimeRange(this._startTime, other.StartTime));
                timeRanges.Add(new TimeRange(other.EndTime, this._endTime));
            } else if (this._startTime >= other.StartTime && this._endTime <= other.EndTime) {
                timeRanges.Add(new TimeRange(this._startTime, this._startTime));//Duration = 0
            } else if (other.EndTime > this._startTime && other.EndTime < this._endTime) {
                timeRanges.Add(new TimeRange(other.EndTime, this._endTime));
            } else if(other.StartTime > this._startTime && other.StartTime < this._endTime ){
                timeRanges.Add(new TimeRange(this._startTime, other._startTime));
            }

            return timeRanges;
        }
    }
}
