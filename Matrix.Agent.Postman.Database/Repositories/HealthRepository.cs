using Matrix.Agent.Configuration;
using Matrix.Agent.Database;
using Matrix.Agent.Model;
using NLog;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Database.Repositories
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

            Logger.Trace("BEGIN | Matrix.Server.Postman.Database.HealthRepository.Test");

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
                Logger.Trace("ERROR | Matrix.Server.Postman.Database.HealthRepository.Test");
                Logger.Error(e);

                result.Status = false;
                result.Text = e.Message;
            }

            Logger.Trace("END | Matrix.Server.Postman.Database.ApplicationRepository.Test");

            return result;
        }
    }
}