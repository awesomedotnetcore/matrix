using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.DeleteUserRoleResponse", ExchangeName = "Directory.DeleteUserRoleResponse")]
    public class DeleteUserRoleResponse : Response
    {
        public bool Deleted { get; set; }

        public DeleteUserRoleResponse()
            : base(Guid.Empty)
        {
        }

        public DeleteUserRoleResponse(Guid requestId)
           : base(requestId)
        {
        }

        public DeleteUserRoleResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}