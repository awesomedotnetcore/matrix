using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.DeleteUserResponse", ExchangeName = "Directory.DeleteUserResponse")]
    public class DeleteUserResponse : Response
    {
        public bool Deleted { get; set; }

        public DeleteUserResponse()
            : base(Guid.Empty)
        {
        }

        public DeleteUserResponse(Guid requestId)
           : base(requestId)
        {
        }

        public DeleteUserResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}