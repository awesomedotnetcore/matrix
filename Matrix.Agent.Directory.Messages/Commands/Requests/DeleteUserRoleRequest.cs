using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.DeleteUserRoleRequest", ExchangeName = "Directory.DeleteUserRoleRequest")]
    public class DeleteUserRoleRequest : Request
    {
        public Guid Id { get; set; }

        public DeleteUserRoleRequest()
        {
        }

        public DeleteUserRoleRequest(Guid app)
            : base(app)
        {
        }
    }
}