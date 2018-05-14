using Matrix.Agent.Business;
using Matrix.Agent.Journal.Business;
using Matrix.Agent.Journal.Database;
using Matrix.Agent.Journal.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Business
{
    public class ApplicationService : Service, IApplicationService
    {
        public IApplicationRepository Repository { get; }

        public ApplicationService(IServiceContext context, ILogger logger, IApplicationRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Application>> GetApplications()
        {
            var result = new List<Application>();

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.ApplicationService.GetApplications");

            try
            {
                if (Repository != null)
                    result.AddRange(await Repository.GetApplications());
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.ApplicationService.GetApplications");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.ApplicationService.GetApplications");

            return result;
        }

        public async Task<bool> HasApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.ApplicationService.HasApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.HasApplication(id);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.ApplicationService.HasApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.ApplicationService.HasApplication");

            return result;
        }

        public async Task<bool> CreateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.ApplicationService.CreateApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.CreateApplication(id, name, description);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.ApplicationService.CreateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.ApplicationService.CreateApplication");

            return result;
        }

        public async Task<bool> UpdateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.ApplicationService.UpdateApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.UpdateApplication(id, name, description);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.ApplicationService.UpdateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.ApplicationService.UpdateApplication");

            return result;
        }

        public async Task<bool> DeleteApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Log.Business.ApplicationService.DeleteApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.DeleteApplication(id);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Business.ApplicationService.DeleteApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Log.Business.ApplicationService.DeleteApplication");

            return result;
        }
    }
}