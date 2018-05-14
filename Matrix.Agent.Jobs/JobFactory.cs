using Quartz;
using Quartz.Spi;
using SimpleInjector;

namespace Matrix.Agent.Jobs
{
    public class JobFactory : IJobFactory
    {
        public Container Container { get; }

        public JobFactory(Container container)
        {
            Container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return Container.GetInstance(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}