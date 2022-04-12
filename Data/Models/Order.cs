﻿using System;
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
        public string ImagePath { get; set; }
        public int Price { get; set; }
        public string Material { get; set; }
        public int OrderCustomerId { get; set; }
        [Display(Name = "Kund")]
        public Customer ownerOfOrder { get; set; }
        public int? OrderEmployeeId { get; set; }
        [Display(Name = "Aktiv anställd")]
        public Employee employeeMakingOrder { get; set; }
        public int OrderProductId { get; set; }
        [Display(Name = "Produkt")]
        public Product productInOrder { get; set; }

    }
}
