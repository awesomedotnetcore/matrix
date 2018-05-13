using Matrix.Agent.Database;
using Matrix.Agent.Model;
using NLog;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Database.SqlServer
{
    public class HealthRepository : SqlServerRepository, IHealthRepository
    {
        public HealthRepository(IDatabaseContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public async Task<HealthTestResult> Test()
        {
            var result = new HealthTestResult();

            Logger.Trace("BEGIN | Matrix.Server.Registry.Database.SqlServer.HealthRepository.HealthCheck");

            SqlConnection connection = null;

            try
            {
                using (connection = new SqlConnection(Context.Connection))
                {
                    await connection.OpenAsync();

                    result.Status = true;
                    result.Text = "OK";

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Trace("ERROR | Matrix.Server.Registry.Database.SqlServer.HealthRepository.HealthCheck");
                Logger.Error(e);

                result.Status = false;
                result.Text = e.Message;
            }

            Logger.Trace("END | Matrix.Server.Registry.Database.SqlServer.ApplicationRepository.CreateApplication");

            return result;
        }
    }
}