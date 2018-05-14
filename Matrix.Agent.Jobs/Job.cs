using Matrix.Agent.Configuration;
using Matrix.Agent.Middlewares;
using NLog;
using Quartz;
using System.Threading.Tasks;

namespace Matrix.Agent.Jobs
{
    public abstract class Job : IJob
    {
        protected IConfiguration Settings { get; }

        protected ILogger Logger { get; }

        protected IMiddleware Middleware { get; }

        public Job(ILogger logger, IConfiguration settings, IMiddleware middleware)
        {
            Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            Settings = settings ?? throw new System.ArgumentNullException(nameof(settings));
            Middleware = middleware ?? throw new System.ArgumentNullException(nameof(middleware));
        }

        public abstract Task Execute(IJobExecutionContext context);
    }
}