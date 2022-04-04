using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class EmployeeInOrder
    {
        [Key, ForeignKey("Employee"), Column(Order = 0)]
        public int ProfileId { get; set; }

        [Key, ForeignKey("Order"), Column(Order = 1)]
        public int ProjectID { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Order Order { get; set; }
    }
}
