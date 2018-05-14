using NLog;
using System;

namespace Matrix.Agent.Business
{
    public class Service : IService
    {
        protected IServiceContext Context { get; }

        protected ILogger Logger { get; }

        public Service(IServiceContext context, ILogger logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}