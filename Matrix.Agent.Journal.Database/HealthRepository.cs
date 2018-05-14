using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Model;
using NLog;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Database
{
    public class HealthRepository : Repository, IHealthRepository
    {
        public HealthRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }

        public async Task<HealthTestResult> Test()
        {
            var result = new HealthTestResult();

            Logger.Trace("BEGIN | Matrix.Server.Log.Database.SqlServer.HealthRepository.HealthCheck");

            DbConnection connection = null;

            try
            {
                using (connection = GetDbConnection())
                {
                    await connection.OpenAsync();

                    result.Status = true;
                    result.Text = "OK";

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Log.Database.SqlServer.HealthRepository.HealthCheck");
                Logger.Error(e);

                result.Status = false;
                result.Text = e.Message;
            }

            Logger.Trace("END | Matrix.Server.Log.Database.SqlServer.ApplicationRepository.CreateApplication");

            return result;
        }
    }
}