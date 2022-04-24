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
        {if (db.Orders.Count() > 0)
            {
                if (db.Orders.Where(x => x.IsPaid == true).Count() > 0)
                {
                    double vat =
                        db.Orders
                        .Where(order => order.DateCreated.Year == DateTime.Now.Year)
                        .Where(order => order.IsPaid == true)
                        .Select(order => order.Price)
                        .Sum() * 0.25;

                    //Gör en lista av produkter
                    var produkter = db.Products.ToList();
                    var ordrarna = db.Orders.Where(x => x.IsPaid == true).ToList();
                    List<Order> sorteradeOrdrar = new List<Order>();
                    foreach (var order in ordrarna)
                    {
                        if (!sorteradeOrdrar.Any(x=>x.OrderProductId == order.OrderProductId))
                        {
                            sorteradeOrdrar.Add(order);
                        }
                        else { }
                    }

                    

                    StatisticsViewModel statistics = new StatisticsViewModel
                    {
                        TotalVAT = vat,
                        //Orders =
                        //    new SortedDictionary<int, string>(orderedProducts)
                        SorteradeOrders = sorteradeOrdrar,
                        SamtligaOrders = ordrarna,
                        SamtligaProdukter = produkter,
                    };
                    return View(statistics);
                }
                else 
                {
                    StatisticsViewModel statistics = new StatisticsViewModel
                    {
                        TotalVAT = 0
                    };
                    return View(statistics);
                }
                //var PolyesterNumber = db.Orders.Where(order => order.Material == "Polyester").Where(order => order.HaveMaterials == false).Count();
                //var CottonNumber = db.Orders.Where(order => order.Material == "Cotton").Where(order => order.HaveMaterials == false).Count();
                //var LeatherNumber = db.Orders.Where(order => order.Material == "Leather").Where(order => order.HaveMaterials == false).Count();
                //ViewBag.PolyesterNeeded = PolyesterNumber;
                //ViewBag.CottonNeeded = CottonNumber;
                //ViewBag.LeatherNeeded = LeatherNumber;
                
                
            }
            else { return View(); }
        }
    }
}