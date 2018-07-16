using Matrix.Agent.Configuration;
using NLog;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Matrix.Agent.Database
{
    public abstract class Repository : IRepository
    {
        protected IDatabaseContext Context { get; private set; }

        protected IConfiguration Configuration { get; set; }

        protected ILogger Logger { get; private set; }

        public Repository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected DbConnection GetDbConnection()
        {
            DbConnection result = null;

            if (Configuration.Database.Type.Equals(DatabaseType.SqlServer.ToString(), StringComparison.CurrentCultureIgnoreCase))
                result = new SqlConnection(Context.Connection);

            if (Configuration.Database.Type.Equals(DatabaseType.Sqlite.ToString(), StringComparison.CurrentCultureIgnoreCase))
                result = new SQLiteConnection(Context.Connection);

            return result;
        }
    }
}