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
        [Key] public int EmployeeId { get; set; }

        public string Email { get; set; }
        [RegularExpression(@"^[A-Öa-ö ]+$", ErrorMessage = "Names can only have letters and space")]
        public string Fullname { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Adress can only have letters, numbers and space")]
        public ICollection<Order> ActiveInOrders { get; set; }
    }
}
