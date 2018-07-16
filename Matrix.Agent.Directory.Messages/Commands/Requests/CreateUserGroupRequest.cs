using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.CreateUserGroupRequest", ExchangeName = "Directory.CreateUserGroupRequest")]
    public class CreateUserGroupRequest : Request
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public CreateUserGroupRequest()
        {
        }

        public CreateUserGroupRequest(Guid app)
            : base(app)
        {
        }
    }
}