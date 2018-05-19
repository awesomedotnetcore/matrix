using Matrix.Agent.Middlewares.Handlers;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.Handlers
{
    public class ListApplicationHandler : Handler
    {
        private IRegistryService Server { get; }

        public ListApplicationHandler(ILogger logger, IRegistryService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<ListApplicationResponse> Execute(ListApplicationRequest o)
        {
            var result = new ListApplicationResponse();

            result.Applications.AddRange(await Server.GetApplications());

            return result;
        }
    }
}