using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.ListUserRequest", ExchangeName = "Directory.ListUserRequest")]
    public class ListUserRequest : Request
    {
        public ListUserRequest()
        {
        }

        public ListUserRequest(Guid app)
            : base(app)
        {
        }
    }
}