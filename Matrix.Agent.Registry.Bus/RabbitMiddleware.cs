using EasyNetQ;
using Matrix.Agent.Messages;
using Matrix.Agent.Middlewares;
using Matrix.Agent.Registry.Bus.Handlers;
using Matrix.Agent.Registry.Bus.Responders;
using Matrix.Agent.Registry.Business;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Agent.Registry.Messages.Commands.Responses;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus
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

                var application = new ApplicationResponder(Logger, Server);
                Bus.RespondAsync<ListApplicationRequest, ListApplicationResponse>(application.GetApplications);
                Bus.RespondAsync<GetApplicationByIdRequest, GetApplicationByIdResponse>(application.GetApplicationById);
                Bus.RespondAsync<RegisterApplicationRequest, CreateApplicationResponse>(application.Register);
                Bus.RespondAsync<UpdateApplicationRequest, UpdateApplicationResponse>(application.Update);
                Bus.RespondAsync<DeleteApplicationRequest, DeleteApplicationResponse>(application.Delete);
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