using Matrix.Agent.Configuration;
using NLog;

namespace Matrix.Agent.Database.Embedded
{
    public abstract class EmbeddedRepository : Repository, IEmbeddedRepository
    {
        public EmbeddedRepository(IDatabaseContext context, IConfiguration configuration, ILogger logger)
            : base(context, configuration, logger)
        {
        }
    }
}