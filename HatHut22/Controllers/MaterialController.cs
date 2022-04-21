using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using CVSITE21.Data;

namespace HatHut22.Controllers
{
    [Authorize]
    public class MaterialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Material
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
               
                
                var materials = db.Materials.Include(o => o.MaterialOfOrders);

               
                return View(materials.ToList());
            }
        }

        // GET: Material/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Material/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Material/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "MaterialId, MaterialName, Color")] Material material)
        {
            if (ModelState.IsValid)
            {
                db.Materials.Add(material);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            else { return View(material); }
        }

        // GET: Material/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Material/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Material/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Material/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
