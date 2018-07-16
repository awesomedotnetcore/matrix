using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.UpdateUserResponse", ExchangeName = "Directory.UpdateUserResponse")]
    public class UpdateUserResponse : Response
    {
        public bool Updated { get; set; }

        public UpdateUserResponse()
            : base(Guid.Empty)
        {
        }

        public UpdateUserResponse(Guid requestId)
           : base(requestId)
        {
        }

        public UpdateUserResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}