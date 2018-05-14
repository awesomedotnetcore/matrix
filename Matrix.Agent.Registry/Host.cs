using Matrix.Agent.Configuration;
using Matrix.Agent.Host;
using Matrix.Agent.Middlewares;
using Quartz;
using System;

namespace Matrix.Agent.Registry
{
    public class Host : ServerHost
    {
        public Host(IMiddleware middleware, IConfiguration configuration, IScheduler scheduler)
            : base(middleware, configuration, scheduler)
        {
            OnStart += (object sender, EventArgs e) =>
            {
                var beacon = JobBuilder.Create<Pulse>().WithIdentity(configuration.App.Id.ToString(), nameof(Pulse)).Build();
                var trigger = TriggerBuilder.Create().ForJob(configuration.App.Id.ToString(), nameof(Pulse)).StartNow().WithSimpleSchedule(o => o.RepeatForever().WithIntervalInSeconds(30)).Build();

                if (beacon != null && trigger != null)
                    scheduler.ScheduleJob(beacon, trigger);
            };
        }
    }
}