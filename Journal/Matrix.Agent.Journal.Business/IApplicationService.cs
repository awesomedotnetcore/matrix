using Matrix.Agent.Business;
using Matrix.Agent.Journal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Business.Interfaces
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