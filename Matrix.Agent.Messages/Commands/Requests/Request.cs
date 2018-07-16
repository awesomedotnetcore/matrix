using System;

namespace Matrix.Agent.Messages.Commands.Requests
{
    public abstract class Request : Command, IRequest
    {
        public Guid RequestId { get; set; }

        public Request()
        {
            RequestId = Guid.NewGuid();
        }

        public Request(Guid app)
            : base(app)
        {
            RequestId = Guid.NewGuid();
        }
    }
}