using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Customer
    {
        [Key] public int CostumerId { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [RegularExpression(@"^[A-Öa-ö ]+$", ErrorMessage = "Names can only have letters and space")]
        public string Fullname { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Adress can only have letters, numbers and space")]
        public string Address { get; set; }
        public string Notes { get; set; }
        [RegularExpression(@"^[ 0-9 ]+$", ErrorMessage = "Phone numbers can only consist of numbers and area code.")]
        public int phoneNumber { get; set; }
        public ICollection<Order> OwnerOfOrders { get; set; }
    }
}