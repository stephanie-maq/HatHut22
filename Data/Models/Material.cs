using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Material
    {
        [Key] public int MaterialId { get; set; }
        [RegularExpression(@"^[A-Öa-ö ]+$", ErrorMessage = "Names can only have letters and space")]
        public string MaterialName { get; set; }
        [RegularExpression(@"^[A-Öa-ö ]+$", ErrorMessage = "Names can only have letters and space")]
        public string Color { get; set; }
        public ICollection<Order> MaterialOfOrders { get; set; }
    }
}
