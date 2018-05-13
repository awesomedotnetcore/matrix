using System;
using System.Threading.Tasks;

namespace Matrix.SDK.Middlewares
{
    public interface IMiddleware : IDisposable
    {
        bool Connected { get; }

        Task Connect();

        Task Disconnect();

        Task Send<T>(T o, int timeout = 0) where T : class;
    }

    public abstract class Middleware : IMiddleware
    {
        protected IMiddlewareContext Context { get; }

        public abstract bool Connected { get; }

        public Middleware(IMiddlewareContext context)
        {
            Context = context;
        }

        public abstract Task Connect();

        public abstract Task Disconnect();

        public abstract Task Send<T>(T o, int timeout = 0) where T : class;

        public virtual void Dispose()
        {
        }
    }
}