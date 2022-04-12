using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using CVSITE21.Data;
using Data.Migrations;
using Data.Models;
using Microsoft.AspNet.Identity;
using HatHut22.Models;

namespace HatHut22.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customer
        public ActionResult List()
        {
            using (var context = new ApplicationDbContext())
            {
                var username = User.Identity.Name;
                if (username != null && username != "")
                {
                    var customers = context.Customers.ToList();
                    ViewBag.ProfileId = username;

                    return View(customers);
                }
                return View();
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                //Customer customer = context.Customers.inFind(id);
                var customer = context.Customers.Include(x => x.OwnerOfOrders).Where(x => x.CostumerId == id).FirstOrDefault();
                var orders = customer.OwnerOfOrders.ToList();
                

                var tempCustomerProfile = new TempCustomer();
                tempCustomerProfile.CostumerId = customer.CostumerId;
                tempCustomerProfile.Email = customer.Email;
                tempCustomerProfile.Fullname = customer.Fullname;
                tempCustomerProfile.Address = customer.Address;
                tempCustomerProfile.Notes = customer.Notes;
                tempCustomerProfile.phoneNumber = customer.phoneNumber;


                List<string> userOrders = new List<string>();
                foreach (var item in orders)
                {
                    userOrders.Add(item.orderId.ToString());

                }
                tempCustomerProfile.OwnerOfOrders = userOrders;
                

                return View(tempCustomerProfile);
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


        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {

            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                    return RedirectToAction("List");
                }

                return RedirectToAction("List");
            }

        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            using (var context = new ApplicationDbContext())
            {
               
                
                var customer = context.Customers.FirstOrDefault(x => x.CostumerId == id);
                
                    

                    Customer customernullcheck = context.Customers.Find(id);
                    if (customernullcheck == null)
                    {
                        return HttpNotFound();
                    }

                    return View(customer);
                
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "CostumerId,Email,Fullname,Address,Notes,phoneNumber")] Customer customer)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    context.Entry(customer).State = EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("List");
                }

                return View(customer);
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            using (var context = new ApplicationDbContext())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Customer customer = context.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                return View(customer);
            }
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                Customer customer = context.Customers.Find(id);
                context.Customers.Remove(customer);
                context.SaveChanges();
                return RedirectToAction("List");
            }

        }
    }
}
