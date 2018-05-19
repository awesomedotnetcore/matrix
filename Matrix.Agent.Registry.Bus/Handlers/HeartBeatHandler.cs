using Matrix.Agent.Messages;
using Matrix.Agent.Middlewares.Handlers;
using NLog;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.Handlers
{
    public class HeartBeatHandler : Handler
    {
        public HeartBeatHandler(ILogger logger)
            : base(logger)
        {
        }

        public async Task Execute(HeartBeat o)
        {
            await Task.Run(() =>
            {
                Logger.Info($"HEARTBEAT | {o.Hostname} | {o.Program} | {o.Process}");
            });
        }
    }
}