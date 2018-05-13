using Matrix.Agent.Business;
using Matrix.Agent.Registry.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Business
{
    public interface IRegistryService : IService
    {
        Task<List<Application>> GetApplications();

        Task<Application> GetApplicationById(Guid id);

        Task<bool> ContainsApplication(Guid id);

        Task<Guid> Register(string name, string description);

        Task<bool> Update(Guid id, string name, string description);

        Task<bool> Delete(Guid id);
    }
}