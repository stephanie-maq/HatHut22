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
        [Display(Name = "Produkt namn")]

        [Required(ErrorMessage = "Vänligen ange ett namn till produkten.")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Beskrivningen borde vara minst 2 tecken.")]
        public string Title { get; set; }
        [Display(Name = "Beskrivning")]
        [Required(ErrorMessage = "Vänligen ange en kort beskrivning till produkten.")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "Beskrivningen borde vara minst 10 tecken.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Ange ett datum med följande format: mm/dd/yyyy")]
        [Display(Name = "Start Datum")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Pris")]
        public int Price { get; set; }
        public ICollection<Order> ExistInOrders { get; set; }
        [Display(Name = "Lagerförd produkt")]
        public bool IsStockProduct { get; set; }
        [Display(Name = "Special produkt")]
        public bool IsSpecialProduct { get; set; }
        [Display(Name = "Skräddarsydd produkt")]
        public bool IsCostumerMeasuredProduct { get; set; }
    }
}
