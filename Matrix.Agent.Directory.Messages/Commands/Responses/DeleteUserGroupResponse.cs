using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.DeleteUserGroupResponse", ExchangeName = "Directory.DeleteUserGroupResponse")]
    public class DeleteUserGroupResponse : Response
    {
        public bool Deleted { get; set; }

        public DeleteUserGroupResponse()
            : base(Guid.Empty)
        {
        }

        public DeleteUserGroupResponse(Guid requestId)
           : base(requestId)
        {
        }

        public DeleteUserGroupResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}