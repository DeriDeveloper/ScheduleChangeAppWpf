using System;
using System.Collections.Generic;

namespace ScheduleChangeAppWpf.Models
{
    public class CellSchedule
    {
        public int Id { get; set; }
        public int NumberPair { get; set; }
        public System.DayOfWeek DayOfWeek { get; set; }
        public int TimesPairId { get; set; }
        public TimesPair TimesPair { get; set; }
        public List<AcademicSubject> AcademicSubjects { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Audience> Audiences { get; set; }
        public Group Group { get; set; }
        public Types.Enums.CellScheduleType TypeCell { get; set; } = Types.Enums.CellScheduleType.common;
        public bool IsChange { get; set; }


        // change cell
        public DateTime? Date { get; set; }


        
    }
}
