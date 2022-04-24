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
        [Key] 
        public int MaterialId { get; set; }
        [Display(Name = "Material namn")]
        [RegularExpression(@"^[A-Öa-ö ]+$", ErrorMessage = "Namn kan bara bestå av bokstäver och mellanslag.")]
        public string MaterialName { get; set; }
        [Display(Name = "Ordrar med materialet")]
        public ICollection<Order> MaterialOfOrders { get; set; }
    }
}
