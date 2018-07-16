using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Database.Embedded;
using Matrix.Agent.Directory.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Database.Embedded
{
    public class UserRoleRepository : EmbeddedRepository, IUserRoleRepository
    {
        public UserRoleRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public Task<List<UserRole>> GetUserRoles(Guid application)
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> GetUserRoleById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> GetUserRoleByName(Guid application, string name)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateUserRole(Guid application, string name, string description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserRole(Guid id, string name, string description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserRole(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}