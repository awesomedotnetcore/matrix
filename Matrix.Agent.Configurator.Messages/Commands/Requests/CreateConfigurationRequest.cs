using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Requests
{
    public class CreateConfigurationRequest : Request
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public CreateConfigurationRequest()
        {
        }

        public CreateConfigurationRequest(Guid app)
            : base(app)
        {
        }
    }
}