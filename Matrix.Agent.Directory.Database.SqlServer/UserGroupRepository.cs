using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Database.SqlServer;
using Matrix.Agent.Directory.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Database.SqlServer
{
    public class UserGroupRepository : SqlServerRepository, IUserGroupRepository
    {
        public UserGroupRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public Task<List<UserGroup>> GetUserGroups(Guid application)
        {
            throw new NotImplementedException();
        }

        public Task<UserGroup> GetUserGroupById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserGroup> GetUserGroupByName(Guid application, string name)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateUserGroup(Guid application, string name, string description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserGroup(Guid id, string name, string description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserGroup(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}