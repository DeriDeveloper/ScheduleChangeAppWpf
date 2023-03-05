using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ScheduleChangeAppWpf.Services
{
    internal class Converter
    {
        static internal Brush ToBrushByHex(string hex)
        {
            return (Brush)new BrushConverter().ConvertFrom(hex);
        }
    }
}
