using Matrix.Agent.Business;
using Matrix.Agent.Registry.Database;
using Matrix.Agent.Registry.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Business
{
    public class RegistryService : Service, IRegistryService
    {
        public IApplicationRepository Repository { get; }

        public RegistryService(IServiceContext context, ILogger logger, IApplicationRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Application>> GetApplications()
        {
            var result = new List<Application>();

            Logger.Trace("BEGIN | Matrix.Server.Registry.Business.ApplicationService.GetApplications");

            try
            {
                if (Repository != null)
                    result.AddRange(await Repository.GetApplications());
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Business.ApplicationService.GetApplications");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Business.ApplicationService.GetApplications");

            return result;
        }

        public async Task<Application> GetApplicationById(Guid id)
        {
            Application result = null;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Business.ApplicationService.GetApplicationById");

            try
            {
                if (Repository != null)
                    result = await Repository.GetApplicationById(id);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Business.ApplicationService.GetApplicationById");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Business.ApplicationService.GetApplicationById");

            return result;
        }

        public async Task<bool> ContainsApplication(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Business.ApplicationService.ContainsApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.ContainsApplication(id);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Business.ApplicationService.ContainsApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Business.ApplicationService.ContainsApplication");

            return result;
        }

        public async Task<Guid> Register(string name, string description)
        {
            var result = Guid.Empty;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Business.ApplicationService.CreateApplication");

            try
            {
                if (Repository != null)
                {
                    var id = Guid.NewGuid();

                    if (await Repository.CreateApplication(id, name, description))
                        result = id;
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Business.ApplicationService.CreateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Business.ApplicationService.CreateApplication");

            return result;
        }

        public async Task<bool> Update(Guid id, string name, string description)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Business.ApplicationService.UpdateApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.UpdateApplication(id, name, description);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Business.ApplicationService.UpdateApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Business.ApplicationService.UpdateApplication");

            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = false;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Business.ApplicationService.DeleteApplication");

            try
            {
                if (Repository != null)
                    result = await Repository.DeleteApplication(id);
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Business.ApplicationService.DeleteApplication");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Business.ApplicationService.DeleteApplication");

            return result;
        }
    }
}