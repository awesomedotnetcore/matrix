using Matrix.Agent.Business;
using Matrix.Agent.Directory.Database.Repositories;
using Matrix.Agent.Directory.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Business.Services
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

            Logger.Trace("BEGIN | Matrix.Server.Directory.Business.ApplicationService.GetApplications");

            try
            {
                if (Repository != null)
                    result.AddRange(await Repository.GetApplications());
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Business.ApplicationService.GetApplications");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Business.ApplicationService.GetApplications");

            return result;
        }

        public async Task<bool> HasApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Business.ApplicationService.HasApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.ContainsApplication(id);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Business.ApplicationService.HasApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Business.ApplicationService.HasApplication");

            return result;
        }

        public async Task<bool> CreateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Business.ApplicationService.CreateApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.CreateApplication(id, name, description);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Business.ApplicationService.CreateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Business.ApplicationService.CreateApplication");

            return result;
        }

        public async Task<bool> UpdateApplication(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Business.ApplicationService.UpdateApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.UpdateApplication(id, name, description);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Business.ApplicationService.UpdateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Business.ApplicationService.UpdateApplication");

            return result;
        }

        public async Task<bool> DeleteApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Directory.Business.ApplicationService.DeleteApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.DeleteApplication(id);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Directory.Business.ApplicationService.DeleteApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Directory.Business.ApplicationService.DeleteApplication");

            return result;
        }
    }
}