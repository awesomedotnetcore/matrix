using Matrix.Agent.Middlewares.Responders;
using Matrix.Agent.Postman.Bus.Commands.Requests;
using Matrix.Agent.Postman.Bus.Commands.Responses;
using Matrix.Agent.Postman.Business.Services;
using NLog;
using System;
using System.Threading.Tasks;

namespace Matrix.Agent.Postman.Bus.Responders
{
    public class PhoneResponder : Endpoint
    {
        private IPhoneService Server { get; }

        public PhoneResponder(ILogger logger, IPhoneService server)
            : base(logger)
        {
            Server = server ?? throw new ArgumentNullException(nameof(server));
        }

        public async Task<SendMessageResponse> SendMessage(SendMessageRequest o)
        {
            var result = new SendMessageResponse(o.RequestId);

            result.Id = await Server.SendMessage(o.Application, o.From, o.To, o.Message);

            return result;
        }
    }
}