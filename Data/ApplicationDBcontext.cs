using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Models;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVSITE21.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Material> Materials { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
            .HasRequired<Customer>(s => s.ownerOfOrder)
            .WithMany(g => g.OwnerOfOrders)
            .HasForeignKey<int>(s => s.OrderCustomerId);

            modelBuilder.Entity<Order>()
            .HasRequired<Employee>(s => s.employeeMakingOrder)
            .WithMany(g => g.ActiveInOrders)
            .HasForeignKey<int?>(s => s.OrderEmployeeId);

            modelBuilder.Entity<Order>()
            .HasRequired<Product>(s => s.productInOrder)
            .WithMany(g => g.ExistInOrders)
            .HasForeignKey<int>(s => s.OrderProductId);

            modelBuilder.Entity<Order>()
            .HasRequired<Material>(s => s.MaterialInOrder)
            .WithMany(g => g.MaterialOfOrders)
            .HasForeignKey<int>(s => s.OrderMaterialId);
        }

    }
}
