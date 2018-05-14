using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Middlewares;
using Matrix.Messages;
using NLog;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Jobs
{
    public class Beacon : Job
    {
        private IHealthService Health { get; }

        public Beacon(ILogger logger, IMiddleware middleware, IConfiguration settings, IHealthService health)
            : base(logger, settings, middleware)
        {
            Health = health ?? throw new ArgumentNullException(nameof(health));
        }

        public async override Task Execute(IJobExecutionContext context)
        {
            try
            {
                var result = new HeartBeat(Settings.App.Id);

                try
                {
                    var test = await Health.Test();

                    if (test != null)
                    {
                        if (!test.Status)
                            result.Errors.Add(test.Text);
                    }
                    else
                        throw new Exception("health check failed");
                }
                catch (Exception exception)
                {
                    result.Errors.Add("health check failed");
                    result.Errors.Add(exception.ToString());
                }

                await Execute(result);

                var timeout = 60000;

                if (context.PreviousFireTimeUtc.HasValue && context.NextFireTimeUtc.HasValue)
                    timeout = (int)context.NextFireTimeUtc.Value.Subtract(context.PreviousFireTimeUtc.Value).TotalMilliseconds;

                if (Middleware.Connected)
                    await Middleware.Send(result, timeout);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        protected virtual async Task Execute(HeartBeat heartbeat)
        {
            await Task.Run(() =>
            {
                // implement default heartbeat properties and tags
            });
        }
    }
}