using System;
using System.Collections.Generic;

namespace EHD.Model {

    /// <summary>
    /// Contains an ordered list of TimeRange.
    /// </summary>
    public class TimeRangeCollection {

        private IList<TimeRange> _list = new List<TimeRange>();

        public IList<TimeRange> GetList() {
            return _list;
        }

        /// <summary>
        /// Add a new TimeRange to the list. Duplicated and overlapped time range will be merged.
        /// If TimeRange's duration is 0, it will be ignored.
        /// The TimeRange list is in ascending order.
        /// </summary>
        /// <param name="timeRange"></param>
        public void Add(TimeRange timeRange) {
            if (timeRange == null || timeRange.StartTime == timeRange.EndTime) return;

            TimeRange newTimeRange = new TimeRange(timeRange.StartTime, timeRange.EndTime);

            if (_list.Count <= 0) {
                _list.Add(newTimeRange);
            } else {

                List<TimeRange> _newList = new List<TimeRange>();

                bool newTimeRangeAdded = false;

                for (int i=0; i<_list.Count; i++) {

                    TimeRange item = _list[i];

                    if(newTimeRangeAdded){
                        _newList.Add(item);
                        continue;
                    }

                    if (item.EndTime < newTimeRange.StartTime) {
                        _newList.Add(item);
                    } else if (item.EndTime == newTimeRange.StartTime) {
                        newTimeRange.Merge(item);
                    } else if (item.StartTime > newTimeRange.EndTime) {
                        _newList.Add(newTimeRange);
                        _newList.Add(item);
                        newTimeRangeAdded = true;
                    } else if (item.StartTime == newTimeRange.EndTime) {
                        _newList.Add(new TimeRange(newTimeRange.StartTime, item.EndTime));
                        newTimeRangeAdded = true;
                    } else if (newTimeRange.StartTime <= item.StartTime && newTimeRange.EndTime <= item.EndTime) {
                        _newList.Add(new TimeRange(newTimeRange.StartTime, item.EndTime));
                        newTimeRangeAdded = true;
                    } else if (newTimeRange.StartTime <= item.StartTime && newTimeRange.EndTime > item.EndTime) {
                        //_newList.Add(newTimeRange);
                    } else if (newTimeRange.StartTime > item.StartTime && newTimeRange.EndTime <= item.EndTime) {
                        _newList.Add(item);
                        newTimeRangeAdded = true;
                    } else if (newTimeRange.StartTime > item.StartTime && newTimeRange.EndTime > item.EndTime) {
                        newTimeRange.Merge(item);
                    }
                }

                if (!newTimeRangeAdded) {
                    _newList.Add(newTimeRange);
                }

                _list = _newList;
            }
        }

        /// <summary>
        /// Add TimeRanges to the list. Duplicated and overlapped time range will be merged.
        /// If TimeRange's duration is 0, it will be ignored.
        /// The TimeRange list is in ascending order.
        /// </summary>
        /// <param name="timeRange"></param>
        public void AddRange(IEnumerable<TimeRange> timeRanges) {
            foreach (TimeRange timeRange in timeRanges) {
                Add(timeRange);
            }
        }

        /// <summary>
        /// Crop the given time range from the TimeRange list.
        /// </summary>
        /// <param name="timeRange"></param>
        public void Crop(TimeRange timeRange) {

            if (timeRange != null) {

                List<TimeRange> _newList = new List<TimeRange>();
                
                foreach (TimeRange item in _list) {
                    _newList.AddRange(item.Crop(timeRange));
                }

                _list = _newList;
            }

        }

        /// <summary>
        /// Check if the TimeRange list covers (contains) the given TimeRange
        /// </summary>
        /// <param name="timeRange"></param>
        /// <returns></returns>
        public bool Cover(TimeRange timeRange) {
            bool ret = false;

            if (timeRange != null) {
                foreach (TimeRange item in _list) {
                    if (item.Cover(timeRange)) {
                        ret = true;
                        break;
                    }
                }
            }

            return ret;
        }

        public void Clear() {
            _list.Clear();
        }
    }
}
