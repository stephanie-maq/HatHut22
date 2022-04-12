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
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext()) { 
                var user = System.Web.HttpContext.Current.User.Identity.Name;
                var employee = context.Employees.Include(x => x.ActiveInOrders).Where(x => x.Email == user).FirstOrDefault();
                var ordersList = employee.ActiveInOrders.ToList();

                List<string> listOfOrderId = new List<string>();
                foreach (var item in ordersList)
                {
                    listOfOrderId.Add(item.orderId.ToString());

                }
                ViewBag.Orders = listOfOrderId;
                var orders = db.Orders.Include(o => o.employeeMakingOrder).Include(o => o.ownerOfOrder).Include(o => o.productInOrder);
                return View(orders.ToList());
            }
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
        public ActionResult Create(int? id)
        {
            using (var context = new ApplicationDbContext())
            {
                var productID = id;
                var products = context.Products.FirstOrDefault(x => x.productId == productID);
                ViewBag.ProductPrice = products.Price;
                ViewBag.ProductTitle = products.Title;
                ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email");
                ViewBag.OrderCustomerId = new SelectList(db.Customers, "CostumerId", "Email");
                ViewBag.OrderProductId = productID;
                return View();
            }
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
        public ActionResult Invoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ApplicationDbContext())
            {
                var order = db.Orders.Find(id);
                var productID = order.OrderProductId;
                var customerId = order.OrderCustomerId;
                var product = context.Products.FirstOrDefault(x => x.productId == productID);
                var customer = context.Customers.FirstOrDefault(x => x.CostumerId == customerId);
                ViewBag.ProductPrice = product.Price;
                ViewBag.ProductTitle = product.Title;
                ViewBag.ProductDescription = product.Description;
                ViewBag.CustomerAddress = customer.Address;
                ViewBag.CustomerEmail = customer.Email;
                ViewBag.CustomerName = customer.Fullname;
                ViewBag.CustomerPhone = customer.phoneNumber;

                //ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email");
                //ViewBag.OrderCustomerId = new SelectList(db.Customers, "CostumerId", "Email");
                //ViewBag.OrderProductId = productID;
                return View(order);
            }            
        }

        public ActionResult OrderSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ApplicationDbContext())
            {
                var order = db.Orders.Find(id);
                var productID = order.OrderProductId;
                var customerId = order.OrderCustomerId;
                var product = context.Products.FirstOrDefault(x => x.productId == productID);
                var customer = context.Customers.FirstOrDefault(x => x.CostumerId == customerId);
                ViewBag.ProductPrice = product.Price;
                ViewBag.ProductTitle = product.Title;
                ViewBag.ProductDescription = product.Description;
                ViewBag.CustomerAddress = customer.Address;
                ViewBag.CustomerEmail = customer.Email;
                ViewBag.CustomerName = customer.Fullname;
                ViewBag.CustomerPhone = customer.phoneNumber;

                //ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email");
                //ViewBag.OrderCustomerId = new SelectList(db.Customers, "CostumerId", "Email");
                //ViewBag.OrderProductId = productID;
                return View(order);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult AddEmployeeToOrder(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                string username = System.Web.HttpContext.Current.User.Identity.Name;
                Employee wantedEmployee = context.Employees.Where(x => x.Email == username).FirstOrDefault();
                Order wantedOrder = context.Orders.Where(x => x.orderId == id).FirstOrDefault();
                wantedOrder.employeeMakingOrder = wantedEmployee;
                wantedOrder.OrderEmployeeId = wantedEmployee.EmployeeId;
                context.Entry(wantedOrder).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
                
            }
        }

        public ActionResult RemoveEmployeeToOrder(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                string username = System.Web.HttpContext.Current.User.Identity.Name;
                Employee wantedEmployee = context.Employees.Where(x => x.Fullname == "ingen").FirstOrDefault();
                Order wantedOrder = context.Orders.Where(x => x.orderId == id).FirstOrDefault();
                wantedOrder.employeeMakingOrder = wantedEmployee;
                wantedOrder.OrderEmployeeId = wantedEmployee.EmployeeId;
                context.Entry(wantedOrder).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
