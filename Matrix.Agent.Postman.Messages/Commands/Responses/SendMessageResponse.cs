using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Postman.Bus.Commands.Responses
{
    public class SendMessageResponse : Response
    {
        public Guid Id { get; set; }

        public SendMessageResponse(Guid requestId)
            : base(requestId)
        {
        }

        public SendMessageResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}