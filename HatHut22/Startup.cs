using CVSITE21.Data;
using Data.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HatHut22.Startup))]
namespace HatHut22
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            using (var context = new ApplicationDbContext())
            {
                var id = 0;
                var DumpProfile = context.Employees.Find(id);
                if (DumpProfile == null) 
                    { 
                        Employee employee = new Employee();
                        employee.EmployeeId = 0;
                        employee.Fullname = "ingen";
                        context.Employees.Add(employee);
                        context.SaveChanges();
                    }
                else { }
            }
            ConfigureAuth(app);
        }
    }
}
