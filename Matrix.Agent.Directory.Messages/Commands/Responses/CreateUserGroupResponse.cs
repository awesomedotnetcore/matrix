using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.CreateUserGroupResponse", ExchangeName = "Directory.CreateUserGroupResponse")]
    public class CreateUserGroupResponse : Response
    {
        public Guid Id { get; set; }

        public CreateUserGroupResponse()
            : base(Guid.Empty)
        {
        }

        public CreateUserGroupResponse(Guid requestId)
           : base(requestId)
        {
        }

        public CreateUserGroupResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}
