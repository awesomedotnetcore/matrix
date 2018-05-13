using Matrix.Agent.Configuration;
using Matrix.Agent.Middlewares;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Host
{
    public class ServerHost : IHost
    {
        private IConfiguration Configuration { get; }

        private IMiddleware Middleware { get; set; }

        private IScheduler Scheduler { get; }

        public event EventHandler OnStart;

        public event EventHandler OnStop;

        public ServerHost(IMiddleware middleware, IConfiguration configuration, IScheduler scheduler)
        {
            Middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public async Task Start()
        {
            await Middleware.Connect();
            await Scheduler.Start();

            OnStart?.Invoke(this, new EventArgs());
        }

        public async Task Stop()
        {
            OnStop?.Invoke(this, new EventArgs());

            await Scheduler.Shutdown();
            await Middleware.Disconnect();
        }
    }
}