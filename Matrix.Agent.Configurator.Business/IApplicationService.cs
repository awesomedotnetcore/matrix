using Matrix.Agent.Business;
using Matrix.Agent.Configurator.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Configurator.Business
{
    public interface IApplicationService : IService
    {
        Task<List<Application>> GetApplications();

        Task<bool> ContainsApplication(Guid id);

        Task<bool> CreateApplication(Guid id, string name, string description);

        Task<bool> UpdateApplication(Guid id, string name, string description);

        Task<bool> DeleteApplication(Guid id);
    }
}