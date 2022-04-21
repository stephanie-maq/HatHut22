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
        [Display(Name = "Betalad?")]
        public bool IsPaid { get; set; }
        [Display(Name = "Är hatten klar?")]
        public bool IsHatFinnished { get; set; }
        [Display(Name = "Är ordern skickad?")]
        public bool IsSent { get; set; }
        [Display(Name = "Finns material?")]
        public bool HaveMaterials { get; set; }
        public string ImagePath { get; set; }
        public int Price { get; set; }
        [Display(Name = "Kund")]
        public int OrderCustomerId { get; set; }
        [Display(Name = "Kund")]
        public Customer ownerOfOrder { get; set; }
        [Display(Name = "Anställd")]
        public int? OrderEmployeeId { get; set; }
        [Display(Name = "Aktiv anställd")]
        public Employee employeeMakingOrder { get; set; }
        [Display(Name = "Produkt")]
        public int OrderProductId { get; set; }
        [Display(Name = "Produkt")]
        public Product productInOrder { get; set; }
        [Display(Name = "Material")]
        public int OrderMaterialId { get; set; }
        [Display(Name = "Material")]
        public Material MaterialInOrder { get; set; }

    }
}
