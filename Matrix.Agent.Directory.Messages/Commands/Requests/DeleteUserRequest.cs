using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.DeleteUserRequest", ExchangeName = "Directory.DeleteUserRequest")]
    public class DeleteUserRequest : Request
    {
        public Guid Id { get; set; }

        public DeleteUserRequest()
        {
        }

        public DeleteUserRequest(Guid app)
            : base(app)
        {
        }
    }
}