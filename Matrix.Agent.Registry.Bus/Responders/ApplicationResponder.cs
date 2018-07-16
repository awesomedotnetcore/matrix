using Matrix.Agent.Middlewares.Responders;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.Responders
{
    public class ApplicationResponder : Endpoint
    {
        private IRegistryService Server { get; }

        public ApplicationResponder(ILogger logger, IRegistryService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<ListApplicationResponse> GetApplications(ListApplicationRequest o)
        {
            var result = new ListApplicationResponse(o.RequestId);

            result.Applications.AddRange(await Server.GetApplications());

            return result;
        }

        public async Task<GetApplicationByIdResponse> GetApplicationById(GetApplicationByIdRequest o)
        {
            var result = new GetApplicationByIdResponse(o.RequestId);

            result.Application = await Server.GetApplicationById(o.Id);

            return result;
        }

        public async Task<CreateApplicationResponse> Register(RegisterApplicationRequest o)
        {
            var result = new CreateApplicationResponse(o.RequestId);

            result.Id = await Server.Register(o.Name, o.Description);

            return result;
        }

        public async Task<UpdateApplicationResponse> Update(UpdateApplicationRequest o)
        {
            var result = new UpdateApplicationResponse(o.RequestId);

            result.Updated = await Server.Update(o.Id, o.Name, o.Description);

            return result;
        }

        public async Task<DeleteApplicationResponse> Delete(DeleteApplicationRequest o)
        {
            var result = new DeleteApplicationResponse(o.RequestId);

            result.Deleted = await Server.Delete(o.Id);

            return result;
        }
    }
}