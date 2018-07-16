using Matrix.Agent.Configurator.Business;
using Matrix.Agent.Configurator.Messages.Commands.Requests;
using Matrix.Agent.Configurator.Messages.Commands.Responses;
using Matrix.Agent.Middlewares.Responders;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator.Bus.Responders
{
    public class ConfigurationResponder : Endpoint
    {
        private IConfigurationService Server { get; }

        public ConfigurationResponder(ILogger logger, IConfigurationService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<GetSettingsResponse> GetSettings(GetSettingsRequest o)
        {
            var result = new GetSettingsResponse(o.RequestId);

            result.Settings.AddRange((await Server.GetSettings(o.Application)));

            return result;
        }

        public async Task<GetConfigurationResponse> GetConfiguration(GetConfigurationRequest o)
        {
            var result = new GetConfigurationResponse(o.RequestId);

            result.Key = o.Key;
            result.Value = await Server.GetSettings(o.Application, o.Key);

            return result;
        }

        public async Task<CreateConfigurationResponse> CreateConfiguration(CreateConfigurationRequest o)
        {
            var result = new CreateConfigurationResponse(o.RequestId);

            result.Created = await Server.CreateSettings(o.Application, o.Key, o.Value);

            return result;
        }

        public async Task<UpdateConfigurationResponse> UpdateConfiguration(UpdateConfigurationRequest o)
        {
            var result = new UpdateConfigurationResponse(o.RequestId);

            result.Updated = await Server.UpdateSettings(o.Application, o.Key, o.Value);

            return result;
        }

        public async Task<DeleteConfigurationResponse> DeleteConfiguration(DeleteConfigurationRequest o)
        {
            var result = new DeleteConfigurationResponse(o.RequestId);

            result.Deleted = await Server.DeleteSettings(o.Application, o.Key);

            return result;
        }
    }
}