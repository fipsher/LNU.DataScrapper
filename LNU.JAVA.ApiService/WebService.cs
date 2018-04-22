using LNU.JAVA.API.App_Start;
using LNU.JAVA.Core;
using LNU.JAVA.Scheduler.JobRunner;
using LNU.JAVA.Scheduler.Jobs;
using Microsoft.Owin.Hosting;
using System;

namespace LNU.JAVA.API
{
    public class WebService : IService
    {
        private readonly string WebServiceUrl;
        private IDisposable _webapp;
        private IJobScheduler _scheduler;

        public WebService(string webServiceUrl, IJobScheduler scheduler)
        {
            WebServiceUrl = webServiceUrl;
            _scheduler = scheduler;
        }

        public void OnStart()
        {
            _scheduler.ScheduleJob<GetRssJob>();

            _webapp = WebApp.Start
                    (
                        new StartOptions(url: WebServiceUrl),
                        appBuilder => new WebServiceStartup().Configuration(appBuilder)
                    );
        }

        public void OnStop()
        {
            _scheduler.Stop();
            _webapp?.Dispose();
        }
    }
}
