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
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string searchBy, string search)
        {
            List<Product> allProducts = new List<Product>();
            allProducts = db.Products.Where(x=>x.Title != "borttagen produkt").ToList();

            if (searchBy == "All Products")
            {
                return View(allProducts);
            }
            else if (searchBy == "Stock Product" && search != "")
            {
                return View(allProducts.Where(x => x.IsStockProduct.Equals(true)));
            }
            else if (searchBy == "Customized Stock Product" && search != "")
            {
                return View(allProducts.Where(x => x.IsSpecialProduct.Equals(true)));
            }
            else if (searchBy == "Custom Made Product" && search != "")
            {
                return View(allProducts.Where(x => x.IsCostumerMeasuredProduct.Equals(true)));
            }


            return View(allProducts);


        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.Date= DateTime.Now;
            return View();
        }
        public ActionResult CreateCopy(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                ModelState.Clear();
                Product copyProduct = new Product();
                ViewBag.Date = DateTime.Now;
                var productID = id;
                var products = context.Products.FirstOrDefault(x => x.productId == productID);
                copyProduct.Price = products.Price;
                copyProduct.Title = products.Title;
                copyProduct.Description = products.Description;
                return View(copyProduct);
            }
        }
        [HttpPost]
        public ActionResult CreateCopy([Bind(Include = "productid,title,description,datecreated,price,isstockproduct,isspecialproduct,iscostumermeasuredproduct")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        //metod för att lägg till produkt
        public ActionResult create([Bind(Include = "productid,title,description,datecreated,price,isstockproduct,isspecialproduct,iscostumermeasuredproduct")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View(product);
        }

        // för att lägga till en ny produkt
        //public ActionResult Create(Product product)
        //{

        //    using (var context = new ApplicationDbContext())
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            context.Products.Add(product);
        //            context.SaveChanges();
        //            return RedirectToAction("List");
        //        }

        //        return RedirectToAction("List");
        //    }

        // }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productId,Title,Description,DateCreated,Price,IsStockProduct,IsSpecialProduct,IsCostumerMeasuredProduct")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            int produktID = product.productId;
            Product wantedProduct = db.Products.Where(x => x.Title == "borttagen produkt").FirstOrDefault();
            var wantedOrders = db.Orders.Where(x => x.OrderProductId == id);
            foreach (var item in wantedOrders)
            {
                
                item.productInOrder = wantedProduct;
                item.OrderProductId = wantedProduct.productId;

                db.Entry(item).State = EntityState.Modified;
                

            }
            product.ExistInOrders = null;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            db.Products.Remove(product);
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
