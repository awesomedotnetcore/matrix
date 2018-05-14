using Matrix.Agent.Middlewares.Handlers;
using Matrix.Messages;
using NLog;
using Rebus.Handlers;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.MSMQ
{
    public class HeartBeatHandler : Handler, IHandleMessages<HeartBeat>
    {
        public HeartBeatHandler(ILogger logger)
            : base(logger)
        {
        }

        public async Task Handle(HeartBeat o)
        {
            await Task.Run(() =>
            {
                Logger.Info($"HEARTBEAT | {o.Hostname} | {o.Program} | {o.Process}");
            });
        }
    }
}