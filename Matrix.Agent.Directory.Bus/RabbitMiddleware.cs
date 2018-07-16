using EasyNetQ;
using Matrix.Agent.Directory.Bus.Endpoints;
using Matrix.Agent.Directory.Bus.Handlers;
using Matrix.Agent.Directory.Business.Services;
using Matrix.Agent.Directory.Messages.Commands.Requests;
using Matrix.Agent.Directory.Messages.Commands.Responses;
using Matrix.Agent.Messages;
using Matrix.Agent.Middlewares;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Bus
{
    public class RabbitMiddleware : Middleware
    {
        public override bool Connected { get { return Bus != null && Bus.IsConnected; } }

        private IBus Bus { get; set; }

        private IApplicationService Application { get; }

        private IUserService Users { get; }

        private IUserGroupService UserGroups { get; }

        private IUserRoleService UserRoles { get; }

        public RabbitMiddleware(IMiddlewareContext context, ILogger logger, IApplicationService application, IUserService users, IUserGroupService userGroups, IUserRoleService userRoles)
            : base(context, logger)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
            Users = users ?? throw new ArgumentNullException(nameof(users));
            UserGroups = userGroups ?? throw new ArgumentNullException(nameof(userGroups));
            UserRoles = userRoles ?? throw new ArgumentNullException(nameof(userRoles));
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Bus = RabbitHutch.CreateBus(Context.Connection);
                Bus.SubscribeAsync<HeartBeat>(Process.GetCurrentProcess().ProcessName, new HeartBeatHandler(Logger, Application).Execute);

                Bus.RespondAsync<ListUserRequest, ListUserResponse>(new UserEndpoint(Logger, Users).GetUsers);
                Bus.RespondAsync<CreateUserRequest, CreateUserResponse>(new UserEndpoint(Logger, Users).CreateUser);
                Bus.RespondAsync<UpdateUserRequest, UpdateUserResponse>(new UserEndpoint(Logger, Users).UpdateUser);
                Bus.RespondAsync<DeleteUserRequest, DeleteUserResponse>(new UserEndpoint(Logger, Users).DeleteUser);

                Bus.RespondAsync<ListUserGroupRequest, ListUserGroupResponse>(new UserGroupEndpoint(Logger, UserGroups).GetUserGroups);
                Bus.RespondAsync<CreateUserGroupRequest, CreateUserGroupResponse>(new UserGroupEndpoint(Logger, UserGroups).CreateUserGroup);
                Bus.RespondAsync<UpdateUserGroupRequest, UpdateUserGroupResponse>(new UserGroupEndpoint(Logger, UserGroups).UpdateUserGroup);
                Bus.RespondAsync<DeleteUserGroupRequest, DeleteUserGroupResponse>(new UserGroupEndpoint(Logger, UserGroups).DeleteUserGroup);

                Bus.RespondAsync<ListUserRoleRequest, ListUserRoleResponse>(new UserRoleEndpoint(Logger, UserRoles).GetUserRoles);
                Bus.RespondAsync<CreateUserRoleRequest, CreateUserRoleResponse>(new UserRoleEndpoint(Logger, UserRoles).CreateUserRole);
                Bus.RespondAsync<UpdateUserRoleRequest, UpdateUserRoleResponse>(new UserRoleEndpoint(Logger, UserRoles).UpdateUserRole);
                Bus.RespondAsync<DeleteUserRoleRequest, DeleteUserRoleResponse>(new UserRoleEndpoint(Logger, UserRoles).DeleteUserRole);
            });
        }

        public override async Task Disconnect()
        {
            await Task.Run(() => { });
        }

        public override async Task Send<T>(T o, int timeout = 0)
        {
            if (timeout.Equals(0))
            {
                await Bus.PublishAsync(o);
            }
            else
            {
                await Bus.PublishAsync(o, i =>
                {
                    i.WithExpires(timeout);
                });
            }
        }

        public override void Dispose()
        {
            Bus.Dispose();
        }
    }
}