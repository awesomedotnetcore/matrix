using Matrix.Agent.Middlewares;
using Matrix.Agent.Registry.Messages.Commands.Requests;
using Matrix.Messages;
using NLog;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using System.Threading.Tasks;

namespace Matrix.Agent.Registry.Bus.MSMQ
{
    public class MsmqMiddleware : Middleware
    {
        public override bool Connected { get { return Activator != null && Activator.Bus != null; } }

        private BuiltinHandlerActivator Activator { get; set; }

        public MsmqMiddleware(IMiddlewareContext context, ILogger logger)
            : base(context, logger)
        {
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Activator = new BuiltinHandlerActivator();
                Activator.Register(() => new HeartBeatHandler(Logger));

                Configure.With(Activator)
                    .Subscriptions(i => i.StoreInMemory())
                    .Routing(i => i.TypeBased()
                                   .Map<HeartBeat>("matrix.heartbeat")
                                   .Map<ListApplicationRequest>("matrix.application.list")
                                   .Map<RegisterApplicationRequest>("matrix.application.create")
                                   .Map<UpdateApplicationRequest>("matrix.application.update")
                                   .Map<DeleteApplicationRequest>("matrix.application.delete"))
                    .Transport(i => i.UseMsmq(Context.Connection))
                    .Options(i => i.SetNumberOfWorkers(2))
                    .Options(i => i.SimpleRetryStrategy())
                    .Logging(i => i.ColoredConsole())
                    .Start();

                Activator.Bus.Subscribe<HeartBeat>();
                Activator.Bus.Subscribe<Log>();
                Activator.Bus.Subscribe<ListApplicationRequest>();
                Activator.Bus.Subscribe<RegisterApplicationRequest>();
                Activator.Bus.Subscribe<UpdateApplicationRequest>();
                Activator.Bus.Subscribe<DeleteApplicationRequest>();
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