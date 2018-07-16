using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Registry.Messages.Commands.Responses
{
    public class CreateApplicationResponse : Response
    {
        public Guid Id { get; set; }

        public CreateApplicationResponse()
            : base(Guid.Empty)
        {
        }

        public CreateApplicationResponse(Guid requestId)
           : base(requestId)
        {
        }

        public CreateApplicationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}