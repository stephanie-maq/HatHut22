using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }

        [Required(ErrorMessage = "Please enter a title for your project")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "The title should be between {2} and {1} characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a short description for your project")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "The description should be between {2} and {1} characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter a date with the following format: mm/dd/yyyy")]
        [Display(Name = "Start Date")]
        public DateTime DateCreated { get; set; }

        public ICollection<Order> ExistInOrders { get; set; }

        public bool IsStockProduct { get; set; }
        public bool IsSpecialProduct { get; set; }
        public bool IsCostumerProduct { get; set; }
    }
}
