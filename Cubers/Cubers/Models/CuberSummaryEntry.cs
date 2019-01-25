using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubers.Models
{
    public class CuberSummaryEntry
    {
        public int CuberId { get; set; }
        public string CuberName { get; set; }
        public double Pb3x3 { get; set; }
        public double PbOh { get; set; }
        public double Pb4x4 { get; set; }
    }
}
