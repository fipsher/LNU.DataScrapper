using Quartz;

namespace LNU.JAVA.Scheduler.JobRunner
{
    public interface IJobScheduler
    {
        void ScheduleJob<TJob>() where TJob : IJob;

        void Stop();
    }
}
