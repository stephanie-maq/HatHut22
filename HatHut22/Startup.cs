using CVSITE21.Data;
using Data.Models;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(HatHut22.Startup))]
namespace HatHut22
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            using (var context = new ApplicationDbContext())
            {
                var namn = "ingen";
                var DumpProfile = context.Employees.FirstOrDefault(x => x.Fullname == namn);
                var ingenprodukt = "borttagen produkt";
                var DumpProdukt = context.Products.FirstOrDefault(x => x.Title == ingenprodukt);
                    if (DumpProfile == null) 
                    { 
                        Employee employee = new Employee();
                        employee.EmployeeId = 0;
                        employee.Fullname = namn;
                        context.Employees.Add(employee);
                        context.SaveChanges();
                    }
                else { }
                if (DumpProdukt == null)
                {
                    Product product = new Product();
                    product.productId = 0;
                    product.Title = ingenprodukt;
                    product.Description = "Produkten har blivit borttagen";
                    product.DateCreated = DateTime.Now;
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                else { }
            }
            ConfigureAuth(app);
        }
    }
}
