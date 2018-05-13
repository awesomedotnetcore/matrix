using Matrix.SDK.Middlewares;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Matrix.SDK.Log
{
    public class XLog
    {
        private Guid Application { get; set; }

        private IMiddleware Network { get; set; }

        private string Source { get; set; }

        public XLog(Guid app, string network, string endpoint)
        {
            Application = app;

            Source = Assembly.GetCallingAssembly()?.GetName()?.Name;

            if (!string.IsNullOrEmpty(network))
            {
                var context = new MiddlewareContext(endpoint);

                if (network.Equals("RABBITMQ", StringComparison.CurrentCultureIgnoreCase))
                    Network = new Rabbit(context);

                if (network.Equals("MSMQ", StringComparison.CurrentCultureIgnoreCase))
                    Network = new Msmq(context);
            }
        }

        public async Task Trace(string message)
        {
            await Network.Send(new Messages.Log(Application)
            {
                Level = (int)LogLevel.Trace,
                Message = message,
                Source = Source,
                Event = 0
            });
        }

        public async Task Info(string message)
        {
            await Network.Send(new Messages.Log(Application)
            {
                Level = (int)LogLevel.Info,
                Message = message,
                Source = Source,
                Event = 0
            });
        }

        public async Task Debug(string message)
        {
            await Network.Send(new Messages.Log(Application)
            {
                Level = (int)LogLevel.Debug,
                Message = message,
                Source = Source,
                Event = 0
            });
        }

        public async Task Warn(string message)
        {
            await Network.Send(new Messages.Log(Application)
            {
                Level = (int)LogLevel.Warn,
                Message = message,
                Source = Source,
                Event = 0
            });
        }

        public async Task Error(string message)
        {
            await Network.Send(new Messages.Log(Application)
            {
                Level = (int)LogLevel.Error,
                Message = message,
                Source = Source,
                Event = 0
            });
        }

        public async Task Error(Exception e)
        {
            await Network.Send(new Messages.Log(Application)
            {
                Level = (int)LogLevel.Error,
                Message = e.ToString(),
                Source = Source,
                Event = 0
            });
        }

        public async Task Error(Exception e, string message)
        {
            await Network.Send(new Messages.Log(Application)
            {
                Level = (int)LogLevel.Error,
                Message = $"{message} : {e.ToString()}",
                Source = Source,
                Event = 0
            });
        }
    }
}