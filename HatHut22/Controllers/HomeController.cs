﻿using CVSITE21.Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HatHut22.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

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
            double vat =
                db.Orders
                .Where(order => order.DateCreated.Year == DateTime.Now.Year)
                .Select(order => order.Price)
                .Sum() * 0.25;

            Dictionary<int, string> orderedProducts =
                     db.Orders
                     .GroupBy(order => order.productInOrder.Title)
                     .ToDictionary(g => g.Count(), g => g.Key);

            StatisticsViewModel statistics = new StatisticsViewModel
            {
                TotalVAT = vat,
                Orders =
                    new SortedDictionary<int, string>(orderedProducts)
            };

            return View(statistics);
        }
    }
}