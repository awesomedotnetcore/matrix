using Matrix.Agent.Configurator.Business;
using Matrix.Agent.Configurator.Messages.Commands.Requests;
using Matrix.Agent.Middlewares.Handlers;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator.Bus.Handlers
{
    public class ConfigurationHandler : Handler
    {
        private IConfigurationService Application { get; }

        public ConfigurationHandler(ILogger logger, IConfigurationService application)
            : base(logger)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
        }

        public async Task Handle(GetConfigurationRequest o)
        {
        }
    }
}