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
    public class UserRoleService : Service, IUserRoleService
    {
        public IUserRoleRepository Repository { get; }

        public UserRoleService(IServiceContext context, ILogger logger, IUserRoleRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<UserRole>> GetUserRoles(Guid application)
        {
            var result = new List<UserRole>();

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result.AddRange(await Repository.GetUserRoles(application));
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<UserRole> GetUserRoleById(Guid id)
        {
            UserRole result = null;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.GetUserRoleById(id);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<UserRole> GetUserRoleByName(Guid application, string name)
        {
            UserRole result = null;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.GetUserRoleByName(application, name);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<Guid> CreateUserRole(Guid application, string name, string description)
        {
            var result = Guid.Empty;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.CreateUserRole(application, name, description);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> UpdateUserRole(Guid id, string name, string description)
        {
            var result = false;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.UpdateUserRole(id, name, description);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> DeleteUserRole(Guid id)
        {
            var result = false;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.DeleteUserRole(id);
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