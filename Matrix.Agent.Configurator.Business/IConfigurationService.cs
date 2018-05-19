using Matrix.Agent.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator.Business
{
    public interface IConfigurationService : IService
    {
        Task<List<KeyValuePair<string, string>>> GetSettings(Guid application);

        Task<string> GetSettings(Guid application, string key);

        Task<bool> UpdateSettings(Guid application, string key, string value);

        Task<bool> DeleteSettings(Guid application, string key);
    }
}