
/*
 * Canada Holidays
 * --------------------------------------------------------------
 * New Year				Jan 1  (Move to the next Monday if on Sat/Sun)
 * Family Day			Third Monday in Feb
 * Good Friday			3 days before Easter Monday
 * Easter				Needs a special method to calculate (refer to http://leo-xie.blogspot.ca/2012/09/how-to-calculate-date-of-canada-easter.html)
 * Victoria Day			Monday on or before May 24
 * Canada Day			July 1 (Move to the next Monday if on Sat/Sun)
 * Civic Holiday		First Monday in August
 * Labour Day			First Monday in September
 * Thanksgiving			Second Monday in October
 * Remembrance Day		Nov 11 (Move to the next Monday if on Sat/Sun)
 * Christmas Day		Dec 25 (Move to the next Monday if on Sat/Sun)
 * Boxing Day			The next day of Christmas (Move to the next Monday if on Sat/Sun)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HolidayCalculator {
    public static class CanadaHolidays {
        public static IEnumerable<HolidayInfo> GetHolidaysByYear(int year) {
            List<HolidayInfo> holidays = new List<HolidayInfo>();

            holidays.Add(GetNewYearDay(year));
            holidays.Add(GetFamilyDay(year));
            holidays.Add(GetGoodFriday(year));
            holidays.Add(GetEasterMonday(year));
            holidays.Add(GetVictoriaDay(year));
            holidays.Add(GetCanadaDay(year));
            holidays.Add(GetCivicHoliday(year));
            holidays.Add(GetLabourDay(year));
            holidays.Add(GetThanksgiving(year));
            holidays.Add(GetRemembranceDay(year));
            holidays.Add(GetChristmasDay(year));
            holidays.Add(GetBoxingDay(year));

            return holidays;
        }

        //Check if the given date is a Canada holiday
        public static bool IsHoliday(DateTime date){
            foreach (HolidayInfo holiday in GetHolidaysByYear(date.Year)) {
                if (date.Date == holiday.ActualHolidayDate.Date ||
                   (holiday.OfficalHolidayDate.HasValue && date.Date == holiday.OfficalHolidayDate.Value)) {
                       return true;
                }
            }

            return false;
        }

        public static HolidayInfo GetHolidayBy(DateTime date) {

            foreach (HolidayInfo holiday in GetHolidaysByYear(date.Year)) {
                if (date.Date == holiday.ActualHolidayDate.Date ||
                   (holiday.OfficalHolidayDate.HasValue && date.Date == holiday.OfficalHolidayDate.Value)) {
                       return holiday;
                }
            }

            return null;
        }

        public static HolidayInfo GetNewYearDay(int year) {
            return new HolidayInfo(
                "New Year", 
                new DateTime(year, 1, 1), 
                getNextMondayIfSaturdaySunday(new DateTime(year, 1, 1)));
        }

        public static HolidayInfo GetFamilyDay(int year) {
            return new HolidayInfo(
                "Family Day",
                null,
                getWeekDayInMonth(year, 2, DayOfWeek.Monday, 2));
        }

        public static HolidayInfo GetGoodFriday(int year) {
            return new HolidayInfo(
                "Good Friday",
                null,
                GetEasterMonday(year).ActualHolidayDate.AddDays(-3));
        }

        public static HolidayInfo GetEasterMonday(int year) {
            int gn, xx, cy, qq, DM, dateOffset;
            gn = year % 19;
            xx = year/100;
            qq = 3*(xx+1)/4;
            cy = qq - (13+xx*8)/25;
            xx = ( 6 + (5*year/4) - qq ) % 7;
            DM = 21 + (gn*19 + cy + 15)%30; 
            if(gn > 10){
                if(DM > 48) DM--;
            }else{
                if(DM > 49) DM--;
            }
            dateOffset = DM + 1 + (66 - xx - DM) % 7;

            return new HolidayInfo(
                "Easter Monday",
                null,
                (new DateTime(year, 3, 1)).AddDays(dateOffset));
        }

        public static HolidayInfo GetVictoriaDay(int year) {
            return new HolidayInfo(
                "Victoria Day",
                new DateTime(year, 5, 24),
                getNearestWeekDay(new DateTime(year, 5, 24), DayOfWeek.Monday, true));
        }

        public static HolidayInfo GetCanadaDay(int year) {
            return new HolidayInfo(
                "Canada Day",
                new DateTime(year, 7, 1),
                getNextMondayIfSaturdaySunday(new DateTime(year, 7, 1)));
        }

        public static HolidayInfo GetCivicHoliday(int year) {
            return new HolidayInfo(
                "Civic Day",
                null,
                getWeekDayInMonth(year, 8, DayOfWeek.Monday, 0));
        }

        public static HolidayInfo GetLabourDay(int year) {
            return new HolidayInfo(
                "Labour Day",
                null,
                getWeekDayInMonth(year, 9, DayOfWeek.Monday, 0));
        }

        public static HolidayInfo GetThanksgiving(int year) {
            return new HolidayInfo(
                "Thanksgiving",
                null,
                getWeekDayInMonth(year, 10, DayOfWeek.Monday, 1));
        }

        public static HolidayInfo GetRemembranceDay(int year) {
            return new HolidayInfo(
                "Remembrance Day",
                new DateTime(year, 11, 11),
                getNextMondayIfSaturdaySunday(new DateTime(year, 11, 11)));
        }

        public static HolidayInfo GetChristmasDay(int year) {
            return new HolidayInfo(
                "Christmas Day",
                new DateTime(year, 12, 25),
                getNextMondayIfSaturdaySunday(new DateTime(year, 12, 25)));
        }

        public static HolidayInfo GetBoxingDay(int year) {
            return new HolidayInfo(
                "Boxing Day",
                new DateTime(year, 12, 26),
                getNextMondayIfSaturdaySunday(GetChristmasDay(year).ActualHolidayDate.AddDays(1)));
        }

        /// <summary>
        /// Return the next Monday if it's Saturday or Sunday. If it's not Saturday nor Sunday. Return itself.
        /// </summary>
        /// <param name="dt">A date</param>
        /// <returns></returns>
        private static DateTime getNextMondayIfSaturdaySunday(DateTime dt) {
            DateTime ret = dt;

            if(dt.DayOfWeek == DayOfWeek.Saturday) {
                ret = dt.AddDays(2);
            } else if(dt.DayOfWeek == DayOfWeek.Sunday) {
                ret = dt.AddDays(1);
            }

            return ret;
        }

        /// <summary>
        /// Return the nearest weekday by the given date.
        /// </summary>
        /// <param name="dt">A date.</param>
        /// <param name="dayOfWeek">DayOfWeek to return.</param>
        /// <param name="searchBackword">True to search backward (smaller date). False to search forward (greater date).</param>
        /// <returns></returns>
        private static DateTime getNearestWeekDay(DateTime dt, DayOfWeek dayOfWeek, bool searchBackward) {
            DateTime ret = dt;

            while(ret.DayOfWeek != dayOfWeek) {
                ret = ret.AddDays(searchBackward ? -1 : 1);
            }

            return ret;
        }

        /// <summary>
        /// Return a date by the given year, month and DayOfWeek is dayOfWeek
        /// </summary>
        /// <param name="year">Year of the date.</param>
        /// <param name="month">Month the date. Through 1 to 12.</param>
        /// <param name="dayOfWeek">DayOfWeek of the date.</param>
        /// <param name="weekOffset">The sequence number of the week. 0 - the first week.</param>
        /// <returns></returns>
        private static DateTime getWeekDayInMonth(int year, int month, DayOfWeek dayOfWeek, int weekOffset) {
            DateTime ret = new DateTime(year, month, 1);

            //Get the first day which is on dayOfWeek
            while(ret.DayOfWeek != dayOfWeek) {
                ret = ret.AddDays(1);
            }

            //Apply the weekOffset
            if(weekOffset != 0) {
                ret = ret.AddDays(weekOffset * 7);
            }

            return ret;
        }
    }
}
