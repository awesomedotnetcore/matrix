using Matrix.Agent.Business;
using Matrix.Agent.Database;
using Matrix.Agent.Model;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Business
{
    public class HealthService : Service, IHealthService
    {
        public IHealthRepository Repository { get; }

        public HealthService(IServiceContext context, ILogger logger, IHealthRepository repository)
            : base(context, logger)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<HealthTestResult> Test()
        {
            HealthTestResult result = null;

            Logger.Trace("BEGIN | Matrix.Server.Registry.Business.HealthService.Test");

            try
            {
                if (Repository != null)
                    result = await Repository.Test();
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Business.HealthService.Test");
                Logger.Error(e);
            }

            Logger.Trace("END | Matrix.Server.Registry.Business.HealthService.Test");

            return result;
        }
    }
}