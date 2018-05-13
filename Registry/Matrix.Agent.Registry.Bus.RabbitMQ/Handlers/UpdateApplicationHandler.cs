using Matrix.Agent.Middlewares.Handlers;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.RabbitMQ.Handlers
{
    public class UpdateApplicationHandler : Handler
    {
        private IRegistryService Server { get; }

        public UpdateApplicationHandler(ILogger logger, IRegistryService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<UpdateApplicationResponse> Execute(UpdateApplicationRequest o)
        {
            var result = new UpdateApplicationResponse();

            result.Updated = await Server.Update(o.ApplicationId, o.Name, o.Description);

            return result;
        }
    }
}