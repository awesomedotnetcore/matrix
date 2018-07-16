using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Responses
{
    public class DeleteConfigurationResponse : Response
    {
        public bool Deleted { get; set; }

        public DeleteConfigurationResponse()
            : base(Guid.Empty)
        {
        }

        public DeleteConfigurationResponse(Guid requestId)
            : base(requestId)
        {
        }

        public DeleteConfigurationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}