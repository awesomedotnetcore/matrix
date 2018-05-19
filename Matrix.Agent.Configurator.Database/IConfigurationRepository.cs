using Matrix.Agent.Configurator.Model;
using Matrix.Agent.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator.Database
{
    public interface IConfigurationRepository : IRepository
    {
        Task<List<KeyValuePair>> GetSettings(Guid application);

        Task<KeyValuePair> GetSettings(Guid application, string key);

        Task<bool> CreateSettings(Guid application, string key, string value);

        Task<bool> UpdateSettings(Guid application, string key, string value);

        Task<bool> DeleteSettings(Guid application, string key);
    }
}