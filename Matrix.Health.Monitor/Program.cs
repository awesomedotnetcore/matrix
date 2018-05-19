using EasyNetQ;
using Matrix.Agent.Messages;
using System;
using System.Configuration;
using System.Diagnostics;

namespace Matrix.Health.Monitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus(ConfigurationManager.AppSettings["matrix.bus.url"]))
            {
                bus.SubscribeAsync<HeartBeat>(Process.GetCurrentProcess().ProcessName, new Handler().Handle);

                Console.ReadLine();
            }
        }
    }
}