using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoStore4.Startup))]
namespace DemoStore4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
