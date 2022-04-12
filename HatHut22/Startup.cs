using CVSITE21.Data;
using Data.Models;
using Microsoft.Owin;
using Owin;
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
