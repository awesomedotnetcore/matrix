using Matrix.Agent.Middlewares.Handlers;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.Handlers
{
    public class RegisterApplicationHandler : Handler
    {
        private IRegistryService Server { get; }

        public RegisterApplicationHandler(ILogger logger, IRegistryService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<CreateApplicationResponse> Execute(RegisterApplicationRequest o)
        {
            var result = new CreateApplicationResponse();

            result.ApplicationId = await Server.Register(o.Name, o.Description);

            return result;
        }
    }
}