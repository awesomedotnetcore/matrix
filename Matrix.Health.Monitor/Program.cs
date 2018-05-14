using EasyNetQ;
using Matrix.Messages;
using Rebus.Activation;
using Rebus.Config;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Matrix.Health.Monitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (ConfigurationManager.AppSettings["matrix.bus.type"].Equals("MSMQ", StringComparison.CurrentCultureIgnoreCase))
            {
                using (var o = new BuiltinHandlerActivator())
                {
                    o.Register(() => new Handler());

                    Configure.With(o)
                        .Transport(i => i.UseMsmq(ConfigurationManager.AppSettings["matrix.bus.url"]))
                        .Logging(i => i.None())
                        .Start();

                    Console.ReadLine();
                }
            }

            if (ConfigurationManager.AppSettings["matrix.bus.type"].Equals("RABBITMQ", StringComparison.CurrentCultureIgnoreCase))
            {
                using (var bus = RabbitHutch.CreateBus(ConfigurationManager.AppSettings["matrix.bus.url"]))
                {
                    bus.SubscribeAsync<HeartBeat>(Process.GetCurrentProcess().ProcessName, new Handler().Handle);

                    Console.ReadLine();
                }
            }
        }
    }
}