using Matrix.Agent.Middlewares.Responders;
using Matrix.Agent.Postman.Bus.Commands.Requests;
using Matrix.Agent.Postman.Bus.Commands.Responses;
using Matrix.Agent.Postman.Business.Services;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Bus.Responders
{
    public class EmailResponder : Endpoint
    {
        private IEmailService Server { get; }

        public EmailResponder(ILogger logger, IEmailService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<SendMailResponse> SendMail(SendMailRequest o)
        {
            var result = new SendMailResponse(o.RequestId);

            result.Id = await Server.SendEmail(o.Application, o.From, o.To, o.Cc, o.Bcc, o.Subject, o.Body, o.Html);

            return result;
        }
    }
}