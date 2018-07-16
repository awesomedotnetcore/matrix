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
    public class UserRepository : SqlServerRepository, IUserRepository
    {
        public UserRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        // GetUsers

        public Task<List<User>> GetUsers(Guid application)
        {
            throw new NotImplementedException();
        }

        // GetUser

        public Task<User> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsername(Guid application, string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(Guid application, string email)
        {
            throw new NotImplementedException();
        }

        // CreateUser

        public Task<Guid> CreateUser(Guid application, string username, string password, string firstName, string lastName, string email, string phone)
        {
            throw new NotImplementedException();
        }

        // UpdateUserProfile

        public Task<bool> UpdateUserProfileById(Guid userId, string firstName, string lastName, string email, string phone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserProfileByUsername(Guid application, string username, string firstName, string lastName, string email, string phone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserProfileByEmail(Guid application, string email, string firstName, string lastName, string phone)
        {
            throw new NotImplementedException();
        }

        // UpdateUserPassword

        public Task<bool> UpdateUserPasswordByEmail(Guid application, string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserPasswordById(Guid userId, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserPasswordByUsername(Guid application, string username, string password)
        {
            throw new NotImplementedException();
        }

        // AddUserGroups

        public Task<bool> AddUserGroupsById(Guid userId, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByUsername(Guid application, string username, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByUsername(Guid application, string username, params string[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByEmail(Guid application, string email, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserGroupsByEmail(Guid application, string email, params string[] groups)
        {
            throw new NotImplementedException();
        }

        // AddUserRoles

        public Task<bool> AddUserRolesById(Guid userId, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesUsername(Guid application, string username, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesUsername(Guid application, string username, params string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesByEmail(Guid application, string email, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserRolesByEmail(Guid application, string email, params string[] roles)
        {
            throw new NotImplementedException();
        }

        // RemoveUserGroups

        public Task<bool> RemoveUserGroupsById(Guid userId, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params string[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params Guid[] groups)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params string[] groups)
        {
            throw new NotImplementedException();
        }

        // RemoveUserRoles

        public Task<bool> RemoveUserRolesById(Guid userId, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByUsername(Guid application, string username, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByUsername(Guid application, string username, params string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByEmail(Guid application, string email, params Guid[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveUserRolesByEmail(Guid application, string email, params string[] roles)
        {
            throw new NotImplementedException();
        }

        // DeleteUser

        public Task<bool> DeleteUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserByUsername(Guid application, string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserByEmail(Guid application, string email)
        {
            throw new NotImplementedException();
        }
    }
}