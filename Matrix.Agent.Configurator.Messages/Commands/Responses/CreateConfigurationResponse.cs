using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Responses
{
    public class CreateConfigurationResponse : Response
    {
        public bool Created { get; set; }

        public CreateConfigurationResponse()
            : base(Guid.Empty)
        {
        }

        public CreateConfigurationResponse(Guid requestId)
            : base(requestId)
        {
        }

        public CreateConfigurationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}