using EasyNetQ;
using Matrix.Agent.Messages;
using Matrix.Agent.Middlewares;
using Matrix.Agent.Postman.Bus.Commands.Requests;
using Matrix.Agent.Postman.Bus.Commands.Responses;
using Matrix.Agent.Postman.Bus.Handlers;
using Matrix.Agent.Postman.Bus.Responders;
using Matrix.Agent.Postman.Business.Services;
using NLog;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Bus
{
    public class RabbitMiddleware : Middleware
    {
        public override bool Connected { get { return Bus != null && Bus.IsConnected; } }

        private IBus Bus { get; set; }

        private IApplicationService Application { get; }

        private IEmailService Email { get; }

        private IPhoneService Phone { get; }

        public RabbitMiddleware(IMiddlewareContext context, ILogger logger, IApplicationService application, IEmailService email, IPhoneService phone)
            : base(context, logger)
        {
            Application = application ?? throw new ArgumentNullException(nameof(application));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        }

        public override async Task Connect()
        {
            await Task.Run(() =>
            {
                Bus = RabbitHutch.CreateBus(Context.Connection);
                Bus.SubscribeAsync<HeartBeat>(Process.GetCurrentProcess().ProcessName, new HeartBeatHandler(Logger, Application).Handle);
                Bus.RespondAsync<SendMailRequest, SendMailResponse>(new EmailResponder(Logger, Email).SendMail);
                Bus.RespondAsync<SendMessageRequest, SendMessageResponse>(new PhoneResponder(Logger, Phone).SendMessage);
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