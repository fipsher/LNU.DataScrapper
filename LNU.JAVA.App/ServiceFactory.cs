﻿using LNU.JAVA.API;
using LNU.JAVA.Core;
using LNU.JAVA.Scheduler.JobRunner;
using System.Collections.Generic;

namespace LNU.JAVA.App
{
    public class ServiceFactory : IService
    {
        private readonly List<IService> services;

        public ServiceFactory()
        {
            services = new List<IService>{
                new WebService(Settings.WebServiceUrl, new JobScheduler())
            };
        }

        public void OnStart()
        {
            services.ForEach(service => service.OnStart());
        }

        public void OnStop()
        {
            services.ForEach(service => service.OnStop());
        }
    }
}
