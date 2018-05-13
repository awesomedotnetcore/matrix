using Matrix.Agent.Database;
using Matrix.Agent.Model;
using Matrix.Agent.Registry.Model;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Database.Embedded
{
    public class HealthRepository : EmbeddedRepository, IHealthRepository
    {
        public HealthRepository(IDatabaseContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public async Task<HealthTestResult> Test()
        {
            var result = new HealthTestResult();

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.Embedded.HealthRepository.HealthCheck");

            try
            {
                await Task.Run(() =>
                {
                    result.Status = db?.GetCollection<Application>() != null;
                    result.Text = result.Status ? "OK" : "database connection failed";
                });
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.Embedded.HealthRepository.HealthCheck");
                Logger.Error(e);

                result.Status = false;
                result.Text = e.Message;
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.Embedded.ApplicationRepository.CreateApplication");

            return result;
        }
    }
}