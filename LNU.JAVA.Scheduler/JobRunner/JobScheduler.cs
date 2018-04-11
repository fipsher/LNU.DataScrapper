using Quartz;
using Quartz.Impl;

namespace LNU.JAVA.Scheduler.JobRunner
{
    public class JobScheduler : IJobScheduler
    {
        public void ScheduleJob<TJob>() where TJob : IJob
        {
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler().Result;

            string jobName = typeof(TJob).Name;

            IJobDetail jobDetail = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                               .WithIdentity(jobName)
                               .WithDescription("every hour")
                               .WithSimpleSchedule(x => x.WithIntervalInHours(1))
                               .StartNow()
                               .Build();            
            sched.DeleteJob(new JobKey(jobName));
            sched.ScheduleJob(jobDetail, trigger);

            sched.Start();
        }

        public void Stop()
        {
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler().Result;
            sched.Clear();
        }
    }
}
