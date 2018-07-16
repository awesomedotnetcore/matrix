using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Responses
{
    public class UpdateConfigurationResponse : Response
    {
        public bool Updated { get; set; }

        public UpdateConfigurationResponse()
            : base(Guid.Empty)
        {
        }

        public UpdateConfigurationResponse(Guid requestId)
            : base(requestId)
        {
        }

        public UpdateConfigurationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}