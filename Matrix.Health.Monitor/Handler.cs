using Matrix.Messages;
using NLog;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace Matrix.Health.Monitor
{
    public class Handler : IHandleMessages<HeartBeat>
    {
        private ILogger Log { get; set; }

        public async Task Handle(HeartBeat o)
        {
            Log = LogManager.GetLogger(GetType().Namespace);

            await Task.Run(() =>
            {
                var summary = o.Warnings.Count.Equals(0) && o.Errors.Count.Equals(0) ? "No errors or warnings" : $"ATTENTION: {o.Errors.Count} errors and {o.Warnings.Count} warnings found";

                var time = DateTime.Now.Subtract(o.Timestamp).TotalMilliseconds.ToString("F");

                Log.Info($"{o.Hostname} | {o.Program} [{o.Process}] | {time} ms | {summary}");
            });
        }
    }
}