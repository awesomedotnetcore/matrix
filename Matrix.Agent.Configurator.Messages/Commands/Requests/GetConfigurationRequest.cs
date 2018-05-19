using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Requests
{
    public class GetConfigurationRequest : Request
    {
        public string Key { get; set; }

        public GetConfigurationRequest()
        {
        }

        public GetConfigurationRequest(Guid app)
            : base(app)
        {
        }
    }
}