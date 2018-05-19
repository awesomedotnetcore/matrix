using Matrix.Agent.Business;
using Matrix.Agent.Configuration;
using Matrix.Agent.Jobs;
using Matrix.Agent.Messages;
using Matrix.Agent.Middlewares;
using Matrix.Agent.Registry.Business;
using Newtonsoft.Json;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry
{
    public class Pulse : Beacon
    {
        private IRegistryService Application { get; }

        public Pulse(ILogger logger, IMiddleware middleware, IConfiguration settings, IHealthService health, IRegistryService application)
            : base(logger, middleware, settings, health)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
        }

        protected override async Task Execute(HeartBeat heartbeat)
        {
            foreach (var i in await Application.GetApplications())
            {
                heartbeat.Tags.Add(i.Id.ToString());
                heartbeat.Properties.Add(i.Id.ToString(), JsonConvert.SerializeObject(i));
            }
        }
    }
}