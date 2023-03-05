using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleChangeAppWpf.Models
{
    public class CellScheduleExam
    {
        public int Id { get; set; }
        public Types.Enums.CellScheduleExamType CellScheduleExamType { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int AudienceId { get; set; }
        public Audience Audiences { get; set; }
        public DateTime Time { get; set; }
        public DateTime Date { get; set; }
    }
}
