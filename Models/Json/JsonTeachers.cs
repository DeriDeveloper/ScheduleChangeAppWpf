using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleChangeAppWpf.Models.Json
{
    internal class JsonTeachers
    {
        public class Root
        {
            public List<Models.Teacher> Teachers { get; set; }
        }
    }
}
