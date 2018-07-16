using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class UpdateApplicationResponse : Response
    {
        public bool Updated { get; set; }

        public UpdateApplicationResponse()
            : base(Guid.Empty)
        {
        }

        public UpdateApplicationResponse(Guid requestId)
            : base(requestId)
        {
        }

        public UpdateApplicationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}