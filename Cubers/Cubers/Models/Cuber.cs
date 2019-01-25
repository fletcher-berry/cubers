using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubers.Models
{
    public class Cuber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<double> Solves3x3 { get; set; }
        public List<double> SolvesOh { get; set; }
        public List<double> Solves4x4 { get; set; }
        public List<CuberMetadata> Metadata { get; set; }

        public Cuber()
        {
            Solves3x3 = new List<double>();
            SolvesOh = new List<double>();
            Solves4x4 = new List<double>();
            Metadata = new List<CuberMetadata>();
        }

    }
}
