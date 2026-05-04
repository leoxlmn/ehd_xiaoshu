using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.DTO {
    public class TherapistWeeklyAvailDTO {
        public int Id { get; set; }
        public int TherapistId { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public bool IsFullDay { get; set; }
        public decimal StartHour { get; set; }
        public decimal EndHour { get; set; }
    }
}
