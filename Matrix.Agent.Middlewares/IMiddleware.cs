using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Middlewares
{
    public interface IMiddleware : IDisposable
    {
        bool Connected { get; }

        Task Connect();

        Task Disconnect();

        Task Send<T>(T o, int timeout = 0) where T : class;
    }
}