using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class DeleteApplicationResponse : Response
    {
        public bool Deleted { get; set; }

        public DeleteApplicationResponse()
            : base(Guid.Empty)
        {
        }

        public DeleteApplicationResponse(Guid requestId)
            : base(requestId)
        {
        }

        public DeleteApplicationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}