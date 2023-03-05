using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleChangeAppWpf.Types
{
    public class Enums
    {
        public enum CellScheduleType : int
        {
            common,
            numerator,
            denominator,
        }

        public enum StatusOperation : int
        {
            Ok,
            Error,
            InvalidFormat
        }
        public enum CellScheduleExamType : int
        {
            Сonsultation,
            Exam
        }
    }
}
