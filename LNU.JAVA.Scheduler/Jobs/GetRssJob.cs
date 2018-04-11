using LNU.JAVA.Core;
using LNU.JAVA.Data;
using Quartz;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace LNU.JAVA.Scheduler.Jobs
{
    public class GetRssJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var url = ConfigurationManager.AppSettings["Url"];
            var connectionString = ConfigurationManager.AppSettings["RssConnectionString"];

            var provider = new CNNDataProvider(url);
            try
            {
                var list = await provider.GetRSS(new Query
                {
                    Type = "top-headlines",
                    Source = "cnn"
                });

                var repo = new Repository(connectionString);
                await repo.Update(list);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
