using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Requests
{
    public class UpdateConfigurationRequest : Request
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public UpdateConfigurationRequest()
        {
        }

        public UpdateConfigurationRequest(Guid app)
            : base(app)
        {
        }
    }
}