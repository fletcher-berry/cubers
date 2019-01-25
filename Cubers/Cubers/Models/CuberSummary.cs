using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubers.Models
{
    public class CuberSummary
    {
        public List<CuberSummaryEntry> Cubers { get; set; }
        public CuberSummaryLeaders Leaders { get; set; }    
    }
}
