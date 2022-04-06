using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVSITE21.Data;
using Data.Models;

namespace HatHut22.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.employeeMakingOrder).Include(o => o.ownerOfOrder).Include(o => o.productInOrder);
            return View(orders.ToList());
        }
// skapa en order lägg till produkt i varukort
       

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {

            ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email");
            ViewBag.OrderCustomerId = new SelectList(db.Customers, "CostumerId", "Email");
            ViewBag.OrderProductId = new SelectList(db.Products, "productId", "Title");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderId,Description,DateCreated,IsPaid,IsHatFinnished,ImagePath,Price,OrderCustomerId,OrderEmployeeId,OrderProductId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", order.OrderEmployeeId);
            ViewBag.OrderCustomerId = new SelectList(db.Customers, "CostumerId", "Email", order.OrderCustomerId);
            ViewBag.OrderProductId = new SelectList(db.Products, "productId", "Title", order.OrderProductId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", order.OrderEmployeeId);
            ViewBag.OrderCustomerId = new SelectList(db.Customers, "CostumerId", "Email", order.OrderCustomerId);
            ViewBag.OrderProductId = new SelectList(db.Products, "productId", "Title", order.OrderProductId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderId,Description,DateCreated,IsPaid,IsHatFinnished,ImagePath,Price,OrderCustomerId,OrderEmployeeId,OrderProductId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", order.OrderEmployeeId);
            ViewBag.OrderCustomerId = new SelectList(db.Customers, "CostumerId", "Email", order.OrderCustomerId);
            ViewBag.OrderProductId = new SelectList(db.Products, "productId", "Title", order.OrderProductId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
