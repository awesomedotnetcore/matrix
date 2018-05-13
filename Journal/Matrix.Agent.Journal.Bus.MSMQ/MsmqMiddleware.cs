using Matrix.Agent.Journal.Bus.MSMQ.Handlers;
using Matrix.Agent.Journal.Business.Interfaces;
using Matrix.Agent.Middlewares;
using Matrix.Messages;
using NLog;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Journal.Bus.MSMQ
{
    public class MsmqMiddleware : Middleware
    {
        public override bool Connected { get { return Activator != null && Activator.Bus != null; } }

        private BuiltinHandlerActivator Activator { get; set; }

        private IApplicationService Application { get; }

        private ILogService Server { get; set; }

        public MsmqMiddleware(IMiddlewareContext context, ILogger logger, IApplicationService application, ILogService server)
            : base(context, logger)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Activator = new BuiltinHandlerActivator();
                Activator.Register(() => new HeartBeatHandler(Logger, Application));
                Activator.Register(() => new LogHandler(Logger, Server));

                Configure.With(Activator)
                    .Subscriptions(i => i.StoreInMemory())
                    .Routing(i => i.TypeBased()
                                   .Map<HeartBeat>("matrix.heartbeat")
                                   .Map<Messages.Log>("matrix.log"))
                    .Transport(i => i.UseMsmq(Context.Connection))
                    .Options(i => i.SetNumberOfWorkers(2))
                    .Options(i => i.SimpleRetryStrategy())
                    .Logging(i => i.ColoredConsole())
                    .Start();

                Activator.Bus.Subscribe<HeartBeat>();
                Activator.Bus.Subscribe<Messages.Log>();
            });
        }

        public override async Task Disconnect()
        {
            await Task.Run(() => { });
        }

        public override async Task Send<T>(T o, int timeout = 0)
        {
            await Activator.Bus.Send(o);
        }

        public override void Dispose()
        {
            if (Activator != null)
                Activator.Dispose();
        }
    }
}