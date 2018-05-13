using EasyNetQ;
using Matrix.Agent.Middlewares;
using Matrix.Agent.Registry.Bus.RabbitMQ.Handlers;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using Matrix.Messages;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.RabbitMQ
{
    public class RabbitMiddleware : Middleware
    {
        public override bool Connected { get { return Bus != null && Bus.IsConnected; } }

        private IBus Bus { get; set; }

        private IRegistryService Server { get; }

        public RabbitMiddleware(IMiddlewareContext context, ILogger logger, IRegistryService server)
            : base(context, logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Bus = RabbitHutch.CreateBus(Context.Connection);
                Bus.SubscribeAsync<HeartBeat>(Process.GetCurrentProcess().ProcessName, new HeartBeatHandler(Logger).Execute);
                Bus.RespondAsync<ListApplicationRequest, ListApplicationResponse>(new ListApplicationHandler(Logger, Server).Execute);
                Bus.RespondAsync<RegisterApplicationRequest, CreateApplicationResponse>(new RegisterApplicationHandler(Logger, Server).Execute);
                Bus.RespondAsync<UpdateApplicationRequest, UpdateApplicationResponse>(new UpdateApplicationHandler(Logger, Server).Execute);
                Bus.RespondAsync<DeleteApplicationRequest, DeleteApplicationResponse>(new DeleteApplicationHandler(Logger, Server).Execute);
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