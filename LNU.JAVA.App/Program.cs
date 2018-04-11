using LNU.JAVA.App;
using Topshelf;

namespace LNU.JAVA
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceFactory>(s =>
                {
                    s.ConstructUsing(name => new ServiceFactory());
                    s.WhenStarted(tc => tc.OnStart());
                    s.WhenStopped(tc => tc.OnStop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("LNU Shedule Topshelf Windows Service.");
                x.SetDisplayName("LNU Shedule Service");
                x.SetServiceName("AspNetSelfHostLNUShedule");
            });
        }
    }
}
