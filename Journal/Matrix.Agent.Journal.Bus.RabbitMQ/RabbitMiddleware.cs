using EasyNetQ;
using Matrix.Agent.Journal.Bus.RabbitMQ.Handlers;
using Matrix.Agent.Journal.Business.Interfaces;
using Matrix.Agent.Middlewares;
using Matrix.Messages;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Bus.RabbitMQ
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
                Bus.SubscribeAsync<Messages.Log>(Process.GetCurrentProcess().ProcessName, new LogHandler(Logger, Server).Handle);
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