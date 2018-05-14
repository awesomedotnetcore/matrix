using Matrix.Agent.Journal.Business;
using Matrix.Agent.Middlewares.Handlers;
using NLog;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Bus.MSMQ.Handlers
{
    public class LogHandler : Handler, IHandleMessages<Messages.Log>
    {
        public ILogService Service { get; }

        public LogHandler(ILogger logger, ILogService service)
            : base(logger)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task Handle(Messages.Log o)
        {
            if (o != null && Service != null)
                await Service.CreateLogEntry(o.Application, o.Timestamp, o.Source, o.Level, o.Event, o.Message, o.Properties, o.Tags);
        }
    }
}