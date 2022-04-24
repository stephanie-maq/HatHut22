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
        [Key] 
        public int CostumerId { get; set; }
        [Display(Name = "Epost address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-posten är inte giltlig.")]
        public string Email { get; set; }
        [Display(Name = "Fullständigt namn")]
        [RegularExpression(@"^[A-Öa-ö ]+$", ErrorMessage = "Namn kan bara bestå av bokstäver och mellanslag.")]
        public string Fullname { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "en address kan bara bestå av bokstäver, nummer och mellanslag.")]
        public string Address { get; set; }
        [Display(Name = "Anteckningar")]
        public string Notes { get; set; }
        [Display(Name ="Telefonnummer")]
        [RegularExpression(@"^\s*-?[0-9]{1,10}\s*$", ErrorMessage = "Telefonnummer kan enbart vara siffror och max. 9 siffror långt.")]
        public int phoneNumber { get; set; }
        [Display(Name = "Gjorda beställningar")]
        public ICollection<Order> OwnerOfOrders { get; set; }
    }
}