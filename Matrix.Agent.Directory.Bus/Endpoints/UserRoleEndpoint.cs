using Matrix.Agent.Directory.Business.Services;
using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Matrix.Agent.Middlewares.Responders;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Bus.Endpoints
{
    public class UserRoleEndpoint : Endpoint
    {
        public IUserRoleService Server { get; }

        public UserRoleEndpoint(ILogger logger, IUserRoleService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<ListUserRoleResponse> GetUserRoles(ListUserRoleRequest o)
        {
            var result = new ListUserRoleResponse(o.RequestId);

            result.Application = o.Application;
            result.UserRoles.AddRange(await Server.GetUserRoles(o.Application));

            return result;
        }

        public async Task<CreateUserRoleResponse> CreateUserRole(CreateUserRoleRequest o)
        {
            var result = new CreateUserRoleResponse(o.RequestId);

            result.Application = o.Application;
            result.Id = await Server.CreateUserRole(o.Application, o.Name, o.Description);

            return result;
        }

        public async Task<UpdateUserRoleResponse> UpdateUserRole(UpdateUserRoleRequest o)
        {
            var result = new UpdateUserRoleResponse(o.RequestId);

            result.Application = o.Application;
            result.Updated = await Server.UpdateUserRole(o.Id, o.Name, o.Description);

            return result;
        }

        public async Task<DeleteUserRoleResponse> DeleteUserRole(DeleteUserRoleRequest o)
        {
            var result = new DeleteUserRoleResponse(o.RequestId);

            result.Application = o.Application;
            result.Deleted = await Server.DeleteUserRole(o.Id);

            return result;
        }
    }
}