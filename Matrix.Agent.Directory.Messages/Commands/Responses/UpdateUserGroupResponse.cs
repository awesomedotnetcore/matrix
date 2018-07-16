using EasyNetQ;
using Matrix.Agent.Messages.Commands.Responses;
using System;

namespace Matrix.Agent.Directory.Messages.Commands.Responses
{
    [Queue("Directory.UpdateUserGroupResponse", ExchangeName = "Directory.UpdateUserGroupResponse")]
    public class UpdateUserGroupResponse : Response
    {
        public bool Updated { get; set; }

        public UpdateUserGroupResponse()
            : base(Guid.Empty)
        {
        }

        public UpdateUserGroupResponse(Guid requestId)
           : base(requestId)
        {
        }

        public UpdateUserGroupResponse(Guid requestId, Guid app)
            : base(requestId, app)
        {
        }
    }
}