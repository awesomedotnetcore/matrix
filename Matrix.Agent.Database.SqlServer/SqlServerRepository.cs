using Matrix.Agent.Configuration;
using NLog;

namespace Matrix.Agent.Database.SqlServer
{
    public abstract class SqlServerRepository : Repository, ISqlServerRepository
    {
        public SqlServerRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }
    }
}