using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Jobs;
using Matrix.Agent.Messages;
using Matrix.Agent.Middlewares;
using NLog;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator
{
    public class Pulse : Beacon
    {
        public Pulse(ILogger logger, IMiddleware middleware, IConfiguration settings, IHealthService health)
            : base(logger, middleware, settings, health)
        {
        }

        protected override async Task Execute(HeartBeat heartbeat)
        {
        }
    }
}