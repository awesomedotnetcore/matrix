using System;

namespace Matrix.Messages.Commands.Requests
{
    public abstract class Request : Command, IRequest
    {
        public Request()
        {
        }

        public Request(Guid app)
            : base(app)
        {
        }
    }
}