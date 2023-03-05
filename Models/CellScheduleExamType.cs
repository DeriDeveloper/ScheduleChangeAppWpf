using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleChangeAppWpf.Models
{
    internal class CellScheduleExamType
    {
        public Types.Enums.CellScheduleExamType Type { get; set; }
        public string Name
        {
            get
            {
                return Services.Background.Worker.GetCellScheduleExamType(Type);
            }
        }
    }
}
