using Matrix.Agent.Business;
using Matrix.Agent.Directory.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Business.Services
{
    public interface IApplicationService : IService
    {
        Task<List<Application>> GetApplications();

        Task<bool> HasApplication(Guid id);

        Task<bool> CreateApplication(Guid id, string name, string description);

        Task<bool> UpdateApplication(Guid id, string name, string description);

        Task<bool> DeleteApplication(Guid id);
    }
}