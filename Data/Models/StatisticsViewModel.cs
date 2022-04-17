using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class StatisticsViewModel
    {
        public SortedDictionary<int, string> Orders { get; set; }
        public double TotalVAT { get; set; }
    }
}
