using LNU.JAVA.API.App_Start;
using LNU.JAVA.Core;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace LNU.JAVA.API
{
    public class WebService : IService
    {
        private readonly string WebServiceUrl;
        private IDisposable _webapp;
        
        public WebService(string webServiceUrl)
        {
            WebServiceUrl = webServiceUrl;
        }

        public void OnStart()
        {
            _webapp = WebApp.Start
                    (
                        new StartOptions(url: WebServiceUrl),
                        appBuilder => new WebServiceStartup().Configuration(appBuilder)
                    );
        }

        public void OnStop()
        {
            _webapp?.Dispose();
        }
    }
}
