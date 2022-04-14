using CVSITE21.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HatHut22.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Statistics()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.orderCount = from p in db.Orders
                                 group p by p.OrderProductId into g
                                 select new { ProductCount = g.Count() };

            return View(db.Products);
        }
    }
}