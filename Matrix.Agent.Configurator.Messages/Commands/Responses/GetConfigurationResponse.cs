using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Responses
{
    public class GetConfigurationResponse : Response
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public GetConfigurationResponse()
            : base(Guid.Empty)
        {
        }

        public GetConfigurationResponse(Guid requestId)
            : base(requestId)
        {
        }

        public GetConfigurationResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}