using Matrix.Agent.Business;
using Matrix.Agent.Configurator.Database;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator.Business
{
    public class ConfigurationService : Service, IConfigurationService
    {
        public IConfigurationRepository Repository { get; }

        public ConfigurationService(IServiceContext context, ILogger logger, IConfigurationRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<KeyValuePair<string, string>>> GetSettings(Guid application)
        {
            var result = new List<KeyValuePair<string, string>>();

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Business.ConfigurationService.GetSettings");

            try
            {
                if (Repository != null)
                {
                    foreach (var i in await Repository.GetSettings(application))
                        result.Add(new KeyValuePair<string, string>(i.Key, i.Value));
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Business.ConfigurationService.GetSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Business.ConfigurationService.GetSettings");

            return result;
        }

        public async Task<string> GetSettings(Guid application, string key)
        {
            var result = string.Empty;

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Business.ConfigurationService.GetSettings");

            try
            {
                if (Repository != null)
                {
                    var config = await Repository.GetSettings(application, key);

                    if (config != null)
                        result = config.Value;
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Business.ConfigurationService.GetSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Business.ConfigurationService.GetSettings");

            return result;
        }

        public async Task<bool> CreateSettings(Guid application, string key, string value)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Business.ConfigurationService.CreateSettings");

            try
            {
                if (Repository != null)
                    result = await Repository.CreateSettings(application, key, value);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Business.ConfigurationService.CreateSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Business.ConfigurationService.CreateSettings");

            return result;
        }

        public async Task<bool> UpdateSettings(Guid application, string key, string value)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Business.ConfigurationService.UpdateSettings");

            try
            {
                if (Repository != null)
                    result = await Repository.UpdateSettings(application, key, value);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Business.ConfigurationService.UpdateSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Business.ConfigurationService.UpdateSettings");

            return result;
        }

        public async Task<bool> DeleteSettings(Guid application, string key)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Configurator.Business.ConfigurationService.DeleteSettings");

            try
            {
                if (Repository != null)
                    result = await Repository.DeleteSettings(application, key);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Configurator.Business.ConfigurationService.DeleteSettings");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Configurator.Business.ConfigurationService.DeleteSettings");

            return result;
        }
    }
}