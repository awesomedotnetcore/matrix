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
    public class UserGroupService : Service, IUserGroupService
    {
        public IUserGroupRepository Repository { get; }

        public UserGroupService(IServiceContext context, ILogger logger, IUserGroupRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<UserGroup>> GetUserGroups(Guid application)
        {
            var result = new List<UserGroup>();

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result.AddRange(await Repository.GetUserGroups(application));
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<UserGroup> GetUserGroupById(Guid id)
        {
            UserGroup result = null;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.GetUserGroupById(id);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<UserGroup> GetUserGroupByName(Guid application, string name)
        {
            UserGroup result = null;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.GetUserGroupByName(application, name);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<Guid> CreateUserGroup(Guid application, string name, string description)
        {
            var result = Guid.Empty;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.CreateUserGroup(application, name, description);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> UpdateUserGroup(Guid id, string name, string description)
        {
            var result = false;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.UpdateUserGroup(id, name, description);
            }
            catch (Exception e)
            {
                Logger.Exception(e);
            }

            Logger.End();

            return result;
        }

        public async Task<bool> DeleteUserGroup(Guid id)
        {
            var result = false;

            Logger.Begin();

            try
            {
                if (Repository != null)
                    result = await Repository.DeleteUserGroup(id);
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