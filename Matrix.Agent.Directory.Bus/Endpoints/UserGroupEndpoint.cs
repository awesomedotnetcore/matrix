using Matrix.Agent.Directory.Business.Services;
using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Matrix.Agent.Middlewares.Responders;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Bus.Endpoints
{
    public class UserGroupEndpoint : Endpoint
    {
        public IUserGroupService Server { get; }

        public UserGroupEndpoint(ILogger logger, IUserGroupService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<ListUserGroupResponse> GetUserGroups(ListUserGroupRequest o)
        {
            var result = new ListUserGroupResponse(o.RequestId);

            result.Application = o.Application;
            result.UserGroups.AddRange(await Server.GetUserGroups(o.Application));

            return result;
        }

        public async Task<CreateUserGroupResponse> CreateUserGroup(CreateUserGroupRequest o)
        {
            var result = new CreateUserGroupResponse(o.RequestId);

            result.Application = o.Application;
            result.Id = await Server.CreateUserGroup(o.Application, o.Name, o.Description);

            return result;
        }

        public async Task<UpdateUserGroupResponse> UpdateUserGroup(UpdateUserGroupRequest o)
        {
            var result = new UpdateUserGroupResponse(o.RequestId);

            result.Application = o.Application;
            result.Updated = await Server.UpdateUserGroup(o.Id, o.Name, o.Description);

            return result;
        }

        public async Task<DeleteUserGroupResponse> DeleteUserGroup(DeleteUserGroupRequest o)
        {
            var result = new DeleteUserGroupResponse(o.RequestId);

            result.Application = o.Application;
            result.Deleted = await Server.DeleteUserGroup(o.Id);

            return result;
        }
    }
}