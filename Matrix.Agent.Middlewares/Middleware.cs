using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Middlewares
{
    public abstract class Middleware : IMiddleware
    {
        protected IMiddlewareContext Context { get; }

        protected ILogger Logger { get; }

        public abstract bool Connected { get; }

        public Middleware(IMiddlewareContext context, ILogger logger)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public abstract Task Connect();

        public abstract Task Disconnect();

        public abstract Task Send<T>(T o, int timeout = 0) where T : class;

        public virtual void Dispose()
        {
        }
    }
}