using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LNU.JAVA.Mvc.Startup))]
namespace LNU.JAVA.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
