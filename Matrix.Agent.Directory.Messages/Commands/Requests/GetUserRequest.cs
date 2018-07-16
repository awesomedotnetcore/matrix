using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.GetUserRequest", ExchangeName = "Directory.GetUserRequest")]
    public class GetUserRequest : Request
    {
        public Guid Id { get; set; }

        public GetUserRequest()
        {
        }

        public GetUserRequest(Guid app)
            : base(app)
        {
        }
    }
}