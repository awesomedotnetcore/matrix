using Matrix.Agent.Directory.Business.Services;
using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Matrix.Agent.Middlewares.Responders;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Bus.Endpoints
{
    public class UserEndpoint : Endpoint
    {
        public IUserService Server { get; }

        public UserEndpoint(ILogger logger, IUserService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<ListUserResponse> GetUsers(ListUserRequest o)
        {
            var result = new ListUserResponse(o.RequestId);

            result.Application = o.Application;
            result.Users.AddRange(await Server.GetUsers(o.Application));

            return result;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest o)
        {
            var result = new CreateUserResponse(o.RequestId);

            result.Application = o.Application;
            result.Id = await Server.CreateUser(o.Application, o.Username, o.Password, o.FirstName, o.LastName, o.Email, o.Phone);

            return result;
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest o)
        {
            var result = new UpdateUserResponse(o.RequestId);

            result.Application = o.Application;
            result.Updated = await Server.UpdateUserProfileById(o.Id, o.FirstName, o.LastName, o.Email, o.Phone);

            return result;
        }

        public async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest o)
        {
            var result = new DeleteUserResponse(o.RequestId);

            result.Application = o.Application;
            result.Deleted = await Server.DeleteUserById(o.Id);

            return result;
        }
    }
}