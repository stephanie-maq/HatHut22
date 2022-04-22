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
using Rotativa;

namespace HatHut22.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index(string searchBy)
        {
            using (var context = new ApplicationDbContext())
            {
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

                if (searchBy == "Alla Ordrar")
                {
                    return View(orders.ToList());
                }
                else if (searchBy == "Betalda Ordrar")
                {
                    return View(orders.ToList().Where(x => x.IsPaid.Equals(true)));
                }
                else if (searchBy == "Färdiga Ordrar")
                {
                    return View(orders.ToList().Where(x => x.IsHatFinnished.Equals(true)));
                }


                return View(orders.ToList());
            }
        }
        // skapa en order lägg till produkt i varukort


        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            using (var context = new ApplicationDbContext())
            {
                

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var order = context.Orders.Where(x=>x.orderId == id).Include(o => o.employeeMakingOrder).Include(o => o.ownerOfOrder).Include(o => o.productInOrder);
                if (order == null)
                {
                    return HttpNotFound();
                }
                Order ordern = order.FirstOrDefault(x => x.orderId == id);
                var MaterialID = ordern.OrderMaterialId;
                Material currentMatrial = context.Materials.FirstOrDefault(x => x.MaterialId == MaterialID);
                ViewBag.Material = currentMatrial.MaterialName;
                return View(order.ToList());
            }
        }

        // GET: Orders/Create
        public ActionResult Create(int? id)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Customers.Where(x => x.Fullname != "borttagen kund").ToList().Count() != 0 && context.Materials.ToList().Count() != 0)
                {
                    var productID = id;
                    var products = context.Products.FirstOrDefault(x => x.productId == productID);
                    ViewBag.ProductPrice = products.Price;
                    ViewBag.ProductTitle = products.Title;
                    ViewBag.OrderCustomerId = new SelectList(db.Customers.Where(x => x.Fullname != "borttagen kund"), "CostumerId", "Email");
                    ViewBag.OrderMaterialId = new SelectList(db.Materials, "MaterialId", "MaterialName");
                    ViewBag.OrderProductId = productID;
                    return View();
                }
                else if (context.Materials.ToList().Count() == 0)
                {

                    return RedirectToAction("Create", "Material");
                }
                else { return RedirectToAction("Create", "Customer"); }
            }
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderId,Description,DateCreated,IsPaid,IsHatFinnished,IsSent,HaveMaterials,ImagePath,Price,OrderMaterialId,OrderCustomerId,OrderProductId")] Order order)
        {
            using (var context = new ApplicationDbContext())
            {
                var namn = "ingen";
                Employee defaultEmploye = context.Employees.Where(x => x.Fullname == namn).FirstOrDefault();
                order.employeeMakingOrder = defaultEmploye;
                order.OrderEmployeeId = defaultEmploye.EmployeeId;

                if (ModelState.IsValid)
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", order.OrderEmployeeId);
                ViewBag.OrderCustomerId = new SelectList(db.Customers.Where(x => x.Fullname != "borttagen kund"), "CostumerId", "Email", order.OrderCustomerId);
                ViewBag.OrderMaterialId = new SelectList(db.Materials, "MaterialId", "MaterialName", order.OrderMaterialId);
                ViewBag.OrderProductId = new SelectList(db.Products.Where(x => x.Title != "borttagen produkt"), "productId", "Title", order.OrderProductId);
                return View(order);
            }
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
            ViewBag.OrderCustomerId = new SelectList(db.Customers.Where(x => x.Fullname != "borttagen kund"), "CostumerId", "Email", order.OrderCustomerId);
            ViewBag.OrderMaterialId = new SelectList(db.Materials, "MaterialId", "MaterialName", order.OrderMaterialId);
            ViewBag.OrderProductId = new SelectList(db.Products.Where(x => x.Title != "borttagen produkt"), "productId", "Title", order.OrderProductId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderId,Description,DateCreated,IsPaid,IsHatFinnished,IsSent,HaveMaterials,Price,OrderMaterialId,OrderCustomerId,OrderProductId")] Order order)
        {
            using (var context = new ApplicationDbContext())
            {

                Order orderBeforeEdit = context.Orders.Find(order.orderId);
                order.OrderEmployeeId = orderBeforeEdit.OrderEmployeeId;
                if (ModelState.IsValid)
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.OrderEmployeeId = new SelectList(db.Employees, "EmployeeId", "Email", order.OrderEmployeeId);
                ViewBag.OrderCustomerId = new SelectList(db.Customers.Where(x => x.Fullname != "borttagen kund"), "CostumerId", "Email", order.OrderCustomerId);
                ViewBag.OrderMaterialId = new SelectList(db.Materials, "MaterialId", "MaterialName", order.OrderMaterialId);
                ViewBag.OrderProductId = new SelectList(db.Products.Where(x => x.Title != "borttagen produkt"), "productId", "Title", order.OrderProductId);
                return View(order);
            }
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
            
                return View(order);
            }
        }

        public ActionResult InvoicePdf(int? id)
        {

            return new Rotativa.ActionAsPdf("Invoice", new { id = id })
            {
                FileName = "SpecificOrder.pdf",
                Cookies = GetCooikes()
            };
        }



        public ActionResult SpecificOrderSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var totalPrice = 0;
            using (var context = new ApplicationDbContext())
            {
                var orderList = db.Orders.Where(x => x.orderId == id);
                foreach (var item in orderList)
                {
                    if (!item.IsPaid)
                    {
                        totalPrice += item.Price;


                    }
                }

                ViewBag.OrderList = orderList;
                ViewBag.TotalPrice = totalPrice;
                
                return View();
            }
        }

        public Dictionary<string, string> GetCooikes()
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            return cookieCollection;
        } 

        public ActionResult SpecificOrderSummaryPdf(int? id)
        {
           
            return new Rotativa.ActionAsPdf("SpecificOrderSummary", new { id = id })
            {
                FileName = "SpecificOrder.pdf",
                Cookies = GetCooikes()
            };
        }

        public ActionResult OrderSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var totalPrice = 0;
            using (var context = new ApplicationDbContext())
            {
                var orderList = db.Orders.Where(x => x.OrderCustomerId == id);
                foreach (var item in orderList)
                {
                    if (!item.IsPaid)
                    {
                        totalPrice += item.Price;

                    }
                }

                ViewBag.OrderList = orderList;
                ViewBag.TotalPrice = totalPrice;
                return View();
            }
        }

        public ActionResult OrderSummaryPdf(int? id)
        {

            return new Rotativa.ActionAsPdf("OrderSummary", new { id = id })
            {
                FileName = "OrderSummary.pdf",
                Cookies = GetCooikes()
            };
        }

        public ActionResult CurrentOrderSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var totalPrice = 0;
            using (var context = new ApplicationDbContext())
            {
                var orderList = db.Orders.Where(x => x.OrderCustomerId == id).Where(x => x.IsSent == false);
                foreach (var item in orderList)
                {
                    if (!item.IsPaid)
                    {
                        totalPrice += item.Price;


                    }
                }

                ViewBag.OrderList = orderList;
                ViewBag.TotalPrice = totalPrice;
                return View();
            }
        }

        public ActionResult CurrentOrderSummaryPdf(int? id)
        {

            return new Rotativa.ActionAsPdf("CurrentOrderSummary", new { id = id })
            {
                FileName = "CurrentOrderSummary.pdf",
                Cookies = GetCooikes()
            };
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

        public ActionResult MaterialsOrdered(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var orders = db.Orders.Where(x => x.OrderMaterialId == id);

                foreach (var item in orders)
                {

                    item.HaveMaterials = true;

                    db.Entry(item).State = EntityState.Modified;

                }
                db.SaveChanges();
                return RedirectToAction("Index", "Material");
            }
        }

    }
}
