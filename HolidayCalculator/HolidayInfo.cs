using System;

namespace HolidayCalculator {
    public class HolidayInfo {
        /// <summary>
        /// The offical name of the holiday
        /// </summary>
        public string HolidayName { get; set; }

        /// <summary>
        /// The offical date of the holiday. Some holidays don't have a fixed offical date.
        /// </summary>
        public DateTime? OfficalHolidayDate { get; set; }

        /// <summary>
        /// When the offical holiday date is on Saturday or Sunday, the actual date will be moved to the nearest weekday, such as Monday or Friday.
        /// </summary>
        public DateTime ActualHolidayDate { get; set; }

        #region Constructor
        public HolidayInfo() { }
        public HolidayInfo(string holidayName, DateTime? officalDate, DateTime actualDate) {
            HolidayName = holidayName;
            OfficalHolidayDate = officalDate;
            ActualHolidayDate = actualDate;
        }
        #endregion Constructor
    }
}
