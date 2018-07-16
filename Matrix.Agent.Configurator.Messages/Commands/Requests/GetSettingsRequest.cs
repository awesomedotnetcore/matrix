using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Requests
{
    public class GetSettingsRequest : Request
    {
        public GetSettingsRequest()
        {
        }

        public GetSettingsRequest(Guid app)
            : base(app)
        {
        }
    }
}