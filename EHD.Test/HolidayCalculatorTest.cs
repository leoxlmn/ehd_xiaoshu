using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HolidayCalculator;

namespace EHD.Test {
    [TestClass]
    public class HolidayCalculatorTest {
        [TestMethod]
        public void ListHolidaysFor2012() {
            Console.WriteLine("List all holidays in year 2012.");
            try {
                IEnumerable<HolidayInfo> holidays = HolidayCalculator.CanadaHolidays.GetHolidaysByYear(2012);
                foreach(HolidayInfo day in holidays) {
                    Console.WriteLine(day.HolidayName + ": " + day.ActualHolidayDate.ToShortDateString() + (day.OfficalHolidayDate.HasValue ? "(" + day.OfficalHolidayDate.Value.ToShortDateString() + ")" : ""));
                }
            } catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        [TestMethod]
        public void ListHolidaysFor2011() {
            Console.WriteLine("List all holidays in year 2011.");
            try {
                IEnumerable<HolidayInfo> holidays = HolidayCalculator.CanadaHolidays.GetHolidaysByYear(2011);
                foreach(HolidayInfo day in holidays) {
                    Console.WriteLine(day.HolidayName + ": " + day.ActualHolidayDate.ToShortDateString() + (day.OfficalHolidayDate.HasValue ? "(" + day.OfficalHolidayDate.Value.ToShortDateString() + ")" : ""));
                }
            } catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        [TestMethod]
        public void ListHolidaysFor2010() {
            Console.WriteLine("List all holidays in year 2010.");
            try {
                IEnumerable<HolidayInfo> holidays = HolidayCalculator.CanadaHolidays.GetHolidaysByYear(2012);
                foreach(HolidayInfo day in holidays) {
                    Console.WriteLine(day.HolidayName + ": " + day.ActualHolidayDate.ToShortDateString() + (day.OfficalHolidayDate.HasValue ? "(" + day.OfficalHolidayDate.Value.ToShortDateString() + ")" : ""));
                }
            } catch(Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
