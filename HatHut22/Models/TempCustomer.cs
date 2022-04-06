

using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatHut22.Models
{
    public class TempCustomer
    {
        public int CostumerId { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int phoneNumber { get; set; }
        public List<string> OwnerOfOrders { get; set; }
    }
}