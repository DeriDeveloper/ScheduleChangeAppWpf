using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleChangeAppWpf.Models.Json
{
    internal class StatusOperation
    {
        public Types.Enums.StatusOperation Status { get; set; }
        public string Message { get; set; }
        public object? Object { get; set; }
    }
}
