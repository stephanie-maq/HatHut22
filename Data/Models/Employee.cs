using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Employee
    {
        [Key] 
        public int EmployeeId { get; set; }
        [Display(Name = "Epost address")]
        public string Email { get; set; }
        [Display(Name = "Fullständigt namn")]
        [RegularExpression(@"^[A-Öa-ö ]+$", ErrorMessage = "Namn kan bara bestå av bokstäver och mellanslag.")]
        public string Fullname { get; set; }
        [Display(Name = "Aktiv i ordrar")]
        public ICollection<Order> ActiveInOrders { get; set; }
    }
}
