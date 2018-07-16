using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.UpdateUserRoleResponse", ExchangeName = "Directory.UpdateUserRoleResponse")]
    public class UpdateUserRoleResponse : Response
    {
        public bool Updated { get; set; }

        public UpdateUserRoleResponse()
            : base(Guid.Empty)
        {
        }

        public UpdateUserRoleResponse(Guid requestId)
           : base(requestId)
        {
        }

        public UpdateUserRoleResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}