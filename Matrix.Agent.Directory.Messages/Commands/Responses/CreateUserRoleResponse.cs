using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.CreateUserRoleResponse", ExchangeName = "Directory.CreateUserRoleResponse")]
    public class CreateUserRoleResponse : Response
    {
        public Guid Id { get; set; }

        public CreateUserRoleResponse()
            : base(Guid.Empty)
        {
        }

        public CreateUserRoleResponse(Guid requestId)
           : base(requestId)
        {
        }

        public CreateUserRoleResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}