using System;
namespace Utility {
    public static class DateTimeHelper {
        public static DateTime DoubleHourToTime(double Hour) {
            DateTime dt = DateTime.Now.Date;

            if (Hour < 0 || Hour > 24) {
                throw new ArgumentException("Hour has to be between 0 and 24.");
            }

            int h = Convert.ToInt32(Math.Floor(Hour));
            int m = Convert.ToInt32(Math.Round((Hour - h) * 60));

            dt = dt.AddHours(h).AddMinutes(m);

            return dt;
        }

        public static string DoubleHourToTime(double Hour, string format) {
            return DoubleHourToTime(Hour).ToString(format);
        }
    }
}
