using Matrix.Agent.Business;
using Matrix.Agent.Directory.Database.Repositories;
using Matrix.Agent.Directory.Model;
using Matrix.Extension;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Business.Services
{
    public class UserService : Service, IUserService
    {
        public IUserRepository Repository { get; }

        public UserService(IServiceContext context, ILogger logger, IUserRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // .ctor

        public async Task<List<User>> GetUsers(Guid application)
        {
            var result = new List<User>();

            Logger.Begin();

            try
            {
                result.AddRange(await Repository.GetUsers(application));
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // GetUser

        public async Task<User> GetUserById(Guid userId)
        {
            User result = null;

            Logger.Begin();

            try
            {
                result = await Repository.GetUserById(userId);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<User> GetUserByUsername(Guid application, string username)
        {
            User result = null;

            Logger.Begin();

            try
            {
                result = await Repository.GetUserByUsername(application, username);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<User> GetUserByEmail(Guid application, string email)
        {
            User result = null;

            Logger.Begin();

            try
            {
                result = await Repository.GetUserByEmail(application, email);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // CreateUser

        public async Task<Guid> CreateUser(Guid application, string username, string password, string firstName, string lastName, string email, string phone)
        {
            var result = Guid.Empty;

            Logger.Begin();

            try
            {
                result = await Repository.CreateUser(application, username, password, firstName, lastName, email, phone);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // UpdateUser

        public async Task<bool> UpdateUserProfileById(Guid userId, string firstName, string lastName, string email, string phone)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.UpdateUserProfileById(userId, firstName, lastName, email, phone);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> UpdateUserProfileByUsername(Guid application, string username, string firstName, string lastName, string email, string phone)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.UpdateUserProfileByUsername(application, username, firstName, lastName, email, phone);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> UpdateUserProfileByEmail(Guid application, string email, string firstName, string lastName, string phone)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.UpdateUserProfileByEmail(application, email, firstName, lastName, phone);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // UpdateUserPassword

        public async Task<bool> UpdateUserPasswordById(Guid userId, string password)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.UpdateUserPasswordById(userId, password);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> UpdateUserPasswordByUsername(Guid application, string username, string password)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.UpdateUserPasswordByUsername(application, username, password);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> UpdateUserPasswordByEmail(Guid application, string email, string password)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.UpdateUserPasswordByEmail(application, email, password);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // AddUserGroups

        public async Task<bool> AddUserGroupsById(Guid userId, params Guid[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserGroupsById(userId, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserGroupsByUsername(Guid application, string username, params Guid[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserGroupsByEmail(application, username, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserGroupsByUsername(Guid application, string username, params string[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserGroupsByEmail(application, username, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserGroupsByEmail(Guid application, string email, params Guid[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserGroupsByEmail(application, email, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserGroupsByEmail(Guid application, string email, params string[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserGroupsByEmail(application, email, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // AddUserRoles

        public async Task<bool> AddUserRolesById(Guid userId, params Guid[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserRolesById(userId, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserRolesUsername(Guid application, string username, params Guid[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserRolesUsername(application, username, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserRolesUsername(Guid application, string username, params string[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserRolesUsername(application, username, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserRolesByEmail(Guid application, string email, params Guid[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserRolesByEmail(application, email, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> AddUserRolesByEmail(Guid application, string email, params string[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.AddUserRolesByEmail(application, email, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // RemoveUserGroups

        public async Task<bool> RemoveUserGroupsById(Guid userId, params Guid[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserGroupsById(userId, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params Guid[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserGroupsByUsername(application, username, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params string[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserGroupsByUsername(application, username, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params Guid[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserGroupsByEmail(application, email, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params string[] groups)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserGroupsByEmail(application, email, groups);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // RemoveUserRoles

        public async Task<bool> RemoveUserRolesById(Guid userId, params Guid[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserRolesById(userId, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserRolesByUsername(Guid application, string username, params Guid[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserRolesByUsername(application, username, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserRolesByUsername(Guid application, string username, params string[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserRolesByUsername(application, username, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserRolesByEmail(Guid application, string email, params Guid[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserRolesByEmail(application, email, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> RemoveUserRolesByEmail(Guid application, string email, params string[] roles)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.RemoveUserRolesByEmail(application, email, roles);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        // DeleteUser

        public async Task<bool> DeleteUserById(Guid userId)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.DeleteUserById(userId);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> DeleteUserByUsername(Guid application, string username)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.DeleteUserByUsername(application, username);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> DeleteUserByEmail(Guid application, string email)
        {
            var result = false;

            Logger.Begin();

            try
            {
                result = await Repository.DeleteUserByEmail(application, email);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }
    }
}