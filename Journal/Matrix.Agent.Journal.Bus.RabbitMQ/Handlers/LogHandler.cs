using Matrix.Agent.Journal.Business.Interfaces;
using Matrix.Agent.Middlewares.Handlers;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Bus.RabbitMQ.Handlers
{
    public class LogHandler : Handler
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