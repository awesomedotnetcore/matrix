using EasyNetQ;
using Matrix.Agent.Messages.Commands.Requests;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Requests
{
    [Queue("Directory.DeleteUserGroup", ExchangeName = "Directory.DeleteUserGroup")]
    public class DeleteUserGroupRequest : Request
    {
        public Guid Id { get; set; }

        public DeleteUserGroupRequest()
        {
        }

        public DeleteUserGroupRequest(Guid app)
            : base(app)
        {
        }
    }
}