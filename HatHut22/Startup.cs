using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HatHut22.Startup))]
namespace HatHut22
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
