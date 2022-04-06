using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data.Models
{
    public class SaveImage
    {
        public HttpPostedFileBase Image { get; set; }
        public string ImagePath { get; set; }
    }
}