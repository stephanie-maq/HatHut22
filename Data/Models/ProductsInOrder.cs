﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductsInOrder
    {
        [Key, ForeignKey("Product"), Column(Order = 0)]
        public int ProfileId { get; set; }

        [Key, ForeignKey("Order"), Column(Order = 1)]
        public int ProjectID { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
