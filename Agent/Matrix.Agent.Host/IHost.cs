using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Host
{
    public interface IHost
    {
        event EventHandler OnStart;

        event EventHandler OnStop;

        Task Start();

        Task Stop();
    }
}