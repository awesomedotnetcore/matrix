using Matrix.Agent.Messages.Commands.Responses;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Configurator.Messages.Commands.Responses
{
    public class GetSettingsResponse : Response
    {
        public List<KeyValuePair<string, string>> Settings { get; set; }

        public GetSettingsResponse()
            : base(Guid.Empty)
        {
            Settings = new List<KeyValuePair<string, string>>();
        }

        public GetSettingsResponse(Guid requestId)
            : base(requestId)
        {
            Settings = new List<KeyValuePair<string, string>>();
        }

        public GetSettingsResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
            Settings = new List<KeyValuePair<string, string>>();
        }
    }
}