using EasyNetQ;
using System.Threading.Tasks;

namespace Matrix.SDK.Middlewares
{
    public class Rabbit : Middleware
    {
        public override bool Connected { get { return Bus != null && Bus.IsConnected; } }

        private IBus Bus { get; set; }

        public Rabbit(IMiddlewareContext context)
            : base(context)
        {
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Bus = RabbitHutch.CreateBus(Context.Connection);
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