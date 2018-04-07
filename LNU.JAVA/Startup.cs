using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LNU.JAVA.Startup))]
namespace LNU.JAVA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
