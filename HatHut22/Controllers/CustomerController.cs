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
        public ActionResult List(string search)
        {
            using (var context = new ApplicationDbContext())
            {
                if (search == null || search == "")
                {
                    var customers = context.Customers.Where(x=>x.Fullname != "borttagen kund").ToList();

                    return View(customers);
                }
                if (search != null && search != "")
                {
                    return View(context.Customers.Where(x => x.Fullname != "borttagen kund").Where(x => x.Fullname.ToString().ToLower().Contains(search.ToLower().ToString())).ToList());
                }
                return View(context.Customers);
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
                if (customer == null)
                {
                    return HttpNotFound();
                }

                int customerID = customer.CostumerId;
                Customer wantedCustomer = db.Customers.Where(x => x.Fullname == "borttagen kund").FirstOrDefault();
                var wantedOrders = db.Orders.Where(x => x.OrderCustomerId == id);
                foreach (var item in wantedOrders)
                {

                    item.ownerOfOrder = wantedCustomer;
                    item.OrderCustomerId = wantedCustomer.CostumerId;

                    db.Entry(item).State = EntityState.Modified;


                }
                customer.OwnerOfOrders = null;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                db.Customers.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("List");
            }

        }

    }
}
