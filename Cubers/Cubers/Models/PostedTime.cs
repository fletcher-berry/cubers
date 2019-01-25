using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubers.Models
{
    public class PostedTime
    {
        public int CuberId { get; set; }
        public string Event { get; set; }
        public double Time { get; set; }
    }
}
