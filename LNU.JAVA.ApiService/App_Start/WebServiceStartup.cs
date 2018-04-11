using Owin;
using System.Web.Http;

namespace LNU.JAVA.API.App_Start
{
    public class WebServiceStartup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
