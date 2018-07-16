using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.CreateUserResponse", ExchangeName = "Directory.CreateUserResponse")]
    public class CreateUserResponse : Response
    {
        public Guid Id { get; set; }

        public CreateUserResponse()
            : base(Guid.Empty)
        {
        }

        public CreateUserResponse(Guid requestId)
           : base(requestId)
        {
        }

        public CreateUserResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}