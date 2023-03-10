using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleChangeAppWpf.Models
{
    public class TimesPair
    {
        public int Id { get; set; }
        public System.DayOfWeek DayOfWeek { get; set; }
        public int NumberPair { get; set; }
        public long TicksTimeStart
        {
            get
            {
                return TimeStart.Ticks;
            }
            set
            {
                TimeStart = new DateTime(value);
            }
        }
        public DateTime TimeStart { get; set; }
        public long TicksTimeEnd
        {
            get
            {
                return TimeEnd.Ticks;
            }
            set
            {
                TimeEnd = new DateTime(value);
            }
        }
        public DateTime TimeEnd { get; set; }
        public int? EducationalInstitutionId { get; set; }
        public EducationalInstitution? EducationalInstitution { get; set; }

        public DateTime? Date { get; set; }
        public bool IsChange { get; set; }

    }
}
