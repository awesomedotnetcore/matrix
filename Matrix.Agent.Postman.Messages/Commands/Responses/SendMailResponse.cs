using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Postman.Bus.Commands.Responses
{
    public class SendMailResponse : Response
    {
        public Guid Id { get; set; }

        public SendMailResponse(Guid requestId)
            : base(requestId)
        {
        }

        public SendMailResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}