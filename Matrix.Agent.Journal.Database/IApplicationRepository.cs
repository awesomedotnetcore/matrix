using Matrix.Agent.Database;
using Matrix.Agent.Journal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Database
{
    public interface IApplicationRepository : IRepository
    {
        Task<List<Application>> GetApplications();

        Task<bool> HasApplication(Guid id);

        Task<bool> CreateApplication(Guid id, string name, string description);

        Task<bool> UpdateApplication(Guid id, string name, string description);

        Task<bool> DeleteApplication(Guid id);
    }
}