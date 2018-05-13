using LiteDB;
using Matrix.Agent.Database;
using NLog;

namespace Matrix.Agent.Registry.Database.Embedded
{
    public class EmbeddedRepository : Repository
    {
        protected LiteDatabase db;

        public EmbeddedRepository(IDatabaseContext context, ILogger logger)
            : base(context, logger)
        {
            db = new LiteDatabase(context.Connection);
        }
    }
}