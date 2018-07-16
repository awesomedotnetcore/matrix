using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Configurator.Messages.Commands.Requests
{
    public class DeleteConfigurationRequest : Request
    {
        public string Key { get; set; }

        public DeleteConfigurationRequest()
        {
        }

        public DeleteConfigurationRequest(Guid app)
            : base(app)
        {
        }
    }
}