using Matrix.Agent.Journal.Business;
using Matrix.Agent.Journal.Model;
using Matrix.Agent.Middlewares.Handlers;
using Matrix.Messages;
using Newtonsoft.Json;
using NLog;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Bus.MSMQ
{
    public class HeartBeatHandler : Handler, IHandleMessages<HeartBeat>
    {
        public IApplicationService Application { get; }

        public HeartBeatHandler(ILogger logger, IApplicationService application)
            : base(logger)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public async Task Handle(HeartBeat o)
        {
            Logger.Info($"HEARTBEAT | {o.Hostname} | {o.Program} | {o.Process}");

            if (o.Program == "Matrix.Agent.Registry")
            {
                foreach (var i in o.Properties.Keys)
                {
                    var app = JsonConvert.DeserializeObject<Application>(o.Properties[i]);

                    if (app != null)
                    {
                        if (await Application.HasApplication(app.Id))
                            await Application.UpdateApplication(app.Id, app.Name, app.Description);
                        else
                            await Application.CreateApplication(app.Id, app.Name, app.Description);
                    }
                }
            }
        }
    }
}