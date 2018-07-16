using EasyNetQ;
using Matrix.Agent.Journal.Bus.Handlers;
using Matrix.Agent.Journal.Bus.Responders;
using Matrix.Agent.Journal.Business;
using Matrix.Agent.Journal.Messages;
using Matrix.Agent.Journal.Messages.Commands.Requests;
using Matrix.Agent.Journal.Messages.Commands.Responses;
using Matrix.Agent.Messages;
using Matrix.Agent.Middlewares;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Bus
{
    public class RabbitMiddleware : Middleware
    {
        public override bool Connected { get { return Bus != null && Bus.IsConnected; } }

        private IBus Bus { get; set; }

        private IApplicationService Application { get; }

        private ILogService Server { get; }

        public RabbitMiddleware(IMiddlewareContext context, ILogger logger, IApplicationService application, ILogService server)
            : base(context, logger)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Bus = RabbitHutch.CreateBus(Context.Connection);

                Bus.SubscribeAsync<HeartBeat>(Process.GetCurrentProcess().ProcessName, new HeartBeatHandler(Logger, Application).Handle);
                Bus.SubscribeAsync<Log>(Process.GetCurrentProcess().ProcessName, new LogHandler(Logger, Server).Handle);
                Bus.RespondAsync<GetLogsRequest, GetLogsResponse>(new LogResponder(Logger, Server).GetLogs);
                Bus.RespondAsync<SearchLogsRequest, SearchLogsResponse>(new LogResponder(Logger, Server).SearchLogs);
            });
        }

        public override async Task Disconnect()
        {
            await Task.Run(() => { });
        }

        public override async Task Send<T>(T o, int timeout = 0)
        {
            if (timeout.Equals(0))
            {
                await Bus.PublishAsync(o);
            }
            else
            {
                await Bus.PublishAsync(o, i =>
                {
                    i.WithExpires(timeout);
                });
            }
        }

        public override void Dispose()
        {
            Bus.Dispose();
        }
    }
}