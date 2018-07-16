using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Jobs;
using Matrix.Agent.Middlewares;
using NLog;

namespace Matrix.Agent.Directory
{
    public class Pulse : Beacon
    {
        public Pulse(ILogger logger, IMiddleware middleware, IConfiguration settings, IHealthService health)
            : base(logger, middleware, settings, health)
        {
        }
    }
}