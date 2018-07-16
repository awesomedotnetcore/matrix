using System;

namespace Matrix.Agent.Messages.Commands.Responses
{
    public class Response : Command, IResponse
    {
        public Guid RequestId { get; set; }

        public Guid ResponseId { get; set; }

        public int Status { get; set; }

        public bool Error { get; set; }

        public Response(Guid requestId)
        {
            RequestId = requestId;
            ResponseId = Guid.NewGuid();
        }

        public Response(Guid requestId, Guid app)
            : base(app)
        {
            RequestId = requestId;
            ResponseId = Guid.NewGuid();
        }
    }
}