using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class StatisticsViewModel
    {
        //public SortedDictionary<int, string> Orders { get; set; }
        public List<Product> SamtligaProdukter { get; set; }
        public List<Order> SamtligaOrders { get; set; }
        public List<Order> SorteradeOrders { get; set; }
        [Display(Name = "Total moms")]
        public double TotalVAT { get; set; }
    }
}
