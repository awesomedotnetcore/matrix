using Matrix.Agent.Middlewares.Handlers;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.Handlers
{
    public class DeleteApplicationHandler : Handler
    {
        private IRegistryService Server { get; }

        public DeleteApplicationHandler(ILogger logger, IRegistryService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<DeleteApplicationResponse> Execute(DeleteApplicationRequest o)
        {
            var result = new DeleteApplicationResponse();

            result.Deleted = await Server.Delete(o.ApplicationId);

            return result;
        }
    }
}