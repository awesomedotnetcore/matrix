using Matrix.Agent.Database;
using NLog;

namespace Matrix.Agent.Registry.Database.SqlServer
{
    public class SqlServerRepository : Repository
    {
        public SqlServerRepository(IDatabaseContext context, ILogger logger)
            : base(context, logger)
        {
        }
    }
}