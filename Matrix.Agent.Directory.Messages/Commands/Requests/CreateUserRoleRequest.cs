using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.CreateUserRoleRequest", ExchangeName = "Directory.CreateUserRoleRequest")]
    public class CreateUserRoleRequest : Request
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public CreateUserRoleRequest()
        {
        }

        public CreateUserRoleRequest(Guid app)
            : base(app)
        {
        }
    }
}