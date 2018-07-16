using EasyNetQ;
using Matrix.Agent.Directory.Model;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.GetUserResponse", ExchangeName = "Directory.GetUserResponse")]
    public class GetUserResponse : Response
    {
        public User User { get; set; }

        public GetUserResponse()
            : base(Guid.Empty)
        {
        }

        public GetUserResponse(Guid requestId)
           : base(requestId)
        {
        }

        public GetUserResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}