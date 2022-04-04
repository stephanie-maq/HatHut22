using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter a date with the following format: mm/dd/yyyy")]
        [Display(Name = "Start Date")]
        public DateTime DateCreated { get; set; }
        public bool IsPaid { get; set; }
        public bool IsFinnished { get; set; }
        public string ImagePath { get; set; }
        public int OrderCustomerId { get; set; }
        public Customer ownerOfOrder { get; set; }
        public int OrderEmployeeId { get; set; }
        public Employee employeeMakingOrder { get; set; }
        public int OrderProductId { get; set; }
        public Product productInOrder { get; set; }

    }
}
