using Matrix.Messages;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Persistence.InMem;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using System.Threading.Tasks;

namespace Matrix.SDK.Middlewares
{
    public class Msmq : Middleware
    {
        public override bool Connected { get { return Activator != null && Activator.Bus != null; } }

        private BuiltinHandlerActivator Activator { get; set; }

        public Msmq(IMiddlewareContext context)
            : base(context)
        {
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Activator = new BuiltinHandlerActivator();

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